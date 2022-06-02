using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq.Expressions;
using System.Text;
using AutoMapper;
using BSI.Integra.Aplicacion.Planificacion.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;

namespace BSI.Integra.Aplicacion.Planificacion.Repositorio
{
    public class PEspecificoConsumoRepositorio : BaseRepository<TPespecificoConsumo, PEspecificoConsumoBO>
    {
        #region Metodos Base
        public PEspecificoConsumoRepositorio() : base()
        {
        }
        public PEspecificoConsumoRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<PEspecificoConsumoBO> GetBy(Expression<Func<TPespecificoConsumo, bool>> filter)
        {
            IEnumerable<TPespecificoConsumo> listado = base.GetBy(filter);
            List<PEspecificoConsumoBO> listadoBO = new List<PEspecificoConsumoBO>();
            foreach (var itemEntidad in listado)
            {
                PEspecificoConsumoBO objetoBO = Mapper.Map<TPespecificoConsumo, PEspecificoConsumoBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public PEspecificoConsumoBO FirstById(int id)
        {
            try
            {
                TPespecificoConsumo entidad = base.FirstById(id);
                PEspecificoConsumoBO objetoBO = new PEspecificoConsumoBO();
                Mapper.Map<TPespecificoConsumo, PEspecificoConsumoBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public PEspecificoConsumoBO FirstBy(Expression<Func<TPespecificoConsumo, bool>> filter)
        {
            try
            {
                TPespecificoConsumo entidad = base.FirstBy(filter);
                PEspecificoConsumoBO objetoBO = Mapper.Map<TPespecificoConsumo, PEspecificoConsumoBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(PEspecificoConsumoBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TPespecificoConsumo entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<PEspecificoConsumoBO> listadoBO)
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

        public bool Update(PEspecificoConsumoBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TPespecificoConsumo entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<PEspecificoConsumoBO> listadoBO)
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
        private void AsignacionId(TPespecificoConsumo entidad, PEspecificoConsumoBO objetoBO)
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

        private TPespecificoConsumo MapeoEntidad(PEspecificoConsumoBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TPespecificoConsumo entidad = new TPespecificoConsumo();
                entidad = Mapper.Map<PEspecificoConsumoBO, TPespecificoConsumo>(objetoBO,
                    opt => opt.ConfigureMap(MemberList.None));

                //mapea los hijos

                return entidad;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public IEnumerable<PEspecificoConsumoBO> GetFiltered<KProperty>(IEnumerable<Expression<Func<TPespecificoConsumo, bool>>> filters, Expression<Func<TPespecificoConsumo, KProperty>> orderBy, bool ascending)
        {
            IEnumerable<TPespecificoConsumo> listado = base.GetFiltered(filters, orderBy, ascending);
            List<PEspecificoConsumoBO> listadoBO = new List<PEspecificoConsumoBO>();

            foreach (var itemEntidad in listado)
            {
                PEspecificoConsumoBO objetoBO = Mapper.Map<TPespecificoConsumo, PEspecificoConsumoBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }
            return listadoBO;
        }
		#endregion
		/// <summary>
		/// Obtiene numero de semana de una fecha determinada
		/// </summary>
		/// <param name="date"></param>
		/// <returns></returns>
		public int ObtenerNumeroSemana(DateTime date)
		{
			CultureInfo ciCurr = CultureInfo.CurrentCulture;
			int weekNum = ciCurr.Calendar.GetWeekOfYear(date, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Sunday);
			return weekNum;
		}
	}
}
