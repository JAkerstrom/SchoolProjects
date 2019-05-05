using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace johanna_battleship.BattleShip
{
    public class Game
    {
        List<Boat> Boats { get; set; }
        List<string> UsedCoordinates { get; set; }
        List<string> sunkBoats;
        public int beginner;

        public Game()
        {
            GenerateBoats();
            UsedCoordinates = new List<string>();
            sunkBoats = new List<string>();
            beginner = new Random().Next(1, 3);
        }

        public string Shoot(string coordinate)
        {
            if(UsedCoordinates.Any(c => c == coordinate))
            {
                return "Du har redan skjutit den koordinaten";
            }

            foreach(var boat in Boats)
            {
                if(boat.Hit(coordinate) == true)
                {
                    UsedCoordinates.Add(coordinate);
                    boat.RemoveCoordinate(coordinate);
                    if (boat.Sunk())
                    {
                        sunkBoats.Add(boat.Name);
                        if (sunkBoats.Count() == 2)
                            return "260 You sunk all my boats, you win!";
                        return $"25{boat.Number} You sunk my {boat.Name}";
                    }

                    return $"24{boat.Number} You hit my {boat.Name}";
                }
            }

            return "230 Miss";
        }

        public void GenerateBoats()
        {
            Boats = new List<Boat>()
            {
                new Boat("Carrier", 1),
                new Boat("Battleship", 2),
                new Boat("Destroyer", 3),
                new Boat("Submarine", 4),
                new Boat("Patrol boat", 5)
            };
        }
    }
}
