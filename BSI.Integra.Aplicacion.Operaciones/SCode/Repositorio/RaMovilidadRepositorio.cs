using System;
using System.Collections.Generic;
using System.Linq;
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
    public class RaMovilidadRepositorio : BaseRepository<TRaMovilidad, RaMovilidadBO>
    {
        #region Metodos Base
        public RaMovilidadRepositorio() : base()
        {
        }
        public RaMovilidadRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<RaMovilidadBO> GetBy(Expression<Func<TRaMovilidad, bool>> filter)
        {
            IEnumerable<TRaMovilidad> listado = base.GetBy(filter);
            List<RaMovilidadBO> listadoBO = new List<RaMovilidadBO>();
            foreach (var itemEntidad in listado)
            {
                RaMovilidadBO objetoBO = Mapper.Map<TRaMovilidad, RaMovilidadBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public RaMovilidadBO FirstById(int id)
        {
            try
            {
                TRaMovilidad entidad = base.FirstById(id);
                RaMovilidadBO objetoBO = new RaMovilidadBO();
                Mapper.Map<TRaMovilidad, RaMovilidadBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public RaMovilidadBO FirstBy(Expression<Func<TRaMovilidad, bool>> filter)
        {
            try
            {
                TRaMovilidad entidad = base.FirstBy(filter);
                RaMovilidadBO objetoBO = Mapper.Map<TRaMovilidad, RaMovilidadBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(RaMovilidadBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TRaMovilidad entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<RaMovilidadBO> listadoBO)
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

        public bool Update(RaMovilidadBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TRaMovilidad entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<RaMovilidadBO> listadoBO)
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
        private void AsignacionId(TRaMovilidad entidad, RaMovilidadBO objetoBO)
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

        private TRaMovilidad MapeoEntidad(RaMovilidadBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TRaMovilidad entidad = new TRaMovilidad();
                entidad = Mapper.Map<RaMovilidadBO, TRaMovilidad>(objetoBO,
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

        public List<DatoAerolineaDTO> ObtenerMovilidades()
        {
            try
            {
                List<DatoAerolineaDTO> datosAerolinea = new List<DatoAerolineaDTO>();
                var _query = string.Empty;
                _query = "SELECT NombreTipoMovilidad, NombreMovilidad, Telefono, NombreCiudad FROM ope.V_ObtenerMovilidades WHERE EstadoMovilidad = 1 AND EstadoTipoMovilidad = 1 AND EstadoCiudad = 1";
                var datosAerolineaDB = _dapper.QueryDapper(_query, null);
                if (!datosAerolineaDB.Contains("[]"))
                {
                    datosAerolinea = JsonConvert.DeserializeObject<List<DatoAerolineaDTO>>(datosAerolineaDB);
                }
                return datosAerolinea;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

		public List<FiltroGenericoDTO> ObtenerMovilidadesParaCombo()
		{
			try
			{
				return GetBy(x => x.Estado == true, x => new FiltroGenericoDTO { Value = x.Id, Text = x.Nombre }).ToList();
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}
    }
}
