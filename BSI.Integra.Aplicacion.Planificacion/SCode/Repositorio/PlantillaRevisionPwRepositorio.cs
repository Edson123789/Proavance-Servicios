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
    public class PlantillaRevisionPwRepositorio : BaseRepository<TPlantillaRevisionPw, PlantillaRevisionPwBO>
    {
        #region Metodos Base
        public PlantillaRevisionPwRepositorio() : base()
        {
        }
        public PlantillaRevisionPwRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<PlantillaRevisionPwBO> GetBy(Expression<Func<TPlantillaRevisionPw, bool>> filter)
        {
            IEnumerable<TPlantillaRevisionPw> listado = base.GetBy(filter);
            List<PlantillaRevisionPwBO> listadoBO = new List<PlantillaRevisionPwBO>();
            foreach (var itemEntidad in listado)
            {
                PlantillaRevisionPwBO objetoBO = Mapper.Map<TPlantillaRevisionPw, PlantillaRevisionPwBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public PlantillaRevisionPwBO FirstById(int id)
        {
            try
            {
                TPlantillaRevisionPw entidad = base.FirstById(id);
                PlantillaRevisionPwBO objetoBO = new PlantillaRevisionPwBO();
                Mapper.Map<TPlantillaRevisionPw, PlantillaRevisionPwBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public PlantillaRevisionPwBO FirstBy(Expression<Func<TPlantillaRevisionPw, bool>> filter)
        {
            try
            {
                TPlantillaRevisionPw entidad = base.FirstBy(filter);
                PlantillaRevisionPwBO objetoBO = Mapper.Map<TPlantillaRevisionPw, PlantillaRevisionPwBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(PlantillaRevisionPwBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TPlantillaRevisionPw entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<PlantillaRevisionPwBO> listadoBO)
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

        public bool Update(PlantillaRevisionPwBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TPlantillaRevisionPw entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<PlantillaRevisionPwBO> listadoBO)
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
        private void AsignacionId(TPlantillaRevisionPw entidad, PlantillaRevisionPwBO objetoBO)
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

        private TPlantillaRevisionPw MapeoEntidad(PlantillaRevisionPwBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TPlantillaRevisionPw entidad = new TPlantillaRevisionPw();
                entidad = Mapper.Map<PlantillaRevisionPwBO, TPlantillaRevisionPw>(objetoBO,
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
        /// Obtiene todos los registros de Plantilla Revisiones Nivel por el IdPlantillaPW.
        /// </summary>
        /// <returns></returns>
        public List<PlantillaRevisionPwFiltroPlantillaPwDTO> ObtenerPlantillaRevisionNivelPorIdPlantillaPW(int idPlantillaPw)
        {
            try
            {
                List<PlantillaRevisionPwFiltroPlantillaPwDTO> documentos = new List<PlantillaRevisionPwFiltroPlantillaPwDTO>();
                var _query = string.Empty;
                _query = "SELECT Id,IdPlantillaPw,IdRevisionNivelPw,IdPersonal,Nombre,Prioridad,NombreCompleto,Email,Rol,IdTipoRevisionPw FROM pla.V_ObtenerPlantillaRevisionNivelPorIdPlantilla WHERE " +
                    "IdPlantillaPw = @IdPlantillaPw";
                var respuestaDapper = _dapper.QueryDapper(_query, new { IdPlantillaPw = idPlantillaPw });
                if (!string.IsNullOrEmpty(respuestaDapper) && !respuestaDapper.Contains("[]"))
                {
                    documentos = JsonConvert.DeserializeObject<List<PlantillaRevisionPwFiltroPlantillaPwDTO>>(respuestaDapper);
                }

                return documentos;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }

        /// <summary>
        /// Elimina (Actualiza estado a false ) todos las Plantilla Revision asociados a una PlantillaPw
        /// </summary>
        /// <param name="idPlantilla"></param>
        /// <returns></returns>
        public void EliminacionLogicoPorPlantillaPw(int idPlantilla, string usuario, List<PlantillaRevisionPwDTO> nuevos)
        {
            try
            {
                var listaBorrar = GetBy(x => x.IdPlantillaPw == idPlantilla && x.Estado == true).ToList();
                listaBorrar.RemoveAll(x => nuevos.Any(y => y.IdRevisionNivelPw == x.IdRevisionNivelPw));
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
