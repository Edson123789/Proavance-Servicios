using AutoMapper;
using BSI.Integra.Aplicacion.Marketing.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace BSI.Integra.Aplicacion.Marketing.Repositorio
{
    /// Repositorio: Marketing/AsesorAreaCapacitacionDetalle
    /// Autor: Gian Miranda
    /// Fecha: 21/01/2021
    /// <summary>
    /// Repositorio para la interaccion con la DB mkt.T_AsesorAreaCapacitacionDetalle
    /// </summary>
    public class AsesorAreaCapacitacionDetalleRepositorio : BaseRepository<TAsesorAreaCapacitacionDetalle, AsesorAreaCapacitacionDetalleBO>
    {
        #region Metodos Base
        public AsesorAreaCapacitacionDetalleRepositorio() : base()
        {
        }
        public AsesorAreaCapacitacionDetalleRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<AsesorAreaCapacitacionDetalleBO> GetBy(Expression<Func<TAsesorAreaCapacitacionDetalle, bool>> filter)
        {
            IEnumerable<TAsesorAreaCapacitacionDetalle> listado = base.GetBy(filter);
            List<AsesorAreaCapacitacionDetalleBO> listadoBO = new List<AsesorAreaCapacitacionDetalleBO>();
            foreach (var itemEntidad in listado)
            {
                AsesorAreaCapacitacionDetalleBO objetoBO = Mapper.Map<TAsesorAreaCapacitacionDetalle, AsesorAreaCapacitacionDetalleBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public AsesorAreaCapacitacionDetalleBO FirstById(int id)
        {
            try
            {
                TAsesorAreaCapacitacionDetalle entidad = base.FirstById(id);
                AsesorAreaCapacitacionDetalleBO objetoBO = new AsesorAreaCapacitacionDetalleBO();
                Mapper.Map<TAsesorAreaCapacitacionDetalle, AsesorAreaCapacitacionDetalleBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public AsesorAreaCapacitacionDetalleBO FirstBy(Expression<Func<TAsesorAreaCapacitacionDetalle, bool>> filter)
        {
            try
            {
                TAsesorAreaCapacitacionDetalle entidad = base.FirstBy(filter);
                AsesorAreaCapacitacionDetalleBO objetoBO = Mapper.Map<TAsesorAreaCapacitacionDetalle, AsesorAreaCapacitacionDetalleBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(AsesorAreaCapacitacionDetalleBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TAsesorAreaCapacitacionDetalle entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<AsesorAreaCapacitacionDetalleBO> listadoBO)
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

        public bool Update(AsesorAreaCapacitacionDetalleBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TAsesorAreaCapacitacionDetalle entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<AsesorAreaCapacitacionDetalleBO> listadoBO)
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
        private void AsignacionId(TAsesorAreaCapacitacionDetalle entidad, AsesorAreaCapacitacionDetalleBO objetoBO)
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

        private TAsesorAreaCapacitacionDetalle MapeoEntidad(AsesorAreaCapacitacionDetalleBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TAsesorAreaCapacitacionDetalle entidad = new TAsesorAreaCapacitacionDetalle();
                entidad = Mapper.Map<AsesorAreaCapacitacionDetalleBO, TAsesorAreaCapacitacionDetalle>(objetoBO,
                    opt => opt.ConfigureMap(MemberList.None));

                //mapea los hijos

                return entidad;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public IEnumerable<AsesorAreaCapacitacionDetalleBO> GetFiltered<KProperty>(IEnumerable<Expression<Func<TAsesorAreaCapacitacionDetalle, bool>>> filters, Expression<Func<TAsesorAreaCapacitacionDetalle, KProperty>> orderBy, bool ascending)
        {
            IEnumerable<TAsesorAreaCapacitacionDetalle> listado = base.GetFiltered(filters, orderBy, ascending);
            List<AsesorAreaCapacitacionDetalleBO> listadoBO = new List<AsesorAreaCapacitacionDetalleBO>();

            foreach (var itemEntidad in listado)
            {
                AsesorAreaCapacitacionDetalleBO objetoBO = Mapper.Map<TAsesorAreaCapacitacionDetalle, AsesorAreaCapacitacionDetalleBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }
            return listadoBO;
        }
        #endregion
    }
}
