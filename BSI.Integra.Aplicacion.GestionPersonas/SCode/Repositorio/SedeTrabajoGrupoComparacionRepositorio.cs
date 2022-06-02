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
    public class SedeTrabajoGrupoComparacionRepositorio : BaseRepository<TSedeTrabajoGrupoComparacion, SedeTrabajoGrupoComparacionBO>
    {
        #region Metodos Base
        public SedeTrabajoGrupoComparacionRepositorio() : base()
        {
        }
        public SedeTrabajoGrupoComparacionRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<SedeTrabajoGrupoComparacionBO> GetBy(Expression<Func<TSedeTrabajoGrupoComparacion, bool>> filter)
        {
            IEnumerable<TSedeTrabajoGrupoComparacion> listado = base.GetBy(filter);
            List<SedeTrabajoGrupoComparacionBO> listadoBO = new List<SedeTrabajoGrupoComparacionBO>();
            foreach (var itemEntidad in listado)
            {
                SedeTrabajoGrupoComparacionBO objetoBO = Mapper.Map<TSedeTrabajoGrupoComparacion, SedeTrabajoGrupoComparacionBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public SedeTrabajoGrupoComparacionBO FirstById(int id)
        {
            try
            {
                TSedeTrabajoGrupoComparacion entidad = base.FirstById(id);
                SedeTrabajoGrupoComparacionBO objetoBO = new SedeTrabajoGrupoComparacionBO();
                Mapper.Map<TSedeTrabajoGrupoComparacion, SedeTrabajoGrupoComparacionBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public SedeTrabajoGrupoComparacionBO FirstBy(Expression<Func<TSedeTrabajoGrupoComparacion, bool>> filter)
        {
            try
            {
                TSedeTrabajoGrupoComparacion entidad = base.FirstBy(filter);
                SedeTrabajoGrupoComparacionBO objetoBO = Mapper.Map<TSedeTrabajoGrupoComparacion, SedeTrabajoGrupoComparacionBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(SedeTrabajoGrupoComparacionBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TSedeTrabajoGrupoComparacion entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<SedeTrabajoGrupoComparacionBO> listadoBO)
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

        public bool Update(SedeTrabajoGrupoComparacionBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TSedeTrabajoGrupoComparacion entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<SedeTrabajoGrupoComparacionBO> listadoBO)
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
        private void AsignacionId(TSedeTrabajoGrupoComparacion entidad, SedeTrabajoGrupoComparacionBO objetoBO)
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

        private TSedeTrabajoGrupoComparacion MapeoEntidad(SedeTrabajoGrupoComparacionBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TSedeTrabajoGrupoComparacion entidad = new TSedeTrabajoGrupoComparacion();
                entidad = Mapper.Map<SedeTrabajoGrupoComparacionBO, TSedeTrabajoGrupoComparacion>(objetoBO,
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
