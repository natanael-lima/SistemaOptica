using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClaseBase
{
    public class Zona
    {
        private int zona_Codigo;

        public int Zona_Codigo
        {
            get { return zona_Codigo; }
            set { zona_Codigo = value; }
        }
        private string zona_Descripcion;

        public string Zona_Descripcion
        {
            get { return zona_Descripcion; }
            set { zona_Descripcion = value; }
        }
        private string zona_Piso;

        public string Zona_Piso
        {
            get { return zona_Piso; }
            set { zona_Piso = value; }
        }
    
        public Zona()
        {
            throw new System.NotImplementedException();
        }
    }
}
