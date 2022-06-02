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
    public class AsignacionAutomaticaOrigenRepositorio : BaseRepository<TAsignacionAutomaticaOrigen, AsignacionAutomaticaOrigenBO>
    {
        #region Metodos Base
        public AsignacionAutomaticaOrigenRepositorio() : base()
        {
        }
        public AsignacionAutomaticaOrigenRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<AsignacionAutomaticaOrigenBO> GetBy(Expression<Func<TAsignacionAutomaticaOrigen, bool>> filter)
        {
            IEnumerable<TAsignacionAutomaticaOrigen> listado = base.GetBy(filter);
            List<AsignacionAutomaticaOrigenBO> listadoBO = new List<AsignacionAutomaticaOrigenBO>();
            foreach (var itemEntidad in listado)
            {
                AsignacionAutomaticaOrigenBO objetoBO = Mapper.Map<TAsignacionAutomaticaOrigen, AsignacionAutomaticaOrigenBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public AsignacionAutomaticaOrigenBO FirstById(int id)
        {
            try
            {
                TAsignacionAutomaticaOrigen entidad = base.FirstById(id);
                AsignacionAutomaticaOrigenBO objetoBO = new AsignacionAutomaticaOrigenBO();
                Mapper.Map<TAsignacionAutomaticaOrigen, AsignacionAutomaticaOrigenBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public AsignacionAutomaticaOrigenBO FirstBy(Expression<Func<TAsignacionAutomaticaOrigen, bool>> filter)
        {
            try
            {
                TAsignacionAutomaticaOrigen entidad = base.FirstBy(filter);
                AsignacionAutomaticaOrigenBO objetoBO = Mapper.Map<TAsignacionAutomaticaOrigen, AsignacionAutomaticaOrigenBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(AsignacionAutomaticaOrigenBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TAsignacionAutomaticaOrigen entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<AsignacionAutomaticaOrigenBO> listadoBO)
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

        public bool Update(AsignacionAutomaticaOrigenBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TAsignacionAutomaticaOrigen entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<AsignacionAutomaticaOrigenBO> listadoBO)
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
        private void AsignacionId(TAsignacionAutomaticaOrigen entidad, AsignacionAutomaticaOrigenBO objetoBO)
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

        private TAsignacionAutomaticaOrigen MapeoEntidad(AsignacionAutomaticaOrigenBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TAsignacionAutomaticaOrigen entidad = new TAsignacionAutomaticaOrigen();
                entidad = Mapper.Map<AsignacionAutomaticaOrigenBO, TAsignacionAutomaticaOrigen>(objetoBO,
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
