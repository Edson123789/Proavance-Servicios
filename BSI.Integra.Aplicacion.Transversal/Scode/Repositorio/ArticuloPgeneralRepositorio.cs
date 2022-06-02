using AutoMapper;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace BSI.Integra.Aplicacion.Transversal.Repositorio
{
    public class ArticuloPgeneralRepositorio : BaseRepository<TArticuloPgeneral, ArticuloPgeneralBO>
    {
        #region Metodos Base
        public ArticuloPgeneralRepositorio() : base()
        {
        }
        public ArticuloPgeneralRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<ArticuloPgeneralBO> GetBy(Expression<Func<TArticuloPgeneral, bool>> filter)
        {
            IEnumerable<TArticuloPgeneral> listado = base.GetBy(filter);
            List<ArticuloPgeneralBO> listadoBO = new List<ArticuloPgeneralBO>();
            foreach (var itemEntidad in listado)
            {
                ArticuloPgeneralBO objetoBO = Mapper.Map<TArticuloPgeneral, ArticuloPgeneralBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public ArticuloPgeneralBO FirstById(int id)
        {
            try
            {
                TArticuloPgeneral entidad = base.FirstById(id);
                ArticuloPgeneralBO objetoBO = new ArticuloPgeneralBO();
                Mapper.Map<TArticuloPgeneral, ArticuloPgeneralBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public ArticuloPgeneralBO FirstBy(Expression<Func<TArticuloPgeneral, bool>> filter)
        {
            try
            {
                TArticuloPgeneral entidad = base.FirstBy(filter);
                ArticuloPgeneralBO objetoBO = Mapper.Map<TArticuloPgeneral, ArticuloPgeneralBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(ArticuloPgeneralBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TArticuloPgeneral entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<ArticuloPgeneralBO> listadoBO)
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

        public bool Update(ArticuloPgeneralBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TArticuloPgeneral entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<ArticuloPgeneralBO> listadoBO)
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
        private void AsignacionId(TArticuloPgeneral entidad, ArticuloPgeneralBO objetoBO)
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

        private TArticuloPgeneral MapeoEntidad(ArticuloPgeneralBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TArticuloPgeneral entidad = new TArticuloPgeneral();
                entidad = Mapper.Map<ArticuloPgeneralBO, TArticuloPgeneral>(objetoBO,
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
