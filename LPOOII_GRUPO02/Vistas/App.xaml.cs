using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows;
using ClaseBase;

namespace Vistas
{
    
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        // Variable utilizada para guardar el rol del empleado, la utilizo en FromMain
        public static Usuario UserGlobal { get; set; }
    }
}
