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
    public class FormularioRespuestaPlantillaRepositorio : BaseRepository<TFormularioRespuestaPlantilla, FormularioRespuestaPlantillaBO>
    {
        #region Metodos Base
        public FormularioRespuestaPlantillaRepositorio() : base()
        {
        }
        public FormularioRespuestaPlantillaRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<FormularioRespuestaPlantillaBO> GetBy(Expression<Func<TFormularioRespuestaPlantilla, bool>> filter)
        {
            IEnumerable<TFormularioRespuestaPlantilla> listado = base.GetBy(filter);
            List<FormularioRespuestaPlantillaBO> listadoBO = new List<FormularioRespuestaPlantillaBO>();
            foreach (var itemEntidad in listado)
            {
                FormularioRespuestaPlantillaBO objetoBO = Mapper.Map<TFormularioRespuestaPlantilla, FormularioRespuestaPlantillaBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public FormularioRespuestaPlantillaBO FirstById(int id)
        {
            try
            {
                TFormularioRespuestaPlantilla entidad = base.FirstById(id);
                FormularioRespuestaPlantillaBO objetoBO = new FormularioRespuestaPlantillaBO();
                Mapper.Map<TFormularioRespuestaPlantilla, FormularioRespuestaPlantillaBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public FormularioRespuestaPlantillaBO FirstBy(Expression<Func<TFormularioRespuestaPlantilla, bool>> filter)
        {
            try
            {
                TFormularioRespuestaPlantilla entidad = base.FirstBy(filter);
                FormularioRespuestaPlantillaBO objetoBO = Mapper.Map<TFormularioRespuestaPlantilla, FormularioRespuestaPlantillaBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(FormularioRespuestaPlantillaBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TFormularioRespuestaPlantilla entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<FormularioRespuestaPlantillaBO> listadoBO)
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

        public bool Update(FormularioRespuestaPlantillaBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TFormularioRespuestaPlantilla entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<FormularioRespuestaPlantillaBO> listadoBO)
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
        private void AsignacionId(TFormularioRespuestaPlantilla entidad, FormularioRespuestaPlantillaBO objetoBO)
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

        private TFormularioRespuestaPlantilla MapeoEntidad(FormularioRespuestaPlantillaBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TFormularioRespuestaPlantilla entidad = new TFormularioRespuestaPlantilla();
                entidad = Mapper.Map<FormularioRespuestaPlantillaBO, TFormularioRespuestaPlantilla>(objetoBO,
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
        /// Obtiene todos los registros para mostrar en la grilla.
        /// </summary>
        /// <returns></returns>
        public List<FormularioRespuestaPlantillaGridDTO> ObtenerRegistrosGrid()
        {
            try
            {
                List<FormularioRespuestaPlantillaGridDTO> lista = new List<FormularioRespuestaPlantillaGridDTO>();
                lista = GetBy(x => true, y => new FormularioRespuestaPlantillaGridDTO
                {
                    Id = y.Id,
                    NombrePlantilla = y.NombrePlantilla,
                    TextoBotonBrochure = y.TextoBotonBrochure,
                    TextoBotonInvitacionPagina = y.TextoBotonInvitacionPagina,
                    TextoBotonInvitacionArea = y.TextoBotonInvitacionArea
                }).OrderByDescending(x => x.Id).ToList();
                return lista;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Obtiene un registro por el Id
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public FormularioRespuestaPlantillaDTO ObtenerRegistroPorId(int id)
        {
            try
            {
                FormularioRespuestaPlantillaDTO registro = new FormularioRespuestaPlantillaDTO();
                registro = GetBy(x => x.Id == id, y => new FormularioRespuestaPlantillaDTO
                {
                    Id = y.Id,
                    NombrePlantilla = y.NombrePlantilla,
                    ColorTextoPgeneral = y.ColorTextoPgeneral,
                    ColorTextoDescripcionPgeneral = y.ColorTextoDescripcionPgeneral,
                    ColorTextoInvitacionBrochure = y.ColorTextoInvitacionBrochure,
                    TextoBotonBrochure = y.TextoBotonBrochure,
                    ColorFondoBotonBrochure = y.ColorFondoBotonBrochure,
                    ColorTextoBotonBrochure = y.ColorTextoBotonBrochure,
                    ColorBordeInferiorBotonBrochure = y.ColorBordeInferiorBotonBrochure,
                    ColorTextoBotonInvitacion = y.ColorTextoBotonInvitacion,
                    ColorFondoBotonInvitacion = y.ColorFondoBotonInvitacion,
                    FondoBotonLadoInvitacion = y.FondoBotonLadoInvitacion,
                    UrlimagenLadoInvitacion = y.UrlimagenLadoInvitacion,
                    TextoBotonInvitacionPagina = y.TextoBotonInvitacionPagina,
                    TextoBotonInvitacionArea = y.TextoBotonInvitacionArea,
                    ContenidoSeccionTelefonos = y.ContenidoSeccionTelefonos,
                    TextoInvitacionBrochure = y.TextoInvitacionBrochure,
                }).FirstOrDefault();
                return registro;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        ///  Obtiene la lista de registros [Id, NOmbre] (Estado=1) de T_FormularioPlantilla (Usado para el llenado de autocomplete).
        /// </summary>
        /// <returns></returns>
        public List<FiltroDTO> ObtenerTodoFormularioPlantillaFiltro()
        {
            try
            { 
                List<FiltroDTO> Registros = new List<FiltroDTO>();
                var _query = "SELECT Id,  Nombre FROM [mkt].[V_TFormularioPlantilla]";
                var result = _dapper.QueryDapper(_query, null);
                if (!string.IsNullOrEmpty(result) && !result.Contains("[]")) 
                {
                    Registros = JsonConvert.DeserializeObject<List<FiltroDTO>>(result);
                }
                return Registros;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
