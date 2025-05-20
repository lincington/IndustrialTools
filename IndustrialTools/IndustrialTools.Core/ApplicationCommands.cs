using Prism.Commands;
using Prism.Mvvm;

namespace IndustrialTools.Core
{
    public interface IApplicationCommands
    {
        CompositeCommand Connection { get; }

        // Add other commands here as needed
        CompositeCommand Welcome { get; }
        CompositeCommand About   { get; }

    }

    public class ApplicationCommands : IApplicationCommands
    {
        public CompositeCommand Connection  { get; } = new CompositeCommand();
        public CompositeCommand Welcome { get; } = new CompositeCommand();
        public CompositeCommand About { get; } = new CompositeCommand();

    }
}
