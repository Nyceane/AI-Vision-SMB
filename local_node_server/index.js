//
// # SimpleServer
//
// A simple chat server using Socket.IO, Express, and Async.
//
var http = require('http');
var path = require('path');

var express = require('express');

var net = require('net');
var client = new net.Socket();

client.connect(5000, '192.168.1.81', function() {
  console.log('Connected');
});

client.on('close', function() {
  console.log('Connection closed');
});
var router = express();
var server = http.createServer(router);

router.use(express.static(path.resolve(__dirname, 'client')));

var status = "empty";

router.use(function(req, res, next) {
  res.header("Access-Control-Allow-Origin", "*");
  res.header("Access-Control-Allow-Headers", "Origin, X-Requested-With, Content-Type, Accept");
  next();
});


router.get('/test', function (req, res) {
    res.send('id: ' + req.query.id);
});


router.get('/setCamera', function (req, res) {
  var query = req.query.id + '\n';
  client.write(query);
  res.send(query);
});


server.listen(process.env.PORT || 8888, process.env.IP || "0.0.0.0", function(){
  var addr = server.address();
  console.log("Chat server listening at", addr.address + ":" + addr.port);
});
