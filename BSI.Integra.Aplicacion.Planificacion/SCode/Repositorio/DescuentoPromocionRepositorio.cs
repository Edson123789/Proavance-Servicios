using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using AutoMapper;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Planificacion.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using Newtonsoft.Json;

namespace BSI.Integra.Aplicacion.Planificacion.Repositorio
{
    public class DescuentoPromocionRepositorio : BaseRepository<TDescuentoPromocion, DescuentoPromocionBO>
    {
        #region Metodos Base
        public DescuentoPromocionRepositorio() : base()
        {
        }
        public DescuentoPromocionRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<DescuentoPromocionBO> GetBy(Expression<Func<TDescuentoPromocion, bool>> filter)
        {
            IEnumerable<TDescuentoPromocion> listado = base.GetBy(filter);
            List<DescuentoPromocionBO> listadoBO = new List<DescuentoPromocionBO>();
            foreach (var itemEntidad in listado)
            {
                DescuentoPromocionBO objetoBO = Mapper.Map<TDescuentoPromocion, DescuentoPromocionBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public DescuentoPromocionBO FirstById(int id)
        {
            try
            {
                TDescuentoPromocion entidad = base.FirstById(id);
                DescuentoPromocionBO objetoBO = new DescuentoPromocionBO();
                Mapper.Map<TDescuentoPromocion, DescuentoPromocionBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public DescuentoPromocionBO FirstBy(Expression<Func<TDescuentoPromocion, bool>> filter)
        {
            try
            {
                TDescuentoPromocion entidad = base.FirstBy(filter);
                DescuentoPromocionBO objetoBO = Mapper.Map<TDescuentoPromocion, DescuentoPromocionBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(DescuentoPromocionBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TDescuentoPromocion entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<DescuentoPromocionBO> listadoBO)
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

        public bool Update(DescuentoPromocionBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TDescuentoPromocion entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<DescuentoPromocionBO> listadoBO)
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
        private void AsignacionId(TDescuentoPromocion entidad, DescuentoPromocionBO objetoBO)
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

        private TDescuentoPromocion MapeoEntidad(DescuentoPromocionBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TDescuentoPromocion entidad = new TDescuentoPromocion();
                entidad = Mapper.Map<DescuentoPromocionBO, TDescuentoPromocion>(objetoBO,
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
