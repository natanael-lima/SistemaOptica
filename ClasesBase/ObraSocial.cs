using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClasesBase
{
    public class ObraSocial
    {
        private int os_Id;

        public int Os_Id
        {
            get { return os_Id; }
            set { os_Id = value; }
        }
        private string os_CUIT;

        public string Os_CUIT
        {
            get { return os_CUIT; }
            set { os_CUIT = value; }
        }
        private string os_RazonSocial;

        public string Os_RazonSocial
        {
            get { return os_RazonSocial; }
            set { os_RazonSocial = value; }
        }
        private string os_Direccion;

        public string Os_Direccion
        {
            get { return os_Direccion; }
            set { os_Direccion = value; }
        }
        private string os_Telefono;

        public string Os_Telefono
        {
            get { return os_Telefono; }
            set { os_Telefono = value; }
        }

        public ObraSocial()
        {

        }
    }
}
