using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Linq;
using System.Text;
using AutoMapper;
using BSI.Integra.Aplicacion.Marketing.BO;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using Newtonsoft.Json;

namespace BSI.Integra.Aplicacion.Marketing.Repositorio
{
	public class MessengerEnvioMasivoRepositorio : BaseRepository<TMessengerEnvioMasivo, MessengerEnvioMasivoBO>
	{
		#region Metodos Base
		public MessengerEnvioMasivoRepositorio() : base()
		{
		}
		public MessengerEnvioMasivoRepositorio(integraDBContext contexto) : base(contexto)
		{
		}
		public IEnumerable<MessengerEnvioMasivoBO> GetBy(Expression<Func<TMessengerEnvioMasivo, bool>> filter)
		{
			IEnumerable<TMessengerEnvioMasivo> listado = base.GetBy(filter);
			List<MessengerEnvioMasivoBO> listadoBO = new List<MessengerEnvioMasivoBO>();
			foreach (var itemEntidad in listado)
			{
				MessengerEnvioMasivoBO objetoBO = Mapper.Map<TMessengerEnvioMasivo, MessengerEnvioMasivoBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
				listadoBO.Add(objetoBO);
			}

			return listadoBO;
		}
		public MessengerEnvioMasivoBO FirstById(int id)
		{
			try
			{
				TMessengerEnvioMasivo entidad = base.FirstById(id);
				MessengerEnvioMasivoBO objetoBO = new MessengerEnvioMasivoBO();
				Mapper.Map<TMessengerEnvioMasivo, MessengerEnvioMasivoBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

				return objetoBO;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}
		public MessengerEnvioMasivoBO FirstBy(Expression<Func<TMessengerEnvioMasivo, bool>> filter)
		{
			try
			{
				TMessengerEnvioMasivo entidad = base.FirstBy(filter);
				MessengerEnvioMasivoBO objetoBO = Mapper.Map<TMessengerEnvioMasivo, MessengerEnvioMasivoBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

				return objetoBO;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}

		public bool Insert(MessengerEnvioMasivoBO objetoBO)
		{
			try
			{
				//mapeo de la entidad
				TMessengerEnvioMasivo entidad = MapeoEntidad(objetoBO);

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

		public bool Insert(IEnumerable<MessengerEnvioMasivoBO> listadoBO)
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

		public bool Update(MessengerEnvioMasivoBO objetoBO)
		{
			try
			{
				if (objetoBO == null)
				{
					throw new ArgumentNullException("Entidad nula");
				}

				//mapeo de la entidad
				TMessengerEnvioMasivo entidad = MapeoEntidad(objetoBO);

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

		public bool Update(IEnumerable<MessengerEnvioMasivoBO> listadoBO)
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
		private void AsignacionId(TMessengerEnvioMasivo entidad, MessengerEnvioMasivoBO objetoBO)
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

		private TMessengerEnvioMasivo MapeoEntidad(MessengerEnvioMasivoBO objetoBO)
		{
			try
			{
				//crea la entidad padre
				TMessengerEnvioMasivo entidad = new TMessengerEnvioMasivo();
				entidad = Mapper.Map<MessengerEnvioMasivoBO, TMessengerEnvioMasivo>(objetoBO,
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
        /// Obtiene los mensajes masivos de MEssenger por idActividadCabecera
        /// </summary>
        /// <param name="idActividadCabecera"></param>
        /// <returns></returns>
        public List<MessengerEnvioMasivoDTO> ObtenerMessengerEnvioMasivo(int idActividadCabecera)
        {
            try
            {
                List<MessengerEnvioMasivoDTO> resultado = new List<MessengerEnvioMasivoDTO>();
                string _queryResultado = "Select Id, Nombre, Descripcion, PresupuestoDiario, IdPersonal, IdPGeneral, IdActividadCabecera, IdPlantilla, IdConjuntoListaDetalle, IdFacebookPagina, NombreFacebookPagina From mkt.V_ObtenerMessengerEnvioMasivo Where IdActividadCabecera = @idActividadCabecera";
                var queryResultado = _dapper.QueryDapper(_queryResultado, new { idActividadCabecera });
                if (queryResultado != "[]" && queryResultado != "null")
                {
                    resultado = JsonConvert.DeserializeObject<List<MessengerEnvioMasivoDTO>>(queryResultado);
                }
                return resultado;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Obtiene los mensajes masivos de MEssenger por psid
        /// </summary>
        /// <param name="idActividadCabecera"></param>
        /// <returns></returns>
        public MensajeMasivoEnviadoMessengerDTO ObtenerMensajeMasivoEnviadoMessenger(string PSID, DateTime fechaInteraccion)
        {
            try
            {
                MensajeMasivoEnviadoMessengerDTO resultado = new MensajeMasivoEnviadoMessengerDTO();
                string _queryResultado = "Select IdFacebookAnuncio, PSID, Plantilla From mkt.V_ObtenerMensajeMasivoEnviadoMessenger Where @fechaInteraccion BETWEEN FechaInicioActividad AND FechaFinActividad AND PSID = @PSID";
                var queryResultado = _dapper.FirstOrDefault(_queryResultado, new { fechaInteraccion, PSID });
                if (queryResultado != "[]" && queryResultado != "null")
                {
                    resultado = JsonConvert.DeserializeObject<MensajeMasivoEnviadoMessengerDTO>(queryResultado);
                }
                return resultado;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public List<FacebookConjuntoAnuncioDTO> ObtenerConjuntoAnuncioFacebookEnvioMasivo(DateTime fecha)
        {
            try
            {
                List<FacebookConjuntoAnuncioDTO> resultado = new List<FacebookConjuntoAnuncioDTO>();
                string _queryResultado = "Select FacebookIdConjuntoAnuncio, IdConjuntoAnuncioFacebook From mkt.V_ObtenerConjuntoAnuncioFacebookEnvioMasivo Where @fecha BETWEEN FechaInicioActividad AND FechaFinActividad";
                var queryResultado = _dapper.QueryDapper(_queryResultado, new { fecha });
                if (queryResultado != "[]" && queryResultado != "null")
                {
                    resultado = JsonConvert.DeserializeObject<List<FacebookConjuntoAnuncioDTO>>(queryResultado);
                }
                return resultado;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

    }
}
