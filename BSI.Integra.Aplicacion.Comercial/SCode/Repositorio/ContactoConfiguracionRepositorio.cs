using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using AutoMapper;
using BSI.Integra.Aplicacion.Comercial.BO;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using Newtonsoft.Json;

namespace BSI.Integra.Aplicacion.Comercial.Repositorio
{
    public class ContactoConfiguracionRepositorio : BaseRepository<TContactoConfiguracion, ContactoConfiguracionBO>
    {
        #region Metodos Base
        public ContactoConfiguracionRepositorio() : base()
        {
        }
        public ContactoConfiguracionRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<ContactoConfiguracionBO> GetBy(Expression<Func<TContactoConfiguracion, bool>> filter)
        {
            IEnumerable<TContactoConfiguracion> listado = base.GetBy(filter);
            List<ContactoConfiguracionBO> listadoBO = new List<ContactoConfiguracionBO>();
            foreach (var itemEntidad in listado)
            {
                ContactoConfiguracionBO objetoBO = Mapper.Map<TContactoConfiguracion, ContactoConfiguracionBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public ContactoConfiguracionBO FirstById(int id)
        {
            try
            {
                TContactoConfiguracion entidad = base.FirstById(id);
                ContactoConfiguracionBO objetoBO = new ContactoConfiguracionBO();
                Mapper.Map<TContactoConfiguracion, ContactoConfiguracionBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public ContactoConfiguracionBO FirstBy(Expression<Func<TContactoConfiguracion, bool>> filter)
        {
            try
            {
                TContactoConfiguracion entidad = base.FirstBy(filter);
                ContactoConfiguracionBO objetoBO = Mapper.Map<TContactoConfiguracion, ContactoConfiguracionBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(ContactoConfiguracionBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TContactoConfiguracion entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<ContactoConfiguracionBO> listadoBO)
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

        public bool Update(ContactoConfiguracionBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TContactoConfiguracion entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<ContactoConfiguracionBO> listadoBO)
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
        private void AsignacionId(TContactoConfiguracion entidad, ContactoConfiguracionBO objetoBO)
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

        private TContactoConfiguracion MapeoEntidad(ContactoConfiguracionBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TContactoConfiguracion entidad = new TContactoConfiguracion();
                entidad = Mapper.Map<ContactoConfiguracionBO, TContactoConfiguracion>(objetoBO,
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

        /// <summary>
        /// Obtiene el registro de la tabla de acuerdo al IdTipoDato.
        /// </summary>
        /// <param name="idTipoDato"></param>
        /// <returns></returns>
        public ContactoConfiguracionDTO GetConfiguracionContactos(int idTipoDato)
        {
            try
            {
                string query = "SELECT IdTipoDato, DescripcionTipoDato, IdOrigen, NombreOrigen, IdFaseOportunidad, CodigoFaseOportunidad FROM com.V_ObtenerDescripcionContactoConfiguracion WHERE Estado = 1 AND IdTipoDato = @idTipoDato";
                string queryRespuesta = _dapper.FirstOrDefault(query, new { IdTipoDato = idTipoDato });
                if (queryRespuesta != "null")
                {
                    return JsonConvert.DeserializeObject<ContactoConfiguracionDTO>(queryRespuesta);
                }
                return null;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
