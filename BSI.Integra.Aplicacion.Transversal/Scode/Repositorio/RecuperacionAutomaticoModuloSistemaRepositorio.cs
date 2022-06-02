using AutoMapper;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace BSI.Integra.Aplicacion.Transversal.Repositorio
{
    public class RecuperacionAutomaticoModuloSistemaRepositorio : BaseRepository<TRecuperacionAutomaticoModuloSistema, RecuperacionAutomaticoModuloSistemaBO>
    {
        #region Metodos Base
        public RecuperacionAutomaticoModuloSistemaRepositorio() : base()
        {
        }
        public RecuperacionAutomaticoModuloSistemaRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<RecuperacionAutomaticoModuloSistemaBO> GetBy(Expression<Func<TRecuperacionAutomaticoModuloSistema, bool>> filter)
        {
            IEnumerable<TRecuperacionAutomaticoModuloSistema> listado = base.GetBy(filter);
            List<RecuperacionAutomaticoModuloSistemaBO> listadoBO = new List<RecuperacionAutomaticoModuloSistemaBO>();
            foreach (var itemEntidad in listado)
            {
                RecuperacionAutomaticoModuloSistemaBO objetoBO = Mapper.Map<TRecuperacionAutomaticoModuloSistema, RecuperacionAutomaticoModuloSistemaBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public RecuperacionAutomaticoModuloSistemaBO FirstById(int id)
        {
            try
            {
                TRecuperacionAutomaticoModuloSistema entidad = base.FirstById(id);
                RecuperacionAutomaticoModuloSistemaBO objetoBO = new RecuperacionAutomaticoModuloSistemaBO();
                Mapper.Map<TRecuperacionAutomaticoModuloSistema, RecuperacionAutomaticoModuloSistemaBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public RecuperacionAutomaticoModuloSistemaBO FirstBy(Expression<Func<TRecuperacionAutomaticoModuloSistema, bool>> filter)
        {
            try
            {
                TRecuperacionAutomaticoModuloSistema entidad = base.FirstBy(filter);
                RecuperacionAutomaticoModuloSistemaBO objetoBO = Mapper.Map<TRecuperacionAutomaticoModuloSistema, RecuperacionAutomaticoModuloSistemaBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(RecuperacionAutomaticoModuloSistemaBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TRecuperacionAutomaticoModuloSistema entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<RecuperacionAutomaticoModuloSistemaBO> listadoBO)
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

        public bool Update(RecuperacionAutomaticoModuloSistemaBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TRecuperacionAutomaticoModuloSistema entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<RecuperacionAutomaticoModuloSistemaBO> listadoBO)
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
        private void AsignacionId(TRecuperacionAutomaticoModuloSistema entidad, RecuperacionAutomaticoModuloSistemaBO objetoBO)
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

        private TRecuperacionAutomaticoModuloSistema MapeoEntidad(RecuperacionAutomaticoModuloSistemaBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TRecuperacionAutomaticoModuloSistema entidad = new TRecuperacionAutomaticoModuloSistema();
                entidad = Mapper.Map<RecuperacionAutomaticoModuloSistemaBO, TRecuperacionAutomaticoModuloSistema>(objetoBO,
                    opt => opt.ConfigureMap(MemberList.None));

                //mapea los hijos

                return entidad;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public IEnumerable<RecuperacionAutomaticoModuloSistemaBO> GetFiltered<KProperty>(IEnumerable<Expression<Func<TRecuperacionAutomaticoModuloSistema, bool>>> filters, Expression<Func<TRecuperacionAutomaticoModuloSistema, KProperty>> orderBy, bool ascending)
        {
            IEnumerable<TRecuperacionAutomaticoModuloSistema> listado = base.GetFiltered(filters, orderBy, ascending);
            List<RecuperacionAutomaticoModuloSistemaBO> listadoBO = new List<RecuperacionAutomaticoModuloSistemaBO>();

            foreach (var itemEntidad in listado)
            {
                RecuperacionAutomaticoModuloSistemaBO objetoBO = Mapper.Map<TRecuperacionAutomaticoModuloSistema, RecuperacionAutomaticoModuloSistemaBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }
            return listadoBO;
        }
        #endregion
    }
}
