using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using AutoMapper;
using BSI.Integra.Aplicacion.Marketing.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;

namespace BSI.Integra.Aplicacion.Marketing.Repositorio
{
    public class TipoInteraccionMandrilRepositorio : BaseRepository<TTipoInteraccionMandril, TipoInteraccionMandrilBO>
    {
        #region Metodos Base
        public TipoInteraccionMandrilRepositorio() : base()
        {
        }
        public TipoInteraccionMandrilRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<TipoInteraccionMandrilBO> GetBy(Expression<Func<TTipoInteraccionMandril, bool>> filter)
        {
            IEnumerable<TTipoInteraccionMandril> listado = base.GetBy(filter);
            List<TipoInteraccionMandrilBO> listadoBO = new List<TipoInteraccionMandrilBO>();
            foreach (var itemEntidad in listado)
            {
                TipoInteraccionMandrilBO objetoBO = Mapper.Map<TTipoInteraccionMandril, TipoInteraccionMandrilBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public TipoInteraccionMandrilBO FirstById(int id)
        {
            try
            {
                TTipoInteraccionMandril entidad = base.FirstById(id);
                TipoInteraccionMandrilBO objetoBO = new TipoInteraccionMandrilBO();
                Mapper.Map<TTipoInteraccionMandril, TipoInteraccionMandrilBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public TipoInteraccionMandrilBO FirstBy(Expression<Func<TTipoInteraccionMandril, bool>> filter)
        {
            try
            {
                TTipoInteraccionMandril entidad = base.FirstBy(filter);
                TipoInteraccionMandrilBO objetoBO = Mapper.Map<TTipoInteraccionMandril, TipoInteraccionMandrilBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(TipoInteraccionMandrilBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TTipoInteraccionMandril entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<TipoInteraccionMandrilBO> listadoBO)
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

        public bool Update(TipoInteraccionMandrilBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TTipoInteraccionMandril entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<TipoInteraccionMandrilBO> listadoBO)
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
        private void AsignacionId(TTipoInteraccionMandril entidad, TipoInteraccionMandrilBO objetoBO)
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

        private TTipoInteraccionMandril MapeoEntidad(TipoInteraccionMandrilBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TTipoInteraccionMandril entidad = new TTipoInteraccionMandril();
                entidad = Mapper.Map<TipoInteraccionMandrilBO, TTipoInteraccionMandril>(objetoBO,
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
