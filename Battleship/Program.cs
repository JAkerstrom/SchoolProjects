using System;
using System.IO;
using System.Net.Sockets;
using System.Net;
using johanna_battleship.TCP;
using johanna_battleship.Protocol;
using johanna_battleship.BattleShip;

namespace johanna_battleship
{
    class Program
    {
        private static Connection connection;
        private static Translator translator;
        private static Game game;
        private static int errors = 0;

        static void Main(string[] args)
        {            
            translator = new Translator();
            do
            {
                bool started = false;
                do
                {
                    started = Start();
                } while (!started);

                game = new Game();

                if (game.beginner == 1)
                {
                    connection.WriteLine("222 Server starts");
                    RunGameServerStarts();
                }
                else
                {
                    connection.WriteLine("221 You start");
                    RunGameClientStarts();
                }
            } while (true);

        }

        private static void CreateConnection()
        {
            if (connection != null)
            {
                connection.CloseAll();
                errors = 0;
            }

            connection = new Connection(5000);
            connection.startListening();
        }
        
        private static bool Start()
        {
            CreateConnection();

            if (connection.Connected)
            {
                if (HandShake())
                {
                    if (AwaitStart())
                    {
                        return true;
                    }
                    else                   
                        return false;                   
                }
                else
                    return false;
            }
            else
                return false;                
        }

        private static bool AwaitStart()
        {

            string response = " ";
            bool procceed = false;

            do
            {
                if (errors > 2)
                {
                    connection.WriteLine("270 server is disconnecting, bye..");
                    return false;
                }

                response = connection.ReadLine();
                var result = translator.Translate(response, "start", 2);

                if (result.StartsWith("2"))
                {
                    errors = 0;
                    procceed = true;
                }
                else if (CheckResult(result) == false) return false;

            } while (procceed == false);

            return true;
        }

        private static bool HandShake()
        {
            string response = " ";
            bool procceed = false;
            
            do
            {
                if (errors > 2)
                {
                    connection.WriteLine("270 server is disconnecting, bye..");
                    return false;
                }

                connection.WriteLine("210 BATTLESHIP/1.0");
                response = connection.ReadLine();
                var result = translator.Translate(response, "hello ", 1);

                if (result.StartsWith("2")) {
                    errors = 0;
                    procceed = true;
                } 
                else if (CheckResult(result) == false) return false;

            } while (procceed == false);

            connection.WriteLine($"220 Server");
            return true;

        }

        private static bool CheckResult(string result, int hitOrMiss = 0)
        {
            if (result == "null" || result == " ")
            {
                connection.WriteLine("500 client returned null");
                return false;
            }
            else if (result.StartsWith("5"))
            {
                errors += 1;
                if(result == "500") connection.WriteLine("500 Unknown command");
                if (result == "501") connection.WriteLine("501 Sequence error");
                return true;
            }
            else if (result == "quit")
            {
                connection.WriteLine("270 bye..");
                return false;
            }
            else if(hitOrMiss == 1)
            {
                if(result.ToLower().StartsWith("2") && result.ToLower() != "miss")
                {
                    connection.WriteLine("500 Unknown command, your turn");
                    return true;
                }
            }
            return true;
        }

        private static void RunGameClientStarts()
        {
            bool proceed = true;
            do
            {
                var command = connection.ReadLine();
                var translation = translator.Translate(command, "230 fire", 3);

                if (CheckResult(translation) == false)
                {
                    proceed = false;
                    break;
                }
                if (!translation.StartsWith("5"))
                {
                    errors += 1;

                    var hitOrMiss = game.Shoot(translation);
                    if (!connection.WriteLine(hitOrMiss))
                    {
                        proceed = false;
                        break;
                    }

                    var serverCommand = Console.ReadLine();
                    if (!connection.WriteLine(serverCommand))
                    {
                        proceed = false;
                        break;
                    }
                    
                    var result = connection.ReadLine();
                    if (CheckResult(result) == false)
                        proceed = false;
                }
                else {

                    errors = 0;
                    
                    if (!connection.WriteLine("Serverns tur"))
                    {
                        proceed = false;
                        break;
                    }

                    var serverCommand = Console.ReadLine();
                    if (!connection.WriteLine(serverCommand))
                    {
                        proceed = false;
                        break;
                    }
                    
                    var result = connection.ReadLine();
                    if (CheckResult(result) == false)
                        proceed = false;
                }
              


            } while (proceed);
        }

        private static void RunGameServerStarts()
        {
            bool proceed = true;
            do
            {
                var serverCommand = Console.ReadLine();

                if (!connection.WriteLine(serverCommand))
                {
                    proceed = false;
                    break;
                }
                    
                var result = connection.ReadLine();
                if (CheckResult(result, 1) == false)
                {
                    proceed = false;
                    break;
                }
                    

                var command = connection.ReadLine();
                var translation = translator.Translate(command, "230 fire", 3);

                if (CheckResult(translation) == false)
                {
                    proceed = false;
                    break;
                }
                    
                if (!translation.StartsWith("5"))
                {
                    errors += 1;

                    var hitOrMiss = game.Shoot(translation);
                    
                    if (!connection.WriteLine(hitOrMiss))                   
                        proceed = false;
                }
                else {

                    errors = 0;

                    if (!connection.WriteLine("Serverns tur"))
                        proceed = false;                    
                }

            } while (proceed);
        }
    }
}
