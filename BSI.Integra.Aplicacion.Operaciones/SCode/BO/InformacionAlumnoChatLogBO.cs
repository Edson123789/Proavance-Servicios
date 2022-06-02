using BSI.Integra.Aplicacion.Base.BO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.Operaciones.BO
{
    public class InformacionAlumnoChatLogBO : BaseBO
    {
        public int IdInformacionAlumnoChatLog { get; set; }
        public int? IdMatriculaCabecera { get; set; }
        public int? IdAlumno { get; set; }
        public int? IdProgramaGeneralPadre { get; set; }
        public int? IdProgramaGeneralHijo { get; set; }
        public int? IdCentroCosto { get; set; }
        public int? IdCapitulo { get; set; }
        public int? IdSesion { get; set; }
        public string IdInteraccionSesion { get; set; }
        public int? IdCoordinadora { get; set; }
        public string CodigoMatricula { get; set; }
        public int? IdMigracion { get; set; }
    }
}
