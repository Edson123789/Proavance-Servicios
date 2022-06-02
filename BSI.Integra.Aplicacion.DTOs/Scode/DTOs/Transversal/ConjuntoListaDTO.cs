using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    [AttributeUsage(AttributeTargets.Property,
              Inherited = false,
              AllowMultiple = false)]
    internal sealed class OptionalAttribute : Attribute
    {
    }

    public class ConjuntoListaDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public string NombreCategoriaObjetoFiltro { get; set; }
        public int IdCategoriaObjetoFiltro { get; set; }
        public int IdFiltroSegmento { get; set; }
        public int IdFiltroSegmentoTipoContacto { get; set; }
        public byte NroListasRepeticionContacto { get; set; }
        public bool? ConsiderarYaEnviados { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public DateTime? FechaModificacion { get; set; }
    }

    public class ConjuntoListaDetalleDTO
    {
        public int IdConjuntoLista { get; set; }
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public byte Prioridad { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public DateTime? FechaModificacion { get; set; }

        public List<FiltroSegmentoValorTipoDTO> ListaArea { get; set; }
        public List<FiltroSegmentoValorTipoDTO> ListaSubArea { get; set; }
        public List<FiltroSegmentoValorTipoDTO> ListaProgramaGeneral { get; set; }

        public ConjuntoListaDetalleDTO() {
            ListaArea = new List<FiltroSegmentoValorTipoDTO>();
            ListaSubArea = new List<FiltroSegmentoValorTipoDTO>();
            ListaProgramaGeneral = new List<FiltroSegmentoValorTipoDTO>();
        }
    }

    public class ConjuntoListaDetalleCompletoDTO{
        [Optional]
        public ConjuntoListaDTO ConjuntoLista { get; set; }
        public List<ConjuntoListaDetalleDTO> ConjuntoListaDetalle { get; set; }
        public string NombreUsuario { get; set; }
    }

    public class FiltroConjuntoLista {
       public List<FiltroDTO> ListaCategoriaObjetoFiltro { get; set; }
       public List<FiltroDTO> ListaArea { get; set; }
       public List<SubAreaCapacitacionFiltroDTO> ListaSubArea { get; set; }
       public List<FiltroPGeneralDTO> ListaProgramaGeneral { get; set; }
       public List<FiltroDTO> ListaFiltroSegmento { get; set; }
    }
}
