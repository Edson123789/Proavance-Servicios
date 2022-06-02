using AutoMapper;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.DTOs.Scode.DTOs.Transversal;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace BSI.Integra.Aplicacion.Transversal.Repositorio
{
    public class ProgramaGeneralProblemaRepositorio : BaseRepository<TProgramaGeneralProblema, ProgramaGeneralProblemaBO>
    {
        #region Metodos Base
        public ProgramaGeneralProblemaRepositorio() : base()
        {
        }
        public ProgramaGeneralProblemaRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<ProgramaGeneralProblemaBO> GetBy(Expression<Func<TProgramaGeneralProblema, bool>> filter)
        {
            IEnumerable<TProgramaGeneralProblema> listado = base.GetBy(filter);
            List<ProgramaGeneralProblemaBO> listadoBO = new List<ProgramaGeneralProblemaBO>();
            foreach (var itemEntidad in listado)
            {
                ProgramaGeneralProblemaBO objetoBO = Mapper.Map<TProgramaGeneralProblema, ProgramaGeneralProblemaBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public ProgramaGeneralProblemaBO FirstById(int id)
        {
            try
            {
                TProgramaGeneralProblema entidad = base.FirstById(id);
                ProgramaGeneralProblemaBO objetoBO = new ProgramaGeneralProblemaBO();
                Mapper.Map<TProgramaGeneralProblema, ProgramaGeneralProblemaBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public ProgramaGeneralProblemaBO FirstBy(Expression<Func<TProgramaGeneralProblema, bool>> filter)
        {
            try
            {
                TProgramaGeneralProblema entidad = base.FirstBy(filter);
                ProgramaGeneralProblemaBO objetoBO = Mapper.Map<TProgramaGeneralProblema, ProgramaGeneralProblemaBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(ProgramaGeneralProblemaBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TProgramaGeneralProblema entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<ProgramaGeneralProblemaBO> listadoBO)
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

        public bool Update(ProgramaGeneralProblemaBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TProgramaGeneralProblema entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<ProgramaGeneralProblemaBO> listadoBO)
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
        private void AsignacionId(TProgramaGeneralProblema entidad, ProgramaGeneralProblemaBO objetoBO)
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

        private TProgramaGeneralProblema MapeoEntidad(ProgramaGeneralProblemaBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TProgramaGeneralProblema entidad = new TProgramaGeneralProblema();
                entidad = Mapper.Map<ProgramaGeneralProblemaBO, TProgramaGeneralProblema>(objetoBO,
                    opt => opt.ConfigureMap(MemberList.None));

                //mapea los hijos
                if (objetoBO.ProgramaGeneralProblemaModalidad != null && objetoBO.ProgramaGeneralProblemaModalidad.Count > 0)
                {
                    foreach (var hijo in objetoBO.ProgramaGeneralProblemaModalidad)
                    {
                        TProgramaGeneralProblemaModalidad entidadHijo = new TProgramaGeneralProblemaModalidad();
                        entidadHijo = Mapper.Map<ProgramaGeneralProblemaModalidadBO, TProgramaGeneralProblemaModalidad>(hijo,
                            opt => opt.ConfigureMap(MemberList.None));
                        entidad.TProgramaGeneralProblemaModalidad.Add(entidadHijo);
                    }
                }
                if (objetoBO.programaGeneralProblemaDetalleSolucion != null && objetoBO.programaGeneralProblemaDetalleSolucion.Count > 0)
                {
                    foreach (var hijo in objetoBO.programaGeneralProblemaDetalleSolucion)
                    {
                        TProgramaGeneralProblemaDetalleSolucion entidadHijo = new TProgramaGeneralProblemaDetalleSolucion();
                        entidadHijo = Mapper.Map<ProgramaGeneralProblemaDetalleSolucionBO, TProgramaGeneralProblemaDetalleSolucion>(hijo,
                            opt => opt.ConfigureMap(MemberList.None));
                        entidad.TProgramaGeneralProblemaDetalleSolucion.Add(entidadHijo);
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

        /// Autor: Jashin Salazar
        /// Fecha: 06/2021
        /// Version: 1.0
        /// <summary>
        /// Obtiene una lista de beneficios y argumentos  por programa pertecientes a la modalidades
        /// </summary>
        /// <returns></returns>
        public List<CompuestoProblemaModalidadAlternoDTO> ObteneProblemasPorModalidades(int idPGeneral)
        {
            try
            {
                List<ProblemaModalidadDTO> motivaciones = new List<ProblemaModalidadDTO>();
                List<CompuestoProblemaModalidadAlternoDTO> motivacionesModalidades = new List<CompuestoProblemaModalidadAlternoDTO>();
                var _query = string.Empty;
                _query = "SELECT IdProblema,IdPGeneral,NombreProblema,IdModalidadCurso,NombreModalidad,IdArgumentoProblema,DetalleArgumentoProblema,SolucionArgumentoProblema, IdModalidadProblema FROM pla.V_TProgramaGeneralProblema_Problemas " +
                    "WHERE EstadoModalidad = 1 and EstadoProblema = 1 and IdPGeneral = @idPGeneral";
                var query = _dapper.QueryDapper(_query, new { idPGeneral });
                if (!string.IsNullOrEmpty(query) && !query.Contains("[]"))
                {
                    motivaciones = JsonConvert.DeserializeObject<List<ProblemaModalidadDTO>>(query);
                    motivacionesModalidades = (from p in motivaciones
                                               group p by new
                                               {
                                                   p.IdPGeneral,
                                                   p.IdProblema,
                                                   p.NombreProblema
                                               } into g
                                               select new CompuestoProblemaModalidadAlternoDTO
                                               {
                                                   IdProblema = g.Key.IdProblema,
                                                   IdPGeneral = g.Key.IdPGeneral,
                                                   NombreProblema = g.Key.NombreProblema,

                                                   Modalidades = g.Select(o => new ModalidadCursoAlternoDTO
                                                   {
                                                       Id = o.IdModalidadProblema,
                                                       Nombre = o.NombreModalidad,
                                                       IdModalidadCurso = o.IdModalidadCurso
                                                   }).GroupBy(i => i.Id).Select(i => i.FirstOrDefault()).ToList(),
                                                   ProblemasArgumentos = g.Select(o => new ProblemaDetalleSolucionDTO
                                                   {
                                                       Id = o.IdArgumentoProblema,
                                                       Detalle = o.DetalleArgumentoProblema,
                                                       Solucion=o.SolucionArgumentoProblema
                                                   }).GroupBy(i => i.Id).Select(i => i.First()).ToList().Where(i => i.Id != null).ToList(),

                                               }).ToList();

                }
                return motivacionesModalidades;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }

        }
        /// Autor: Jashin Salazar
        /// Fecha: 06/2021
        /// Version: 1.0
        /// <summary>
        /// Obtiene una lista de beneficios y argumentos  por programa pertecientes a la modalidades
        /// </summary>
        /// <returns></returns>
        public List<CompuestoProblemaModalidadAlternoDTO> ObtenerTodoProblemas()
        {
            try
            {
                List<ProblemaModalidadDTO> motivaciones = new List<ProblemaModalidadDTO>();
                List<CompuestoProblemaModalidadAlternoDTO> motivacionesModalidades = new List<CompuestoProblemaModalidadAlternoDTO>();
                var _query = string.Empty;
                _query = "SELECT IdProblema,IdPGeneral,NombreProblema,IdModalidadCurso,NombreModalidad,IdArgumentoProblema,DetalleArgumentoProblema,SolucionArgumentoProblema, IdModalidadProblema FROM pla.V_TProgramaGeneralProblema_Problemas " +
                    "WHERE  (EstadoArgumento is null or EstadoArgumento = 1 )  and EstadoModalidad = 1 and EstadoProblema = 1 ";
                var query = _dapper.QueryDapper(_query, new {  });
                if (!string.IsNullOrEmpty(query) && !query.Contains("[]"))
                {
                    motivaciones = JsonConvert.DeserializeObject<List<ProblemaModalidadDTO>>(query);
                    motivacionesModalidades = (from p in motivaciones
                                               group p by new
                                               {
                                                   p.IdPGeneral,
                                                   p.IdProblema,
                                                   p.NombreProblema
                                               } into g
                                               select new CompuestoProblemaModalidadAlternoDTO
                                               {
                                                   IdProblema = g.Key.IdProblema,
                                                   IdPGeneral = g.Key.IdPGeneral,
                                                   NombreProblema = g.Key.NombreProblema,

                                                   Modalidades = g.Select(o => new ModalidadCursoAlternoDTO
                                                   {
                                                       Id = o.IdModalidadProblema,
                                                       Nombre = o.NombreModalidad,
                                                       IdModalidadCurso = o.IdModalidadCurso
                                                   }).GroupBy(i => i.Id).Select(i => i.FirstOrDefault()).ToList(),
                                                   ProblemasArgumentos = g.Select(o => new ProblemaDetalleSolucionDTO
                                                   {
                                                       Id = o.IdArgumentoProblema,
                                                       Detalle = o.DetalleArgumentoProblema,
                                                       Solucion = o.SolucionArgumentoProblema
                                                   }).GroupBy(i => i.Id).Select(i => i.First()).ToList().Where(i => i.Id != null).ToList(),

                                               }).ToList();

                }
                return motivacionesModalidades;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }

        }
    }
}
