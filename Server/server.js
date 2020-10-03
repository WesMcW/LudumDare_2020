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

    socket.on('LoadTheScore', (place) => {
        console.log(HighScores.size + ', ' + place);

        var score = GetScore(place);
        console.log(score.key + " | " + score.value);

        if (score.value > 0) {
            console.log('sending score...');

            io.emit('LoadScore', {
                name: score.key,
                score: score.value
            });
        }
    });

    socket.on('EnterScore', (score) => {
        //var thing = JSON.parse(score);

        console.log(score);

        HighScores.set(
            score.name,
            score.score
        );

        //HighScores = new Map([HighScores.entries()].sort((a, b) => a[1] - b[1]));

        console.log(score.name + ' | ' + HighScores.get(score.name) + ' saved.');
    });

    socket.on('disconnect', () => {
        console.log('user disconnected');
    });

    function GetScore(placement) {
        // 1 = 1st, 2 = 2nd...
        if (HighScores.size >= placement) {
            var key = Array.from(HighScores.keys())[placement - 1];
            return {
                key: key,
                value: HighScores.get(key)
            };
        }
        else {
            return {
                key: 'none',
                value: -1
            };
        }
    }
});

server.listen(PORT);