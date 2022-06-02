using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class DatosConfigurarVideoProgramaDTO
    {
        public ConfigurarVideoProgramaDTO ConfigurarVideoPrograma { get; set; }
        public List<SesionConfigurarVideoDTO> SesionesConfiguradasPrograma { get; set; }
        public List<SesionConfigurarVideoEliminarDTO> SesionesConfiguradasEliminadas { get; set; }
        public string Usuario { get; set; }

    }

    public class ConfigurarVideoProgramaDTO
    {
        public int Id { get; set; }
        public int IdPGeneral { get; set; }
        public int IdDocumentoSeccionPw { get; set; }
        public string VideoId { get; set; }
        public string VideoIdBrightcove { get; set; }
        public string TotalMinutos { get; set; }
        public string Archivo { get; set; }
        public string NroDiapositivas { get; set; }
        public bool Configurado { get; set; }

        public bool ConImagenVideo { get; set; }
        public string ImagenVideoNombre { get; set; }
        public string ImagenVideoAncho { get; set; }
        public string ImagenVideoAlto { get; set; }
        public string ImagenVideoPosicionX { get; set; }
        public string ImagenVideoPosicionY { get; set; }


        public bool ConImagenDiapositiva { get; set; }
        public string ImagenDiapositivaNombre { get; set; }
        public string ImagenDiapositivaAncho { get; set; }
        public string ImagenDiapositivaAlto { get; set; }
        public string ImagenDiapositivaPosicionX { get; set; }
        public string ImagenDiapositivaPosicionY { get; set; }

        public int NumeroFila { get; set; }
    }

    public class SesionConfigurarVideoDTO
    {
        public int Id { get; set; }
        public int IdConfigurarVideoPrograma { get; set; }
        public int Minuto { get; set; }
        public int IdTipoVista { get; set; }
        public int NroDiapositiva { get; set; }
        public int IdEvaluacion { get; set; }
        public bool ConLogoVideo { get; set; }
        public bool ConLogoDiapositiva { get; set; }
    }

    public class SesionConfigurarVideoEliminarDTO
    {
        public int Id { get; set; }
        public int IdConfigurarVideoPrograma { get; set; }
        public int Minuto { get; set; }
        public int IdTipoVista { get; set; }
        public int NroDiapositiva { get; set; }
        public int IdEvaluacion { get; set; }
        public bool Estado { get; set; }
        public bool ConLogoVideo { get; set; }
        public bool ConLogoDiapositiva { get; set; }
    }

    public class ConfigurarVideoProgramaBasicoDTO
    {
        public int Id { get; set; }
        public int IdPGeneral { get; set; }
        public int NumeroFila { get; set; }
        public string NroDiapositivas { get; set; }
        public string TotalMinutos { get; set; }
    }
}
