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
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.Transversal.Scode.BO;
using System.Linq;
using BSI.Integra.Aplicacion.DTOs.Scode.DTOs.Transversal;

namespace BSI.Integra.Aplicacion.Transversal.Scode.Repositorio
{
    public class TipoDocumentoAlumnoSubEstadoMatriculaRepositorio : BaseRepository<TTipoDocumentoAlumnoSubEstadoMatricula, TipoDocumentoAlumnoSubEstadoMatriculaBO>
    {
        #region Metodos Base
        public TipoDocumentoAlumnoSubEstadoMatriculaRepositorio() : base()
        {
        }
        public TipoDocumentoAlumnoSubEstadoMatriculaRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<TipoDocumentoAlumnoSubEstadoMatriculaBO> GetBy(Expression<Func<TTipoDocumentoAlumnoSubEstadoMatricula, bool>> filter)
        {
            IEnumerable<TTipoDocumentoAlumnoSubEstadoMatricula> listado = base.GetBy(filter);
            List<TipoDocumentoAlumnoSubEstadoMatriculaBO> listadoBO = new List<TipoDocumentoAlumnoSubEstadoMatriculaBO>();
            foreach (var itemEntidad in listado)
            {
                TipoDocumentoAlumnoSubEstadoMatriculaBO objetoBO = Mapper.Map<TTipoDocumentoAlumnoSubEstadoMatricula, TipoDocumentoAlumnoSubEstadoMatriculaBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public TipoDocumentoAlumnoSubEstadoMatriculaBO FirstById(int id)
        {
            try
            {
                TTipoDocumentoAlumnoSubEstadoMatricula entidad = base.FirstById(id);
                TipoDocumentoAlumnoSubEstadoMatriculaBO objetoBO = new TipoDocumentoAlumnoSubEstadoMatriculaBO();
                Mapper.Map<TTipoDocumentoAlumnoSubEstadoMatricula, TipoDocumentoAlumnoSubEstadoMatriculaBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public TipoDocumentoAlumnoSubEstadoMatriculaBO FirstBy(Expression<Func<TTipoDocumentoAlumnoSubEstadoMatricula, bool>> filter)
        {
            try
            {
                TTipoDocumentoAlumnoSubEstadoMatricula entidad = base.FirstBy(filter);
                TipoDocumentoAlumnoSubEstadoMatriculaBO objetoBO = Mapper.Map<TTipoDocumentoAlumnoSubEstadoMatricula, TipoDocumentoAlumnoSubEstadoMatriculaBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(TipoDocumentoAlumnoSubEstadoMatriculaBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TTipoDocumentoAlumnoSubEstadoMatricula entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<TipoDocumentoAlumnoSubEstadoMatriculaBO> listadoBO)
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

        public bool Update(TipoDocumentoAlumnoSubEstadoMatriculaBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TTipoDocumentoAlumnoSubEstadoMatricula entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<TipoDocumentoAlumnoSubEstadoMatriculaBO> listadoBO)
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
        private void AsignacionId(TTipoDocumentoAlumnoSubEstadoMatricula entidad, TipoDocumentoAlumnoSubEstadoMatriculaBO objetoBO)
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

        private TTipoDocumentoAlumnoSubEstadoMatricula MapeoEntidad(TipoDocumentoAlumnoSubEstadoMatriculaBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TTipoDocumentoAlumnoSubEstadoMatricula entidad = new TTipoDocumentoAlumnoSubEstadoMatricula();
                entidad = Mapper.Map<TipoDocumentoAlumnoSubEstadoMatriculaBO, TTipoDocumentoAlumnoSubEstadoMatricula>(objetoBO,
                    opt => opt.ConfigureMap(MemberList.None));

                ////mapea los hijos
                //if (objetoBO.ListaDetalleCuotas != null && objetoBO.ListaDetalleCuotas.Count > 0)
                //{
                //    foreach (var hijo in objetoBO.ListaDetalleCuotas)
                //    {
                //        TTipoDocumentoAlumnoSubEstadoMatriculaDetalle entidadHijo = new TTipoDocumentoAlumnoSubEstadoMatriculaDetalle();
                //        entidadHijo = Mapper.Map<TipoDocumentoAlumnoSubEstadoMatriculaDetalleBO, TTipoDocumentoAlumnoSubEstadoMatriculaDetalle>(hijo,
                //            opt => opt.ConfigureMap(MemberList.None));
                //        entidad.TTipoDocumentoAlumnoSubEstadoMatriculaDetalle.Add(entidadHijo);
                //    }
                //}

                return entidad;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        #endregion

        public void DeleteLogicoPorTipoDocumento(int IdTipoDocumento, string usuario, List<int> nuevos)
        {
            try
            {
                List<EliminacionIdsDTO> listaBorrar = new List<EliminacionIdsDTO>();
                string _query = "SELECT Id FROM  ope.T_TipoDocumentoAlumnoSubEstadoMatricula WHERE Estado = 1 and IdTipoDocumentoAlumno = @IdTipoDocumento ";
                var query = _dapper.QueryDapper(_query, new { IdTipoDocumento });
                listaBorrar = JsonConvert.DeserializeObject<List<EliminacionIdsDTO>>(query);
                listaBorrar.RemoveAll(x => nuevos.Any(y => y == x.Id));
                foreach (var item in listaBorrar)
                {
                    Delete(item.Id, usuario);
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public List<TipoDocumentoAlumnoSubEstadoMatriculaDTO> ListarTipoDocumentoAlumnoSubEstadoPorId(int IdTipoDocumentoAlumno)
        {
            try
            {
                List<TipoDocumentoAlumnoSubEstadoMatriculaDTO> modalidadfiltro = new List<TipoDocumentoAlumnoSubEstadoMatriculaDTO>();
                var _query = "Select IdSubEstadoMatricula FROM ope.T_TipoDocumentoAlumnoSubEstadoMatricula where Estado = 1 and IdTipoDocumentoAlumno = @IdTipoDocumentoAlumno";
                var Subfiltro = _dapper.QueryDapper(_query, new { IdTipoDocumentoAlumno });
                if (!string.IsNullOrEmpty(Subfiltro) && !Subfiltro.Contains("[]"))
                {
                    modalidadfiltro = JsonConvert.DeserializeObject<List<TipoDocumentoAlumnoSubEstadoMatriculaDTO>>(Subfiltro);
                }
                return modalidadfiltro;
            }
            catch (Exception E)
            {
                throw new Exception(E.Message);
            }
        }
    }
}
