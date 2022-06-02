using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using AutoMapper;
using BSI.Integra.Aplicacion.Base.BO;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Operaciones.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using Newtonsoft.Json;

namespace BSI.Integra.Aplicacion.Operaciones.Repositorio
{
    public class RaSesionBitacoraRepositorio : BaseRepository<TRaSesionBitacora, RaSesionBitacoraBO>
    {
        #region Metodos Base
        public RaSesionBitacoraRepositorio() : base()
        {
        }
        public RaSesionBitacoraRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<RaSesionBitacoraBO> GetBy(Expression<Func<TRaSesionBitacora, bool>> filter)
        {
            IEnumerable<TRaSesionBitacora> listado = base.GetBy(filter);
            List<RaSesionBitacoraBO> listadoBO = new List<RaSesionBitacoraBO>();
            foreach (var itemEntidad in listado)
            {
                RaSesionBitacoraBO objetoBO = Mapper.Map<TRaSesionBitacora, RaSesionBitacoraBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }
            

            return listadoBO;
        }
        public RaSesionBitacoraBO FirstById(int id)
        {
            try
            {
                TRaSesionBitacora entidad = base.FirstById(id);
                RaSesionBitacoraBO objetoBO = new RaSesionBitacoraBO();
                Mapper.Map<TRaSesionBitacora, RaSesionBitacoraBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public RaSesionBitacoraBO FirstBy(Expression<Func<TRaSesionBitacora, bool>> filter)
        {
            try
            {
                TRaSesionBitacora entidad = base.FirstBy(filter);
                RaSesionBitacoraBO objetoBO = Mapper.Map<TRaSesionBitacora, RaSesionBitacoraBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(RaSesionBitacoraBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TRaSesionBitacora entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<RaSesionBitacoraBO> listadoBO)
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

        public bool Update(RaSesionBitacoraBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TRaSesionBitacora entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<RaSesionBitacoraBO> listadoBO)
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
        private void AsignacionId(TRaSesionBitacora entidad, RaSesionBitacoraBO objetoBO)
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

        private TRaSesionBitacora MapeoEntidad(RaSesionBitacoraBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TRaSesionBitacora entidad = new TRaSesionBitacora();
                entidad = Mapper.Map<RaSesionBitacoraBO, TRaSesionBitacora>(objetoBO,
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
        /// Obtiene un listado de la cabecera de bitacora filtrado
        /// </summary>
        /// <param name="fecha"></param>
        /// <param name="idRaCentroCosto"></param>
        /// <param name="idRaCurso"></param>
        /// <returns></returns>
        public List<BitacoraSesionCabeceraDTO> ObtenerListadoFiltradoCabecera(DateTime? fecha, int? idRaCentroCosto, int? idRaCurso)
        {
            try
            {
                List<BitacoraSesionCabeceraDTO> listadoCabecera = new List<BitacoraSesionCabeceraDTO>();
                string query = string.Empty;
                string listadoCabeceraDB = string.Empty;
                if (fecha != null)
                {
                    query = "SELECT IdRaCentroCosto, IdRaCurso, NombreCentroCosto, NombreCurso FROM ope.V_ObtenerBitacorasSesion WHERE Fecha = @fecha";
                    listadoCabeceraDB = _dapper.QueryDapper(query, new { fecha = fecha.Value.Date });
                }
                else if (idRaCentroCosto != null && idRaCurso != null){
                    query = "SELECT IdRaCentroCosto, IdRaCurso, NombreCentroCosto, NombreCurso FROM ope.V_ObtenerBitacorasSesion WHERE IdRaCentroCosto = @idRaCentroCosto AND IdRaCurso = @idRaCurso";
                    listadoCabeceraDB = _dapper.QueryDapper(query, new { idRaCentroCosto, idRaCurso });
                }
                if (!string.IsNullOrEmpty(listadoCabeceraDB) && !listadoCabeceraDB.Contains("[]"))
                {
                    listadoCabecera = JsonConvert.DeserializeObject<List<BitacoraSesionCabeceraDTO>>(listadoCabeceraDB);
                }
                return listadoCabecera.DistinctBy(x => new { x.IdRaCentroCosto, x.IdRaCurso, x.NombreCentroCosto, x.NombreCurso }).ToList();
                //return listadoCabecera.GroupBy( x => new { x.IdRaCentroCosto, x.IdRaCurso, x.NombreCentroCosto, x.NombreCurso }).
                //    Select( g => g.First()).ToList();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        


        /// <summary>
        /// Obtiene un listado con el detalle
        /// </summary>
        /// <param name="fecha"></param>
        /// <param name="idRaCentroCosto"></param>
        /// <param name="idRaCurso"></param>
        /// <returns></returns>
        public List<BitacoraSesionDetalleDTO> ObtenerListadoFiltradoDetalle(int idRaCentroCosto, int idRaCurso)
        {
            try
            {
                List<BitacoraSesionDetalleDTO> listadoDetalle = new List<BitacoraSesionDetalleDTO>();
                string query = "SELECT HoraInicio, FechaCreacion, UsuarioCreacion, Detalle FROM ope.V_ObtenerBitacorasSesion WHERE IdRaCentroCosto = @idRaCentroCosto AND IdRaCurso = @idRaCurso";
                var listadoDetalleDB = _dapper.QueryDapper(query,  new { idRaCentroCosto, idRaCurso});
                if (!string.IsNullOrEmpty(listadoDetalleDB) && !listadoDetalleDB.Contains("[]"))
                {
                    listadoDetalle = JsonConvert.DeserializeObject<List<BitacoraSesionDetalleDTO>>(listadoDetalleDB);
                }
                return listadoDetalle;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
