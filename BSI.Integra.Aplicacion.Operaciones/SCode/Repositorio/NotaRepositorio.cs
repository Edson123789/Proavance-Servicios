using AutoMapper;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.DTOs.Operaciones;
using BSI.Integra.Aplicacion.Operaciones.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace BSI.Integra.Aplicacion.Operaciones.Repositorio
{
    public class NotaRepositorio : BaseRepository<TNota, NotaBO>
    {
        #region Metodos Base
        public NotaRepositorio() : base()
        {
        }
        public NotaRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<NotaBO> GetBy(Expression<Func<TNota, bool>> filter)
        {
            IEnumerable<TNota> listado = base.GetBy(filter);
            List<NotaBO> listadoBO = new List<NotaBO>();
            foreach (var itemEntidad in listado)
            {
                NotaBO objetoBO = Mapper.Map<TNota, NotaBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public NotaBO FirstById(int id)
        {
            try
            {
                TNota entidad = base.FirstById(id);
                NotaBO objetoBO = new NotaBO();
                Mapper.Map<TNota, NotaBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public NotaBO FirstBy(Expression<Func<TNota, bool>> filter)
        {
            try
            {
                TNota entidad = base.FirstBy(filter);
                NotaBO objetoBO = Mapper.Map<TNota, NotaBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(NotaBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TNota entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<NotaBO> listadoBO)
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

        public bool Update(NotaBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TNota entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<NotaBO> listadoBO)
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
        private void AsignacionId(TNota entidad, NotaBO objetoBO)
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

        private TNota MapeoEntidad(NotaBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TNota entidad = new TNota();
                entidad = Mapper.Map<NotaBO, TNota>(objetoBO,
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

        public IEnumerable<DocentePorGrupoListadoDTO> ListadoCcPorDocente(int idDocente, string filtro)
        {
            try
            {
                var query = "select * from ope.V_PEspecifico_DocentePorGrupo where idExpositor = @idDocente";
                var res = _dapper.QueryDapper(query, new { idDocente = idDocente });
                return JsonConvert.DeserializeObject<List<DocentePorGrupoListadoDTO>>(res);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public IEnumerable<DocentePorGrupoListadoDTO> ListadoCcPorDocenteFiltrado(CursoPorDocenteFiltroDTO filtro)
        {
            try
            {
                var query = "pla.SP_ObtenerPEspecifico_Nota_DocentePorGrupo_Filtrado";
                var res = _dapper.QuerySPDapper(query,
                    new
                    {
                        filtro.IdExpositor,
                        filtro.IdProgramaEspecifico, filtro.IdCentroCosto, filtro.IdCodigoBSCiudad, filtro.IdEstadoPEspecifico,
                        filtro.IdModalidadCurso, filtro.IdPGeneral, filtro.IdArea, filtro.IdSubArea,
                        filtro.IdCentroCostoD
                    });
                
                return JsonConvert.DeserializeObject<List<DocentePorGrupoListadoDTO>>(res);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public IEnumerable<AsistenciaListadoDTO> ListadoAsistenciaPorPrograma(int idPespecifico)
        {
            try
            {
                var query = "select * from ope.V_MatriculasActivas_PorPEspecifico where IdPespecifico = @idPespecifico";
                var res = _dapper.QueryDapper(query, new { idPespecifico = idPespecifico });
                return JsonConvert.DeserializeObject<List<AsistenciaListadoDTO>>(res);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public IEnumerable<MatriculaActiva_PEspecifico> ListadoMatriculaPorPrograma(int idPespecifico)
        {
            try
            {
                var query = "select * from ope.V_MatriculasActivas_PorPEspecifico where IdPespecifico = @idPespecifico";
                var res = _dapper.QueryDapper(query, new { idPespecifico = idPespecifico });
                return JsonConvert.DeserializeObject<List<MatriculaActiva_PEspecifico>>(res);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public IEnumerable<MatriculaActiva_PEspecifico> ListadoMatriculaPorProgramaPorGrupo(int idPespecifico, int grupo)
        {
            try
            {
                var query = "select * from ope.V_MatriculasActivas_PorPEspecifico where IdPespecifico = @idPespecifico AND GrupoCurso = @grupo";
                var res = _dapper.QueryDapper(query, new { idPespecifico = idPespecifico, grupo = grupo });
                return JsonConvert.DeserializeObject<List<MatriculaActiva_PEspecifico>>(res);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public IEnumerable<MatriculaActiva_PEspecifico> ListadoMatriculaPorProgramaPorGrupo(int idPespecifico, int grupo,int IdMatriculaCabecera)
        {
            try
            {
                var query = "select * from ope.V_MatriculasActivas_PorPEspecifico where IdPespecifico = @idPespecifico AND GrupoCurso = @grupo AND IdMatriculaCabecera=@IdMatriculaCabecera";
                var res = _dapper.QueryDapper(query, new { idPespecifico = idPespecifico, grupo = grupo , IdMatriculaCabecera = IdMatriculaCabecera });
                return JsonConvert.DeserializeObject<List<MatriculaActiva_PEspecifico>>(res);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public IEnumerable<NotaPresencialDTO> ListadoNotaPorMatriculaCabecera(int IdMatriculaCabecera)
        {
            try
            {
                var query = "select * from [ope].[T_ObtenerNotaAsistenciaPresencialOnline] where IdMatriculaCabecera=@IdMatriculaCabecera";
                var res = _dapper.QueryDapper(query, new { IdMatriculaCabecera = IdMatriculaCabecera });
                return JsonConvert.DeserializeObject<List<NotaPresencialDTO>>(res);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public IEnumerable<NotaPresencialPromedioDTO> ListadoNotaPorMatriculaCabeceraPromedio(int IdMatriculaCabecera)
        {
            try
            {
                var query = "select * from [ope].[T_ObtenerNotaAsistenciaPresencialOnlinePromedio] where IdMatriculaCabecera=@IdMatriculaCabecera";
                var res = _dapper.QueryDapper(query, new { IdMatriculaCabecera = IdMatriculaCabecera });
                return JsonConvert.DeserializeObject<List<NotaPresencialPromedioDTO>>(res);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public IEnumerable<NotaPresencialPromedioEspecificoDTO> ListadoNotaPorMatriculaCabeceraPromedioIdEspecifico(int IdMatriculaCabecera, int IdPEspecifico)
        {
            try
            {
                var query = "select * from [ope].[T_ObtenerNotaAsistenciaPresencialOnlinePromedioPorIdEspecifico] where IdMatriculaCabecera=@IdMatriculaCabecera AND IdPEspecifico=@IdPEspecifico";
                var res = _dapper.QueryDapper(query, new { IdMatriculaCabecera = IdMatriculaCabecera, IdPEspecifico = IdPEspecifico });
                return JsonConvert.DeserializeObject<List<NotaPresencialPromedioEspecificoDTO>>(res);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public IEnumerable<NotaPresencialDTO> ListadoNotaPorIdAlumno(int IdAlumno)
        {
            try
            {
                var query = "select * from [ope].[T_ObtenerNotaAsistenciaPresencialOnline] where IdAlumno=@IdAlumno";
                var res = _dapper.QueryDapper(query, new { IdAlumno = IdAlumno });
                return JsonConvert.DeserializeObject<List<NotaPresencialDTO>>(res);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public IEnumerable<ActividadCalificableDetalleDTO> ListadoActividadesCalificablesDocentePorCurso(int idMatriculaCabecera, int idPEspecifico, int grupo, int idModalidad)
        {
            try
            {
                var query = "select * from ope.V_ListadoActividadesCalificables_Docente_PorCurso where IdMatriculaCabecera=@idMatriculaCabecera AND IdPEspecifico=@idPEspecifico AND Grupo=@grupo AND IdModalidadCurso=@idModalidad";
                var res = _dapper.QueryDapper(query,
                    new
                    {
                        idMatriculaCabecera = idMatriculaCabecera, idPEspecifico = idPEspecifico, grupo = grupo,
                        idModalidad = idModalidad
                    });
                return JsonConvert.DeserializeObject<List<ActividadCalificableDetalleDTO>>(res);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
