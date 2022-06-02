using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using AutoMapper;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;

namespace BSI.Integra.Aplicacion.Transversal.Repositorio
{
    public class PostComentarioUsuarioLogRepositorio : BaseRepository<TPostComentarioUsuarioLog, PostComentarioUsuarioLogBO>
    {
        #region Metodos Base
        public PostComentarioUsuarioLogRepositorio() : base()
        {
        }
        public PostComentarioUsuarioLogRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<PostComentarioUsuarioLogBO> GetBy(Expression<Func<TPostComentarioUsuarioLog, bool>> filter)
        {
            IEnumerable<TPostComentarioUsuarioLog> listado = base.GetBy(filter);
            List<PostComentarioUsuarioLogBO> listadoBO = new List<PostComentarioUsuarioLogBO>();
            foreach (var itemEntidad in listado)
            {
                PostComentarioUsuarioLogBO objetoBO = Mapper.Map<TPostComentarioUsuarioLog, PostComentarioUsuarioLogBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public PostComentarioUsuarioLogBO FirstById(int id)
        {
            try
            {
                TPostComentarioUsuarioLog entidad = base.FirstById(id);
                PostComentarioUsuarioLogBO objetoBO = new PostComentarioUsuarioLogBO();
                Mapper.Map<TPostComentarioUsuarioLog, PostComentarioUsuarioLogBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public PostComentarioUsuarioLogBO FirstBy(Expression<Func<TPostComentarioUsuarioLog, bool>> filter)
        {
            try
            {
                TPostComentarioUsuarioLog entidad = base.FirstBy(filter);
                PostComentarioUsuarioLogBO objetoBO = Mapper.Map<TPostComentarioUsuarioLog, PostComentarioUsuarioLogBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(PostComentarioUsuarioLogBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TPostComentarioUsuarioLog entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<PostComentarioUsuarioLogBO> listadoBO)
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

        public bool Update(PostComentarioUsuarioLogBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TPostComentarioUsuarioLog entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<PostComentarioUsuarioLogBO> listadoBO)
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
        private void AsignacionId(TPostComentarioUsuarioLog entidad, PostComentarioUsuarioLogBO objetoBO)
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

        private TPostComentarioUsuarioLog MapeoEntidad(PostComentarioUsuarioLogBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TPostComentarioUsuarioLog entidad = new TPostComentarioUsuarioLog();
                entidad = Mapper.Map<PostComentarioUsuarioLogBO, TPostComentarioUsuarioLog>(objetoBO,
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
