using System;
using System.Collections.Generic;
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
    public class TituloRepositorio : BaseRepository<TTitulo, TituloBO>
    {
        #region Metodos Base
        public TituloRepositorio() : base()
        {
        }
        public TituloRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<TituloBO> GetBy(Expression<Func<TTitulo, bool>> filter)
        {
            IEnumerable<TTitulo> listado = base.GetBy(filter);
            List<TituloBO> listadoBO = new List<TituloBO>();
            foreach (var itemEntidad in listado)
            {
                TituloBO objetoBO = Mapper.Map<TTitulo, TituloBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public TituloBO FirstById(int id)
        {
            try
            {
                TTitulo entidad = base.FirstById(id);
                TituloBO objetoBO = new TituloBO();
                Mapper.Map<TTitulo, TituloBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public TituloBO FirstBy(Expression<Func<TTitulo, bool>> filter)
        {
            try
            {
                TTitulo entidad = base.FirstBy(filter);
                TituloBO objetoBO = Mapper.Map<TTitulo, TituloBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(TituloBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TTitulo entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<TituloBO> listadoBO)
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

        public bool Update(TituloBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TTitulo entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<TituloBO> listadoBO)
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
        private void AsignacionId(TTitulo entidad, TituloBO objetoBO)
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

        private TTitulo MapeoEntidad(TituloBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TTitulo entidad = new TTitulo();
                entidad = Mapper.Map<TituloBO, TTitulo>(objetoBO,
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
        /// Obtiene titulos para seleccionar el Registro de Adicionales web programa general 
        /// con los campos necesarios para llenar un Combobox
        /// </summary>
        /// <returns></returns>
        public List<TituloFiltroDTO> ObtenerTitulosFiltro()
        {
            try
            {
                List<TituloFiltroDTO> titulo = new List<TituloFiltroDTO>();
                string _queryTitulo = string.Empty;
                _queryTitulo = "Select Id,Nombre From pla.V_TTitulo_Filtro Where Estado=1";
                var queryTitulo = _dapper.QueryDapper(_queryTitulo, null);
                if (!string.IsNullOrEmpty(queryTitulo) && !queryTitulo.Contains("[]"))
                {
                    titulo = JsonConvert.DeserializeObject<List<TituloFiltroDTO>>(queryTitulo);
                }
                return titulo;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
