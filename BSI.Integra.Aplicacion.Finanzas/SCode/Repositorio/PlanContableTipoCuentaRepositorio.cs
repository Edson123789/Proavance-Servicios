using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using AutoMapper;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Finanzas.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using Newtonsoft.Json;


namespace BSI.Integra.Aplicacion.Finanzas.Repositorio
{
    public class PlanContableTipoCuentaRepositorio : BaseRepository<TPlanContableTipoCuenta, PlanContableTipoCuentaBO>
    {
        #region Metodos Base
        public PlanContableTipoCuentaRepositorio() : base()
        {
        }
        public PlanContableTipoCuentaRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<PlanContableTipoCuentaBO> GetBy(Expression<Func<TPlanContableTipoCuenta, bool>> filter)
        {
            IEnumerable<TPlanContableTipoCuenta> listado = base.GetBy(filter);
            List<PlanContableTipoCuentaBO> listadoBO = new List<PlanContableTipoCuentaBO>();
            foreach (var itemEntidad in listado)
            {
                PlanContableTipoCuentaBO objetoBO = Mapper.Map<TPlanContableTipoCuenta, PlanContableTipoCuentaBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public PlanContableTipoCuentaBO FirstById(int id)
        {
            try
            {
                TPlanContableTipoCuenta entidad = base.FirstById(id);
                PlanContableTipoCuentaBO objetoBO = new PlanContableTipoCuentaBO();
                Mapper.Map<TPlanContableTipoCuenta, PlanContableTipoCuentaBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public PlanContableTipoCuentaBO FirstBy(Expression<Func<TPlanContableTipoCuenta, bool>> filter)
        {
            try
            {
                TPlanContableTipoCuenta entidad = base.FirstBy(filter);
                PlanContableTipoCuentaBO objetoBO = Mapper.Map<TPlanContableTipoCuenta, PlanContableTipoCuentaBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(PlanContableTipoCuentaBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TPlanContableTipoCuenta entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<PlanContableTipoCuentaBO> listadoBO)
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

        public bool Update(PlanContableTipoCuentaBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TPlanContableTipoCuenta entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<PlanContableTipoCuentaBO> listadoBO)
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
        private void AsignacionId(TPlanContableTipoCuenta entidad, PlanContableTipoCuentaBO objetoBO)
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

        private TPlanContableTipoCuenta MapeoEntidad(PlanContableTipoCuentaBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TPlanContableTipoCuenta entidad = new TPlanContableTipoCuenta();
                entidad = Mapper.Map<PlanContableTipoCuentaBO, TPlanContableTipoCuenta>(objetoBO,
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
