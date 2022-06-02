using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using AutoMapper;
using BSI.Integra.Aplicacion.Operaciones.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using Newtonsoft.Json;

namespace BSI.Integra.Aplicacion.Operaciones.Repositorio
{
    /// Repositorio: InformacionAlumnoChatLogRepositorio
    /// Autor: Jose Villena
    /// Fecha: 16/07/2021
    /// <summary>
    /// Gestion con la base de datos de la tabla ope.T_InformacionAlumnoChatLog
    /// </summary>
    public class InformacionAlumnoChatLogRepositorio : BaseRepository<TInformacionAlumnoChatLog, InformacionAlumnoChatLogBO>
    {
        #region Metodos Base
        public InformacionAlumnoChatLogRepositorio() : base()
        {
        }
        public InformacionAlumnoChatLogRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<InformacionAlumnoChatLogBO> GetBy(Expression<Func<TInformacionAlumnoChatLog, bool>> filter)
        {
            IEnumerable<TInformacionAlumnoChatLog> listado = base.GetBy(filter);
            List<InformacionAlumnoChatLogBO> listadoBO = new List<InformacionAlumnoChatLogBO>();
            foreach (var itemEntidad in listado)
            {
                InformacionAlumnoChatLogBO objetoBO = Mapper.Map<TInformacionAlumnoChatLog, InformacionAlumnoChatLogBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public InformacionAlumnoChatLogBO FirstById(int id)
        {
            try
            {
                TInformacionAlumnoChatLog entidad = base.FirstById(id);
                InformacionAlumnoChatLogBO objetoBO = new InformacionAlumnoChatLogBO();
                Mapper.Map<TInformacionAlumnoChatLog, InformacionAlumnoChatLogBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public InformacionAlumnoChatLogBO FirstBy(Expression<Func<TInformacionAlumnoChatLog, bool>> filter)
        {
            try
            {
                TInformacionAlumnoChatLog entidad = base.FirstBy(filter);
                InformacionAlumnoChatLogBO objetoBO = Mapper.Map<TInformacionAlumnoChatLog, InformacionAlumnoChatLogBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(InformacionAlumnoChatLogBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TInformacionAlumnoChatLog entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<InformacionAlumnoChatLogBO> listadoBO)
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

        public bool Update(InformacionAlumnoChatLogBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TInformacionAlumnoChatLog entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<InformacionAlumnoChatLogBO> listadoBO)
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
        private void AsignacionId(TInformacionAlumnoChatLog entidad, InformacionAlumnoChatLogBO objetoBO)
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

        private TInformacionAlumnoChatLog MapeoEntidad(InformacionAlumnoChatLogBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TInformacionAlumnoChatLog entidad = new TInformacionAlumnoChatLog();
                entidad = Mapper.Map<InformacionAlumnoChatLogBO, TInformacionAlumnoChatLog>(objetoBO,
                    opt => opt.ConfigureMap(MemberList.None));

                //mapea los hijos

                return entidad;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public IEnumerable<InformacionAlumnoChatLogBO> GetFiltered<KProperty>(IEnumerable<Expression<Func<TInformacionAlumnoChatLog, bool>>> filters, Expression<Func<TInformacionAlumnoChatLog, KProperty>> orderBy, bool ascending)
        {
            IEnumerable<TInformacionAlumnoChatLog> listado = base.GetFiltered(filters, orderBy, ascending);
            List<InformacionAlumnoChatLogBO> listadoBO = new List<InformacionAlumnoChatLogBO>();

            foreach (var itemEntidad in listado)
            {
                InformacionAlumnoChatLogBO objetoBO = Mapper.Map<TInformacionAlumnoChatLog, InformacionAlumnoChatLogBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }
            return listadoBO;
        }
        #endregion


    }
}
