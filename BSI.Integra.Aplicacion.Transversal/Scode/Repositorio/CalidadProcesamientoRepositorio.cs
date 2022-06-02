using BSI.Integra.Persistencia.Repository;
using System;
using System.Collections.Generic;
using System.Text;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Persistencia.Models;
using System.Linq.Expressions;
using AutoMapper;
using BSI.Integra.Persistencia.SCode.Repository;

namespace BSI.Integra.Aplicacion.Transversal.Repositorio
{
    public class CalidadProcesamientoRepositorio : BaseRepository<TCalidadProcesamiento, CalidadProcesamientoBO>
    {
        #region Metodos Base
        public CalidadProcesamientoRepositorio() : base()
        {
        }
        public CalidadProcesamientoRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<CalidadProcesamientoBO> GetBy(Expression<Func<TCalidadProcesamiento, bool>> filter)
        {
            IEnumerable<TCalidadProcesamiento> listado = base.GetBy(filter);
            List<CalidadProcesamientoBO> listadoBO = new List<CalidadProcesamientoBO>();
            foreach (var itemEntidad in listado)
            {
                CalidadProcesamientoBO objetoBO = Mapper.Map<TCalidadProcesamiento, CalidadProcesamientoBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public CalidadProcesamientoBO FirstById(int id)
        {
            try
            {
                TCalidadProcesamiento entidad = base.FirstById(id);
                CalidadProcesamientoBO objetoBO = new CalidadProcesamientoBO();
                Mapper.Map<TCalidadProcesamiento, CalidadProcesamientoBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public CalidadProcesamientoBO FirstBy(Expression<Func<TCalidadProcesamiento, bool>> filter)
        {
            try
            {
                TCalidadProcesamiento entidad = base.FirstBy(filter);
                CalidadProcesamientoBO objetoBO = Mapper.Map<TCalidadProcesamiento, CalidadProcesamientoBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(CalidadProcesamientoBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TCalidadProcesamiento entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<CalidadProcesamientoBO> listadoBO)
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

        public bool Update(CalidadProcesamientoBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TCalidadProcesamiento entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<CalidadProcesamientoBO> listadoBO)
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
        private void AsignacionId(TCalidadProcesamiento entidad, CalidadProcesamientoBO objetoBO)
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

        private TCalidadProcesamiento MapeoEntidad(CalidadProcesamientoBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TCalidadProcesamiento entidad = new TCalidadProcesamiento();
				entidad = Mapper.Map<CalidadProcesamientoBO, TCalidadProcesamiento>(objetoBO,
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
        public void EliminarOportunidadCompetidorDetalle(int idOportunidadCompetidor, string usuario )
        {
            try
            {
                _dapper.QuerySPDapper("com.SP_Oportunidad_EliminarOportunidadCompetidorDetalle", new { IdOportunidadCompetido = idOportunidadCompetidor, Usuario = usuario });

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
