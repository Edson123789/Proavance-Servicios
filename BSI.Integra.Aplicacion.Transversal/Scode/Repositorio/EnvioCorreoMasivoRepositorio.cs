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
    public class EnvioCorreoMasivoRepositorio:BaseRepository<TEnvioCorreoMasivo, EnvioCorreoMasivoBO>
    {
        #region Metodos Base
        public EnvioCorreoMasivoRepositorio() : base()
        {
        }
        public EnvioCorreoMasivoRepositorio(integraDBContext contexto) : base(contexto)
        {
        }

        public IEnumerable<EnvioCorreoMasivoBO> GetBy(Expression<Func<TEnvioCorreoMasivo, bool>> filter)
        {
            IEnumerable<TEnvioCorreoMasivo> listado = base.GetBy(filter).ToList();
            List<EnvioCorreoMasivoBO> listadoBO = new List<EnvioCorreoMasivoBO>();
            foreach (var itemEntidad in listado)
            {
                EnvioCorreoMasivoBO objetoBO = Mapper.Map<TEnvioCorreoMasivo, EnvioCorreoMasivoBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public EnvioCorreoMasivoBO FirstById(int id)
        {
            try
            {
                TEnvioCorreoMasivo entidad = base.FirstById(id);
                EnvioCorreoMasivoBO objetoBO = new EnvioCorreoMasivoBO();
                Mapper.Map<TEnvioCorreoMasivo, EnvioCorreoMasivoBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public EnvioCorreoMasivoBO FirstBy(Expression<Func<TEnvioCorreoMasivo, bool>> filter)
        {
            try
            {
                TEnvioCorreoMasivo entidad = base.FirstBy(filter);
                EnvioCorreoMasivoBO objetoBO = Mapper.Map<TEnvioCorreoMasivo, EnvioCorreoMasivoBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(EnvioCorreoMasivoBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TEnvioCorreoMasivo entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<EnvioCorreoMasivoBO> listadoBO)
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

        public bool Update(EnvioCorreoMasivoBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TEnvioCorreoMasivo entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<EnvioCorreoMasivoBO> listadoBO)
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
        private void AsignacionId(TEnvioCorreoMasivo entidad, EnvioCorreoMasivoBO objetoBO)
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

        private TEnvioCorreoMasivo MapeoEntidad(EnvioCorreoMasivoBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TEnvioCorreoMasivo entidad = new TEnvioCorreoMasivo();
                entidad = Mapper.Map<EnvioCorreoMasivoBO, TEnvioCorreoMasivo>(objetoBO,
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
