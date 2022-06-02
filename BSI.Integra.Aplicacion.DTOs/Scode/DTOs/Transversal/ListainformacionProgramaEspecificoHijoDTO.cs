﻿using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class ListainformacionProgramaEspecificoHijoDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Duracion { get; set; }
        public int? IdCiudad { get; set; }
        public string TipoAmbiente { get; set; }
        public int? IdAmbiente { get; set; }
        public int? IdExpositor_Referencia { get; set; }
        public int? IdProgramaGeneral { get; set; }
		public DateTime? FechaHoraInicio { get; set; }
		public int IdCentroCosto { get; set; }
		public int? IdProveedor { get; set; }
        public int? IdEstadoPEspecifico { get; set; }
        public int? IdModalidadCurso { get; set; }
        public int? IdCursoMoodle { get; set; }
        public int? IdCursoMoodlePrueba { get; set; }
        public string Codigo { get; set; }

        public List<FiltroDTO> ListaGrupo { get; set; }
        public List<FiltroDTO> ListaGrupoEdicion { get; set; }
        public ListainformacionProgramaEspecificoHijoDTO() {
            ListaGrupo = new List<FiltroDTO>();
            ListaGrupoEdicion = new List<FiltroDTO>();
        }
    }
}
