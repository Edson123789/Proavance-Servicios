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
    public class PostulanteNivelPotencialRepositorio : BaseRepository<TPostulanteNivelPotencial, PostulanteNivelPotencialBO>
    {
        #region Metodos Base
        public PostulanteNivelPotencialRepositorio() : base()
        {
        }
        public PostulanteNivelPotencialRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<PostulanteNivelPotencialBO> GetBy(Expression<Func<TPostulanteNivelPotencial, bool>> filter)
        {
            IEnumerable<TPostulanteNivelPotencial> listado = base.GetBy(filter);
            List<PostulanteNivelPotencialBO> listadoBO = new List<PostulanteNivelPotencialBO>();
            foreach (var itemEntidad in listado)
            {
                PostulanteNivelPotencialBO objetoBO = Mapper.Map<TPostulanteNivelPotencial, PostulanteNivelPotencialBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public PostulanteNivelPotencialBO FirstById(int id)
        {
            try
            {
                TPostulanteNivelPotencial entidad = base.FirstById(id);
                PostulanteNivelPotencialBO objetoBO = new PostulanteNivelPotencialBO();
                Mapper.Map<TPostulanteNivelPotencial, PostulanteNivelPotencialBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public PostulanteNivelPotencialBO FirstBy(Expression<Func<TPostulanteNivelPotencial, bool>> filter)
        {
            try
            {
                TPostulanteNivelPotencial entidad = base.FirstBy(filter);
                PostulanteNivelPotencialBO objetoBO = Mapper.Map<TPostulanteNivelPotencial, PostulanteNivelPotencialBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(PostulanteNivelPotencialBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TPostulanteNivelPotencial entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<PostulanteNivelPotencialBO> listadoBO)
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

        public bool Update(PostulanteNivelPotencialBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TPostulanteNivelPotencial entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<PostulanteNivelPotencialBO> listadoBO)
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
        private void AsignacionId(TPostulanteNivelPotencial entidad, PostulanteNivelPotencialBO objetoBO)
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

        private TPostulanteNivelPotencial MapeoEntidad(PostulanteNivelPotencialBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TPostulanteNivelPotencial entidad = new TPostulanteNivelPotencial();
                entidad = Mapper.Map<PostulanteNivelPotencialBO, TPostulanteNivelPotencial>(objetoBO,
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
