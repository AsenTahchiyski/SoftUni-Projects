function Card(value, name, qty, description) {
    this.value = value;
    this.name = name;
    this.qty = qty;
    this.picture = 'resources/cardFaces/' + this.name.toLowerCase() + '.png';
    this.description = description;
    this.sound = 'resources/cardSounds/' + this.name.toLowerCase() + '.wav';
}

Card.prototype.toString = function () {
    return '(' + this.value + ')' + this.name + ': ' + this.description;
};

function Player(name) {
    this.name = name;
    this.hand = [];
    this.discardPile = [];
    this.points = 0;
    this.isAlive = true;
    this.isProtected = false;
    this.enemyHands = {};
}

function generateDeck() {
    var guard = new Card(1, 'Guard', 5, 'Name a non-Guard card and choose another player. If that player has that card, he or she is out of the round.');
    var priest = new Card(2, 'Priest', 2, 'Look at another player\'s hand.');
    var baron = new Card(3, 'Baron', 2, 'You and another player secretly compare hands. The player with the lower value is out of the round.');
    var handmaid = new Card(4, 'Handmaid', 2, 'Until your next turn, ignore all effects from other players\' actions.');
    var prince = new Card(5, 'Prince', 2, 'Choose any player (including yourself) to discard his or her hand and draw a new card.');
    var king = new Card(6, 'King', 1, 'Trade hands with another player of your choice.');
    var countess = new Card(7, 'Countess', 1, 'If you have this card and the King or Prince in your hand, you must discard this card.');
    var princess = new Card(8, 'Princess', 1, 'If you discard this card, you are out of the round.');
    var allCardTypes = [guard, priest, baron, handmaid, prince, king, countess, princess];
    var deck = [];
    for (var card = 0; card < allCardTypes.length; card++) {
        for (var i = 0; i < allCardTypes[card].qty; i++) {
            deck.push(allCardTypes[card]);
        }
    }

    return deck;
}

function drawText(currentCard, context){
    context.clearRect(0,0,context.canvas.width, context.canvas.height);
    context.font = '30pt Arial';
    context.fillStyle = getLinearGradient(context);
    context.fillText(currentCard.toString(),100,500);

}

function playSound(soundResource) {
    if (soundResource) {
        var currentAudio = new Audio();
        currentAudio.src = soundResource;
        currentAudio.play();
    }
}
// My edit ---
function drawCard(currentCard) {
    if (Modernizr.canvas) {
        var cardCanvas = document.getElementById('cardCanvas');
        var context = cardCanvas.getContext('2d');

        if (context) {
            //var currentCard = drawRandomCard();
            if (currentCard) {
                var currentImage = new Image();
                currentImage.onload = function () {
                    context.drawImage(currentImage, 0, 0, 400, 400);
                };
                currentImage.src = currentCard.picture;
                //playSound(currentCard.sound);
                drawText(currentCard, context);
                //alert(currentCard.toString());
            }
        }
    } else {
        alert('Canvas is not supported');
    }
}

//$('#btnDrawCard').on('click', function () {
//    drawCard();
//});
// ---

function getRandomCard(stack) {
    var cardIndex = Math.floor(Math.random() * stack.length);
    var card = stack[cardIndex];
    stack.splice(cardIndex, 1);
    return card;
}

//---
function chooseTarget(player, players) {
    var indexOfAttacker = arrayObjectIndexOf(players, player);
    for (var i = 0; i < players.length; i++) {
        if (i == indexOfAttacker || !players[i].isAlive || players[i].isProtected) {
            continue;
        } else if (players[i].points >= player.points) {
            return players[i];
        }
    }

    // if no one has more points
    for (var i = 0; i < players.length; i++) {
        if (i == indexOfAttacker || !players[i].isAlive || players[i].isProtected) {
            continue;
        } else {
            return players[i];
        }
    }
}

function updateProtection(player) {
    if (player.isProtected) {
        player.isProtected = false;
    }
}

function chooseCardToPlay(player) {
    var card1 = player.hand[0];
    var card2 = player.hand[1];

    if (card1.value == 7 || card2.value == 7) { // Countess
        if (card1.value == 6 || card2.value == 6 ||
            card1.value == 5 || card2.value == 5 ||
            card1.value == 8 || card2.value == 8) {
            return card1.value == 7 ? card1 : card2;
        }
    }

    if (card1.value == 1 || card2.value == 1) { // Guard
        return card1.value == 1 ? card1 : card2;
    }

    if (card1.value == 2 || card2.value == 2) { // Priest
        return card1.value == 2 ? card1 : card2;
    }

    if (card1.value == 3 || card2.value == 3) { // Baron
        return card1.value == 3 ? card1 : card2;
    }

    if (card1.value == 4 || card2.value == 4) { // Handmaid
        return card1.value == 4 ? card1 : card2;
    }

    if (card1.value == 5 || card2.value == 5) { // Prince
        return card1.value == 5 ? card1 : card2;
    }

    if (card1.value == 6 || card2.value == 6) { // King
        return card1.value == 6 ? card1 : card2;
    }
}

function getDiscardPile(players) {
    var pile = [];
    players.forEach(function (p) {
        p.discardPile.forEach(function (c) {
            pile.push(c);
        });
    });

    return pile;
}

// Start of round
function processRound(players) {
    var deck = generateDeck();
    var backupCard = getRandomCard(deck); // Don't you forget about me

    // deal cards
    player1.hand.push(getRandomCard(deck));
    drawCard(player1.hand[0]);
    player2.hand.push(getRandomCard(deck));
    player3.hand.push(getRandomCard(deck));
    player4.hand.push(getRandomCard(deck));

    // choose first player
    var firstPlayer = Math.floor(Math.random() * 3.999);

    // turn cycle
    do {
        var somebodyWon = false;
        var currentPlayer = players[firstPlayer];
        updateProtection(currentPlayer);
        var newCard = getRandomCard(deck);
        currentPlayer.hand.push(newCard);
        // check for something wrong with hand count
        if (currentPlayer.hand.length > 2) {
            console.log('too many cards exception har-har');
        } else if (currentPlayer.hand.length <= 0) {
            console.log('not enough cards exception, wat?');
        }

        // choose target and card
        var targetPlayer = chooseTarget(currentPlayer, players);

        var cardToPlay = chooseCardToPlay(currentPlayer, targetPlayer);
        currentPlayer.hand.splice(currentPlayer.hand.indexOf(cardToPlay), 1);
        currentPlayer.discardPile.push(cardToPlay);
        updateEnemyHandInfo(cardToPlay, currentPlayer, players);

        // log user turn
        if (targetPlayer == undefined) {
            console.log(currentPlayer.name + ' has no effective action, discards ' + cardToPlay.name);
        } else {
            console.log('--> ' + currentPlayer.name + ' plays ' + cardToPlay.name + ' against ' + targetPlayer.name);

            // process card effect
            switch (cardToPlay.value) {
                case 1:
                    playGuard(currentPlayer, targetPlayer);
                    break;
                case 2:
                    playPriest(currentPlayer, targetPlayer);
                    break;
                case 3:
                    playBaron(currentPlayer, targetPlayer);
                    break;
                case 4:
                    playHandmaid(currentPlayer);
                    break;
                case 5:
                    playPrince(targetPlayer, deck);
                    break;
                case 6:
                    playKing(currentPlayer, targetPlayer);
                    break;
                case 7:
                    playCountess();
                    break;
                case 8:
                    playPrincess(currentPlayer);
                    break;
                default:
                    break;
            }
        }

        var alivePlayers = getAlivePlayers(players);
        // check for win condition
        var winner;
        if (alivePlayers.length === 0) {
            console.log('everybody dead');
            break;
        } else if (alivePlayers.length === 1) {
            winner = endRound(players);
            somebodyWon = true;
            break;
        } else if (deck.length == 0) {
            // add condition to use backup card
            winner = endRound(players); // TODO, backup card
            somebodyWon = true;
            break;
        }

        // set next player
        do {
            firstPlayer = (firstPlayer + 1) % players.length;
        } while (!players[firstPlayer].isAlive);
    } while (!somebodyWon);

    // reset for next round
    players.forEach(function (p) {
        p.isAlive = true;
        p.hand = [];
        p.discardPile = [];
    });
    deck = generateDeck();
}

function getAlivePlayers(players) {
    var alive = [];
    players.forEach(function (p) {
        if (p.isAlive) {
            alive.push(p);
        }
    });
    return alive;
}

function endRound(players) {
    var alive = getAlivePlayers(players);

    if (alive.length == 1) {
        alive[0].points++;
        console.log(alive[0].name + ' wins the round!\n');
        showScore(players);
        return alive[0];
    } else if (alive.length == 0) {
        console.log('wat? no alive players on game end exception');
        return false;
    } else {
        var potentialWinners = alive.sort(function (p1, p2) {
            if (p1.hand[0] != undefined && p2.hand[0] != undefined) {
                return p1.hand[0].value > p2.hand[0].value;
            }
        });
        var winner = potentialWinners[0];
        winner.points++;
        console.log(winner.name + ' wins the round!\n');
        showScore(players);
        return winner;
    }
}

function showScore(players) {
    var result = '';
    players.forEach(function (p) {
        if (p !== undefined) {
            result += '#' + p.name + ': ' + p.points + ' pts; ';
        }
    });
    console.log(result);
}

function arrayObjectIndexOf(myArray, searchTerm) {
    for (var i = 0, len = myArray.length; i < len; i++) {
        if (myArray[i] === searchTerm) return i;
    }
    return -1;
}

function getPossibleEnemyCards(discardPile, hand) {
    var deck = generateDeck();
    discardPile.forEach(function (c) {
        var indexOfCard = arrayObjectIndexOf(deck, c);
        deck.splice(indexOfCard, 1);
    });
    hand.forEach(function (c) {
        var indexOfCard = arrayObjectIndexOf(deck, c);
        deck.splice(indexOfCard, 1);
    });
    deck = deck.filter(function (e) {
        return e;
    });
    return deck;
}

// Cards effects

function playGuard(attacker, target) {
    var guess;
    var discardPile = getDiscardPile(players);
    var possibleCards = getPossibleEnemyCards(discardPile, attacker.hand);
    var somethingWrongCounter = 0;
    do {
        if (target === undefined) { // no useful action
            console.log('no target, Guard is drunk');
            return;
        }

        if (attacker.enemyHands[target.name] != undefined && attacker.enemyHands[target.name].length > 0) {
            guess = Math.floor(Math.random() * (attacker.enemyHands[target.name].length - 1)); // random so far
        } else {
            guess = Math.floor(Math.random() * (possibleCards.length - 1));
        }

        // check if only guards are left
        var totalGuards = 0;
        possibleCards.forEach(function (c) {
            if (c.value == 1) {
                totalGuards++;
            }
        });
        if (totalGuards == possibleCards.length) {
            return;
        }
        if (isNaN(guess) || possibleCards[guess] == undefined) {
            console.log('!-- guess = NaN || possibleCards[guess] == undefined');
            return;
        }

        somethingWrongCounter++;
        if (somethingWrongCounter > 15) {
            possibleCards.sort(function (a, b) {
                return a.value > b.value;
            });
            guess = possibleCards.length - 1;
        }
    } while (possibleCards[guess].value == 1);

    if (guess == target.hand[0].value) {
        target.isAlive = false;
        console.log(target.name + ' is out of the round');
    } else {
        console.log(attacker.name + ' guessed ' + possibleCards[guess].name + ', not correct');
    }

    return true;
}

function playPriest(attacker, target) {
    var enemyCard = target.hand[0];
    attacker.enemyHands[target.name] = enemyCard;
    console.log(attacker.name + ' now knows the card of ' + target.name);
    return true;
}

function playBaron(attacker, target) {
    var attackerCard = attacker.hand[0];
    var targetCard = target.hand[0];
    if (attackerCard.value > targetCard.value) {
        target.isAlive = false;
        console.log(target.name + ' is out of the round');
    } else if (attackerCard.value < targetCard.value) {
        attacker.isAlive = false;
        console.log(attacker.name + ' is out of the round');
    } else {     // if they are equal nothing happens
        console.log(attacker.name + ' has the same card as ' + target.name);
    }
    return true;
}

function playHandmaid(player) {
    player.isProtected = true;
    console.log(player.name + ' is now protected until his/her next turn');
    return true;
}

function playPrince(target, deck) {
    var discarded = target.hand.pop();
    target.discardPile.push(discarded);
    if (discarded.value === 8) {
        target.isAlive = false;
        console.log(target.name + ' dropped the princess, bye-bye');
    } else {
        target.hand.push(getRandomCard(deck));
        console.log(target.name + ' discards his/her card and draws another');
    }

    return true;
}

function playKing(attacker, target) {
    var cardToGive = attacker.hand.pop();
    attacker.hand.push(target.hand.pop());
    target.hand.push(cardToGive);
    console.log(attacker.name + ' swaps hand with ' + target.name);
    return true;
}

function playCountess() {
    console.log('guess why?');
    return true;
}

function playPrincess(player) {
    player.isAlive = false;
    console.log(player.name + ' dropped the princess, bye-bye');
    return true;
}

//------

function updateEnemyHandInfo(cardPlayed, player, players) {
    players.forEach(function (p) {
        if (p.enemyHands[player.name] !== undefined) {
            var cardIndex = arrayObjectIndexOf(p.enemyHands[player.name], cardPlayed);
            if (p !== player && cardIndex >= 0) {
                p.enemyHands[player.name].splice(cardIndex, 1);
            }
        }
    });
    return true;
}

function chooseEnemy(player) {

}

// >>> GAME START <<< //

var player1 = new Player('Player 1');
var player2 = new Player('Player 2');
var player3 = new Player('Player 3');
var player4 = new Player('Player 4');
var players = [player1, player2, player3, player4];

var roundWinner;
var roundCounter = 1;
do {
    console.log('<<< ROUND ' + roundCounter + ' >>>');
    roundWinner = processRound(players);
    roundCounter++;
    if (roundWinner) {
        break;
    }
} while (player1.points < 4 && player2.points < 4 &&
player3.points < 4 && player4.points < 4);

var winner = players.sort(function (a, b) {
    return a.points > b.points;
})[players.length - 1];
console.log(winner.name + ' wins the game!');


