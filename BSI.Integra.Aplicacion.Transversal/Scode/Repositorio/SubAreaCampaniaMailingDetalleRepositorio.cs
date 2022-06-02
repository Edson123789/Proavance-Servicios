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
    public class SubAreaCampaniaMailingDetalleRepositorio : BaseRepository<TSubAreaCampaniaMailingDetalle, SubAreaCampaniaMailingDetalleBO>
    {
        #region Metodos Base
        public SubAreaCampaniaMailingDetalleRepositorio() : base()
        {
        }
        public SubAreaCampaniaMailingDetalleRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<SubAreaCampaniaMailingDetalleBO> GetBy(Expression<Func<TSubAreaCampaniaMailingDetalle, bool>> filter)
        {
            IEnumerable<TSubAreaCampaniaMailingDetalle> listado = base.GetBy(filter);
            List<SubAreaCampaniaMailingDetalleBO> listadoBO = new List<SubAreaCampaniaMailingDetalleBO>();
            foreach (var itemEntidad in listado)
            {
                SubAreaCampaniaMailingDetalleBO objetoBO = Mapper.Map<TSubAreaCampaniaMailingDetalle, SubAreaCampaniaMailingDetalleBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public SubAreaCampaniaMailingDetalleBO FirstById(int id)
        {
            try
            {
                TSubAreaCampaniaMailingDetalle entidad = base.FirstById(id);
                SubAreaCampaniaMailingDetalleBO objetoBO = new SubAreaCampaniaMailingDetalleBO();
                Mapper.Map<TSubAreaCampaniaMailingDetalle, SubAreaCampaniaMailingDetalleBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public SubAreaCampaniaMailingDetalleBO FirstBy(Expression<Func<TSubAreaCampaniaMailingDetalle, bool>> filter)
        {
            try
            {
                TSubAreaCampaniaMailingDetalle entidad = base.FirstBy(filter);
                SubAreaCampaniaMailingDetalleBO objetoBO = Mapper.Map<TSubAreaCampaniaMailingDetalle, SubAreaCampaniaMailingDetalleBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(SubAreaCampaniaMailingDetalleBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TSubAreaCampaniaMailingDetalle entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<SubAreaCampaniaMailingDetalleBO> listadoBO)
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

        public bool Update(SubAreaCampaniaMailingDetalleBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TSubAreaCampaniaMailingDetalle entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<SubAreaCampaniaMailingDetalleBO> listadoBO)
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
        private void AsignacionId(TSubAreaCampaniaMailingDetalle entidad, SubAreaCampaniaMailingDetalleBO objetoBO)
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

        private TSubAreaCampaniaMailingDetalle MapeoEntidad(SubAreaCampaniaMailingDetalleBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TSubAreaCampaniaMailingDetalle entidad = new TSubAreaCampaniaMailingDetalle();
                entidad = Mapper.Map<SubAreaCampaniaMailingDetalleBO, TSubAreaCampaniaMailingDetalle>(objetoBO,
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
        /// Elimina (Actualiza estado a false ) todos las SubAreas Clave Valor asociados a una Campania Mailing Detalle
        /// </summary>
        /// <param name="idPGeneral"></param>
        /// <returns></returns>
        public void EliminacionLogicoPorCampaniaMailing(int idCampaniaMailingDetalle, string usuario, List<int> nuevos)
        {
            try
            {
                var listaBorrar = GetBy(x => x.IdCampaniaMailingDetalle == idCampaniaMailingDetalle && x.Estado == true).ToList();
                listaBorrar.RemoveAll(x => nuevos.Any(y => y == x.IdSubAreaCapacitacion));
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
