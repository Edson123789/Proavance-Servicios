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
using Newtonsoft.Json;

namespace BSI.Integra.Aplicacion.Transversal.Repositorio
{
    public class WhatsAppConfiguracionEnvioPorProgramaRepositorio : BaseRepository<TWhatsAppConfiguracionEnvioPorPrograma, WhatsAppConfiguracionEnvioPorProgramaBO>
    {
        #region Metodos Base
        public WhatsAppConfiguracionEnvioPorProgramaRepositorio() : base()
        {
        }
        public WhatsAppConfiguracionEnvioPorProgramaRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<WhatsAppConfiguracionEnvioPorProgramaBO> GetBy(Expression<Func<TWhatsAppConfiguracionEnvioPorPrograma, bool>> filter)
        {
            IEnumerable<TWhatsAppConfiguracionEnvioPorPrograma> listado = base.GetBy(filter);
            List<WhatsAppConfiguracionEnvioPorProgramaBO> listadoBO = new List<WhatsAppConfiguracionEnvioPorProgramaBO>();
            foreach (var itemEntidad in listado)
            {
                WhatsAppConfiguracionEnvioPorProgramaBO objetoBO = Mapper.Map<TWhatsAppConfiguracionEnvioPorPrograma, WhatsAppConfiguracionEnvioPorProgramaBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public WhatsAppConfiguracionEnvioPorProgramaBO FirstById(int id)
        {
            try
            {
                TWhatsAppConfiguracionEnvioPorPrograma entidad = base.FirstById(id);
                WhatsAppConfiguracionEnvioPorProgramaBO objetoBO = new WhatsAppConfiguracionEnvioPorProgramaBO();
                Mapper.Map<TWhatsAppConfiguracionEnvioPorPrograma, WhatsAppConfiguracionEnvioPorProgramaBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public WhatsAppConfiguracionEnvioPorProgramaBO FirstBy(Expression<Func<TWhatsAppConfiguracionEnvioPorPrograma, bool>> filter)
        {
            try
            {
                TWhatsAppConfiguracionEnvioPorPrograma entidad = base.FirstBy(filter);
                WhatsAppConfiguracionEnvioPorProgramaBO objetoBO = Mapper.Map<TWhatsAppConfiguracionEnvioPorPrograma, WhatsAppConfiguracionEnvioPorProgramaBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(WhatsAppConfiguracionEnvioPorProgramaBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TWhatsAppConfiguracionEnvioPorPrograma entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<WhatsAppConfiguracionEnvioPorProgramaBO> listadoBO)
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

        public bool Update(WhatsAppConfiguracionEnvioPorProgramaBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TWhatsAppConfiguracionEnvioPorPrograma entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<WhatsAppConfiguracionEnvioPorProgramaBO> listadoBO)
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
        private void AsignacionId(TWhatsAppConfiguracionEnvioPorPrograma entidad, WhatsAppConfiguracionEnvioPorProgramaBO objetoBO)
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

        private TWhatsAppConfiguracionEnvioPorPrograma MapeoEntidad(WhatsAppConfiguracionEnvioPorProgramaBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TWhatsAppConfiguracionEnvioPorPrograma entidad = new TWhatsAppConfiguracionEnvioPorPrograma();
                entidad = Mapper.Map<WhatsAppConfiguracionEnvioPorProgramaBO, TWhatsAppConfiguracionEnvioPorPrograma>(objetoBO,
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
