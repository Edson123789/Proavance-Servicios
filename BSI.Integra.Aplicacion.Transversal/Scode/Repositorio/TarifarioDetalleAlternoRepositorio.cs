using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using AutoMapper;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;

namespace BSI.Integra.Aplicacion.Transversal.Repositorio
{
    public class TarifarioDetalleAlternoRepositorio : BaseRepository<TTarifarioDetalleAlterno, TarifarioDetalleAlternoBO>
    {
        #region Metodos Base
        public TarifarioDetalleAlternoRepositorio() : base()
        {
        }
        public TarifarioDetalleAlternoRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<TarifarioDetalleAlternoBO> GetBy(Expression<Func<TTarifarioDetalleAlterno, bool>> filter)
        {
            IEnumerable<TTarifarioDetalleAlterno> listado = base.GetBy(filter);
            List<TarifarioDetalleAlternoBO> listadoBO = new List<TarifarioDetalleAlternoBO>();
            foreach (var itemEntidad in listado)
            {
                TarifarioDetalleAlternoBO objetoBO = Mapper.Map<TTarifarioDetalleAlterno, TarifarioDetalleAlternoBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public TarifarioDetalleAlternoBO FirstById(int id)
        {
            try
            {
                TTarifarioDetalleAlterno entidad = base.FirstById(id);
                TarifarioDetalleAlternoBO objetoBO = new TarifarioDetalleAlternoBO();
                Mapper.Map<TTarifarioDetalleAlterno, TarifarioDetalleAlternoBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public TarifarioDetalleAlternoBO FirstBy(Expression<Func<TTarifarioDetalleAlterno, bool>> filter)
        {
            try
            {
                TTarifarioDetalleAlterno entidad = base.FirstBy(filter);
                TarifarioDetalleAlternoBO objetoBO = Mapper.Map<TTarifarioDetalleAlterno, TarifarioDetalleAlternoBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(TarifarioDetalleAlternoBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TTarifarioDetalleAlterno entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<TarifarioDetalleAlternoBO> listadoBO)
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

        public bool Update(TarifarioDetalleAlternoBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TTarifarioDetalleAlterno entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<TarifarioDetalleAlternoBO> listadoBO)
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
        private void AsignacionId(TTarifarioDetalleAlterno entidad, TarifarioDetalleAlternoBO objetoBO)
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

        private TTarifarioDetalleAlterno MapeoEntidad(TarifarioDetalleAlternoBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TTarifarioDetalleAlterno entidad = new TTarifarioDetalleAlterno();
                entidad = Mapper.Map<TarifarioDetalleAlternoBO, TTarifarioDetalleAlterno>(objetoBO,
                    opt => opt.ConfigureMap(MemberList.None));

                //mapea los hijos

                return entidad;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public IEnumerable<TarifarioDetalleAlternoBO> GetFiltered<KProperty>(IEnumerable<Expression<Func<TTarifarioDetalleAlterno, bool>>> filters, Expression<Func<TTarifarioDetalleAlterno, KProperty>> orderBy, bool ascending)
        {
            IEnumerable<TTarifarioDetalleAlterno> listado = base.GetFiltered(filters, orderBy, ascending);
            List<TarifarioDetalleAlternoBO> listadoBO = new List<TarifarioDetalleAlternoBO>();

            foreach (var itemEntidad in listado)
            {
                TarifarioDetalleAlternoBO objetoBO = Mapper.Map<TTarifarioDetalleAlterno, TarifarioDetalleAlternoBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }
            return listadoBO;
        }
        #endregion




    }
}
