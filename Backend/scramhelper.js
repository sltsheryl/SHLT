const { Buffer } = require('node:buffer');
const crypto = require('crypto');

function randomString(length, chars) {
	if (!chars) {
		throw new Error('Argument \'chars\' is undefined');
	}

	const charsLength = chars.length;
	if (charsLength > 256) {
		throw new Error('Argument \'chars\' should not have more than 256 characters'
			+ ', otherwise unpredictability will be broken');
	}

	const randomBytes = crypto.randomBytes(length);
	let result = new Array(length);

	let cursor = 0;
	for (let i = 0; i < length; i++) {
		cursor += randomBytes[i];
		result[i] = chars[cursor % charsLength];
	}

	return result.join('');
}

function randomAsciiString(length) {
	return randomString(length,
		'abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789');
}

function bufferXOR(buf1, buf2) {
	const result = Buffer.alloc(buf1.length);
	for (let i = 0; i < buf1.length; i++) {
		result[i] = buf1[i] ^ buf2[i];
	}
	return result;
}

function parseAuthorizationHeader(authorizationHeader) {
	const clientDataPattern = /data=([a-zA-Z0-9+/]+={0,2})/g;
	const clientSidPattern = /sid=([a-zA-Z0-9]{16})/g;

	const params = authorizationHeader.split(" ");
	if (params.length < 2 || params.length > 3) {
		return null;
	}

	if (params.length == 2) {
		if (params[0] != "SCRAM-SHA-256" || !params[1].startsWith("data=")) {
			return null;
		}
		
		let clientData = clientDataPattern.exec(params[1])[1];
		if (clientData == null) {
			return null;
		}

		const buff = Buffer.from(clientData, "base64");
		clientData = buff.toString("ascii");
		return { "data": clientData }
    }

	if (params.length == 3) {
		if (params[0] != "SCRAM-SHA-256" || !params[1].startsWith("sid=") || !params[2].startsWith("data=")) {
			return null;
		}

		let clientSid = clientSidPattern.exec(params[1])[1];
		if (clientSid == null) {
			return null;
		}

		let clientData = clientDataPattern.exec(params[2])[1];
		if (clientData == null) {
			return null;
		}

		const buff = Buffer.from(clientData, "base64");
		clientData = buff.toString("ascii");
		return { "data": clientData, "sid": clientSid };
    }
}

function parseClientFirst(dataString) {
	const clientFirstPattern = /n,,n=([a-zA-Z0-9]+),r=([a-zA-Z0-9]+)/g;

	const clientFirstParams = clientFirstPattern.exec(dataString);
	if (clientFirstParams != null) {
		const username = clientFirstParams[1];
		const cNonce = clientFirstParams[2];
		return { "username": username, "cNonce": cNonce };
	}
	return null;
}

async function handleClientFirst(username, cNonce, db) {
	const details = await db.getUserScramDetails(username);
	const { salt, i } = details;
	const sNonce = randomAsciiString(32);
	const r = `${cNonce}%${sNonce}`;
	const serverFirstData = `r=${r},s=${salt},i=${i}`;

	const buff = Buffer.from(serverFirstData, "ascii");
	serverFirst = buff.toString("base64");

	return serverFirst;
}

function parseClientFinal(dataString) {
	const clientFinalPattern = /c=([a-zA-Z0-9/+]+={0,2}),r=([a-zA-Z0-9]+%[a-zA-Z0-9]+),p=([a-zA-Z0-9/+]+={0,2})/g;

	const clientFinalParams = clientFinalPattern.exec(dataString);
	if (clientFinalParams != null) {
		const gs2 = clientFinalParams[1];
		const nonce = clientFinalParams[2];
		const clientProof = clientFinalParams[3];
		return { "gs2": gs2, "nonce": nonce, "clientProof": clientProof };
	}
	return null;
}

async function handleClientFinal(gs2, cNonce, nonce, clientProof, username, db) {
	const buff = Buffer.from(clientProof, "base64");

	const details = await db.getUserScramDetails(username);
	const { salt, i } = details;
	const authString = `r=${cNonce},r=${nonce},s=${salt},i=${i},c=${gs2},r=${nonce}`;

	const hashDetails = await db.getUserScramHash(username);
	const hash = Buffer.from(hashDetails.hash, 'hex');
	const clientKey = bufferXOR(buff, crypto.createHmac('sha256', hash).update(authString).digest());

	if (hashDetails.hash == crypto.createHash('sha256').update(clientKey).digest('hex')) {
		const serverKey = Buffer.from(hashDetails.serverKey, 'hex');
		const serverSignature = crypto.createHmac('sha256', serverKey).update(authString).digest().toString("base64");
		const serverFinalData = `v=${serverSignature}`;

		const serverFinalBuff = Buffer.from(serverFinalData, "utf-8");
		const serverFinal = serverFinalBuff.toString("base64");
		return serverFinal;
	} else {
		return null;
    }
}

module.exports = {
	parseAuthorizationHeader,
	parseClientFirst,
	handleClientFirst,
	parseClientFinal,
	handleClientFinal,
	randomAsciiString
};