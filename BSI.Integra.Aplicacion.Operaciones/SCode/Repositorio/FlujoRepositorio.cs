using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using AutoMapper;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.DTOs.Operaciones;
using BSI.Integra.Aplicacion.Operaciones.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using Newtonsoft.Json;

namespace BSI.Integra.Aplicacion.Operaciones.Repositorio
{
    public class FlujoRepositorio : BaseRepository<TFlujo, FlujoBO>
    {
        #region Metodos Base
        public FlujoRepositorio() : base()
        {
        }
        public FlujoRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<FlujoBO> GetBy(Expression<Func<TFlujo, bool>> filter)
        {
            IEnumerable<TFlujo> listado = base.GetBy(filter);
            List<FlujoBO> listadoBO = new List<FlujoBO>();
            foreach (var itemEntidad in listado)
            {
                FlujoBO objetoBO = Mapper.Map<TFlujo, FlujoBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public FlujoBO FirstById(int id)
        {
            try
            {
                TFlujo entidad = base.FirstById(id);
                FlujoBO objetoBO = new FlujoBO();
                Mapper.Map<TFlujo, FlujoBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public FlujoBO FirstBy(Expression<Func<TFlujo, bool>> filter)
        {
            try
            {
                TFlujo entidad = base.FirstBy(filter);
                FlujoBO objetoBO = Mapper.Map<TFlujo, FlujoBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(FlujoBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TFlujo entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<FlujoBO> listadoBO)
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

        public bool Update(FlujoBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TFlujo entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<FlujoBO> listadoBO)
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
        private void AsignacionId(TFlujo entidad, FlujoBO objetoBO)
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

        private TFlujo MapeoEntidad(FlujoBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TFlujo entidad = new TFlujo();
                entidad = Mapper.Map<FlujoBO, TFlujo>(objetoBO,
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

        public IEnumerable<FlujoBO> ObtenerTodo()
        {
            IEnumerable<TFlujo> listado = base.GetAll();
            List<FlujoBO> listadoBO = new List<FlujoBO>();
            foreach (var itemEntidad in listado)
            {
                FlujoBO objetoBO = Mapper.Map<TFlujo, FlujoBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }

        public List<FlujoDetalleAgendaDTO> ObtenerDetalleAgenda(int idFlujo, int idClasificacionPersona, int idPespecifico)
        {
            try
            {
                List<FlujoDetalleAgendaDTO> listado = new List<FlujoDetalleAgendaDTO>();
                var _query = string.Empty;

                _query = $@"
                    SELECT * FROM ope.V_Flujo_DetalleFlujoAgenda WHERE IdFlujo = @idFlujo AND IdClasificacionPersona = @idClasificacionPersona AND IdPespecifico = @idPespecifico
                  ";

                var actividadesDB = _dapper.QueryDapper(_query,
                    new
                    {
                        idFlujo = idFlujo, idClasificacionPersona = idClasificacionPersona,
                        idPespecifico = idPespecifico
                    });
                listado = JsonConvert.DeserializeObject<List<FlujoDetalleAgendaDTO>>(actividadesDB);

                return listado;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public List<FlujoDetalleAgendaDTO> ObtenerPrimeraActividad(int idFlujo)
        {
            try
            {
                List<FlujoDetalleAgendaDTO> listado = new List<FlujoDetalleAgendaDTO>();
                var _query = string.Empty;

                _query = $@"
                    SELECT TOP 1 * FROM ope.V_Flujo_DetalleConfiguracion WHERE IdFlujo = @idFlujo ORDER BY OrdenFase, OrdenActividad, OrdenOcurrencia
                  ";

                var actividadesDB = _dapper.QueryDapper(_query, new { idFlujo = idFlujo });
                listado = JsonConvert.DeserializeObject<List<FlujoDetalleAgendaDTO>>(actividadesDB);

                return listado;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public List<FlujoDetalleProgramadoDTO> ObtenerFlujoDetalleProgramadoPendiente(int idClasificacionPersona)
        {
            try
            {
                List<FlujoDetalleProgramadoDTO> listado = new List<FlujoDetalleProgramadoDTO>();
                var _query = string.Empty;

                _query = $@"
                    SELECT 
	                    IdFlujoPorPEspecifico, IdClasificacionPersona, IdPEspecifico, IdFlujoActividad, IdFlujoOcurrencia, FechaEjecucion, FechaSeguimiento, NombrePEspecifico, NombreActividad, NombreOcurrencia
                    FROM ope.V_Flujo_DetalleProgramadoPendiente
                    WHERE IdClasificacionPersona = @idClasificacionPersona
                  ";

                var actividadesDB = _dapper.QueryDapper(_query, new { idClasificacionPersona = idClasificacionPersona });
                listado = JsonConvert.DeserializeObject<List<FlujoDetalleProgramadoDTO>>(actividadesDB);

                return listado;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
