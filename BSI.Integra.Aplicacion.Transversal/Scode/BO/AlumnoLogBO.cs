using BSI.Integra.Aplicacion.Base.BO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.Transversal.BO
{
    public class AlumnoLogBO : BaseBO
    {
        public int IdAlumno { get; set; }
        public string CampoActualizado { get; set; }
        public string ValorAnterior { get; set; }
        public string ValorNuevo { get; set; }
        public int? IdMigracion { get; set; }

        public AlumnoLogBO()
        {

        }

        public AlumnoLogBO(int IdAlumno, string CampoActualizado, string ValorAnterior, string ValorNuevo, string Usuario)
        {
            this.IdAlumno = IdAlumno;
            this.CampoActualizado = CampoActualizado;
            this.ValorAnterior = ValorAnterior;
            this.ValorNuevo = ValorNuevo;
            this.Estado= true;
            this.UsuarioCreacion= Usuario;
            this.UsuarioModificacion= Usuario;
            this.FechaCreacion= DateTime.Now;
            this.FechaModificacion= DateTime.Now;
        }
    }

}
