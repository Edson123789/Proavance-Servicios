using AutoMapper;
using BSI.Integra.Aplicacion.Marketing.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace BSI.Integra.Aplicacion.Marketing.Repositorio
{
    public class EjecucionEstadoWhatsAppLogRepositorio : BaseRepository<TEjecucionEstadoWhatsAppLog, EjecucionEstadoWhatsAppLogBO>
    {
        #region Metodos Base
        public EjecucionEstadoWhatsAppLogRepositorio() : base()
        {
        }
        public EjecucionEstadoWhatsAppLogRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<EjecucionEstadoWhatsAppLogBO> GetBy(Expression<Func<TEjecucionEstadoWhatsAppLog, bool>> filter)
        {
            IEnumerable<TEjecucionEstadoWhatsAppLog> listado = base.GetBy(filter);
            List<EjecucionEstadoWhatsAppLogBO> listadoBO = new List<EjecucionEstadoWhatsAppLogBO>();
            foreach (var itemEntidad in listado)
            {
                EjecucionEstadoWhatsAppLogBO objetoBO = Mapper.Map<TEjecucionEstadoWhatsAppLog, EjecucionEstadoWhatsAppLogBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public EjecucionEstadoWhatsAppLogBO FirstById(int id)
        {
            try
            {
                TEjecucionEstadoWhatsAppLog entidad = base.FirstById(id);
                EjecucionEstadoWhatsAppLogBO objetoBO = new EjecucionEstadoWhatsAppLogBO();
                Mapper.Map<TEjecucionEstadoWhatsAppLog, EjecucionEstadoWhatsAppLogBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public EjecucionEstadoWhatsAppLogBO FirstBy(Expression<Func<TEjecucionEstadoWhatsAppLog, bool>> filter)
        {
            try
            {
                TEjecucionEstadoWhatsAppLog entidad = base.FirstBy(filter);
                EjecucionEstadoWhatsAppLogBO objetoBO = Mapper.Map<TEjecucionEstadoWhatsAppLog, EjecucionEstadoWhatsAppLogBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(EjecucionEstadoWhatsAppLogBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TEjecucionEstadoWhatsAppLog entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<EjecucionEstadoWhatsAppLogBO> listadoBO)
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

        public bool Update(EjecucionEstadoWhatsAppLogBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TEjecucionEstadoWhatsAppLog entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<EjecucionEstadoWhatsAppLogBO> listadoBO)
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
        private void AsignacionId(TEjecucionEstadoWhatsAppLog entidad, EjecucionEstadoWhatsAppLogBO objetoBO)
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

        private TEjecucionEstadoWhatsAppLog MapeoEntidad(EjecucionEstadoWhatsAppLogBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TEjecucionEstadoWhatsAppLog entidad = new TEjecucionEstadoWhatsAppLog();
                entidad = Mapper.Map<EjecucionEstadoWhatsAppLogBO, TEjecucionEstadoWhatsAppLog>(objetoBO,
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
