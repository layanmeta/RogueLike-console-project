using System;
using System.Collections.Generic;
using System.Text;

namespace RogueLike
{
    public class Map
    {        
        public string[,] Grid;
        public int rows;
        public int cols;
        public bool steppedOnTrap = false;
        public bool hasKey = false;
        public bool gainedHealth = false;
        public bool weaponQuipped = false;
        public List<Map> _allMaps;
        
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
                        Console.ForegroundColor = ConsoleColor.Black;
                        if (steppedOnTrap == true)
                        {
                            Console.ForegroundColor = ConsoleColor.Cyan;
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
                        if (gainedHealth == true)
                        {
                            Console.ForegroundColor = ConsoleColor.Black;
                        }
                    }
                    else if (element == "!")
                    {
                        Console.ForegroundColor = ConsoleColor.DarkCyan;
                        if (weaponQuipped == true)
                        {
                            Console.ForegroundColor = ConsoleColor.Black;
                        }
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
            return Grid[y, x] == " " || Grid[y, x] == "X" || Grid[y, x] == "T" || Grid[y, x] == "C" || Grid[y, x] == "H"|| Grid[y, x] == "M" || Grid[y, x] == "!";
        }

        public void FoundChest(Player player)
        {
            //checks if C was collected
            player.GiveItem("Key");
            this.hasKey = true;
        }

        public bool FoundWeapon(Player player)
        {
            //checks if you gained a new weapon
            if (this.weaponQuipped !=true)
            {
                player.EquipWeapon(new Sword("Sword", 4, 2));
                return this.weaponQuipped = true;
            }
            return false;
        }

        public bool FoundHeart(Player player)
        {
            //checks if you already got heal from 1 map
            if (this.gainedHealth != true)
            {
                player.Stats.Hp += 3;
                this.gainedHealth = true;
                return true;
            }
            return false;
        }

        public void ActivatedTrap(Player player)
        {
            this.steppedOnTrap = true;
            player.Stats.Hp -= 1;
            player.Respawn();
        }

        public bool Exit(Player player)
        {
            if (HasCompletedRequirements(player))
            {
                player.Inventory.Remove("Key");                
                return true;
            }
            else
            {
                return false;
            }
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
