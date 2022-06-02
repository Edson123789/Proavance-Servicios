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
    public class CorreoPlantillaPorOcurrenciaActividadRepositorio : BaseRepository<TCorreoPlantillaPorOcurrenciaActividad, CorreoPlantillaPorOcurrenciaActividadBO>
    {
        #region Metodos Base
        public CorreoPlantillaPorOcurrenciaActividadRepositorio() : base()
        {
        }
        public CorreoPlantillaPorOcurrenciaActividadRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<CorreoPlantillaPorOcurrenciaActividadBO> GetBy(Expression<Func<TCorreoPlantillaPorOcurrenciaActividad, bool>> filter)
        {
            IEnumerable<TCorreoPlantillaPorOcurrenciaActividad> listado = base.GetBy(filter);
            List<CorreoPlantillaPorOcurrenciaActividadBO> listadoBO = new List<CorreoPlantillaPorOcurrenciaActividadBO>();
            foreach (var itemEntidad in listado)
            {
                CorreoPlantillaPorOcurrenciaActividadBO objetoBO = Mapper.Map<TCorreoPlantillaPorOcurrenciaActividad, CorreoPlantillaPorOcurrenciaActividadBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public CorreoPlantillaPorOcurrenciaActividadBO FirstById(int id)
        {
            try
            {
                TCorreoPlantillaPorOcurrenciaActividad entidad = base.FirstById(id);
                CorreoPlantillaPorOcurrenciaActividadBO objetoBO = new CorreoPlantillaPorOcurrenciaActividadBO();
                Mapper.Map<TCorreoPlantillaPorOcurrenciaActividad, CorreoPlantillaPorOcurrenciaActividadBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public CorreoPlantillaPorOcurrenciaActividadBO FirstBy(Expression<Func<TCorreoPlantillaPorOcurrenciaActividad, bool>> filter)
        {
            try
            {
                TCorreoPlantillaPorOcurrenciaActividad entidad = base.FirstBy(filter);
                CorreoPlantillaPorOcurrenciaActividadBO objetoBO = Mapper.Map<TCorreoPlantillaPorOcurrenciaActividad, CorreoPlantillaPorOcurrenciaActividadBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(CorreoPlantillaPorOcurrenciaActividadBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TCorreoPlantillaPorOcurrenciaActividad entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<CorreoPlantillaPorOcurrenciaActividadBO> listadoBO)
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

        public bool Update(CorreoPlantillaPorOcurrenciaActividadBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TCorreoPlantillaPorOcurrenciaActividad entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<CorreoPlantillaPorOcurrenciaActividadBO> listadoBO)
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
        private void AsignacionId(TCorreoPlantillaPorOcurrenciaActividad entidad, CorreoPlantillaPorOcurrenciaActividadBO objetoBO)
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

        private TCorreoPlantillaPorOcurrenciaActividad MapeoEntidad(CorreoPlantillaPorOcurrenciaActividadBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TCorreoPlantillaPorOcurrenciaActividad entidad = new TCorreoPlantillaPorOcurrenciaActividad();
                entidad = Mapper.Map<CorreoPlantillaPorOcurrenciaActividadBO, TCorreoPlantillaPorOcurrenciaActividad>(objetoBO,
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

        public List<CorreoPlantillaPorOcurrenciaActividadDTO> ObtenerAsociacionWhatsAppPlantillaPorIdActividadOcurrencia(int IdActividadOcurrencia)
        {
            try
            {
                List<CorreoPlantillaPorOcurrenciaActividadDTO> AsociacionPlantilla = new List<CorreoPlantillaPorOcurrenciaActividadDTO>();
                var _queryAsociacionPlantilla = "Select IdOcurrenciaActividad,IdPlantilla,NumeroDiasSinContacto from com.V_TWhatsAppPlantillaPorOcurrenciaActividad_PorIdOcurrenciaActividad where Estado=1 and IdOcurrenciaActividad=@IdActividadOcurrencia";
                var queryAsociacionPlantilla = _dapper.QueryDapper(_queryAsociacionPlantilla, new { IdActividadOcurrencia });
                if (queryAsociacionPlantilla.Contains("[]"))
                {
                    return AsociacionPlantilla;
                }
                else
                {   
                    AsociacionPlantilla = JsonConvert.DeserializeObject<List<CorreoPlantillaPorOcurrenciaActividadDTO>>(queryAsociacionPlantilla);

                    return AsociacionPlantilla;
                }
                
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public List<CorreoPlantillaPorOcurrenciaActividadDTO> ObtenerAsociacionCorreoPlantillaPorIdActividadOcurrencia(int IdActividadOcurrencia)
        {
            try
            {
                List<CorreoPlantillaPorOcurrenciaActividadDTO> AsociacionPlantilla = new List<CorreoPlantillaPorOcurrenciaActividadDTO>();
                var _queryAsociacionPlantilla = "Select IdOcurrenciaActividad,IdPlantilla,NumeroDiasSinContacto from com.V_TCorreoPlantillaPorOcurrenciaActividad_PorIdOcurrenciaActividad where Estado=1 and IdOcurrenciaActividad=@IdActividadOcurrencia";
                var queryAsociacionPlantilla = _dapper.QueryDapper(_queryAsociacionPlantilla, new { IdActividadOcurrencia });
                if (queryAsociacionPlantilla.Contains("[]"))
                {
                    return AsociacionPlantilla;
                }
                else
                {
                    AsociacionPlantilla = JsonConvert.DeserializeObject<List<CorreoPlantillaPorOcurrenciaActividadDTO>>(queryAsociacionPlantilla);

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
