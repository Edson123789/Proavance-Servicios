using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using AutoMapper;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;

namespace BSI.Integra.Aplicacion.Transversal.Repositorio
{
    public class GmailFolderRepositorio : BaseRepository<TGmailFolder, GmailFolderBO>
    {
        #region Metodos Base
        public GmailFolderRepositorio() : base()
        {
        }
        public GmailFolderRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<GmailFolderBO> GetBy(Expression<Func<TGmailFolder, bool>> filter)
        {
            IEnumerable<TGmailFolder> listado = base.GetBy(filter);
            List<GmailFolderBO> listadoBO = new List<GmailFolderBO>();
            foreach (var itemEntidad in listado)
            {
                GmailFolderBO objetoBO = Mapper.Map<TGmailFolder, GmailFolderBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public GmailFolderBO FirstById(int id)
        {
            try
            {
                TGmailFolder entidad = base.FirstById(id);
                GmailFolderBO objetoBO = new GmailFolderBO();
                Mapper.Map<TGmailFolder, GmailFolderBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public GmailFolderBO FirstBy(Expression<Func<TGmailFolder, bool>> filter)
        {
            try
            {
                TGmailFolder entidad = base.FirstBy(filter);
                GmailFolderBO objetoBO = Mapper.Map<TGmailFolder, GmailFolderBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(GmailFolderBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TGmailFolder entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<GmailFolderBO> listadoBO)
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

        public bool Update(GmailFolderBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TGmailFolder entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<GmailFolderBO> listadoBO)
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
        private void AsignacionId(TGmailFolder entidad, GmailFolderBO objetoBO)
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

        private TGmailFolder MapeoEntidad(GmailFolderBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TGmailFolder entidad = new TGmailFolder();
                entidad = Mapper.Map<GmailFolderBO, TGmailFolder>(objetoBO,
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
