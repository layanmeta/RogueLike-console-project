using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RogueLike
{
    public class Enemy
    {
        public int x { get; set; }
        public int y { get; set; }
        private string enemyMark;
        private ConsoleColor enemyColor;

        public Stats Stats = new Stats();

        public Enemy(int x, int y)
        {
            Stats.Hp = 5;
            Stats.damage = 3;
            Stats.Speed = 1;
            this.x = x;
            this.y = y;

            enemyMark = "M";
            enemyColor = ConsoleColor.Red;
        }

        public void SetPosition()
        {
            //sets enemy's position
            Console.ForegroundColor = enemyColor;
            Console.SetCursorPosition(x, y);
            Console.Write(enemyMark);
            Console.ResetColor();
        }

        public void HandleMovement(Map map)
        {
            var random = new Random();
            var x = random.Next(-1, 2);
            var y = random.Next(-1, 2);
            if (map.IsItWalkable(this.x + x, this.y + y))
            {
                this.x += x;
                this.y += y;
            }
        }

        public void Kill(List<Enemy> currentEnemyList)
        {
            currentEnemyList.Remove(this);
        }

        public void GetHit(int dmg)
        {
            Stats.Hp -= dmg;;
        }
    }
}
