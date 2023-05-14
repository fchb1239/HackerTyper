using System;
using ComputerInterface.Interfaces;

namespace HackerTyper.ComputerInterface
{
    class HackerEntry : IComputerModEntry
    {
        public string EntryName => "<color=green>Hacker Typer</color>";

        // This is the first view that is going to be shown if the user select you mod
        // The Computer Interface mod will instantiate your view 
        public Type EntryViewType => typeof(HackerView);
    }
}
