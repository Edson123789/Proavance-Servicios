using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using AutoMapper;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.GestionPersonas.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using Newtonsoft.Json;

namespace BSI.Integra.Aplicacion.GestionPersonas.Repositorio
{
    public class EstadoEtapaProcesoSeleccionRepositorio : BaseRepository<TEstadoEtapaProcesoSeleccion, EstadoEtapaProcesoSeleccionBO>
    {
        #region Metodos Base
        public EstadoEtapaProcesoSeleccionRepositorio() : base()
        {
        }
        public EstadoEtapaProcesoSeleccionRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<EstadoEtapaProcesoSeleccionBO> GetBy(Expression<Func<TEstadoEtapaProcesoSeleccion, bool>> filter)
        {
            IEnumerable<TEstadoEtapaProcesoSeleccion> listado = base.GetBy(filter);
            List<EstadoEtapaProcesoSeleccionBO> listadoBO = new List<EstadoEtapaProcesoSeleccionBO>();
            foreach (var itemEntidad in listado)
            {
                EstadoEtapaProcesoSeleccionBO objetoBO = Mapper.Map<TEstadoEtapaProcesoSeleccion, EstadoEtapaProcesoSeleccionBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public EstadoEtapaProcesoSeleccionBO FirstById(int id)
        {
            try
            {
                TEstadoEtapaProcesoSeleccion entidad = base.FirstById(id);
                EstadoEtapaProcesoSeleccionBO objetoBO = new EstadoEtapaProcesoSeleccionBO();
                Mapper.Map<TEstadoEtapaProcesoSeleccion, EstadoEtapaProcesoSeleccionBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public EstadoEtapaProcesoSeleccionBO FirstBy(Expression<Func<TEstadoEtapaProcesoSeleccion, bool>> filter)
        {
            try
            {
                TEstadoEtapaProcesoSeleccion entidad = base.FirstBy(filter);
                EstadoEtapaProcesoSeleccionBO objetoBO = Mapper.Map<TEstadoEtapaProcesoSeleccion, EstadoEtapaProcesoSeleccionBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(EstadoEtapaProcesoSeleccionBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TEstadoEtapaProcesoSeleccion entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<EstadoEtapaProcesoSeleccionBO> listadoBO)
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

        public bool Update(EstadoEtapaProcesoSeleccionBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TEstadoEtapaProcesoSeleccion entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<EstadoEtapaProcesoSeleccionBO> listadoBO)
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
        private void AsignacionId(TEstadoEtapaProcesoSeleccion entidad, EstadoEtapaProcesoSeleccionBO objetoBO)
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

        private TEstadoEtapaProcesoSeleccion MapeoEntidad(EstadoEtapaProcesoSeleccionBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TEstadoEtapaProcesoSeleccion entidad = new TEstadoEtapaProcesoSeleccion();
                entidad = Mapper.Map<EstadoEtapaProcesoSeleccionBO, TEstadoEtapaProcesoSeleccion>(objetoBO,
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
