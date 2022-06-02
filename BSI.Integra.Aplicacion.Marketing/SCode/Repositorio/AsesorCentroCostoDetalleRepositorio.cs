using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using AutoMapper;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Marketing.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using Newtonsoft.Json;

namespace BSI.Integra.Aplicacion.Marketing.Repositorio
{
    public class AsesorCentroCostoDetalleRepositorio : BaseRepository<TAsesorCentroCostoDetalle, AsesorCentroCostoDetalleBO>
    {
        #region Metodos Base
        public AsesorCentroCostoDetalleRepositorio() : base()
        {
        }
        public AsesorCentroCostoDetalleRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<AsesorCentroCostoDetalleBO> GetBy(Expression<Func<TAsesorCentroCostoDetalle, bool>> filter)
        {
            IEnumerable<TAsesorCentroCostoDetalle> listado = base.GetBy(filter);
            List<AsesorCentroCostoDetalleBO> listadoBO = new List<AsesorCentroCostoDetalleBO>();
            foreach (var itemEntidad in listado)
            {
                AsesorCentroCostoDetalleBO objetoBO = Mapper.Map<TAsesorCentroCostoDetalle, AsesorCentroCostoDetalleBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public AsesorCentroCostoDetalleBO FirstById(int id)
        {
            try
            {
                TAsesorCentroCostoDetalle entidad = base.FirstById(id);
                AsesorCentroCostoDetalleBO objetoBO = new AsesorCentroCostoDetalleBO();
                Mapper.Map<TAsesorCentroCostoDetalle, AsesorCentroCostoDetalleBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public AsesorCentroCostoDetalleBO FirstBy(Expression<Func<TAsesorCentroCostoDetalle, bool>> filter)
        {
            try
            {
                TAsesorCentroCostoDetalle entidad = base.FirstBy(filter);
                AsesorCentroCostoDetalleBO objetoBO = Mapper.Map<TAsesorCentroCostoDetalle, AsesorCentroCostoDetalleBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(AsesorCentroCostoDetalleBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TAsesorCentroCostoDetalle entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<AsesorCentroCostoDetalleBO> listadoBO)
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

        public bool Update(AsesorCentroCostoDetalleBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TAsesorCentroCostoDetalle entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<AsesorCentroCostoDetalleBO> listadoBO)
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
        private void AsignacionId(TAsesorCentroCostoDetalle entidad, AsesorCentroCostoDetalleBO objetoBO)
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

        private TAsesorCentroCostoDetalle MapeoEntidad(AsesorCentroCostoDetalleBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TAsesorCentroCostoDetalle entidad = new TAsesorCentroCostoDetalle();
                entidad = Mapper.Map<AsesorCentroCostoDetalleBO, TAsesorCentroCostoDetalle>(objetoBO,
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
        /// Obtiene la lista de nombres de los programas (activos)  registradas en el sistema 
        ///  y sus IDs, (Usado para el llenado de combobox).
        /// </summary>
        /// <param name="idAsesorCentroCosto"> Id de la tabla T_AsesorCentroCosto</param>
        /// <returns>Lista de Programas Generales</returns>
        public List<ProgramaGeneralAsociadoDTO> ObtenerPGeneralPorCentroCosto(int idAsesorCentroCosto)
        {
            try
            {

                List<ProgramaGeneralAsociadoDTO> programasGenerales = new List<ProgramaGeneralAsociadoDTO>();
                var query = string.Empty;
                query = "SELECT IdAsesorCentroCosto, Prioridad, IdPGeneral, NombrePGeneral, CantidadIdPGeneral,Estado FROM pla.V_ObtenerPGeneralPorCentroCosto WHERE Estado = 1 and IdAsesorCentroCosto=@idAsesorCentroCosto";
                var pgeneralDB = _dapper.QueryDapper(query, new { IdAsesorCentroCosto = idAsesorCentroCosto });
                if (!string.IsNullOrEmpty(pgeneralDB) && !pgeneralDB.Contains("[]"))
                {
                    programasGenerales = JsonConvert.DeserializeObject<List<ProgramaGeneralAsociadoDTO>>(pgeneralDB);
                }
                return programasGenerales;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }

    }
}
