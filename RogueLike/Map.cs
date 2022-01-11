using System;
using System.Collections.Generic;
using System.Text;

namespace RogueLike
{
    public class Map
    {        
        private string[,] Grid;
        private int rows;
        private int cols;
        //private int parameters;
        //make bool for T, if true I change color, and call the bool in game
        public bool steppedOnTrap = false;
        public bool hasKey = false;

        public Map(string[,] grid)
        {
            //grid of all maps
            Grid = grid;
            rows = grid.GetLength(0);
            cols = grid.GetLength(1);
        }

        public void printMap()
        {
            //function to print all of the maps and map's elements
            for (int y = 0; y < rows; y++)
            {
                for (int x = 0; x < cols; x++)
                {
                    string element = Grid[y, x];
                    Console.SetCursorPosition(x, y);
                    if (element == "X")
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                    }
                    else if (element == "T")
                    {                      
                        Console.ForegroundColor = ConsoleColor.Gray;
                        if (steppedOnTrap == true)
                        {
                            Console.ForegroundColor = ConsoleColor.Blue;
                        }
                    }
                    else if (element == "M")
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                    }                    
                    else if (element == "C")
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        if (hasKey == true)
                        {
                            Console.ForegroundColor = ConsoleColor.Gray;
                        }
                    }
                    else if (element == "H")
                    {
                        Console.ForegroundColor = ConsoleColor.Magenta;
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                    Console.Write(element);
                }
            }
        }

        public bool IsItWalkable(int x, int y)
        {           
            //blocks the player from walking into walls
            if (x < 0 || y < 0 || x >= cols || y >= rows)
            {
                return false;
            }           
            return Grid[y, x] == " " || Grid[y, x] == "X" || Grid[y, x] == "T" || Grid[y, x] == "C" || Grid[y, x] == "H";
        }

        public void FoundChest(Player player)
        {
            player.GiveItem("Key");
        }

        public bool HasCompletedRequirements(Player player)
        {
            //requierments you need in order to leave the level
            if (player.Inventory.Contains("Key")) return true;
            return false;
        }

        public string GetElement(int x, int y)
        {
            //can be used for finish line, traps, treasures etc...
            return Grid[y,x];
        }
    }
}
