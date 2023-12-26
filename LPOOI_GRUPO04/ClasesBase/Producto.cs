using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClasesBase
{
    public class Producto
    {
        private string prod_Codigo;

        public string Prod_Codigo
        {
            get { return prod_Codigo; }
            set { prod_Codigo = value; }
        }
        private string prod_Categoria;

        public string Prod_Categoria
        {
            get { return prod_Categoria; }
            set { prod_Categoria = value; }
        }
        private string prod_Descripcion;

        public string Prod_Descripcion
        {
            get { return prod_Descripcion; }
            set { prod_Descripcion = value; }
        }
        private decimal prod_Precio;

        public decimal Prod_Precio
        {
            get { return prod_Precio; }
            set { prod_Precio = value; }
        }
        public Producto()
        {

        }

        private int prod_Id;

        public int Prod_Id
        {
            get { return prod_Id; }
            set { prod_Id = value; }
        }

        private int prod_Activo;

        public int Prod_Activo
        {
            get { return prod_Activo; }
            set { prod_Activo = value; }
        }
    }
}
