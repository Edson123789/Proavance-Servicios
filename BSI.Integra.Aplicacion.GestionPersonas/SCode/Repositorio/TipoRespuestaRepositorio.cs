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
    /// Repositorio: TipoRespuestaRepositorio
    /// Autor: Britsel Calluchi
    /// Fecha: 07/09/2021
    /// <summary>
    /// Repositorio para la tabla T_TipoRespuesta
    /// </summary>
    public class TipoRespuestaRepositorio: BaseRepository<TTipoRespuesta, TipoRespuestaBO>
    {
        #region Metodos Base
        public TipoRespuestaRepositorio() : base()
        {
        }
        public TipoRespuestaRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<TipoRespuestaBO> GetBy(Expression<Func<TTipoRespuesta, bool>> filter)
        {
            IEnumerable<TTipoRespuesta> listado = base.GetBy(filter);
            List<TipoRespuestaBO> listadoBO = new List<TipoRespuestaBO>();
            foreach (var itemEntidad in listado)
            {
                TipoRespuestaBO objetoBO = Mapper.Map<TTipoRespuesta, TipoRespuestaBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public TipoRespuestaBO FirstById(int id)
        {
            try
            {
                TTipoRespuesta entidad = base.FirstById(id);
                TipoRespuestaBO objetoBO = new TipoRespuestaBO();
                Mapper.Map<TTipoRespuesta, TipoRespuestaBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public TipoRespuestaBO FirstBy(Expression<Func<TTipoRespuesta, bool>> filter)
        {
            try
            {
                TTipoRespuesta entidad = base.FirstBy(filter);
                TipoRespuestaBO objetoBO = Mapper.Map<TTipoRespuesta, TipoRespuestaBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(TipoRespuestaBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TTipoRespuesta entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<TipoRespuestaBO> listadoBO)
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

        public bool Update(TipoRespuestaBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TTipoRespuesta entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<TipoRespuestaBO> listadoBO)
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
        private void AsignacionId(TTipoRespuesta entidad, TipoRespuestaBO objetoBO)
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

        private TTipoRespuesta MapeoEntidad(TipoRespuestaBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TTipoRespuesta entidad = new TTipoRespuesta();
                entidad = Mapper.Map<TipoRespuestaBO, TTipoRespuesta>(objetoBO,
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
    }
}
