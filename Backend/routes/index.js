const express = require('express');
const router = express.Router();

let db;

const response = data => ({ message: data });

router.post('/api/login', async(req, res) => {
    const { username, pwd } = req.body;

    return db.checkUser(username, pwd)
        .then(() => res.send(response('Logged in successfully!')))
        .catch(e => {
            console.log(e);
            return res.status(500).send(response('Something went wrong!'));
        });
});


router.post('/api/register', async(req, res) => {
    const { username, pwd } = req.body;

    return db.addUser(username, pwd)
        .then(() => res.send(response('User created successfully!')))
        .catch(e => {
            console.log(e);
            return res.status(500).send(response('Something went wrong!'));
        });
});

module.exports = database => {
    db = database;
    return router;
};