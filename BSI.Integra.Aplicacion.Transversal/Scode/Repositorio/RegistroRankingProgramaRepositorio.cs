using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.Transversal.Repositorio
{
    public class RegistroRankingProgramaRepositorio : BaseRepository<TConfigurarVideoPrograma, ConfigurarVideoProgramaBO>
    {
        #region Metodos Base
        public RegistroRankingProgramaRepositorio() : base()
        {
        }
        public RegistroRankingProgramaRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        #endregion
        public List<listaRegistroRankingProgramaDTO> listaRegistroRankingPrograma(int TipoPrograma)
        {
            List<listaRegistroRankingProgramaDTO> rpta = new List<listaRegistroRankingProgramaDTO>();
            string _query = "Select Id, Porcentaje,TipoPrograma From pla.T_RegistroRankingPrograma Where Estado=1 AND TipoPrograma=@TipoPrograma";
            string query = _dapper.QueryDapper(_query, new { TipoPrograma = TipoPrograma });
            if (!string.IsNullOrEmpty(query) && !query.Contains("null"))
            {
                rpta = JsonConvert.DeserializeObject<List<listaRegistroRankingProgramaDTO>>(query);
                return rpta;
            }
            else
            {
                return null;
            }
        }

        public bool reiniciarTablaRankingAlumno()
        {
            List<listaRegistroRankingProgramaDTO> rpta = new List<listaRegistroRankingProgramaDTO>();
            string _query = "truncate table pla.T_AlumnoTopRegistroRankingPrograma";
            string query = _dapper.QueryDapper(_query, null);
            if (!string.IsNullOrEmpty(query) && !query.Contains("null"))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool registrarTablaRankingAlumno(registroRankingMatriculaNotaProgramaGeneralBO modelo)
        {
            var FechaCreacion = DateTime.Now;
            var FechaModificacion = DateTime.Now;
            var UsuarioCreacion = "system";
            var UsuarioModificacion = "system";
            var Estado = true;

            List<listaRegistroRankingProgramaDTO> rpta = new List<listaRegistroRankingProgramaDTO>();
            string _query = @"INSERT INTO [pla].[T_AlumnoTopRegistroRankingPrograma] ([IdMatriculaCabecera],[CodigoMatricula],[IdAlumno],[IdPEspecifico],[IdPGeneral],[Tipo],[TipoId],[EscalaCalificacion],[NotaFinal],[Promedio],[Orden],[TopPorcentaje],[Estado],[UsuarioCreacion],[UsuarioModificacion],[FechaCreacion],[FechaModificacion]) 
                              VALUES (@IdMatriculaCabecera,@CodigoMatricula,@IdAlumno,@IdPEspecifico,@IdPGeneral,@Tipo,@TipoId,@EscalaCalificacion,@NotaFinal,@Promedio,@Orden,@TopPorcentaje,@Estado,@UsuarioCreacion,@UsuarioModificacion,@FechaCreacion,@FechaModificacion)";
            string query = _dapper.QueryDapper(_query, new { modelo.IdMatriculaCabecera, modelo.CodigoMatricula, modelo.IdAlumno, modelo.IdPEspecifico, modelo.IdPGeneral, modelo.Tipo, modelo.TipoId, modelo.EscalaCalificacion, modelo.NotaFinal, modelo.Promedio, modelo.Orden, modelo.TopPorcentaje, Estado, UsuarioCreacion, UsuarioModificacion, FechaCreacion, FechaModificacion });
            if (!string.IsNullOrEmpty(query) && !query.Contains("null"))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

    }
}
