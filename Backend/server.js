const express = require("express");

const app = express();
const routes = require('./routes/index.js');
const Database = require('./database');

const db = new Database('users.db');
app.use(express.json());
app.use(express.urlencoded({ extended: false }));
app.use(routes(db));

app.get('/', (req, res) => {
    return res.status(200).send({
        message: 'Server is Running'
    });
});

(async() => {
    await db.connect();
    await db.migrate(); // just for testing
    app.listen(process.env.PORT || 8080, '0.0.0.0', () => console.log('Listening on port 8080'));
})(); 