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
    public class AsesorPaisDetalleRepositorio : BaseRepository<TAsesorPaisDetalle, AsesorPaisDetalleBO>
    {
        #region Metodos Base
        public AsesorPaisDetalleRepositorio() : base()
        {
        }
        public AsesorPaisDetalleRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<AsesorPaisDetalleBO> GetBy(Expression<Func<TAsesorPaisDetalle, bool>> filter)
        {
            IEnumerable<TAsesorPaisDetalle> listado = base.GetBy(filter);
            List<AsesorPaisDetalleBO> listadoBO = new List<AsesorPaisDetalleBO>();
            foreach (var itemEntidad in listado)
            {
                AsesorPaisDetalleBO objetoBO = Mapper.Map<TAsesorPaisDetalle, AsesorPaisDetalleBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public AsesorPaisDetalleBO FirstById(int id)
        {
            try
            {
                TAsesorPaisDetalle entidad = base.FirstById(id);
                AsesorPaisDetalleBO objetoBO = new AsesorPaisDetalleBO();
                Mapper.Map<TAsesorPaisDetalle, AsesorPaisDetalleBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public AsesorPaisDetalleBO FirstBy(Expression<Func<TAsesorPaisDetalle, bool>> filter)
        {
            try
            {
                TAsesorPaisDetalle entidad = base.FirstBy(filter);
                AsesorPaisDetalleBO objetoBO = Mapper.Map<TAsesorPaisDetalle, AsesorPaisDetalleBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(AsesorPaisDetalleBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TAsesorPaisDetalle entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<AsesorPaisDetalleBO> listadoBO)
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

        public bool Update(AsesorPaisDetalleBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TAsesorPaisDetalle entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<AsesorPaisDetalleBO> listadoBO)
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
        private void AsignacionId(TAsesorPaisDetalle entidad, AsesorPaisDetalleBO objetoBO)
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

        private TAsesorPaisDetalle MapeoEntidad(AsesorPaisDetalleBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TAsesorPaisDetalle entidad = new TAsesorPaisDetalle();
                entidad = Mapper.Map<AsesorPaisDetalleBO, TAsesorPaisDetalle>(objetoBO,
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
