using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using AutoMapper;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.GestionPersonas.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using Newtonsoft.Json;

namespace BSI.Integra.Aplicacion.GestionPersonas.Repositorio
{
    /// Repositorio: GradoEstudioRepositorio
    /// Autor: Britsel Calluchi - Luis Huallpa - Edgar Serruto.
    /// Fecha: 29/01/2021
    /// <summary>
    /// Gestión de Grados de Estudio
    /// </summary>
    public class GradoEstudioRepositorio : BaseRepository<TGradoEstudio, GradoEstudioBO>
    {
        #region Metodos Base
        public GradoEstudioRepositorio() : base()
        {
        }
        public GradoEstudioRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<GradoEstudioBO> GetBy(Expression<Func<TGradoEstudio, bool>> filter)
        {
            IEnumerable<TGradoEstudio> listado = base.GetBy(filter);
            List<GradoEstudioBO> listadoBO = new List<GradoEstudioBO>();
            foreach (var itemEntidad in listado)
            {
                GradoEstudioBO objetoBO = Mapper.Map<TGradoEstudio, GradoEstudioBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public GradoEstudioBO FirstById(int id)
        {
            try
            {
                TGradoEstudio entidad = base.FirstById(id);
                GradoEstudioBO objetoBO = new GradoEstudioBO();
                Mapper.Map<TGradoEstudio, GradoEstudioBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public GradoEstudioBO FirstBy(Expression<Func<TGradoEstudio, bool>> filter)
        {
            try
            {
                TGradoEstudio entidad = base.FirstBy(filter);
                GradoEstudioBO objetoBO = Mapper.Map<TGradoEstudio, GradoEstudioBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(GradoEstudioBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TGradoEstudio entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<GradoEstudioBO> listadoBO)
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

        public bool Update(GradoEstudioBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TGradoEstudio entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<GradoEstudioBO> listadoBO)
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
        private void AsignacionId(TGradoEstudio entidad, GradoEstudioBO objetoBO)
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

        private TGradoEstudio MapeoEntidad(GradoEstudioBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TGradoEstudio entidad = new TGradoEstudio();
                entidad = Mapper.Map<GradoEstudioBO, TGradoEstudio>(objetoBO,
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

        /// Repositorio: GradoEstudioRepositorio
        /// Autor: Edgar S.
        /// Fecha: 29/01/2021
        /// <summary>
        /// Obtiene lista de elementos registrados para combo
        /// </summary>
        /// <returns> Lista de Id, Nombre de nivel de Grado de Estudios Registrados </returns>
        /// <returns> Lista de Objeto DTO : List<FiltroIdNombreDTO> </returns>
        public List<FiltroIdNombreDTO> ObtenerListaParaFiltro()
		{
			try
			{
				return this.GetBy(x => true).Select(x => new FiltroIdNombreDTO
				{
					Id = x.Id,
					Nombre = x.Nombre
				}).ToList(); ;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}
        /// Repositorio: GradoEstudioRepositorio
        /// Autor: Luis Huallpa - Edgar S.
        /// Fecha: 29/01/2021
        /// <summary>
        /// Obtiene lista de elementos registrados para combo
        /// </summary>
        /// <returns> List<FiltroIdNombreDTO> </returns>
        public List<FiltroIdNombreDTO> ObtenerListaEstadoEstudioParaFiltro()
		{
			try
			{
				var query = "SELECT Id, Nombre FROM gp.T_EstadoEstudio WHERE Estado = 1";
				var resultado = _dapper.QueryDapper(query, null);
				return JsonConvert.DeserializeObject<List<FiltroIdNombreDTO>>(resultado);
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}
	}
}
