using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace ClaseBase
{
    public class TipoVehiculo : IDataErrorInfo, INotifyPropertyChanged
    {
        private int tv_Id;

        public int Tv_Id
        {
            get { return tv_Id; }
            set {
                if (tv_Id != value)
                {
                    tv_Id = value;
                    OnPropertyChanged("Tv_Id");
                }
            }
        }
        private string tv_Descripcion;

        public string Tv_Descripcion
        {
            get { return tv_Descripcion; }
            set {
                if (tv_Descripcion != value)
                    {
                        tv_Descripcion = value;
                        OnPropertyChanged("Tv_Descripcion");
                    }
            }
        }

        private decimal tv_Tarifa;

        public decimal Tv_Tarifa
        {
            get { return tv_Tarifa; }
            set
            {
                if (tv_Tarifa != value)
                {
                    tv_Tarifa = value;
                    OnPropertyChanged("Tv_Tarifa");
                }
            }
        }

        private string tv_Imagen;

        public string Tv_Imagen
        {
            get { return tv_Imagen; }
            set { tv_Imagen = value; }
        }

        public TipoVehiculo()
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

                if (columnName == "Tv_Descripcion")
                {
                    if (string.IsNullOrEmpty(Tv_Descripcion))
                    {
                        result = "La descripción es obligatoria.";
                    }else if (Tv_Descripcion.Length < 5)
                    {
                        result = "La descripción no puede ser menor a 5 digitos.";
                    }
                    // Agregar más validaciones si es necesario
                }
                else if (columnName == "Tv_Tarifa")
                {
                    if (Tv_Tarifa <=0 )
                    {
                        result = "La tarifa s obligatorio.";
                    }
                    else if (Tv_Tarifa < 200)
                    {
                        result = "La tarifa no puede ser menor a 200.";
                    }
                    // Agregar más validaciones si es necesario
                }

                return result;
            }
        }

        private int tv_Ex;

        public int Tv_Ex
        {
            get { return tv_Ex; }
            set { tv_Ex = value; }
        }

    }
}
