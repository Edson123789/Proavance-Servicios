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

namespace BSI.Integra.Aplicacion.Transversal.Repositorio
{
    public class CriterioDocRepositorio : BaseRepository<TCriterioDoc, CriterioDocBO>
    {
        #region Metodos Base
        public CriterioDocRepositorio() : base()
        {
        }
        public CriterioDocRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<CriterioDocBO> GetBy(Expression<Func<TCriterioDoc, bool>> filter)
        {
            IEnumerable<TCriterioDoc> listado = base.GetBy(filter);
            List<CriterioDocBO> listadoBO = new List<CriterioDocBO>();
            foreach (var itemEntidad in listado)
            {
                CriterioDocBO objetoBO = Mapper.Map<TCriterioDoc, CriterioDocBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public CriterioDocBO FirstById(int id)
        {
            try
            {
                TCriterioDoc entidad = base.FirstById(id);
                CriterioDocBO objetoBO = new CriterioDocBO();
                Mapper.Map<TCriterioDoc, CriterioDocBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public CriterioDocBO FirstBy(Expression<Func<TCriterioDoc, bool>> filter)
        {
            try
            {
                TCriterioDoc entidad = base.FirstBy(filter);
                CriterioDocBO objetoBO = Mapper.Map<TCriterioDoc, CriterioDocBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(CriterioDocBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TCriterioDoc entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<CriterioDocBO> listadoBO)
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

        public bool Update(CriterioDocBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TCriterioDoc entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<CriterioDocBO> listadoBO)
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
        private void AsignacionId(TCriterioDoc entidad, CriterioDocBO objetoBO)
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

        private TCriterioDoc MapeoEntidad(CriterioDocBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TCriterioDoc entidad = new TCriterioDoc();
                entidad = Mapper.Map<CriterioDocBO, TCriterioDoc>(objetoBO,
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

        /// <summary>
        ///Obtiene una lista de criterios con todos los datos para generar depencia por tipo de modalidad
        /// </summary>
        /// <returns></returns>
        public List<CriterioDocSeleccionarDTO> ObtenerTodoSeleccionar() {
            try
            {
                return this.GetBy(x => x.Estado, x => new CriterioDocSeleccionarDTO { Id = x.Id, Nombre = x.Nombre, ModalidadPresencial = x.ModalidadPresencial, ModalidadOnline = x.ModalidadOnline, ModalidadAonline = x.ModalidadAonline }).ToList();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
