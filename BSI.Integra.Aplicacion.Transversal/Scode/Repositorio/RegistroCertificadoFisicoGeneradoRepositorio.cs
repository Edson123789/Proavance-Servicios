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
    public class RegistroCertificadoFisicoGeneradoRepositorio : BaseRepository<TRegistroCertificadoFisicoGenerado, RegistroCertificadoFisicoGeneradoBO>
    {
        #region Metodos Base
        public RegistroCertificadoFisicoGeneradoRepositorio() : base()
        {
        }
        public RegistroCertificadoFisicoGeneradoRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<RegistroCertificadoFisicoGeneradoBO> GetBy(Expression<Func<TRegistroCertificadoFisicoGenerado, bool>> filter)
        {
            IEnumerable<TRegistroCertificadoFisicoGenerado> listado = base.GetBy(filter);
            List<RegistroCertificadoFisicoGeneradoBO> listadoBO = new List<RegistroCertificadoFisicoGeneradoBO>();
            foreach (var itemEntidad in listado)
            {
                RegistroCertificadoFisicoGeneradoBO objetoBO = Mapper.Map<TRegistroCertificadoFisicoGenerado, RegistroCertificadoFisicoGeneradoBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public RegistroCertificadoFisicoGeneradoBO FirstById(int id)
        {
            try
            {
                TRegistroCertificadoFisicoGenerado entidad = base.FirstById(id);
                RegistroCertificadoFisicoGeneradoBO objetoBO = new RegistroCertificadoFisicoGeneradoBO();
                Mapper.Map<TRegistroCertificadoFisicoGenerado, RegistroCertificadoFisicoGeneradoBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public RegistroCertificadoFisicoGeneradoBO FirstBy(Expression<Func<TRegistroCertificadoFisicoGenerado, bool>> filter)
        {
            try
            {
                TRegistroCertificadoFisicoGenerado entidad = base.FirstBy(filter);
                RegistroCertificadoFisicoGeneradoBO objetoBO = Mapper.Map<TRegistroCertificadoFisicoGenerado, RegistroCertificadoFisicoGeneradoBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(RegistroCertificadoFisicoGeneradoBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TRegistroCertificadoFisicoGenerado entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<RegistroCertificadoFisicoGeneradoBO> listadoBO)
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

        public bool Update(RegistroCertificadoFisicoGeneradoBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TRegistroCertificadoFisicoGenerado entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<RegistroCertificadoFisicoGeneradoBO> listadoBO)
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
        private void AsignacionId(TRegistroCertificadoFisicoGenerado entidad, RegistroCertificadoFisicoGeneradoBO objetoBO)
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

        private TRegistroCertificadoFisicoGenerado MapeoEntidad(RegistroCertificadoFisicoGeneradoBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TRegistroCertificadoFisicoGenerado entidad = new TRegistroCertificadoFisicoGenerado();
                entidad = Mapper.Map<RegistroCertificadoFisicoGeneradoBO, TRegistroCertificadoFisicoGenerado>(objetoBO,
                    opt => opt.ConfigureMap(MemberList.None));

                //mapea los hijos

                return entidad;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public IEnumerable<RegistroCertificadoFisicoGeneradoBO> GetFiltered<KProperty>(IEnumerable<Expression<Func<TRegistroCertificadoFisicoGenerado, bool>>> filters, Expression<Func<TRegistroCertificadoFisicoGenerado, KProperty>> orderBy, bool ascending)
        {
            IEnumerable<TRegistroCertificadoFisicoGenerado> listado = base.GetFiltered(filters, orderBy, ascending);
            List<RegistroCertificadoFisicoGeneradoBO> listadoBO = new List<RegistroCertificadoFisicoGeneradoBO>();

            foreach (var itemEntidad in listado)
            {
                RegistroCertificadoFisicoGeneradoBO objetoBO = Mapper.Map<TRegistroCertificadoFisicoGenerado, RegistroCertificadoFisicoGeneradoBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }
            return listadoBO;
        }
        #endregion
    }
}
