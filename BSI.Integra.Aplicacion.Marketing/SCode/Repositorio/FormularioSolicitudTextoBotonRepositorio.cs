using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using AutoMapper;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Marketing.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;

namespace BSI.Integra.Aplicacion.Marketing.Repositorio
{
    public class FormularioSolicitudTextoBotonRepositorio : BaseRepository<TFormularioSolicitudTextoBoton, FormularioSolicitudTextoBotonBO>
    {
        #region Metodos Base
        public FormularioSolicitudTextoBotonRepositorio() : base()
        {
        }
        public FormularioSolicitudTextoBotonRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<FormularioSolicitudTextoBotonBO> GetBy(Expression<Func<TFormularioSolicitudTextoBoton, bool>> filter)
        {
            IEnumerable<TFormularioSolicitudTextoBoton> listado = base.GetBy(filter);
            List<FormularioSolicitudTextoBotonBO> listadoBO = new List<FormularioSolicitudTextoBotonBO>();
            foreach (var itemEntidad in listado)
            {
                FormularioSolicitudTextoBotonBO objetoBO = Mapper.Map<TFormularioSolicitudTextoBoton, FormularioSolicitudTextoBotonBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public FormularioSolicitudTextoBotonBO FirstById(int id)
        {
            try
            {
                TFormularioSolicitudTextoBoton entidad = base.FirstById(id);
                FormularioSolicitudTextoBotonBO objetoBO = new FormularioSolicitudTextoBotonBO();
                Mapper.Map<TFormularioSolicitudTextoBoton, FormularioSolicitudTextoBotonBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public FormularioSolicitudTextoBotonBO FirstBy(Expression<Func<TFormularioSolicitudTextoBoton, bool>> filter)
        {
            try
            {
                TFormularioSolicitudTextoBoton entidad = base.FirstBy(filter);
                FormularioSolicitudTextoBotonBO objetoBO = Mapper.Map<TFormularioSolicitudTextoBoton, FormularioSolicitudTextoBotonBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(FormularioSolicitudTextoBotonBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TFormularioSolicitudTextoBoton entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<FormularioSolicitudTextoBotonBO> listadoBO)
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

        public bool Update(FormularioSolicitudTextoBotonBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TFormularioSolicitudTextoBoton entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<FormularioSolicitudTextoBotonBO> listadoBO)
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
        private void AsignacionId(TFormularioSolicitudTextoBoton entidad, FormularioSolicitudTextoBotonBO objetoBO)
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

        private TFormularioSolicitudTextoBoton MapeoEntidad(FormularioSolicitudTextoBotonBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TFormularioSolicitudTextoBoton entidad = new TFormularioSolicitudTextoBoton();
                entidad = Mapper.Map<FormularioSolicitudTextoBotonBO, TFormularioSolicitudTextoBoton>(objetoBO,
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
        /// Obtiene el Id, Nombre de los textos de botones(activos) registrado en el sistema
        /// </summary>
        /// <returns></returns>
        public List<FormularioSolicitudTextoBotonFiltroDTO> ObtenerTextoBotonFiltro()
        {
            try
            {
                var lista = GetBy(x => x.Estado == true, y => new FormularioSolicitudTextoBotonFiltroDTO
                {
                    Id = y.Id,
                    Nombre = y.TextoBoton
                }).ToList();

                return lista;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }

        /// <summary>
        /// Obtiene todos los registros para mostrar en la grilla.
        /// </summary>
        /// <returns></returns>
        public List<FormularioSolicitudTextoBotonGridDTO> ObtenerRegistrosGrid()
        {
            try
            {
                List<FormularioSolicitudTextoBotonGridDTO> lista = new List<FormularioSolicitudTextoBotonGridDTO>();
                lista = GetBy(x => true, y => new FormularioSolicitudTextoBotonGridDTO
                {
                    Id = y.Id,
                    TextoBoton = y.TextoBoton,
                    Descripcion = y.Descripcion,
                    PorDefecto = y.PorDefecto,
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
