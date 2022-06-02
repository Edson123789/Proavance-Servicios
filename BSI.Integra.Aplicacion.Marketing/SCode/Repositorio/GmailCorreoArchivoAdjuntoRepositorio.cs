using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using AutoMapper;
using BSI.Integra.Aplicacion.Marketing.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using Newtonsoft.Json;
using BSI.Integra.Aplicacion.DTOs;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System.IO;

namespace BSI.Integra.Aplicacion.Marketing.Repositorio
{
    public class GmailCorreoArchivoAdjuntoRepositorio : BaseRepository<TGmailCorreoArchivoAdjunto, GmailCorreoArchivoAdjuntoBO>
    {
        #region Metodos Base
        public GmailCorreoArchivoAdjuntoRepositorio() : base()
        {
        }
        public GmailCorreoArchivoAdjuntoRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<GmailCorreoArchivoAdjuntoBO> GetBy(Expression<Func<TGmailCorreoArchivoAdjunto, bool>> filter)
        {
            IEnumerable<TGmailCorreoArchivoAdjunto> listado = base.GetBy(filter);
            List<GmailCorreoArchivoAdjuntoBO> listadoBO = new List<GmailCorreoArchivoAdjuntoBO>();
            foreach (var itemEntidad in listado)
            {
                GmailCorreoArchivoAdjuntoBO objetoBO = Mapper.Map<TGmailCorreoArchivoAdjunto, GmailCorreoArchivoAdjuntoBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public GmailCorreoArchivoAdjuntoBO FirstById(int id)
        {
            try
            {
                TGmailCorreoArchivoAdjunto entidad = base.FirstById(id);
                GmailCorreoArchivoAdjuntoBO objetoBO = new GmailCorreoArchivoAdjuntoBO();
                Mapper.Map<TGmailCorreoArchivoAdjunto, GmailCorreoArchivoAdjuntoBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public GmailCorreoArchivoAdjuntoBO FirstBy(Expression<Func<TGmailCorreoArchivoAdjunto, bool>> filter)
        {
            try
            {
                TGmailCorreoArchivoAdjunto entidad = base.FirstBy(filter);
                GmailCorreoArchivoAdjuntoBO objetoBO = Mapper.Map<TGmailCorreoArchivoAdjunto, GmailCorreoArchivoAdjuntoBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(GmailCorreoArchivoAdjuntoBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TGmailCorreoArchivoAdjunto entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<GmailCorreoArchivoAdjuntoBO> listadoBO)
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

        public bool Update(GmailCorreoArchivoAdjuntoBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TGmailCorreoArchivoAdjunto entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<GmailCorreoArchivoAdjuntoBO> listadoBO)
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
        private void AsignacionId(TGmailCorreoArchivoAdjunto entidad, GmailCorreoArchivoAdjuntoBO objetoBO)
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

        private TGmailCorreoArchivoAdjunto MapeoEntidad(GmailCorreoArchivoAdjuntoBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TGmailCorreoArchivoAdjunto entidad = new TGmailCorreoArchivoAdjunto();
                entidad = Mapper.Map<GmailCorreoArchivoAdjuntoBO, TGmailCorreoArchivoAdjunto>(objetoBO,
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
