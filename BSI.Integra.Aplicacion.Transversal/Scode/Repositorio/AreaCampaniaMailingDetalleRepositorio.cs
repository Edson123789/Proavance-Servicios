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
    public class AreaCampaniaMailingDetalleRepositorio : BaseRepository<TAreaCampaniaMailingDetalle, AreaCampaniaMailingDetalleBO>
    {
        #region Metodos Base
        public AreaCampaniaMailingDetalleRepositorio() : base()
        {
        }
        public AreaCampaniaMailingDetalleRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<AreaCampaniaMailingDetalleBO> GetBy(Expression<Func<TAreaCampaniaMailingDetalle, bool>> filter)
        {
            IEnumerable<TAreaCampaniaMailingDetalle> listado = base.GetBy(filter);
            List<AreaCampaniaMailingDetalleBO> listadoBO = new List<AreaCampaniaMailingDetalleBO>();
            foreach (var itemEntidad in listado)
            {
                AreaCampaniaMailingDetalleBO objetoBO = Mapper.Map<TAreaCampaniaMailingDetalle, AreaCampaniaMailingDetalleBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public AreaCampaniaMailingDetalleBO FirstById(int id)
        {
            try
            {
                TAreaCampaniaMailingDetalle entidad = base.FirstById(id);
                AreaCampaniaMailingDetalleBO objetoBO = new AreaCampaniaMailingDetalleBO();
                Mapper.Map<TAreaCampaniaMailingDetalle, AreaCampaniaMailingDetalleBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public AreaCampaniaMailingDetalleBO FirstBy(Expression<Func<TAreaCampaniaMailingDetalle, bool>> filter)
        {
            try
            {
                TAreaCampaniaMailingDetalle entidad = base.FirstBy(filter);
                AreaCampaniaMailingDetalleBO objetoBO = Mapper.Map<TAreaCampaniaMailingDetalle, AreaCampaniaMailingDetalleBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(AreaCampaniaMailingDetalleBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TAreaCampaniaMailingDetalle entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<AreaCampaniaMailingDetalleBO> listadoBO)
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

        public bool Update(AreaCampaniaMailingDetalleBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TAreaCampaniaMailingDetalle entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<AreaCampaniaMailingDetalleBO> listadoBO)
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
        private void AsignacionId(TAreaCampaniaMailingDetalle entidad, AreaCampaniaMailingDetalleBO objetoBO)
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

        private TAreaCampaniaMailingDetalle MapeoEntidad(AreaCampaniaMailingDetalleBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TAreaCampaniaMailingDetalle entidad = new TAreaCampaniaMailingDetalle();
                entidad = Mapper.Map<AreaCampaniaMailingDetalleBO, TAreaCampaniaMailingDetalle>(objetoBO,
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
        /// Elimina (Actualiza estado a false ) todos las Areas Clave Valor asociados a una Campania Mailing Detalle
        /// </summary>
        /// <param name="idPGeneral"></param>
        /// <returns></returns>
        public void EliminacionLogicoPorCampaniaMailing(int idCampaniaMailingDetalle, string usuario, List<int> nuevos)
        {
            try
            {
                var listaBorrar = GetBy(x => x.IdCampaniaMailingDetalle == idCampaniaMailingDetalle && x.Estado == true).ToList();
                listaBorrar.RemoveAll(x => nuevos.Any(y => y == x.IdAreaCapacitacion));
                foreach (var item in listaBorrar)
                {
                    Delete(item.Id, usuario);
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
