using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace ClaseBase
{
    public class Cliente : IDataErrorInfo, INotifyPropertyChanged
    {
        private int cli_Id;

        public int Cli_Id
        {
            get { return cli_Id; }
            set
            {
                if (cli_Id != value)
                {
                    cli_Id = value;
                    OnPropertyChanged("Cli_Id");
                }
            }
        }
        private long cli_DNI;

        public long Cli_DNI
        {
            get { return cli_DNI; }
            set
            {
                if (cli_DNI != value)
                {
                    cli_DNI = value;
                    OnPropertyChanged("Cli_DNI");
                }
            }
        }
        private string cli_Apellido;

        public string Cli_Apellido
        {
            get { return cli_Apellido; }
            set
            {
                if (cli_Apellido != value)
                {
                    cli_Apellido = value;
                    OnPropertyChanged("Cli_Apellido");
                }
            }
        }
        private string cli_Nombre;

        public string Cli_Nombre
        {
            get { return cli_Nombre; }
            set
            {
                if (cli_Nombre != value)
                {
                    cli_Nombre = value;
                    OnPropertyChanged("Cli_Nombre");
                    
                }
            }
        }
        private long cli_Telefono;

        public long Cli_Telefono
        {
            get { return cli_Telefono; }
            set
            {
                if (cli_Telefono != value)
                {
                    cli_Telefono = value;
                    OnPropertyChanged("Cli_Telefono");
                }
            }
        }

        public Cliente()
        {
        }

        // Implementación de INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            var handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }


        // Implementación de IDataErrorInfo
        public string Error
        {
            get { return null; }
        }

        public string this[string columnName]
        {
            get
            {
                string result = null;

                if (columnName == "Cli_DNI")
                {
                    if (Cli_DNI <= 0) 
                    {
                        result = "El DNI del cliente es obligatorio.";
                    }else if (Cli_DNI < 9999999)
                    {
                        result = "El DNI no debe tener menos de 8 digitos";
                    }
                    else if (Cli_DNI > 99999999)
                    {
                        result = "El DNI no debe tener mas de 8 digitos";
                    }
                    // Agregar más validaciones si es necesario
                }
                else if (columnName == "Cli_Apellido")
                {
                    if (string.IsNullOrEmpty(Cli_Apellido))
                    {
                        result = "El apellido es obligatorio.";
                    }
                    else if (Cli_Apellido.Length < 3)
                        result = "El apellido debe tener al menos 3 carácteres";
                }
                else if (columnName == "Cli_Nombre")
                {
                    if (string.IsNullOrEmpty(Cli_Nombre))
                    {
                        result = "El nombre es obligatorio.";
                    }
                    else if (Cli_Nombre.Length < 3)
                        result = "El nombre debe tener al menos 3 carácteres";
                }
                else if (columnName == "Cli_Telefono")
                {
                    if (Cli_Telefono <= 0)
                    {
                        result = "El telefono es obligatorio.";
                    }
                    else if (Cli_Telefono < 999999999)
                        result = "El telefono debe tener al menos 10 carácteres";
                }

                return result;
            }
        }


        public override string ToString()
        {
            return ""+Cli_Id+Cli_Nombre+Cli_DNI+Cli_Apellido+Cli_DNI;
        }

        private int cli_Ex;

        public int Cli_Ex
        {
            get { return cli_Ex; }
            set { cli_Ex = value; }
        }



    }
}