using System;
using System.Collections.Generic;
using BSI.Integra.Aplicacion.Base.BO;
using BSI.Integra.Aplicacion.GestionPersonas.Repositorio;
using BSI.Integra.Persistencia.Models;

namespace BSI.Integra.Aplicacion.GestionPersonas.BO
{
    ///BO: PersonalHorarioBO
    ///Autor: Jose Villena.
    ///Fecha: 28/04/2021
    ///<summary>
    ///Columnas y funciones de la tabla T_PersonalHorario
    ///</summary>
    public class PersonalHorarioBO :BaseBO
    {
        ///Propiedades		            Significado
        ///-------------	            -----------------------
        ///IdPersonal                   Fk de T_Personal
        ///Lunes(1,2,3,4)		        Dia Semana
        ///Martes(1,2,3,4)              Dia Semana
        ///Miercoles(1,2,3,4)           Dia Semana
        ///Jueves(1,2,3,4)              Dia Semana
        ///Viernes(1,2,3,4)             Dia Semana
        ///Sabado(1,2,3,4)              Dia Semana
        ///Domingo(1,2,3,4)             Dia Semana
        ///IdMigracion                  Id de migracion
        ///Activo                       Personal Activo
        ///FechaInicio                  Fecha de Inicio
        ///FechaFin                     Fecha de Fin
        public int IdPersonal { get; set; }
        public TimeSpan? Lunes1 { get; set; }
        public TimeSpan? Lunes2 { get; set; }
        public TimeSpan? Lunes3 { get; set; }
        public TimeSpan? Lunes4 { get; set; }
        public TimeSpan? Martes1 { get; set; }
        public TimeSpan? Martes2 { get; set; }
        public TimeSpan? Martes3 { get; set; }
        public TimeSpan? Martes4 { get; set; }
        public TimeSpan? Miercoles1 { get; set; }
        public TimeSpan? Miercoles2 { get; set; }
        public TimeSpan? Miercoles3 { get; set; }
        public TimeSpan? Miercoles4 { get; set; }
        public TimeSpan? Jueves1 { get; set; }
        public TimeSpan? Jueves2 { get; set; }
        public TimeSpan? Jueves3 { get; set; }
        public TimeSpan? Jueves4 { get; set; }
        public TimeSpan? Viernes1 { get; set; }
        public TimeSpan? Viernes2 { get; set; }
        public TimeSpan? Viernes3 { get; set; }
        public TimeSpan? Viernes4 { get; set; }
        public TimeSpan? Sabado1 { get; set; }
        public TimeSpan? Sabado2 { get; set; }
        public TimeSpan? Sabado3 { get; set; }
        public TimeSpan? Sabado4 { get; set; }
        public TimeSpan? Domingo1 { get; set; }
        public TimeSpan? Domingo2 { get; set; }
        public TimeSpan? Domingo3 { get; set; }
        public TimeSpan? Domingo4 { get; set; }
        public int? IdMigracion { get; set; }
        public bool Activo { get; set; }
        public DateTime? FechaInicio { get; set; }
        public DateTime? FechaFin { get; set; }


        private PersonalHorarioRepositorio _repoPersonalHorario;
        public PersonalHorarioBO()
        {
        }
        public PersonalHorarioBO(integraDBContext integraDBContext)
        {
            _repoPersonalHorario = new PersonalHorarioRepositorio(integraDBContext);
        }

        /// <summary>
        /// Obtiene el Horario del Asesor como Tabla
        /// </summary>
        /// <param name="idPersonal"></param>
        /// <returns></returns>
        public List<List<TimeSpan?>> GetHorarioAsTable()
            {

            var horario = _repoPersonalHorario.ObtenerHorarioAsesor(this.IdPersonal);
            if (horario == null)
                throw new Exception("No existe Horario para el PersonalId " + this.IdPersonal);

            List<List<TimeSpan?>> tablaHorario = new List<List<TimeSpan?>>();
            tablaHorario.Add(new List<TimeSpan?> { horario.Domingo1, horario.Domingo2, horario.Domingo3, horario.Domingo4 });
            tablaHorario.Add(new List<TimeSpan?> { horario.Lunes1, horario.Lunes2, horario.Lunes3, horario.Lunes4 });
            tablaHorario.Add(new List<TimeSpan?> { horario.Martes1, horario.Martes2, horario.Martes3, horario.Martes4 });
            tablaHorario.Add(new List<TimeSpan?> { horario.Miercoles1, horario.Miercoles2, horario.Miercoles3, horario.Miercoles4 });
            tablaHorario.Add(new List<TimeSpan?> { horario.Jueves1, horario.Jueves2, horario.Jueves3, horario.Jueves4 });
            tablaHorario.Add(new List<TimeSpan?> { horario.Viernes1, horario.Viernes2, horario.Viernes3, horario.Viernes4 });
            tablaHorario.Add(new List<TimeSpan?> { horario.Sabado1, horario.Sabado2, horario.Sabado3, horario.Sabado4 });

            return tablaHorario;
        }
    }
    
}
