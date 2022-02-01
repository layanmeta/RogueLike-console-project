using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RogueLike
{
    public class UIHandler
    {
        public Queue<string> messageQueue = new Queue<string>();
        private int oldQueueCount;

        public UIHandler()
        {
    
        }

        public void DrawUI(Map map, Player player, List<Enemy> enemies)
        {
            if (oldQueueCount != messageQueue.Count)
            {
                Console.Clear();
                oldQueueCount = messageQueue.Count;
            }

            //calling everything from all class to print easily inside GameLoop()             
            map.printMap();

            //Draw all stats from the player
            Console.WriteLine($"\nHP: {player.Stats.Hp} Dmg: {player.Stats.totalDamage} Speed: {player.Stats.totalSpeed}");
            //Draw Equiped Weapon
            Console.WriteLine($"Equipped: {player.Equipped.Name} | +{player.Equipped.Damage} Dmg\n\n");


            //Dequeue if the queue reaches 5+
            for (var x = 0; x <= (messageQueue.Count - 5); x++) messageQueue.Dequeue();

            //Draw All messages in the queue 
            foreach (var eventMessage in messageQueue)
            {
                Console.WriteLine(eventMessage);
            }

            //Draw inventory 
            Console.WriteLine("\n\n-Inventory-");

            int count = 0;
            foreach(var item in player.Inventory)
            {
                count++;
                Console.WriteLine($"{count} - {item}");
            }

            //Draw the players location
            player.SetPosition();

            //Draw the location of all enemies
            foreach (var enemy in enemies)
            {
                enemy.SetPosition();
            }
        }
        
        // Adds a Event Message into the queue  
        public void AddEventMessage(string eventMessage)
        {
            messageQueue.Enqueue(eventMessage);            
        }
    }
}
