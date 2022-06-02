using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using AutoMapper;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Planificacion.BO;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using Newtonsoft.Json;

namespace BSI.Integra.Aplicacion.Transversal.Repositorio
{
    public class ObservacionesCursosFinalizadosLogRepositorio : BaseRepository<TObservacionesCursosFinalizadosLog, ObservacionesCursosFinalizadosLogBO>
    {
        #region Metodos Base
        public ObservacionesCursosFinalizadosLogRepositorio() : base()
        {
        }
        public ObservacionesCursosFinalizadosLogRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<ObservacionesCursosFinalizadosLogBO> GetBy(Expression<Func<TObservacionesCursosFinalizadosLog, bool>> filter)
        {
            IEnumerable<TObservacionesCursosFinalizadosLog> listado = base.GetBy(filter);
            List<ObservacionesCursosFinalizadosLogBO> listadoBO = new List<ObservacionesCursosFinalizadosLogBO>();
            foreach (var itemEntidad in listado)
            {
                ObservacionesCursosFinalizadosLogBO objetoBO = Mapper.Map<TObservacionesCursosFinalizadosLog, ObservacionesCursosFinalizadosLogBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public ObservacionesCursosFinalizadosLogBO FirstById(int id)
        {
            try
            {
                TObservacionesCursosFinalizadosLog entidad = base.FirstById(id);
                ObservacionesCursosFinalizadosLogBO objetoBO = new ObservacionesCursosFinalizadosLogBO();
                Mapper.Map<TObservacionesCursosFinalizadosLog, ObservacionesCursosFinalizadosLogBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public ObservacionesCursosFinalizadosLogBO FirstBy(Expression<Func<TObservacionesCursosFinalizadosLog, bool>> filter)
        {
            try
            {
                TObservacionesCursosFinalizadosLog entidad = base.FirstBy(filter);
                ObservacionesCursosFinalizadosLogBO objetoBO = Mapper.Map<TObservacionesCursosFinalizadosLog, ObservacionesCursosFinalizadosLogBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(ObservacionesCursosFinalizadosLogBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TObservacionesCursosFinalizadosLog entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<ObservacionesCursosFinalizadosLogBO> listadoBO)
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

        public bool Update(ObservacionesCursosFinalizadosLogBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TObservacionesCursosFinalizadosLog entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<ObservacionesCursosFinalizadosLogBO> listadoBO)
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
        private void AsignacionId(TObservacionesCursosFinalizadosLog entidad, ObservacionesCursosFinalizadosLogBO objetoBO)
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

        private TObservacionesCursosFinalizadosLog MapeoEntidad(ObservacionesCursosFinalizadosLogBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TObservacionesCursosFinalizadosLog entidad = new TObservacionesCursosFinalizadosLog();
                entidad = Mapper.Map<ObservacionesCursosFinalizadosLogBO, TObservacionesCursosFinalizadosLog>(objetoBO,
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
