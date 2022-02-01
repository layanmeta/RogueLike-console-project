using System;
using System.Collections.Generic;
using System.Linq;

namespace RogueLike
{
    public class Game
    {
        private List<Map> _allMaps;
        private Map _currentMap;
        private Player _player;
        private List<Enemy> _enemies;

        private bool _isGameOver;

        private UIHandler _uIHandler;

        public void Start()
        {
            Init();
            GameLoop();
        }

        public void Init()
        {
            Console.CursorVisible = false;

            //Add all maps
            _allMaps = new List<Map>();
            new LevelCreator(_allMaps);

            //prints the maps
            _player = new Player(1, 1);

            //Init enemies
            _enemies = new List<Enemy>();

            //Init the UI Handler
            _uIHandler = new UIHandler();
        }

        public void Intro()
        {
            //game intro       
            Console.WriteLine("How to play:");
            Console.WriteLine("Use WASD keys to move up, down, left, right");
            Console.Write("Your goal is to reach ");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("X");
            Console.ResetColor();
            Console.Write("\nIn order to leave, you need to first collect ");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("C");
            Console.ResetColor();
            Console.Write("\nBe careful because there are hidden traps along the way- ");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write(" T");
            Console.ResetColor();
            Console.Write(" and monsters");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write(" M");
            Console.ResetColor();
            Console.WriteLine("\nPress any key to start");
            Console.ReadKey(true);
            Console.Clear();
        }

        public void Outro()
        {
            //after player beats all levels...
            Console.Clear();
            Console.WriteLine("Congrats! You Escaped!");
            Console.ReadKey(true);
        }

        public void GameOver()
        {
            _isGameOver = true;
            Console.Clear();
            Console.WriteLine("You died, good luck next time");
            Console.ReadKey(true);
        }       

        /// <summary>
        /// Spawn all enemies in the current map
        /// </summary>
        public void SpawnEnemies()
        {
            _enemies.Clear();
            //enemy's spawns
            for (var y = 0; y < _currentMap.rows; y++)
            {
                for (var x = 0; x < _currentMap.cols; x++)
                {
                    if (_currentMap.Grid[y, x] == "M")
                    {
                        _currentMap.Grid[y, x] = " ";
                        _enemies.Add(new Enemy(x, y));
                    }
                }
            }
        }

        /// <summary>
        /// Puts a new level into the game
        /// </summary>
        /// <param name="level"></param>
        public void InitializeNewMap(int level)
        {
            _currentMap = _allMaps[level];
            _player.Respawn();
            SpawnEnemies();
        }

        public void GameLoop()
        {
            //Shows the menu
            Intro();

            //Init the first map
            InitializeNewMap(0);

            while (!_isGameOver)
            {
                //Draws everything 
                _uIHandler.DrawUI(_currentMap, _player, _enemies);

                //Handle player input 
                _player.HandlePlayerInput(_currentMap);
                //Handle enemies inputs
                foreach (var enemy in _enemies) enemy.HandleMovement(_currentMap);

                string elements = _currentMap.GetElement(_player.x, _player.y);
                //all the elements and their implementations
                if (elements == "X")
                {
                    if (_currentMap.Exit(_player))
                    {
                        _uIHandler.AddEventMessage("You used your key to exit");
                        var nextMapIndex = _allMaps.IndexOf(_currentMap) + 1;
                        if (nextMapIndex < _allMaps.Count) InitializeNewMap(nextMapIndex);
                        else
                        {
                            Outro();
                            break;
                        }
                    }
                    else _uIHandler.AddEventMessage("You need a key for this exit");                                   
                }
                else if (elements == "T")
                {
                    _uIHandler.AddEventMessage("You stepped on a trap! You lost 1hp");
                    _currentMap.ActivatedTrap(_player);                    
                }
                else if (elements == "C")
                {
                    if (!_player.Inventory.Contains("Key"))
                    {
                        _uIHandler.AddEventMessage("You gained a key to the exit gate!");
                        _currentMap.FoundChest(_player);                      
                    }
                    else _uIHandler.AddEventMessage("Sorry but you cannot have more keys");
                }
                else if (elements == "!")
                {
                    if(_currentMap.FoundWeapon(_player)) _uIHandler.AddEventMessage("You got a new weapon! Your damage and speed got changed");
                    else _uIHandler.AddEventMessage("You cant get more weapons");
                }
                else if (elements == "H")
                {
                    if(_currentMap.FoundHeart(_player)) _uIHandler.AddEventMessage("You gained 3 hearts!");
                    else _uIHandler.AddEventMessage("You cant get more health");
                }

                //Checks if a monster interacts with player                
                var enemyToFight = _enemies.FirstOrDefault(e => e.x == _player.x && e.y == _player.y);
                if (enemyToFight != null)
                {
                    _uIHandler.AddEventMessage("You encountered a monster");
                    //Get both enemyToFight and players speed and decide who will attack
                    var random = new Random();
                    var playerLuck = random.Next(0, _player.Stats.Speed);
                    var enemyLuck = random.Next(0, enemyToFight.Stats.Speed);
                    if (playerLuck > enemyLuck) 
                    {
                        var totalDamage = _player.Stats.totalDamage; //Player should have a total dmg stat
                        enemyToFight.GetHit(totalDamage);
                        _uIHandler.AddEventMessage($"You hit the monster with {totalDamage} damage");
                        if (enemyToFight.Stats.Hp <= 0)
                        {
                            enemyToFight.Kill(_enemies);
                            _uIHandler.AddEventMessage("You killed a monster");
                        }
                    }
                    else
                    {
                        _player.GetHit(enemyToFight.Stats.damage);                        
                        _uIHandler.AddEventMessage($"The monster hit you with {enemyToFight.Stats.damage} damage");
                    }
                }

                if (_player.Stats.Hp <= 0) GameOver();
            }
        }
    }
}
