using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RogueLike
{
    public class UIHandler
    {
        public Queue<string> messageQueue = new Queue<string>();

        public UIHandler()
        {
    
        }

        public void DrawUI(Map map, Player player)
        {

            //calling everything from all class to print easily inside GameLoop() 
            map.printMap();
            Console.WriteLine($"\nHP: {player.PlayerAttributes.Hp}\n\n");

            //Draw Events from the event queue 
            if (messageQueue.Count > 5) messageQueue.Dequeue();
            foreach (var eventMessage in messageQueue)
            {
                Console.WriteLine(eventMessage);
            }

            player.SetPlayer();
        }

        public void AddEventMessage(string eventMessage)
        {
            messageQueue.Enqueue(eventMessage);            
        }
    }
}
