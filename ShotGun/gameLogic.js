var gameRound;

$("#startbutton").click(function(){
    var player = new Player(0);
    var computer = new Computer(0);     
    computer.skott = 0;
    gameRound = new Game();
    gameRound.Player = player;
    gameRound.Computer = computer;
    gameRound.StartGame();
});
$("#laddaBtn").click(function(){
    gameRound.Player.Ladda();

    if(gameRound.Player.skott >= 1){
        $("#skjutBtn").show();

        if(gameRound.Player.skott == 3){
            $("#shotgunBtn").show();
        }
    }
    var compChoice = gameRound.Computer.nextMove();
    gameRound.ShowPlayerShots();
    gameRound.ShowComputerShots();
    gameRound.GameHandler(1, compChoice);
});
$("#blockBtn").click(function(){
    var compChoice = gameRound.Computer.nextMove();
    gameRound.ShowPlayerShots();
    gameRound.ShowComputerShots();
    gameRound.GameHandler(2, compChoice);
});
$("#skjutBtn").click(function(){
    gameRound.Player.Skjut();
    var compChoice = gameRound.Computer.nextMove();
    gameRound.ShowPlayerShots();
    gameRound.ShowComputerShots();
    gameRound.GameHandler(3, compChoice);
});
$("#shotgunBtn").click(function(){
    var compChoice = gameRound.Computer.nextMove();
    gameRound.ShowPlayerShots();
    gameRound.ShowComputerShots();
    gameRound.GameHandler(4, compChoice);
});

var showResult = function(message, fontcolor){
    $("#datorMove")
    .css("color", fontcolor)
    .text(message)
    .addClass("tada")
    .show();
}

function Game(player, computer){
    this.Player = player;
    this.Computer = computer;

    this.GameHandler = function (playerChoice, computerChoice){
        var pChoice = playerChoice;
        var cChoice = computerChoice; 
    
        switch (cChoice){
            case 1:
                if(pChoice == 1){    
                    showResult("Dator: Ladda,  Player: Ladda", "blue");            
                }
                else if(pChoice == 2){
                    showResult("Dator: Ladda,  Player: Blocka", "blue");                    
                }
                else if(pChoice == 3){
                    showResult("Datorn laddade, Player sköt, Player vann!", "lightgreen");
                    $("#datorMove").fadeOut(3000);                          
                    gameRound.EndGame();
                }
                else if(pChoice == 4){
                    showResult("Player valde Shotgun! Player vann!", "lightgreen");
                    $("#datorMove").fadeOut(3000);
                    gameRound.EndGame();
                }
                break;
            case 2:
                if(pChoice == 1){
                    showResult("Dator: Blocka,  Player: Ladda", "blue");
                }
                else if(pChoice == 2){
                    showResult("Dator: Blocka,  Player: Blocka", "blue");
                }
                else if(pChoice == 3){
                    showResult("Dator: Blocka,  Player: Skjuta", "blue");
                    
                    if(gameRound.Player.skott < 3){
                        $("#shotgunBtn").hide();
                    }
                    if(gameRound.Player.skott == 0){
                        $("#skjutBtn").hide();
                    }
                }
                else if(pChoice == 4){
                    showResult("Player valde Shotgun! Player vann!", "lightgreen");
                    $("#datorMove").fadeOut(3000);
                    gameRound.EndGame();
                }
                break;
            case 3:
                if(pChoice == 1){
                    showResult("Datorn sköt, Player laddade, Datorn vann!", "red");
                    $("#datorMove").fadeOut(3000);
                    gameRound.EndGame();  
                    /*skjuta mot ladda*/
                }
                else if(pChoice == 2){
                    showResult("Dator: Skjuta,  Player: Blocka", "blue");
                    /*skjuta mot blocka*/
                }
                else if(pChoice == 3){
                    showResult("Dator: Skjuta,  Player: Skjuta", "blue");
                    /*skjuta mot skjuta*/
                }
                break;
            case 4:
                showResult("Datorn valde Shotgun! Datorn vann!", "red");
                $("#datorMove").fadeOut(3000);
                gameRound.EndGame();
                break;
        }  
    }
    this.ShowPlayerShots = function(){
        $("#playerAntalSkott").text(gameRound.Player.skott);
    }
    this.ShowComputerShots = function(){
        $("#datorAntalSkott").text(gameRound.Computer.skott);
    }
    this.StartGame = function(){
        $("#startbutton").hide();
        $("#computerContainer").show();
        $("#playerContainer").show();
        $("#computerStatusBar").show();
        $("#bottomContainer").show();
   
        $("#playerAntalSkott").text(gameRound.Player.skott);
        $("#datorAntalSkott").text(gameRound.Computer.skott);
    }
    this.EndGame = function(){
        $("#startbutton").show();
        $("#computerContainer").hide();
        $("#playerContainer").hide();
        $("#bottomContainer").hide();
        $("#skjutBtn").hide();
        $("#shotgunBtn").hide();
    }
}

function Player(antalSkott){
    this.skott = antalSkott;

    this.Ladda = function(){
        this.skott = this.skott + 1;        
    }
    this.Skjut = function(){
        this.skott = this.skott - 1;
    }
}

function Computer(){
    this.prototype = new Player();

    this.nextMove = function(){

        if(this.skott == 3){
            return 4; //shotgun
        }
        if(this.skott == 0){
            var randNumber = Math.floor(Math.random() * 2) + 1;
            if(randNumber == 1){
                this.skott = this.skott + 1;
                return 1; //ladda
            }
            else if(randNumber == 2){
                return 2; //blocka
            }
        }
        else if(this.skott > 0 && this.skott < 3){
            var randNumber = Math.floor(Math.random() * 3) + 1;
            if(randNumber == 1){
                this.skott = this.skott + 1;
                return 1; //ladda
            }
            else if(randNumber == 2){
                return 2;//blocka
            }
            else if(randNumber == 3){
                this.skott = this.skott - 1;
                return 3;//skjut
            }
        }
    }
}

























