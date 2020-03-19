using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheWorld
{
    /// <summary>
    /// An object that can be carried.
    ///
    /// As a bit of Standard Nomenclature, we add an I infront of the name of
    /// Interfaces just as a quick way of noting the difference.  Your IDE also
    /// colors it differently to make that difference more clear.
    /// </summary>
    public interface ICarryable
    {
        string Name { get; set; }
        string Description { get; set; }

        int Weight { get; set; }
    }
}
