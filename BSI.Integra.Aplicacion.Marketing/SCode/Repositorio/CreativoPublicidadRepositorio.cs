using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using AutoMapper;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Marketing.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using Newtonsoft.Json;

namespace BSI.Integra.Aplicacion.Transversal.Repositorio
{
    public class CreativoPublicidadRepositorio : BaseRepository<TCreativoPublicidad, CreativoPublicidadBO>
    {
        #region Metodos Base
        public CreativoPublicidadRepositorio() : base()
        {
        }
        public CreativoPublicidadRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<CreativoPublicidadBO> GetBy(Expression<Func<TCreativoPublicidad, bool>> filter)
        {
            IEnumerable<TCreativoPublicidad> listado = base.GetBy(filter);
            List<CreativoPublicidadBO> listadoBO = new List<CreativoPublicidadBO>();
            foreach (var itemEntidad in listado)
            {
                CreativoPublicidadBO objetoBO = Mapper.Map<TCreativoPublicidad, CreativoPublicidadBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public CreativoPublicidadBO FirstById(int id)
        {
            try
            {
                TCreativoPublicidad entidad = base.FirstById(id);
                CreativoPublicidadBO objetoBO = new CreativoPublicidadBO();
                Mapper.Map<TCreativoPublicidad, CreativoPublicidadBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public CreativoPublicidadBO FirstBy(Expression<Func<TCreativoPublicidad, bool>> filter)
        {
            try
            {
                TCreativoPublicidad entidad = base.FirstBy(filter);
                CreativoPublicidadBO objetoBO = Mapper.Map<TCreativoPublicidad, CreativoPublicidadBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(CreativoPublicidadBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TCreativoPublicidad entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<CreativoPublicidadBO> listadoBO)
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

        public bool Update(CreativoPublicidadBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TCreativoPublicidad entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<CreativoPublicidadBO> listadoBO)
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
        private void AsignacionId(TCreativoPublicidad entidad, CreativoPublicidadBO objetoBO)
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

        private TCreativoPublicidad MapeoEntidad(CreativoPublicidadBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TCreativoPublicidad entidad = new TCreativoPublicidad();
                entidad = Mapper.Map<CreativoPublicidadBO, TCreativoPublicidad>(objetoBO,
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
        ///  Obtiene la lista de registros [Id, NOmbre] (Estado=1) de T_CreativoPublicidad (Usado para el llenado de combobox).
        /// </summary>
        /// <returns></returns>
        public List<FiltroDTO> ObtenerTodoCreativoPublicidadFiltro()
        {
            try
            {
                List<FiltroDTO> Registros = new List<FiltroDTO>();
                var _query = "SELECT Id,  Nombre FROM [mkt].[V_TCreativoPublicidad]";
                var result = _dapper.QueryDapper(_query, null);
                if (!string.IsNullOrEmpty(result) && !result.Contains("[]"))
                {
                    Registros = JsonConvert.DeserializeObject<List<FiltroDTO>>(result);
                }
                return Registros;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

       
    }
}
