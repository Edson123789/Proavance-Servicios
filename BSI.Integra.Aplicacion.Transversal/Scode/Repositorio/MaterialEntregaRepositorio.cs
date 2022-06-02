using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using AutoMapper;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;

namespace BSI.Integra.Aplicacion.Transversal.Repositorio
{
    public class MaterialEntregaRepositorio : BaseRepository<TMaterialEntrega, MaterialEntregaBO>
    {
        #region Metodos Base
        public MaterialEntregaRepositorio() : base()
        {
        }
        public MaterialEntregaRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<MaterialEntregaBO> GetBy(Expression<Func<TMaterialEntrega, bool>> filter)
        {
            IEnumerable<TMaterialEntrega> listado = base.GetBy(filter);
            List<MaterialEntregaBO> listadoBO = new List<MaterialEntregaBO>();
            foreach (var itemEntidad in listado)
            {
                MaterialEntregaBO objetoBO = Mapper.Map<TMaterialEntrega, MaterialEntregaBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public MaterialEntregaBO FirstById(int id)
        {
            try
            {
                TMaterialEntrega entidad = base.FirstById(id);
                MaterialEntregaBO objetoBO = new MaterialEntregaBO();
                Mapper.Map<TMaterialEntrega, MaterialEntregaBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public MaterialEntregaBO FirstBy(Expression<Func<TMaterialEntrega, bool>> filter)
        {
            try
            {
                TMaterialEntrega entidad = base.FirstBy(filter);
                MaterialEntregaBO objetoBO = Mapper.Map<TMaterialEntrega, MaterialEntregaBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(MaterialEntregaBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TMaterialEntrega entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<MaterialEntregaBO> listadoBO)
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

        public bool Update(MaterialEntregaBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TMaterialEntrega entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<MaterialEntregaBO> listadoBO)
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
        private void AsignacionId(TMaterialEntrega entidad, MaterialEntregaBO objetoBO)
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

        private TMaterialEntrega MapeoEntidad(MaterialEntregaBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TMaterialEntrega entidad = new TMaterialEntrega();
                entidad = Mapper.Map<MaterialEntregaBO, TMaterialEntrega>(objetoBO,
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
