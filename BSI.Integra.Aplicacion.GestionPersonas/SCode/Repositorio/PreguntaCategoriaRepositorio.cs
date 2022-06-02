using AutoMapper;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.GestionPersonas.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace BSI.Integra.Aplicacion.GestionPersonas.Repositorio
{
    public class PreguntaCategoriaRepositorio : BaseRepository<TPreguntaCategoria,PreguntaCategoriaBO>
    {
        #region Metodos Base
        public PreguntaCategoriaRepositorio() : base()
        {
        }
        public PreguntaCategoriaRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<PreguntaCategoriaBO> GetBy(Expression<Func<TPreguntaCategoria, bool>> filter)
        {
            IEnumerable<TPreguntaCategoria> listado = base.GetBy(filter);
            List<PreguntaCategoriaBO> listadoBO = new List<PreguntaCategoriaBO>();
            foreach (var itemEntidad in listado)
            {
                PreguntaCategoriaBO objetoBO = Mapper.Map<TPreguntaCategoria, PreguntaCategoriaBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public PreguntaCategoriaBO FirstById(int id)
        {
            try
            {
                TPreguntaCategoria entidad = base.FirstById(id);
                PreguntaCategoriaBO objetoBO = new PreguntaCategoriaBO();
                Mapper.Map<TPreguntaCategoria, PreguntaCategoriaBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public PreguntaCategoriaBO FirstBy(Expression<Func<TPreguntaCategoria, bool>> filter)
        {
            try
            {
                TPreguntaCategoria entidad = base.FirstBy(filter);
                PreguntaCategoriaBO objetoBO = Mapper.Map<TPreguntaCategoria, PreguntaCategoriaBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(PreguntaCategoriaBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TPreguntaCategoria entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<PreguntaCategoriaBO> listadoBO)
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

        public bool Update(PreguntaCategoriaBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TPreguntaCategoria entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<PreguntaCategoriaBO> listadoBO)
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
        private void AsignacionId(TPreguntaCategoria entidad, PreguntaCategoriaBO objetoBO)
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

        private TPreguntaCategoria MapeoEntidad(PreguntaCategoriaBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TPreguntaCategoria entidad = new TPreguntaCategoria();
                entidad = Mapper.Map<PreguntaCategoriaBO, TPreguntaCategoria>(objetoBO,
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
		/// Obtiene lista de categoria de preguntas registradas
		/// </summary>
		/// <returns></returns>
		public List<FiltroDTO> ObtenerCategoriasPreguntasRegistradas()
		{
			try
			{
				return this.GetBy(x => x.Estado == true).Select(x => new FiltroDTO { Id = x.Id, Nombre = x.Nombre }).ToList();
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}
    }
}
