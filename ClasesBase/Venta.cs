using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClasesBase
{
    public class Venta
    {
        private int ven_Nro;
        private DateTime ven_Fecha;
        private int cli_Id;

        public int Cli_Id
        {
            get { return cli_Id; }
            set { cli_Id = value; }
        }

        public DateTime Ven_Fecha
        {
            get { return ven_Fecha; }
            set { ven_Fecha = value; }
        }

        public int Ven_Nro
        {
            get { return ven_Nro; }
            set { ven_Nro = value; }
        }
    }
}
