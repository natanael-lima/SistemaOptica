using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;
using System.Windows.Media;
using System.Globalization;

namespace LibreriaRecursos
{
    public class ConversorDeEstados : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture){
            
            string rojoMuyClaro = "#FFFF6F6F";
            string rojoClaro = "#FFFE0000";

            BrushConverter conv = new BrushConverter();
            Brush br;
            if (value != null)
            {
                //Realizo el parseo del valor seleccionado a uno de tipo int
                int duracion = int.Parse(value.ToString());
                if (duracion == 200){
                    return Brushes.Gray;
                }
                else if(duracion == 0){
                    return Brushes.Green;
                }
                else if (duracion > 0 && duracion <= 30){
                    br = (Brush)conv.ConvertFromString(rojoMuyClaro);
                    return br; // Sector ocupado (Rojo muy claro)
                }
                else if(duracion > 30 && duracion <=60){
                    br = (Brush) conv.ConvertFromString(rojoClaro);
                    return br; // Sector ocupado (Rojo claro)
                }
                else if (duracion > 60 ){
                    return Brushes.DarkRed; // Sector ocupado (Rojo oscuro)
                }
               
            }

            return Brushes.Transparent;
            
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
       
    }
}
