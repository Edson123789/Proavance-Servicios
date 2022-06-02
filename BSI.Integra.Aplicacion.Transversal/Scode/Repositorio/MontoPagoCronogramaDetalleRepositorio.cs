using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using AutoMapper;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;

namespace BSI.Integra.Aplicacion.Transversal.Repositorio
{
    public class MontoPagoCronogramaDetalleRepositorio : BaseRepository<TMontoPagoCronogramaDetalle, MontoPagoCronogramaDetalleBO>
    {
        #region Metodos Base
        public MontoPagoCronogramaDetalleRepositorio() : base()
        {
        }
        public MontoPagoCronogramaDetalleRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<MontoPagoCronogramaDetalleBO> GetBy(Expression<Func<TMontoPagoCronogramaDetalle, bool>> filter)
        {
            IEnumerable<TMontoPagoCronogramaDetalle> listado = base.GetBy(filter);
            List<MontoPagoCronogramaDetalleBO> listadoBO = new List<MontoPagoCronogramaDetalleBO>();
            foreach (var itemEntidad in listado)
            {
                MontoPagoCronogramaDetalleBO objetoBO = Mapper.Map<TMontoPagoCronogramaDetalle, MontoPagoCronogramaDetalleBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public MontoPagoCronogramaDetalleBO FirstById(int id)
        {
            try
            {
                TMontoPagoCronogramaDetalle entidad = base.FirstById(id);
                MontoPagoCronogramaDetalleBO objetoBO = new MontoPagoCronogramaDetalleBO();
                Mapper.Map<TMontoPagoCronogramaDetalle, MontoPagoCronogramaDetalleBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public MontoPagoCronogramaDetalleBO FirstBy(Expression<Func<TMontoPagoCronogramaDetalle, bool>> filter)
        {
            try
            {
                TMontoPagoCronogramaDetalle entidad = base.FirstBy(filter);
                MontoPagoCronogramaDetalleBO objetoBO = Mapper.Map<TMontoPagoCronogramaDetalle, MontoPagoCronogramaDetalleBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(MontoPagoCronogramaDetalleBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TMontoPagoCronogramaDetalle entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<MontoPagoCronogramaDetalleBO> listadoBO)
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

        public bool Update(MontoPagoCronogramaDetalleBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TMontoPagoCronogramaDetalle entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<MontoPagoCronogramaDetalleBO> listadoBO)
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
        private void AsignacionId(TMontoPagoCronogramaDetalle entidad, MontoPagoCronogramaDetalleBO objetoBO)
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

        private TMontoPagoCronogramaDetalle MapeoEntidad(MontoPagoCronogramaDetalleBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TMontoPagoCronogramaDetalle entidad = new TMontoPagoCronogramaDetalle();
                entidad = Mapper.Map<MontoPagoCronogramaDetalleBO, TMontoPagoCronogramaDetalle>(objetoBO,
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
    }
}
