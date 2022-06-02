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
    public class SeccionPreguntaFrecuenteRepositorio : BaseRepository<TSeccionPreguntaFrecuente, SeccionPreguntaFrecuenteBO>
    {
        #region Metodos Base
        public SeccionPreguntaFrecuenteRepositorio() : base()
        {
        }
        public SeccionPreguntaFrecuenteRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<SeccionPreguntaFrecuenteBO> GetBy(Expression<Func<TSeccionPreguntaFrecuente, bool>> filter)
        {
            IEnumerable<TSeccionPreguntaFrecuente> listado = base.GetBy(filter);
            List<SeccionPreguntaFrecuenteBO> listadoBO = new List<SeccionPreguntaFrecuenteBO>();
            foreach (var itemEntidad in listado)
            {
                SeccionPreguntaFrecuenteBO objetoBO = Mapper.Map<TSeccionPreguntaFrecuente, SeccionPreguntaFrecuenteBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public SeccionPreguntaFrecuenteBO FirstById(int id)
        {
            try
            {
                TSeccionPreguntaFrecuente entidad = base.FirstById(id);
                SeccionPreguntaFrecuenteBO objetoBO = new SeccionPreguntaFrecuenteBO();
                Mapper.Map<TSeccionPreguntaFrecuente, SeccionPreguntaFrecuenteBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public SeccionPreguntaFrecuenteBO FirstBy(Expression<Func<TSeccionPreguntaFrecuente, bool>> filter)
        {
            try
            {
                TSeccionPreguntaFrecuente entidad = base.FirstBy(filter);
                SeccionPreguntaFrecuenteBO objetoBO = Mapper.Map<TSeccionPreguntaFrecuente, SeccionPreguntaFrecuenteBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(SeccionPreguntaFrecuenteBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TSeccionPreguntaFrecuente entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<SeccionPreguntaFrecuenteBO> listadoBO)
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

        public bool Update(SeccionPreguntaFrecuenteBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TSeccionPreguntaFrecuente entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<SeccionPreguntaFrecuenteBO> listadoBO)
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
        private void AsignacionId(TSeccionPreguntaFrecuente entidad, SeccionPreguntaFrecuenteBO objetoBO)
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

        private TSeccionPreguntaFrecuente MapeoEntidad(SeccionPreguntaFrecuenteBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TSeccionPreguntaFrecuente entidad = new TSeccionPreguntaFrecuente();
                entidad = Mapper.Map<SeccionPreguntaFrecuenteBO, TSeccionPreguntaFrecuente>(objetoBO,
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
        /// Obtiene todos los registros de SeccionPreguntaFrecuente con los campos Id y Nombre.
        /// </summary>
        /// <returns></returns>
        public List<SeccionPreguntaFrecuenteFiltroDTO> ObtenerSeccionPreguntaFrecuenteFiltro()
        {
            try
            {
                var lista = GetBy(x => true, y => new SeccionPreguntaFrecuenteFiltroDTO
                {
                    Id = y.Id,
                    Nombre = y.Nombre,
                }).ToList();

                return lista;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

    }
     
}
