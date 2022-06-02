using System;
using System.Collections.Generic;
using System.Text;
using BSI.Integra.Aplicacion.Base.BO;
using BSI.Integra.Aplicacion.Base.Classes;
using BSI.Integra.Aplicacion.Classes;

namespace BSI.Integra.Aplicacion.Transversal.BO
{
    public class CalidadProcesamientoBO : BaseBO
    {
        private int idOportunidad;
        public int IdOportunidad {
            get { return idOportunidad; }
            set
            {
                if (value == 0)
                {
                    ValidateRequiredStringProperty(this.GetType().Name, ErrorInfo.Codigos.Obligatorio, this.GetType().GetProperty("IdOportunidad").Name,
                                                   "Id Oportunidad que Pertenece Competidor");
                    return;
                }
                idOportunidad = value;


                ClearErrorFromProperty(this.GetType().GetProperty("IdOportunidad").Name, ErrorInfo.Codigos.Obligatorio);

            }
        }
        public int PerfilCamposLlenos { get; set; }
        public int PerfilCamposTotal { get; set; }
        public bool Dni { get; set; }
        public int PgeneralValidados { get; set; }
        public int PgeneralTotal { get; set; }
        public int PespecificoValidados { get; set; }
        public int PespecificoTotal { get; set; }
        public int BeneficiosValidados { get; set; }
        public int BeneficiosTotales { get; set; }
        public bool CompetidoresVerificacion { get; set; }
        public int ProblemaSeleccionados { get; set; }
        public int ProblemaSolucionados { get; set; }
		public Guid? IdMigracion { get; set; }

		public CalidadProcesamientoBO()
        {
            ActualesErrores = new Dictionary<string, List<ErrorInfo>>();
        }
    }
}
