﻿using BSI.Integra.Aplicacion.Base.BO;
using System;
using System.Collections.Generic;
using BSI.Integra.Aplicacion.Transversal.Repositorio;
using BSI.Integra.Aplicacion.Base.Classes;

namespace BSI.Integra.Aplicacion.Transversal.BO
{
    public class OcurrenciaBO : BaseBO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string NombreM { get; set; }
        public int IdFaseOportunidad { get; set; }
        public int? IdActividadCabecera { get; set; }
        public int? IdPlantillaSpeech { get; set; }
        public int IdEstadoOcurrencia { get; set; }
        public bool Oportunidad { get; set; }
        public string RequiereLlamada { get; set; }
        public string Roles { get; set; }
        public string Color { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public Guid? IdMigracion { get; set; }
        public int? NombreCs { get; set; }
		public int IdPersonalAreaTrabajo { get; set; }


		private OcurrenciaRepositorio _repOcurrencia;

        public OcurrenciaBO()
        {
            ActualesErrores = new Dictionary<string, List<ErrorInfo>>();
        }

        public OcurrenciaBO(int id)
        {
            _repOcurrencia = new OcurrenciaRepositorio();

            ActualesErrores = new Dictionary<string, List<ErrorInfo>>();
            var Ocurrencia = _repOcurrencia.FirstById(id);

            this.Id = id;
            this.Nombre = Ocurrencia.Nombre;
            this.IdFaseOportunidad = Ocurrencia.IdFaseOportunidad;
            this.IdActividadCabecera = Ocurrencia.IdActividadCabecera;
            this.IdPlantillaSpeech = Ocurrencia.IdPlantillaSpeech;
            this.IdEstadoOcurrencia = Ocurrencia.IdEstadoOcurrencia;
            this.Oportunidad = Ocurrencia.Oportunidad;
            this.RequiereLlamada = Ocurrencia.RequiereLlamada;
            this.Roles = Ocurrencia.Roles;
            this.Color = Ocurrencia.Color;
            this.Estado = Ocurrencia.Estado;
            this.UsuarioCreacion = Ocurrencia.UsuarioCreacion;
            this.UsuarioModificacion = Ocurrencia.UsuarioModificacion;
            this.FechaCreacion = Ocurrencia.FechaCreacion;
            this.FechaModificacion = Ocurrencia.FechaModificacion;
            this.RowVersion = Ocurrencia.RowVersion;
			this.IdPersonalAreaTrabajo = Ocurrencia.IdPersonalAreaTrabajo;

		}   
    }
}
