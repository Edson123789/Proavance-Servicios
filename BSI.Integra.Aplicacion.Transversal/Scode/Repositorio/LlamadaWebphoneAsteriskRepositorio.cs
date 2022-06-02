using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using AutoMapper;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.DTOs.Comercial;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using Newtonsoft.Json;

namespace BSI.Integra.Aplicacion.Transversal.Repositorio
{
    /// Repositorio: Transversal/LlamadaWebphoneAsteriskRepositorio
    /// Autor: Ansoli Espinoza
    /// Fecha: 26-01-2021
    /// <summary>
    /// Repositorio de la tabla LlamadaWebphoneAsterisk
    /// </summary>
    public class LlamadaWebphoneAsteriskRepositorio : BaseRepository<TLlamadaWebphoneAsterisk, LlamadaWebphoneAsteriskBO>
    {
        #region Metodos Base
        public LlamadaWebphoneAsteriskRepositorio() : base()
        {
        }
        public LlamadaWebphoneAsteriskRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<LlamadaWebphoneAsteriskBO> GetBy(Expression<Func<TLlamadaWebphoneAsterisk, bool>> filter)
        {
            IEnumerable<TLlamadaWebphoneAsterisk> listado = base.GetBy(filter);
            List<LlamadaWebphoneAsteriskBO> listadoBO = new List<LlamadaWebphoneAsteriskBO>();
            foreach (var itemEntidad in listado)
            {
                LlamadaWebphoneAsteriskBO objetoBO = Mapper.Map<TLlamadaWebphoneAsterisk, LlamadaWebphoneAsteriskBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public LlamadaWebphoneAsteriskBO FirstById(int id)
        {
            try
            {
                TLlamadaWebphoneAsterisk entidad = base.FirstById(id);
                LlamadaWebphoneAsteriskBO objetoBO = new LlamadaWebphoneAsteriskBO();
                Mapper.Map<TLlamadaWebphoneAsterisk, LlamadaWebphoneAsteriskBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public LlamadaWebphoneAsteriskBO FirstBy(Expression<Func<TLlamadaWebphoneAsterisk, bool>> filter)
        {
            try
            {
                TLlamadaWebphoneAsterisk entidad = base.FirstBy(filter);
                LlamadaWebphoneAsteriskBO objetoBO = Mapper.Map<TLlamadaWebphoneAsterisk, LlamadaWebphoneAsteriskBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(LlamadaWebphoneAsteriskBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TLlamadaWebphoneAsterisk entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<LlamadaWebphoneAsteriskBO> listadoBO)
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

        public bool Update(LlamadaWebphoneAsteriskBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TLlamadaWebphoneAsterisk entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<LlamadaWebphoneAsteriskBO> listadoBO)
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
        private void AsignacionId(TLlamadaWebphoneAsterisk entidad, LlamadaWebphoneAsteriskBO objetoBO)
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

        private TLlamadaWebphoneAsterisk MapeoEntidad(LlamadaWebphoneAsteriskBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TLlamadaWebphoneAsterisk entidad = new TLlamadaWebphoneAsterisk();
                entidad = Mapper.Map<LlamadaWebphoneAsteriskBO, TLlamadaWebphoneAsterisk>(objetoBO,
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

        /// Autor: Ansoli Espinoza
        /// Fecha: 26-01-2021
        /// Version: 1.0
        /// <summary>
        /// Devuelve el ultimo id importado en v4
        /// </summary>
        /// <returns>Devuele la respuesta de la importación</returns>
        public int ObtenerUltimoIdImportado()
        {
            if (this.Exist(w => w.Estado == true))
                return this.GetMaxInt(w => w.CdrId);
            else return 0;
        }

        /// Autor: Ansoli Espinoza
        /// Fecha: 26-02-2021
        /// Version: 1.0
        /// <summary>
        /// Devuelve la lista de llamadas pendientes de general respaldo
        /// </summary>
        /// <returns>Lista de LlamadaAsteriskRespaldarDTO</returns>
        public List<LlamadaAsteriskRespaldarDTO> ListadoLlamadaPendienteRespaldar()
        {
            string query = "SELECT Id, NombreGrabacion, Anexo, Anho, Mes from com.V_LlamadaAsterisk_PendienteRespaldar";
            string queryRespuesta = _dapper.QueryDapper(query, null);
            return JsonConvert.DeserializeObject<List<LlamadaAsteriskRespaldarDTO>>(queryRespuesta);
        }

        /// Autor: Ansoli Espinoza
        /// Fecha: 26-02-2021
        /// Version: 1.0
        /// <summary>
        /// Devuelve la lista de llamadas pendientes de eliminar
        /// </summary>
        public List<LlamadaAsteriskRespaldarDTO> ListadoLlamadaPendienteEliminar()
        {
            string query = "SELECT Id, NombreGrabacion FROM com.V_LlamadaAsterisk_PendienteEliminar";
            string queryRespuesta = _dapper.QueryDapper(query, null);
            return JsonConvert.DeserializeObject<List<LlamadaAsteriskRespaldarDTO>>(queryRespuesta);
        }

        /// Autor: Jashin Salazar
        /// Fecha: 15-02-2022
        /// Version: 1.0
        /// <summary>
        /// Modifica una llamada del repositorio
        /// </summary>
        public RespuestaLlamadaWebphoneModificadoDTO ModificarLlamadaWebphone(int IdLlamada, string Url, string NombreUsuario, int DuracionContesto, int NroBytes)
        {
            string query = "com.SP_ModificarLlamadaWebphoneAsterisk";
            string queryRespuesta = _dapper.QuerySPFirstOrDefault(query, new { IdLlamada= IdLlamada, Url=Url, Usuario= NombreUsuario, DuracionContestado= DuracionContesto, Bytes= NroBytes });
            return JsonConvert.DeserializeObject<RespuestaLlamadaWebphoneModificadoDTO>(queryRespuesta);
        }
    }
}
