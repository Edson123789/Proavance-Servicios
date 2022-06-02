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
    /// Repositorio: Comercial/AsesorChat
    /// Autor: Fischer Valdez - Wilber Choque - Gian Miranda
    /// Fecha: 15/04/2021
    /// <summary>
    /// Repositorio para consultas de com.T_AsesorChat
    /// </summary>
    public class AsesorChatRepositorio : BaseRepository<TAsesorChat, AsesorChatBO>
    {
        #region Metodos Base
        public AsesorChatRepositorio() : base()
        {
        }
        public AsesorChatRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<AsesorChatBO> GetBy(Expression<Func<TAsesorChat, bool>> filter)
        {
            IEnumerable<TAsesorChat> listado = base.GetBy(filter);
            List<AsesorChatBO> listadoBO = new List<AsesorChatBO>();
            foreach (var itemEntidad in listado)
            {
                AsesorChatBO objetoBO = Mapper.Map<TAsesorChat, AsesorChatBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public AsesorChatBO FirstById(int id)
        {
            try
            {
                TAsesorChat entidad = base.FirstById(id);
                AsesorChatBO objetoBO = new AsesorChatBO();
                Mapper.Map<TAsesorChat, AsesorChatBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public AsesorChatBO FirstBy(Expression<Func<TAsesorChat, bool>> filter)
        {
            try
            {
                TAsesorChat entidad = base.FirstBy(filter);
                AsesorChatBO objetoBO = Mapper.Map<TAsesorChat, AsesorChatBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public bool Insert(AsesorChatBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TAsesorChat entidad = MapeoEntidad(objetoBO);

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
        public bool Insert(IEnumerable<AsesorChatBO> listadoBO)
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
        public bool Update(AsesorChatBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TAsesorChat entidad = MapeoEntidad(objetoBO);

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
        public bool Update(IEnumerable<AsesorChatBO> listadoBO)
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
        private void AsignacionId(TAsesorChat entidad, AsesorChatBO objetoBO)
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
        private TAsesorChat MapeoEntidad(AsesorChatBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TAsesorChat entidad = new TAsesorChat();
                entidad = Mapper.Map<AsesorChatBO, TAsesorChat>(objetoBO,
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
        /// Obtiene todos los asesorChat con estado = 1
        /// </summary>
        /// <returns>Lista de objetos de clase AsesorChatFiltroDTO</returns>
        public List<AsesorChatFiltroDTO> ObtenerTodoFiltro()
        {
            try
            {
                List<AsesorChatFiltroDTO> asesorChats = new List<AsesorChatFiltroDTO>();
                var query = @"SELECT Id,
                                        IdPersonal,
                                        NombrePersonal,
                                        IdArea,
                                        NombreArea,
                                        IdSubArea,
                                        NombreSubArea,
                                        IdPais,
                                        NombrePais,
                                        IdPGeneral,
                                        NombrePGeneral 
                                FROM com.V_TAsesorChat_ObtenerDatosParaGrilla 
                                WHERE EstadoAsesorChat = 1 AND EstadoAsesorChatDetalle = 1 AND EstadoPGeneral = 1 AND EstadoPais = 1 
                                AND EstadoArea = 1 AND EstadoSubArea = 1";

                var asesorChatDB = _dapper.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(asesorChatDB) && !asesorChatDB.Contains("[]"))
                {
                    asesorChats = JsonConvert.DeserializeObject<List<AsesorChatFiltroDTO>>(asesorChatDB);
                }
                return asesorChats;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Obtiene un listado de los chats asignados y no asignados
        /// </summary>
        /// <returns>Lista de objetos de clase ChatAsignadoNoAsignadoDTO</returns>
        public List<ChatAsignadoNoAsignadoDTO> ObtenerChatAsignadosNoAsignados()
        {
            try
            {
                List<ChatAsignadoNoAsignadoDTO> chatsAsignados = new List<ChatAsignadoNoAsignadoDTO>();
                var _query = "SELECT NombreArea, NombreSubArea, NombrePGeneral, NombrePais, IdAsesorChat, EsAsignado, NombrePersonal FROM com.V_ObtenerChatAsignadosNoAsignados";
                var chatsAsignadosDB = _dapper.QueryDapper(_query, null);
                if (!string.IsNullOrEmpty(chatsAsignadosDB) && !chatsAsignadosDB.Contains("[]"))
                {
                    chatsAsignados = JsonConvert.DeserializeObject<List<ChatAsignadoNoAsignadoDTO>>(chatsAsignadosDB);
                }
                return chatsAsignados;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Obtiene Informacion de usuario por numero de alumno
        /// </summary>
        /// <param name="Numero"></param>
        /// <returns></returns>
        public PersonalAlumnoDTO ObtenerOportunidadPorNumero(int IdCentroCosto, string Numero)
        {
            try
            {
                string _queryPrograma = "Select IdPersonal From [com].[V_ObtenerChatAsignadosParaWhatsApp] Where IdCentroCosto=@IdCentroCosto";
                var queryPrograma = _dapper.FirstOrDefault(_queryPrograma, new { IdCentroCosto });
                var Oportunidad = JsonConvert.DeserializeObject<PersonalAlumnoDTO>(queryPrograma);

                string _queryAlumno = "Select Id AS IdAlumno From [mkt].[T_Alumno] Where Celular=@Numero Order By FechaCreacion Desc";
                var queryAlumno = _dapper.FirstOrDefault(_queryAlumno, new { Numero });
                var Alumno = JsonConvert.DeserializeObject<PersonalAlumnoDTO>(queryAlumno);

                if (Alumno != null)
                {
                    Oportunidad.IdAlumno = Alumno.IdAlumno;
                }

                return Oportunidad;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
    }
}
