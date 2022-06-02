using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using AutoMapper;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;

namespace BSI.Integra.Aplicacion.Transversal.Repositorio
{
    public class SolucionClienteByActividadRepositorio : BaseRepository<TSolucionClienteByActividad, SolucionClienteByActividadBO>
    {
        #region Metodos Base
        public SolucionClienteByActividadRepositorio() : base()
        {
        }
        public SolucionClienteByActividadRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<SolucionClienteByActividadBO> GetBy(Expression<Func<TSolucionClienteByActividad, bool>> filter)
        {
            IEnumerable<TSolucionClienteByActividad> listado = base.GetBy(filter).ToList();
            List<SolucionClienteByActividadBO> listadoBO = new List<SolucionClienteByActividadBO>();
            foreach (var itemEntidad in listado)
            {
                SolucionClienteByActividadBO objetoBO = Mapper.Map<TSolucionClienteByActividad, SolucionClienteByActividadBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public SolucionClienteByActividadBO FirstById(int id)
        {
            try
            {
                TSolucionClienteByActividad entidad = base.FirstById(id);
                SolucionClienteByActividadBO objetoBO = new SolucionClienteByActividadBO();
                Mapper.Map<TSolucionClienteByActividad, SolucionClienteByActividadBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public SolucionClienteByActividadBO FirstBy(Expression<Func<TSolucionClienteByActividad, bool>> filter)
        {
            try
            {
                TSolucionClienteByActividad entidad = base.FirstBy(filter);
                SolucionClienteByActividadBO objetoBO = Mapper.Map<TSolucionClienteByActividad, SolucionClienteByActividadBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(SolucionClienteByActividadBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TSolucionClienteByActividad entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<SolucionClienteByActividadBO> listadoBO)
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

        public bool Update(SolucionClienteByActividadBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TSolucionClienteByActividad entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<SolucionClienteByActividadBO> listadoBO)
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
        private void AsignacionId(TSolucionClienteByActividad entidad, SolucionClienteByActividadBO objetoBO)
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

        private TSolucionClienteByActividad MapeoEntidad(SolucionClienteByActividadBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TSolucionClienteByActividad entidad = new TSolucionClienteByActividad();
                entidad = Mapper.Map<SolucionClienteByActividadBO, TSolucionClienteByActividad>(objetoBO,
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
