using Prism.Commands;
using Prism.Mvvm;

namespace IndustrialTools.Core
{
    public interface IApplicationCommands
    {
        CompositeCommand Connection { get; }
    }

    public class ApplicationCommands : IApplicationCommands
    {
        public CompositeCommand Connection  { get; } = new CompositeCommand();
    }
}
