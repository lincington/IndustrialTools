using IndustrialTools.Common;
using Microsoft.IdentityModel.Logging;
using SqlSugar;

namespace IndustrialToolsConsole
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Task[] tasks = new Task[2];
            String[] files = null;
            String[] dirs = null;
            String docsDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            tasks[0] = Task.Factory.StartNew(() => files = Directory.GetFiles(docsDirectory));
            tasks[1] = Task.Factory.StartNew(() => dirs = Directory.GetDirectories(docsDirectory));

            Task.Factory.ContinueWhenAll(tasks, completedTasks => {
                Console.WriteLine("{0} contains: ", docsDirectory);
                Console.WriteLine("   {0} subdirectories", dirs.Length);
                Console.WriteLine("   {0} files", files.Length);
            });
        }
    }

    public class ComplexInfoModel
    {
        public DbType Key { get; set; }
        public string Text { get; set; } = "";
    }
}
