using System;
using System.Collections.Generic;
using System.Text;

namespace RogueLike
{
    public abstract class Weapon : IEquipment
    {
        public string Name { get; set; }

        public int Damage { get; set; }

        public int Speed { get; set; }

        public int GetDamage()
        {
            return this.Damage;
        }
    }
}
