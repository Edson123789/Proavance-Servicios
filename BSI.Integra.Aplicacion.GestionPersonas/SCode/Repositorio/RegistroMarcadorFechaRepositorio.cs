using AutoMapper;
using BSI.Integra.Aplicacion.GestionPersonas.BO;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using BSI.Integra.Aplicacion.DTOs.Scode.DTOs.Transversal;
using System.Linq;

namespace BSI.Integra.Aplicacion.GestionPersonas.Repositorio
{
    public class RegistroMarcadorFechaRepositorio : BaseRepository<TRegistroMarcadorFecha, RegistroMarcadorFechaBO>
    {
        #region Metodos Base
        public RegistroMarcadorFechaRepositorio() : base()
        {
        }
        public RegistroMarcadorFechaRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<RegistroMarcadorFechaBO> GetBy(Expression<Func<TRegistroMarcadorFecha, bool>> filter)
        {
            IEnumerable<TRegistroMarcadorFecha> listado = base.GetBy(filter);
            List<RegistroMarcadorFechaBO> listadoBO = new List<RegistroMarcadorFechaBO>();
            foreach (var itemEntidad in listado)
            {
                RegistroMarcadorFechaBO objetoBO = Mapper.Map<TRegistroMarcadorFecha, RegistroMarcadorFechaBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public RegistroMarcadorFechaBO FirstById(int id)
        {
            try
            {
                TRegistroMarcadorFecha entidad = base.FirstById(id);
                RegistroMarcadorFechaBO objetoBO = new RegistroMarcadorFechaBO();
                Mapper.Map<TRegistroMarcadorFecha, RegistroMarcadorFechaBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public RegistroMarcadorFechaBO FirstBy(Expression<Func<TRegistroMarcadorFecha, bool>> filter)
        {
            try
            {
                TRegistroMarcadorFecha entidad = base.FirstBy(filter);
                RegistroMarcadorFechaBO objetoBO = Mapper.Map<TRegistroMarcadorFecha, RegistroMarcadorFechaBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(RegistroMarcadorFechaBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TRegistroMarcadorFecha entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<RegistroMarcadorFechaBO> listadoBO)
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

        public bool Update(RegistroMarcadorFechaBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TRegistroMarcadorFecha entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<RegistroMarcadorFechaBO> listadoBO)
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
        private void AsignacionId(TRegistroMarcadorFecha entidad, RegistroMarcadorFechaBO objetoBO)
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

        private TRegistroMarcadorFecha MapeoEntidad(RegistroMarcadorFechaBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TRegistroMarcadorFecha entidad = new TRegistroMarcadorFecha();
                entidad = Mapper.Map<RegistroMarcadorFechaBO, TRegistroMarcadorFecha>(objetoBO,
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
		/// Obtiene la fecha de la ultima llamada realizada por el personal mediante el anexo 3cx
		/// </summary>
		/// <param name="anexo3CX"></param>
		/// <returns></returns>
		public RegistroMarcacionPersonalDTO ObtenerFechaUltimaLlamadaPersonal(string anexo3CX)
		{
			try
			{
				RegistroMarcacionPersonalDTO fecha = new RegistroMarcacionPersonalDTO();
				var query = "SELECT TOP 1 FechaFin FROM centralllamada.dbo.V_LlamadaCentral_Segmento WHERE OrigenExt = @OrigenExt ORDER BY FechaFin DESC";
				var res = _dapper.FirstOrDefault(query, new { OrigenExt = anexo3CX });
				if (!string.IsNullOrEmpty(res) && !res.Contains("[]"))
				{
					fecha = JsonConvert.DeserializeObject<RegistroMarcacionPersonalDTO>(res);
				}
				return fecha;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}
		
		/// <summary>
		/// Obtiene el registro de marcación del personal en una fecha determinada
		/// </summary>
		/// <param name="idPersonal"></param>
		/// <param name="fechaActual"></param>
		/// <returns></returns>
		public RegistroMarcadorFechaBO ObtenerRegistroMarcacionPersonal(int idPersonal, DateTime fechaActual)
		{
			try
			{
				return this.FirstBy(x => x.IdPersonal == idPersonal && x.Fecha.Date == fechaActual.Date);
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}


        public RegistroMarcadorFechaBO ObtenerRegistroMarcacionPersonalDNI(int idPersonal, DateTime fechaActual, string Dni)
        {
            try
            {
                return this.FirstBy(x => x.IdPersonal == idPersonal && x.Fecha.Date == fechaActual.Date && x.Pin == Dni);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }


        public List<AgrupadoReporteMarcacionDTO> ObtenerMarcacionesPersonal (FiltroReporteMarcacionDTO filtro)
        {
            try
            {
                var filtros = new
                {
                    ListaPersonal = filtro.Personal == null ? "" : string.Join(",", filtro.Personal.Select(x => x)),
                    ListaSede = filtro.Sede == null ? "" : string.Join(",", filtro.Sede.Select(x => x)),
                    ListaArea = filtro.Area == null ? "" : string.Join(",", filtro.Area.Select(x => x)),
                    FechaInicio = filtro.FechaInicio,                    
                    FechaFin = filtro.FechaFin,
                };

                List<AgrupadoReporteMarcacionDTO> marcaciones = new List<AgrupadoReporteMarcacionDTO>();
                string query = string.Empty;
                query = "[gp].[SP_ObtenerMarcacionesPersonal]";
                var marcacionp = _dapper.QuerySPDapper(query, filtros);
                if (!string.IsNullOrEmpty(marcacionp) && !marcacionp.Contains("[]"))
                {
                    marcaciones = JsonConvert.DeserializeObject<List<AgrupadoReporteMarcacionDTO>>(marcacionp);
                }                
                return marcaciones;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
