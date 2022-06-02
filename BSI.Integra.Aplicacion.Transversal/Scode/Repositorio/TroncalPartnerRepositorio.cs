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

namespace BSI.Integra.Aplicacion.Transversal.Repositorio
{
    public class TroncalPartnerRepositorio : BaseRepository<TTroncalPartner, TroncalPartnerBO>
    {
        #region Metodos Base
        public TroncalPartnerRepositorio() : base()
        {
        }
        public TroncalPartnerRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<TroncalPartnerBO> GetBy(Expression<Func<TTroncalPartner, bool>> filter)
        {
            IEnumerable<TTroncalPartner> listado = base.GetBy(filter);
            List<TroncalPartnerBO> listadoBO = new List<TroncalPartnerBO>();
            foreach (var itemEntidad in listado)
            {
                TroncalPartnerBO objetoBO = Mapper.Map<TTroncalPartner, TroncalPartnerBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public TroncalPartnerBO FirstById(int id)
        {
            try
            {
                TTroncalPartner entidad = base.FirstById(id);
                TroncalPartnerBO objetoBO = new TroncalPartnerBO();
                Mapper.Map<TTroncalPartner, TroncalPartnerBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public TroncalPartnerBO FirstBy(Expression<Func<TTroncalPartner, bool>> filter)
        {
            try
            {
                TTroncalPartner entidad = base.FirstBy(filter);
                TroncalPartnerBO objetoBO = Mapper.Map<TTroncalPartner, TroncalPartnerBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(TroncalPartnerBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TTroncalPartner entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<TroncalPartnerBO> listadoBO)
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

        public bool Update(TroncalPartnerBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TTroncalPartner entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<TroncalPartnerBO> listadoBO)
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
        private void AsignacionId(TTroncalPartner entidad, TroncalPartnerBO objetoBO)
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

        private TTroncalPartner MapeoEntidad(TroncalPartnerBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TTroncalPartner entidad = new TTroncalPartner();
                entidad = Mapper.Map<TroncalPartnerBO, TTroncalPartner>(objetoBO,
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

        public List<FiltroDTO> ObtenerParaFiltro()
        {
            try {
                return GetBy(w => w.Estado, y => new FiltroDTO { Id = y.Id, Nombre = y.Nombre }).ToList();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
