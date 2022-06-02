using System;
using System.Collections.Generic;
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
    public class WhatsAppPlantillaPorOcurrenciaActividadRepositorio : BaseRepository<TWhatsAppPlantillaPorOcurrenciaActividad, WhatsAppPlantillaPorOcurrenciaActividadBO>
    {
        #region Metodos Base
        public WhatsAppPlantillaPorOcurrenciaActividadRepositorio() : base()
        {
        }
        public WhatsAppPlantillaPorOcurrenciaActividadRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<WhatsAppPlantillaPorOcurrenciaActividadBO> GetBy(Expression<Func<TWhatsAppPlantillaPorOcurrenciaActividad, bool>> filter)
        {
            IEnumerable<TWhatsAppPlantillaPorOcurrenciaActividad> listado = base.GetBy(filter);
            List<WhatsAppPlantillaPorOcurrenciaActividadBO> listadoBO = new List<WhatsAppPlantillaPorOcurrenciaActividadBO>();
            foreach (var itemEntidad in listado)
            {
                WhatsAppPlantillaPorOcurrenciaActividadBO objetoBO = Mapper.Map<TWhatsAppPlantillaPorOcurrenciaActividad, WhatsAppPlantillaPorOcurrenciaActividadBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public WhatsAppPlantillaPorOcurrenciaActividadBO FirstById(int id)
        {
            try
            {
                TWhatsAppPlantillaPorOcurrenciaActividad entidad = base.FirstById(id);
                WhatsAppPlantillaPorOcurrenciaActividadBO objetoBO = new WhatsAppPlantillaPorOcurrenciaActividadBO();
                Mapper.Map<TWhatsAppPlantillaPorOcurrenciaActividad, WhatsAppPlantillaPorOcurrenciaActividadBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public WhatsAppPlantillaPorOcurrenciaActividadBO FirstBy(Expression<Func<TWhatsAppPlantillaPorOcurrenciaActividad, bool>> filter)
        {
            try
            {
                TWhatsAppPlantillaPorOcurrenciaActividad entidad = base.FirstBy(filter);
                WhatsAppPlantillaPorOcurrenciaActividadBO objetoBO = Mapper.Map<TWhatsAppPlantillaPorOcurrenciaActividad, WhatsAppPlantillaPorOcurrenciaActividadBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(WhatsAppPlantillaPorOcurrenciaActividadBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TWhatsAppPlantillaPorOcurrenciaActividad entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<WhatsAppPlantillaPorOcurrenciaActividadBO> listadoBO)
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

        public bool Update(WhatsAppPlantillaPorOcurrenciaActividadBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TWhatsAppPlantillaPorOcurrenciaActividad entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<WhatsAppPlantillaPorOcurrenciaActividadBO> listadoBO)
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
        private void AsignacionId(TWhatsAppPlantillaPorOcurrenciaActividad entidad, WhatsAppPlantillaPorOcurrenciaActividadBO objetoBO)
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

        private TWhatsAppPlantillaPorOcurrenciaActividad MapeoEntidad(WhatsAppPlantillaPorOcurrenciaActividadBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TWhatsAppPlantillaPorOcurrenciaActividad entidad = new TWhatsAppPlantillaPorOcurrenciaActividad();
                entidad = Mapper.Map<WhatsAppPlantillaPorOcurrenciaActividadBO, TWhatsAppPlantillaPorOcurrenciaActividad>(objetoBO,
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

        public List<WhatsAppPlantillaPorOcurrenciaActividadDTO> ObtenerAsociacionWhatsAppPlantillaPorIdActividadOcurrencia(int IdActividadOcurrencia)
        {
            try
            {
                List<WhatsAppPlantillaPorOcurrenciaActividadDTO> AsociacionPlantilla = new List<WhatsAppPlantillaPorOcurrenciaActividadDTO>();
                var _queryAsociacionPlantilla = "Select IdOcurrenciaActividad,IdPlantilla,NumeroDiasSinContacto from com.V_TWhatsAppPlantillaPorOcurrenciaActividad_PorIdOcurrenciaActividad where Estado=1 and IdOcurrenciaActividad=@IdActividadOcurrencia";
                var queryAsociacionPlantilla = _dapper.QueryDapper(_queryAsociacionPlantilla, new { IdActividadOcurrencia });
                if (queryAsociacionPlantilla.Contains("[]"))
                {
                    return AsociacionPlantilla;
                }
                else
                {   
                    AsociacionPlantilla = JsonConvert.DeserializeObject<List<WhatsAppPlantillaPorOcurrenciaActividadDTO>>(queryAsociacionPlantilla);

                    return AsociacionPlantilla;
                }
                
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public List<WhatsAppPlantillaPorOcurrenciaActividadDTO> ObtenerAsociacionCorreoPlantillaPorIdActividadOcurrencia(int IdActividadOcurrencia)
        {
            try
            {
                List<WhatsAppPlantillaPorOcurrenciaActividadDTO> AsociacionPlantilla = new List<WhatsAppPlantillaPorOcurrenciaActividadDTO>();
                var _queryAsociacionPlantilla = "Select IdOcurrenciaActividad,IdPlantilla,NumeroDiasSinContacto from com.V_TCorreoPlantillaPorOcurrenciaActividad_PorIdOcurrenciaActividad where Estado=1 and IdOcurrenciaActividad=@IdActividadOcurrencia";
                var queryAsociacionPlantilla = _dapper.QueryDapper(_queryAsociacionPlantilla, new { IdActividadOcurrencia });
                if (queryAsociacionPlantilla.Contains("[]"))
                {
                    return AsociacionPlantilla;
                }
                else
                {
                    AsociacionPlantilla = JsonConvert.DeserializeObject<List<WhatsAppPlantillaPorOcurrenciaActividadDTO>>(queryAsociacionPlantilla);

                    return AsociacionPlantilla;
                }

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
