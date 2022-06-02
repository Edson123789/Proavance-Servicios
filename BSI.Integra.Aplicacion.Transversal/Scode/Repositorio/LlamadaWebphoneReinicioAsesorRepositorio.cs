using System;
using System.Collections.Generic;
using System.Linq;
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
    /// Repositorio: LlamadaWebphoneReinicioAsesorRepositorio
    /// Autor: Jose Villena
    /// Fecha: 03/05/2021
    /// <summary>
    /// Repositorio para consultas de T_LlamadaWebphoneReinicioAsesor
    /// </summary>
    public class LlamadaWebphoneReinicioAsesorRepositorio : BaseRepository<TLlamadaWebphoneReinicioAsesor, LlamadaWebphoneReinicioAsesorBO>
    {
        #region Metodos Base
        public LlamadaWebphoneReinicioAsesorRepositorio() : base()
        {
        }
        public LlamadaWebphoneReinicioAsesorRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<LlamadaWebphoneReinicioAsesorBO> GetBy(Expression<Func<TLlamadaWebphoneReinicioAsesor, bool>> filter)
        {
            IEnumerable<TLlamadaWebphoneReinicioAsesor> listado = base.GetBy(filter);
            List<LlamadaWebphoneReinicioAsesorBO> listadoBO = new List<LlamadaWebphoneReinicioAsesorBO>();
            foreach (var itemEntidad in listado)
            {
                LlamadaWebphoneReinicioAsesorBO objetoBO = Mapper.Map<TLlamadaWebphoneReinicioAsesor, LlamadaWebphoneReinicioAsesorBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public LlamadaWebphoneReinicioAsesorBO FirstById(int id)
        {
            try
            {
                TLlamadaWebphoneReinicioAsesor entidad = base.FirstById(id);
                LlamadaWebphoneReinicioAsesorBO objetoBO = new LlamadaWebphoneReinicioAsesorBO();
                Mapper.Map<TLlamadaWebphoneReinicioAsesor, LlamadaWebphoneReinicioAsesorBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public LlamadaWebphoneReinicioAsesorBO FirstBy(Expression<Func<TLlamadaWebphoneReinicioAsesor, bool>> filter)
        {
            try
            {
                TLlamadaWebphoneReinicioAsesor entidad = base.FirstBy(filter);
                LlamadaWebphoneReinicioAsesorBO objetoBO = Mapper.Map<TLlamadaWebphoneReinicioAsesor, LlamadaWebphoneReinicioAsesorBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(LlamadaWebphoneReinicioAsesorBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TLlamadaWebphoneReinicioAsesor entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<LlamadaWebphoneReinicioAsesorBO> listadoBO)
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

        public bool Update(LlamadaWebphoneReinicioAsesorBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TLlamadaWebphoneReinicioAsesor entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<LlamadaWebphoneReinicioAsesorBO> listadoBO)
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
        private void AsignacionId(TLlamadaWebphoneReinicioAsesor entidad, LlamadaWebphoneReinicioAsesorBO objetoBO)
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

        private TLlamadaWebphoneReinicioAsesor MapeoEntidad(LlamadaWebphoneReinicioAsesorBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TLlamadaWebphoneReinicioAsesor entidad = new TLlamadaWebphoneReinicioAsesor();
                entidad = Mapper.Map<LlamadaWebphoneReinicioAsesorBO, TLlamadaWebphoneReinicioAsesor>(objetoBO,
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
  
        /// Autor: Jose Villena
        /// Fecha: 03/05/2021
        /// Version: 1.0
        /// <summary>
        /// Retorna si un Asesor tiene Acceso al Reinicio de Webphone en su agenda
        /// </summary>
        /// <param name="idPersonal"> Id del Personal </param>     
        /// <returns>Respuesta: Bool </returns> 
        public bool ValidarReinicioWebphone(int idPersonal)
        {
            try
            {
                return Exist(x => x.IdPersonal == idPersonal && x.AplicaReinicio == true);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        /// <summary>
        /// Retorna la lista de asesores configurables para un acceso al reinicio webphone
        /// </summary>
        /// <param name="idPersonal"></param>
        /// <returns></returns>
        public List<LlamadaWebphoneReinicioAsesorDTO> ObtnerPanelReinicioWebphone()
        {
            try
            {
                List<LlamadaWebphoneReinicioAsesorDTO> datos = new List<LlamadaWebphoneReinicioAsesorDTO>();
                datos = GetBy(x => x.Estado == true, y => new LlamadaWebphoneReinicioAsesorDTO {
                    Id = y.Id,
                    IdPersonal = y.IdPersonal,
                    AplicaReinicio = y.AplicaReinicio,
                }).ToList();

                return datos;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
