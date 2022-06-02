using AutoMapper;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace BSI.Integra.Aplicacion.Transversal.Repositorio
{
    public class EnvioCorreoMandrilRepositorio : BaseRepository<TEnvioCorreoMandril, EnvioCorreoMandrilBO>
    {
        #region Metodos Base
        public EnvioCorreoMandrilRepositorio() : base()
        {
        }
        public EnvioCorreoMandrilRepositorio(integraDBContext contexto) : base(contexto)
        {
        }

        public IEnumerable<EnvioCorreoMandrilBO> GetBy(Expression<Func<TEnvioCorreoMandril, bool>> filter)
        {
            IEnumerable<TEnvioCorreoMandril> listado = base.GetBy(filter).ToList();
            List<EnvioCorreoMandrilBO> listadoBO = new List<EnvioCorreoMandrilBO>();
            foreach (var itemEntidad in listado)
            {
                EnvioCorreoMandrilBO objetoBO = Mapper.Map<TEnvioCorreoMandril, EnvioCorreoMandrilBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public EnvioCorreoMandrilBO FirstById(int id)
        {
            try
            {
                TEnvioCorreoMandril entidad = base.FirstById(id);
                EnvioCorreoMandrilBO objetoBO = new EnvioCorreoMandrilBO();
                Mapper.Map<TEnvioCorreoMandril, EnvioCorreoMandrilBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public EnvioCorreoMandrilBO FirstBy(Expression<Func<TEnvioCorreoMandril, bool>> filter)
        {
            try
            {
                TEnvioCorreoMandril entidad = base.FirstBy(filter);
                EnvioCorreoMandrilBO objetoBO = Mapper.Map<TEnvioCorreoMandril, EnvioCorreoMandrilBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(EnvioCorreoMandrilBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TEnvioCorreoMandril entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<EnvioCorreoMandrilBO> listadoBO)
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

        public bool Update(EnvioCorreoMandrilBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TEnvioCorreoMandril entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<EnvioCorreoMandrilBO> listadoBO)
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
        private void AsignacionId(TEnvioCorreoMandril entidad, EnvioCorreoMandrilBO objetoBO)
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

        private TEnvioCorreoMandril MapeoEntidad(EnvioCorreoMandrilBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TEnvioCorreoMandril entidad = new TEnvioCorreoMandril();
                entidad = Mapper.Map<EnvioCorreoMandrilBO, TEnvioCorreoMandril>(objetoBO,
                    opt => opt.ConfigureMap(MemberList.None));

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
