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
    /// Repositorio: NivelEstudioRepositorio
    /// Autor: Britsel Calluchi - Luis Huallpa - Edgar Serruto.
    /// Fecha: 29/01/2021
    /// <summary>
    /// Gestión de T_NivelEstudio
    /// </summary>
    public class NivelEstudioRepositorio : BaseRepository<TNivelEstudio, NivelEstudioBO>
    {
        #region Metodos Base
        public NivelEstudioRepositorio() : base()
        {
        }
        public NivelEstudioRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<NivelEstudioBO> GetBy(Expression<Func<TNivelEstudio, bool>> filter)
        {
            IEnumerable<TNivelEstudio> listado = base.GetBy(filter);
            List<NivelEstudioBO> listadoBO = new List<NivelEstudioBO>();
            foreach (var itemEntidad in listado)
            {
                NivelEstudioBO objetoBO = Mapper.Map<TNivelEstudio, NivelEstudioBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public NivelEstudioBO FirstById(int id)
        {
            try
            {
                TNivelEstudio entidad = base.FirstById(id);
                NivelEstudioBO objetoBO = new NivelEstudioBO();
                Mapper.Map<TNivelEstudio, NivelEstudioBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public NivelEstudioBO FirstBy(Expression<Func<TNivelEstudio, bool>> filter)
        {
            try
            {
                TNivelEstudio entidad = base.FirstBy(filter);
                NivelEstudioBO objetoBO = Mapper.Map<TNivelEstudio, NivelEstudioBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(NivelEstudioBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TNivelEstudio entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<NivelEstudioBO> listadoBO)
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

        public bool Update(NivelEstudioBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TNivelEstudio entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<NivelEstudioBO> listadoBO)
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
        private void AsignacionId(TNivelEstudio entidad, NivelEstudioBO objetoBO)
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

        private TNivelEstudio MapeoEntidad(NivelEstudioBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TNivelEstudio entidad = new TNivelEstudio();
                entidad = Mapper.Map<NivelEstudioBO, TNivelEstudio>(objetoBO,
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
        /// Repositorio: NivelEstudioRepositorio
        /// Autor: Britsel Calluchi - Luis Huallpa - Edgar Serruto.
        /// Fecha: 29/01/2021
        /// <summary>
        /// Obtiene lista de elementos registrados para combo
        /// </summary>
        /// <returns>List<FiltroIdNombreDTO></returns>
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
        /// Repositorio: NivelEstudioRepositorio
        /// Autor: Britsel Calluchi - Luis Huallpa - Edgar Serruto.
        /// Fecha: 29/01/2021
        /// <summary>
        /// Obtiene lista de niveles de estudio registrados
        /// </summary>
        /// <returns>List<NivelEstudioDTO></returns>
		public List<NivelEstudioDTO> ObtenerListaNivelEstudio()
		{
			try
			{
				List<NivelEstudioDTO> lista = new List<NivelEstudioDTO>();
				var query = "SELECT Id, Nombre, IdTipoFormacion, TipoFormacion FROM [gp].[V_TNivelEstudio_ObtenerListaNivelEstudio] WHERE Estado = 1";
				var res = _dapper.QueryDapper(query, null);
				if (!res.Contains("[]") && !string.IsNullOrEmpty(res))
				{
					lista = JsonConvert.DeserializeObject<List<NivelEstudioDTO>>(res);
				}
				return lista;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}
	}
}
