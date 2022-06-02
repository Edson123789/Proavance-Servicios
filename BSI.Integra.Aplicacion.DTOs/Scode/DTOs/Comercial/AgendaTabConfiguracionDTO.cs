using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class AgendaTabConfiguracionDTO
    {
        public int Id { get; set; }
        public int IdAgendaTab { get; set; }
        public string NombreAgendaTab { get; set; }
        public string ListaTipoCategoriaOrigen { get; set; }
        public string ListaCategoriaOrigen { get; set; }
        public string ListaTipoDato { get; set; }
        public string ListaFaseOportunidad { get; set; }
        public string ListaEstadoOportunidad { get; set; }
        public string ListaProbabilidad { get; set; }

        public string NombreUsuario { get; set; }
    }
}
