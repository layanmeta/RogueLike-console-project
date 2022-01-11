using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RogueLike
{
    class Game
    {
        //Notes for next time: HP resets every level despite HP loss in other levels 
        //make a death message
        //!!BUG, messages override each other (if a message is longer than the one after, the end of the message stays)
        private Map currentMap;
        private List<Map> _allMaps;
        private Player player;
        private UIHandler _uIHandler;
        public void Start()
        {
            Console.Title = "Welcome to my first RogueLike game!";
            Console.CursorVisible = false;
      
            string[,] map1 =
            {
                {"╔","═","═","═","═","═","╦","═","═","╗" },
                {"║"," "," ","║"," "," ","║"," ","X","║" },
                {"║"," "," ","║"," "," "," "," "," ","║" },
                {"║"," "," ","║","T"," "," "," "," ","║" },
                {"║"," "," ","║"," "," ","║"," "," ","║" },
                {"║"," "," "," "," "," ","║"," "," ","║" },
                {"║"," "," "," ","═","═","╣"," "," ","║" },
                {"║"," "," "," "," ","C","║"," "," ","║" },
                {"╚","═","═","═","═","═","╩","═","═","╝" }

            };

            string[,] map2 =
            {
                {"╔","═","═","╦","═","═","╦","═","═","═" ,"╦","═","═","╗" },
                {"║"," "," "," "," "," ","║"," "," "," " ," "," ","X","║" },
                {"║"," "," ","║"," "," ","║"," "," "," " ,"╔","═","═","║" },
                {"║"," "," ","║"," "," ","║"," "," "," " ,"║"," "," ","║" },
                {"║"," "," ","║"," "," ","║"," ","T"," " ," "," "," ","║" },
                {"║"," "," ","║"," "," "," "," "," "," " ,"║"," "," ","║" },
                {"║"," "," ","╚","═","═","╗"," "," "," " ,"║"," ","M","║" },
                {"║"," "," "," "," ","C","║"," "," "," " ,"║"," "," ","║" },
                {"╚","═","═","═","═","═","╩","═","═","═" ,"╩","═","═","╝" }
            };

            string[,] map3 =
            {
                {"╔","═","╦","═","═","═","╦","═","═","═" ,"╦","═","═","╗" },
                {"║"," ","║"," "," "," ","║"," "," "," " ," "," ","X","║" },
                {"║"," ","║"," ","╔"," ","║"," "," "," " ,"╔","═","═","║" },
                {"║"," ","║"," ","║"," ","║"," "," ","T" ,"║"," "," ","║" },
                {"║"," ","║"," ","║"," ","║"," "," "," " ," "," "," ","║" },
                {"║"," ","║"," ","║"," "," "," "," "," " ,"║"," "," ","║" },
                {"║"," "," "," ","╚","═","╗"," "," "," " ,"║"," ","C","║" },
                {"║"," "," "," "," ","M","║"," ","H"," " ,"║"," "," ","║" },
                {"╚","═","═","═","═","═","╩","═","═","═" ,"╩","═","═","╝" }
            };

            //list of all the maps added above
            _allMaps = new List<Map>();
            _allMaps.Add(new Map(map1));
            _allMaps.Add(new Map(map2));
            _allMaps.Add(new Map(map3));
            
            //prints the maps
            currentMap = _allMaps.First();
            player = new Player(1,1);

            //Init the UI Handler
            _uIHandler = new UIHandler();
            GameLoop();
        }

        public void Intro()
        {
            Console.ForegroundColor = ConsoleColor.White;
            //game intro
            Console.WriteLine("Welcome to my RogueLike game!");
            Console.WriteLine("\nHow to play:");
            Console.WriteLine("Use the arrow keys to move up, down, left, right");
            Console.WriteLine("Your goal is to reach X");
            Console.WriteLine("Press any key to start");
            Console.ReadKey(true);
        }

        public void Outro()
        {
            //after player beats all levels...
            Console.Clear();
            Console.WriteLine("Congrats! You Escaped!");
            Console.ReadKey(true);
        }

        public void Death()
        {
            Console.Clear();
            Console.WriteLine("You died, good luck next time");
            Console.ReadKey(true);
        }
        //Sets the given map as current map
        public void SetMap(Map map)
        {
            currentMap = map;
        }

        public void PlayerInput()
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
                case ConsoleKey.UpArrow:
                    if (currentMap.IsItWalkable(player.x, player.y - 1))
                    {
                        player.y -= 1;
                    }       
                    break;
                case ConsoleKey.DownArrow:
                    if (currentMap.IsItWalkable(player.x, player.y + 1))
                    {
                        player.y += 1;
                    }
                    break;
                case ConsoleKey.LeftArrow:
                    if (currentMap.IsItWalkable(player.x - 1, player.y ))
                    {
                        player.x -= 1;

                    }
                    break;
                case ConsoleKey.RightArrow:
                    if (currentMap.IsItWalkable(player.x + 1, player.y ))
                    {
                        player.x += 1;

                    }
                    break;            
                default:
                    break;
            }
        }

        public void Respawn()
        {
            //respawn the player when dies or steps on traps
            player.x = 1;
            player.y = 1;
            player.SetPlayer();
            player.Inventory.Clear();
        }

        public void GameLoop()
        {  
            Intro();
            Console.Clear();
            bool IsAlive = true;
            while (IsAlive)
            {
                _uIHandler.DrawUI(currentMap, player);
                PlayerInput();
                string elements = currentMap.GetElement(player.x, player.y);
                //all the elements and their implementations
                if (elements == "X")
                {
                    if (currentMap.HasCompletedRequirements(player))
                    {
                        var currrentMapIndex = _allMaps.IndexOf(currentMap) + 1;
                        //Go to the next map if its not the last level
                        if (currrentMapIndex < _allMaps.Count)
                        {
                            SetMap(_allMaps[currrentMapIndex]);
                            Respawn();
                            continue;
                        }

                        Outro();
                        break;
                    }
                    else
                    {
                        _uIHandler.AddEventMessage("You need a key for this exit");
                    }
                }
                else if (elements == "T")
                {
                    _uIHandler.AddEventMessage("You stepped on a trap! You lost 1hp");
                    currentMap.steppedOnTrap = true;
                    player.PlayerAttributes.Hp -= 1;
                    Respawn();
                }
                else if (elements == "M")
                {
                    //Monster function
                }
                else if (elements == "C")
                {
                    //BUG!!!!Only need to collect c once,then am able to leave all maps without it
                    if (!player.Inventory.Contains("Key"))
                    {
                        _uIHandler.AddEventMessage("You gained a key to the exit gate!");
                        currentMap.FoundChest(player);
                        currentMap.hasKey = true;
                    }
                    else
                    {
                        _uIHandler.AddEventMessage("Sorry but you cannot have more keys");
                    }                   
                }
                else if (elements == "H")
                {
                    _uIHandler.AddEventMessage("You gained 1 heart!");
                    player.PlayerAttributes.Hp += 1;
                }                  
            }
            Death();
        }
    }
}
