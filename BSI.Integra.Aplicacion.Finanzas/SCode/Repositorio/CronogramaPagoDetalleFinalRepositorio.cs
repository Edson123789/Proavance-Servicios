using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using AutoMapper;
using BSI.Integra.Aplicacion.Transversal.Helper;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Finanzas.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using Newtonsoft.Json;
using System.Globalization;
using BSI.Integra.Aplicacion.Transversal.Repositorio;

namespace BSI.Integra.Aplicacion.Finanzas.Repositorio
{
    public class CronogramaPagoDetalleFinalCierreRepositorio : BaseRepository<TCronogramaPagoDetalleFinalCierre, CronogramaPagoDetalleFinalCierreBO>
    {
        #region Metodos Base
        public CronogramaPagoDetalleFinalCierreRepositorio() : base()
        {
        }
        public CronogramaPagoDetalleFinalCierreRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<CronogramaPagoDetalleFinalCierreBO> GetBy(Expression<Func<TCronogramaPagoDetalleFinalCierre, bool>> filter)
        {
            IEnumerable<TCronogramaPagoDetalleFinalCierre> listado = base.GetBy(filter);
            List<CronogramaPagoDetalleFinalCierreBO> listadoBO = new List<CronogramaPagoDetalleFinalCierreBO>();
            foreach (var itemEntidad in listado)
            {
                CronogramaPagoDetalleFinalCierreBO objetoBO = Mapper.Map<TCronogramaPagoDetalleFinalCierre, CronogramaPagoDetalleFinalCierreBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public CronogramaPagoDetalleFinalCierreBO FirstById(int id)
        {
            try
            {
                TCronogramaPagoDetalleFinalCierre entidad = base.FirstById(id);
                CronogramaPagoDetalleFinalCierreBO objetoBO = new CronogramaPagoDetalleFinalCierreBO();
                Mapper.Map<TCronogramaPagoDetalleFinalCierre, CronogramaPagoDetalleFinalCierreBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public CronogramaPagoDetalleFinalCierreBO FirstBy(Expression<Func<TCronogramaPagoDetalleFinalCierre, bool>> filter)
        {
            try
            {
                TCronogramaPagoDetalleFinalCierre entidad = base.FirstBy(filter);
                CronogramaPagoDetalleFinalCierreBO objetoBO = Mapper.Map<TCronogramaPagoDetalleFinalCierre, CronogramaPagoDetalleFinalCierreBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(CronogramaPagoDetalleFinalCierreBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TCronogramaPagoDetalleFinalCierre entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<CronogramaPagoDetalleFinalCierreBO> listadoBO)
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

        public bool Update(CronogramaPagoDetalleFinalCierreBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TCronogramaPagoDetalleFinalCierre entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<CronogramaPagoDetalleFinalCierreBO> listadoBO)
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
        private void AsignacionId(TCronogramaPagoDetalleFinalCierre entidad, CronogramaPagoDetalleFinalCierreBO objetoBO)
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

        private TCronogramaPagoDetalleFinalCierre MapeoEntidad(CronogramaPagoDetalleFinalCierreBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TCronogramaPagoDetalleFinalCierre entidad = new TCronogramaPagoDetalleFinalCierre();
                entidad = Mapper.Map<CronogramaPagoDetalleFinalCierreBO, TCronogramaPagoDetalleFinalCierre>(objetoBO,
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
