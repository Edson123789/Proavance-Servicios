using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class PrioridadMailChimpListaDTO
    {

    }

    public class PrioridadMailChimpListaInsercionDTO
    {
        public int Id { get; set; }
        public int IdCampaniaMailing { get; set; }
        public int? IdCampaniaMailingDetalle { get; set; }
        public int? IdCampaniaGeneralDetalle { get; set; }
        public string Asunto { get; set; }
        public string Contenido { get; set; }
        public string AsuntoLista { get; set; }
        public int IdPersonal { get; set; }
        public string NombreAsesor { get; set; }
        public string Alias { get; set; }
        public string Etiquetas { get; set; }
        public bool Enviado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
    }

    public class PrioridadMailingGeneralPreObtencionDTO
    {
        public int IdPrioridadMailChimpListaCorreo { get; set; }
        public int IdAlumno { get; set; }
        public string Celular { get; set; }
        public int IdCodigoPais { get; set; }
    }
}
