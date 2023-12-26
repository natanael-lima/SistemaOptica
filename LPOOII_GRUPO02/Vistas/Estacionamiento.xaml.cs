using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using LibreriaRecursos;
using System.Globalization;
using System.Collections.ObjectModel;
using ClaseBase;
namespace Vistas
{
    /// <summary>
    /// Interaction logic for Estacionamiento.xaml
    /// </summary>
    public partial class Estacionamiento : Window
    {
        private Brush[] colores = { Brushes.Red,Brushes.Green,Brushes.Gray};
        private bool sectorDisponible = true;

        public Estacionamiento()
        {
            InitializeComponent();
        }

        private void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            salir();
        }

        private void btnSalir_Click(object sender, RoutedEventArgs e)
        {
            salir();
        }

       private void salir()
        {
            FormMain menuPrincipal = new FormMain();
            menuPrincipal.Show();
            this.Close();
        }
        //Actualizacion de Botones
       private void actualizarEstadoSector(Button boton)
       {
           SolidColorBrush colorBoton = boton.Background as SolidColorBrush;

           if (colorBoton != null && colorBoton.Color == Colors.Green)
           {
               sectorDisponible = true;
               actualizarColorSector(boton);
           }
           else
           {
               sectorDisponible = false;
               actualizarColorSector(boton);
           }
       }

       private void actualizarColorSector(Button boton)
       {
           if (sectorDisponible)
           {
               MessageBoxResult resp = MessageBox.Show("Registrar Entrada", "Sector Disponible", MessageBoxButton.YesNo);
               if (resp == MessageBoxResult.Yes)
               {
                   boton.Background = colores[0];//rojo
               }
           }
           else
           {
               MessageBoxResult resp = MessageBox.Show("Registrar Salida", "Sector Ocupado", MessageBoxButton.YesNo);
               if (resp == MessageBoxResult.Yes)
               {
                   boton.Background = colores[1];//verde
               }
           }
       }


       private void btnE2_Click(object sender, RoutedEventArgs e)
       {
           RegistrarEntrada frmRegistrar = new RegistrarEntrada(this, btnE2.Content.ToString());
           frmRegistrar.Show();
       }


       private void btnE6_Click(object sender, RoutedEventArgs e)
       {
           RegistrarEntrada frmRegistrar = new RegistrarEntrada(this, btnE6.Content.ToString());
           frmRegistrar.Show();
       }


       //entra al formulario para registrar una entrada
       private void btnE1_Click(object sender, RoutedEventArgs e)
       {
           RegistrarEntrada frmRegistrar = new RegistrarEntrada(this,btnE1.Content.ToString());
           frmRegistrar.Show();
       }

       //pinta el rectangulo segun el tiempo;
       public void pintar(int min,string cod)
       {
           ConversorDeEstados conversor = new ConversorDeEstados();
           Rectangle rectangulo = FindName(cod) as Rectangle;
           Object result = conversor.Convert(min, typeof(Brush), null, CultureInfo.CurrentCulture);
           Brush brush = result as Brush;
           rectangulo.Fill = brush;
       }

      //metodo que pinta cada rectangulo segun la situacion (ocupado, disponible)
       private void chekearPintar()
       {
           ObservableCollection<Sector> lista= new ObservableCollection<Sector>();
           lista = TrabajarSector.traerSectores();

           foreach (Sector sec in lista)
           {
               Ticket tiket = new Ticket();
               tiket = TrabajarTicket.traerTicket(TrabajarSector.traerSector(sec.Sec_Identificador));

               if (tiket != null)
               {   
                   //MessageBox.Show(tiket.ToString());
                   if (TrabajarSector.traerSector(sec.Sec_Identificador).Sec_Habilitado == 2)
                   {
                       pintar(Convert.ToInt32(tiket.T_Duracion), sec.Sec_Identificador);
                   }
                   else if (TrabajarSector.traerSector(sec.Sec_Identificador).Sec_Habilitado == 1)
                   {
                       pintar(0, sec.Sec_Identificador);
                   }
               }
               else if (TrabajarSector.traerSector(sec.Sec_Identificador).Sec_Habilitado == 0)
               {
                   pintar(200, sec.Sec_Identificador);
               }
               else{
                   pintar(0, sec.Sec_Identificador);
               }
               tiket = new Ticket();
           }
           
       }

       private void Window_Loaded(object sender, RoutedEventArgs e)
       {
           chekearPintar();
           
       }


       //tpf p2
       private void btnE1_MouseEnter(object sender, MouseEventArgs e)
       {
           btnE1.ToolTip = toolTipp("E1");
       }

       private void btnE3_Click(object sender, RoutedEventArgs e)
       {
           //Manda el formulario y el nombre del sector...
           if (TrabajarSector.traerSector("E3").Sec_Habilitado != 0)
           {
               RegistrarEntrada frmRegistrar = new RegistrarEntrada(this, btnE3.Content.ToString());
               frmRegistrar.Show();
           }
       }

       private void btnE3_MouseEnter(object sender, MouseEventArgs e)
       {
           btnE3.ToolTip = toolTipp("E3");
       }

       //tpf pt2 tooltip segun el sector
       private ToolTip toolTipp(string codSec)
       {
           Ticket tiket = new Ticket();
           tiket = TrabajarTicket.traerTicket(TrabajarSector.traerSector(codSec));

           string info = "";
           ToolTip toolTip = new ToolTip();


           if (tiket != null)
           {
               if (TrabajarSector.traerSector(codSec).Sec_Habilitado == 2)
               {
                   TimeSpan dif = tiket.T_FechaHoraEnt - DateTime.Now; 
 
                   string tiempoTrans = dif.ToString(@"hh\:mm\:ss");
                   info = "El sector esta ocupado \n Tiempo transcurrido: " + tiempoTrans + " \n Tipo de Vehiculo: " + tiket.Tv_Id.Tv_Descripcion + "\n Total: $" + tiket.T_Total;
                   toolTip.Content = info;
               }
               else if (TrabajarSector.traerSector(codSec).Sec_Habilitado == 1)
               {
                   DateTime ahora = DateTime.Now;

                   TimeSpan tiempoTranscurrido = tiket.T_FechaHoraSal - ahora; 
                   info = "Sector libre desde las: " + tiempoTranscurrido.ToString(@"hh\:mm\:ss");
                   toolTip.Content = info;
               }
               

           }
           else if (TrabajarSector.traerSector(codSec).Sec_Habilitado == 0)
           {
               info = "El sector no esta disponible";
               toolTip.Content = info;
           }
           else
           {
               DateTime ahora = DateTime.Now;
               DateTime horaInicio = new DateTime(ahora.Year, ahora.Month, ahora.Day, 8, 0, 0);
               TimeSpan tiempoTranscurrido = horaInicio - ahora; 
               info = "Sector libre desde las: " + tiempoTranscurrido.ToString(@"hh\:mm\:ss");
               toolTip.Content = info;
           }

           return toolTip;
       }

       private void btnE2_MouseEnter(object sender, MouseEventArgs e)
       {
           btnE2.ToolTip = toolTipp("E2");
       }

       private void btnE6_MouseEnter(object sender, MouseEventArgs e)
       {
           btnE6.ToolTip = toolTipp("E6");
       }

       private void btnE7_Click(object sender, RoutedEventArgs e)
       {
           RegistrarEntrada frmRegistrar = new RegistrarEntrada(this, btnE7.Content.ToString());
           frmRegistrar.Show();
       }

       private void btnE7_MouseEnter(object sender, MouseEventArgs e)
       {
           btnE7.ToolTip = toolTipp("E7");
       }

       private void btnE8_Click(object sender, RoutedEventArgs e)
       {
           RegistrarEntrada frmRegistrar = new RegistrarEntrada(this, btnE8.Content.ToString());
           frmRegistrar.Show();
       }

       private void btnE8_MouseEnter(object sender, MouseEventArgs e)
       {
           btnE8.ToolTip = toolTipp("E8");
       }

       private void btnE4_Click(object sender, RoutedEventArgs e)
       {
           RegistrarEntrada frmRegistrar = new RegistrarEntrada(this, btnE4.Content.ToString());
           frmRegistrar.Show();
       }

       private void btnE4_MouseEnter(object sender, MouseEventArgs e)
       {
           btnE4.ToolTip = toolTipp("E4");
       }

       private void btnE9_Click(object sender, RoutedEventArgs e)
       {
           RegistrarEntrada frmRegistrar = new RegistrarEntrada(this, btnE9.Content.ToString());
           frmRegistrar.Show();
       }

       private void btnE9_MouseEnter(object sender, MouseEventArgs e)
       {
           btnE9.ToolTip = toolTipp("E9");
       }

       private void btnE5_Click(object sender, RoutedEventArgs e)
       {
           RegistrarEntrada frmRegistrar = new RegistrarEntrada(this, btnE5.Content.ToString());
           frmRegistrar.Show();
       }

       private void btnE5_MouseEnter(object sender, MouseEventArgs e)
       {
           btnE5.ToolTip = toolTipp("E5");
       }

       private void btnE10_Click(object sender, RoutedEventArgs e)
       {
           RegistrarEntrada frmRegistrar = new RegistrarEntrada(this, btnE10.Content.ToString());
           frmRegistrar.Show();
       }

       private void btnE10_MouseEnter(object sender, MouseEventArgs e)
       {
           btnE10.ToolTip = toolTipp("E10");
       }

       private void btnE11_Click(object sender, RoutedEventArgs e)
       {
           RegistrarEntrada frmRegistrar = new RegistrarEntrada(this, btnE11.Content.ToString());
           frmRegistrar.Show();
       }

       private void btnE11_MouseEnter(object sender, MouseEventArgs e)
       {
           btnE11.ToolTip = toolTipp("E11");
       }

       private void btnE16_Click(object sender, RoutedEventArgs e)
       {
           RegistrarEntrada frmRegistrar = new RegistrarEntrada(this, btnE16.Content.ToString());
           frmRegistrar.Show();
       }

       private void btnE16_MouseEnter(object sender, MouseEventArgs e)
       {
           btnE16.ToolTip = toolTipp("E16");
       }

       private void btnE12_Click(object sender, RoutedEventArgs e)
       {
           RegistrarEntrada frmRegistrar = new RegistrarEntrada(this, btnE12.Content.ToString());
           frmRegistrar.Show();
       }

       private void btnE12_MouseEnter(object sender, MouseEventArgs e)
       {
           btnE12.ToolTip = toolTipp("E12");
       }

       private void btnE17_Click(object sender, RoutedEventArgs e)
       {
           RegistrarEntrada frmRegistrar = new RegistrarEntrada(this, btnE17.Content.ToString());
           frmRegistrar.Show();
       }

       private void btnE17_MouseEnter(object sender, MouseEventArgs e)
       {
           btnE17.ToolTip = toolTipp("E17");
       }

       private void btnE13_Click(object sender, RoutedEventArgs e)
       {
           RegistrarEntrada frmRegistrar = new RegistrarEntrada(this, btnE13.Content.ToString());
           frmRegistrar.Show();
       }

       private void btnE13_MouseEnter(object sender, MouseEventArgs e)
       {
           btnE13.ToolTip = toolTipp("E13");
       }

       private void btnE18_Click(object sender, RoutedEventArgs e)
       {
           RegistrarEntrada frmRegistrar = new RegistrarEntrada(this, btnE18.Content.ToString());
           frmRegistrar.Show();
       }

       private void btnE18_MouseEnter(object sender, MouseEventArgs e)
       {
           btnE18.ToolTip = toolTipp("E18");
       }

       private void btnE14_Click(object sender, RoutedEventArgs e)
       {
           RegistrarEntrada frmRegistrar = new RegistrarEntrada(this, btnE14.Content.ToString());
           frmRegistrar.Show();
       }

       private void btnE14_MouseEnter(object sender, MouseEventArgs e)
       {
           btnE14.ToolTip = toolTipp("E14");
       }

       private void btnE19_Click(object sender, RoutedEventArgs e)
       {
           RegistrarEntrada frmRegistrar = new RegistrarEntrada(this, btnE19.Content.ToString());
           frmRegistrar.Show();
       }

       private void btnE19_MouseEnter(object sender, MouseEventArgs e)
       {
           btnE19.ToolTip = toolTipp("E19");
       }

       private void btnE15_Click(object sender, RoutedEventArgs e)
       {
           RegistrarEntrada frmRegistrar = new RegistrarEntrada(this, btnE15.Content.ToString());
           frmRegistrar.Show();
       }

       private void btnE15_MouseEnter(object sender, MouseEventArgs e)
       {
           btnE15.ToolTip = toolTipp("E15");
       }

       private void btnE20_Click(object sender, RoutedEventArgs e)
       {
           RegistrarEntrada frmRegistrar = new RegistrarEntrada(this, btnE20.Content.ToString());
           frmRegistrar.Show();
       }

       private void btnE20_MouseEnter(object sender, MouseEventArgs e)
       {
           btnE20.ToolTip = toolTipp("E20");
       }

       private void btnE21_Click(object sender, RoutedEventArgs e)
       {
           RegistrarEntrada frmRegistrar = new RegistrarEntrada(this, btnE21.Content.ToString());
           frmRegistrar.Show();
       }

       private void btnE21_MouseEnter(object sender, MouseEventArgs e)
       {
           btnE21.ToolTip = toolTipp("E21");
       }

       private void btnE26_Click(object sender, RoutedEventArgs e)
       {
           RegistrarEntrada frmRegistrar = new RegistrarEntrada(this, btnE26.Content.ToString());
           frmRegistrar.Show();
       }

       private void btnE26_MouseEnter(object sender, MouseEventArgs e)
       {
           btnE26.ToolTip = toolTipp("E26");
       }

       private void btnE22_Click(object sender, RoutedEventArgs e)
       {
           RegistrarEntrada frmRegistrar = new RegistrarEntrada(this, btnE22.Content.ToString());
           frmRegistrar.Show();
       }

       private void btnE22_MouseEnter(object sender, MouseEventArgs e)
       {
           btnE22.ToolTip = toolTipp("E22");
       }

       private void btnE27_Click(object sender, RoutedEventArgs e)
       {
           RegistrarEntrada frmRegistrar = new RegistrarEntrada(this, btnE27.Content.ToString());
           frmRegistrar.Show();
       }

       private void btnE27_MouseEnter(object sender, MouseEventArgs e)
       {
           btnE27.ToolTip = toolTipp("E27");
       }

       private void btnE23_Click(object sender, RoutedEventArgs e)
       {
           RegistrarEntrada frmRegistrar = new RegistrarEntrada(this, btnE23.Content.ToString());
           frmRegistrar.Show();
       }

       private void btnE23_MouseEnter(object sender, MouseEventArgs e)
       {
           btnE23.ToolTip = toolTipp("E23");
       }

       private void btnE28_Click(object sender, RoutedEventArgs e)
       {
           RegistrarEntrada frmRegistrar = new RegistrarEntrada(this, btnE28.Content.ToString());
           frmRegistrar.Show();
       }

       private void btnE28_MouseEnter(object sender, MouseEventArgs e)
       {
           btnE28.ToolTip = toolTipp("E28");
       }

       private void btnE24_Click(object sender, RoutedEventArgs e)
       {
           RegistrarEntrada frmRegistrar = new RegistrarEntrada(this, btnE24.Content.ToString());
           frmRegistrar.Show();
       }

       private void btnE24_MouseEnter(object sender, MouseEventArgs e)
       {
           btnE24.ToolTip = toolTipp("E24");
       }

       private void btnE29_Click(object sender, RoutedEventArgs e)
       {
           RegistrarEntrada frmRegistrar = new RegistrarEntrada(this, btnE29.Content.ToString());
           frmRegistrar.Show();
       }

       private void btnE29_MouseEnter(object sender, MouseEventArgs e)
       {
           btnE29.ToolTip = toolTipp("E29");
       }

       private void btnE25_Click(object sender, RoutedEventArgs e)
       {
           RegistrarEntrada frmRegistrar = new RegistrarEntrada(this, btnE25.Content.ToString());
           frmRegistrar.Show();
       }

       private void btnE25_MouseEnter(object sender, MouseEventArgs e)
       {
           btnE29.ToolTip = toolTipp("E25");
       }

       private void btnE30_Click(object sender, RoutedEventArgs e)
       {
           RegistrarEntrada frmRegistrar = new RegistrarEntrada(this, btnE30.Content.ToString());
           frmRegistrar.Show();
       }

       private void btnE30_MouseEnter(object sender, MouseEventArgs e)
       {
           btnE30.ToolTip = toolTipp("E30");
       }

       private void btnVerSector_Click(object sender, RoutedEventArgs e)
       {
           FormSectores form = new FormSectores();
           form.Show();
           this.Close();
       }


       
    }
}
