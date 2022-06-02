using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using AutoMapper;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using BSI.Integra.Aplicacion.DTOs;
using Newtonsoft.Json;
using System.Linq;
using BSI.Integra.Aplicacion.Transversal.Helper;
using BSI.Integra.Aplicacion.DTOs.Scode.DTOs.Transversal;
namespace BSI.Integra.Aplicacion.Transversal.Repositorio
{
    public class EnvioCorreoBicOperacionesRepositorio : BaseRepository<TEnvioCorreoBicOperaciones, EnvioCorreoBicOperacionesBO>
    {
        #region Metodos Base
        public EnvioCorreoBicOperacionesRepositorio() : base()
        {
        }
        public EnvioCorreoBicOperacionesRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<EnvioCorreoBicOperacionesBO> GetBy(Expression<Func<TEnvioCorreoBicOperaciones, bool>> filter)
        {
            IEnumerable<TEnvioCorreoBicOperaciones> listado = base.GetBy(filter);
            List<EnvioCorreoBicOperacionesBO> listadoBO = new List<EnvioCorreoBicOperacionesBO>();
            foreach (var itemEntidad in listado)
            {
                EnvioCorreoBicOperacionesBO objetoBO = Mapper.Map<TEnvioCorreoBicOperaciones, EnvioCorreoBicOperacionesBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public EnvioCorreoBicOperacionesBO FirstById(int id)
        {
            try
            {
                TEnvioCorreoBicOperaciones entidad = base.FirstById(id);
                EnvioCorreoBicOperacionesBO objetoBO = new EnvioCorreoBicOperacionesBO();
                Mapper.Map<TEnvioCorreoBicOperaciones, EnvioCorreoBicOperacionesBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public EnvioCorreoBicOperacionesBO FirstBy(Expression<Func<TEnvioCorreoBicOperaciones, bool>> filter)
        {
            try
            {
                TEnvioCorreoBicOperaciones entidad = base.FirstBy(filter);
                EnvioCorreoBicOperacionesBO objetoBO = Mapper.Map<TEnvioCorreoBicOperaciones, EnvioCorreoBicOperacionesBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public bool Insert(EnvioCorreoBicOperacionesBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TEnvioCorreoBicOperaciones entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<EnvioCorreoBicOperacionesBO> listadoBO)
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

        public bool Update(EnvioCorreoBicOperacionesBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TEnvioCorreoBicOperaciones entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<EnvioCorreoBicOperacionesBO> listadoBO)
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
        private void AsignacionId(TEnvioCorreoBicOperaciones entidad, EnvioCorreoBicOperacionesBO objetoBO)
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

        private TEnvioCorreoBicOperaciones MapeoEntidad(EnvioCorreoBicOperacionesBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TEnvioCorreoBicOperaciones entidad = new TEnvioCorreoBicOperaciones();
                entidad = Mapper.Map<EnvioCorreoBicOperacionesBO, TEnvioCorreoBicOperaciones>(objetoBO,
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
