using BSI.Integra.Aplicacion.Base.BO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.Marketing.BO
{
	public class FormularioRespuestaPlantillaBO : BaseBO
	{
		public string NombrePlantilla { get; set; }
		public string ColorTextoPgeneral { get; set; }
		public string ColorTextoDescripcionPgeneral { get; set; }
		public string ColorTextoInvitacionBrochure { get; set; }
		public string TextoBotonBrochure { get; set; }
		public string ColorFondoBotonBrochure { get; set; }
		public string ColorTextoBotonBrochure { get; set; }
		public string ColorBordeInferiorBotonBrochure { get; set; }
		public string ColorTextoBotonInvitacion { get; set; }
		public string ColorFondoBotonInvitacion { get; set; }
		public string FondoBotonLadoInvitacion { get; set; }
		public string UrlimagenLadoInvitacion { get; set; }
		public string TextoBotonInvitacionPagina { get; set; }
		public string TextoBotonInvitacionArea { get; set; }
		public string ContenidoSeccionTelefonos { get; set; }
		public string TextoInvitacionBrochure { get; set; }
        public Guid? IdMigracion { get; set; }

        public FormularioRespuestaPlantillaBO()
        {
            ActualesErrores = new Dictionary<string, List<Base.Classes.ErrorInfo>>();
        }
    }
}
