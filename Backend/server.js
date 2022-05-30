const express = require("express");
const app = express();
const routes = require('./routes/index.js');
const Database = require('./database');

const db = new Database('users.db');
app.use(express.json());

app.use(routes(db));

app.all('*', (req, res) => {
	return res.status(404).send({
		message: '404 Not Found'
	});
});

(async () => {
	await db.connect();
	await db.migrate();  // just for testing
	app.listen(8080, '0.0.0.0', () => console.log('Listening on port 8080'));
})();