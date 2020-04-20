using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheWorld
{
    /// <summary>
    /// An item that can be equipped
  
    public interface IEquippableItem
    {
        string Name { get; set; }
        string Description { get; set; }
        StatChart Stats // it has stats
        {
            get;
            set;
        }
        bool IsEquipped // it can be equipped 
        {
            get;
            set;
        }

        void Equip();

       

        
    }
}
