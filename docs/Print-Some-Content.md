<!-- START doctoc generated TOC please keep comment here to allow auto update -->
<!-- DON'T EDIT THIS SECTION, INSTEAD RE-RUN doctoc TO UPDATE -->



<!-- END doctoc generated TOC please keep comment here to allow auto update -->

This is the quintessential "Hello World" program in Mutagen flavor.

Its goal is to load up a mod, and print the names of all the NPCs.
```csharp
using var mod = OblivionMod.CreateFromBinaryOverlay(pathToMod);
foreach (var name in mod.NPCs.Records
    .Select(npc => npc.Name)
    .Distinct()
    .Where(s => !string.IsNullOrWhiteSpace(s)))
{
    System.Console.WriteLine(name);
}
```

An outline of what is going on in the code above:
- A mod object is created (in the [Overlay Pattern](Binary-Overlay))
- The NPC group's records are iterated over
- Using typical C# LINQ patterns, `Name` is selected and filtered out if empty or duplicates
- Each name is printed out to the console
- The mod object is disposed as we exit the `using` statement, which closes out any open file streams

This code is self-sufficient, aside from needing to supply the desired `pathToMod` to open.  No other bootstrap code or other frameworking is required.

This example can be found in the Example Project, under the [PrintContent](https://github.com/Mutagen-Modding/Mutagen/blob/master/Mutagen.Bethesda.Examples/Code/PrintContentCode.cs) section.