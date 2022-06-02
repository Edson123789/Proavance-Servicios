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
    public class TipoAmbienteRepositorio : BaseRepository<TTipoAmbiente, TipoAmbienteBO>
    {
        #region Metodos Base
        public TipoAmbienteRepositorio() : base()
        {
        }
        public TipoAmbienteRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<TipoAmbienteBO> GetBy(Expression<Func<TTipoAmbiente, bool>> filter)
        {
            IEnumerable<TTipoAmbiente> listado = base.GetBy(filter);
            List<TipoAmbienteBO> listadoBO = new List<TipoAmbienteBO>();
            foreach (var itemEntidad in listado)
            {
                TipoAmbienteBO objetoBO = Mapper.Map<TTipoAmbiente, TipoAmbienteBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public TipoAmbienteBO FirstById(int id)
        {
            try
            {
                TTipoAmbiente entidad = base.FirstById(id);
                TipoAmbienteBO objetoBO = new TipoAmbienteBO();
                Mapper.Map<TTipoAmbiente, TipoAmbienteBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public TipoAmbienteBO FirstBy(Expression<Func<TTipoAmbiente, bool>> filter)
        {
            try
            {
                TTipoAmbiente entidad = base.FirstBy(filter);
                TipoAmbienteBO objetoBO = Mapper.Map<TTipoAmbiente, TipoAmbienteBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(TipoAmbienteBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TTipoAmbiente entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<TipoAmbienteBO> listadoBO)
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

        public bool Update(TipoAmbienteBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TTipoAmbiente entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<TipoAmbienteBO> listadoBO)
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
        private void AsignacionId(TTipoAmbiente entidad, TipoAmbienteBO objetoBO)
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

        private TTipoAmbiente MapeoEntidad(TipoAmbienteBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TTipoAmbiente entidad = new TTipoAmbiente();
                entidad = Mapper.Map<TipoAmbienteBO, TTipoAmbiente>(objetoBO,
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
        /// Obtiene todos los registros 
        /// </summary>
        /// <returns></returns>
        public List<TipoAmbienteDTO> ObtenerTodoGrid()
        {
            try
            {
                var listaTipoAmbiente = GetBy(x => true, y => new TipoAmbienteDTO
                {
                    Id = y.Id,
                    Nombre = y.Nombre,
                    Descripcion = y.Descripcion,
                }).OrderByDescending(x => x.Id).ToList();

                return listaTipoAmbiente;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Obtiene todas los ambientes para combobox
        /// </summary>
        /// <returns></returns>
        public List<TipoAmbienteFiltroDTO> ObtenerAmbienteFiltro()
        {
            try
            {
                var listaAmbiente = GetBy(x => true, y => new TipoAmbienteFiltroDTO
                {
                    Id = y.Id,
                    Nombre = y.Nombre

                }).ToList();

                return listaAmbiente;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }

    }
     
}
