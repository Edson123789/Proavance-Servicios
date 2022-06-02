using System;
using System.Collections.Generic;
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
    public class LlamadaWebphoneEstadoRepositorio : BaseRepository<TLlamadaWebphoneEstado, LlamadaWebphoneEstadoBO>
    {
        #region Metodos Base
        public LlamadaWebphoneEstadoRepositorio() : base()
        {
        }
        public LlamadaWebphoneEstadoRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<LlamadaWebphoneEstadoBO> GetBy(Expression<Func<TLlamadaWebphoneEstado, bool>> filter)
        {
            IEnumerable<TLlamadaWebphoneEstado> listado = base.GetBy(filter);
            List<LlamadaWebphoneEstadoBO> listadoBO = new List<LlamadaWebphoneEstadoBO>();
            foreach (var itemEntidad in listado)
            {
                LlamadaWebphoneEstadoBO objetoBO = Mapper.Map<TLlamadaWebphoneEstado, LlamadaWebphoneEstadoBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public LlamadaWebphoneEstadoBO FirstById(int id)
        {
            try
            {
                TLlamadaWebphoneEstado entidad = base.FirstById(id);
                LlamadaWebphoneEstadoBO objetoBO = new LlamadaWebphoneEstadoBO();
                Mapper.Map<TLlamadaWebphoneEstado, LlamadaWebphoneEstadoBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public LlamadaWebphoneEstadoBO FirstBy(Expression<Func<TLlamadaWebphoneEstado, bool>> filter)
        {
            try
            {
                TLlamadaWebphoneEstado entidad = base.FirstBy(filter);
                LlamadaWebphoneEstadoBO objetoBO = Mapper.Map<TLlamadaWebphoneEstado, LlamadaWebphoneEstadoBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(LlamadaWebphoneEstadoBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TLlamadaWebphoneEstado entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<LlamadaWebphoneEstadoBO> listadoBO)
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

        public bool Update(LlamadaWebphoneEstadoBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TLlamadaWebphoneEstado entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<LlamadaWebphoneEstadoBO> listadoBO)
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
        private void AsignacionId(TLlamadaWebphoneEstado entidad, LlamadaWebphoneEstadoBO objetoBO)
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

        private TLlamadaWebphoneEstado MapeoEntidad(LlamadaWebphoneEstadoBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TLlamadaWebphoneEstado entidad = new TLlamadaWebphoneEstado();
                entidad = Mapper.Map<LlamadaWebphoneEstadoBO, TLlamadaWebphoneEstado>(objetoBO,
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
