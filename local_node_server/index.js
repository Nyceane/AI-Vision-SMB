var express = require('express');
var app = express();
var path = require('path');

app.use(express.static('views'));

// viewed at http://localhost:8080
app.get('/', function(req, res) {
    res.sendFile(path.join(__dirname + '/views/index.html'));
});

app.listen(8080);
