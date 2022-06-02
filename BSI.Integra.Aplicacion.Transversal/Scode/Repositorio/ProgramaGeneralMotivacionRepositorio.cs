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
    public class ProgramaGeneralMotivacionRepositorio : BaseRepository<TProgramaGeneralMotivacion, ProgramaGeneralMotivacionBO>
    {
        #region Metodos Base
        public ProgramaGeneralMotivacionRepositorio() : base()
        {
        }
        public ProgramaGeneralMotivacionRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<ProgramaGeneralMotivacionBO> GetBy(Expression<Func<TProgramaGeneralMotivacion, bool>> filter)
        {
            IEnumerable<TProgramaGeneralMotivacion> listado = base.GetBy(filter);
            List<ProgramaGeneralMotivacionBO> listadoBO = new List<ProgramaGeneralMotivacionBO>();
            foreach (var itemEntidad in listado)
            {
                ProgramaGeneralMotivacionBO objetoBO = Mapper.Map<TProgramaGeneralMotivacion, ProgramaGeneralMotivacionBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public ProgramaGeneralMotivacionBO FirstById(int id)
        {
            try
            {
                TProgramaGeneralMotivacion entidad = base.FirstById(id);
                ProgramaGeneralMotivacionBO objetoBO = new ProgramaGeneralMotivacionBO();
                Mapper.Map<TProgramaGeneralMotivacion, ProgramaGeneralMotivacionBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public ProgramaGeneralMotivacionBO FirstBy(Expression<Func<TProgramaGeneralMotivacion, bool>> filter)
        {
            try
            {
                TProgramaGeneralMotivacion entidad = base.FirstBy(filter);
                ProgramaGeneralMotivacionBO objetoBO = Mapper.Map<TProgramaGeneralMotivacion, ProgramaGeneralMotivacionBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(ProgramaGeneralMotivacionBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TProgramaGeneralMotivacion entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<ProgramaGeneralMotivacionBO> listadoBO)
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

        public bool Update(ProgramaGeneralMotivacionBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TProgramaGeneralMotivacion entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<ProgramaGeneralMotivacionBO> listadoBO)
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
        private void AsignacionId(TProgramaGeneralMotivacion entidad, ProgramaGeneralMotivacionBO objetoBO)
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

        private TProgramaGeneralMotivacion MapeoEntidad(ProgramaGeneralMotivacionBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TProgramaGeneralMotivacion entidad = new TProgramaGeneralMotivacion();
                entidad = Mapper.Map<ProgramaGeneralMotivacionBO, TProgramaGeneralMotivacion>(objetoBO,
                    opt => opt.ConfigureMap(MemberList.None));

                //mapea los hijos
                if (objetoBO.ProgramaGeneralMotivacionModalidad != null && objetoBO.ProgramaGeneralMotivacionModalidad.Count > 0)
                {
                    foreach (var hijo in objetoBO.ProgramaGeneralMotivacionModalidad)
                    {
                        TProgramaGeneralMotivacionModalidad entidadHijo = new TProgramaGeneralMotivacionModalidad();
                        entidadHijo = Mapper.Map<ProgramaGeneralMotivacionModalidadBO, TProgramaGeneralMotivacionModalidad>(hijo,
                            opt => opt.ConfigureMap(MemberList.None));
                        entidad.TProgramaGeneralMotivacionModalidad.Add(entidadHijo);
                    }
                }
                if (objetoBO.programaGeneralMotivacionArgumento != null && objetoBO.programaGeneralMotivacionArgumento.Count > 0)
                {
                    foreach (var hijo in objetoBO.programaGeneralMotivacionArgumento)
                    {
                        TProgramaGeneralMotivacionArgumento entidadHijo = new TProgramaGeneralMotivacionArgumento();
                        entidadHijo = Mapper.Map<ProgramaGeneralMotivacionArgumentoBO, TProgramaGeneralMotivacionArgumento>(hijo,
                            opt => opt.ConfigureMap(MemberList.None));
                        entidad.TProgramaGeneralMotivacionArgumento.Add(entidadHijo);
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
        public List<CompuestoMotivacionModalidadAlternoDTO> ObteneMotivacionesPorModalidades(int idPGeneral)
        {
            try
            {
                List<MotivacionModalidadDTO> motivaciones = new List<MotivacionModalidadDTO>();
                List<CompuestoMotivacionModalidadAlternoDTO> motivacionesModalidades = new List<CompuestoMotivacionModalidadAlternoDTO>();
                var _query = string.Empty;
                _query = "SELECT IdMotivacion,IdPGeneral,NombreMotivacion,IdModalidadCurso,NombreModalidad,IdArgumentoMotivacion,NombreArgumentoMotivacion, IdModalidadMotivacion FROM pla.  V_TProgramaGeneralMotivacion_Motivaciones " +
                    "WHERE EstadoModalidad = 1 and EstadoMotivacion = 1 and IdPGeneral = @idPGeneral";
                var query = _dapper.QueryDapper(_query, new { idPGeneral });
                if (!string.IsNullOrEmpty(query) && !query.Contains("[]"))
                {
                    motivaciones = JsonConvert.DeserializeObject<List<MotivacionModalidadDTO>>(query);
                    motivacionesModalidades = (from p in motivaciones
                                               group p by new
                                               {
                                                   p.IdPGeneral,
                                                   p.IdMotivacion,
                                                   p.NombreMotivacion
                                               } into g
                                               select new CompuestoMotivacionModalidadAlternoDTO
                                               {
                                                   IdMotivacion = g.Key.IdMotivacion,
                                                   IdPGeneral = g.Key.IdPGeneral,
                                                   NombreMotivacion = g.Key.NombreMotivacion,

                                                   Modalidades = g.Select(o => new ModalidadCursoAlternoDTO
                                                   {
                                                       Id = o.IdModalidadMotivacion,
                                                       Nombre = o.NombreModalidad,
                                                       IdModalidadCurso = o.IdModalidadCurso
                                                   }).GroupBy(i => i.Id).Select(i => i.FirstOrDefault()).ToList(),
                                                   MotivacionesArgumentos = g.Select(o => new MotivacionArgumentoDTO
                                                   {
                                                       Id = o.IdArgumentoMotivacion,
                                                       Nombre = o.NombreArgumentoMotivacion
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
