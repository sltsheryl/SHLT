const express = require('express');
const { parseAuthorizationHeader, parseClientFirst, handleClientFirst, parseClientFinal, handleClientFinal, randomAsciiString } = require("../scramhelper");

const router = express.Router();

let db;

let sessions = {}
router.get('/api/login', async (req, res) => {
    if (req.headers.authorization) {
        parsed = parseAuthorizationHeader(req.headers.authorization);
        if (parsed == null) {
            return res.status(401).send("Malformed request");
        }
        if (parsed.sid == null) {  // client-first
            parsed = parseClientFirst(parsed.data);
            if (parsed == null) {
                return res.status(401).send("Malformed request");
            }
            let serverFirstData;
            try {
                serverFirstData = await handleClientFirst(parsed.username, parsed.cNonce, db);
            } catch (e) {
                console.log(e);
                return res.status(401).send("Authentication failed");
            }
            
            if (serverFirstData != null) {
                sid = randomAsciiString(16);
                sessions[sid] = { ...parsed, stage: "client-final" };

                res.set("WWW-Authenticate", `SCRAM-SHA-256 sid=${sid} data=${serverFirstData}`);
                return res.status(401).send();
            }
        } else if (sessions[parsed.sid] != null && sessions[parsed.sid].stage == "client-final") {  // client-final
            sid = parsed.sid;
            parsed = parseClientFinal(parsed.data);
            if (parsed == null) {
                return res.status(401).send("Malformed request");
            }

            let serverFinal;
            try {
                serverFinal = await handleClientFinal(parsed.gs2, sessions[sid].cNonce, parsed.nonce, parsed.clientProof, sessions[sid].username, db);
                delete sessions[sid];
                if (serverFinal != null) {
                    res.set("Authentication-Info", `sid=${sid} data=${serverFinal}`);
                    return res.status(200).send();
                } else {
                    return res.status(401).send("Authentication failed");
                }
            } catch (e) {
                return res.status(401).send("Authentication failed");
            }
        }
    }
    return res.status(401).send("Unknown request");
});

router.post('/api/register', async (req, res) => {
    const { username, pwd } = req.body;

    return db.addUser(username, pwd)
        .then(() => {
            console.log(`Successful registration ${username}`);
            res.send('User created successfully!')
        })
        .catch(e => {
            console.log(e);
            return res.status(500).send('Something went wrong!');
        });
});

router.post('/api/resetpassword', async (req, res) => {
    const { username, pwd } = req.body;
    return db.modifyUser(username, pwd)
        .then(() => {
            console.log("Success reset")
            res.send('User modified successfully!')
        })
        .catch(e => {
            console.log(e);
            if (e == "Invalid user") {
                return res.status(500).send('Invalid user');
            } else {
                return res.status(500).send('Something went wrong!');
            }
        });
});

module.exports = database => {
    db = database;
    return router;
};