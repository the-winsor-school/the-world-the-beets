using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheWorld
{
    /// <summary>
    /// An object that can be carried.
    /// </summary>
    public interface ICarryable
    {
        string Name { get; set; }
        string Description { get; set; }
    }
}
