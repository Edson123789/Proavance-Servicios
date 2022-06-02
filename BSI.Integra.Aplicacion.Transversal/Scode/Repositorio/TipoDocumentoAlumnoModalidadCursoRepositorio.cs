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
   public class TipoDocumentoAlumnoModalidadCursoRepositorio : BaseRepository<TTipoDocumentoAlumnoModalidadCurso, TipoDocumentoAlumnoModalidadCursoBO>
    {
        #region Metodos Base
        public TipoDocumentoAlumnoModalidadCursoRepositorio() : base()
        {
        }
        public TipoDocumentoAlumnoModalidadCursoRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<TipoDocumentoAlumnoModalidadCursoBO> GetBy(Expression<Func<TTipoDocumentoAlumnoModalidadCurso, bool>> filter)
        {
            IEnumerable<TTipoDocumentoAlumnoModalidadCurso> listado = base.GetBy(filter);
            List<TipoDocumentoAlumnoModalidadCursoBO> listadoBO = new List<TipoDocumentoAlumnoModalidadCursoBO>();
            foreach (var itemEntidad in listado)
            {
                TipoDocumentoAlumnoModalidadCursoBO objetoBO = Mapper.Map<TTipoDocumentoAlumnoModalidadCurso, TipoDocumentoAlumnoModalidadCursoBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public TipoDocumentoAlumnoModalidadCursoBO FirstById(int id)
        {
            try
            {
                TTipoDocumentoAlumnoModalidadCurso entidad = base.FirstById(id);
                TipoDocumentoAlumnoModalidadCursoBO objetoBO = new TipoDocumentoAlumnoModalidadCursoBO();
                Mapper.Map<TTipoDocumentoAlumnoModalidadCurso, TipoDocumentoAlumnoModalidadCursoBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public TipoDocumentoAlumnoModalidadCursoBO FirstBy(Expression<Func<TTipoDocumentoAlumnoModalidadCurso, bool>> filter)
        {
            try
            {
                TTipoDocumentoAlumnoModalidadCurso entidad = base.FirstBy(filter);
                TipoDocumentoAlumnoModalidadCursoBO objetoBO = Mapper.Map<TTipoDocumentoAlumnoModalidadCurso, TipoDocumentoAlumnoModalidadCursoBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(TipoDocumentoAlumnoModalidadCursoBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TTipoDocumentoAlumnoModalidadCurso entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<TipoDocumentoAlumnoModalidadCursoBO> listadoBO)
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

        public bool Update(TipoDocumentoAlumnoModalidadCursoBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TTipoDocumentoAlumnoModalidadCurso entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<TipoDocumentoAlumnoModalidadCursoBO> listadoBO)
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
        private void AsignacionId(TTipoDocumentoAlumnoModalidadCurso entidad, TipoDocumentoAlumnoModalidadCursoBO objetoBO)
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

        private TTipoDocumentoAlumnoModalidadCurso MapeoEntidad(TipoDocumentoAlumnoModalidadCursoBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TTipoDocumentoAlumnoModalidadCurso entidad = new TTipoDocumentoAlumnoModalidadCurso();
                entidad = Mapper.Map<TipoDocumentoAlumnoModalidadCursoBO, TTipoDocumentoAlumnoModalidadCurso>(objetoBO,
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
                string _query = "SELECT Id FROM  ope.T_TipoDocumentoAlumnoModalidadCurso WHERE Estado = 1 and IdTipoDocumentoAlumno = @IdTipoDocumento ";
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

        public List<TipoDocumentoAlumnoModalidadCursoDTO> ListarTipoDocumentoAlumnoModalidadCursoPorId(int IdTipoDocumentoAlumno)
        {
            try
            {
                List<TipoDocumentoAlumnoModalidadCursoDTO> modalidadfiltro = new List<TipoDocumentoAlumnoModalidadCursoDTO>();
                var _query = "Select IdModalidad FROM ope.T_TipoDocumentoAlumnoModalidadCurso where Estado = 1 and IdTipoDocumentoAlumno = @IdTipoDocumentoAlumno";
                var Subfiltro = _dapper.QueryDapper(_query, new { IdTipoDocumentoAlumno });
                if (!string.IsNullOrEmpty(Subfiltro) && !Subfiltro.Contains("[]"))
                {
                    modalidadfiltro = JsonConvert.DeserializeObject<List<TipoDocumentoAlumnoModalidadCursoDTO>>(Subfiltro);
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
