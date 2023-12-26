using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClaseBase
{
    public class Ticket
    {
        private int t_Id;

        public int T_Id
        {
            get { return t_Id; }
            set { t_Id = value; }
        }
        private DateTime t_FechaHoraEnt;

        public DateTime T_FechaHoraEnt
        {
            get { return t_FechaHoraEnt; }
            set { t_FechaHoraEnt = value; }
        }
        private DateTime t_FechaHoraSal;

        public DateTime T_FechaHoraSal
        {
            get { return t_FechaHoraSal; }
            set { t_FechaHoraSal = value; }
        }
        private string t_Patente;

        public string T_Patente
        {
            get { return t_Patente; }
            set { t_Patente = value; }
        }
        private double t_Duracion;

        public double T_Duracion
        {
            get { return t_Duracion; }
            set { t_Duracion = value; }
        }
        private decimal t_Tarifa;

        public decimal T_Tarifa
        {
            get { return t_Tarifa; }
            set { t_Tarifa = value; }
        }
        private decimal t_Total;

        public decimal T_Total
        {
            get { return t_Total; }
            set { t_Total = value; }
        }
        private Sector sec_Id;

        public Sector Sec_Id
        {
            get { return sec_Id; }
            set { sec_Id = value; }
        }
        private Cliente cli_Id;

        public Cliente Cli_Id
        {
            get { return cli_Id; }
            set { cli_Id = value; }
        }
        private TipoVehiculo tv_Id;

        public TipoVehiculo Tv_Id
        {
            get { return tv_Id; }
            set { tv_Id = value; }
        }

        public Ticket()
        {
            
        }
        public override string ToString()
        {
            return T_Id +""+T_Duracion+ Sec_Id + Cli_Id;
        }
    }
}
