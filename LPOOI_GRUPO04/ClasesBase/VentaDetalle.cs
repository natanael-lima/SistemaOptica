using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClasesBase
{
    public class VentaDetalle
    {
        private int det_Nro;
        private int ven_Nro;
        private double det_Precio;
        private double det_Cantidad;
        private double det_Total;
        private int prod_Id;

        public int Prod_Id
        {
            get { return prod_Id; }
            set { prod_Id = value; }
        }

        public double Det_Total
        {
            get { return det_Total; }
            set { det_Total = value; }
        }

        public double Det_Cantidad
        {
            get { return det_Cantidad; }
            set { det_Cantidad = value; }
        }

        public double Det_Precio
        {
            get { return det_Precio; }
            set { det_Precio = value; }
        }

        public int Ven_Nro
        {
            get { return ven_Nro; }
            set { ven_Nro = value; }
        }

        public int Det_Nro
        {
            get { return det_Nro; }
            set { det_Nro = value; }
        }

        public double total()
        {
            return this.det_Cantidad * this.det_Precio;
        }
    }
}
