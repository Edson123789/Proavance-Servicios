using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.Transversal.DTO
{
    public class ProyectoAplicacionEnvioSinCalificarResumenCabeceraCorreoDTO
    {
        public int IdPgeneralProyectoAplicacionEnvio { get; set; }
        public int IdMatriculaCabecera { get; set; }
        public string CodigoMatricula { get; set; }
        public string Alumno { get; set; }
        public string PEspecifico { get; set; }
        public int IdProveedor { get; set; }
        public string EmailProveedor { get; set; }
        public int? IdPersonalResponsableCoordinacion { get; set; }
        public string EmailResponsableCoordinacion { get; set; }
    }
}
