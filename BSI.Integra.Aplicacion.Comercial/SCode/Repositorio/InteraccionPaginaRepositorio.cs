using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using AutoMapper;
using BSI.Integra.Aplicacion.Comercial.BO;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using Newtonsoft.Json;

namespace BSI.Integra.Aplicacion.Comercial.Repositorio
{
    public class InteraccionPaginaRepositorio : BaseRepository<TInteraccionPagina, InteraccionPaginaBO>
    {
        #region Metodos Base
        public InteraccionPaginaRepositorio() : base()
        {
        }
        public InteraccionPaginaRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<InteraccionPaginaBO> GetBy(Expression<Func<TInteraccionPagina, bool>> filter)
        {
            IEnumerable<TInteraccionPagina> listado = base.GetBy(filter);
            List<InteraccionPaginaBO> listadoBO = new List<InteraccionPaginaBO>();
            foreach (var itemEntidad in listado)
            {
                InteraccionPaginaBO objetoBO = Mapper.Map<TInteraccionPagina, InteraccionPaginaBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public InteraccionPaginaBO FirstById(int id)
        {
            try
            {
                TInteraccionPagina entidad = base.FirstById(id);
                InteraccionPaginaBO objetoBO = new InteraccionPaginaBO();
                Mapper.Map<TInteraccionPagina, InteraccionPaginaBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public InteraccionPaginaBO FirstBy(Expression<Func<TInteraccionPagina, bool>> filter)
        {
            try
            {
                TInteraccionPagina entidad = base.FirstBy(filter);
                InteraccionPaginaBO objetoBO = Mapper.Map<TInteraccionPagina, InteraccionPaginaBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public bool Insert(InteraccionPaginaBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TInteraccionPagina entidad = MapeoEntidad(objetoBO);

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
        public bool Insert(IEnumerable<InteraccionPaginaBO> listadoBO)
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
        public bool Update(InteraccionPaginaBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TInteraccionPagina entidad = MapeoEntidad(objetoBO);

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
        public bool Update(IEnumerable<InteraccionPaginaBO> listadoBO)
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
        private void AsignacionId(TInteraccionPagina entidad, InteraccionPaginaBO objetoBO)
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
        private TInteraccionPagina MapeoEntidad(InteraccionPaginaBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TInteraccionPagina entidad = new TInteraccionPagina();
                entidad = Mapper.Map<InteraccionPaginaBO, TInteraccionPagina>(objetoBO,
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

        public List<InteraccionAlumno> ObtenerInteraccionesPorAlumno(int IdAlumno)
        {
            try
            {
                string registrosDB = _dapper.QuerySPDapper("com.SP_ObtenerInteraccionesPorAlumno", new { IdAlumno });
                if (!string.IsNullOrEmpty(registrosDB))
                {
                    return JsonConvert.DeserializeObject<List<InteraccionAlumno>>(registrosDB);
                }
                return new List<InteraccionAlumno>();
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }

    }
}
