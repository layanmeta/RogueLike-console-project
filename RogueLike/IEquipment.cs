using System;
using System.Collections.Generic;
using System.Text;

namespace RogueLike
{
    //Interface for all equipments
    public interface IEquipment
    {
        public string Name { get; set; }
        public int Damage { get; set; }
    }
}
