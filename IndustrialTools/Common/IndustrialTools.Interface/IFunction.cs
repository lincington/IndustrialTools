namespace IndustrialTools.Interface
{
    public interface IFunction
    {
        string Execute();
    }

    public class FunctionA : IFunction
    {
        public string Execute() => "FunctionA executed";
    }

    public class FunctionB : IFunction
    {
        public string Execute() => "FunctionB executed";
    }


}
