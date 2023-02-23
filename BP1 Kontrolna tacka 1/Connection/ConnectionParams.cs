using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BP1_Kontrolna_tacka_1.Connection
{
    public class ConnectionParams
    {

        public static readonly string LOCAL_DATA_SOURCE = "//localhost:1521/xe";
        public static readonly string CLASSROOM_DATA_SOURCE = "//192.168.0.102:1522/db2016";

        public static readonly string USER_ID = "student"; // Uglavnom je sam naziv baze. (Valjda?)
        public static readonly string PASSWORD = "ftn2"; // UVEK JE FTN. (Nesto sam uradio i resetovao i sad je ftn2)
    }
}
