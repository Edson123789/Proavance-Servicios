using AutoMapper;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.GestionPersonas.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace BSI.Integra.Aplicacion.GestionPersonas.Repositorio
{
    public class EvaluacionPersonaRepositorio : BaseRepository<TEvaluacionPersona, EvaluacionPersonaBO>
    {
        #region Metodos Base
        public EvaluacionPersonaRepositorio() : base()
        {
        }
        public EvaluacionPersonaRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<EvaluacionPersonaBO> GetBy(Expression<Func<TEvaluacionPersona, bool>> filter)
        {
            IEnumerable<TEvaluacionPersona> listado = base.GetBy(filter);
            List<EvaluacionPersonaBO> listadoBO = new List<EvaluacionPersonaBO>();
            foreach (var itemEntidad in listado)
            {
                EvaluacionPersonaBO objetoBO = Mapper.Map<TEvaluacionPersona, EvaluacionPersonaBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public EvaluacionPersonaBO FirstById(int id)
        {
            try
            {
                TEvaluacionPersona entidad = base.FirstById(id);
                EvaluacionPersonaBO objetoBO = new EvaluacionPersonaBO();
                Mapper.Map<TEvaluacionPersona, EvaluacionPersonaBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public EvaluacionPersonaBO FirstBy(Expression<Func<TEvaluacionPersona, bool>> filter)
        {
            try
            {
                TEvaluacionPersona entidad = base.FirstBy(filter);
                EvaluacionPersonaBO objetoBO = Mapper.Map<TEvaluacionPersona, EvaluacionPersonaBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(EvaluacionPersonaBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TEvaluacionPersona entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<EvaluacionPersonaBO> listadoBO)
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

        public bool Update(EvaluacionPersonaBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TEvaluacionPersona entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<EvaluacionPersonaBO> listadoBO)
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
        private void AsignacionId(TEvaluacionPersona entidad, EvaluacionPersonaBO objetoBO)
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

        private TEvaluacionPersona MapeoEntidad(EvaluacionPersonaBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TEvaluacionPersona entidad = new TEvaluacionPersona();
                entidad = Mapper.Map<EvaluacionPersonaBO, TEvaluacionPersona>(objetoBO,
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

        public List<EvaluacionPersonaCompletoDTO> ObtenerEvaluacionPersonaCompleto()
        {
            try
            {
                List<EvaluacionPersonaCompletoDTO> EvaluacionPersona = new List<EvaluacionPersonaCompletoDTO>();
                var campos = "IdEvaluacionPersona,NombreEvaluacion,TituloEvaluacion,CronometrarExamen,TiempoLimiteExamen,IdEvaluacionConfiguracionFormato,ColorTextoTitulo,TamanioTextoTitulo" +
                    ",ColorFondoTitulo,TipoLetraTitulo,ColorTextoEnunciado,TamanioTextoEnunciado,ColorFondoEnunciado,TipoLetraEnunciado,ColorTextoRespuesta,TamanioTextoRespuesta,ColorFondoRespuesta" +
                    ",TipoLetraRespuesta,IdEvaluacionComportamiento,ResponderTodasLasPreguntas,IdEvaluacionFeedBackAprobado,IdEvaluacionFeedBackDesaprobado,IdEvaluacionFeedBackCancelado,IdEvaluacionResultado" +
                    ",CalificarExamen,PuntajeExamen,PuntajeAprobacion,MostrarResultado,MostrarPuntajeTotal,MostrarPuntajePregunta,IdPGeneral,IdPEspecifico,IdPEspecificoSesion,UsuarioModificacion ";
                var _query = "SELECT " +campos+ " FROM  gp.V_ObtenerEvaluacionPersona ";
                var dataDB = _dapper.QueryDapper(_query, null);
                if (!dataDB.Contains("[]") && !string.IsNullOrEmpty(dataDB))
                {
                    EvaluacionPersona = JsonConvert.DeserializeObject<List<EvaluacionPersonaCompletoDTO>>(dataDB);
                }
                return EvaluacionPersona;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

    }
}
