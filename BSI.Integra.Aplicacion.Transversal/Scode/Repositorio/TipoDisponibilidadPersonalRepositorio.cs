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
using System.Linq;
using BSI.Integra.Aplicacion.Transversal.Helper;
using static BSI.Integra.Aplicacion.Base.Enums.Enums;
using BSI.Integra.Aplicacion.DTOs.Scode.DTOs.Transversal;

namespace BSI.Integra.Aplicacion.Transversal.Scode.Repositorio
{
    public class TipoDisponibilidadPersonalRepositorio : BaseRepository<TTipoDisponibilidadPersonal, TipoDisponibilidadPersonalBO>
    {
        #region Metodos Base
        public TipoDisponibilidadPersonalRepositorio() : base()
        {
        }
        public TipoDisponibilidadPersonalRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<TipoDisponibilidadPersonalBO> GetBy(Expression<Func<TTipoDisponibilidadPersonal, bool>> filter)
        {
            IEnumerable<TTipoDisponibilidadPersonal> listado = base.GetBy(filter);
            List<TipoDisponibilidadPersonalBO> listadoBO = new List<TipoDisponibilidadPersonalBO>();
            foreach (var itemEntidad in listado)
            {
                TipoDisponibilidadPersonalBO objetoBO = Mapper.Map<TTipoDisponibilidadPersonal, TipoDisponibilidadPersonalBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public TipoDisponibilidadPersonalBO FirstById(int id)
        {
            try
            {
                TTipoDisponibilidadPersonal entidad = base.FirstById(id);
                TipoDisponibilidadPersonalBO objetoBO = new TipoDisponibilidadPersonalBO();
                Mapper.Map<TTipoDisponibilidadPersonal, TipoDisponibilidadPersonalBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public TipoDisponibilidadPersonalBO FirstBy(Expression<Func<TTipoDisponibilidadPersonal, bool>> filter)
        {
            try
            {
                TTipoDisponibilidadPersonal entidad = base.FirstBy(filter);
                TipoDisponibilidadPersonalBO objetoBO = Mapper.Map<TTipoDisponibilidadPersonal, TipoDisponibilidadPersonalBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public bool Insert(TipoDisponibilidadPersonalBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TTipoDisponibilidadPersonal entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<TipoDisponibilidadPersonalBO> listadoBO)
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

        public bool Update(TipoDisponibilidadPersonalBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TTipoDisponibilidadPersonal entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<TipoDisponibilidadPersonalBO> listadoBO)
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
        private void AsignacionId(TTipoDisponibilidadPersonal entidad, TipoDisponibilidadPersonalBO objetoBO)
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

        private TTipoDisponibilidadPersonal MapeoEntidad(TipoDisponibilidadPersonalBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TTipoDisponibilidadPersonal entidad = new TTipoDisponibilidadPersonal();
                entidad = Mapper.Map<TipoDisponibilidadPersonalBO, TTipoDisponibilidadPersonal>(objetoBO,
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

        public List<TipoDisponibilidadPersonalDTO> ObtenerTipoDisponibilidadPersonal()
        {
            try
            {
                List<TipoDisponibilidadPersonalDTO> beneficiosCodigoMatricula = new List<TipoDisponibilidadPersonalDTO>();
                var _query = "SELECT Id,Nombre , GeneraCosto FROM [ope].[V_TipoDisponibilidadPersonal]";
                var beneficiosCodigoMatriculaDB = _dapper.QueryDapper(_query, new { });
                if (!string.IsNullOrEmpty(beneficiosCodigoMatriculaDB) && !beneficiosCodigoMatriculaDB.Contains("[]"))
                {
                    beneficiosCodigoMatricula = JsonConvert.DeserializeObject<List<TipoDisponibilidadPersonalDTO>>(beneficiosCodigoMatriculaDB);
                }
                return beneficiosCodigoMatricula;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        

        public TipoDisponibilidadPersonalDTO InsertarTipoDisponibilidadPersonal(string Nombre, bool GeneraCosto, string Usuario)
        {
            try
            {
                TipoDisponibilidadPersonalDTO rpta = new TipoDisponibilidadPersonalDTO();
                //var _query = "com.SP_InsertarHabilidadSimulador";
                string query = _dapper.QuerySPFirstOrDefault("com.SP_InsertarTipoDisponibilidadPersonal", new { Nombre, GeneraCosto, Usuario });
                //var beneficiosCodigoMatriculaDB = _dapper.QuerySPDapper(_query, new { Nombre, GeneraCosto,Usuario });
                if (!string.IsNullOrEmpty(query) && !query.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<TipoDisponibilidadPersonalDTO>(query);

                }

                return rpta;

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public int ActualizarTipoDisponibilidadPersonal(int Id, string Nombre, bool GeneraCosto, string Usuario)
        {
            try
            {

                var _query = "com.SP_ActualizarTipoDisponibilidadPersonal";
                var beneficiosCodigoMatriculaDB = _dapper.QuerySPDapper(_query, new { Id, Nombre, GeneraCosto, Usuario });
                if (!string.IsNullOrEmpty(beneficiosCodigoMatriculaDB) && !beneficiosCodigoMatriculaDB.Contains("[]"))
                {
                    return 1;
                }
                return 0;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public int EliminarTipoDisponibilidadPersonal(int Id)
        {
            try
            {

                var _query = "com.SP_EliminarTipoDisponibilidadPersonal";
                var beneficiosCodigoMatriculaDB = _dapper.QuerySPDapper(_query, new { Id });
                if (!string.IsNullOrEmpty(beneficiosCodigoMatriculaDB) && !beneficiosCodigoMatriculaDB.Contains("[]"))
                {
                    return 1;
                }
                return 0;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool FirstByIdTipoDisponibilidadPersonal(int Id)
        {
            try
            {
                bool existe = false;

                var _query = "SELECT Id,Nombre , GeneraCosto FROM [ope].[V_TipoDisponibilidadPersonal] where Id=@Id";
                var beneficiosCodigoMatriculaDB = _dapper.FirstOrDefault(_query, new { Id });
                if (!string.IsNullOrEmpty(beneficiosCodigoMatriculaDB) && !beneficiosCodigoMatriculaDB.Contains("[]"))
                {
                    existe = true;
                }
                else
                {
                    existe = false;
                }
                return existe;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }


        public List<TipoDisponibilidadPersonalDTO> ObtenerTipoEstadoRiesgo()
        {
            try
            {
                List<TipoDisponibilidadPersonalDTO> beneficiosCodigoMatricula = new List<TipoDisponibilidadPersonalDTO>();
                var _query = "SELECT Id,Nombre FROM [pla].[V_TipoEstadoRiesgo_Nombre]";
                var beneficiosCodigoMatriculaDB = _dapper.QueryDapper(_query, new { });
                if (!string.IsNullOrEmpty(beneficiosCodigoMatriculaDB) && !beneficiosCodigoMatriculaDB.Contains("[]"))
                {
                    beneficiosCodigoMatricula = JsonConvert.DeserializeObject<List<TipoDisponibilidadPersonalDTO>>(beneficiosCodigoMatriculaDB);
                }
                return beneficiosCodigoMatricula;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }


        public TipoDisponibilidadPersonalDTO InsertarTipoEstadoRiesgo(string Nombre,  string Usuario)
        {
            try
            {
                TipoDisponibilidadPersonalDTO rpta = new TipoDisponibilidadPersonalDTO();
                //var _query = "com.SP_InsertarHabilidadSimulador";
                string query = _dapper.QuerySPFirstOrDefault("pla.SP_InsertarTipoEstadoRiesgo", new { Nombre,  Usuario });
                //var beneficiosCodigoMatriculaDB = _dapper.QuerySPDapper(_query, new { Nombre, GeneraCosto,Usuario });
                if (!string.IsNullOrEmpty(query) && !query.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<TipoDisponibilidadPersonalDTO>(query);

                }

                return rpta;

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public int ActualizarTipoEstadoRiesgo(int Id, string Nombre, string Usuario)
        {
            try
            {

                var _query = "com.SP_ActualizarTipoEstadoRiesgo";
                var beneficiosCodigoMatriculaDB = _dapper.QuerySPDapper(_query, new { Id, Nombre, Usuario });
                if (!string.IsNullOrEmpty(beneficiosCodigoMatriculaDB) && !beneficiosCodigoMatriculaDB.Contains("[]"))
                {
                    return 1;
                }
                return 0;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public int EliminarTipoEstadoRiesgo(int Id)
        {
            try
            {

                var _query = "pla.SP_EliminarTipoEstadoRiesgo";
                var beneficiosCodigoMatriculaDB = _dapper.QuerySPDapper(_query, new { Id });
                if (!string.IsNullOrEmpty(beneficiosCodigoMatriculaDB) && !beneficiosCodigoMatriculaDB.Contains("[]"))
                {
                    return 1;
                }
                return 0;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool FirstByIdTipoEstadoRiesgo(int Id)
        {
            try
            {
                bool existe = false;

                var _query = "SELECT Id,Nombre FROM [ope].[V_TipoEstadoRiesgo_Nombre] where Id=@Id";
                var beneficiosCodigoMatriculaDB = _dapper.FirstOrDefault(_query, new { Id });
                if (!string.IsNullOrEmpty(beneficiosCodigoMatriculaDB) && !beneficiosCodigoMatriculaDB.Contains("[]"))
                {
                    existe = true;
                }
                else
                {
                    existe = false;
                }
                return existe;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
