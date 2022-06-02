using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using AutoMapper;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using BSI.Integra.Aplicacion.DTOs;
using Newtonsoft.Json;
using System.Linq;

namespace BSI.Integra.Aplicacion.Transversal.Repositorio
{
    public class PreguntaFrecuenteRepositorio : BaseRepository<TPreguntaFrecuente, PreguntaFrecuenteBO>
    {
        #region Metodos Base
        public PreguntaFrecuenteRepositorio() : base()
        {
        }
        public PreguntaFrecuenteRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<PreguntaFrecuenteBO> GetBy(Expression<Func<TPreguntaFrecuente, bool>> filter)
        {
            IEnumerable<TPreguntaFrecuente> listado = base.GetBy(filter);
            List<PreguntaFrecuenteBO> listadoBO = new List<PreguntaFrecuenteBO>();
            foreach (var itemEntidad in listado)
            {
                PreguntaFrecuenteBO objetoBO = Mapper.Map<TPreguntaFrecuente, PreguntaFrecuenteBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public PreguntaFrecuenteBO FirstById(int id)
        {
            try
            {
                TPreguntaFrecuente entidad = base.FirstById(id);
                PreguntaFrecuenteBO objetoBO = new PreguntaFrecuenteBO();
                Mapper.Map<TPreguntaFrecuente, PreguntaFrecuenteBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public PreguntaFrecuenteBO FirstBy(Expression<Func<TPreguntaFrecuente, bool>> filter)
        {
            try
            {
                TPreguntaFrecuente entidad = base.FirstBy(filter);
                PreguntaFrecuenteBO objetoBO = Mapper.Map<TPreguntaFrecuente, PreguntaFrecuenteBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(PreguntaFrecuenteBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TPreguntaFrecuente entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<PreguntaFrecuenteBO> listadoBO)
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

        public bool Update(PreguntaFrecuenteBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TPreguntaFrecuente entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<PreguntaFrecuenteBO> listadoBO)
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
        private void AsignacionId(TPreguntaFrecuente entidad, PreguntaFrecuenteBO objetoBO)
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

        private TPreguntaFrecuente MapeoEntidad(PreguntaFrecuenteBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TPreguntaFrecuente entidad = new TPreguntaFrecuente();
                entidad = Mapper.Map<PreguntaFrecuenteBO, TPreguntaFrecuente>(objetoBO,
                    opt => opt.ConfigureMap(MemberList.None));

                //mapea los hijos

                if (objetoBO.PreguntaFrecuentePgeneral != null && objetoBO.PreguntaFrecuentePgeneral.Count > 0)
                {
                    foreach (var hijo in objetoBO.PreguntaFrecuentePgeneral)
                    {
                        TPreguntaFrecuentePgeneral entidadHijo = new TPreguntaFrecuentePgeneral();
                        entidadHijo = Mapper.Map<PreguntaFrecuentePgeneralBO, TPreguntaFrecuentePgeneral>(hijo,
                            opt => opt.ConfigureMap(MemberList.None));
                        entidad.TPreguntaFrecuentePgeneral.Add(entidadHijo);
                    }
                }

                if (objetoBO.PreguntaFrecuenteArea != null && objetoBO.PreguntaFrecuenteArea.Count > 0)
                {
                    foreach (var hijo in objetoBO.PreguntaFrecuenteArea)
                    {
                        TPreguntaFrecuenteArea entidadHijo = new TPreguntaFrecuenteArea();
                        entidadHijo = Mapper.Map<PreguntaFrecuenteAreaBO, TPreguntaFrecuenteArea>(hijo,
                            opt => opt.ConfigureMap(MemberList.None));
                        entidad.TPreguntaFrecuenteArea.Add(entidadHijo);
                    }
                }

                if (objetoBO.PreguntaFrecuenteSubArea != null && objetoBO.PreguntaFrecuenteSubArea.Count > 0)
                {
                    foreach (var hijo in objetoBO.PreguntaFrecuenteSubArea)
                    {
                        TPreguntaFrecuenteSubArea entidadHijo = new TPreguntaFrecuenteSubArea();
                        entidadHijo = Mapper.Map<PreguntaFrecuenteSubAreaBO, TPreguntaFrecuenteSubArea>(hijo,
                            opt => opt.ConfigureMap(MemberList.None));
                        entidad.TPreguntaFrecuenteSubArea.Add(entidadHijo);
                    }
                }

                if (objetoBO.PreguntaFrecuenteTipo != null && objetoBO.PreguntaFrecuenteTipo.Count > 0)
                {
                    foreach (var hijo in objetoBO.PreguntaFrecuenteTipo)
                    {
                        TPreguntaFrecuenteTipo entidadHijo = new TPreguntaFrecuenteTipo();
                        entidadHijo = Mapper.Map<PreguntaFrecuenteTipoBO, TPreguntaFrecuenteTipo>(hijo,
                            opt => opt.ConfigureMap(MemberList.None));
                        entidad.TPreguntaFrecuenteTipo.Add(entidadHijo);
                    }
                }

                return entidad;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        #endregion

        /// <summary>
        /// Obtiene todos los registros sin los campos de auditoría.
        /// </summary>
        /// <returns></returns>
        public List<PreguntaFrecuenteDTO> ObtenerTodoGrid()
        {
            try
            {
                var lista = GetBy(x => true, y => new PreguntaFrecuenteDTO
                {
                    Id = y.Id,
                    IdSeccionPreguntaFrecuente = y.IdSeccionPreguntaFrecuente,
                    Pregunta = y.Pregunta,
                    Respuesta = y.Respuesta,
                    Tipo = y.Tipo,
                }).OrderByDescending(x => x.Id).ToList();

                return lista;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Obtiene los registros en una lista segun los filtro que pueden ser AreaCapacitacion, SubAreaCapacitacion, ProgramaGeneral o ModalidadCurso
        /// </summary>
        /// <returns></returns>
        public List<PreguntaFrecuenteFiltroPaginacionDTO> ObtenerListaPreguntaFrecuente(PreguntaFrecuenteFiltroDTO filtro)
        {
            try
            {
                var listaPaginada = ObtenerPreguntaFrecuentePaginado(filtro);
                List<PreguntaFrecuenteFiltroPaginacionDTO> listaRespuesta = new List<PreguntaFrecuenteFiltroPaginacionDTO>();
                PreguntaFrecuenteAreaRepositorio area = new PreguntaFrecuenteAreaRepositorio();
                PreguntaFrecuenteSubAreaRepositorio subArea = new PreguntaFrecuenteSubAreaRepositorio();
                PreguntaFrecuentePgeneralRepositorio PGeneral = new PreguntaFrecuentePgeneralRepositorio();
                PreguntaFrecuenteTipoRepositorio tipo = new PreguntaFrecuenteTipoRepositorio();

                var listaArea = area.ObtenerTodoGrid();
                var listaSubArea = subArea.ObtenerTodoGrid();
                var listaPgeneral = PGeneral.ObtenerTodoGrid();
                var listaTipo = tipo.ObtenerTodoGrid();

                foreach (var item in listaPaginada)
                {
                    PreguntaFrecuenteFiltroPaginacionDTO preguntaFrecuente = new PreguntaFrecuenteFiltroPaginacionDTO();

                    preguntaFrecuente.Id = item.Id;
                    preguntaFrecuente.IdSeccion = item.IdSeccion;
                    preguntaFrecuente.total = item.total;
                    preguntaFrecuente.rowIndex = item.rowIndex;
                    preguntaFrecuente.Pregunta = item.Pregunta;
                    preguntaFrecuente.Respuesta = item.Respuesta;
                    preguntaFrecuente.IdSeccion = item.IdSeccion;
                    preguntaFrecuente.NombreSeccion = item.NombreSeccion;

                    preguntaFrecuente.listaAreas = new List<int>();
                    preguntaFrecuente.listaAreas = listaArea.Where(o => o.IdPreguntaFrecuente.Equals(preguntaFrecuente.Id)).Select(i => i.IdArea).ToList();

                    if (preguntaFrecuente.listaAreas.Contains(0)) 
                    {
                        preguntaFrecuente.listaAreas.Remove(0);
                    }

                    preguntaFrecuente.listaSubAreas = new List<int>();
                    preguntaFrecuente.listaSubAreas = listaSubArea.Where(o => o.IdPreguntaFrecuente.Equals(preguntaFrecuente.Id)).Select(i => i.IdSubArea).ToList();

                    if (preguntaFrecuente.listaSubAreas.Contains(0))
                    {
                        preguntaFrecuente.listaSubAreas.Remove(0);
                    }

                    preguntaFrecuente.listaPGenerales = new List<int?>();
                    preguntaFrecuente.listaPGenerales = listaPgeneral.Where(o => o.IdPreguntaFrecuente.Equals(preguntaFrecuente.Id)).Select(i => i.IdPGeneral).ToList();

                    if (preguntaFrecuente.listaPGenerales.Contains(null))
                    {
                        preguntaFrecuente.listaPGenerales.Remove(null);
                    }

                    preguntaFrecuente.listaTipos = new List<int>();
                    preguntaFrecuente.listaTipos = listaTipo.Where(o => o.IdPreguntaFrecuente.Equals(preguntaFrecuente.Id)).Select(i => i.IdTipo).ToList();

                    if (preguntaFrecuente.listaTipos.Contains(3))
                    {
                        preguntaFrecuente.listaTipos.Remove(3);
                    }

                    listaRespuesta.Add(preguntaFrecuente);
                }

                return listaRespuesta;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }


        /// <summary>
        /// Obtiene los registros en una lista segun los filtro que pueden ser AreaCapacitacion, SubAreaCapacitacion, ProgramaGeneral o ModalidadCurso
        /// </summary>
        /// <returns></returns>
        public List<PreguntaFrecuenteFiltroPaginacionDTO> ObtenerPreguntaFrecuentePaginado(PreguntaFrecuenteFiltroDTO filtro)
        {

            try
            {
                List <PreguntaFrecuenteFiltroPaginacionDTO> preguntaFrecuenteFiltroPaginacion = new List<PreguntaFrecuenteFiltroPaginacionDTO>();

                if (filtro != null)
                {
                    var _query = "SELECT Id, IdSeccionPreguntaFrecuente, Pregunta, Respuesta, total Tipo FROM pla.V_ObtenerDatosPorIdPreguntaFrecuente WHERE Estado = 1  AND IdPGeneral in @IdPGeneral AND IdArea in @IdArea AND IdSubArea in @IdSubArea AND IdTipo in @IdTipo Order By Id OFFSET @Skip ROWS FETCH NEXT @Take ROWS ONLY";
                    var preguntaFrecuente = _dapper.QueryDapper(_query, new { IdPGeneral = filtro.pgenerales, IdArea = filtro.areas, IdSubArea = filtro.subareas, IdTipo = filtro.tipos, Skip = filtro.skip, Take = filtro.pageSize });
                    if (!string.IsNullOrEmpty(preguntaFrecuente) && !preguntaFrecuente.Contains("[]") && !preguntaFrecuente.Contains("null"))
                    {
                        preguntaFrecuenteFiltroPaginacion = JsonConvert.DeserializeObject<List<PreguntaFrecuenteFiltroPaginacionDTO>>(preguntaFrecuente);
                    }
                }
                else
                {
                    var _query = "SELECT Id, IdSeccionPreguntaFrecuente, Pregunta, Respuesta, Tipo FROM pla.T_PreguntaFrecuente WHERE Estado = 1";
                    var preguntaFrecuente = _dapper.QueryDapper(_query, null);
                    if (!string.IsNullOrEmpty(preguntaFrecuente) && !preguntaFrecuente.Contains("[]") && !preguntaFrecuente.Contains("null"))
                    {
                        preguntaFrecuenteFiltroPaginacion = JsonConvert.DeserializeObject<List<PreguntaFrecuenteFiltroPaginacionDTO>>(preguntaFrecuente);
                    }
                }

                
                return preguntaFrecuenteFiltroPaginacion;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

    }

}

