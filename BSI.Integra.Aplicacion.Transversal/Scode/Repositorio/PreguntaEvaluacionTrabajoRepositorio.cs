using AutoMapper;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace BSI.Integra.Aplicacion.Transversal.Repositorio
{
    public class PreguntaEvaluacionTrabajoRepositorio : BaseRepository<TPreguntaEvaluacionTrabajo, PreguntaEvaluacionTrabajoBO>
    {
        #region Metodos Base
        public PreguntaEvaluacionTrabajoRepositorio() : base()
        {
        }
        public PreguntaEvaluacionTrabajoRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<PreguntaEvaluacionTrabajoBO> GetBy(Expression<Func<TPreguntaEvaluacionTrabajo, bool>> filter)
        {
            IEnumerable<TPreguntaEvaluacionTrabajo> listado = base.GetBy(filter);
            List<PreguntaEvaluacionTrabajoBO> listadoBO = new List<PreguntaEvaluacionTrabajoBO>();
            foreach (var itemEntidad in listado)
            {
                PreguntaEvaluacionTrabajoBO objetoBO = Mapper.Map<TPreguntaEvaluacionTrabajo, PreguntaEvaluacionTrabajoBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public PreguntaEvaluacionTrabajoBO FirstById(int id)
        {
            try
            {
                TPreguntaEvaluacionTrabajo entidad = base.FirstById(id);
                PreguntaEvaluacionTrabajoBO objetoBO = new PreguntaEvaluacionTrabajoBO();
                Mapper.Map<TPreguntaEvaluacionTrabajo, PreguntaEvaluacionTrabajoBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public PreguntaEvaluacionTrabajoBO FirstBy(Expression<Func<TPreguntaEvaluacionTrabajo, bool>> filter)
        {
            try
            {
                TPreguntaEvaluacionTrabajo entidad = base.FirstBy(filter);
                PreguntaEvaluacionTrabajoBO objetoBO = Mapper.Map<TPreguntaEvaluacionTrabajo, PreguntaEvaluacionTrabajoBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(PreguntaEvaluacionTrabajoBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TPreguntaEvaluacionTrabajo entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<PreguntaEvaluacionTrabajoBO> listadoBO)
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

        public bool Update(PreguntaEvaluacionTrabajoBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TPreguntaEvaluacionTrabajo entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<PreguntaEvaluacionTrabajoBO> listadoBO)
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
        private void AsignacionId(TPreguntaEvaluacionTrabajo entidad, PreguntaEvaluacionTrabajoBO objetoBO)
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

        private TPreguntaEvaluacionTrabajo MapeoEntidad(PreguntaEvaluacionTrabajoBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TPreguntaEvaluacionTrabajo entidad = new TPreguntaEvaluacionTrabajo();
                entidad = Mapper.Map<PreguntaEvaluacionTrabajoBO, TPreguntaEvaluacionTrabajo>(objetoBO,
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

        public List<registroPreguntaEvaluacionTrabajoBO> ObtenerPreguntaEvaluacionTrabajoPorConfiguacion(int IdConfigurarEvaluacionTrabajo)
        {
            try
            {
                List<registroPreguntaEvaluacionTrabajoBO> capitulosFiltro = new List<registroPreguntaEvaluacionTrabajoBO>();
                //Select IdPGeneral, Nombre, Titulo, Contenido, NombreTitulo, NroCapitulo From pla.V_ListadoEstructuraProgramaCapitulosV2 Where IdPGeneral = @IdPGeneral
                var _queryfiltrocapitulo = "Select IdConfigurarEvaluacionTrabajo,IdPregunta FROM pla.T_PreguntaEvaluacionTrabajo Where Estado=1 AND IdConfigurarEvaluacionTrabajo=@IdConfigurarEvaluacionTrabajo";
                var SubfiltroCapitulo = _dapper.QueryDapper(_queryfiltrocapitulo, new { IdConfigurarEvaluacionTrabajo = IdConfigurarEvaluacionTrabajo });
                if (!string.IsNullOrEmpty(SubfiltroCapitulo) && !SubfiltroCapitulo.Contains("[]"))
                {
                    capitulosFiltro = JsonConvert.DeserializeObject<List<registroPreguntaEvaluacionTrabajoBO>>(SubfiltroCapitulo);
                }
                return capitulosFiltro;
            }
            catch (Exception E)
            {
                throw new Exception(E.Message);
            }

        }
    }
}
