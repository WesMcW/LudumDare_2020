// JavaScript source code
var app = require('express')();
var server = require('http').Server(app);
var io = require('socket.io')(server);

const PORT = process.env.PORT || 3000;

app.get('/', function (req, res) {
    res.sendFile(__dirname + '/index.html');
});

var HighScores = new Map();  // declaring Users structure


io.sockets.on('connection', (socket) => {
    console.log('a user connected');

    socket.emit('connectionmessage', {
        id: socket.id
    });

    socket.on('LoadScores', () => {
        var i;
        var names;
        var scores;
        var start = HighScores.keys().next();

        for (i = 0; i < 5; i++) {
            names[i] = start.key;
            scores[i] = start.value;

            start = Highscores[start.key].keys().next();
        }

        io.emit('LoadScores', {
            names: names,
            scores: scores
        });
    });

    socket.on('EnterScore', (score) => {
        HighScores.set(
            score.name,
            score.number
        );

        HighScores = new Map(HighScores.sort());
    });

    socket.on('disconnect', () => {
        console.log('user disconnected');
        removeUser(socket);
    });
});

server.listen(PORT);