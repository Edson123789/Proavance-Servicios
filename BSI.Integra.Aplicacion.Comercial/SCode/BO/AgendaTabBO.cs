using BSI.Integra.Aplicacion.Base.BO;
using System;

namespace BSI.Integra.Aplicacion.Comercial.BO
{
    public class AgendaTabBO : BaseBO
    {
        public string Nombre { get; set; }
        public string CodigoAreaTrabajo { get; set; }
        public bool VisualizarActividad { get; set; }
        public bool CargarInformacionInicial { get; set; }
        public int Numeracion { get; set; }
        public bool ValidarFecha { get; set; }
       
        public byte[] RowVersion { get; set; }
        public int? IdMigracion { get; set; }

        public AgendaTabBO()
        {
        }
    }
}
