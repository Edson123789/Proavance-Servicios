using BSI.Integra.Aplicacion.Base.BO;
using BSI.Integra.Aplicacion.DTOs;
using System;
using System.Collections.Generic;
using System.Text;
using BSI.Integra.Aplicacion.Transversal.Helper;
using BSI.Integra.Aplicacion.Finanzas.Repositorio;
using BSI.Integra.Persistencia.Models;

namespace BSI.Integra.Aplicacion.Finanzas.BO
{
    public class BeneficiosAlumnoPEspecificoBO : BaseBO
    {
        public int IdAlumno { get; set; }
        public int IdPgeneral { get; set; }
        public int IdPespecifico { get; set; }
        public int IdMatriculaCabecera { get; set; }
        public string Beneficios { get; set; }
        public int? IdMigracion { get; set; }

    }

}
