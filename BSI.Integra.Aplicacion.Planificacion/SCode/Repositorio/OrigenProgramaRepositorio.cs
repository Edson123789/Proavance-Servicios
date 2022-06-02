using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using AutoMapper;
using BSI.Integra.Aplicacion.Planificacion.BO;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using Newtonsoft.Json;

namespace BSI.Integra.Aplicacion.Planificacion.Repositorio
{
    public class OrigenProgramaRepositorio : BaseRepository<TOrigenPrograma, OrigenProgramaBO>
    {
        #region Metodos Base
        public OrigenProgramaRepositorio() : base()
        {
        }
        public OrigenProgramaRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<OrigenProgramaBO> GetBy(Expression<Func<TOrigenPrograma, bool>> filter)
        {
            IEnumerable<TOrigenPrograma> listado = base.GetBy(filter);
            List<OrigenProgramaBO> listadoBO = new List<OrigenProgramaBO>();
            foreach (var itemEntidad in listado)
            {
                OrigenProgramaBO objetoBO = Mapper.Map<TOrigenPrograma, OrigenProgramaBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public OrigenProgramaBO FirstById(int id)
        {
            try
            {
                TOrigenPrograma entidad = base.FirstById(id);
                OrigenProgramaBO objetoBO = new OrigenProgramaBO();
                Mapper.Map<TOrigenPrograma, OrigenProgramaBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public OrigenProgramaBO FirstBy(Expression<Func<TOrigenPrograma, bool>> filter)
        {
            try
            {
                TOrigenPrograma entidad = base.FirstBy(filter);
                OrigenProgramaBO objetoBO = Mapper.Map<TOrigenPrograma, OrigenProgramaBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(OrigenProgramaBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TOrigenPrograma entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<OrigenProgramaBO> listadoBO)
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

        public bool Update(OrigenProgramaBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TOrigenPrograma entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<OrigenProgramaBO> listadoBO)
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
        private void AsignacionId(TOrigenPrograma entidad, OrigenProgramaBO objetoBO)
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

        private TOrigenPrograma MapeoEntidad(OrigenProgramaBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TOrigenPrograma entidad = new TOrigenPrograma();
                entidad = Mapper.Map<OrigenProgramaBO, TOrigenPrograma>(objetoBO,
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

		public List<OrigenProgramasDTO> ObtenerOrigenProgramas()
		{
			try
			{
				string _queryOrigenPrograma = "Select Id,Nombre From pla.V_DatosOrigenPrograma Where Estado=1";
				var queryOrigenPrograma = _dapper.QueryDapper(_queryOrigenPrograma, null);
				return JsonConvert.DeserializeObject<List<OrigenProgramasDTO>>(queryOrigenPrograma);
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}
	}
}
