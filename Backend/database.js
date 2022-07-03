const sqlite = require('sqlite-async');
const crypto = require('crypto');
const e = require('express');

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
				salt         VARCHAR(255)
            );
        `);
    }

    async checkUser(username, pwd) {
        const hash = crypto.createHash('sha256').update(pwd).digest('hex');
        return new Promise(async(resolve, reject) => {
            try {
                let stmt = await this.db.prepare('SELECT hash FROM users WHERE username = ?');
                stmt.get(username)
                    .then(rows => {
                        if (rows == undefined) {
                            reject("Invalid user");
                        } else if (rows.hash == hash) {
                            resolve();
                        } else {
                            reject("Wrong password");
                        }
                    })
            } catch (e) {
                reject(e);
            }
        });
    }

    async addUser(username, pwd) {
        const hash = crypto.createHash('sha256').update(pwd).digest('hex');
        return new Promise(async(resolve, reject) => {
            try {
                let stmt = await this.db.prepare('INSERT INTO users (username, hash) VALUES( ?, ? )');
                resolve(stmt.run(username, hash));
            } catch (e) {
                reject(e);
            }
        });
    }

    async modifyUser(username, pwd) {
        const hash = crypto.createHash('sha256').update(pwd).digest('hex');
        return new Promise(async(resolve, reject) => {
            try {
                const newHash = crypto.createHash('sha256').update(pwd).digest('hex');
                let stmt = await this.db.prepare('UPDATE users SET hash = ? WHERE username = ?');
                resolve(stmt.run(newHash, username));
            } catch (e) {
                reject(e)
            }
        });
    }
}

module.exports = Database;