using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BP1_Kontrolna_tacka_1.UIHandler;
using Oracle.ManagedDataAccess.Client;

namespace BP1_Kontrolna_tacka_1
{
    class Program
    {
        //private static readonly ComplexQueryUIHandler complexQueryUIHandler = new ComplexQueryUIHandler();
        private static readonly MainUIHandler mainUIHandler = new MainUIHandler();

        static void Main(string[] args)
        {
            //complexQueryUIHandler.HandleComplexQueryMenu();
            mainUIHandler.HandleMainMenu();
        }
    }
}
