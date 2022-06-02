using System;
using System.Collections.Generic;
using System.Linq;
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
    public class FormularioRespuestaRepositorio : BaseRepository<TFormularioRespuesta, FormularioRespuestaBO>
    {
        #region Metodos Base
        public FormularioRespuestaRepositorio() : base()
        {
        }
        public FormularioRespuestaRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<FormularioRespuestaBO> GetBy(Expression<Func<TFormularioRespuesta, bool>> filter)
        {
            IEnumerable<TFormularioRespuesta> listado = base.GetBy(filter);
            List<FormularioRespuestaBO> listadoBO = new List<FormularioRespuestaBO>();
            foreach (var itemEntidad in listado)
            {
                FormularioRespuestaBO objetoBO = Mapper.Map<TFormularioRespuesta, FormularioRespuestaBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public FormularioRespuestaBO FirstById(int id)
        {
            try
            {
                TFormularioRespuesta entidad = base.FirstById(id);
                FormularioRespuestaBO objetoBO = new FormularioRespuestaBO();
                Mapper.Map<TFormularioRespuesta, FormularioRespuestaBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public FormularioRespuestaBO FirstBy(Expression<Func<TFormularioRespuesta, bool>> filter)
        {
            try
            {
                TFormularioRespuesta entidad = base.FirstBy(filter);
                FormularioRespuestaBO objetoBO = Mapper.Map<TFormularioRespuesta, FormularioRespuestaBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(FormularioRespuestaBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TFormularioRespuesta entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<FormularioRespuestaBO> listadoBO)
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

        public bool Update(FormularioRespuestaBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TFormularioRespuesta entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<FormularioRespuestaBO> listadoBO)
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
        private void AsignacionId(TFormularioRespuesta entidad, FormularioRespuestaBO objetoBO)
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

        private TFormularioRespuesta MapeoEntidad(FormularioRespuestaBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TFormularioRespuesta entidad = new TFormularioRespuesta();
                entidad = Mapper.Map<FormularioRespuestaBO, TFormularioRespuesta>(objetoBO,
                    opt => opt.ConfigureMap(MemberList.None));

                //mapea los hijos

                return entidad;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

		public IEnumerable<FormularioRespuestaBO> GetFiltered<KProperty>(IEnumerable<Expression<Func<TFormularioRespuesta, bool>>> filters, Expression<Func<TFormularioRespuesta, KProperty>> orderBy, bool ascending)
		{
			IEnumerable<TFormularioRespuesta> listado = base.GetFiltered(filters, orderBy, ascending);
			List<FormularioRespuestaBO> listadoBO = new List<FormularioRespuestaBO>();

			foreach (var itemEntidad in listado)
			{
				FormularioRespuestaBO objetoBO = Mapper.Map<TFormularioRespuesta, FormularioRespuestaBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
				listadoBO.Add(objetoBO);
			}
			return listadoBO;
		}
		#endregion
		/// <summary>
		/// Obtiene Id,Nombre de FormularioRespuesta
		/// </summary>
		/// <param name="filtro"></param>
		/// <returns></returns>
		public List<FiltroDTO> FormularioRespuestaFiltro(string filtro)
        {
            try
            {
                string _queryFormularioRespuesta = "Select Id,Nombre From mkt.V_TFormularioRespuesta_Filtro Where Estado=1 and Nombre like @Filtro";
                var queryFormularioRespuesta = _dapper.QueryDapper(_queryFormularioRespuesta, new { Filtro = "%"+filtro+"%" });
                return JsonConvert.DeserializeObject<List<FiltroDTO>>(queryFormularioRespuesta);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            
        }

		/// <summary>
		/// Se obtienen todos los formularios respuesta para filtro
		/// </summary>
		/// <returns></returns>
        public List<FiltroDTO> FormularioRespuestaParaFiltro()
        {
            string _queryFormularioRespuesta = "Select Id,Nombre From mkt.V_TFormularioRespuesta_Filtro Where Estado=1";
            var queryFormularioRespuesta = _dapper.QueryDapper(_queryFormularioRespuesta, null);
            return JsonConvert.DeserializeObject<List<FiltroDTO>>(queryFormularioRespuesta);
        }


		/// <summary>
		/// Obtiene la lista de Formularios respuesta (activos) registrados en el sistema
		/// con todos sus campos excepto los de auditoria.
		/// </summary>
		/// <param name="filtro"></param>
		/// <returns></returns>
		public List<FormularioRespuestaDTO> ListarFormularioRespuesta(FiltroPaginadorDTO filtro)
		{
			try
			{
				
				List<FormularioRespuestaDTO> items = new List<FormularioRespuestaDTO>();
				List<Expression<Func<TFormularioRespuesta, bool>>> filters = new List<Expression<Func<TFormularioRespuesta, bool>>>();
				var total = 0;
				List<FormularioRespuestaBO> lista = new List<FormularioRespuestaBO>();
				if (filtro != null)
				{
					if ((filtro.FiltroKendo != null && filtro.FiltroKendo.Filters.Count > 0))
					{
						// Creamos la Lista de filtros
						foreach (var filterGrid in filtro.FiltroKendo.Filters)
						{
							switch (filterGrid.Field)
							{
								case "Nombre":
									filters.Add(o => o.Nombre.Contains(filterGrid.Value));
									break;
								case "Codigo":
									filters.Add(o => o.Codigo.Contains(filterGrid.Value));
									break;
								default:
									filters.Add(o => true);
									break;
							}
						}
					}
					else
					{
					}
					lista = GetFiltered(filters, p => p.Id, false).ToList();
					total = lista.Count();
					lista = lista.Skip(filtro.Skip).Take(filtro.Take).ToList();
				}
				else
				{
					lista = GetBy(o => true).OrderByDescending(x => x.Id).ToList();
					total = lista.Count();
				}
				items = lista.Select(x => new FormularioRespuestaDTO
				{
					Id = x.Id,
					Nombre = x.Nombre,
					Codigo = x.Codigo,
					IdPgeneral = x.IdPgeneral,
					ProgramaGeneral = x.ProgramaGeneral,
					ResumenProgramaGeneral = x.ResumenProgramaGeneral,
					ColorTextoPgeneral = x.ColorTextoPgeneral,
					ColorTextoDescripcionPgeneral = x.ColorTextoDescripcionPgeneral,
					ColorTextoInvitacionBrochure = x.ColorTextoInvitacionBrochure,
					TextoBotonBrochure = x.TextoBotonBrochure,
					ColorFondoBotonBrochure = x.ColorFondoBotonBrochure,
					ColorTextoBotonBrochure = x.ColorTextoBotonBrochure,
					ColorBordeInferiorBotonBrochure = x.ColorBordeInferiorBotonBrochure,
					ColorTextoBotonInvitacion = x.ColorTextoBotonInvitacion,
					ColorFondoBotonInvitacion = x.ColorFondoBotonInvitacion,
					FondoBotonLadoInvitacion = x.FondoBotonLadoInvitacion,
					UrlImagenLadoInvitacion = x.UrlImagenLadoInvitacion,
					TextoBotonInvitacionPagina = x.TextoBotonInvitacionPagina,
					UrlBotonInvitacionPagina = x.UrlBotonInvitacionPagina,
					TextoBotonInvitacionArea = x.TextoBotonInvitacionArea,
					UrlBotonInvitacionArea = x.UrlBotonInvitacionArea,
					ContenidoSeccionTelefonos = x.ContenidoSeccionTelefonos,
					IdFormularioRespuestaPlantilla = x.IdFormularioRespuestaPlantilla,
					Urlbrochure = x.Urlbrochure,
					Urllogotipo = x.Urllogotipo,
					TextoInvitacionBrochure = x.TextoInvitacionBrochure,
					Total = total
				}).ToList();

				return items;
			}

			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}
	}
}
