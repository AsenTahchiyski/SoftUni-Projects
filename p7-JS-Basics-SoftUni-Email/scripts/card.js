var OWNERS = {
    DECK: 'Deck',
    PLAYER_1: 'Player_1',
    PLAYER_2: 'Player_2',
    PLAYER_3: 'Player_3',
    PLAYER_4: 'Player_4',
    BACKUP_CARD: 'Backup_card'
};

var enumeration = string_of_enum(OWNERS,'Player_1');
console.log(Math.floor(Math.random()*100));

function string_of_enum(enu, value) {
    for (var k in enu) {
        if (enu[k] === value
        )
            return k;
    }
    return null;
}