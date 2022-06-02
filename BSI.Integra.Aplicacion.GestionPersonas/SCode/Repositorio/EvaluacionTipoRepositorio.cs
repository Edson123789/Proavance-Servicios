using AutoMapper;
using BSI.Integra.Aplicacion.GestionPersonas.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace BSI.Integra.Aplicacion.GestionPersonas.Repositorio
{
    /// Repositorio: GestionPersonas/EvaluacionTipo
    /// Autor: Britsel Calluchi
    /// Fecha: 08/09/2021
    /// <summary>
    /// Repositorio para consultas de gp.T_EvaluacionTipo
    /// </summary>
    public class EvaluacionTipoRepositorio : BaseRepository<TEvaluacionTipo, EvaluacionTipoBO>
    {
        #region Metodos Base
        public EvaluacionTipoRepositorio() : base()
        {
        }
        public EvaluacionTipoRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<EvaluacionTipoBO> GetBy(Expression<Func<TEvaluacionTipo, bool>> filter)
        {
            IEnumerable<TEvaluacionTipo> listado = base.GetBy(filter);
            List<EvaluacionTipoBO> listadoBO = new List<EvaluacionTipoBO>();
            foreach (var itemEntidad in listado)
            {
                EvaluacionTipoBO objetoBO = Mapper.Map<TEvaluacionTipo, EvaluacionTipoBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public EvaluacionTipoBO FirstById(int id)
        {
            try
            {
                TEvaluacionTipo entidad = base.FirstById(id);
                EvaluacionTipoBO objetoBO = new EvaluacionTipoBO();
                Mapper.Map<TEvaluacionTipo, EvaluacionTipoBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public EvaluacionTipoBO FirstBy(Expression<Func<TEvaluacionTipo, bool>> filter)
        {
            try
            {
                TEvaluacionTipo entidad = base.FirstBy(filter);
                EvaluacionTipoBO objetoBO = Mapper.Map<TEvaluacionTipo, EvaluacionTipoBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(EvaluacionTipoBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TEvaluacionTipo entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<EvaluacionTipoBO> listadoBO)
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

        public bool Update(EvaluacionTipoBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TEvaluacionTipo entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<EvaluacionTipoBO> listadoBO)
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
        private void AsignacionId(TEvaluacionTipo entidad, EvaluacionTipoBO objetoBO)
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

        private TEvaluacionTipo MapeoEntidad(EvaluacionTipoBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TEvaluacionTipo entidad = new TEvaluacionTipo();
                entidad = Mapper.Map<EvaluacionTipoBO, TEvaluacionTipo>(objetoBO,
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
