// Constants
var WIDTH = 1220;
var HEIGHT = 800;
var OWNERS = {
    DECK: 'Deck',
    PLAYER_1: 'Player_1',
    PLAYER_2: 'Player_2',
    PLAYER_3: 'Player_3',
    PLAYER_4: 'Player_4',
    BACKUP_CARD: 'Backup_card',
    DISCARD: 'Discard'
};
var CARDS = {
    GUARD: {
        name: 'Guard',
        value: 1,
        quantity: 5,
        requiresEnemy: true,
        //picture: 'resources/cardFaces/guard.png',
        picture: 'resources/cardFaces/1.jpg',
        description: 'Name a non-Guard card and choose another player. If that player has that card, he or she is out of the round.'
    },
    PRIEST: {
        name: 'Priest',
        value: 2,
        quantity: 2,
        requiresEnemy: true,
        //picture: 'resources/cardFaces/priest.png',
        picture: 'resources/cardFaces/2.jpg',
        description: 'Look at another player\'s hand.'
    },
    BARON: {
        name: 'Baron',
        value: 3,
        quantity: 2,
        requiresEnemy: true,
        //picture: 'resources/cardFaces/baron.png',
        picture: 'resources/cardFaces/3.jpg',
        description: 'You and another player secretly compare hands. The player with the lower value is out of the round.'
    },
    HANDMAID: {
        name: 'Handmaid',
        value: 4,
        quantity: 2,
        requiresEnemy: false,
        //picture: 'resources/cardFaces/handmaid.png',
        picture: 'resources/cardFaces/4.jpg',
        description: 'Until your next turn, ignore all effects from other players\' actions.'
    },
    PRINCE: {
        name: 'Prince',
        value: 5,
        quantity: 2,
        requiresEnemy: true,
        //picture: 'resources/cardFaces/prince.png',
        picture: 'resources/cardFaces/5.jpg',
        description: 'Choose any player (including yourself) to discard his or her hand and draw a new card.'
    },
    KING: {
        name: 'King',
        value: 6,
        quantity: 1,
        requiresEnemy: true,
        //picture: 'resources/cardFaces/king.png',
        picture: 'resources/cardFaces/6.jpg',
        description: 'Trade hands with another player of your choice.'
    },
    COUNTESS: {
        name: 'Countess',
        value: 7,
        quantity: 1,
        requiresEnemy: false,
        //picture: 'resources/cardFaces/countess.png',
        picture: 'resources/cardFaces/7.jpg',
        description: 'If you have this card and the King or Prince in your hand, you must discard this card.'
    },
    PRINCESS: {
        name: 'Princess',
        value: 8,
        quantity: 1,
        requiresEnemy: false,
        //picture: 'resources/cardFaces/princess.png',
        picture: 'resources/cardFaces/8.jpg',
        description: 'If you discard this card, you are out of the round.'
    }
};

// Player stuff
function Player(name, isHuman) {
    this.name = name;
    this.hand = [];
    this.discardPile = [];
    this.points = 0;
    this.isAlive = true;
    this.isProtected = false;
    this.enemyHands = {};
    this.position = givePlayerPositionByID(name);
    this.isHuman = isHuman;
}

// Main elements

//Card stuff
function Card(value, name, description, owner, position, requiresEnemy) {
    this.value = value;
    this.name = name;
    this.description = description;
    this.owner = owner;
    this.position = position;
    this.image = null;
    this.isActive = false;
	this.isShown = false;
    this.requiresEnemy = requiresEnemy;
    //this.previousArrayIndex = null;
    //this.isHovered = false;
}

var canvas, stage;
var offset;
var update;
var deck, players;
var backupCard;
var container;
var myPlayer;

window.onload = function main() {
    init();
    tick();
    //processAllRounds();
};

function processAllRounds() {

    var roundWinner;
    var roundCounter = 1;
    do {
        console.log('<<< ROUND ' + roundCounter + ' >>>');
        roundWinner = processRound(players);
        roundCounter++;
        if (roundWinner) {
            break;
        }
    } while (players[0].points < 4 && players[1].points < 4 &&
    players[2].points < 4 && players[3].points < 4);

    var winner = players.sort(function (a, b) {
        return a.points > b.points;
    })[players.length - 1];
    console.log(winner.name + ' wins the game!');
}

// Initialization of game
function init() {
    // Create canvas on document
    canvas = document.createElement('canvas');
    canvas.setAttribute('id', 'myCanvas');
    canvas.width = WIDTH;
    canvas.height = HEIGHT;
    document.body.appendChild(canvas);
    container = new createjs.Container();

    //Create stage
    stage = new createjs.Stage(canvas);

    //Enable mouse over event
    stage.enableMouseOver(10);

    // Initialize deck - generate all 16 cards and shuffle them
    if (deck == null) {
        deck = generateDeck();
        addElementToBoard(deck);
    }

    // Initialize players
    if (players == null) {
        var count = 4; // to be edited
        players = initializePlayers(count);
    }

    // Draw backup card - random card from deck
    backupCard = (getRandomCard(deck.cards));
    backupCard.owner = OWNERS.BACKUP_CARD;

    renderPlayers();
}

function stop() {
    createjs.Ticker.removeEventListener('tick', tick);
}

function renderPlayers() {
	canvas.width+=0;
    players.forEach(function (player) {
        var i = 0;
		if(player.isAlive){
			player.hand.forEach(function (card) {
				card.position = new Position(player.position.x, player.position.y);
				card.position.x += i * 40;
				card.position.y += i * 40;
				addElementToBoard(card);
				i++;
			});
		}
    });
}

function tick(event) {
    if (update) {
        update = false; // only update once
        stage.update(event);
    }
}

Card.prototype.toString = function () {
    return '(' + this.value + ')' + this.name + ': ' + this.description;
};

function addElementToBoard(obj) {
    var picture = obj.image;
    var image = new Image();
    image.src = picture;
    image.onload = function (event) {
        var image = event.target;
        handleImageLoad(image, obj);
    };
}

function handleImageLoad(image, obj) { //card
    var bitmap;
    stage.addChild(container);
    if (obj instanceof Card && obj.owner !== myPlayer.name) {
		if(obj.isShown){
			image = 'resources/cardFaces/' + obj.value + '.jpg';	
		}
		else{
        	image = 'resources/back.png';
		}	
    }
    bitmap = new createjs.Bitmap(image);
    container.addChild(bitmap);
    bitmap.x = obj.position.x;
    bitmap.y = obj.position.y;
    bitmap.scale = 1;
    bitmap.name = 'object' + Math.floor(Math.random() * 100);
    bitmap.cursor = 'pointer';
    stage.update();
    var player = null;

    if (obj instanceof Card) {
        player = getCardOwner(obj);
    }

    bitmap.object = obj;
    obj.bitmap = bitmap;
	
    bitmap.on('mousedown', function (evt) {
        this.parent.addChild(this);
        this.offset = {x: this.x - evt.stageX, y: this.y - evt.stageY};
        if (obj instanceof Deck) {
            if (obj.cards.length === 0) {
                container.removeChild(this);
            }
            playerDrawCard(myPlayer);
        } else {
            if (obj instanceof Card && obj.owner === myPlayer.name) {
                var activeCards = myPlayer.hand.filter(function (c) {
                    return c.isActive;
                });
                this.object.isActive = true;
                if (activeCards.length > 0) {
                    if (activeCards[0].name !== 'Prince') {
                        activeCards[0].isActive = false;
                        this.object.isActive = true;
                    } else {
                        handlePlayerPlayCard(activeCards[0], myPlayer);
                        container.removeChild(activeCards[0].bitmap);
                    }
                }
                if (!this.object.requiresEnemy) {
                    container.removeChild(activeCards[0].bitmap);
                    handlePlayerPlayCard(this.object);
                }
            } else if (obj instanceof Card) {
                // TODO check if another card in hand is active
                var activeCards = myPlayer.hand.filter(function (c) {
                    return c.isActive;
                });
                if (activeCards.length > 0) {
                    if (activeCards[0].requiresEnemy && player !== myPlayer) {
                        container.removeChild(activeCards[0].bitmap);
                        handlePlayerPlayCard(activeCards[0], player);
                    }
                }
            }
        }
        stage.update();
    });
	
    bitmap.on('rollover', function (evt) {
        this.parent.addChild(this);
        this.scaleX = this.scaleY = this.scale * 1.1;
        update = true;
    });


    //card.image = bitmap;

    bitmap.on('rollout', function (evt) {
        this.scaleX = this.scaleY = this.scale;
        //this.card.isHovered = false;
        update = true;
    });

    createjs.Ticker.addEventListener('tick', tick);
}

function getCardOwner(obj) {
    var playerIndex = obj.owner.slice(-1) - 1;
    return players[playerIndex];
}

function handlePlayerPlayCard(card, target) {
    // Remove card from player hand and add it to discard pile
    var playerIndex = card.owner.slice(-1) - 1;
    var player = players[playerIndex];
    if (!player.isHuman) {
        target = chooseTarget(player, players);
    }
    var indexOfCard = 0;
    while (true) {
        if (players[playerIndex].hand[indexOfCard] === card) {
            players[playerIndex].hand.splice(indexOfCard, 1);
            card.owner = OWNERS.DISCARD;
            players[playerIndex].discardPile.push(card);
            break;
        }
        indexOfCard++;
    }

    // Activate card effect
    if (card.name === 'Guard') {
        playGuard(player, target);
    } else if (card.name === 'Priest') {
        playPriest(player, target);
    } else if (card.name === 'Baron') {
        playBaron(player, target);
    } else if (card.name === 'Handmaid') {
        playHandmaid(player);
    } else if (card.name === 'Prince') {
        playPrince(target);
    } else if (card.name === 'King') {
        playKing(player, target);
    } else if (card.name === 'Countess') {
        playCountess();
    } else if (card.name === 'Princess') {
        playPrincess(player);
    }
	
	container.removeAllChildren();
	renderPlayers();
	generateDeck();
	addElementToBoard(deck);
	
    stage.update();
}

// Deck stuff
function Deck(cards, position) {
    this.cards = cards;
    this.position = position;
    this.image = null;
}

function generateDeck() {
    var deckCards = [];
    for (var cardType in CARDS) {
        var type = CARDS[cardType];
        for (var i = 0; i < type.quantity; i++) {
            var card = new Card(type.value, type.name, type.description, OWNERS.DECK, new Position(0, 0), type.requiresEnemy);
            card.image = type.picture;
            deckCards.push(card);
        }
    }
    var deckPosition = new Position(WIDTH - 230, HEIGHT - 325);
    deckCards = shuffleDeck(deckCards);
    var deck = new Deck(deckCards, deckPosition);
    deck.image = 'resources/back.png';

    return deck;
}

function getRandomCard() {
    var cardIndex = Math.floor(Math.random() * deck.cards.length);
    var card = deck.cards[cardIndex];
    deck.cards.splice(cardIndex, 1);
    return card;
}

function shuffleDeck(cards) {
    var i = cards.length, j, tempi, tempj;
    if (i === 0) return false;
    while (--i) {
        j = Math.floor(Math.random() * (i + 1));
        tempi = cards[i];
        tempj = cards[j];
        cards[i] = tempj;
        cards[j] = tempi;
    }

    return cards;
}

function getCardFromDeck() {
    if (deck.cards.length >= 0) {
        return deck.cards.pop();
    }
}

function initializePlayers(count) {
    players = [];
    var isHuman = false;
    for (var i = 1; i <= count; i++) {
        isHuman = i === 1 ? true : false;
        var player = new Player('Player' + i, isHuman);
        var card = getCardFromDeck();
        card.owner = player.name;
        card.position = new Position(player.position.x, player.position.y);
        player.hand.push(card);

        for (var j = 0; j < player.hand.length; j++) {
            var currCard = player.hand[i];
            if (currCard === card) {
                card.previousArrayIndex = !!j;
                break;
            }
        }

        players.push(player);
    }

    myPlayer = players[0]; // should be edited for multi
    return players;
}

function playerDrawCard(player) {
    if (player.hand.length < 2) {
        var newCard = getCardFromDeck();
        newCard.owner = player.name;
        player.hand.push(newCard);
        renderPlayers();
    }
}

function givePlayerPositionByID(playerID) {
    if (playerID === 'Player1') {
        return new Position(50, 20);
    } else if (playerID === 'Player2') {
        return new Position(330, 20);
    } else if (playerID === 'Player3') {
        return new Position(610, 20);
    } else {
        return new Position(890, 20);
    }
}

// GUI stuff
function drawText() {
}

//function playSound(soundResource) {
//    if (soundResource) {
//        var currentAudio = new Audio();
//        currentAudio.src = soundResource;
//        currentAudio.play();
//    }
//}

//---- main game logic ----//

function getDiscardPile(players) {
    var pile = [];
    players.forEach(function (p) {
        p.discardPile.forEach(function (c) {
            pile.push(c);
        });
    });

    return pile;
}

function playGuard(attacker, target) {
    var guess;
    var discardPile = getDiscardPile(players);
    var possibleCards = getPossibleEnemyCards(discardPile, attacker.hand);
    var somethingWrongCounter = 0;
	if(attacker.isHuman){
		guess = prompt("Your choice: ");
		var validValue = false;
		
		for(var property in CARDS) {
		   	if(CARDS[property].name == guess){
				validValue = true;
			}
		}
		
		if(!validValue){
			return false;
		}
		
		if (guess == target.hand[0].name) {
			target.isAlive = false;
			console.log(target.name + ' is out of the round');
		} else {
			console.log(attacker.name + ' guessed ' + guess + ', not correct');
		}
	
		
	}
	else
	{
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
		} else 
		{
			console.log(attacker.name + ' guessed ' + possibleCards[guess].name + ', not correct');
		}
	}	
    return true;
}

function playPriest(attacker, target) {
    var enemyCard = target.hand[0];
    attacker.enemyHands[target.name] = enemyCard;
    enemyCard.image = 'resources/cardFaces/' + enemyCard.value + '.jpg';
    console.log(attacker.name + ' now knows the card of ' + target.name);
	target.hand[0].isShown = true;
	
	console.log('The enemy card is ' + enemyCard.name + '.');
	
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

function playPrince(target) {
    var discarded = target.hand.pop();
    target.discardPile.push(discarded);
    if (discarded.value === 8) {
        target.isAlive = false;
        console.log(target.name + ' dropped the princess, bye-bye');
    } else {
        target.hand.push(getCardFromDeck());
        console.log(target.name + ' discards his/her card and draws another');
    }

    return true;
}

function playKing(attacker, target) {
    var cardToGive = attacker.hand.pop();
    cardToGive.owner = target.name;
    var cardToTake = target.hand.pop();
    cardToTake.owner = attacker.name;
    attacker.hand.push(cardToTake);
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

function arrayObjectIndexOf(myArray, searchTerm) {
    for (var i = 0, len = myArray.length; i < len; i++) {
        if (myArray[i] === searchTerm) return i;
    }
    return -1;
}

function getPossibleEnemyCards(discardPile, hand) {
    var possibleCards = generateDeck().cards;
    discardPile.forEach(function (c) {
        var indexOfCard = arrayObjectIndexOf(possibleCards, c);
        possibleCards.splice(indexOfCard, 1);
    });
    hand.forEach(function (c) {
        var indexOfCard = arrayObjectIndexOf(possibleCards, c);
        possibleCards.splice(indexOfCard, 1);
    });
    possibleCards = possibleCards.filter(function (e) {
        return e;
    });
    return possibleCards;
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

function processRound(players) {
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
        var targetPlayer = null;
        if (!currentPlayer.isHuman) {
            targetPlayer = chooseTarget(currentPlayer, players)
        } else {
            targetPlayer = selectEnemy();
        }
        var cardToPlay = null;
        if (!currentPlayer.isHuman) {
            cardToPlay = chooseCardToPlay(currentPlayer, targetPlayer);
        }

        if (currentPlayer.isHuman) {
            // TODO fix wait time

            waitUserToResponse();
            //
            cardToPlay = chosenCard;
            targetPlayer = chosenTarget;
        }

        currentPlayer.hand.splice(currentPlayer.hand.indexOf(cardToPlay), 1);
        currentPlayer.discardPile.push(cardToPlay);

        // if first card is played set the second to first ([0])
        var cardLeft = currentPlayer.hand.pop();
        currentPlayer.hand[0] = cardLeft;

        updateEnemyHandInfo(cardToPlay, currentPlayer, players);

        // log user turn
        if (targetPlayer == undefined) {
            //console.log(currentPlayer.name + ' has no effective action, discards ' + cardToPlay.name);
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

function updateProtection(player) {
    if (player.isProtected) {
        player.isProtected = false;
    }
}

function selectEnemy() {
    return players[1];
}

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

function waitUserToResponse() {
    if (playerIsReady) {
        playerIsReady = false;
    } else {
        console.log('shit');
        setTimeout(waitUserToResponse, 1000);
    }
}