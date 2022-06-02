using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using AutoMapper;
using BSI.Integra.Aplicacion.Marketing.BO;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using Newtonsoft.Json;
using System.Linq;
namespace BSI.Integra.Aplicacion.Marketing.Repositorio
{
    public class ListaPlantillaRepositorio : BaseRepository<TListaPlantilla, ListaPlantillaBO>
    {
        #region Metodos Base
        public ListaPlantillaRepositorio() : base()
        {
        }
        public ListaPlantillaRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<ListaPlantillaBO> GetBy(Expression<Func<TListaPlantilla, bool>> filter)
        {
            IEnumerable<TListaPlantilla> listado = base.GetBy(filter);
            List<ListaPlantillaBO> listadoBO = new List<ListaPlantillaBO>();
            foreach (var itemEntidad in listado)
            {
                ListaPlantillaBO objetoBO = Mapper.Map<TListaPlantilla, ListaPlantillaBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public ListaPlantillaBO FirstById(int id)
        {
            try
            {
                TListaPlantilla entidad = base.FirstById(id);
                ListaPlantillaBO objetoBO = new ListaPlantillaBO();
                Mapper.Map<TListaPlantilla, ListaPlantillaBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public ListaPlantillaBO FirstBy(Expression<Func<TListaPlantilla, bool>> filter)
        {
            try
            {
                TListaPlantilla entidad = base.FirstBy(filter);
                ListaPlantillaBO objetoBO = Mapper.Map<TListaPlantilla, ListaPlantillaBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(ListaPlantillaBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TListaPlantilla entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<ListaPlantillaBO> listadoBO)
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

        public bool Update(ListaPlantillaBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TListaPlantilla entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<ListaPlantillaBO> listadoBO)
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
        private void AsignacionId(TListaPlantilla entidad, ListaPlantillaBO objetoBO)
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

        private TListaPlantilla MapeoEntidad(ListaPlantillaBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TListaPlantilla entidad = new TListaPlantilla();
                entidad = Mapper.Map<ListaPlantillaBO, TListaPlantilla>(objetoBO,
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
        /// Obtiene toda la lista de todas las 'ListaPlantilla' para la interface de usuario
        /// </summary>
        /// <returns> id, nombre, descripcion, meta</returns>
        public List<ListaPlantillaDTO> ObtenerTodasListaPlantilla()
        {
            try
            {
                List<ListaPlantillaDTO> lista = new List<ListaPlantillaDTO>();
                var ListaPlantillaDB = GetBy(x => x.Estado == true, x => new { x.Id, x.Nombre, x.Disenho});
                foreach (var item in ListaPlantillaDB)
                {
                    var TodoListaPlantillaTemp = new ListaPlantillaDTO()
                    {
                        Id = item.Id,
                        Nombre = item.Nombre,
                        Disenho = item.Disenho
                    };
                    lista.Add(TodoListaPlantillaTemp);
                }
                return lista;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Obtiene todos los registros con los campos Id y Nombre
        /// </summary>
        /// <returns></returns>
        public List<FiltroIdNombreDTO> ObtenerIdNombre()
        {
            try
            {
                return GetBy(x => true, y => new FiltroIdNombreDTO
                {
                    Id = y.Id,
                    Nombre = y.Nombre
                }
            ).ToList();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            
        }
    }
}
