using System;
using System.Collections.Generic;
using System.Text;
using BSI.Integra.Aplicacion.Classes;
using BSI.Integra.Aplicacion.Base.BO;

namespace BSI.Integra.Aplicacion.Transversal.BO
{
    public class TipoDescuentoBO : BaseBO
    {
        public string Codigo { get; set; }
        public string Descripcion { get; set; }
        public int Formula { get; set; }
        public int? PorcentajeGeneral { get; set; }
        public int? PorcentajeMatricula { get; set; }
        public int? FraccionesMatricula { get; set; }
        public int? PorcentajeCuotas { get; set; }
        public int? CuotasAdicionales { get; set; }
        //para los tipos descuento
        public string Tipo { get; set; }

        //Para TipoDescuentoAsesorCoordinadorPw
        public List <TipoDescuentoAsesorCoordinadorPwBO> TipoDescuentoAsesorCoordinadorPw { get; set; }

    }
}
