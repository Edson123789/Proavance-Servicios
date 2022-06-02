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
    public class DiaSemanaRepositorio : BaseRepository<TDiaSemana, DiaSemanaBO>
    {
        #region Metodos Base
        public DiaSemanaRepositorio() : base()
        {
        }
        public DiaSemanaRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<DiaSemanaBO> GetBy(Expression<Func<TDiaSemana, bool>> filter)
        {
            IEnumerable<TDiaSemana> listado = base.GetBy(filter);
            List<DiaSemanaBO> listadoBO = new List<DiaSemanaBO>();
            foreach (var itemEntidad in listado)
            {
                DiaSemanaBO objetoBO = Mapper.Map<TDiaSemana, DiaSemanaBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public DiaSemanaBO FirstById(int id)
        {
            try
            {
                TDiaSemana entidad = base.FirstById(id);
                DiaSemanaBO objetoBO = new DiaSemanaBO();
                Mapper.Map<TDiaSemana, DiaSemanaBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public DiaSemanaBO FirstBy(Expression<Func<TDiaSemana, bool>> filter)
        {
            try
            {
                TDiaSemana entidad = base.FirstBy(filter);
                DiaSemanaBO objetoBO = Mapper.Map<TDiaSemana, DiaSemanaBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(DiaSemanaBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TDiaSemana entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<DiaSemanaBO> listadoBO)
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

        public bool Update(DiaSemanaBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TDiaSemana entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<DiaSemanaBO> listadoBO)
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
        private void AsignacionId(TDiaSemana entidad, DiaSemanaBO objetoBO)
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

        private TDiaSemana MapeoEntidad(DiaSemanaBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TDiaSemana entidad = new TDiaSemana();
                entidad = Mapper.Map<DiaSemanaBO, TDiaSemana>(objetoBO,
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
        public List<FiltroDTO> ObtenerDiaSemana()
        {
            try
            {
                List<FiltroDTO> listaDiaSemana = this.GetBy(x => x.Estado == true, x => new FiltroDTO { Id = x.Id, Nombre = x.Nombre.ToUpper() }).ToList();
                return listaDiaSemana;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
