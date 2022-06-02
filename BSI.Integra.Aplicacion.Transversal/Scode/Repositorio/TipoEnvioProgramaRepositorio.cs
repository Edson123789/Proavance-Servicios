using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using AutoMapper;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using Newtonsoft.Json;

namespace BSI.Integra.Aplicacion.Transversal.Repositorio
{
    public class TipoEnvioProgramaRepositorio :BaseRepository<TTipoEnvioPrograma, TipoEnvioProgramaBO>
    {
        #region Metodos Base
        public TipoEnvioProgramaRepositorio() : base()
        {
        }
        public TipoEnvioProgramaRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<TipoEnvioProgramaBO> GetBy(Expression<Func<TTipoEnvioPrograma, bool>> filter)
        {
            IEnumerable<TTipoEnvioPrograma> listado = base.GetBy(filter);
            List<TipoEnvioProgramaBO> listadoBO = new List<TipoEnvioProgramaBO>();
            foreach (var itemEntidad in listado)
            {
                TipoEnvioProgramaBO objetoBO = Mapper.Map<TTipoEnvioPrograma, TipoEnvioProgramaBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public TipoEnvioProgramaBO FirstById(int id)
        {
            try
            {
                TTipoEnvioPrograma entidad = base.FirstById(id);
                TipoEnvioProgramaBO objetoBO = new TipoEnvioProgramaBO();
                Mapper.Map<TTipoEnvioPrograma, TipoEnvioProgramaBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public TipoEnvioProgramaBO FirstBy(Expression<Func<TTipoEnvioPrograma, bool>> filter)
        {
            try
            {
                TTipoEnvioPrograma entidad = base.FirstBy(filter);
                TipoEnvioProgramaBO objetoBO = Mapper.Map<TTipoEnvioPrograma, TipoEnvioProgramaBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(TipoEnvioProgramaBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TTipoEnvioPrograma entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<TipoEnvioProgramaBO> listadoBO)
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

        public bool Update(TipoEnvioProgramaBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TTipoEnvioPrograma entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<TipoEnvioProgramaBO> listadoBO)
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
        private void AsignacionId(TTipoEnvioPrograma entidad, TipoEnvioProgramaBO objetoBO)
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

        private TTipoEnvioPrograma MapeoEntidad(TipoEnvioProgramaBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TTipoEnvioPrograma entidad = new TTipoEnvioPrograma();
                entidad = Mapper.Map<TipoEnvioProgramaBO, TTipoEnvioPrograma>(objetoBO,
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
