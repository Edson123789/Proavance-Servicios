using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using AutoMapper;
using BSI.Integra.Aplicacion.Marketing.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;

namespace BSI.Integra.Aplicacion.Marketing.Repositorio
{
    public class AsesorProbabilidadDetalleRepositorio : BaseRepository<TAsesorProbabilidadDetalle, AsesorProbabilidadDetalleBO>
    {
        #region Metodos Base
        public AsesorProbabilidadDetalleRepositorio() : base()
        {
        }
        public AsesorProbabilidadDetalleRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<AsesorProbabilidadDetalleBO> GetBy(Expression<Func<TAsesorProbabilidadDetalle, bool>> filter)
        {
            IEnumerable<TAsesorProbabilidadDetalle> listado = base.GetBy(filter);
            List<AsesorProbabilidadDetalleBO> listadoBO = new List<AsesorProbabilidadDetalleBO>();
            foreach (var itemEntidad in listado)
            {
                AsesorProbabilidadDetalleBO objetoBO = Mapper.Map<TAsesorProbabilidadDetalle, AsesorProbabilidadDetalleBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public AsesorProbabilidadDetalleBO FirstById(int id)
        {
            try
            {
                TAsesorProbabilidadDetalle entidad = base.FirstById(id);
                AsesorProbabilidadDetalleBO objetoBO = new AsesorProbabilidadDetalleBO();
                Mapper.Map<TAsesorProbabilidadDetalle, AsesorProbabilidadDetalleBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public AsesorProbabilidadDetalleBO FirstBy(Expression<Func<TAsesorProbabilidadDetalle, bool>> filter)
        {
            try
            {
                TAsesorProbabilidadDetalle entidad = base.FirstBy(filter);
                AsesorProbabilidadDetalleBO objetoBO = Mapper.Map<TAsesorProbabilidadDetalle, AsesorProbabilidadDetalleBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(AsesorProbabilidadDetalleBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TAsesorProbabilidadDetalle entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<AsesorProbabilidadDetalleBO> listadoBO)
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

        public bool Update(AsesorProbabilidadDetalleBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TAsesorProbabilidadDetalle entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<AsesorProbabilidadDetalleBO> listadoBO)
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
        private void AsignacionId(TAsesorProbabilidadDetalle entidad, AsesorProbabilidadDetalleBO objetoBO)
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

        private TAsesorProbabilidadDetalle MapeoEntidad(AsesorProbabilidadDetalleBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TAsesorProbabilidadDetalle entidad = new TAsesorProbabilidadDetalle();
                entidad = Mapper.Map<AsesorProbabilidadDetalleBO, TAsesorProbabilidadDetalle>(objetoBO,
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

