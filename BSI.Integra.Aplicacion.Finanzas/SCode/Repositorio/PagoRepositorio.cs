using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using BSI.Integra.Aplicacion.Finanzas.BO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq.Expressions;
using AutoMapper;

namespace BSI.Integra.Aplicacion.Finanzas.Repositorio
{
    public class PagoRepositorio:BaseRepository<TPago, PagoBO>
    {
        #region Metodos Base

        public PagoRepositorio() : base()
        {
        }
        public PagoRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<PagoBO> GetBy(Expression<Func<TPago, bool>> filter)
        {
            IEnumerable<TPago> listado = base.GetBy(filter);
            List<PagoBO> listadoBO = new List<PagoBO>();
            foreach (var itemEntidad in listado)
            {
                PagoBO objetoBO = Mapper.Map<TPago, PagoBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public PagoBO FirstById(int id)
        {
            try
            {
                TPago entidad = base.FirstById(id);
                PagoBO objetoBO = new PagoBO();
                Mapper.Map<TPago, PagoBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public PagoBO FirstBy(Expression<Func<TPago, bool>> filter)
        {
            try
            {
                TPago entidad = base.FirstBy(filter);
                PagoBO objetoBO = Mapper.Map<TPago, PagoBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(PagoBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TPago entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<PagoBO> listadoBO)
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

        public bool Update(PagoBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TPago entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<PagoBO> listadoBO)
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
        private void AsignacionId(TPago entidad, PagoBO objetoBO)
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

        private TPago MapeoEntidad(PagoBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TPago entidad = new TPago();
                entidad = Mapper.Map<PagoBO, TPago>(objetoBO,
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
