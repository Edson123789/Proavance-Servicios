using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using AutoMapper;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.GestionPersonas.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using Newtonsoft.Json;

namespace BSI.Integra.Aplicacion.GestionPersonas.Repositorio
{
    public class TipoDocumentacionPersonalRepositorio : BaseRepository<TTipoDocumentacionPersonal, TipoDocumentacionPersonalBO>
    {
        #region Metodos Base
        public TipoDocumentacionPersonalRepositorio() : base()
        {
        }
        public TipoDocumentacionPersonalRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<TipoDocumentacionPersonalBO> GetBy(Expression<Func<TTipoDocumentacionPersonal, bool>> filter)
        {
            IEnumerable<TTipoDocumentacionPersonal> listado = base.GetBy(filter);
            List<TipoDocumentacionPersonalBO> listadoBO = new List<TipoDocumentacionPersonalBO>();
            foreach (var itemEntidad in listado)
            {
                TipoDocumentacionPersonalBO objetoBO = Mapper.Map<TTipoDocumentacionPersonal, TipoDocumentacionPersonalBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public TipoDocumentacionPersonalBO FirstById(int id)
        {
            try
            {
                TTipoDocumentacionPersonal entidad = base.FirstById(id);
                TipoDocumentacionPersonalBO objetoBO = new TipoDocumentacionPersonalBO();
                Mapper.Map<TTipoDocumentacionPersonal, TipoDocumentacionPersonalBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public TipoDocumentacionPersonalBO FirstBy(Expression<Func<TTipoDocumentacionPersonal, bool>> filter)
        {
            try
            {
                TTipoDocumentacionPersonal entidad = base.FirstBy(filter);
                TipoDocumentacionPersonalBO objetoBO = Mapper.Map<TTipoDocumentacionPersonal, TipoDocumentacionPersonalBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(TipoDocumentacionPersonalBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TTipoDocumentacionPersonal entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<TipoDocumentacionPersonalBO> listadoBO)
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

        public bool Update(TipoDocumentacionPersonalBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TTipoDocumentacionPersonal entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<TipoDocumentacionPersonalBO> listadoBO)
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
        private void AsignacionId(TTipoDocumentacionPersonal entidad, TipoDocumentacionPersonalBO objetoBO)
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

        private TTipoDocumentacionPersonal MapeoEntidad(TipoDocumentacionPersonalBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TTipoDocumentacionPersonal entidad = new TTipoDocumentacionPersonal();
                entidad = Mapper.Map<TipoDocumentacionPersonalBO, TTipoDocumentacionPersonal>(objetoBO,
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
