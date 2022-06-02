using System;
using System.Collections.Generic;
using System.Text;
using BSI.Integra.Aplicacion.Base.BO;
using BSI.Integra.Aplicacion.DTOs;

namespace BSI.Integra.Aplicacion.Transversal.BO
{
    public class PreguntaFrecuenteBO : BaseBO
    {
        public int IdSeccionPreguntaFrecuente { get; set; }
        public string Pregunta { get; set; }
        public string Respuesta { get; set; }
        public int Tipo { get; set; }
        public Guid? IdMigracion { get; set; }
        public List<PreguntaFrecuentePgeneralBO> PreguntaFrecuentePgeneral { get; set; }
        public List<PreguntaFrecuenteAreaBO> PreguntaFrecuenteArea { get; set; }
        public List<PreguntaFrecuenteSubAreaBO> PreguntaFrecuenteSubArea { get; set; }
        public List<PreguntaFrecuenteTipoBO> PreguntaFrecuenteTipo { get; set; }


    }
}
