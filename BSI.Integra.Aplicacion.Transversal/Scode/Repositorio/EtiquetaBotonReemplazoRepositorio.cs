using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using AutoMapper;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;

namespace BSI.Integra.Aplicacion.Transversal.Repositorio
{
    public class EtiquetaBotonReemplazoRepositorio : BaseRepository<TEtiquetaBotonReemplazo, EtiquetaBotonReemplazoBO>
    {
        #region Metodos Base
        public EtiquetaBotonReemplazoRepositorio() : base()
        {
        }
        public EtiquetaBotonReemplazoRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<EtiquetaBotonReemplazoBO> GetBy(Expression<Func<TEtiquetaBotonReemplazo, bool>> filter)
        {
            IEnumerable<TEtiquetaBotonReemplazo> listado = base.GetBy(filter);
            List<EtiquetaBotonReemplazoBO> listadoBO = new List<EtiquetaBotonReemplazoBO>();
            foreach (var itemEntidad in listado)
            {
                EtiquetaBotonReemplazoBO objetoBO = Mapper.Map<TEtiquetaBotonReemplazo, EtiquetaBotonReemplazoBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public EtiquetaBotonReemplazoBO FirstById(int id)
        {
            try
            {
                TEtiquetaBotonReemplazo entidad = base.FirstById(id);
                EtiquetaBotonReemplazoBO objetoBO = new EtiquetaBotonReemplazoBO();
                Mapper.Map<TEtiquetaBotonReemplazo, EtiquetaBotonReemplazoBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public EtiquetaBotonReemplazoBO FirstBy(Expression<Func<TEtiquetaBotonReemplazo, bool>> filter)
        {
            try
            {
                TEtiquetaBotonReemplazo entidad = base.FirstBy(filter);
                EtiquetaBotonReemplazoBO objetoBO = Mapper.Map<TEtiquetaBotonReemplazo, EtiquetaBotonReemplazoBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(EtiquetaBotonReemplazoBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TEtiquetaBotonReemplazo entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<EtiquetaBotonReemplazoBO> listadoBO)
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

        public bool Update(EtiquetaBotonReemplazoBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TEtiquetaBotonReemplazo entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<EtiquetaBotonReemplazoBO> listadoBO)
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
        private void AsignacionId(TEtiquetaBotonReemplazo entidad, EtiquetaBotonReemplazoBO objetoBO)
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

        private TEtiquetaBotonReemplazo MapeoEntidad(EtiquetaBotonReemplazoBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TEtiquetaBotonReemplazo entidad = new TEtiquetaBotonReemplazo();
                entidad = Mapper.Map<EtiquetaBotonReemplazoBO, TEtiquetaBotonReemplazo>(objetoBO,
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
