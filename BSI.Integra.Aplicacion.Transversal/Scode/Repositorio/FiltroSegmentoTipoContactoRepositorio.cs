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
    public class FiltroSegmentoTipoContactoRepositorio : BaseRepository<TFiltroSegmentoTipoContacto, FiltroSegmentoTipoContactoBO>
    {
        #region Metodos Base
        public FiltroSegmentoTipoContactoRepositorio() : base()
        {
        }
        public FiltroSegmentoTipoContactoRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<FiltroSegmentoTipoContactoBO> GetBy(Expression<Func<TFiltroSegmentoTipoContacto, bool>> filter)
        {
            IEnumerable<TFiltroSegmentoTipoContacto> listado = base.GetBy(filter);
            List<FiltroSegmentoTipoContactoBO> listadoBO = new List<FiltroSegmentoTipoContactoBO>();
            foreach (var itemEntidad in listado)
            {
                FiltroSegmentoTipoContactoBO objetoBO = Mapper.Map<TFiltroSegmentoTipoContacto, FiltroSegmentoTipoContactoBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public FiltroSegmentoTipoContactoBO FirstById(int id)
        {
            try
            {
                TFiltroSegmentoTipoContacto entidad = base.FirstById(id);
                FiltroSegmentoTipoContactoBO objetoBO = new FiltroSegmentoTipoContactoBO();
                Mapper.Map<TFiltroSegmentoTipoContacto, FiltroSegmentoTipoContactoBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public FiltroSegmentoTipoContactoBO FirstBy(Expression<Func<TFiltroSegmentoTipoContacto, bool>> filter)
        {
            try
            {
                TFiltroSegmentoTipoContacto entidad = base.FirstBy(filter);
                FiltroSegmentoTipoContactoBO objetoBO = Mapper.Map<TFiltroSegmentoTipoContacto, FiltroSegmentoTipoContactoBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(FiltroSegmentoTipoContactoBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TFiltroSegmentoTipoContacto entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<FiltroSegmentoTipoContactoBO> listadoBO)
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

        public bool Update(FiltroSegmentoTipoContactoBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TFiltroSegmentoTipoContacto entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<FiltroSegmentoTipoContactoBO> listadoBO)
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
        private void AsignacionId(TFiltroSegmentoTipoContacto entidad, FiltroSegmentoTipoContactoBO objetoBO)
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

        private TFiltroSegmentoTipoContacto MapeoEntidad(FiltroSegmentoTipoContactoBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TFiltroSegmentoTipoContacto entidad = new TFiltroSegmentoTipoContacto();
                entidad = Mapper.Map<FiltroSegmentoTipoContactoBO, TFiltroSegmentoTipoContacto>(objetoBO,
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
        /// Obtiene los filtro segmetentojunto aisted
        /// </summary>
        /// <returns></returns>
        public List<FiltroDTO> ObtenerTodoFiltro() {
            try
            {
                return this.GetBy(x => x.Estado == true, x => new FiltroDTO { Id = x.Id, Nombre = x.Nombre }).ToList();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

    }
}
