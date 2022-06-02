using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using AutoMapper;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;

namespace BSI.Integra.Aplicacion.Transversal.Repositorio
{
    public class EstadoContactoWhatsAppRepositorio : BaseRepository<TEstadoContactoWhatsApp, EstadoContactoWhatsAppBO>
    {
        #region Metodos Base
        public EstadoContactoWhatsAppRepositorio() : base()
        {
        }
        public EstadoContactoWhatsAppRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<EstadoContactoWhatsAppBO> GetBy(Expression<Func<TEstadoContactoWhatsApp, bool>> filter)
        {
            IEnumerable<TEstadoContactoWhatsApp> listado = base.GetBy(filter);
            List<EstadoContactoWhatsAppBO> listadoBO = new List<EstadoContactoWhatsAppBO>();
            foreach (var itemEntidad in listado)
            {
                EstadoContactoWhatsAppBO objetoBO = Mapper.Map<TEstadoContactoWhatsApp, EstadoContactoWhatsAppBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public EstadoContactoWhatsAppBO FirstById(int id)
        {
            try
            {
                TEstadoContactoWhatsApp entidad = base.FirstById(id);
                EstadoContactoWhatsAppBO objetoBO = new EstadoContactoWhatsAppBO();
                Mapper.Map<TEstadoContactoWhatsApp, EstadoContactoWhatsAppBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public EstadoContactoWhatsAppBO FirstBy(Expression<Func<TEstadoContactoWhatsApp, bool>> filter)
        {
            try
            {
                TEstadoContactoWhatsApp entidad = base.FirstBy(filter);
                EstadoContactoWhatsAppBO objetoBO = Mapper.Map<TEstadoContactoWhatsApp, EstadoContactoWhatsAppBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(EstadoContactoWhatsAppBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TEstadoContactoWhatsApp entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<EstadoContactoWhatsAppBO> listadoBO)
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

        public bool Update(EstadoContactoWhatsAppBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TEstadoContactoWhatsApp entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<EstadoContactoWhatsAppBO> listadoBO)
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
        private void AsignacionId(TEstadoContactoWhatsApp entidad, EstadoContactoWhatsAppBO objetoBO)
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

        private TEstadoContactoWhatsApp MapeoEntidad(EstadoContactoWhatsAppBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TEstadoContactoWhatsApp entidad = new TEstadoContactoWhatsApp();
                entidad = Mapper.Map<EstadoContactoWhatsAppBO, TEstadoContactoWhatsApp>(objetoBO,
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
