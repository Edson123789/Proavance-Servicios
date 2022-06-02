using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using AutoMapper;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;

namespace BSI.Integra.Aplicacion.Transversal.Repositorio
{
    /// Repositorio: Transversal/ControlDescargaLlamadaAsteriskRepositorio
    /// Autor: Ansoli Espinoza
    /// Fecha: 26-01-2021
    /// <summary>
    /// Repositorio de la tabla T_ControlDescargaLlamadaAsterisk
    /// </summary>
    public class ControlDescargaLlamadaAsteriskRepositorio : BaseRepository<TControlDescargaLlamadaAsterisk, ControlDescargaLlamadaAsteriskBO>
    {
        #region Metodos Base
        public ControlDescargaLlamadaAsteriskRepositorio() : base()
        {
        }
        public ControlDescargaLlamadaAsteriskRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<ControlDescargaLlamadaAsteriskBO> GetBy(Expression<Func<TControlDescargaLlamadaAsterisk, bool>> filter)
        {
            IEnumerable<TControlDescargaLlamadaAsterisk> listado = base.GetBy(filter);
            List<ControlDescargaLlamadaAsteriskBO> listadoBO = new List<ControlDescargaLlamadaAsteriskBO>();
            foreach (var itemEntidad in listado)
            {
                ControlDescargaLlamadaAsteriskBO objetoBO = Mapper.Map<TControlDescargaLlamadaAsterisk, ControlDescargaLlamadaAsteriskBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public ControlDescargaLlamadaAsteriskBO FirstById(int id)
        {
            try
            {
                TControlDescargaLlamadaAsterisk entidad = base.FirstById(id);
                ControlDescargaLlamadaAsteriskBO objetoBO = new ControlDescargaLlamadaAsteriskBO();
                Mapper.Map<TControlDescargaLlamadaAsterisk, ControlDescargaLlamadaAsteriskBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public ControlDescargaLlamadaAsteriskBO FirstBy(Expression<Func<TControlDescargaLlamadaAsterisk, bool>> filter)
        {
            try
            {
                TControlDescargaLlamadaAsterisk entidad = base.FirstBy(filter);
                ControlDescargaLlamadaAsteriskBO objetoBO = Mapper.Map<TControlDescargaLlamadaAsterisk, ControlDescargaLlamadaAsteriskBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(ControlDescargaLlamadaAsteriskBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TControlDescargaLlamadaAsterisk entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<ControlDescargaLlamadaAsteriskBO> listadoBO)
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

        public bool Update(ControlDescargaLlamadaAsteriskBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TControlDescargaLlamadaAsterisk entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<ControlDescargaLlamadaAsteriskBO> listadoBO)
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
        private void AsignacionId(TControlDescargaLlamadaAsterisk entidad, ControlDescargaLlamadaAsteriskBO objetoBO)
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

        private TControlDescargaLlamadaAsterisk MapeoEntidad(ControlDescargaLlamadaAsteriskBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TControlDescargaLlamadaAsterisk entidad = new TControlDescargaLlamadaAsterisk();
                entidad = Mapper.Map<ControlDescargaLlamadaAsteriskBO, TControlDescargaLlamadaAsterisk>(objetoBO,
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
