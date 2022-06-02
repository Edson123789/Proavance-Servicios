using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using AutoMapper;
using BSI.Integra.Aplicacion.Planificacion.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using BSI.Integra.Aplicacion.DTOs;
using Newtonsoft.Json;
using System.Linq;

namespace BSI.Integra.Aplicacion.Planificacion.Repositorio
{
    public class TroncalCiudadRepositorio : BaseRepository<TTroncalCiudad, TroncalCiudadBO>
    {
        #region Metodos Base
        public TroncalCiudadRepositorio() : base()
        {
        }
        public TroncalCiudadRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<TroncalCiudadBO> GetBy(Expression<Func<TTroncalCiudad, bool>> filter)
        {
            IEnumerable<TTroncalCiudad> listado = base.GetBy(filter);
            List<TroncalCiudadBO> listadoBO = new List<TroncalCiudadBO>();
            foreach (var itemEntidad in listado)
            {
                TroncalCiudadBO objetoBO = Mapper.Map<TTroncalCiudad, TroncalCiudadBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public TroncalCiudadBO FirstById(int id)
        {
            try
            {
                TTroncalCiudad entidad = base.FirstById(id);
                TroncalCiudadBO objetoBO = new TroncalCiudadBO();
                Mapper.Map<TTroncalCiudad, TroncalCiudadBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public TroncalCiudadBO FirstBy(Expression<Func<TTroncalCiudad, bool>> filter)
        {
            try
            {
                TTroncalCiudad entidad = base.FirstBy(filter);
                TroncalCiudadBO objetoBO = Mapper.Map<TTroncalCiudad, TroncalCiudadBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(TroncalCiudadBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TTroncalCiudad entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<TroncalCiudadBO> listadoBO)
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

        public bool Update(TroncalCiudadBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TTroncalCiudad entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<TroncalCiudadBO> listadoBO)
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
        private void AsignacionId(TTroncalCiudad entidad, TroncalCiudadBO objetoBO)
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

        private TTroncalCiudad MapeoEntidad(TroncalCiudadBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TTroncalCiudad entidad = new TTroncalCiudad();
                entidad = Mapper.Map<TroncalCiudadBO, TTroncalCiudad>(objetoBO,
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

        /// <summary>
        /// Obtiene Lista de codigo de ciudades
        /// </summary>
        /// <returns></returns>
        public List<CiudadTroncalPaisFiltroDTO> ObtenerTroncalCiudadFiltro()
        {
            try
            {
                List<CiudadTroncalPaisFiltroDTO> troncalFiltro = new List<CiudadTroncalPaisFiltroDTO>();
                string _queryTroncal = string.Empty;
                _queryTroncal = "SELECT CodigoCiudad,NombreCiudad,IdPais FROM pla.V_TSede_Filtro WHERE EstadoSede=1 and EstadoTroncal=1";
                var queryTroncal = _dapper.QueryDapper(_queryTroncal, null);
                if (!string.IsNullOrEmpty(queryTroncal) && !queryTroncal.Contains("[]"))
                {
                    troncalFiltro = JsonConvert.DeserializeObject<List<CiudadTroncalPaisFiltroDTO>>(queryTroncal);
                }
                return troncalFiltro;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        /// <summary>
        /// Obtiene Lista de codigo de ciudades
        /// </summary>
        /// <returns></returns>
        public List<CiudadTroncalPaisFiltroDTO> ObtenerCiudadPorFeriado(int idCiudad, int IdPais)
        {
            try
            {
                var ciudades = ObtenerTroncalCiudadFiltro();
                var ciudad = ciudades.Where(x => x.CodigoCiudad == idCiudad).FirstOrDefault();
                var listaciudades = ciudades.Where(x => x.IdPais == IdPais).ToList();

                return (listaciudades);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        public List<FiltroDTO> ObtenerTroncalCiudad()
        {
            try
            {
                string _queryTroncal = string.Empty;
                _queryTroncal = "SELECT Id,Nombre FROM pla.V_TTroncalCiudad_IdNombre WHERE Estado=1";
                var CentroCostoTroncal = _dapper.QueryDapper(_queryTroncal, null);
                return JsonConvert.DeserializeObject<List<FiltroDTO>>(CentroCostoTroncal);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }


    }
}