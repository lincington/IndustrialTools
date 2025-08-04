using Prism.Commands;
using Prism.Mvvm;

namespace IndustrialTools.Core
{
    public interface IApplicationCommands
    {
        CompositeCommand Vision  { get; }

        CompositeCommand Motion { get; }

        CompositeCommand SECS  { get; }

        CompositeCommand Connection { get; }

        CompositeCommand Help  { get; }
     
    }

    public class ApplicationCommands : IApplicationCommands
    {
        public CompositeCommand Connection  { get; } = new CompositeCommand();

        public CompositeCommand Help { get; } = new CompositeCommand();

        public CompositeCommand Vision { get; } = new CompositeCommand();

        public CompositeCommand Motion { get; } = new CompositeCommand();

        public CompositeCommand SECS { get; } = new CompositeCommand();

    }
}
