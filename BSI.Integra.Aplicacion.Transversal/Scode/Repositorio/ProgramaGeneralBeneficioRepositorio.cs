using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using AutoMapper;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.DTOs.Scode.DTOs.Transversal;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using Newtonsoft.Json;

namespace BSI.Integra.Aplicacion.Transversal.Repositorio
{
    public class ProgramaGeneralBeneficioRepositorio : BaseRepository<TProgramaGeneralBeneficio, ProgramaGeneralBeneficioBO>
    {
        #region Metodos Base
        public ProgramaGeneralBeneficioRepositorio() : base()
        {
        }
        public ProgramaGeneralBeneficioRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<ProgramaGeneralBeneficioBO> GetBy(Expression<Func<TProgramaGeneralBeneficio, bool>> filter)
        {
            IEnumerable<TProgramaGeneralBeneficio> listado = base.GetBy(filter);
            List<ProgramaGeneralBeneficioBO> listadoBO = new List<ProgramaGeneralBeneficioBO>();
            foreach (var itemEntidad in listado)
            {
                ProgramaGeneralBeneficioBO objetoBO = Mapper.Map<TProgramaGeneralBeneficio, ProgramaGeneralBeneficioBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public ProgramaGeneralBeneficioBO FirstById(int id)
        {
            try
            {
                TProgramaGeneralBeneficio entidad = base.FirstById(id);
                ProgramaGeneralBeneficioBO objetoBO = new ProgramaGeneralBeneficioBO();
                Mapper.Map<TProgramaGeneralBeneficio, ProgramaGeneralBeneficioBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public ProgramaGeneralBeneficioBO FirstBy(Expression<Func<TProgramaGeneralBeneficio, bool>> filter)
        {
            try
            {
                TProgramaGeneralBeneficio entidad = base.FirstBy(filter);
                ProgramaGeneralBeneficioBO objetoBO = Mapper.Map<TProgramaGeneralBeneficio, ProgramaGeneralBeneficioBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(ProgramaGeneralBeneficioBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TProgramaGeneralBeneficio entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<ProgramaGeneralBeneficioBO> listadoBO)
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

        public bool Update(ProgramaGeneralBeneficioBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TProgramaGeneralBeneficio entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<ProgramaGeneralBeneficioBO> listadoBO)
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
        private void AsignacionId(TProgramaGeneralBeneficio entidad, ProgramaGeneralBeneficioBO objetoBO)
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

        private TProgramaGeneralBeneficio MapeoEntidad(ProgramaGeneralBeneficioBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TProgramaGeneralBeneficio entidad = new TProgramaGeneralBeneficio();
                entidad = Mapper.Map<ProgramaGeneralBeneficioBO, TProgramaGeneralBeneficio>(objetoBO,
                    opt => opt.ConfigureMap(MemberList.None));

                //mapea los hijos
                if (objetoBO.ProgramaGeneralBeneficioModalidad != null && objetoBO.ProgramaGeneralBeneficioModalidad.Count > 0)
                {
                    foreach (var hijo in objetoBO.ProgramaGeneralBeneficioModalidad)
                    {
                        TProgramaGeneralBeneficioModalidad entidadHijo = new TProgramaGeneralBeneficioModalidad();
                        entidadHijo = Mapper.Map<ProgramaGeneralBeneficioModalidadBO, TProgramaGeneralBeneficioModalidad>(hijo,
                            opt => opt.ConfigureMap(MemberList.None));
                        entidad.TProgramaGeneralBeneficioModalidad.Add(entidadHijo);
                    }
                }
                if (objetoBO.programaGeneralBeneficioArgumento != null && objetoBO.programaGeneralBeneficioArgumento.Count > 0)
                {
                    foreach (var hijo in objetoBO.programaGeneralBeneficioArgumento)
                    {
                        TProgramaGeneralBeneficioArgumento entidadHijo = new TProgramaGeneralBeneficioArgumento();
                        entidadHijo = Mapper.Map<ProgramaGeneralBeneficioArgumentoBO, TProgramaGeneralBeneficioArgumento>(hijo,
                            opt => opt.ConfigureMap(MemberList.None));
                        entidad.TProgramaGeneralBeneficioArgumento.Add(entidadHijo);
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
        /// Obtiene una lista de beneficios y argumentos  por programa pertecientes a la modalidades
        /// </summary>
        /// <returns></returns>
        public List<CompuestoBeneficioModalidadAlternoDTO> ObteneBeneficiosPorModalidades(int idPGeneral)
        {
            try
            {
                List<BeneficioModalidadDTO> beneficios = new List<BeneficioModalidadDTO>();
                List<CompuestoBeneficioModalidadAlternoDTO> beneficiosModalidades = new List<CompuestoBeneficioModalidadAlternoDTO>();
                var _query = string.Empty;
                _query = "SELECT IdBeneficio,IdPGeneral,NombreBeneficio,IdModalidadCurso,NombreModalidad,IdArgumentoBeneficio,NombreArgumentoBeneficio, IdModalidadBeneficio FROM pla.V_TProgramaGeneralBeneficio_Beneficios " +
                    "WHERE EstadoModalidad = 1 and EstadoBeneficio = 1 and IdPGeneral = @idPGeneral";
                var query = _dapper.QueryDapper(_query, new { idPGeneral });
                if (!string.IsNullOrEmpty(query) && !query.Contains("[]"))
                {
                    beneficios = JsonConvert.DeserializeObject<List<BeneficioModalidadDTO>>(query);
                    beneficiosModalidades = (from p in beneficios
                                             group p by new
                                             {
                                                 p.IdPGeneral,
                                                 p.IdBeneficio,
                                                 p.NombreBeneficio
                                             } into g
                                             select new CompuestoBeneficioModalidadAlternoDTO
                                             {
                                                 IdBeneficio = g.Key.IdBeneficio,
                                                 IdPGeneral = g.Key.IdPGeneral,
                                                 NombreBeneficio = g.Key.NombreBeneficio,

                                                 Modalidades = g.Select(o => new ModalidadCursoAlternoDTO
                                                 {
                                                     Id = o.IdModalidadBeneficio,
                                                     Nombre = o.NombreModalidad,
                                                     IdModalidadCurso = o.IdModalidadCurso
                                                 }).GroupBy(i => i.Id).Select(i => i.FirstOrDefault()).ToList(),
                                                 BeneficiosArgumentos = g.Select(o => new BeneficioArgumentoDTO
                                                 {
                                                     Id = o.IdArgumentoBeneficio,
                                                     Nombre = o.NombreArgumentoBeneficio
                                                 }).GroupBy(i => i.Id).Select(i => i.First()).ToList().Where(i => i.Id != null).ToList(),

                                             }).ToList();

                }
                return beneficiosModalidades;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }

        }
        /// <summary>
        /// Elimina (Actualiza estado a false ) todos los Beneficios asociados a un programa
        /// </summary>
        /// <param name="idPGeneral"></param>
        /// <returns></returns>
        public List<int> EliminacionLogicoPorPrograma(int idPGeneral, string usuario, List<CompuestoBeneficioModalidadDTO> nuevos)
        {
            try
            {
                var listaBorrar = GetBy(x => x.IdPgeneral == idPGeneral && x.Estado == true).ToList();
                listaBorrar.RemoveAll(x => nuevos.Any(y => y.IdBeneficio == x.Id));
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
