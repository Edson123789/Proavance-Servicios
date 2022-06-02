using BSI.Integra.Aplicacion.Base.BO;
using System;
using System.Collections.Generic;

namespace BSI.Integra.Aplicacion.Transversal.BO
{
    public class ConjuntoListaDetalleBO : BaseBO
    {
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public int IdConjuntoLista { get; set; }
        public byte Prioridad { get; set; }
        public int? IdMigracion { get; set; }

        public List<ConjuntoListaDetalleValorBO> ListaConjuntoListaDetalleValor { get; set; }

        public ConjuntoListaDetalleBO() {
            ListaConjuntoListaDetalleValor = new List<ConjuntoListaDetalleValorBO>();
        }
    }
}
