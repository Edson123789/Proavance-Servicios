
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using AutoMapper;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using Newtonsoft.Json;

namespace BSI.Integra.Aplicacion.Transversal.Repositorio
{
    public class TiempoFrecuenciaRepositorio : BaseRepository<TTiempoFrecuencia, TiempoFrecuenciaBO>
    {
        #region Metodos Base
        public TiempoFrecuenciaRepositorio() : base()
        {
        }
        public TiempoFrecuenciaRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<TiempoFrecuenciaBO> GetBy(Expression<Func<TTiempoFrecuencia, bool>> filter)
        {
            IEnumerable<TTiempoFrecuencia> listado = base.GetBy(filter);
            List<TiempoFrecuenciaBO> listadoBO = new List<TiempoFrecuenciaBO>();
            foreach (var itemEntidad in listado)
            {
                TiempoFrecuenciaBO objetoBO = Mapper.Map<TTiempoFrecuencia, TiempoFrecuenciaBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public TiempoFrecuenciaBO FirstById(int id)
        {
            try
            {
                TTiempoFrecuencia entidad = base.FirstById(id);
                TiempoFrecuenciaBO objetoBO = new TiempoFrecuenciaBO();
                Mapper.Map<TTiempoFrecuencia, TiempoFrecuenciaBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public TiempoFrecuenciaBO FirstBy(Expression<Func<TTiempoFrecuencia, bool>> filter)
        {
            try
            {
                TTiempoFrecuencia entidad = base.FirstBy(filter);
                TiempoFrecuenciaBO objetoBO = Mapper.Map<TTiempoFrecuencia, TiempoFrecuenciaBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(TiempoFrecuenciaBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TTiempoFrecuencia entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<TiempoFrecuenciaBO> listadoBO)
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

        public bool Update(TiempoFrecuenciaBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TTiempoFrecuencia entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<TiempoFrecuenciaBO> listadoBO)
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
        private void AsignacionId(TTiempoFrecuencia entidad, TiempoFrecuenciaBO objetoBO)
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

        private TTiempoFrecuencia MapeoEntidad(TiempoFrecuenciaBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TTiempoFrecuencia entidad = new TTiempoFrecuencia();
                entidad = Mapper.Map<TiempoFrecuenciaBO, TTiempoFrecuencia>(objetoBO,
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
        /// Obtiene toda la lista de TiempoFrecuencia  para ser usado en combobox
        /// </summary>
        /// <returns> id, nombre, descripcion, meta</returns>
        public List<TiempoFrecuenciaDTO> ObtenerListaTiempoFrecuencia()
        {
            try
            {
                List<TiempoFrecuenciaDTO> tiempoFrecuencia = new List<TiempoFrecuenciaDTO>();
                tiempoFrecuencia = GetBy(x => x.Estado == true, x => new TiempoFrecuenciaDTO
                {
                    Id = x.Id,
                    Nombre = x.Nombre
                }).ToList();
                //foreach (var item in TiempoFrecuenciaDB)
                //{
                //    var TiempoFrecuenciaTemp = new TiempoFrecuenciaDTO()
                //    {
                //        Id = item.Id,
                //        Nombre = item.Nombre
                        
                //    };
                //    TiempoFrecuencia.Add(TiempoFrecuenciaTemp);
                //}
                return tiempoFrecuencia;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
