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

namespace BSI.Integra.Aplicacion.Transversal.Repositorio
{
    public class HabilidadSimuladorRepositorio : BaseRepository<THabilidadSimulador, HabilidadSimuladorBO>
    {
        #region Metodos Base
        public HabilidadSimuladorRepositorio() : base()
        {
        }
        public HabilidadSimuladorRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<HabilidadSimuladorBO> GetBy(Expression<Func<THabilidadSimulador, bool>> filter)
        {
            IEnumerable<THabilidadSimulador> listado = base.GetBy(filter);
            List<HabilidadSimuladorBO> listadoBO = new List<HabilidadSimuladorBO>();
            foreach (var itemEntidad in listado)
            {
                HabilidadSimuladorBO objetoBO = Mapper.Map<THabilidadSimulador, HabilidadSimuladorBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public HabilidadSimuladorBO FirstById(int id)
        {
            try
            {
                THabilidadSimulador entidad = base.FirstById(id);
                HabilidadSimuladorBO objetoBO = new HabilidadSimuladorBO();
                Mapper.Map<THabilidadSimulador, HabilidadSimuladorBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public HabilidadSimuladorBO FirstBy(Expression<Func<THabilidadSimulador, bool>> filter)
        {
            try
            {
                THabilidadSimulador entidad = base.FirstBy(filter);
                HabilidadSimuladorBO objetoBO = Mapper.Map<THabilidadSimulador, HabilidadSimuladorBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public bool Insert(HabilidadSimuladorBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                THabilidadSimulador entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<HabilidadSimuladorBO> listadoBO)
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

        public bool Update(HabilidadSimuladorBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                THabilidadSimulador entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<HabilidadSimuladorBO> listadoBO)
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
        private void AsignacionId(THabilidadSimulador entidad, HabilidadSimuladorBO objetoBO)
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

        private THabilidadSimulador MapeoEntidad(HabilidadSimuladorBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                THabilidadSimulador entidad = new THabilidadSimulador();
                entidad = Mapper.Map<HabilidadSimuladorBO, THabilidadSimulador>(objetoBO,
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
        //public HabilidadSimuladorDTO InsertarHabilidadSimulacion(string nombre, int puntajemaximo, int puntajeminimo, string usuario)
        //{
        //    return Ok();
        //}

        public List<HabilidadSimuladorDTO> ObtenerHabilidadesSimulador()
        {
            try
            {
                List<HabilidadSimuladorDTO> beneficiosCodigoMatricula = new List<HabilidadSimuladorDTO>();
                var _query = "SELECT Id,Nombre , PuntajeMaximo,PuntajeMinimo FROM [ope].[V_HabilidadSimulador]";
                var beneficiosCodigoMatriculaDB = _dapper.QueryDapper(_query, new { });
                if (!string.IsNullOrEmpty(beneficiosCodigoMatriculaDB) && !beneficiosCodigoMatriculaDB.Contains("[]"))
                {
                    beneficiosCodigoMatricula = JsonConvert.DeserializeObject<List<HabilidadSimuladorDTO>>(beneficiosCodigoMatriculaDB);
                }
                return beneficiosCodigoMatricula;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public HabilidadSimuladorDTO InsertarHabilidadesSimulador(string Nombre, int PuntajeMaximo, int PuntajeMinimo, string Usuario)
        {
            try
            {
                HabilidadSimuladorDTO rpta = new HabilidadSimuladorDTO();
                //var _query = "com.SP_InsertarHabilidadSimulador";
                string query = _dapper.QuerySPFirstOrDefault("com.SP_InsertarHabilidadSimulador", new { Nombre, PuntajeMinimo, PuntajeMaximo, Usuario });
                //var beneficiosCodigoMatriculaDB = _dapper.QuerySPDapper(_query, new { Nombre, PuntajeMinimo,PuntajeMaximo,Usuario });
                if (!string.IsNullOrEmpty(query) && !query.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<HabilidadSimuladorDTO>(query);

                }

                return rpta;

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public int ActualizarHabilidadesSimulador(int Id, string Nombre, int PuntajeMaximo, int PuntajeMinimo, string Usuario)
        {
            try
            {

                var _query = "com.SP_ActualizarHabilidadSimulador";
                var beneficiosCodigoMatriculaDB = _dapper.QuerySPDapper(_query, new { Id, Nombre, PuntajeMinimo, PuntajeMaximo, Usuario });
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

        public int EliminarHabilidadesSimulador(int Id)
        {
            try
            {

                var _query = "com.SP_EliminarHabilidadSimulador";
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

        public bool FirstByIdHabilidadesSimulador(int Id)
        {
            try
            {
                bool existe = false;

                var _query = "SELECT Id FROM[ope].[V_HabilidadSimulador]where Id=@Id";
                var beneficiosCodigoMatriculaDB = _dapper.FirstOrDefault(_query, new { Id });
                if (!string.IsNullOrEmpty(beneficiosCodigoMatriculaDB) && !beneficiosCodigoMatriculaDB.Contains("[]") && beneficiosCodigoMatriculaDB != "null")
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
