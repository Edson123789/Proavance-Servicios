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
    public class ProgramaGeneralPrerequisitoRepositorio : BaseRepository<TProgramaGeneralPrerequisito, ProgramaGeneralPrerequisitoBO>
    {
        #region Metodos Base
        public ProgramaGeneralPrerequisitoRepositorio() : base()
        {
        }
        public ProgramaGeneralPrerequisitoRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<ProgramaGeneralPrerequisitoBO> GetBy(Expression<Func<TProgramaGeneralPrerequisito, bool>> filter)
        {
            IEnumerable<TProgramaGeneralPrerequisito> listado = base.GetBy(filter);
            List<ProgramaGeneralPrerequisitoBO> listadoBO = new List<ProgramaGeneralPrerequisitoBO>();
            foreach (var itemEntidad in listado)
            {
                ProgramaGeneralPrerequisitoBO objetoBO = Mapper.Map<TProgramaGeneralPrerequisito, ProgramaGeneralPrerequisitoBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public ProgramaGeneralPrerequisitoBO FirstById(int id)
        {
            try
            {
                TProgramaGeneralPrerequisito entidad = base.FirstById(id);
                ProgramaGeneralPrerequisitoBO objetoBO = new ProgramaGeneralPrerequisitoBO();
                Mapper.Map<TProgramaGeneralPrerequisito, ProgramaGeneralPrerequisitoBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public ProgramaGeneralPrerequisitoBO FirstBy(Expression<Func<TProgramaGeneralPrerequisito, bool>> filter)
        {
            try
            {
                TProgramaGeneralPrerequisito entidad = base.FirstBy(filter);
                ProgramaGeneralPrerequisitoBO objetoBO = Mapper.Map<TProgramaGeneralPrerequisito, ProgramaGeneralPrerequisitoBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(ProgramaGeneralPrerequisitoBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TProgramaGeneralPrerequisito entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<ProgramaGeneralPrerequisitoBO> listadoBO)
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

        public bool Update(ProgramaGeneralPrerequisitoBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TProgramaGeneralPrerequisito entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<ProgramaGeneralPrerequisitoBO> listadoBO)
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
        private void AsignacionId(TProgramaGeneralPrerequisito entidad, ProgramaGeneralPrerequisitoBO objetoBO)
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

        private TProgramaGeneralPrerequisito MapeoEntidad(ProgramaGeneralPrerequisitoBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TProgramaGeneralPrerequisito entidad = new TProgramaGeneralPrerequisito();
                entidad = Mapper.Map<ProgramaGeneralPrerequisitoBO, TProgramaGeneralPrerequisito>(objetoBO,
                    opt => opt.ConfigureMap(MemberList.None));

                //mapea los hijos
                if (objetoBO.ProgramaGeneralPrerequisitoModalidad != null && objetoBO.ProgramaGeneralPrerequisitoModalidad.Count > 0)
                {
                    foreach (var hijo in objetoBO.ProgramaGeneralPrerequisitoModalidad)
                    {
                        TProgramaGeneralPrerequisitoModalidad entidadHijo = new TProgramaGeneralPrerequisitoModalidad();
						entidadHijo = Mapper.Map<ProgramaGeneralPrerequisitoModalidadBO, TProgramaGeneralPrerequisitoModalidad>(hijo,
							opt => opt.ConfigureMap(MemberList.None));
                        entidad.TProgramaGeneralPrerequisitoModalidad.Add(entidadHijo);
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
        /// Obtiene una lista de prerequisitos por programa pertecientes a la modalidades
        /// </summary>
        /// <returns></returns>
        public List<CompuestoPreRequisitoModalidadDTO> ObtenerPreRequisitosPorModalidades(int idPGeneral)
        {
            try
            {
                List<PreRequisitoModalidadDTO> preRequisitos = new List<PreRequisitoModalidadDTO>();
                List<CompuestoPreRequisitoModalidadDTO> preRequisitosModalidades = new List<CompuestoPreRequisitoModalidadDTO>();
                var _query = string.Empty;
                _query = "SELECT IdPreRequisito,IdPGeneral, NombrePreRequisito,Orden,Tipo,IdModalidadCurso,NombreModalidad,IdModalidadPreRequisito  FROM pla.V_TProgramaGeneralPrerequisito_PreRequisitos " +
                    "WHERE EstadoModalidad = 1 and EstadoPreRequisito = 1 and IdPGeneral = @idPGeneral";
                var query = _dapper.QueryDapper(_query, new { idPGeneral });
                if (!string.IsNullOrEmpty(query) && !query.Contains("[]"))
                {
                    preRequisitos = JsonConvert.DeserializeObject<List<PreRequisitoModalidadDTO>>(query);
                    preRequisitosModalidades = (from p in preRequisitos
                                                group p by new
                                                {
                                                    p.IdPGeneral,
                                                    p.IdPreRequisito,
                                                    p.NombrePreRequisito,
                                                    p.Orden,
                                                    p.Tipo
                                                } into g
                                                select new CompuestoPreRequisitoModalidadDTO
                                                {
                                                    IdPreRequisito = g.Key.IdPreRequisito,
                                                    IdPGeneral = g.Key.IdPGeneral,
                                                    NombrePreRequisito = g.Key.NombrePreRequisito,
                                                    Orden = g.Key.Orden,
                                                    Tipo = g.Key.Tipo,

                                                    Modalidades = g.Select(o => new ModalidadCursoDTO
                                                    {
                                                        Id = o.IdModalidadPreRequisito,
                                                        Nombre = o.NombreModalidad,
                                                        IdModalidad = o.IdModalidadCurso
                                                    }).ToList()
                                                }).OrderBy(x => x.Orden).ToList();

                }

                return preRequisitosModalidades;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }

        }
        /// <summary>
        /// Elimina (Actualiza estado a false ) todos los Prerequisitos asociados a un programa
        /// </summary>
        /// <param name="idPGeneral"></param>
        /// <returns></returns>
        public List<int> EliminacionLogicoPorPrograma(int idPGeneral, string usuario, List<CompuestoPreRequisitoModalidadDTO> nuevos)
        {
            try
            {
                var listaBorrar = GetBy(x => x.IdPgeneral == idPGeneral && x.Estado == true).ToList();
                listaBorrar.RemoveAll(x => nuevos.Any(y => y.IdPreRequisito == x.Id));
                foreach (var item in listaBorrar)
                {
                    Delete(item.Id, usuario);
                }
                List<int> result = new List<int>();
                result = listaBorrar.Select(x => x.Id).ToList();
                return result;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

    }
}
