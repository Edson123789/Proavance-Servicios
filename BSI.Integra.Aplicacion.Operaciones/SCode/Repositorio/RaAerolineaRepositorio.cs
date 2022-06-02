using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using AutoMapper;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Operaciones.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;

namespace BSI.Integra.Aplicacion.Operaciones.Repositorio
{
    public class RaAerolineaRepositorio : BaseRepository<TRaAerolinea, RaAerolineaBO>
    {
        #region Metodos Base
        public RaAerolineaRepositorio() : base()
        {
        }
        public RaAerolineaRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<RaAerolineaBO> GetBy(Expression<Func<TRaAerolinea, bool>> filter)
        {
            IEnumerable<TRaAerolinea> listado = base.GetBy(filter);
            List<RaAerolineaBO> listadoBO = new List<RaAerolineaBO>();
            foreach (var itemEntidad in listado)
            {
                RaAerolineaBO objetoBO = Mapper.Map<TRaAerolinea, RaAerolineaBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public RaAerolineaBO FirstById(int id)
        {
            try
            {
                TRaAerolinea entidad = base.FirstById(id);
                RaAerolineaBO objetoBO = new RaAerolineaBO();
                Mapper.Map<TRaAerolinea, RaAerolineaBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public RaAerolineaBO FirstBy(Expression<Func<TRaAerolinea, bool>> filter)
        {
            try
            {
                TRaAerolinea entidad = base.FirstBy(filter);
                RaAerolineaBO objetoBO = Mapper.Map<TRaAerolinea, RaAerolineaBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(RaAerolineaBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TRaAerolinea entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<RaAerolineaBO> listadoBO)
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

        public bool Update(RaAerolineaBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TRaAerolinea entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<RaAerolineaBO> listadoBO)
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
        private void AsignacionId(TRaAerolinea entidad, RaAerolineaBO objetoBO)
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

        private TRaAerolinea MapeoEntidad(RaAerolineaBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TRaAerolinea entidad = new TRaAerolinea();
                entidad = Mapper.Map<RaAerolineaBO, TRaAerolinea>(objetoBO,
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

		public List<FiltroGenericoDTO> ObtenerAerolineasFiltro()
		{
			try
			{
				return GetBy(x => x.Estado == true, x => new FiltroGenericoDTO { Value = x.Id, Text = x.Nombre }).ToList();
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}
    }
}
