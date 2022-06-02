using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using AutoMapper;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.DTOs.Scode.DTOs.Marketing;
using BSI.Integra.Aplicacion.Marketing.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using Newtonsoft.Json;

namespace BSI.Integra.Aplicacion.Transversal.Repositorio
{
    public class FormularioPlantillaRepositorio : BaseRepository<TFormularioPlantilla, FormularioPlantillaBO>
    {
        #region Metodos Base
        public FormularioPlantillaRepositorio() : base()
        {
        }
        public FormularioPlantillaRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<FormularioPlantillaBO> GetBy(Expression<Func<TFormularioPlantilla, bool>> filter)
        {
            IEnumerable<TFormularioPlantilla> listado = base.GetBy(filter);
            List<FormularioPlantillaBO> listadoBO = new List<FormularioPlantillaBO>();
            foreach (var itemEntidad in listado)
            {
                FormularioPlantillaBO objetoBO = Mapper.Map<TFormularioPlantilla, FormularioPlantillaBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public FormularioPlantillaBO FirstById(int id)
        {
            try
            {
                TFormularioPlantilla entidad = base.FirstById(id);
                FormularioPlantillaBO objetoBO = new FormularioPlantillaBO();
                Mapper.Map<TFormularioPlantilla, FormularioPlantillaBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public FormularioPlantillaBO FirstBy(Expression<Func<TFormularioPlantilla, bool>> filter)
        {
            try
            {
                TFormularioPlantilla entidad = base.FirstBy(filter);
                FormularioPlantillaBO objetoBO = Mapper.Map<TFormularioPlantilla, FormularioPlantillaBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(FormularioPlantillaBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TFormularioPlantilla entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<FormularioPlantillaBO> listadoBO)
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

        public bool Update(FormularioPlantillaBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TFormularioPlantilla entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<FormularioPlantillaBO> listadoBO)
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
        private void AsignacionId(TFormularioPlantilla entidad, FormularioPlantillaBO objetoBO)
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

        private TFormularioPlantilla MapeoEntidad(FormularioPlantillaBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TFormularioPlantilla entidad = new TFormularioPlantilla();
                entidad = Mapper.Map<FormularioPlantillaBO, TFormularioPlantilla>(objetoBO,
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
        /// Obtiene los datos necesarios para llenar la grilla del CRUD de FormularioPlantilla
        /// </summary>
        /// <returns></returns>
        public List<FormularioPlantillaDTO> ObtenerFormularioPlantilla()
        {
            try
            {
                List<FormularioPlantillaDTO> PlantillaFormulario = new List<FormularioPlantillaDTO>();
                var campos = "Id, Nombre, FechaCreacion, UsuarioModificacion, IdPlantillaLandingPage, NombrePlantillaLandingPage, IdFormularioSolicitudTextoBoton, TextoFormulario, Titulo, Texto, TituloProgramaAutomatico, DescripcionWebAutomatico, IdFormularioSolicitud, IdFormularioLandingPage";
                var _query = "SELECT " + campos + " FROM  [mkt].[V_TFormularioPlantillaDatosGrilla]";
                var dataDB = _dapper.QueryDapper(_query, null);
                if (!dataDB.Contains("[]") && !string.IsNullOrEmpty(dataDB))
                {
                    PlantillaFormulario = JsonConvert.DeserializeObject<List<FormularioPlantillaDTO>>(dataDB);
                }
                return PlantillaFormulario;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

    }
}
