using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using AutoMapper;
using BSI.Integra.Aplicacion.Marketing.BO;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using Newtonsoft.Json;
using System.Linq;

namespace BSI.Integra.Aplicacion.Marketing.Repositorio
{
    public class FormularioSolicitudRepositorio : BaseRepository<TFormularioSolicitud, FormularioSolicitudBO>
    {
        #region Metodos Base
        public FormularioSolicitudRepositorio() : base()
        {
        }
        public FormularioSolicitudRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<FormularioSolicitudBO> GetBy(Expression<Func<TFormularioSolicitud, bool>> filter)
        {
            IEnumerable<TFormularioSolicitud> listado = base.GetBy(filter);
            List<FormularioSolicitudBO> listadoBO = new List<FormularioSolicitudBO>();
            foreach (var itemEntidad in listado)
            {
                FormularioSolicitudBO objetoBO = Mapper.Map<TFormularioSolicitud, FormularioSolicitudBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public FormularioSolicitudBO FirstById(int id)
        {
            try
            {
                TFormularioSolicitud entidad = base.FirstById(id);
                FormularioSolicitudBO objetoBO = new FormularioSolicitudBO();
                Mapper.Map<TFormularioSolicitud, FormularioSolicitudBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public FormularioSolicitudBO FirstBy(Expression<Func<TFormularioSolicitud, bool>> filter)
        {
            try
            {
                TFormularioSolicitud entidad = base.FirstBy(filter);
                FormularioSolicitudBO objetoBO = Mapper.Map<TFormularioSolicitud, FormularioSolicitudBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(FormularioSolicitudBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TFormularioSolicitud entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<FormularioSolicitudBO> listadoBO)
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

        public bool Update(FormularioSolicitudBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TFormularioSolicitud entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<FormularioSolicitudBO> listadoBO)
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
        private void AsignacionId(TFormularioSolicitud entidad, FormularioSolicitudBO objetoBO)
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

        private TFormularioSolicitud MapeoEntidad(FormularioSolicitudBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TFormularioSolicitud entidad = new TFormularioSolicitud();
                entidad = Mapper.Map<FormularioSolicitudBO, TFormularioSolicitud>(objetoBO,
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
        /// Obtiene los registros que contengan el mismo nombre 
        /// </summary>
        /// <param name="nombre"></param>
        /// <returns></returns>
        public List<FormularioSolicitudFiltroDTO> GetFiltroIdNombre(string nombre)
        {
            var lista = GetBy(x => x.Nombre.Contains(nombre) , y => new FormularioSolicitudFiltroDTO {
                Id = y.Id,
                Nombre = y.Nombre }).ToList(); 
            return lista;
        }

        /// <summary>
        /// Obtiene el nombre por el Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public FormularioSolicitudFiltroDTO GetNombrePorId(int  id)
        {
            var lista = FirstBy(x => x.Id == id, y => new FormularioSolicitudFiltroDTO
            {
                Id = y.Id,
                Nombre = y.Nombre
            });
            return lista;
        }

        /// <summary>
        /// Obtiene todos Los registros de FormularioSolicitud
        /// </summary>
        public List<FormularioSolicitudCompuestoDTO> ObtenerTodo(FiltroCompuestroGrillaDTO paginador)
        {
            string Condicion = "";
            string Paginacion = "";
            string FormularioRespuesta = "";
            string Nombre="";
            string Codigo="";
            string NombreCampania = "";
            string Proveedor = "";
            //int Skip = 0;
            //int Take = 0;
            if (paginador.paginador.take != 0)
            {
                Paginacion = " OFFSET @Skip ROWS FETCH NEXT @Take ROWS ONLY";
            }

            if (paginador.filter != null)
            {
                
                foreach (var item in paginador.filter.Filters)
                {
                    if (item.Field == "FormularioRespuesta" && item.Value.Contains(""))
                    {
                        Condicion = Condicion + " and FormularioRespuesta=@FormularioRespuesta";
                        FormularioRespuesta = item.Value;
                    }
                    else if (item.Field == "Nombre" && item.Value.Contains(""))
                    {
                        Condicion = Condicion + " and Nombre=@Nombre";
                        Nombre= item.Value;
                    }
                    else if (item.Field == "Codigo" && item.Value.Contains(""))
                    {
                        Condicion = Condicion + " and Codigo=@Codigo";
                        Codigo = item.Value;
                    }
                    else if (item.Field == "NombreCampania" && item.Value.Contains(""))
                    {
                        Condicion = Condicion + " and NombreCampania=@NombreCampania";
                        NombreCampania = item.Value;
                    }
                    else if (item.Field == "Proveedor" && item.Value.Contains(""))
                    {
                        Condicion = Condicion + " and Proveedor=@Proveedor";
                        Proveedor = item.Value;
                    }
                }
            }
            string _queryFormularioSolicitud = "Select Id,IdFormularioRespuesta,FormularioRespuesta,Nombre,Codigo,NombreCampania,IdCampania,Proveedor,IdFormularioSolicitudTextoBoton,TipoSegmento,CodigoSegmento,TipoEvento,URLBotonInvitacionPagina" +
                                                " From mkt.V_TFormularioSolicitud_ObtenerTodo Where Estado=1 "+ Condicion + " Order by Id desc "+ Paginacion + "";
            var queryFormularioSolicitud = _dapper.QueryDapper(_queryFormularioSolicitud, new { FormularioRespuesta,Nombre, Codigo, NombreCampania, Proveedor, Skip =paginador.paginador.skip, Take=paginador.paginador.take });
            
            var rpta =JsonConvert.DeserializeObject<List<FormularioSolicitudCompuestoDTO>>(queryFormularioSolicitud);

            if (rpta.Count() == 0) return rpta;

            string _queryFormularioSolicitudTotal = "Select count(*) From mkt.V_TFormularioSolicitud_ObtenerTodo Where Estado=1 " + Condicion + "";
            var queryFormularioSolicitudTotal = _dapper.FirstOrDefault(_queryFormularioSolicitudTotal, new { FormularioRespuesta, Nombre, Codigo, NombreCampania, Proveedor});
            var FormularioSolicitudTotal= JsonConvert.DeserializeObject<Dictionary<string, int>>(queryFormularioSolicitudTotal);

            rpta.FirstOrDefault().Total = FormularioSolicitudTotal.Select(w => w.Value).FirstOrDefault();
            return rpta;
        }

        public List<ConjuntoAnuncioFiltroCompuestoDTO> ObtenerConjuntoAnunciosFiltro(string filtro)
        {
            string _queryConjuntoAnuncio = "Select Id,Nombre,IdProveedor,NombreProveedor,Codigo From mkt.V_T_FormularioSolicitud_ObtenerCampania Where Estado=1 and IdFormulario is null and Nombre Like @Filtro Order by Nombre asc";
            var queryConjuntoAnuncio = _dapper.QueryDapper(_queryConjuntoAnuncio,new { Filtro="%"+filtro+"%"});
            return JsonConvert.DeserializeObject<List<ConjuntoAnuncioFiltroCompuestoDTO>>(queryConjuntoAnuncio);
        }

        public List<ConjuntoAnuncioFiltroCompuestoDTO> ObtenerConjuntoAnunciosFiltro(int IdConjuntoAnuncio)
        {
            string _queryConjuntoAnuncio = "Select Id,Nombre,IdProveedor,NombreProveedor,Codigo From mkt.V_T_FormularioSolicitud_ObtenerCampaniaV2 Where Estado=1 and Id=@IdConjuntoAnuncio";
            var queryConjuntoAnuncio = _dapper.QueryDapper(_queryConjuntoAnuncio, new { IdConjuntoAnuncio = IdConjuntoAnuncio });
            return JsonConvert.DeserializeObject<List<ConjuntoAnuncioFiltroCompuestoDTO>>(queryConjuntoAnuncio);
        }
    }
}
