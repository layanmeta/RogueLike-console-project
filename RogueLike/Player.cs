using System;
using System.Collections.Generic;
using System.Text;

namespace RogueLike
{
    public class Player
    {
        public int x { get; set; }
        public int y { get; set; }
        private string playerMark;
        public List<string> Inventory;
        public Weapon Equipped;
        public Stats Stats = new Stats();
        private ConsoleColor playerColor;

        public Player(int Xpath, int Ypath)
        {
            //Give the player stats
            Stats.Hp = 20;
            Stats.damage = 3;
            Stats.Speed = 3;

            //Give the player basic items
            Inventory = new List<string>();
            EquipWeapon(new Sword("Basic Dagger", 1));

            //Set the player on the spawn location
            playerMark = "@";
            playerColor = ConsoleColor.Blue;
            x = Xpath;
            y = Ypath;                      
        }

        public void EquipWeapon(Weapon weapon)
        {
            Equipped = weapon;
            Stats.totalDamage = Stats.damage + Equipped.Damage;
            Stats.totalSpeed = Stats.Speed + Equipped.Speed;
        }

        public bool IsAlive()
        {
            //checks if player is dead or not
            return Stats.Hp > 0;
        }

        public void Kill()
        {
            Stats.Hp = 0;
        }

        public void Respawn()
        {
            //respawn the player when dies or steps on traps
            x = 1;
            y = 1;
            SetPosition();
        }

        public void GetHit(int dmg)
        {
            Stats.Hp -= dmg;             
            
            if (Stats.Hp <= 0) Kill();
        }

        public void SetPosition()
        {
            //sets the player's position
            Console.ForegroundColor = playerColor;
            Console.SetCursorPosition(x, y);
            Console.Write(playerMark);
            Console.ResetColor();
        }

        public void GiveItem(string item)
        {
            //adds item in inventory 
            Inventory.Add(item);
        }

        public void HandlePlayerInput(Map map)
        {
            //player's movement
            ConsoleKey key;
            do
            {
                ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                key = keyInfo.Key;

            } while (Console.KeyAvailable);

            switch (key)
            {
                case ConsoleKey.W:
                    if (map.IsItWalkable(x, y - 1))
                    {
                        y -= 1;
                    }
                    break;
                case ConsoleKey.S:
                    if (map.IsItWalkable(x, y + 1))
                    {
                        y += 1;
                    }
                    break;
                case ConsoleKey.A:
                    if (map.IsItWalkable(x - 1, y))
                    {
                        x -= 1;

                    }
                    break;
                case ConsoleKey.D:
                    if (map.IsItWalkable(x + 1, y))
                    {
                        x += 1;

                    }
                    break;
                default:
                    break;
            }

        }

    }
}
