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

namespace BSI.Integra.Aplicacion.Transversal.Scode.Repositorio
{
   public class TipoDocumentoAlumnoEstadoMatriculaRepositorio : BaseRepository<TTipoDocumentoAlumnoEstadoMatricula, TipoDocumentoAlumnoEstadoMatriculaBO>
    {
        #region Metodos Base
        public TipoDocumentoAlumnoEstadoMatriculaRepositorio() : base()
        {
        }
        public TipoDocumentoAlumnoEstadoMatriculaRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<TipoDocumentoAlumnoEstadoMatriculaBO> GetBy(Expression<Func<TTipoDocumentoAlumnoEstadoMatricula, bool>> filter)
        {
            IEnumerable<TTipoDocumentoAlumnoEstadoMatricula> listado = base.GetBy(filter);
            List<TipoDocumentoAlumnoEstadoMatriculaBO> listadoBO = new List<TipoDocumentoAlumnoEstadoMatriculaBO>();
            foreach (var itemEntidad in listado)
            {
                TipoDocumentoAlumnoEstadoMatriculaBO objetoBO = Mapper.Map<TTipoDocumentoAlumnoEstadoMatricula, TipoDocumentoAlumnoEstadoMatriculaBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public TipoDocumentoAlumnoEstadoMatriculaBO FirstById(int id)
        {
            try
            {
                TTipoDocumentoAlumnoEstadoMatricula entidad = base.FirstById(id);
                TipoDocumentoAlumnoEstadoMatriculaBO objetoBO = new TipoDocumentoAlumnoEstadoMatriculaBO();
                Mapper.Map<TTipoDocumentoAlumnoEstadoMatricula, TipoDocumentoAlumnoEstadoMatriculaBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public TipoDocumentoAlumnoEstadoMatriculaBO FirstBy(Expression<Func<TTipoDocumentoAlumnoEstadoMatricula, bool>> filter)
        {
            try
            {
                TTipoDocumentoAlumnoEstadoMatricula entidad = base.FirstBy(filter);
                TipoDocumentoAlumnoEstadoMatriculaBO objetoBO = Mapper.Map<TTipoDocumentoAlumnoEstadoMatricula, TipoDocumentoAlumnoEstadoMatriculaBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(TipoDocumentoAlumnoEstadoMatriculaBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TTipoDocumentoAlumnoEstadoMatricula entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<TipoDocumentoAlumnoEstadoMatriculaBO> listadoBO)
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

        public bool Update(TipoDocumentoAlumnoEstadoMatriculaBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TTipoDocumentoAlumnoEstadoMatricula entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<TipoDocumentoAlumnoEstadoMatriculaBO> listadoBO)
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
        private void AsignacionId(TTipoDocumentoAlumnoEstadoMatricula entidad, TipoDocumentoAlumnoEstadoMatriculaBO objetoBO)
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

        private TTipoDocumentoAlumnoEstadoMatricula MapeoEntidad(TipoDocumentoAlumnoEstadoMatriculaBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TTipoDocumentoAlumnoEstadoMatricula entidad = new TTipoDocumentoAlumnoEstadoMatricula();
                entidad = Mapper.Map<TipoDocumentoAlumnoEstadoMatriculaBO, TTipoDocumentoAlumnoEstadoMatricula>(objetoBO,
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

        public void DeleteLogicoPorTipoDocumento(int IdTipoDocumento, string usuario, List<int> nuevos)
        {
            try
            {
                List<EliminacionIdsDTO> listaBorrar = new List<EliminacionIdsDTO>();
                string _query = "SELECT Id FROM  ope.T_TipoDocumentoAlumnoEstadoMatricula WHERE Estado = 1 and IdTipoDocumentoAlumno = @IdTipoDocumento ";
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

        public List<TipoDocumentoAlumnoEstadoMatriculaDTO> ListarTipoDocumentoAlumnoEstadoPorId(int IdTipoDocumentoAlumno)
        {
            try
            {
                List<TipoDocumentoAlumnoEstadoMatriculaDTO> modalidadfiltro = new List<TipoDocumentoAlumnoEstadoMatriculaDTO>();
                var _query = "Select IdEstadoMatricula FROM ope.T_TipoDocumentoAlumnoEstadoMatricula where Estado = 1 and IdTipoDocumentoAlumno = @IdTipoDocumentoAlumno";
                var Subfiltro = _dapper.QueryDapper(_query, new { IdTipoDocumentoAlumno });
                if (!string.IsNullOrEmpty(Subfiltro) && !Subfiltro.Contains("[]"))
                {
                    modalidadfiltro = JsonConvert.DeserializeObject<List<TipoDocumentoAlumnoEstadoMatriculaDTO>>(Subfiltro);
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
