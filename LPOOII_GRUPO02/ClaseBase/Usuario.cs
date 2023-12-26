using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace ClaseBase
{
    public class Usuario : IDataErrorInfo, INotifyPropertyChanged
    {
        private int user_Id;

        public int User_Id
        {
            get { return user_Id; }
            set
            {
                if (user_Id != value)
                {
                    user_Id = value;
                    OnPropertyChanged("User_Id");
                }
            }
        }

        private string user_Name;

        public string User_Name
        {
            get { return user_Name; }
            set
            {
                if (user_Name != value)
                {
                    user_Name = value;
                    OnPropertyChanged("User_Name");
                }
            }
        }

        private string user_Password;

        public string User_Password
        {
            get { return user_Password; }
            set
            {
                if (user_Password != value)
                {
                    user_Password = value;
                    OnPropertyChanged("User_Password");
                }
            }
        }

        private string user_Nombre;

        public string User_Nombre
        {
            get { return user_Nombre; }
            set
            {
                if (user_Nombre != value)
                {
                    user_Nombre = value;
                    OnPropertyChanged("User_Nombre");
                }
            }
        }

        private string user_Apellido;

        public string User_Apellido
        {
            get { return user_Apellido; }
            set
            {
                if (user_Apellido != value)
                {
                    user_Apellido = value;
                    OnPropertyChanged("User_Apellido");
                }
            }
        }

        private string user_Rol;

        public string User_Rol
        {
            get { return user_Rol; }
            set
            {
                if (user_Rol != value)
                {
                    user_Rol = value;
                    OnPropertyChanged("User_Rol");
                }
            }
        }

        private string user_Foto;

        public string User_Foto
        {
            get { return user_Foto; }
            set
            {
                if (user_Foto != value)
                {
                    user_Foto = value;
                    OnPropertyChanged("User_Foto");
                }
            }
        }

        public Usuario()
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

                if (columnName == "User_Name")
                {
                    if (string.IsNullOrEmpty(User_Name))
                    {
                        result = "El nombre de usuario es obligatorio.";
                    }
                    else if (User_Name.Length < 3)
                    {
                        result = "El nombre de usuario debe tener al menos 3 caracteres.";
                    }
                    // Agregar más validaciones si es necesario
                }
                else if (columnName == "User_Password")
                {
                    if (string.IsNullOrEmpty(User_Password))
                    {
                        result = "La contraseña es obligatoria.";
                    }
                    else if (User_Password.Length < 3)
                    {
                        result = "La contraseña debe tener al menos 3 caracteres.";
                    }
                    // Agregar más validaciones si es necesario
                }
                else if (columnName == "User_Nombre")
                {
                    if (string.IsNullOrEmpty(User_Nombre))
                    {
                        result = "El nombre es obligatorio.";
                    }
                    else if (User_Nombre.Length < 4)
                    {
                        result = "El nombre debe tener al menos 4 caracteres.";
                    }
                    // Agregar más validaciones si es necesario
                }
                else if (columnName == "User_Apellido")
                {
                    if (string.IsNullOrEmpty(User_Apellido))
                    {
                        result = "El apellido es obligatorio.";
                    }
                    else if (User_Apellido.Length < 4)
                    {
                        result = "El apellido debe tener al menos 4 caracteres.";
                    }
                    // Agregar más validaciones si es necesario
                }
                else if (columnName == "User_Rol")
                {
                    if (string.IsNullOrEmpty(User_Rol))
                    {
                        result = "El rol es obligatorio.";
                    }
                    // Agregar más validaciones si es necesario
                }

                return result;
            }
        }
    }
}
