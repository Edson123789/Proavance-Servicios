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
    public class PostulanteFactorDesaprobatorioRepositorio : BaseRepository<TPostulanteFactorDesaprobatorio, PostulanteFactorDesaprobatorioBO>
    {
        #region Metodos Base
        public PostulanteFactorDesaprobatorioRepositorio() : base()
        {
        }
        public PostulanteFactorDesaprobatorioRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<PostulanteFactorDesaprobatorioBO> GetBy(Expression<Func<TPostulanteFactorDesaprobatorio, bool>> filter)
        {
            IEnumerable<TPostulanteFactorDesaprobatorio> listado = base.GetBy(filter);
            List<PostulanteFactorDesaprobatorioBO> listadoBO = new List<PostulanteFactorDesaprobatorioBO>();
            foreach (var itemEntidad in listado)
            {
                PostulanteFactorDesaprobatorioBO objetoBO = Mapper.Map<TPostulanteFactorDesaprobatorio, PostulanteFactorDesaprobatorioBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public PostulanteFactorDesaprobatorioBO FirstById(int id)
        {
            try
            {
                TPostulanteFactorDesaprobatorio entidad = base.FirstById(id);
                PostulanteFactorDesaprobatorioBO objetoBO = new PostulanteFactorDesaprobatorioBO();
                Mapper.Map<TPostulanteFactorDesaprobatorio, PostulanteFactorDesaprobatorioBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public PostulanteFactorDesaprobatorioBO FirstBy(Expression<Func<TPostulanteFactorDesaprobatorio, bool>> filter)
        {
            try
            {
                TPostulanteFactorDesaprobatorio entidad = base.FirstBy(filter);
                PostulanteFactorDesaprobatorioBO objetoBO = Mapper.Map<TPostulanteFactorDesaprobatorio, PostulanteFactorDesaprobatorioBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(PostulanteFactorDesaprobatorioBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TPostulanteFactorDesaprobatorio entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<PostulanteFactorDesaprobatorioBO> listadoBO)
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

        public bool Update(PostulanteFactorDesaprobatorioBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TPostulanteFactorDesaprobatorio entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<PostulanteFactorDesaprobatorioBO> listadoBO)
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
        private void AsignacionId(TPostulanteFactorDesaprobatorio entidad, PostulanteFactorDesaprobatorioBO objetoBO)
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

        private TPostulanteFactorDesaprobatorio MapeoEntidad(PostulanteFactorDesaprobatorioBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TPostulanteFactorDesaprobatorio entidad = new TPostulanteFactorDesaprobatorio();
                entidad = Mapper.Map<PostulanteFactorDesaprobatorioBO, TPostulanteFactorDesaprobatorio>(objetoBO,
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
