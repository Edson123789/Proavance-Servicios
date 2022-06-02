using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs.Transversal
{
    public class CertificadoModularDTO
    {
        public int IdMatriculaCabacera { get; set; }
        public List<CertificadoModularCursosMoodleDTO> Cursos { get; set; }
    }
    public class CertificadoModularCursosMoodleDTO
    {
        public int IdCursoMoodle { get; set; }
        public int IdPeneral { get; set; }
    }
}
