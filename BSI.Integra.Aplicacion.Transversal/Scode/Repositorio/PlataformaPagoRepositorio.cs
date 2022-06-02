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
    public class PlataformaPagoRepositorio : BaseRepository<TPlataformaPago, PlataformaPagoBO>
    {
        #region Metodos Base
        public PlataformaPagoRepositorio() : base()
        {
        }
        public PlataformaPagoRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<PlataformaPagoBO> GetBy(Expression<Func<TPlataformaPago, bool>> filter)
        {
            IEnumerable<TPlataformaPago> listado = base.GetBy(filter).ToList();
            List<PlataformaPagoBO> listadoBO = new List<PlataformaPagoBO>();
            foreach (var itemEntidad in listado)
            {
                PlataformaPagoBO objetoBO = Mapper.Map<TPlataformaPago, PlataformaPagoBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public PlataformaPagoBO FirstById(int id)
        {
            try
            {
                TPlataformaPago entidad = base.FirstById(id);
                PlataformaPagoBO objetoBO = new PlataformaPagoBO();
                Mapper.Map<TPlataformaPago, PlataformaPagoBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public PlataformaPagoBO FirstBy(Expression<Func<TPlataformaPago, bool>> filter)
        {
            try
            {
                TPlataformaPago entidad = base.FirstBy(filter);
                PlataformaPagoBO objetoBO = Mapper.Map<TPlataformaPago, PlataformaPagoBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(PlataformaPagoBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TPlataformaPago entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<PlataformaPagoBO> listadoBO)
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

        public bool Update(PlataformaPagoBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TPlataformaPago entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<PlataformaPagoBO> listadoBO)
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
        private void AsignacionId(TPlataformaPago entidad, PlataformaPagoBO objetoBO)
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

        private TPlataformaPago MapeoEntidad(PlataformaPagoBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TPlataformaPago entidad = new TPlataformaPago();
                entidad = Mapper.Map<PlataformaPagoBO, TPlataformaPago>(objetoBO,
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
        /// Obtiene la plataforma de pago (activos) registrada en el sistema
        /// </summary>
        /// <returns></returns>
        public List<PlataformaPagoFiltroDTO> ObtenerPlataformasPagoFiltro()
        {
            try
            {
                var lista = GetBy(x => x.Estado == true, y => new PlataformaPagoFiltroDTO
                {
                    Id = y.Id,
                    Nombre = y.Nombre
                }).ToList();

                return lista;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }

        /// <summary>
        /// Obtiene todos los registros sin los campos de auditoría.
        /// </summary>
        /// <returns></returns>
        public List<PlataformaPagoDTO> ObtenerTodoGrid()
        {
            try
            {
                var lista = GetBy(x => true, y => new PlataformaPagoDTO
                {
                    Id = y.Id,
                    Nombre = y.Nombre,
                    Descripcion = y.Descripcion,
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
