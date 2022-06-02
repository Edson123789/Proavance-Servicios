using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using AutoMapper;
using BSI.Integra.Aplicacion.Marketing.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;

namespace BSI.Integra.Aplicacion.Marketing.Repositorio
{
    public class EmbudoPreRepositorio : BaseRepository<TEmbudoPre, EmbudoPreBO>
    {
        #region Metodos Base
        public EmbudoPreRepositorio() : base()
        {
        }
        public EmbudoPreRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<EmbudoPreBO> GetBy(Expression<Func<TEmbudoPre, bool>> filter)
        {
            IEnumerable<TEmbudoPre> listado = base.GetBy(filter);
            List<EmbudoPreBO> listadoBO = new List<EmbudoPreBO>();
            foreach (var itemEntidad in listado)
            {
                EmbudoPreBO objetoBO = Mapper.Map<TEmbudoPre, EmbudoPreBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public EmbudoPreBO FirstById(int id)
        {
            try
            {
                TEmbudoPre entidad = base.FirstById(id);
                EmbudoPreBO objetoBO = new EmbudoPreBO();
                Mapper.Map<TEmbudoPre, EmbudoPreBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public EmbudoPreBO FirstBy(Expression<Func<TEmbudoPre, bool>> filter)
        {
            try
            {
                TEmbudoPre entidad = base.FirstBy(filter);
                EmbudoPreBO objetoBO = Mapper.Map<TEmbudoPre, EmbudoPreBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(EmbudoPreBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TEmbudoPre entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<EmbudoPreBO> listadoBO)
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

        public bool Update(EmbudoPreBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TEmbudoPre entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<EmbudoPreBO> listadoBO)
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
        private void AsignacionId(TEmbudoPre entidad, EmbudoPreBO objetoBO)
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

        private TEmbudoPre MapeoEntidad(EmbudoPreBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TEmbudoPre entidad = new TEmbudoPre();
                entidad = Mapper.Map<EmbudoPreBO, TEmbudoPre>(objetoBO,
                    opt => opt.ConfigureMap(MemberList.None));

                //mapea los hijos

                return entidad;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public IEnumerable<EmbudoPreBO> GetFiltered<KProperty>(IEnumerable<Expression<Func<TEmbudoPre, bool>>> filters, Expression<Func<TEmbudoPre, KProperty>> orderBy, bool ascending)
        {
            IEnumerable<TEmbudoPre> listado = base.GetFiltered(filters, orderBy, ascending);
            List<EmbudoPreBO> listadoBO = new List<EmbudoPreBO>();

            foreach (var itemEntidad in listado)
            {
                EmbudoPreBO objetoBO = Mapper.Map<TEmbudoPre, EmbudoPreBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }
            return listadoBO;
        }
        #endregion
    }
}
