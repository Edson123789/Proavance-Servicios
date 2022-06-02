using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using AutoMapper;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Maestros.BO;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using Newtonsoft.Json;

namespace BSI.Integra.Aplicacion.Transversal.Repositorio
{
    public class CorreoGmailArchivoAdjuntoRepositorio : BaseRepository<TCorreoGmailArchivoAdjunto, CorreoGmailArchivoAdjuntoBO>
    {
        #region Metodos Base
        public CorreoGmailArchivoAdjuntoRepositorio() : base()
        {
        }
        public CorreoGmailArchivoAdjuntoRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<CorreoGmailArchivoAdjuntoBO> GetBy(Expression<Func<TCorreoGmailArchivoAdjunto, bool>> filter)
        {
            IEnumerable<TCorreoGmailArchivoAdjunto> listado = base.GetBy(filter);
            List<CorreoGmailArchivoAdjuntoBO> listadoBO = new List<CorreoGmailArchivoAdjuntoBO>();
            foreach (var itemEntidad in listado)
            {
                CorreoGmailArchivoAdjuntoBO objetoBO = Mapper.Map<TCorreoGmailArchivoAdjunto, CorreoGmailArchivoAdjuntoBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public CorreoGmailArchivoAdjuntoBO FirstById(int id)
        {
            try
            {
                TCorreoGmailArchivoAdjunto entidad = base.FirstById(id);
                CorreoGmailArchivoAdjuntoBO objetoBO = new CorreoGmailArchivoAdjuntoBO();
                Mapper.Map<TCorreoGmailArchivoAdjunto, CorreoGmailArchivoAdjuntoBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public CorreoGmailArchivoAdjuntoBO FirstBy(Expression<Func<TCorreoGmailArchivoAdjunto, bool>> filter)
        {
            try
            {
                TCorreoGmailArchivoAdjunto entidad = base.FirstBy(filter);
                CorreoGmailArchivoAdjuntoBO objetoBO = Mapper.Map<TCorreoGmailArchivoAdjunto, CorreoGmailArchivoAdjuntoBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(CorreoGmailArchivoAdjuntoBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TCorreoGmailArchivoAdjunto entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<CorreoGmailArchivoAdjuntoBO> listadoBO)
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

        public bool Update(CorreoGmailArchivoAdjuntoBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TCorreoGmailArchivoAdjunto entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<CorreoGmailArchivoAdjuntoBO> listadoBO)
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
        private void AsignacionId(TCorreoGmailArchivoAdjunto entidad, CorreoGmailArchivoAdjuntoBO objetoBO)
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

        private TCorreoGmailArchivoAdjunto MapeoEntidad(CorreoGmailArchivoAdjuntoBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TCorreoGmailArchivoAdjunto entidad = new TCorreoGmailArchivoAdjunto();
                entidad = Mapper.Map<CorreoGmailArchivoAdjuntoBO, TCorreoGmailArchivoAdjunto>(objetoBO,
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
