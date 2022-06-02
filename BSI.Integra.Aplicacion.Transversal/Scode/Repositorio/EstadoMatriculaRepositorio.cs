using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using AutoMapper;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using System.Collections.Generic;
using System.Text;
using BSI.Integra.Aplicacion.Transversal.Scode.BO;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.DTOs.Scode.DTOs.Transversal;
using System.Linq;
using Newtonsoft.Json;

namespace BSI.Integra.Aplicacion.Transversal.Repositorio
{
    public class EstadoMatriculaRepositorio : BaseRepository<TEstadoMatricula, EstadoMatriculaBO>
    {
        #region Metodos Base
        public EstadoMatriculaRepositorio() : base()
        {
        }
        public EstadoMatriculaRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<EstadoMatriculaBO> GetBy(Expression<Func<TEstadoMatricula, bool>> filter)
        {
            IEnumerable<TEstadoMatricula> listado = base.GetBy(filter);
            List<EstadoMatriculaBO> listadoBO = new List<EstadoMatriculaBO>();
            foreach (var itemEntidad in listado)
            {
                EstadoMatriculaBO objetoBO = Mapper.Map<TEstadoMatricula, EstadoMatriculaBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public EstadoMatriculaBO FirstById(int id)
        {
            try
            {
                TEstadoMatricula entidad = base.FirstById(id);
                EstadoMatriculaBO objetoBO = new EstadoMatriculaBO();
                Mapper.Map<TEstadoMatricula, EstadoMatriculaBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public EstadoMatriculaBO FirstBy(Expression<Func<TEstadoMatricula, bool>> filter)
        {
            try
            {
                TEstadoMatricula entidad = base.FirstBy(filter);
                EstadoMatriculaBO objetoBO = Mapper.Map<TEstadoMatricula, EstadoMatriculaBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(EstadoMatriculaBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TEstadoMatricula entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<EstadoMatriculaBO> listadoBO)
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

        public bool Update(EstadoMatriculaBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TEstadoMatricula entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<EstadoMatriculaBO> listadoBO)
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
        private void AsignacionId(TEstadoMatricula entidad, EstadoMatriculaBO objetoBO)
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

        private TEstadoMatricula MapeoEntidad(EstadoMatriculaBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TEstadoMatricula entidad = new TEstadoMatricula();
                entidad = Mapper.Map<EstadoMatriculaBO, TEstadoMatricula>(objetoBO,
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



        /// Autor: Jose Villena
        /// Fecha: 01/03/2021
        /// Version: 1.0
        /// <summary>
        /// Obtiene un listado de estados de matricula para ser usados en filtros
        /// </summary>
        /// <param></param>
        /// <returns>Lista</returns>
        public List<FiltroDTO> ObtenerTodoFiltro()
        {
            try
            {
                return this.GetBy(x => x.Estado, x => new FiltroDTO { Id = x.Id, Nombre = x.EstadoMatricula }).ToList();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Jose Villena
        /// Fecha: 01/03/2021
        /// Version: 1.0
        /// <summary>
        /// Obtiene un listado de estados de matricula para ser usados en filtros
        /// </summary>
        /// <param></param>
        /// <returns>Lista</returns>
        public List<FiltroConfiguracionCoordinadoraEstadoMatriculaDTO> ObtenerTodoFiltroConfiguracionCoordinadora()
        {
            try
            {
                return this.GetBy(x => x.Estado, x => new FiltroConfiguracionCoordinadoraEstadoMatriculaDTO { IdEstadoMatricula = x.Id, EstadoMatricula = x.EstadoMatricula }).ToList();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public List<SubEstadoMatriculaFiltroDTO> ObtenerComboSubEstadoMatricula()
        {
            try
            {
                List<SubEstadoMatriculaFiltroDTO> estadoMatriculado = new List<SubEstadoMatriculaFiltroDTO>();
                var _query = "Select Id,Nombre,IdEstadoMatricula From fin.V_ObtenerSubEstadoMatricula Where Estado=1 and IdEstadoMatricula=19";
                var pEspecificoDB = _dapper.QueryDapper(_query, null);
                if (!string.IsNullOrEmpty(pEspecificoDB) && !pEspecificoDB.Contains("[]"))
                {
                    estadoMatriculado = JsonConvert.DeserializeObject<List<SubEstadoMatriculaFiltroDTO>>(pEspecificoDB);
                }
                return estadoMatriculado;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public List<TCRM_SubEstadoMatriculaDTO> ObtenerSubEstadoMatricula()
        {
            try
            {
                List<TCRM_SubEstadoMatriculaDTO> estadoMatriculado = new List<TCRM_SubEstadoMatriculaDTO>();
                var _query = "Select Id,Nombre,UsuarioCreacion,FechaCreacion,Estado,IdOpcionAvanceAcademico,ValorAvanceAcademico1,ValorAvanceAcademico2,IdEstadoPago,IdOpcionNotaPromedio,ValorNotaPromedio1,ValorNotaPromedio2,TieneDeuda,ProyectoFinal,RequiereVerificacionInformacion,EstadoMatricula From fin.V_ObtenerSubEstadoMatricula Where Estado=1";
                var pEspecificoDB = _dapper.QueryDapper(_query, null);
                if (!string.IsNullOrEmpty(pEspecificoDB) && !pEspecificoDB.Contains("[]"))
                {
                    estadoMatriculado = JsonConvert.DeserializeObject<List<TCRM_SubEstadoMatriculaDTO>>(pEspecificoDB);
                }
                return estadoMatriculado;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Funcion referenciada en EstadoMatriculaController
        /// Devuelve los subestados segun el Id del EstadoMatricula
        /// </summary>
        /// <param name="IdEstadoMatricula"></param>
        /// <returns></returns>
        public List<TCRM_SubEstadoMatriculaDTO> ObtenerFiltroSubEstadoMatricula(int IdEstadoMatricula)//LPPG
        {
            try
            {
                List<TCRM_SubEstadoMatriculaDTO> estadoMatriculado = new List<TCRM_SubEstadoMatriculaDTO>();
                var _query = "Select Id,Nombre From fin.V_ObtenerSubEstadoMatricula Where Estado=1 and IdEstadoMatricula = @IdEstadoMatricula";
                var pEspecificoDB = _dapper.QueryDapper(_query, new { IdEstadoMatricula = IdEstadoMatricula });
                if (!string.IsNullOrEmpty(pEspecificoDB) && !pEspecificoDB.Contains("[]"))
                {
                    estadoMatriculado = JsonConvert.DeserializeObject<List<TCRM_SubEstadoMatriculaDTO>>(pEspecificoDB);
                }
                return estadoMatriculado;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public List<EstadoMatriculaDTO> ObtenerEstadosMatricula()
        {
            try
            {
                List<EstadoMatriculaDTO> estados = new List<EstadoMatriculaDTO>();
                var _query = string.Empty;
                _query = "SELECT Id, EstadoMatricula, Estado, UsuarioCreacion, FechaCreacion FROM fin.V_ObtenerEstadosMatricula where Estado=1";
                var estadoDB = _dapper.QueryDapper(_query, null);
                if (!string.IsNullOrEmpty(estadoDB) && !estadoDB.Contains("[]"))
                {
                    estados = JsonConvert.DeserializeObject<List<EstadoMatriculaDTO>>(estadoDB);
                }
                return estados;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public SubEstadoMatriculaDTO InsertarSubEstado(string Nombre, bool Estado, string UsuarioCreacion, string UsuarioModificacion, DateTime FechaCreacion, DateTime FechaModificacion, int idEstadoMatricula,int IdOpcionAvaceAcademico,int ValorAvanceAcademico1,int ValorAvanceAcademico2,string IdEstadoPago,int IdOpcionNotaPromedio,int ValorNotaPromedio1,int ValorNotaPromedio2,int TieneDeuda,int ProyectoFinal,int RequiereVerificacionInformacion)
        {
            SubEstadoMatriculaDTO rpta = new SubEstadoMatriculaDTO();
            rpta.SubEstado = new List<TCRM_SubEstadoMatriculaDTO>();
            var query = _dapper.QuerySPDapper("fin.SP_InsertarSubEstadosMatricula", new
            {
                Nombre = Nombre,
                Estado = Estado,
                IdEstadoMatricula = idEstadoMatricula,
                UsuarioCreacion = UsuarioCreacion,
                UsuarioModificacion = UsuarioModificacion,
                FechaCreacion = FechaCreacion,
                FechaModificacion = FechaModificacion,

                IdOpcionAvaceAcademico = IdOpcionAvaceAcademico,
                ValorAvanceAcademico1 = ValorAvanceAcademico1,
                ValorAvanceAcademico2 = ValorAvanceAcademico2,
                IdEstadoPago = IdEstadoPago,
                IdOpcionNotaPromedio = IdOpcionNotaPromedio,
                ValorNotaPromedio1 = ValorNotaPromedio1,
                ValorNotaPromedio2 = ValorNotaPromedio2,
                TieneDeuda = TieneDeuda,
                ProyectoFinal = ProyectoFinal,
                RequiereVerificacionInformacion = RequiereVerificacionInformacion
            });

            if (!string.IsNullOrEmpty(query) && !query.Contains("[]"))
            {
                rpta.SubEstado = JsonConvert.DeserializeObject<List<TCRM_SubEstadoMatriculaDTO>>(query);

            }
            return rpta;

        }
        public EstadoMatriculaListDTO InsertarEstadoSubestado(string EstadoMatricula, bool Estado, string UsuarioCreacion, string UsuarioModificacion, DateTime FechaCreacion, DateTime FechaModificacion, string IdSubEstados)
        {
            EstadoMatriculaListDTO rpta = new EstadoMatriculaListDTO();
            rpta.Estado = new List<TCRM_EstadoMatriculaInsertarDTO>();
            var query = _dapper.QuerySPDapper("fin.SP_InsertarEstadosMatricula", new
            {
                EstadoMatricula = EstadoMatricula,
                Estado = Estado,
                UsuarioCreacion = UsuarioCreacion,
                UsuarioModificacion = UsuarioModificacion,
                FechaCreacion = FechaCreacion,
                FechaModificacion = FechaModificacion,
                IdSubEstados = IdSubEstados,
            });

            if (!string.IsNullOrEmpty(query) && !query.Contains("[]"))
            {
                rpta.Estado = JsonConvert.DeserializeObject<List<TCRM_EstadoMatriculaInsertarDTO>>(query);

            }
            return rpta;

        }
        public SubEstadoMatriculaDTO ObtenerSubEstadoIndividual(int IdEstadoMatricula)
        {
            SubEstadoMatriculaDTO rpta = new SubEstadoMatriculaDTO();
            rpta.SubEstado = new List<TCRM_SubEstadoMatriculaDTO>();
            var query = _dapper.QuerySPDapper("fin.SP_SubEstadosMatriculaIndividual", new
            {
                IdEstadoMatricula = IdEstadoMatricula,
            });

            if (!string.IsNullOrEmpty(query) && !query.Contains("[]"))
            {
                rpta.SubEstado = JsonConvert.DeserializeObject<List<TCRM_SubEstadoMatriculaDTO>>(query);

            }
            return rpta;
        }
        public SubEstadoMatriculaDTO EditarSubEstado(string Nombre, int Id, string UsuarioModificacion, DateTime FechaModificacion, int IdOpcionAvaceAcademico, int ValorAvanceAcademico1, int ValorAvanceAcademico2, string IdEstadoPago, int IdOpcionNotaPromedio, int ValorNotaPromedio1, int ValorNotaPromedio2, int TieneDeuda, int ProyectoFinal, int RequiereVerificacionInformacion)
        {
            SubEstadoMatriculaDTO rpta = new SubEstadoMatriculaDTO();
            rpta.SubEstado = new List<TCRM_SubEstadoMatriculaDTO>();
            var query = _dapper.QuerySPDapper("fin.SP_EditarSubEstadoMatricula", new
            {
                Id = Id,
                Nombre = Nombre,
                UsuarioModificacion = UsuarioModificacion,
                FechaModificacion = FechaModificacion,

                IdOpcionAvaceAcademico = IdOpcionAvaceAcademico,
                ValorAvanceAcademico1 = ValorAvanceAcademico1,
                ValorAvanceAcademico2 = ValorAvanceAcademico2,
                IdEstadoPago = IdEstadoPago,
                IdOpcionNotaPromedio = IdOpcionNotaPromedio,
                ValorNotaPromedio1 = ValorNotaPromedio1,
                ValorNotaPromedio2 = ValorNotaPromedio2,
                TieneDeuda = TieneDeuda,
                ProyectoFinal = ProyectoFinal,
                RequiereVerificacionInformacion = RequiereVerificacionInformacion
            });

            if (!string.IsNullOrEmpty(query) && !query.Contains("[]"))
            {
                rpta.SubEstado = JsonConvert.DeserializeObject<List<TCRM_SubEstadoMatriculaDTO>>(query);

            }
            return rpta;
        }
        public EstadoMatriculaListDTO EditarEstado(int Id, string EstadoMatricula, string UsuarioModificacion, DateTime FechaModificacion, string IdSubEstados)
        {
            EstadoMatriculaListDTO rpta = new EstadoMatriculaListDTO();
            rpta.Estado = new List<TCRM_EstadoMatriculaInsertarDTO>();
            var query = _dapper.QuerySPDapper("fin.SP_EditarEstadoMatricula", new
            {
                Id = Id,
                EstadoMatricula = EstadoMatricula,
                UsuarioModificacion = UsuarioModificacion,
                FechaModificacion = FechaModificacion,
                IdSubEstados = IdSubEstados,
            });

            if (!string.IsNullOrEmpty(query) && !query.Contains("[]"))
            {
                rpta.Estado = JsonConvert.DeserializeObject<List<TCRM_EstadoMatriculaInsertarDTO>>(query);

            }
            return rpta;
        }
        public SubEstadoMatriculaDTO EliminarSubEstado(int Id)
        {
            SubEstadoMatriculaDTO rpta = new SubEstadoMatriculaDTO();
            rpta.SubEstado = new List<TCRM_SubEstadoMatriculaDTO>();
            var query = _dapper.QuerySPDapper("fin.SP_EliminarSubEstado", new
            {
                Id = Id
            });

            if (!string.IsNullOrEmpty(query) && !query.Contains("[]"))
            {
                rpta.SubEstado = JsonConvert.DeserializeObject<List<TCRM_SubEstadoMatriculaDTO>>(query);

            }
            return rpta;
        }

        public SubEstadoMatriculaEnvioAutomatico ObtenerSubEstadoMatriculaEnvioAutomatico(int IdSubEstadoMatricula)
        {
            try
            {
                SubEstadoMatriculaEnvioAutomatico subEstadoMatriculado = new SubEstadoMatriculaEnvioAutomatico();
                var _query = "Select Id,Nombre From fin.T_SubEstadoMatricula Where Estado=1 and Id = @IdSubEstadoMatricula";
                var SubEstadoDB = _dapper.FirstOrDefault(_query, new { IdSubEstadoMatricula });
                if (!string.IsNullOrEmpty(SubEstadoDB) && !SubEstadoDB.Contains("[]"))
                {
                    subEstadoMatriculado = JsonConvert.DeserializeObject<SubEstadoMatriculaEnvioAutomatico>(SubEstadoDB);
                }
                return subEstadoMatriculado;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public EstadoMatriculaEnvioAutomatico ObtenerEstadoMatriculaEnvioAutomatico(int IdEstadoMatricula)
        {
            try
            {
                EstadoMatriculaEnvioAutomatico EstadoMatriculado = new EstadoMatriculaEnvioAutomatico();
                var _query = "Select Id,Estado_Matricula AS Nombre From fin.T_EstadoMatricula Where Estado=1 and Id = @IdEstadoMatricula";
                var EstadoDB = _dapper.FirstOrDefault(_query, new { IdEstadoMatricula });
                if (!string.IsNullOrEmpty(EstadoDB) && !EstadoDB.Contains("[]"))
                {
                    EstadoMatriculado = JsonConvert.DeserializeObject<EstadoMatriculaEnvioAutomatico>(EstadoDB);
                }
                return EstadoMatriculado;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
