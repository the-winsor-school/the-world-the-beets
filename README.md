# TheWorld
TheWorld text based adventure game programmed in C#. Used as a final project for Intro Programming.

## Software Requirements
* [Microsoft Visual Studio](https://visualstudio.microsoft.com/vs/community/) (For Windows or Mac)
* Xamarin Studio, which I have used to develop

## General Approach
TheWorld provides the foundation for an Object Oriented approach to a game.  It has very basic functionality as examples for students to build on.  The project provides basic classes which students can implement and develop extensions for.  This project allows students to explore **class hierarchy**, **static classes**, **interfaces**, and object interaction.

The project should be introduced with a read-through of the classes and discussion of how the different classes work together.  The WorldBuilder.cs class is an excellent place to start.

## Things to Watch Out For
### Keywords
When naming objects, areas, items or anything else, it is important to remember that the *unique identifiers* for each thing must be all lowercase and cannot contain spaces.  

For example:
```
start.AddItem(new Item()
{
  Name = "Boulder",
  Description = "It's a big granite boulder.  It has a weird glyph carved into it, but you can't make any sense of it."
}, "boulder");
```
The last parameter in the **AddItem** method is the *unique identifier* (unique within the **start** area, anyway).  The **Name** and **Description** fields are not subject to this rule because they are only used for display.  the *unique identifier* must be all lowercase and have no spaces because it is a word that the user types as part of a command.

It should be noted that this is an artifact of how the `Program.ParseCommand` method is written.  This is something students can improve!

### Connecting Neighbors
**Areas** must be linked together in order to be functionally a part of the world.  Also, if that connection is supposed to be bi-directional, the areas must also be doubly linked.

For Example:
```
start.AddNeighbor(stream, "north");
stream.AddNeighbor(start, "south");
```
This ensures that when I **go north** from the **start** Area, I enter the **stream** Area and can **go south** to return to **start**.  This also means that I can create Areas which are connected in only *one* direction (like, falling down a cliff to go from one Area to another).

### Deliberately poorly written algorithms
Some of the provided algorithms in the base code are deliberately poorly written with the intent that students will make improvements to the functionality.  Some of these problems are within the provided methods, while others are more structural (e.g. the logical placement of methods into particular classes).

Some code is also missing entirely!  The "help" command, as it is, is not very helpful at all.  These methods are flagged with **TODO:** comments.
