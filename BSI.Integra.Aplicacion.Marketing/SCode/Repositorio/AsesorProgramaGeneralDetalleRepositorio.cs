using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using AutoMapper;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Marketing.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;

namespace BSI.Integra.Aplicacion.Marketing.Repositorio
{
    public class AsesorProgramaGeneralDetalleRepositorio : BaseRepository<TAsesorProgramaGeneralDetalle, AsesorProgramaGeneralDetalleBO>
    {
        #region Metodos Base
        public AsesorProgramaGeneralDetalleRepositorio() : base()
        {
        }
        public AsesorProgramaGeneralDetalleRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<AsesorProgramaGeneralDetalleBO> GetBy(Expression<Func<TAsesorProgramaGeneralDetalle, bool>> filter)
        {
            IEnumerable<TAsesorProgramaGeneralDetalle> listado = base.GetBy(filter);
            List<AsesorProgramaGeneralDetalleBO> listadoBO = new List<AsesorProgramaGeneralDetalleBO>();
            foreach (var itemEntidad in listado)
            {
                AsesorProgramaGeneralDetalleBO objetoBO = Mapper.Map<TAsesorProgramaGeneralDetalle, AsesorProgramaGeneralDetalleBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public AsesorProgramaGeneralDetalleBO FirstById(int id)
        {
            try
            {
                TAsesorProgramaGeneralDetalle entidad = base.FirstById(id);
                AsesorProgramaGeneralDetalleBO objetoBO = new AsesorProgramaGeneralDetalleBO();
                Mapper.Map<TAsesorProgramaGeneralDetalle, AsesorProgramaGeneralDetalleBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public AsesorProgramaGeneralDetalleBO FirstBy(Expression<Func<TAsesorProgramaGeneralDetalle, bool>> filter)
        {
            try
            {
                TAsesorProgramaGeneralDetalle entidad = base.FirstBy(filter);
                AsesorProgramaGeneralDetalleBO objetoBO = Mapper.Map<TAsesorProgramaGeneralDetalle, AsesorProgramaGeneralDetalleBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(AsesorProgramaGeneralDetalleBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TAsesorProgramaGeneralDetalle entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<AsesorProgramaGeneralDetalleBO> listadoBO)
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

        public bool Update(AsesorProgramaGeneralDetalleBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TAsesorProgramaGeneralDetalle entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<AsesorProgramaGeneralDetalleBO> listadoBO)
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
        private void AsignacionId(TAsesorProgramaGeneralDetalle entidad, AsesorProgramaGeneralDetalleBO objetoBO)
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

        private TAsesorProgramaGeneralDetalle MapeoEntidad(AsesorProgramaGeneralDetalleBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TAsesorProgramaGeneralDetalle entidad = new TAsesorProgramaGeneralDetalle();
                entidad = Mapper.Map<AsesorProgramaGeneralDetalleBO, TAsesorProgramaGeneralDetalle>(objetoBO,
                    opt => opt.ConfigureMap(MemberList.None));

                //mapea los hijos

                return entidad;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public IEnumerable<AsesorProgramaGeneralDetalleBO> GetFiltered<KProperty>(IEnumerable<Expression<Func<TAsesorProgramaGeneralDetalle, bool>>> filters, Expression<Func<TAsesorProgramaGeneralDetalle, KProperty>> orderBy, bool ascending)
        {
            IEnumerable<TAsesorProgramaGeneralDetalle> listado = base.GetFiltered(filters, orderBy, ascending);
            List<AsesorProgramaGeneralDetalleBO> listadoBO = new List<AsesorProgramaGeneralDetalleBO>();

            foreach (var itemEntidad in listado)
            {
                AsesorProgramaGeneralDetalleBO objetoBO = Mapper.Map<TAsesorProgramaGeneralDetalle, AsesorProgramaGeneralDetalleBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }
            return listadoBO;
        }
        #endregion


    }
}
