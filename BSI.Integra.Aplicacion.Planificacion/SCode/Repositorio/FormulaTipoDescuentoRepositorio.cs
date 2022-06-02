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
    public class FormulaTipoDescuentoRepositorio : BaseRepository<TFormulaTipoDescuento, FormulaTipoDescuentoBO>
    {
        #region Metodos Base
        public FormulaTipoDescuentoRepositorio() : base()
        {
        }
        public FormulaTipoDescuentoRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<FormulaTipoDescuentoBO> GetBy(Expression<Func<TFormulaTipoDescuento, bool>> filter)
        {
            IEnumerable<TFormulaTipoDescuento> listado = base.GetBy(filter);
            List<FormulaTipoDescuentoBO> listadoBO = new List<FormulaTipoDescuentoBO>();
            foreach (var itemEntidad in listado)
            {
                FormulaTipoDescuentoBO objetoBO = Mapper.Map<TFormulaTipoDescuento, FormulaTipoDescuentoBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public FormulaTipoDescuentoBO FirstById(int id)
        {
            try
            {
                TFormulaTipoDescuento entidad = base.FirstById(id);
                FormulaTipoDescuentoBO objetoBO = new FormulaTipoDescuentoBO();
                Mapper.Map<TFormulaTipoDescuento, FormulaTipoDescuentoBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public FormulaTipoDescuentoBO FirstBy(Expression<Func<TFormulaTipoDescuento, bool>> filter)
        {
            try
            {
                TFormulaTipoDescuento entidad = base.FirstBy(filter);
                FormulaTipoDescuentoBO objetoBO = Mapper.Map<TFormulaTipoDescuento, FormulaTipoDescuentoBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(FormulaTipoDescuentoBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TFormulaTipoDescuento entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<FormulaTipoDescuentoBO> listadoBO)
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

        public bool Update(FormulaTipoDescuentoBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TFormulaTipoDescuento entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<FormulaTipoDescuentoBO> listadoBO)
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
        private void AsignacionId(TFormulaTipoDescuento entidad, FormulaTipoDescuentoBO objetoBO)
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

        private TFormulaTipoDescuento MapeoEntidad(FormulaTipoDescuentoBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TFormulaTipoDescuento entidad = new TFormulaTipoDescuento();
                entidad = Mapper.Map<FormulaTipoDescuentoBO, TFormulaTipoDescuento>(objetoBO,
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
        public List<FormulaTipoDescuentoDTO> ObtenerTodoGrid()
        {
            try
            {
                var lista = GetBy(x => true, y => new FormulaTipoDescuentoDTO
                {
                    Id = y.Id,
                    Nombre = y.Nombre,
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
