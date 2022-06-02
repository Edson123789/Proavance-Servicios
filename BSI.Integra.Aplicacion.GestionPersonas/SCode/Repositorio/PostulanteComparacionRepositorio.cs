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
    /// Repositorio: PostulanteComparacionRepositorio
    /// Autor: Edgar S.
    /// Fecha: 29/01/2021
    /// <summary>
    /// Gestión de Grupo de Comparación de Postulantes
    /// </summary>
    public class PostulanteComparacionRepositorio : BaseRepository<TPostulanteComparacion, PostulanteComparacionBO>
    {
        #region Metodos Base
        public PostulanteComparacionRepositorio() : base()
        {
        }
        public PostulanteComparacionRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<PostulanteComparacionBO> GetBy(Expression<Func<TPostulanteComparacion, bool>> filter)
        {
            IEnumerable<TPostulanteComparacion> listado = base.GetBy(filter);
            List<PostulanteComparacionBO> listadoBO = new List<PostulanteComparacionBO>();
            foreach (var itemEntidad in listado)
            {
                PostulanteComparacionBO objetoBO = Mapper.Map<TPostulanteComparacion, PostulanteComparacionBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public PostulanteComparacionBO FirstById(int id)
        {
            try
            {
                TPostulanteComparacion entidad = base.FirstById(id);
                PostulanteComparacionBO objetoBO = new PostulanteComparacionBO();
                Mapper.Map<TPostulanteComparacion, PostulanteComparacionBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public PostulanteComparacionBO FirstBy(Expression<Func<TPostulanteComparacion, bool>> filter)
        {
            try
            {
                TPostulanteComparacion entidad = base.FirstBy(filter);
                PostulanteComparacionBO objetoBO = Mapper.Map<TPostulanteComparacion, PostulanteComparacionBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(PostulanteComparacionBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TPostulanteComparacion entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<PostulanteComparacionBO> listadoBO)
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

        public bool Update(PostulanteComparacionBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TPostulanteComparacion entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<PostulanteComparacionBO> listadoBO)
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
        private void AsignacionId(TPostulanteComparacion entidad, PostulanteComparacionBO objetoBO)
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

        private TPostulanteComparacion MapeoEntidad(PostulanteComparacionBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TPostulanteComparacion entidad = new TPostulanteComparacion();
                entidad = Mapper.Map<PostulanteComparacionBO, TPostulanteComparacion>(objetoBO,
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
