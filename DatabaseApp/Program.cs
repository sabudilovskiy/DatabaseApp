using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;





namespace DatabaseApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            AppContext app= new AppContext();
            ConsoleController consoleController = new ConsoleController(app);
            consoleController.Start();
        }
    }
}
