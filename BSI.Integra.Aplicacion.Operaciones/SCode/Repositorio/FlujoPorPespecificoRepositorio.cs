using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using AutoMapper;
using BSI.Integra.Aplicacion.Operaciones.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;

namespace BSI.Integra.Aplicacion.Operaciones.Repositorio
{
    public class FlujoPorPespecificoRepositorio : BaseRepository<TFlujoPorPespecifico, FlujoPorPespecificoBO>
    {
        #region Metodos Base
        public FlujoPorPespecificoRepositorio() : base()
        {
        }
        public FlujoPorPespecificoRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<FlujoPorPespecificoBO> GetBy(Expression<Func<TFlujoPorPespecifico, bool>> filter)
        {
            IEnumerable<TFlujoPorPespecifico> listado = base.GetBy(filter);
            List<FlujoPorPespecificoBO> listadoBO = new List<FlujoPorPespecificoBO>();
            foreach (var itemEntidad in listado)
            {
                FlujoPorPespecificoBO objetoBO = Mapper.Map<TFlujoPorPespecifico, FlujoPorPespecificoBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public FlujoPorPespecificoBO FirstById(int id)
        {
            try
            {
                TFlujoPorPespecifico entidad = base.FirstById(id);
                FlujoPorPespecificoBO objetoBO = new FlujoPorPespecificoBO();
                Mapper.Map<TFlujoPorPespecifico, FlujoPorPespecificoBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public FlujoPorPespecificoBO FirstBy(Expression<Func<TFlujoPorPespecifico, bool>> filter)
        {
            try
            {
                TFlujoPorPespecifico entidad = base.FirstBy(filter);
                FlujoPorPespecificoBO objetoBO = Mapper.Map<TFlujoPorPespecifico, FlujoPorPespecificoBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(FlujoPorPespecificoBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TFlujoPorPespecifico entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<FlujoPorPespecificoBO> listadoBO)
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

        public bool Update(FlujoPorPespecificoBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TFlujoPorPespecifico entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<FlujoPorPespecificoBO> listadoBO)
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
        private void AsignacionId(TFlujoPorPespecifico entidad, FlujoPorPespecificoBO objetoBO)
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

        private TFlujoPorPespecifico MapeoEntidad(FlujoPorPespecificoBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TFlujoPorPespecifico entidad = new TFlujoPorPespecifico();
                entidad = Mapper.Map<FlujoPorPespecificoBO, TFlujoPorPespecifico>(objetoBO,
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
