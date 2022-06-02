using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using AutoMapper;
using BSI.Integra.Aplicacion.Comercial.BO;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using Newtonsoft.Json;

namespace BSI.Integra.Aplicacion.Comercial.Repositorio
{
    /// Repositorio: Mandril
    /// Autor: Joao Benavente - Ansoli Espinoza - Fischer Valdez - Gian Miranda
    /// Fecha: 08/02/2021
    /// <summary>
    /// Gestión de mandril
    /// </summary>
    public class MandrilRepositorio : BaseRepository<TMandril, MandrilBO>
    {
        #region Metodos Base
        public MandrilRepositorio() : base()
        {
        }
        public MandrilRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<MandrilBO> GetBy(Expression<Func<TMandril, bool>> filter)
        {
            IEnumerable<TMandril> listado = base.GetBy(filter);
            List<MandrilBO> listadoBO = new List<MandrilBO>();
            foreach (var itemEntidad in listado)
            {
                MandrilBO objetoBO = Mapper.Map<TMandril, MandrilBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public MandrilBO FirstById(int id)
        {
            try
            {
                TMandril entidad = base.FirstById(id);
                MandrilBO objetoBO = new MandrilBO();
                Mapper.Map<TMandril, MandrilBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public MandrilBO FirstBy(Expression<Func<TMandril, bool>> filter)
        {
            try
            {
                TMandril entidad = base.FirstBy(filter);
                MandrilBO objetoBO = Mapper.Map<TMandril, MandrilBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(MandrilBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TMandril entidad = MapeoEntidad(objetoBO);

                bool resultado = base.Insert(entidad);
                if (resultado)
                    AsignacionId(entidad, objetoBO);

                return resultado;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(IEnumerable<MandrilBO> listadoBO)
        {
            try
            {
                foreach (var objetoBO in listadoBO)
                {
                    bool resultado = Insert(objetoBO);
                    if (resultado == false)
                        return false;
                }

                return true;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public bool Update(MandrilBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TMandril entidad = MapeoEntidad(objetoBO);

                bool resultado = base.Update(entidad);
                if (resultado)
                    AsignacionId(entidad, objetoBO);

                return resultado;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public bool Update(IEnumerable<MandrilBO> listadoBO)
        {
            try
            {
                foreach (var objetoBO in listadoBO)
                {
                    bool resultado = Update(objetoBO);
                    if (resultado == false)
                        return false;
                }

                return true;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        private void AsignacionId(TMandril entidad, MandrilBO objetoBO)
        {
            try
            {
                if (entidad != null && objetoBO != null)
                {
                    objetoBO.Id = entidad.Id;
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        private TMandril MapeoEntidad(MandrilBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TMandril entidad = new TMandril();
                entidad = Mapper.Map<MandrilBO, TMandril>(objetoBO,
                    opt => opt.ConfigureMap(MemberList.None));

                //mapea los hijos

                return entidad;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        #endregion

        /// <summary>
        /// Obtiene los correos enviados por un Asesor a un Alumno en especifico
        /// </summary>
        /// <param name="idAlumno"></param>
        /// <param name="idAsesor"></param>
        /// <returns></returns>
        public List<CorreoInteraccionesAlumnoDTO> ListaInteraccionCorreoAlumnoPorOportunidad(int idAlumno, int idAsesor)
        {
            try
            {
                string _query = "SELECT Id, FechaCreacion, Categoria, Asunto, Estado, CorreoReceptor, CorreoRemitente FROM  com.V_Correo_Interacciones WHERE IdAlumno = @IdAlumno AND IdAsesor = @IdAsesor";
                var _queryRespuesta = _dapper.QueryDapper(_query, new { IdAlumno = idAlumno, IdAsesor = idAsesor });
                List<CorreoInteraccionesAlumnoDTO> listaInteracciones = JsonConvert.DeserializeObject<List<CorreoInteraccionesAlumnoDTO>>(_queryRespuesta);
                return listaInteracciones;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            
        }

        /// <summary>
        /// Obtiene los correos enviados por un Asesor a un Alumno en especifico
        /// </summary>
        /// <param name="idAlumno"></param>
        /// <param name="idAsesor"></param>
        /// <returns></returns>
        public List<CorreoInteraccionesAlumnoDTO> ListaInteraccionCorreoAlumnoPorOportunidadV2(int idAlumno, int idAsesor)
        {
            try
            {
                string _query = @"SELECT sub.*, TIM.Descripcion AS Estado
                FROM
                (
                    SELECT MAX(Id) AS Id, MAX(FechaCreacion) AS FechaCreacion, Categoria, Asunto, CorreoReceptor, CorreoRemitente, IdAlumno, IdAsesor AS IdPersonal, MessageId

                    FROM  com.V_Correo_InteraccionesV2

                    WHERE IdAlumno = @IdAlumno AND IdAsesor = @IdAsesor

                    GROUP BY  Categoria, Asunto, CorreoReceptor, CorreoRemitente, IdAlumno, IdAsesor, MessageId
                ) AS sub
                LEFT JOIN com.T_Mandril AS M ON sub.Id = M.Id
                LEFT JOIN mkt.T_TipoInteraccionMandril AS TIM ON M.Evento = TIM.Nombre
                ORDER BY Id DESC
                ";
                var _queryRespuesta = _dapper.QueryDapper(_query, new { IdAlumno = idAlumno, IdAsesor = idAsesor });
                List<CorreoInteraccionesAlumnoDTO> listaInteracciones = JsonConvert.DeserializeObject<List<CorreoInteraccionesAlumnoDTO>>(_queryRespuesta);
                return listaInteracciones;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }

        /// <summary>
        /// Obtiene la lista de interaccion de un correo por el id del alumno
        /// </summary>
        /// <param name="idAlumno">Id del alumno (PK de la tabla mkt.T_Alumno)</param>
        /// <returns>Lista de objetos de clase CorreoInteraccionesAlumnoDTO</returns>
        public List<CorreoInteraccionesAlumnoDTO> ListaInteraccionCorreoAlumnoPorOportunidad(int idAlumno)
        {
            try
            {
                string query = "SELECT Id, FechaCreacion, Categoria, Asunto, Estado, CorreoReceptor, CorreoRemitente,Remitente,IdAsesor AS IdPersonal, NombreProgramaGeneral FROM  com.V_Correo_Interacciones WHERE IdAlumno = @IdAlumno order by FechaCreacion DESC";
                var queryRespuesta = _dapper.QueryDapper(query, new { IdAlumno = idAlumno});
                List<CorreoInteraccionesAlumnoDTO> listaInteracciones = JsonConvert.DeserializeObject<List<CorreoInteraccionesAlumnoDTO>>(queryRespuesta);
                return listaInteracciones;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            
        }
        /// <summary>
        /// Obtiene los correos enviados por un Asesor a un Alumno en especifico
        /// </summary>
        /// <param name="idAlumno"></param>
        /// <param name="idAsesor"></param>
        /// <returns></returns>
        public List<CorreoInteraccionesAlumnoDTO> ListaInteraccionCorreoAlumno(int idAlumno, int idAsesor, string messageId)
        {
            try
            {
                string _query = "SELECT Id, FechaCreacion, Categoria, Asunto, Estado, CorreoReceptor, CorreoRemitente FROM  com.V_Correo_InteraccionesV2 WHERE IdAlumno = @IdAlumno AND IdAsesor = @IdAsesor AND MessageId=@MessageId";
                var _queryRespuesta = _dapper.QueryDapper(_query, new { IdAlumno = idAlumno, IdAsesor = idAsesor, MessageId = messageId });
                List<CorreoInteraccionesAlumnoDTO> listaInteracciones = JsonConvert.DeserializeObject<List<CorreoInteraccionesAlumnoDTO>>(_queryRespuesta);
                return listaInteracciones;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }
        /// <summary>
        /// Obtiene los correos enviados por un Asesor a un Alumno en especifico
        /// </summary>
        /// <param name="idAlumno"></param>
        /// <param name="idAsesor"></param>
        /// <returns></returns>
        public CorreoAlumnoSpeechDTO VerCorreoAlumnoSpeech(string correoReceptor, string messageId)
        {
            try
            {
                string query = "com.SP_ObtenerCorreoSpeech";
                var queryRespuesta = _dapper.QuerySPFirstOrDefault(query, new { MessageId = messageId, CorreoReceptor = correoReceptor });
                CorreoAlumnoSpeechDTO listaCorreos = JsonConvert.DeserializeObject<CorreoAlumnoSpeechDTO>(queryRespuesta);
                return listaCorreos;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }
    }
}
