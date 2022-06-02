
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
    /// Repositorio: TipoCategoriaOrigenRepositorio
    /// Autor: Edgar S.
    /// Fecha: 08/02/2021
    /// <summary>
    /// Gestión de Tipo de Categoría de Origen
    /// </summary>
    public class TipoCategoriaOrigenRepositorio : BaseRepository<TTipoCategoriaOrigen, TipoCategoriaOrigenBO>
    {
        #region Metodos Base
        public TipoCategoriaOrigenRepositorio() : base()
        {
        }
        public TipoCategoriaOrigenRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<TipoCategoriaOrigenBO> GetBy(Expression<Func<TTipoCategoriaOrigen, bool>> filter)
        {
            IEnumerable<TTipoCategoriaOrigen> listado = base.GetBy(filter);
            List<TipoCategoriaOrigenBO> listadoBO = new List<TipoCategoriaOrigenBO>();
            foreach (var itemEntidad in listado)
            {
                TipoCategoriaOrigenBO objetoBO = Mapper.Map<TTipoCategoriaOrigen, TipoCategoriaOrigenBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public TipoCategoriaOrigenBO FirstById(int id)
        {
            try
            {
                TTipoCategoriaOrigen entidad = base.FirstById(id);
                TipoCategoriaOrigenBO objetoBO = new TipoCategoriaOrigenBO();
                Mapper.Map<TTipoCategoriaOrigen, TipoCategoriaOrigenBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public TipoCategoriaOrigenBO FirstBy(Expression<Func<TTipoCategoriaOrigen, bool>> filter)
        {
            try
            {
                TTipoCategoriaOrigen entidad = base.FirstBy(filter);
                TipoCategoriaOrigenBO objetoBO = Mapper.Map<TTipoCategoriaOrigen, TipoCategoriaOrigenBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(TipoCategoriaOrigenBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TTipoCategoriaOrigen entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<TipoCategoriaOrigenBO> listadoBO)
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

        public bool Update(TipoCategoriaOrigenBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TTipoCategoriaOrigen entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<TipoCategoriaOrigenBO> listadoBO)
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
        private void AsignacionId(TTipoCategoriaOrigen entidad, TipoCategoriaOrigenBO objetoBO)
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

        private TTipoCategoriaOrigen MapeoEntidad(TipoCategoriaOrigenBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TTipoCategoriaOrigen entidad = new TTipoCategoriaOrigen();
                entidad = Mapper.Map<TipoCategoriaOrigenBO, TTipoCategoriaOrigen>(objetoBO,
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
        /// Obtiene el tipo categoria origen para filtro 
        /// </summary>
        /// <returns>id, nombre</returns>
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

        /// <summary>
        /// Obtiene toda la lista de TipoCategoriaOrigen 
        /// </summary>
        /// <returns> id, nombre, descripcion, meta</returns>
        public List<TipoCategoriaOrigenListaDTO> ObtenerListaTodoTipoCategoriaOrigen()
        {
            try
            {
                List<TipoCategoriaOrigenListaDTO> tipoCategoriaOrigenLista = new List<TipoCategoriaOrigenListaDTO>();
                var tipoCategoriaOrigenDB = GetBy(x => x.Estado == true, x => new { x.Id, x.Nombre, x.Descripcion, x.Meta });
                foreach (var item in tipoCategoriaOrigenDB)
                {
                    var tipoCategoriaOrigenFiltroTemp = new TipoCategoriaOrigenListaDTO()
                    {
                        Id = item.Id,
                        Nombre = item.Nombre,
                        Descripcion = item.Descripcion,
                        Meta = item.Meta
                    };
                    tipoCategoriaOrigenLista.Add(tipoCategoriaOrigenFiltroTemp);
                }
                return tipoCategoriaOrigenLista;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
