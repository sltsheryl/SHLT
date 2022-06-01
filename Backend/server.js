const express = require("express");
const app = express();
// const routes = require('./routes/index.js');
const Database = require('./database');

const db = new Database('users.db');
app.use(express.json());
app.use(express.urlencoded({ extended: false }))
    // app.use(routes(db));

app.get('/', (req, res) => {
    return res.status(200).send({
        message: 'Server is Running'
    });
});

app.post('/api/login', async(req, res) => {
    const { username, pwd } = req.body;

    return db.checkUser(username, pwd)
        .then(() => res.send('Logged in successfully!'))
        .catch(e => {
            console.log(e);
            if (e == "Invalid user") {
                return res.status(500).send('Invalid user');
            } else {
                return res.status(500).send('Wrong password');
            }
        });
});


app.post('/api/register', async(req, res) => {
    const { username, pwd } = req.body;

    return db.addUser(username, pwd)
        .then(() => res.send('User created successfully!'))
        .catch(e => {
            console.log(e);
            return res.status(500).send(response('Something went wrong!'));
        });
});

(async() => {
    await db.connect();
    await db.migrate(); // just for testing
    app.listen(8080, '0.0.0.0', () => console.log('Listening on port 8080'));
})();