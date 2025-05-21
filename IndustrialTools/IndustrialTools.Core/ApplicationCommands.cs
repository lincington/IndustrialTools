using Prism.Commands;
using Prism.Mvvm;

namespace IndustrialTools.Core
{
    public interface IApplicationCommands
    {
        CompositeCommand Connection { get; }

        // Add other commands here as needed
        CompositeCommand Help  { get; }
      

    }

    public class ApplicationCommands : IApplicationCommands
    {
        public CompositeCommand Connection  { get; } = new CompositeCommand();
        public CompositeCommand Help { get; } = new CompositeCommand();
        

    }
}
