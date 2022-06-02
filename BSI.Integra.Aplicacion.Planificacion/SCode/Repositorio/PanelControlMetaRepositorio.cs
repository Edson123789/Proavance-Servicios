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
    public class PanelControlMetaRepositorio : BaseRepository<TPanelControlMeta, PanelControlMetaBO>
    {
        #region Metodos Base
        public PanelControlMetaRepositorio() : base()
        {
        }
        public PanelControlMetaRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<PanelControlMetaBO> GetBy(Expression<Func<TPanelControlMeta, bool>> filter)
        {
            IEnumerable<TPanelControlMeta> listado = base.GetBy(filter);
            List<PanelControlMetaBO> listadoBO = new List<PanelControlMetaBO>();
            foreach (var itemEntidad in listado)
            {
                PanelControlMetaBO objetoBO = Mapper.Map<TPanelControlMeta, PanelControlMetaBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public PanelControlMetaBO FirstById(int id)
        {
            try
            {
                TPanelControlMeta entidad = base.FirstById(id);
                PanelControlMetaBO objetoBO = new PanelControlMetaBO();
                Mapper.Map<TPanelControlMeta, PanelControlMetaBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public PanelControlMetaBO FirstBy(Expression<Func<TPanelControlMeta, bool>> filter)
        {
            try
            {
                TPanelControlMeta entidad = base.FirstBy(filter);
                PanelControlMetaBO objetoBO = Mapper.Map<TPanelControlMeta, PanelControlMetaBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(PanelControlMetaBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TPanelControlMeta entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<PanelControlMetaBO> listadoBO)
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

        public bool Update(PanelControlMetaBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TPanelControlMeta entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<PanelControlMetaBO> listadoBO)
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
        private void AsignacionId(TPanelControlMeta entidad, PanelControlMetaBO objetoBO)
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

        private TPanelControlMeta MapeoEntidad(PanelControlMetaBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TPanelControlMeta entidad = new TPanelControlMeta();
                entidad = Mapper.Map<PanelControlMetaBO, TPanelControlMeta>(objetoBO,
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
        public List<PanelControlMetaDTO> ObtenerTodoGrid()
        {
            try
            {
                var lista = GetBy(x => true, y => new PanelControlMetaDTO
                {
                    Id = y.Id,
                    Nombre = y.Nombre,
                    Meta = y.Meta,
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
