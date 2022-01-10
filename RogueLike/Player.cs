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
        public Attributes PlayerAttributes = new Attributes();
        private ConsoleColor playerColor;

        public Player(int Xpath, int Ypath)
        {
            PlayerAttributes.Hp = 50;
            PlayerAttributes.Damage = 20;

            x = Xpath;
            y = Ypath;
            Inventory = new List<string>();
            playerMark = "@";
            playerColor = ConsoleColor.Blue;    
        }

        public void SetPlayer()
        {
            //sets the player's position
            Console.ForegroundColor = playerColor;
            Console.SetCursorPosition(x, y);
            Console.Write(playerMark);
            Console.ResetColor();
        }

        public void GiveItem(string item)
        {
            Inventory.Add(item);
        }

    }
}
