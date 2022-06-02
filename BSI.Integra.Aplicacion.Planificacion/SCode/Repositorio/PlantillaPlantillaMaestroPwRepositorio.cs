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
    public class PlantillaPlantillaMaestroPwRepositorio : BaseRepository<TPlantillaPlantillaMaestroPw, PlantillaPlantillaMaestroPwBO>
    {
        #region Metodos Base
        public PlantillaPlantillaMaestroPwRepositorio() : base()
        {
        }
        public PlantillaPlantillaMaestroPwRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<PlantillaPlantillaMaestroPwBO> GetBy(Expression<Func<TPlantillaPlantillaMaestroPw, bool>> filter)
        {
            IEnumerable<TPlantillaPlantillaMaestroPw> listado = base.GetBy(filter);
            List<PlantillaPlantillaMaestroPwBO> listadoBO = new List<PlantillaPlantillaMaestroPwBO>();
            foreach (var itemEntidad in listado)
            {
                PlantillaPlantillaMaestroPwBO objetoBO = Mapper.Map<TPlantillaPlantillaMaestroPw, PlantillaPlantillaMaestroPwBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public PlantillaPlantillaMaestroPwBO FirstById(int id)
        {
            try
            {
                TPlantillaPlantillaMaestroPw entidad = base.FirstById(id);
                PlantillaPlantillaMaestroPwBO objetoBO = new PlantillaPlantillaMaestroPwBO();
                Mapper.Map<TPlantillaPlantillaMaestroPw, PlantillaPlantillaMaestroPwBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public PlantillaPlantillaMaestroPwBO FirstBy(Expression<Func<TPlantillaPlantillaMaestroPw, bool>> filter)
        {
            try
            {
                TPlantillaPlantillaMaestroPw entidad = base.FirstBy(filter);
                PlantillaPlantillaMaestroPwBO objetoBO = Mapper.Map<TPlantillaPlantillaMaestroPw, PlantillaPlantillaMaestroPwBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(PlantillaPlantillaMaestroPwBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TPlantillaPlantillaMaestroPw entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<PlantillaPlantillaMaestroPwBO> listadoBO)
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

        public bool Update(PlantillaPlantillaMaestroPwBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TPlantillaPlantillaMaestroPw entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<PlantillaPlantillaMaestroPwBO> listadoBO)
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
        private void AsignacionId(TPlantillaPlantillaMaestroPw entidad, PlantillaPlantillaMaestroPwBO objetoBO)
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

        private TPlantillaPlantillaMaestroPw MapeoEntidad(PlantillaPlantillaMaestroPwBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TPlantillaPlantillaMaestroPw entidad = new TPlantillaPlantillaMaestroPw();
                entidad = Mapper.Map<PlantillaPlantillaMaestroPwBO, TPlantillaPlantillaMaestroPw>(objetoBO,
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
        /// Obtiene todos los registros de PlantillaPlantillaMaestro por el IdPlantillaPw
        /// </summary>
        /// <returns></returns>
        public List<SeccionPwFiltroPlantillaPwDTO> ObtenerPlantillaMaestroPorIdPlantillaPw(int idPlantilla)
        {
            try
            {
                List<SeccionPwFiltroPlantillaPwDTO> plantillasMaestro = new List<SeccionPwFiltroPlantillaPwDTO>();
                var _query = string.Empty;
                _query = "SELECT Id,IdPlantillaPw,Nombre,IdSeccionMaestraPw,Contenido,Descripcion,Titulo,Posicion,Tipo FROM pla.V_ObtenerPlantillaMaestroSeccionPorIdPlantilla WHERE IdPlantillaPw = @IdPlantillaPw and Estado = 1 ";
                var plantillaMaestro = _dapper.QueryDapper(_query, new { IdPlantillaPw = idPlantilla });
                plantillasMaestro = JsonConvert.DeserializeObject<List<SeccionPwFiltroPlantillaPwDTO>>(plantillaMaestro);

                return plantillasMaestro;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Elimina (Actualiza estado a false ) todos las PlantillaPlantillaMaestro asociados a una PlantillaPw
        /// </summary>
        /// <param name="idPlantilla"></param>
        /// <returns></returns>
        public void EliminacionLogicoPorPlantillaPw(int idPlantilla, string usuario, List<PlantillaPlantillaMaestroPwDTO> nuevos)
        {
            try
            {
                var listaBorrar = GetBy(x => x.IdPlantillaPw == idPlantilla && x.Estado == true).ToList();
                listaBorrar.RemoveAll(x => nuevos.Any(y => y.IdSeccionMaestraPw == x.IdSeccionMaestraPw));
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

        /// <summary>
        /// Obtiene el registro de PlantillaPlantillaMaestro filtrado por el IdPlantillaPW y IdSeccionMaestra
        /// </summary>
        /// <param name="idPlantilla"></param>
        /// <param name="idSeccion"></param>
        /// <returns></returns>
        //public List<PlantillaPlantillaMaestroPwDTO> ObtenerPorIdPlantillaIdSeccionMaestro(int idPlantilla, int idSeccionMaestra)
        //{
        //    try
        //    {
        //        List<PlantillaPlantillaMaestroPwFiltroDTO> plantillasMaestro = new List<PlantillaPlantillaMaestroPwFiltroDTO>();
        //        var _query = string.Empty;
        //        _query = "SELECT Id,IdPlantillaPw,Nombre,IdSeccionMaestraPw,Contenido,Descripcion,Titulo,Posicion,Tipo FROM pla.V_ObtenerPlantillaMaestroSeccionPorIdPlantilla WHERE IdPlantillaPw = @IdPlantillaPw and Estado = 1 ";
        //        var plantillaMaestro = _dapper.QueryDapper(_query, new { IdPlantillaPw = idPlantilla });
        //        plantillasMaestro = JsonConvert.DeserializeObject<List<PlantillaPlantillaMaestroPwFiltroDTO>>(plantillaMaestro);

        //        return plantillasMaestro;
        //    }
        //    catch (Exception e)
        //    {
        //        throw new Exception(e.Message);
        //    }
        //}
    }
     
}
