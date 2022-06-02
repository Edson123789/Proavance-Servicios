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
    public class TipoDocumentoAlumnoRepositorio : BaseRepository<TTipoDocumentoAlumno, TipoDocumentoAlumnoBO>
    {
        #region Metodos Base
        public TipoDocumentoAlumnoRepositorio() : base()
        {
        }
        public TipoDocumentoAlumnoRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<TipoDocumentoAlumnoBO> GetBy(Expression<Func<TTipoDocumentoAlumno, bool>> filter)
        {
            IEnumerable<TTipoDocumentoAlumno> listado = base.GetBy(filter);
            List<TipoDocumentoAlumnoBO> listadoBO = new List<TipoDocumentoAlumnoBO>();
            foreach (var itemEntidad in listado)
            {
                TipoDocumentoAlumnoBO objetoBO = Mapper.Map<TTipoDocumentoAlumno, TipoDocumentoAlumnoBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public TipoDocumentoAlumnoBO FirstById(int id)
        {
            try
            {
                TTipoDocumentoAlumno entidad = base.FirstById(id);
                TipoDocumentoAlumnoBO objetoBO = new TipoDocumentoAlumnoBO();
                Mapper.Map<TTipoDocumentoAlumno, TipoDocumentoAlumnoBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public TipoDocumentoAlumnoBO FirstBy(Expression<Func<TTipoDocumentoAlumno, bool>> filter)
        {
            try
            {
                TTipoDocumentoAlumno entidad = base.FirstBy(filter);
                TipoDocumentoAlumnoBO objetoBO = Mapper.Map<TTipoDocumentoAlumno, TipoDocumentoAlumnoBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(TipoDocumentoAlumnoBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TTipoDocumentoAlumno entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<TipoDocumentoAlumnoBO> listadoBO)
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

        public bool Update(TipoDocumentoAlumnoBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TTipoDocumentoAlumno entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<TipoDocumentoAlumnoBO> listadoBO)
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
        private void AsignacionId(TTipoDocumentoAlumno entidad, TipoDocumentoAlumnoBO objetoBO)
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

        private TTipoDocumentoAlumno MapeoEntidad(TipoDocumentoAlumnoBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TTipoDocumentoAlumno entidad = new TTipoDocumentoAlumno();
                entidad = Mapper.Map<TipoDocumentoAlumnoBO, TTipoDocumentoAlumno>(objetoBO,
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

        public List<ObtenerTipoDocumentoAlumnoDTO> ListarTipoDocumentoAlumno()
        {
            try
            {

                List<ObtenerTipoDocumentoAlumnoDTO> tipodocumentoalumno = new List<ObtenerTipoDocumentoAlumnoDTO>();
                var _query = string.Empty;
                _query = "SELECT Id,Nombre,IdPlantillaFrontal,IdPlantillaPosterior,IdOperadorComparacion,TieneDeuda FROM [ope].[V_TipoDocumentoAlumno] where Estado = 1";
                var tipodocumento = _dapper.QueryDapper(_query, null);
                if (!string.IsNullOrEmpty(tipodocumento) && !tipodocumento.Contains("[]"))
                {
                    tipodocumentoalumno = JsonConvert.DeserializeObject<List<ObtenerTipoDocumentoAlumnoDTO>>(tipodocumento);
                }
                return tipodocumentoalumno;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }


        public List<TipoDocumentoQueryDTO> ObtenerGrupoDetallesGrilla(int IdtipoDoc)
        {
            try
            {
                List<TipoDocumentoQueryDTO> EvaluacionGrupo = new List<TipoDocumentoQueryDTO>();
                var campos = "Id,ModalidadCurso, EstadoMatricula, SubEstadoMatricula, OperadorComparacion,TieneDeuda";

                var _query = "SELECT " + campos + " FROM  [ope].[V_CondicionTipoDocumentoAlumno]  where Id=" + IdtipoDoc;
                var dataDB = _dapper.QueryDapper(_query, null);
                if (!dataDB.Contains("[]") && !string.IsNullOrEmpty(dataDB))
                {
                    EvaluacionGrupo = JsonConvert.DeserializeObject<List<TipoDocumentoQueryDTO>>(dataDB);
                }
                return EvaluacionGrupo;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
