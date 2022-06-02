
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
    public class HistoricoDetalleOportunidadRn2Repositorio : BaseRepository<THistoricoDetalleOportunidadRn2, HistoricoDetalleOportunidadRn2BO>
    {
        #region Metodos Base
        public HistoricoDetalleOportunidadRn2Repositorio() : base()
        {
        }
        public HistoricoDetalleOportunidadRn2Repositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<HistoricoDetalleOportunidadRn2BO> GetBy(Expression<Func<THistoricoDetalleOportunidadRn2, bool>> filter)
        {
            IEnumerable<THistoricoDetalleOportunidadRn2> listado = base.GetBy(filter);
            List<HistoricoDetalleOportunidadRn2BO> listadoBO = new List<HistoricoDetalleOportunidadRn2BO>();
            foreach (var itemEntidad in listado)
            {
                HistoricoDetalleOportunidadRn2BO objetoBO = Mapper.Map<THistoricoDetalleOportunidadRn2, HistoricoDetalleOportunidadRn2BO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public HistoricoDetalleOportunidadRn2BO FirstById(int id)
        {
            try
            {
                THistoricoDetalleOportunidadRn2 entidad = base.FirstById(id);
                HistoricoDetalleOportunidadRn2BO objetoBO = new HistoricoDetalleOportunidadRn2BO();
                Mapper.Map<THistoricoDetalleOportunidadRn2, HistoricoDetalleOportunidadRn2BO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public HistoricoDetalleOportunidadRn2BO FirstBy(Expression<Func<THistoricoDetalleOportunidadRn2, bool>> filter)
        {
            try
            {
                THistoricoDetalleOportunidadRn2 entidad = base.FirstBy(filter);
                HistoricoDetalleOportunidadRn2BO objetoBO = Mapper.Map<THistoricoDetalleOportunidadRn2, HistoricoDetalleOportunidadRn2BO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(HistoricoDetalleOportunidadRn2BO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                THistoricoDetalleOportunidadRn2 entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<HistoricoDetalleOportunidadRn2BO> listadoBO)
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

        public bool Update(HistoricoDetalleOportunidadRn2BO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                THistoricoDetalleOportunidadRn2 entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<HistoricoDetalleOportunidadRn2BO> listadoBO)
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
        private void AsignacionId(THistoricoDetalleOportunidadRn2 entidad, HistoricoDetalleOportunidadRn2BO objetoBO)
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

        private THistoricoDetalleOportunidadRn2 MapeoEntidad(HistoricoDetalleOportunidadRn2BO objetoBO)
        {
            try
            {
                //crea la entidad padre
                THistoricoDetalleOportunidadRn2 entidad = new THistoricoDetalleOportunidadRn2();
                entidad = Mapper.Map<HistoricoDetalleOportunidadRn2BO, THistoricoDetalleOportunidadRn2>(objetoBO,
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
