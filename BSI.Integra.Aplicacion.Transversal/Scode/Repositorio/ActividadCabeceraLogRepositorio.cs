using AutoMapper;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace BSI.Integra.Aplicacion.Transversal.Scode.Repositorio
{
    public class ActividadCabeceraLogRepositorio : BaseRepository<TActividadCabeceraLog, ActividadCabeceraLogBO>
    {
        #region Metodos Base
        public ActividadCabeceraLogRepositorio() : base()
        {
        }
        public ActividadCabeceraLogRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<ActividadCabeceraLogBO> GetBy(Expression<Func<TActividadCabeceraLog, bool>> filter)
        {
            IEnumerable<TActividadCabeceraLog> listado = base.GetBy(filter);
            List<ActividadCabeceraLogBO> listadoBO = new List<ActividadCabeceraLogBO>();
            foreach (var itemEntidad in listado)
            {
                ActividadCabeceraLogBO objetoBO = Mapper.Map<TActividadCabeceraLog, ActividadCabeceraLogBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public ActividadCabeceraLogBO FirstById(int id)
        {
            try
            {
                TActividadCabeceraLog entidad = base.FirstById(id);
                ActividadCabeceraLogBO objetoBO = new ActividadCabeceraLogBO();
                Mapper.Map<TActividadCabeceraLog, ActividadCabeceraLogBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public ActividadCabeceraLogBO FirstBy(Expression<Func<TActividadCabeceraLog, bool>> filter)
        {
            try
            {
                TActividadCabeceraLog entidad = base.FirstBy(filter);
                ActividadCabeceraLogBO objetoBO = Mapper.Map<TActividadCabeceraLog, ActividadCabeceraLogBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(ActividadCabeceraLogBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TActividadCabeceraLog entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<ActividadCabeceraLogBO> listadoBO)
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

        public bool Update(ActividadCabeceraLogBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TActividadCabeceraLog entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<ActividadCabeceraLogBO> listadoBO)
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
        private void AsignacionId(TActividadCabeceraLog entidad, ActividadCabeceraLogBO objetoBO)
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

        private TActividadCabeceraLog MapeoEntidad(ActividadCabeceraLogBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TActividadCabeceraLog entidad = new TActividadCabeceraLog();
                entidad = Mapper.Map<ActividadCabeceraLogBO, TActividadCabeceraLog>(objetoBO,
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
