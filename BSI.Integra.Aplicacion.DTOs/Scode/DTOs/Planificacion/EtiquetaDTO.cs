using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class EtiquetaDTO
    {
        public int Id { get; set; }
        public int? IdTipoEtiqueta { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public string CampoDb { get; set; }
        public bool NodoPadre { get; set; }
        public int? IdNodoPadre { get; set; }
        public string Usuario { get; set; }
        public EtiquetaBotonReemplazoDTO EtiquetaBotonReemplazo { get; set; }
    }

    public class EtiquetaBotonReemplazoDTO
    {
        public string Texto { get; set; }
        public bool AbrirEnNuevoTab { get; set; }
        public string Estilos { get; set; }
        public string Url { get; set; }
    }
    public class EtiquetaDBDTO
    {
        public int Id { get; set; }
        public int? IdTipoEtiqueta { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public string CampoDb { get; set; }
        public bool NodoPadre { get; set; }
        public int? IdNodoPadre { get; set; }
        public string Usuario { get; set; }
        public string EtiquetaBotonReemplazoTexto { get; set; }
        public bool? EtiquetaBotonReemplazoAbrirEnNuevoTab { get; set; }
        public string EtiquetaBotonReemplazoEstilos { get; set; }
        public string EtiquetaBotonReemplazoUrl { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
    }

    public class EtiquetaParametroProbarPlantillaDTO
    {
        public int? IdOportunidad { get; set; }
        public int? IdAlumno { get; set; }
        public int? IdPGeneral { get; set; }
        public int IdPlantilla { get; set; }
        public int TipoPlantilla { get; set; }
    }

    public class EtiquetaParametroAlumnoSinOportunidadDTO
    {
        public int IdAlumno { get; set; }
        public int? IdPGeneral { get; set; }
        public int? IdAsesor { get; set; }
    }

    public class EnvioMasivoSMSParametrosDTO
    {
        public List<int> listaIdOportunidad { get; set; }
        public int IdPlantilla { get; set; }
    }
}
