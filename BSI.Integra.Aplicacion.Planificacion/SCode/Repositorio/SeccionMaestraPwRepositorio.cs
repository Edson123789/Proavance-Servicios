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
    public class SeccionMaestraPwRepositorio : BaseRepository<TSeccionMaestraPw, SeccionMaestraPwBO>
    {
        #region Metodos Base
        public SeccionMaestraPwRepositorio() : base()
        {
        }
        public SeccionMaestraPwRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<SeccionMaestraPwBO> GetBy(Expression<Func<TSeccionMaestraPw, bool>> filter)
        {
            IEnumerable<TSeccionMaestraPw> listado = base.GetBy(filter);
            List<SeccionMaestraPwBO> listadoBO = new List<SeccionMaestraPwBO>();
            foreach (var itemEntidad in listado)
            {
                SeccionMaestraPwBO objetoBO = Mapper.Map<TSeccionMaestraPw, SeccionMaestraPwBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public SeccionMaestraPwBO FirstById(int id)
        {
            try
            {
                TSeccionMaestraPw entidad = base.FirstById(id);
                SeccionMaestraPwBO objetoBO = new SeccionMaestraPwBO();
                Mapper.Map<TSeccionMaestraPw, SeccionMaestraPwBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public SeccionMaestraPwBO FirstBy(Expression<Func<TSeccionMaestraPw, bool>> filter)
        {
            try
            {
                TSeccionMaestraPw entidad = base.FirstBy(filter);
                SeccionMaestraPwBO objetoBO = Mapper.Map<TSeccionMaestraPw, SeccionMaestraPwBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(SeccionMaestraPwBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TSeccionMaestraPw entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<SeccionMaestraPwBO> listadoBO)
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

        public bool Update(SeccionMaestraPwBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TSeccionMaestraPw entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<SeccionMaestraPwBO> listadoBO)
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
        private void AsignacionId(TSeccionMaestraPw entidad, SeccionMaestraPwBO objetoBO)
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

        private TSeccionMaestraPw MapeoEntidad(SeccionMaestraPwBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TSeccionMaestraPw entidad = new TSeccionMaestraPw();
                entidad = Mapper.Map<SeccionMaestraPwBO, TSeccionMaestraPw>(objetoBO,
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
        /// Obtiene todos los registros de SeccionMaestra por el IdPlantillaMaestroPw
        /// </summary>
        /// <returns></returns>
        public List<SeccionMaestraPwFiltroDTO> ObtenerSeccionMaestraPorIdPlantillaMaestra(int idPlantillaMaestro)
        {
            try
            {
                List<SeccionMaestraPwFiltroDTO> seccionesMaestra = new List<SeccionMaestraPwFiltroDTO>();
                var _query = string.Empty;
                _query = "SELECT Id,Nombre,Descripcion,Contenido,IdPlantillaMaestroPw FROM pla.V_ObtenerSeccionMaestraPorIdPlantillaMaestroPw WHERE IdPlantillaMaestroPw = @IdPlantillaMaestroPw and Estado = 1 ";
                var seccionMaestra = _dapper.QueryDapper(_query, new { IdPlantillaMaestroPw = idPlantillaMaestro });
                seccionesMaestra = JsonConvert.DeserializeObject<List<SeccionMaestraPwFiltroDTO>>(seccionMaestra);

                return seccionesMaestra;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Elimina (Actualiza estado a false ) todos las registros de ParametroSeo asociados a TagPw
        /// </summary>
        /// <param name="idPlantillaMaestro"></param>
        /// <param name="usuario"></param>
        /// <param name="nuevos"></param>
        public void EliminacionLogicoPorIdPlantillaMaestro(int idPlantillaMaestro, string usuario, List<SeccionMaestraPwDTO> nuevos)
        {
            try
            {
                var listaBorrar = GetBy(x => x.IdPlantillaMaestroPw == idPlantillaMaestro && x.Estado == true).ToList();
                listaBorrar.RemoveAll(x => nuevos.Any(y => y.Equals(x.IdPlantillaMaestroPw)));
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
