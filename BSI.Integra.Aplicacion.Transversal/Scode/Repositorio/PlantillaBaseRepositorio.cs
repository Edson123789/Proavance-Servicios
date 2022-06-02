using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using AutoMapper;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.DTOs.Scode.DTOs.Marketing;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using Newtonsoft.Json;

namespace BSI.Integra.Aplicacion.Transversal.Repositorio
{
    /// Repositorio: Transversal/PlantillaBase
    /// Autor: Wilber Choque - Esthepheny Tanco - Ansoli Espinoza
    /// Fecha: 22/06/2021
    /// <summary>
    /// Gestión de Examenes tabla T_PlantillaBase
    /// </summary>
    public class PlantillaBaseRepositorio : BaseRepository<TPlantillaBase, PlantillaBaseBO>
    {
        #region Metodos Base
        public PlantillaBaseRepositorio() : base()
        {
        }
        public PlantillaBaseRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<PlantillaBaseBO> GetBy(Expression<Func<TPlantillaBase, bool>> filter)
        {
            IEnumerable<TPlantillaBase> listado = base.GetBy(filter);
            List<PlantillaBaseBO> listadoBO = new List<PlantillaBaseBO>();
            foreach (var itemEntidad in listado)
            {
                PlantillaBaseBO objetoBO = Mapper.Map<TPlantillaBase, PlantillaBaseBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public PlantillaBaseBO FirstById(int id)
        {
            try
            {
                TPlantillaBase entidad = base.FirstById(id);
                PlantillaBaseBO objetoBO = new PlantillaBaseBO();
                Mapper.Map<TPlantillaBase, PlantillaBaseBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public PlantillaBaseBO FirstBy(Expression<Func<TPlantillaBase, bool>> filter)
        {
            try
            {
                TPlantillaBase entidad = base.FirstBy(filter);
                PlantillaBaseBO objetoBO = Mapper.Map<TPlantillaBase, PlantillaBaseBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(PlantillaBaseBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TPlantillaBase entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<PlantillaBaseBO> listadoBO)
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

        public bool Update(PlantillaBaseBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TPlantillaBase entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<PlantillaBaseBO> listadoBO)
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
        private void AsignacionId(TPlantillaBase entidad, PlantillaBaseBO objetoBO)
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

        private TPlantillaBase MapeoEntidad(PlantillaBaseBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TPlantillaBase entidad = new TPlantillaBase();
                entidad = Mapper.Map<PlantillaBaseBO, TPlantillaBase>(objetoBO,
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
        /// Obtiene el idPlantilla por nombre
        /// </summary>
        /// <param name="nombre"></param>
        /// <returns></returns>
        public IdPlantillaBaseDTO ObtenerIdPorNombre(string nombre)
        {
            try
            {
                IdPlantillaBaseDTO idPlantillaBase = new IdPlantillaBaseDTO();
                string _queryPlantilla = "SELECT Id FROM pla.V_TPlantillaBase_IdNombre WHERE Nombre = @nombre and Estado = 1";
                var plantillaBase = _dapper.FirstOrDefault(_queryPlantilla, new { nombre });
                idPlantillaBase = JsonConvert.DeserializeObject<IdPlantillaBaseDTO>(plantillaBase);
                return idPlantillaBase;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Obtiene el idPlantilla de speech bienvenida por actividad detalle
        /// </summary>
        /// <returns></returns>
        public SpeechBienvenidaDespedidaDTO ObtenerIdPlantillaSpeechBienvenida(int idActividadDetalle, int idPlantillaBase)
        {
            try
            {
                SpeechBienvenidaDespedidaDTO speechBienvenidaDespedida = new SpeechBienvenidaDespedidaDTO()
                {
                    IdPlantillaBienvenida = 0,
                    IdPlantillaDespedida = 0
                };
                string _query = "SELECT IdPlantilla AS IdPlantillaBienvenida FROM  pla.V_DetalleActividadSpeechBienvenida WHERE IdActividadDetalle = @idActividadDetalle AND IdPlantillaBase = @idPlantillaBase";
                var registroDB = _dapper.FirstOrDefault(_query, new { idActividadDetalle, idPlantillaBase });
                if (!string.IsNullOrEmpty(registroDB) && !registroDB.Contains("[]"))
                {
                    speechBienvenidaDespedida = JsonConvert.DeserializeObject<SpeechBienvenidaDespedidaDTO>(registroDB);
                }
                return speechBienvenidaDespedida;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Obtiene el idPlantilla de speech despedida por actividad detalle
        /// </summary>
        /// <returns></returns>
        public SpeechBienvenidaDespedidaDTO ObtenerIdPlantillaSpeechDespedida(int idActividadDetalle, int idPlantillaBase)
        {
            try
            {
                SpeechBienvenidaDespedidaDTO speechBienvenidaDespedida = new SpeechBienvenidaDespedidaDTO()
                {
                    IdPlantillaBienvenida = 0,
                    IdPlantillaDespedida = 0
                };
                string _query = "SELECT IdPlantilla AS IdPlantillaDespedida FROM  pla.V_DetalleActividadSpeechDespedida WHERE IdActividadDetalle = @idActividadDetalle AND IdPlantillaBase = @idPlantillaBase";
                var registroDB = _dapper.FirstOrDefault(_query, new { idActividadDetalle, idPlantillaBase });
                if (!string.IsNullOrEmpty(registroDB) && !registroDB.Contains("[]") && !registroDB.Contains("null"))
                {
                    speechBienvenidaDespedida = JsonConvert.DeserializeObject<SpeechBienvenidaDespedidaDTO>(registroDB);
                }
                return speechBienvenidaDespedida;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Obtiene las Plantillas Base 
        /// </summary>
        /// <returns></returns>
        public List<PlantillaBaseFiltroDTO> ObtenerPlantillaBase()
        {
            try
            {
                var baseFiltro = GetBy(x => true, y => new PlantillaBaseFiltroDTO
                {
                    Id = y.Id,
                    Nombre = y.Nombre,
                    Descripcion = y.Descripcion
                }).ToList();

                return baseFiltro;

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }


        }

        /// <summary>
        /// Obtiene la plantilla Base por id 
        /// </summary>
        /// <returns></returns>
        public List<PlantillaBaseFiltroDTO> ObtenerPlantillaBasePorId(int id)
        {
            try
            {
                var idBase = FirstById(id);
                var baseFiltro = GetBy(x => x.Id == idBase.Id, y => new PlantillaBaseFiltroDTO
                {
                    Id = y.Id,
                    Nombre = y.Nombre,
                    Descripcion = y.Descripcion

                }).ToList();

                return baseFiltro;

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }


        }
    }
}


