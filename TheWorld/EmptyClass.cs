using System;
namespace TheWorld
{
    public class Backpack
    {
        public Backpack()
        {

        }
    }
}
/// <summary>
/// The items. In Stacks.  By Name.
/// _______________________________________
/// TODO: Hard Achievement
/// Encapsulate this Backpack into a Specialized container class.
/// The Backpack class should include additional properties such as:
///
/// int MaxCapacity  //how much weight can it hold?
/// int CurrentWeight //Calculates the weight of all items currently in it
/// bool Contains(string itemName)
///
/// void Add(ICarryable item, string uid)
/// void Remove(string uid)
///
/// _______________________________________
/// TODO: Hard Achievement (2)
/// Add to the Backpack class
///
/// void Use(string uid)
///
/// such that, only usable items which are in the backpack can be used directly from the backpack.
/// don't forget to handle events like ItemDepletedException.
/// </summary>