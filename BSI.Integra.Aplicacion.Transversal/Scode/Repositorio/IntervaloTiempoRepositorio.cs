using AutoMapper;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace BSI.Integra.Aplicacion.Transversal.Scode.Repositorio
{
    public class IntervaloTiempoRepositorio : BaseRepository<TIntervaloTiempo, IntervaloTiempoBO>
    {
        #region Metodos Base
        public IntervaloTiempoRepositorio() : base()
        {
        }
        public IntervaloTiempoRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<IntervaloTiempoBO> GetBy(Expression<Func<TIntervaloTiempo, bool>> filter)
        {
            IEnumerable<TIntervaloTiempo> listado = base.GetBy(filter);
            List<IntervaloTiempoBO> listadoBO = new List<IntervaloTiempoBO>();
            foreach (var itemEntidad in listado)
            {
                IntervaloTiempoBO objetoBO = Mapper.Map<TIntervaloTiempo, IntervaloTiempoBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public IntervaloTiempoBO FirstById(int id)
        {
            try
            {
                TIntervaloTiempo entidad = base.FirstById(id);
                IntervaloTiempoBO objetoBO = new IntervaloTiempoBO();
                Mapper.Map<TIntervaloTiempo, IntervaloTiempoBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public IntervaloTiempoBO FirstBy(Expression<Func<TIntervaloTiempo, bool>> filter)
        {
            try
            {
                TIntervaloTiempo entidad = base.FirstBy(filter);
                IntervaloTiempoBO objetoBO = Mapper.Map<TIntervaloTiempo, IntervaloTiempoBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(IntervaloTiempoBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TIntervaloTiempo entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<IntervaloTiempoBO> listadoBO)
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

        public bool Update(IntervaloTiempoBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TIntervaloTiempo entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<IntervaloTiempoBO> listadoBO)
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
        private void AsignacionId(TIntervaloTiempo entidad, IntervaloTiempoBO objetoBO)
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

        private TIntervaloTiempo MapeoEntidad(IntervaloTiempoBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TIntervaloTiempo entidad = new TIntervaloTiempo();
                entidad = Mapper.Map<IntervaloTiempoBO, TIntervaloTiempo>(objetoBO,
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

        public List<FiltroDTO> ObtenerFiltro()
        {
            try
            {
                return GetBy(w => w.Estado == true, y=> new FiltroDTO { Id=y.Id,Nombre=y.Nombre}).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        
        }
    }
}
