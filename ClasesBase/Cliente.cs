using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClasesBase
{
    public class Cliente
    {
        private int cli_Id;

        public int Cli_Id
        {
            get { return cli_Id; }
            set { cli_Id = value; }
        }

        private string cli_DNI;

        public string Cli_DNI
        {
            get { return cli_DNI; }
            set { cli_DNI = value; }
        }

        private string cli_Nombre;

        public string Cli_Nombre
        {
            get { return cli_Nombre; }
            set { cli_Nombre = value; }
        }
 
        private string cli_Apellido;

        public string Cli_Apellido
        {
            get { return cli_Apellido; }
            set { cli_Apellido = value; }
        }
        private string cli_Direccion;

        public string Cli_Direccion
        {
            get { return cli_Direccion; }
            set { cli_Direccion = value; }
        }
        private string os_CUIT;

        public string Os_CUIT
        {
            get { return os_CUIT; }
            set { os_CUIT = value; }
        }
        private string cli_NroCarnet;

        public string Cli_NroCarnet
        {
            get { return cli_NroCarnet; }
            set { cli_NroCarnet = value; }
        }
        private int cli_Activo;

        public int Cli_Activo
        {
            get { return cli_Activo; }
            set { cli_Activo = value; }
        }
        public Cliente()
        {

        }

        public Cliente(string dni, string nombre, string apellido, string direccion, string cuit, string nrocarnet, int activo) {
            Cli_DNI = dni;
            Cli_Nombre = nombre;
            Cli_Apellido = apellido;
            Cli_Direccion = direccion;
            Os_CUIT = cuit;
            Cli_NroCarnet = nrocarnet;
            Cli_Activo = activo;
        }

         
    }
}
