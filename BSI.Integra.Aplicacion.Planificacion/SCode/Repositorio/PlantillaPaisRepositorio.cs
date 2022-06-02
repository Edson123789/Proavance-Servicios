using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using AutoMapper;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Planificacion.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using Newtonsoft.Json;

namespace BSI.Integra.Aplicacion.Planificacion.Repositorio
{
    public class PlantillaPaisRepositorio : BaseRepository<TPlantillaPais, PlantillaPaisBO>
    {
        #region Metodos Base
        public PlantillaPaisRepositorio() : base()
        {
        }
        public PlantillaPaisRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<PlantillaPaisBO> GetBy(Expression<Func<TPlantillaPais, bool>> filter)
        {
            IEnumerable<TPlantillaPais> listado = base.GetBy(filter);
            List<PlantillaPaisBO> listadoBO = new List<PlantillaPaisBO>();
            foreach (var itemEntidad in listado)
            {
                PlantillaPaisBO objetoBO = Mapper.Map<TPlantillaPais, PlantillaPaisBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public PlantillaPaisBO FirstById(int id)
        {
            try
            {
                TPlantillaPais entidad = base.FirstById(id);
                PlantillaPaisBO objetoBO = new PlantillaPaisBO();
                Mapper.Map<TPlantillaPais, PlantillaPaisBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public PlantillaPaisBO FirstBy(Expression<Func<TPlantillaPais, bool>> filter)
        {
            try
            {
                TPlantillaPais entidad = base.FirstBy(filter);
                PlantillaPaisBO objetoBO = Mapper.Map<TPlantillaPais, PlantillaPaisBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(PlantillaPaisBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TPlantillaPais entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<PlantillaPaisBO> listadoBO)
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

        public bool Update(PlantillaPaisBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TPlantillaPais entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<PlantillaPaisBO> listadoBO)
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
        private void AsignacionId(TPlantillaPais entidad, PlantillaPaisBO objetoBO)
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

        private TPlantillaPais MapeoEntidad(PlantillaPaisBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TPlantillaPais entidad = new TPlantillaPais();
                entidad = Mapper.Map<PlantillaPaisBO, TPlantillaPais>(objetoBO,
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
        /// Obtiene todos los documentos no asociados para un programa segun sus montos de pago
        /// </summary>
        /// <param name="idPGeneral"></param>
        /// <returns></returns>
        public List<DocumentoNoAsociadoDTO> ObtenerDocumentosNoAsociados(int idPGeneral)
        {
            try
            {
                List<DocumentoNoAsociadoDTO> documentos = new List<DocumentoNoAsociadoDTO>();
                var _query = string.Empty;
                _query = "SELECT IdDocumentos,Nombre,IdPlantillaPW,EstadoFlujo,Asignado FROM pla.V_TPlantillasPaisDocumentos WHERE " +
                    "EstadoMontos = 1 and EstadoDocumentos = 1 and Asignado = 0 and IdPrograma = @idPGeneral";
                var respuestaDapper = _dapper.QueryDapper(_query, new { idPGeneral });
                if (!string.IsNullOrEmpty(respuestaDapper) && !respuestaDapper.Contains("[]"))
                {
                    documentos = JsonConvert.DeserializeObject<List<DocumentoNoAsociadoDTO>>(respuestaDapper);
                }

                return documentos;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }
        /// <summary>
        /// Obtiene todos los documentos  asociados para un programa segun sus montos de pago
        /// </summary>
        /// <param name="idPGeneral"></param>
        /// <returns></returns>
        public List<DocumentoAsociadoDTO> ObtenerDocumentosAsociados(int idPGeneral)
        {
            try
            {
                List<DocumentoAsociadoDTO> documentos = new List<DocumentoAsociadoDTO>();
                var _query = string.Empty;
                _query = "SELECT IdDocumentos,Nombre,IdPlantillaPW,EstadoFlujo,Asignado,IdPGeneralDocumento FROM pla.V_TPGeneralDocumento WHERE " +
                    "EstadoDocumentos = 1 and EstadoPgeneral = 1 and IdPGeneral = @idPGeneral";
                var respuestaDapper = _dapper.QueryDapper(_query, new { idPGeneral });
                if (!string.IsNullOrEmpty(respuestaDapper) && !respuestaDapper.Contains("[]"))
                {
                    documentos = JsonConvert.DeserializeObject<List<DocumentoAsociadoDTO>>(respuestaDapper);
                }

                return documentos;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }

        /// <summary>
        /// Elimina (Actualiza estado a false ) todos los PlantillaPais asociados a una PlantillaPw
        /// </summary>
        /// <param name="idPlantilla"></param>
        /// <returns></returns>
        public void EliminacionLogicoPorPlantillaPw(int idPlantilla, string usuario, List<int> nuevos)
        {
            try
            {
                var listaBorrar = GetBy(x => x.IdPlantilla == idPlantilla && x.Estado == true).ToList();
                listaBorrar.RemoveAll(x => nuevos.Any(y => y == x.IdPais));
                foreach (var item in listaBorrar)
                {
                    Delete(item.Id, usuario);
                }

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
