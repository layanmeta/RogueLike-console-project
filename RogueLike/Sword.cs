using System;
using System.Collections.Generic;
using System.Text;

namespace RogueLike
{
    public class Sword : Weapon
    {
        public Sword(string name, int damage, int speed = 0)
        {
            this.Name = name;
            this.Damage = damage;
            this.Speed = speed;
        }
    }
}
