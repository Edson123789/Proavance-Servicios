using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using AutoMapper;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Marketing.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using Newtonsoft.Json;

namespace BSI.Integra.Aplicacion.Marketing.Repositorio
{
    public class CantidadInteraccionRepositorio : BaseRepository<TCantidadInteraccion, CantidadInteraccionBO>
    {
        #region Metodos Base
        public CantidadInteraccionRepositorio() : base()
        {
        }
        public CantidadInteraccionRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<CantidadInteraccionBO> GetBy(Expression<Func<TCantidadInteraccion, bool>> filter)
        {
            IEnumerable<TCantidadInteraccion> listado = base.GetBy(filter);
            List<CantidadInteraccionBO> listadoBO = new List<CantidadInteraccionBO>();
            foreach (var itemEntidad in listado)
            {
                CantidadInteraccionBO objetoBO = Mapper.Map<TCantidadInteraccion, CantidadInteraccionBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public CantidadInteraccionBO FirstById(int id)
        {
            try
            {
                TCantidadInteraccion entidad = base.FirstById(id);
                CantidadInteraccionBO objetoBO = new CantidadInteraccionBO();
                Mapper.Map<TCantidadInteraccion, CantidadInteraccionBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public CantidadInteraccionBO FirstBy(Expression<Func<TCantidadInteraccion, bool>> filter)
        {
            try
            {
                TCantidadInteraccion entidad = base.FirstBy(filter);
                CantidadInteraccionBO objetoBO = Mapper.Map<TCantidadInteraccion, CantidadInteraccionBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(CantidadInteraccionBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TCantidadInteraccion entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<CantidadInteraccionBO> listadoBO)
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

        public bool Update(CantidadInteraccionBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TCantidadInteraccion entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<CantidadInteraccionBO> listadoBO)
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
        private void AsignacionId(TCantidadInteraccion entidad, CantidadInteraccionBO objetoBO)
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

        private TCantidadInteraccion MapeoEntidad(CantidadInteraccionBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TCantidadInteraccion entidad = new TCantidadInteraccion();
                entidad = Mapper.Map<CantidadInteraccionBO, TCantidadInteraccion>(objetoBO,
                    opt => opt.ConfigureMap(MemberList.None));

                //mapea los hijos

                return entidad;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public IEnumerable<CantidadInteraccionBO> GetFiltered<KProperty>(IEnumerable<Expression<Func<TCantidadInteraccion, bool>>> filters, Expression<Func<TCantidadInteraccion, KProperty>> orderBy, bool ascending)
        {
            IEnumerable<TCantidadInteraccion> listado = base.GetFiltered(filters, orderBy, ascending);
            List<CantidadInteraccionBO> listadoBO = new List<CantidadInteraccionBO>();

            foreach (var itemEntidad in listado)
            {
                CantidadInteraccionBO objetoBO = Mapper.Map<TCantidadInteraccion, CantidadInteraccionBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }
            return listadoBO;
        }
        #endregion
    }
}
