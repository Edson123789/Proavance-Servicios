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
    /// Repositorio: Marketing/AsesorSubAreaCapacitacionDetalle
    /// Autor: Gian Miranda
    /// Fecha: 21/01/2021
    /// <summary>
    /// Repositorio para la interaccion con la DB mkt.T_AsesorSubAreaCapacitacionDetalle
    /// </summary>
    public class AsesorSubAreaCapacitacionDetalleRepositorio : BaseRepository<TAsesorSubAreaCapacitacionDetalle, AsesorSubAreaCapacitacionDetalleBO>
    {
        #region Metodos Base
        public AsesorSubAreaCapacitacionDetalleRepositorio() : base()
        {
        }
        public AsesorSubAreaCapacitacionDetalleRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<AsesorSubAreaCapacitacionDetalleBO> GetBy(Expression<Func<TAsesorSubAreaCapacitacionDetalle, bool>> filter)
        {
            IEnumerable<TAsesorSubAreaCapacitacionDetalle> listado = base.GetBy(filter);
            List<AsesorSubAreaCapacitacionDetalleBO> listadoBO = new List<AsesorSubAreaCapacitacionDetalleBO>();
            foreach (var itemEntidad in listado)
            {
                AsesorSubAreaCapacitacionDetalleBO objetoBO = Mapper.Map<TAsesorSubAreaCapacitacionDetalle, AsesorSubAreaCapacitacionDetalleBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public AsesorSubAreaCapacitacionDetalleBO FirstById(int id)
        {
            try
            {
                TAsesorSubAreaCapacitacionDetalle entidad = base.FirstById(id);
                AsesorSubAreaCapacitacionDetalleBO objetoBO = new AsesorSubAreaCapacitacionDetalleBO();
                Mapper.Map<TAsesorSubAreaCapacitacionDetalle, AsesorSubAreaCapacitacionDetalleBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public AsesorSubAreaCapacitacionDetalleBO FirstBy(Expression<Func<TAsesorSubAreaCapacitacionDetalle, bool>> filter)
        {
            try
            {
                TAsesorSubAreaCapacitacionDetalle entidad = base.FirstBy(filter);
                AsesorSubAreaCapacitacionDetalleBO objetoBO = Mapper.Map<TAsesorSubAreaCapacitacionDetalle, AsesorSubAreaCapacitacionDetalleBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(AsesorSubAreaCapacitacionDetalleBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TAsesorSubAreaCapacitacionDetalle entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<AsesorSubAreaCapacitacionDetalleBO> listadoBO)
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

        public bool Update(AsesorSubAreaCapacitacionDetalleBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TAsesorSubAreaCapacitacionDetalle entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<AsesorSubAreaCapacitacionDetalleBO> listadoBO)
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
        private void AsignacionId(TAsesorSubAreaCapacitacionDetalle entidad, AsesorSubAreaCapacitacionDetalleBO objetoBO)
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

        private TAsesorSubAreaCapacitacionDetalle MapeoEntidad(AsesorSubAreaCapacitacionDetalleBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TAsesorSubAreaCapacitacionDetalle entidad = new TAsesorSubAreaCapacitacionDetalle();
                entidad = Mapper.Map<AsesorSubAreaCapacitacionDetalleBO, TAsesorSubAreaCapacitacionDetalle>(objetoBO,
                    opt => opt.ConfigureMap(MemberList.None));

                //mapea los hijos

                return entidad;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public IEnumerable<AsesorSubAreaCapacitacionDetalleBO> GetFiltered<KProperty>(IEnumerable<Expression<Func<TAsesorSubAreaCapacitacionDetalle, bool>>> filters, Expression<Func<TAsesorSubAreaCapacitacionDetalle, KProperty>> orderBy, bool ascending)
        {
            IEnumerable<TAsesorSubAreaCapacitacionDetalle> listado = base.GetFiltered(filters, orderBy, ascending);
            List<AsesorSubAreaCapacitacionDetalleBO> listadoBO = new List<AsesorSubAreaCapacitacionDetalleBO>();

            foreach (var itemEntidad in listado)
            {
                AsesorSubAreaCapacitacionDetalleBO objetoBO = Mapper.Map<TAsesorSubAreaCapacitacionDetalle, AsesorSubAreaCapacitacionDetalleBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }
            return listadoBO;
        }
        #endregion
    }
}
