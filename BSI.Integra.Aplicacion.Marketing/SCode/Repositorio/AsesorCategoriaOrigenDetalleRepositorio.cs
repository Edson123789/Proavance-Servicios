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
    public class AsesorCategoriaOrigenDetalleRepositorio : BaseRepository<TAsesorCategoriaOrigenDetalle, AsesorCategoriaOrigenDetalleBO>
    {
        #region Metodos Base
        public AsesorCategoriaOrigenDetalleRepositorio() : base()
        {
        }
        public AsesorCategoriaOrigenDetalleRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<AsesorCategoriaOrigenDetalleBO> GetBy(Expression<Func<TAsesorCategoriaOrigenDetalle, bool>> filter)
        {
            IEnumerable<TAsesorCategoriaOrigenDetalle> listado = base.GetBy(filter);
            List<AsesorCategoriaOrigenDetalleBO> listadoBO = new List<AsesorCategoriaOrigenDetalleBO>();
            foreach (var itemEntidad in listado)
            {
                AsesorCategoriaOrigenDetalleBO objetoBO = Mapper.Map<TAsesorCategoriaOrigenDetalle, AsesorCategoriaOrigenDetalleBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public AsesorCategoriaOrigenDetalleBO FirstById(int id)
        {
            try
            {
                TAsesorCategoriaOrigenDetalle entidad = base.FirstById(id);
                AsesorCategoriaOrigenDetalleBO objetoBO = new AsesorCategoriaOrigenDetalleBO();
                Mapper.Map<TAsesorCategoriaOrigenDetalle, AsesorCategoriaOrigenDetalleBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public AsesorCategoriaOrigenDetalleBO FirstBy(Expression<Func<TAsesorCategoriaOrigenDetalle, bool>> filter)
        {
            try
            {
                TAsesorCategoriaOrigenDetalle entidad = base.FirstBy(filter);
                AsesorCategoriaOrigenDetalleBO objetoBO = Mapper.Map<TAsesorCategoriaOrigenDetalle, AsesorCategoriaOrigenDetalleBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(AsesorCategoriaOrigenDetalleBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TAsesorCategoriaOrigenDetalle entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<AsesorCategoriaOrigenDetalleBO> listadoBO)
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

        public bool Update(AsesorCategoriaOrigenDetalleBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TAsesorCategoriaOrigenDetalle entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<AsesorCategoriaOrigenDetalleBO> listadoBO)
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
        private void AsignacionId(TAsesorCategoriaOrigenDetalle entidad, AsesorCategoriaOrigenDetalleBO objetoBO)
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

        private TAsesorCategoriaOrigenDetalle MapeoEntidad(AsesorCategoriaOrigenDetalleBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TAsesorCategoriaOrigenDetalle entidad = new TAsesorCategoriaOrigenDetalle();
                entidad = Mapper.Map<AsesorCategoriaOrigenDetalleBO, TAsesorCategoriaOrigenDetalle>(objetoBO,
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
