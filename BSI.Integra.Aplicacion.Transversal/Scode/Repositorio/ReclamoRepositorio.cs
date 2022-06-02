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
    public class ReclamoRepositorio : BaseRepository<TReclamo, ReclamoBO>
    {
        #region Metodos Base
        public ReclamoRepositorio() : base()
        {
        }
        public ReclamoRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<ReclamoBO> GetBy(Expression<Func<TReclamo, bool>> filter)
        {
            IEnumerable<TReclamo> listado = base.GetBy(filter);
            List<ReclamoBO> listadoBO = new List<ReclamoBO>();
            foreach (var itemEntidad in listado)
            {
                ReclamoBO objetoBO = Mapper.Map<TReclamo, ReclamoBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public ReclamoBO FirstById(int id)
        {
            try
            {
                TReclamo entidad = base.FirstById(id);
                ReclamoBO objetoBO = new ReclamoBO();
                Mapper.Map<TReclamo, ReclamoBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public ReclamoBO FirstBy(Expression<Func<TReclamo, bool>> filter)
        {
            try
            {
                TReclamo entidad = base.FirstBy(filter);
                ReclamoBO objetoBO = Mapper.Map<TReclamo, ReclamoBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(ReclamoBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TReclamo entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<ReclamoBO> listadoBO)
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

        public bool Update(ReclamoBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TReclamo entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<ReclamoBO> listadoBO)
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
        private void AsignacionId(TReclamo entidad, ReclamoBO objetoBO)
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

        private TReclamo MapeoEntidad(ReclamoBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TReclamo entidad = new TReclamo();
                entidad = Mapper.Map<ReclamoBO, TReclamo>(objetoBO,
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

        public List<ReclamoDescripcionIdMatriculaDTO> ListarReclamosIdMatricula(int idMatricula)
        {
            try
            {

                List<ReclamoDescripcionIdMatriculaDTO> programasGenerales = new List<ReclamoDescripcionIdMatriculaDTO>();
                var _query = string.Empty;
                _query = "SELECT Id,Reclamo, EstadoMatricula FROM mkt.V_DatosReclamo WHERE IdMatricula = @idMatricula";
                var pgeneralDB = _dapper.QueryDapper(_query, new { idMatricula = idMatricula });
                if (!string.IsNullOrEmpty(pgeneralDB) && !pgeneralDB.Contains("[]"))
                {
                    programasGenerales = JsonConvert.DeserializeObject<List<ReclamoDescripcionIdMatriculaDTO>>(pgeneralDB);
                }
                return programasGenerales;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }
        //Devuelve todos los reclamos
        public List<ListarReclamosDTO> ListarReclamos()
        {
            try
            {

                List<ListarReclamosDTO> programasGenerales = new List<ListarReclamosDTO>();
                var _query = string.Empty;
                _query = "SELECT Id,IdMatricula,CodigoMatricula,DNI,NombreAlumno,PersonalAsignado,Descripcion,Origen,IdOrigen,CentroCosto,EstadoMatricula,IdEstadoReclamo,ReclamoEstado,FechaUltimaLlamada,FechaUltimoCorreo,FechaUltimoWapp,IdTipoReclamoAlumno,FechaCreacion,TipoReclamoAlumno FROM mkt.V_DatosReclamo";
                var pgeneralDB = _dapper.QueryDapper(_query, null);
                if (!string.IsNullOrEmpty(pgeneralDB) && !pgeneralDB.Contains("[]"))
                {
                    programasGenerales = JsonConvert.DeserializeObject<List<ListarReclamosDTO>>(pgeneralDB);
                }
                return programasGenerales;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }

        //Devuelve todos los reclamos de un alumno en especifico 
        public List<ListarReclamosDTO> ListarReclamosAlumno(int idMatricula)
        {
            try
            {

                List<ListarReclamosDTO> programasGenerales = new List<ListarReclamosDTO>();
                var _query = string.Empty;
                _query = "SELECT Id,IdMatricula,CodigoMatricula,DNI,NombreAlumno,PersonalAsignado,Descripcion,Origen,IdOrigen,CentroCosto,EstadoMatricula,IdEstadoReclamo,ReclamoEstado FROM mkt.V_DatosReclamo  " +
                         "where IdMatricula = @idMatricula and (IdReclamoEstado = 2 or IdReclamoEstado =4)";
                var pgeneralDB = _dapper.QueryDapper(_query, new { idMatricula = idMatricula });
                if (!string.IsNullOrEmpty(pgeneralDB) && !pgeneralDB.Contains("[]"))
                {
                    programasGenerales = JsonConvert.DeserializeObject<List<ListarReclamosDTO>>(pgeneralDB);
                }
                return programasGenerales;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }


        //Eliminar Reclamo
        public List<ReclamoDTO> EliminarReclamo(int Id, string Usuario)
        {
            try
            {
                string _queryEliminar = "mkt.SP_EliminarReclamo";
                var queryDelete = _dapper.QuerySPDapper(_queryEliminar, new
                {
                    Id = Id,
                    Usuario = Usuario
                });
                return JsonConvert.DeserializeObject<List<ReclamoDTO>>(queryDelete);

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        //Se cambia el estado a Enviado
        public List<ReclamoDTO> EnviarReclamo(int Id, string Usuario)
        {
            try
            {
                string _queryEliminar = "mkt.SP_EnviarReclamo";
                var queryDelete = _dapper.QuerySPDapper(_queryEliminar, new
                {
                    Id = Id,
                    Usuario = Usuario
                });
                return JsonConvert.DeserializeObject<List<ReclamoDTO>>(queryDelete);

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        //Se cambia el estado del reclamo a Revisado
        public List<ReclamoDTO> ConfirmarReclamo(int Id, string Usuario)
        {
            try
            {
                string _queryEliminar = "mkt.SP_ConfirmarReclamo";
                var queryDelete = _dapper.QuerySPDapper(_queryEliminar, new
                {
                    Id = Id,
                    Usuario = Usuario
                });
                return JsonConvert.DeserializeObject<List<ReclamoDTO>>(queryDelete);

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}

    
