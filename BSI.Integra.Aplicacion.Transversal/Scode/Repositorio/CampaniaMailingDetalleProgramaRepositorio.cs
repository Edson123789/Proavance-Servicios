using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using AutoMapper;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using Newtonsoft.Json;

namespace BSI.Integra.Aplicacion.Transversal.Repositorio
{
    public class CampaniaMailingDetalleProgramaRepositorio : BaseRepository<TCampaniaMailingDetallePrograma, CampaniaMailingDetalleProgramaBO>
    {
        #region Metodos Base
        public CampaniaMailingDetalleProgramaRepositorio() : base()
        {
        }
        public CampaniaMailingDetalleProgramaRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<CampaniaMailingDetalleProgramaBO> GetBy(Expression<Func<TCampaniaMailingDetallePrograma, bool>> filter)
        {
            IEnumerable<TCampaniaMailingDetallePrograma> listado = base.GetBy(filter);
            List<CampaniaMailingDetalleProgramaBO> listadoBO = new List<CampaniaMailingDetalleProgramaBO>();
            foreach (var itemEntidad in listado)
            {
                CampaniaMailingDetalleProgramaBO objetoBO = Mapper.Map<TCampaniaMailingDetallePrograma, CampaniaMailingDetalleProgramaBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public CampaniaMailingDetalleProgramaBO FirstById(int id)
        {
            try
            {
                TCampaniaMailingDetallePrograma entidad = base.FirstById(id);
                CampaniaMailingDetalleProgramaBO objetoBO = new CampaniaMailingDetalleProgramaBO();
                Mapper.Map<TCampaniaMailingDetallePrograma, CampaniaMailingDetalleProgramaBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public CampaniaMailingDetalleProgramaBO FirstBy(Expression<Func<TCampaniaMailingDetallePrograma, bool>> filter)
        {
            try
            {
                TCampaniaMailingDetallePrograma entidad = base.FirstBy(filter);
                CampaniaMailingDetalleProgramaBO objetoBO = Mapper.Map<TCampaniaMailingDetallePrograma, CampaniaMailingDetalleProgramaBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(CampaniaMailingDetalleProgramaBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TCampaniaMailingDetallePrograma entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<CampaniaMailingDetalleProgramaBO> listadoBO)
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

        public bool Update(CampaniaMailingDetalleProgramaBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TCampaniaMailingDetallePrograma entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<CampaniaMailingDetalleProgramaBO> listadoBO)
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
        private void AsignacionId(TCampaniaMailingDetallePrograma entidad, CampaniaMailingDetalleProgramaBO objetoBO)
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

        private TCampaniaMailingDetallePrograma MapeoEntidad(CampaniaMailingDetalleProgramaBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TCampaniaMailingDetallePrograma entidad = new TCampaniaMailingDetallePrograma();
                entidad = Mapper.Map<CampaniaMailingDetalleProgramaBO, TCampaniaMailingDetallePrograma>(objetoBO,
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
        /// Obteniene el Id de T_PGeneral y la Etiqueta
        /// </summary>
        /// <param name="idPrioridad"></param>
        /// <returns></returns>
        public List<CampaniaMailingPGeneralEtiquetaDTO> ObtenerProgramaYEtiqueta(int idPrioridad)
        {
            try
            {
                List<CampaniaMailingPGeneralEtiquetaDTO> listaPGeneralEtiqueta = new List<CampaniaMailingPGeneralEtiquetaDTO>();
                string _queryObtenerPGeneralEtiqueta = "select IdPGeneral, Etiqueta from mkt.V_ObtenerPGeneralEtiqueta " +
                    "where IdCampaniaMailingDetalle = @IdCampaniaMailingDetalle and EstadoDetallePrograma = 1 and TipoPrograma != @TipoPrograma ";
                string registrosBD = _dapper.QueryDapper(_queryObtenerPGeneralEtiqueta, new
                {
                    @IdCampaniaMailingDetalle = idPrioridad,
                    @TipoPrograma = "Filtro"
                });
                if (!string.IsNullOrEmpty(registrosBD) && !registrosBD.Contains("[]"))
                {
                    listaPGeneralEtiqueta = JsonConvert.DeserializeObject<List<CampaniaMailingPGeneralEtiquetaDTO>>(registrosBD);
                }
                return listaPGeneralEtiqueta;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        
        /// <summary>
        /// Obteniene Programa Por CamapaniMailindetalle
        /// </summary>
        /// <param name="idPrioridad"></param>
        /// <returns></returns>
        public List<FiltroPGeneralDTO> ObtenerProgramaPorCamapaniaMailingDetalle(int IdCampaniaMailinDetalle, string Tipo)
        {
            try
            {
                List<FiltroPGeneralDTO> listaPGeneralEtiqueta = new List<FiltroPGeneralDTO>();
                string _queryObtenerPGeneralEtiqueta = "select Id, Nombre from mkt.V_ObtenerProgramaPorCampaniaMailinDetalle " +
                    "where IdCampaniaMailingDetalle = @IdCampaniaMailingDetalle and Tipo=@Tipo and Estado = 1 ";
                string registrosBD = _dapper.QueryDapper(_queryObtenerPGeneralEtiqueta, new
                {
                    IdCampaniaMailingDetalle = IdCampaniaMailinDetalle,
                    Tipo
                });
                if (!string.IsNullOrEmpty(registrosBD) && !registrosBD.Contains("[]"))
                {
                    listaPGeneralEtiqueta = JsonConvert.DeserializeObject<List<FiltroPGeneralDTO>>(registrosBD);
                }
                return listaPGeneralEtiqueta;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
