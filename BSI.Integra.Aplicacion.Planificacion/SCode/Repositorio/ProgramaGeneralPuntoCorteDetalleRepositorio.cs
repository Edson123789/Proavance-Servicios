using AutoMapper;
using BSI.Integra.Aplicacion.Planificacion.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace BSI.Integra.Aplicacion.Planificacion.Repositorio
{
    public class ProgramaGeneralPuntoCorteDetalleRepositorio : BaseRepository<TProgramaGeneralPuntoCorteDetalle, ProgramaGeneralPuntoCorteDetalleBO>
    {

        #region Metodos Base
        public ProgramaGeneralPuntoCorteDetalleRepositorio() : base()
        {
        }
        public ProgramaGeneralPuntoCorteDetalleRepositorio(integraDBContext contexto) : base(contexto)
        {
        }

        public IEnumerable<ProgramaGeneralPuntoCorteDetalleBO> GetBy(Expression<Func<TProgramaGeneralPuntoCorteDetalle, bool>> filter)
        {
            IEnumerable<TProgramaGeneralPuntoCorteDetalle> listado = base.GetBy(filter);
            List<ProgramaGeneralPuntoCorteDetalleBO> listadoBO = new List<ProgramaGeneralPuntoCorteDetalleBO>();
            foreach (var itemEntidad in listado)
            {
                ProgramaGeneralPuntoCorteDetalleBO objetoBO = Mapper.Map<TProgramaGeneralPuntoCorteDetalle, ProgramaGeneralPuntoCorteDetalleBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public ProgramaGeneralPuntoCorteDetalleBO FirstById(int id)
        {
            try
            {
                TProgramaGeneralPuntoCorteDetalle entidad = base.FirstById(id);
                ProgramaGeneralPuntoCorteDetalleBO objetoBO = new ProgramaGeneralPuntoCorteDetalleBO();
                Mapper.Map<TProgramaGeneralPuntoCorteDetalle, ProgramaGeneralPuntoCorteDetalleBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public ProgramaGeneralPuntoCorteDetalleBO FirstBy(Expression<Func<TProgramaGeneralPuntoCorteDetalle, bool>> filter)
        {
            try
            {
                TProgramaGeneralPuntoCorteDetalle entidad = base.FirstBy(filter);
                ProgramaGeneralPuntoCorteDetalleBO objetoBO = Mapper.Map<TProgramaGeneralPuntoCorteDetalle, ProgramaGeneralPuntoCorteDetalleBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(ProgramaGeneralPuntoCorteDetalleBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TProgramaGeneralPuntoCorteDetalle entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<ProgramaGeneralPuntoCorteDetalleBO> listadoBO)
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

        public bool Update(ProgramaGeneralPuntoCorteDetalleBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TProgramaGeneralPuntoCorteDetalle entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<ProgramaGeneralPuntoCorteDetalleBO> listadoBO)
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
        private void AsignacionId(TProgramaGeneralPuntoCorteDetalle entidad, ProgramaGeneralPuntoCorteDetalleBO objetoBO)
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

        private TProgramaGeneralPuntoCorteDetalle MapeoEntidad(ProgramaGeneralPuntoCorteDetalleBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TProgramaGeneralPuntoCorteDetalle entidad = new TProgramaGeneralPuntoCorteDetalle();
                entidad = Mapper.Map<ProgramaGeneralPuntoCorteDetalleBO, TProgramaGeneralPuntoCorteDetalle>(objetoBO,
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
