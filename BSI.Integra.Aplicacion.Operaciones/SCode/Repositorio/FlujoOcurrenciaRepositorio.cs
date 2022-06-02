using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using AutoMapper;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Operaciones.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using Newtonsoft.Json;

namespace BSI.Integra.Aplicacion.Operaciones.Repositorio
{
    public class FlujoOcurrenciaRepositorio : BaseRepository<TFlujoOcurrencia, FlujoOcurrenciaBO>
    {
        #region Metodos Base
        public FlujoOcurrenciaRepositorio() : base()
        {
        }
        public FlujoOcurrenciaRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<FlujoOcurrenciaBO> GetBy(Expression<Func<TFlujoOcurrencia, bool>> filter)
        {
            IEnumerable<TFlujoOcurrencia> listado = base.GetBy(filter);
            List<FlujoOcurrenciaBO> listadoBO = new List<FlujoOcurrenciaBO>();
            foreach (var itemEntidad in listado)
            {
                FlujoOcurrenciaBO objetoBO = Mapper.Map<TFlujoOcurrencia, FlujoOcurrenciaBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public FlujoOcurrenciaBO FirstById(int id)
        {
            try
            {
                TFlujoOcurrencia entidad = base.FirstById(id);
                FlujoOcurrenciaBO objetoBO = new FlujoOcurrenciaBO();
                Mapper.Map<TFlujoOcurrencia, FlujoOcurrenciaBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public FlujoOcurrenciaBO FirstBy(Expression<Func<TFlujoOcurrencia, bool>> filter)
        {
            try
            {
                TFlujoOcurrencia entidad = base.FirstBy(filter);
                FlujoOcurrenciaBO objetoBO = Mapper.Map<TFlujoOcurrencia, FlujoOcurrenciaBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(FlujoOcurrenciaBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TFlujoOcurrencia entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<FlujoOcurrenciaBO> listadoBO)
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

        public bool Update(FlujoOcurrenciaBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TFlujoOcurrencia entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<FlujoOcurrenciaBO> listadoBO)
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
        private void AsignacionId(TFlujoOcurrencia entidad, FlujoOcurrenciaBO objetoBO)
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

        private TFlujoOcurrencia MapeoEntidad(FlujoOcurrenciaBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TFlujoOcurrencia entidad = new TFlujoOcurrencia();
                entidad = Mapper.Map<FlujoOcurrenciaBO, TFlujoOcurrencia>(objetoBO,
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

        public List<FlujoOcurrenciaDTO> ListadoPorIdFlujoActividad(int idFlujoActividad)
        {
            try
            {
                List<FlujoOcurrenciaDTO> listado = new List<FlujoOcurrenciaDTO>();
                var _query = string.Empty;
                _query = @"
                    SELECT 
	                    T_FlujoOcurrencia.Id, T_FlujoOcurrencia.IdFlujoActividad, T_FlujoOcurrencia.Orden, T_FlujoOcurrencia.Nombre, 
	                    T_FlujoOcurrencia.CerrarSeguimiento, T_FlujoOcurrencia.IdFase_Destino, T_FlujoOcurrencia.IdFlujoActividad_Siguiente,
	                    T_FlujoFase.Nombre AS FaseDestino, T_FlujoActividad.Nombre AS ActividadSiguiente
                    FROM ope.T_FlujoOcurrencia
                    LEFT JOIN ope.T_FlujoFase ON T_FlujoFase.Id = T_FlujoOcurrencia.IdFase_Destino
                    LEFT JOIN ope.T_FlujoActividad ON T_FlujoActividad.Id = T_FlujoOcurrencia.IdFlujoActividad_Siguiente
                    WHERE T_FlujoOcurrencia.IdFlujoActividad = @idFlujoActividad
                ";

                var resultado = _dapper.QueryDapper(_query, new {idFlujoActividad = idFlujoActividad});
                listado = JsonConvert.DeserializeObject<List<FlujoOcurrenciaDTO>>(resultado);
                return listado;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
