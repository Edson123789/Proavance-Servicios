using AutoMapper;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace BSI.Integra.Aplicacion.Transversal.Repositorio
{
    public class ActividadCabeceraDiaSemanaRepositorio : BaseRepository<TActividadCabeceraDiaSemana, ActividadCabeceraDiaSemanaBO>
    {
        #region Metodos Base
        public ActividadCabeceraDiaSemanaRepositorio() : base()
        {
        }
        public ActividadCabeceraDiaSemanaRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<ActividadCabeceraDiaSemanaBO> GetBy(Expression<Func<TActividadCabeceraDiaSemana, bool>> filter)
        {
            IEnumerable<TActividadCabeceraDiaSemana> listado = base.GetBy(filter);
            List<ActividadCabeceraDiaSemanaBO> listadoBO = new List<ActividadCabeceraDiaSemanaBO>();
            foreach (var itemEntidad in listado)
            {
                ActividadCabeceraDiaSemanaBO objetoBO = Mapper.Map<TActividadCabeceraDiaSemana, ActividadCabeceraDiaSemanaBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public ActividadCabeceraDiaSemanaBO FirstById(int id)
        {
            try
            {
                TActividadCabeceraDiaSemana entidad = base.FirstById(id);
                ActividadCabeceraDiaSemanaBO objetoBO = new ActividadCabeceraDiaSemanaBO();
                Mapper.Map<TActividadCabeceraDiaSemana, ActividadCabeceraDiaSemanaBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public ActividadCabeceraDiaSemanaBO FirstBy(Expression<Func<TActividadCabeceraDiaSemana, bool>> filter)
        {
            try
            {
                TActividadCabeceraDiaSemana entidad = base.FirstBy(filter);
                ActividadCabeceraDiaSemanaBO objetoBO = Mapper.Map<TActividadCabeceraDiaSemana, ActividadCabeceraDiaSemanaBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(ActividadCabeceraDiaSemanaBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TActividadCabeceraDiaSemana entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<ActividadCabeceraDiaSemanaBO> listadoBO)
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

        public bool Update(ActividadCabeceraDiaSemanaBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TActividadCabeceraDiaSemana entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<ActividadCabeceraDiaSemanaBO> listadoBO)
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
        private void AsignacionId(TActividadCabeceraDiaSemana entidad, ActividadCabeceraDiaSemanaBO objetoBO)
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

        private TActividadCabeceraDiaSemana MapeoEntidad(ActividadCabeceraDiaSemanaBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TActividadCabeceraDiaSemana entidad = new TActividadCabeceraDiaSemana();
                entidad = Mapper.Map<ActividadCabeceraDiaSemanaBO, TActividadCabeceraDiaSemana>(objetoBO,
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

        public List<int> ObtenerDiaSemanaActividad(int IdActividadCabecera)
        {
            try
            {
                List<int> resultado = new List<int>();
                var respuesta = GetBy(w => w.Estado == true && w.IdActividadCabecera == IdActividadCabecera,y=> new { y.IdDiaSemana}).ToList();

                if (respuesta.Count != 0)
                {
                    resultado = respuesta.Select(w => Convert.ToInt32(w.IdDiaSemana)).ToList();                
                }

                return resultado;
                
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
