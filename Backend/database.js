const sqlite = require('sqlite-async');
const crypto = require('crypto');
const { randomAsciiString } = require('./scramhelper');

class Database {
    constructor(db_file) {
        this.db_file = db_file;
        this.db = undefined;
    }

    async connect() {
        this.db = await sqlite.open(this.db_file);
    }

    async migrate() {
        return this.db.exec(`
            DROP TABLE IF EXISTS users;

            CREATE TABLE users (
                id           INTEGER      NOT NULL PRIMARY KEY AUTOINCREMENT,
				username     VARCHAR(255) NOT NULL UNIQUE,
                hash         VARCHAR(255) NOT NULL,
                serverKey    VARCHAR(255) NOT NULL,
				salt         VARCHAR(255) NOT NULL,
				i			 INTEGER      NOT NULL
            );
        `);
    }

    async getUserScramDetails(username) {
        return new Promise(async(resolve, reject) => {
            try {
                let stmt = await this.db.prepare('SELECT salt, i FROM users WHERE username = ?');
                stmt.get(username)
                    .then(rows => {
                        if (rows == undefined) {
                            reject("Invalid user");
                        } else {
                            resolve(rows);
                        }
                    });
            } catch (e) {
                reject(e);
            }
        });
    }

    async getUserScramHash(username) {
        return new Promise(async (resolve, reject) => {
            try {
                let stmt = await this.db.prepare('SELECT hash, serverKey FROM users WHERE username = ?');
                stmt.get(username)
                    .then(rows => {
                        if (rows == undefined) {
                            reject("Invalid user");
                        } else {
                            resolve(rows);
                        }
                    });
            } catch (e) {
                reject(e);
            }
        });
    }

    async addUser(username, pwd) {
        const salt = randomAsciiString(22);
        const iterationCount = crypto.randomInt(2048, 4096);

        const saltedPassword = crypto.pbkdf2Sync(pwd, salt, iterationCount, 32, 'sha256');
        const clientKey = crypto.createHmac('sha256', saltedPassword).update('Client Key').digest();
        const serverKey = crypto.createHmac('sha256', saltedPassword).update('Server Key').digest('hex');
        const hash = crypto.createHash('sha256').update(clientKey).digest('hex');

        return new Promise(async (resolve, reject) => {
            try {
                let stmt = await this.db.prepare('INSERT INTO users (username, hash, serverKey, salt, i) VALUES ( ?, ?, ?, ?, ? )');
                resolve(stmt.run(username, hash, serverKey, salt, iterationCount));
            } catch (e) {
                reject(e);
            }
        });
    }

    async isUserPresent(username) {
        return new Promise(async (resolve, reject) => {
            try {
                let stmt = await this.db.prepare('SELECT id FROM users WHERE username = ?');
                let rows = await stmt.run(username);
                if (rows.changes == 0) {
                    reject("User does not exist");
                } else {
                    resolve();
                }
            } catch (e) {
                reject(e);
            }
        }) 
    }

    async modifyUser(username, pwd) {
        try {
            await this.isUserPresent(username);
        } catch (e) {
            return Promise.reject(e);
        }

        const salt = randomAsciiString(22);
        const iterationCount = crypto.randomInt(2048, 4096);

        const saltedPassword = crypto.pbkdf2Sync(pwd, salt, iterationCount, 32, 'sha256');
        const clientKey = crypto.createHmac('sha256', saltedPassword).update('Client Key').digest();
        const serverKey = crypto.createHmac('sha256', saltedPassword).update('Server Key').digest('hex');
        const hash = crypto.createHash('sha256').update(clientKey).digest('hex');

        return new Promise(async(resolve, reject) => {
            try {
                let stmt = await this.db.prepare('UPDATE users SET hash = ?, serverKey = ?, salt = ?, i = ? WHERE username = ?');
                resolve(stmt.run(hash, serverKey, salt, iterationCount, username));
            } catch (e) {
                reject(e)
            }
        });
    }
}

module.exports = Database;