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
    public class ProgramaGeneralCertificacionRepositorio: BaseRepository<TProgramaGeneralCertificacion, ProgramaGeneralCertificacionBO>
    {
        #region Metodos Base
        public ProgramaGeneralCertificacionRepositorio() : base()
        {
        }
        public ProgramaGeneralCertificacionRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<ProgramaGeneralCertificacionBO> GetBy(Expression<Func<TProgramaGeneralCertificacion, bool>> filter)
        {
            IEnumerable<TProgramaGeneralCertificacion> listado = base.GetBy(filter);
            List<ProgramaGeneralCertificacionBO> listadoBO = new List<ProgramaGeneralCertificacionBO>();
            foreach (var itemEntidad in listado)
            {
                ProgramaGeneralCertificacionBO objetoBO = Mapper.Map<TProgramaGeneralCertificacion, ProgramaGeneralCertificacionBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public ProgramaGeneralCertificacionBO FirstById(int id)
        {
            try
            {
                TProgramaGeneralCertificacion entidad = base.FirstById(id);
                ProgramaGeneralCertificacionBO objetoBO = new ProgramaGeneralCertificacionBO();
                Mapper.Map<TProgramaGeneralCertificacion, ProgramaGeneralCertificacionBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public ProgramaGeneralCertificacionBO FirstBy(Expression<Func<TProgramaGeneralCertificacion, bool>> filter)
        {
            try
            {
                TProgramaGeneralCertificacion entidad = base.FirstBy(filter);
                ProgramaGeneralCertificacionBO objetoBO = Mapper.Map<TProgramaGeneralCertificacion, ProgramaGeneralCertificacionBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(ProgramaGeneralCertificacionBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TProgramaGeneralCertificacion entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<ProgramaGeneralCertificacionBO> listadoBO)
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

        public bool Update(ProgramaGeneralCertificacionBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TProgramaGeneralCertificacion entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<ProgramaGeneralCertificacionBO> listadoBO)
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
        private void AsignacionId(TProgramaGeneralCertificacion entidad, ProgramaGeneralCertificacionBO objetoBO)
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

        private TProgramaGeneralCertificacion MapeoEntidad(ProgramaGeneralCertificacionBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TProgramaGeneralCertificacion entidad = new TProgramaGeneralCertificacion();
                entidad = Mapper.Map<ProgramaGeneralCertificacionBO, TProgramaGeneralCertificacion>(objetoBO,
                    opt => opt.ConfigureMap(MemberList.None));

                //mapea los hijos
                if (objetoBO.ProgramaGeneralCertificacionModalidad != null && objetoBO.ProgramaGeneralCertificacionModalidad.Count > 0)
                {
                    foreach (var hijo in objetoBO.ProgramaGeneralCertificacionModalidad)
                    {
                        TProgramaGeneralCertificacionModalidad entidadHijo = new TProgramaGeneralCertificacionModalidad();
                        entidadHijo = Mapper.Map<ProgramaGeneralCertificacionModalidadBO, TProgramaGeneralCertificacionModalidad>(hijo,
                            opt => opt.ConfigureMap(MemberList.None));
                        entidad.TProgramaGeneralCertificacionModalidad.Add(entidadHijo);
                    }
                }
                if (objetoBO.programaGeneralCertificacionArgumento != null && objetoBO.programaGeneralCertificacionArgumento.Count > 0)
                {
                    foreach (var hijo in objetoBO.programaGeneralCertificacionArgumento)
                    {
                        TProgramaGeneralCertificacionArgumento entidadHijo = new TProgramaGeneralCertificacionArgumento();
                        entidadHijo = Mapper.Map<ProgramaGeneralCertificacionArgumentoBO, TProgramaGeneralCertificacionArgumento>(hijo,
                            opt => opt.ConfigureMap(MemberList.None));
                        entidad.TProgramaGeneralCertificacionArgumento.Add(entidadHijo);
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
        public List<CompuestoCertificacionModalidadAlternoDTO> ObteneCertificacionesPorModalidades(int idPGeneral)
        {
            try
            {
                List<CertificacionModalidadDTO> motivaciones = new List<CertificacionModalidadDTO>();
                List<CompuestoCertificacionModalidadAlternoDTO> motivacionesModalidades = new List<CompuestoCertificacionModalidadAlternoDTO>();
                var _query = string.Empty;
                _query = "SELECT IdCertificacion,IdPGeneral,NombreCertificacion,IdModalidadCurso,NombreModalidad,IdArgumentoCertificacion,NombreArgumentoCertificacion, IdModalidadCertificacion FROM pla.  V_TProgramaGeneralCertificacion_Certificaciones " +
                    "WHERE EstadoModalidad = 1 and EstadoCertificacion = 1 and IdPGeneral = @idPGeneral";
                var query = _dapper.QueryDapper(_query, new { idPGeneral });
                if (!string.IsNullOrEmpty(query) && !query.Contains("[]"))
                {
                    motivaciones = JsonConvert.DeserializeObject<List<CertificacionModalidadDTO>>(query);
                    motivacionesModalidades = (from p in motivaciones
                                               group p by new
                                               {
                                                   p.IdPGeneral,
                                                   p.IdCertificacion,
                                                   p.NombreCertificacion
                                               } into g
                                               select new CompuestoCertificacionModalidadAlternoDTO
                                               {
                                                   IdCertificacion = g.Key.IdCertificacion,
                                                   IdPGeneral = g.Key.IdPGeneral,
                                                   NombreCertificacion = g.Key.NombreCertificacion,

                                                   Modalidades = g.Select(o => new ModalidadCursoAlternoDTO
                                                   {
                                                       Id = o.IdModalidadCertificacion,
                                                       Nombre = o.NombreModalidad,
                                                       IdModalidadCurso = o.IdModalidadCurso
                                                   }).GroupBy(i => i.Id).Select(i => i.FirstOrDefault()).ToList(),
                                                   CertificacionesArgumentos = g.Select(o => new CertificacionArgumentoDTO
                                                   {
                                                       Id = o.IdArgumentoCertificacion,
                                                       Nombre = o.NombreArgumentoCertificacion
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
