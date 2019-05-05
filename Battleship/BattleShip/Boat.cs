using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace johanna_battleship.BattleShip
{
    public class Boat
    {
        private List<string> _coordinates;
        private string _name { get; set; }
        public int Number { get; set; }

        //för att autogenerera koordinater
        public Boat(string name, int number)
        {
            _name = name;
            Number = number;
            GenerateCoordinates();
        }

        //för att skriva in egna koordinater //ej implementerat
        public Boat(string name, List<string> cords)
        {
            _name = name;
            _coordinates = cords;
        }

        public bool Hit(string coordinate)
        {
            if (_coordinates.Any(c => c == coordinate))
                return true;
            else
                return false;
        }

        public void RemoveCoordinate(string coordinate)
        {
            _coordinates.Remove(coordinate);
        }

        public bool Sunk()
        {
            if (_coordinates.Count == 0)
                return true;
            else
                return false;
        }

        public List<string> Coordinates {
            get
            {
                return _coordinates;
            }
        }

        public string Name {
            get
            {
                return _name;
            }
        }

        private void GenerateCoordinates()
        {
            switch (_name.ToLower())
            {
                case "carrier":
                    _coordinates = new List<string>()
                    {
                        "a1",
                        "a2",
                        "a3",
                        "a4",
                        "a5"
                    };
                    break;
                case "battleship":
                    _coordinates = new List<string>()
                    {
                        "b1",
                        "b2",
                        "b3",
                        "b4"
                    };
                    break;
                case "destroyer":
                    _coordinates = new List<string>()
                    {
                        "c1",
                        "c2",
                        "c3"
                    };
                    break;
                case "submarine":
                    _coordinates = new List<string>()
                    {
                        "d1",
                        "d2",
                        "d3"
                    };
                    break;
                case "patrol boat":
                    _coordinates = new List<string>()
                    {
                        "e1",
                        "e2"
                    };
                    break;

            }

        }
    }
}
