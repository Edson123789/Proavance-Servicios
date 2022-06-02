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
    public class TerminoUsoSitioWebPwRepositorio : BaseRepository<TTerminoUsoSitioWebPw, TerminoUsoSitioWebPwBO>
    {
        #region Metodos Base
        public TerminoUsoSitioWebPwRepositorio() : base()
        {
        }
        public TerminoUsoSitioWebPwRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<TerminoUsoSitioWebPwBO> GetBy(Expression<Func<TTerminoUsoSitioWebPw, bool>> filter)
        {
            IEnumerable<TTerminoUsoSitioWebPw> listado = base.GetBy(filter);
            List<TerminoUsoSitioWebPwBO> listadoBO = new List<TerminoUsoSitioWebPwBO>();
            foreach (var itemEntidad in listado)
            {
                TerminoUsoSitioWebPwBO objetoBO = Mapper.Map<TTerminoUsoSitioWebPw, TerminoUsoSitioWebPwBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public TerminoUsoSitioWebPwBO FirstById(int id)
        {
            try
            {
                TTerminoUsoSitioWebPw entidad = base.FirstById(id);
                TerminoUsoSitioWebPwBO objetoBO = new TerminoUsoSitioWebPwBO();
                Mapper.Map<TTerminoUsoSitioWebPw, TerminoUsoSitioWebPwBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public TerminoUsoSitioWebPwBO FirstBy(Expression<Func<TTerminoUsoSitioWebPw, bool>> filter)
        {
            try
            {
                TTerminoUsoSitioWebPw entidad = base.FirstBy(filter);
                TerminoUsoSitioWebPwBO objetoBO = Mapper.Map<TTerminoUsoSitioWebPw, TerminoUsoSitioWebPwBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(TerminoUsoSitioWebPwBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TTerminoUsoSitioWebPw entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<TerminoUsoSitioWebPwBO> listadoBO)
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

        public bool Update(TerminoUsoSitioWebPwBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TTerminoUsoSitioWebPw entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<TerminoUsoSitioWebPwBO> listadoBO)
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
        private void AsignacionId(TTerminoUsoSitioWebPw entidad, TerminoUsoSitioWebPwBO objetoBO)
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

        private TTerminoUsoSitioWebPw MapeoEntidad(TerminoUsoSitioWebPwBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TTerminoUsoSitioWebPw entidad = new TTerminoUsoSitioWebPw();
                entidad = Mapper.Map<TerminoUsoSitioWebPwBO, TTerminoUsoSitioWebPw>(objetoBO,
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
        /// Obtiene todos los registros sin los campos de auditoría.
        /// </summary>
        /// <returns></returns>
        public List<TerminoUsoSitioWebPwDTO> ObtenerTodoGrid()
        {
            try
            {
                var lista = GetBy(x => true, y => new TerminoUsoSitioWebPwDTO
                {
                    Id = y.Id,
                    CodigoIsopais = y.CodigoIsopais,
                    NombrePais = y.NombrePais,
                    Nombre = y.Nombre,
                    Contenido = y.Contenido,    
                }).OrderByDescending(x => x.Id).ToList();

                return lista;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

    }
     
}
