using AutoMapper;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace BSI.Integra.Aplicacion.Transversal.Repositorio
{
    public class PgeneralConfiguracionPlantillaDetalleRepositorio : BaseRepository<TPgeneralConfiguracionPlantillaDetalle, PgeneralConfiguracionPlantillaDetalleBO>
    {
        #region Metodos Base
        public PgeneralConfiguracionPlantillaDetalleRepositorio() : base()
        {
        }
        public PgeneralConfiguracionPlantillaDetalleRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<PgeneralConfiguracionPlantillaDetalleBO> GetBy(Expression<Func<TPgeneralConfiguracionPlantillaDetalle, bool>> filter)
        {
            IEnumerable<TPgeneralConfiguracionPlantillaDetalle> listado = base.GetBy(filter);
            List<PgeneralConfiguracionPlantillaDetalleBO> listadoBO = new List<PgeneralConfiguracionPlantillaDetalleBO>();
            foreach (var itemEntidad in listado)
            {
                PgeneralConfiguracionPlantillaDetalleBO objetoBO = Mapper.Map<TPgeneralConfiguracionPlantillaDetalle, PgeneralConfiguracionPlantillaDetalleBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public PgeneralConfiguracionPlantillaDetalleBO FirstById(int id)
        {
            try
            {
                TPgeneralConfiguracionPlantillaDetalle entidad = base.FirstById(id);
                PgeneralConfiguracionPlantillaDetalleBO objetoBO = new PgeneralConfiguracionPlantillaDetalleBO();
                Mapper.Map<TPgeneralConfiguracionPlantillaDetalle, PgeneralConfiguracionPlantillaDetalleBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public PgeneralConfiguracionPlantillaDetalleBO FirstBy(Expression<Func<TPgeneralConfiguracionPlantillaDetalle, bool>> filter)
        {
            try
            {
                TPgeneralConfiguracionPlantillaDetalle entidad = base.FirstBy(filter);
                PgeneralConfiguracionPlantillaDetalleBO objetoBO = Mapper.Map<TPgeneralConfiguracionPlantillaDetalle, PgeneralConfiguracionPlantillaDetalleBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(PgeneralConfiguracionPlantillaDetalleBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TPgeneralConfiguracionPlantillaDetalle entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<PgeneralConfiguracionPlantillaDetalleBO> listadoBO)
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

        public bool Update(PgeneralConfiguracionPlantillaDetalleBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TPgeneralConfiguracionPlantillaDetalle entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<PgeneralConfiguracionPlantillaDetalleBO> listadoBO)
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
        private void AsignacionId(TPgeneralConfiguracionPlantillaDetalle entidad, PgeneralConfiguracionPlantillaDetalleBO objetoBO)
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

        private TPgeneralConfiguracionPlantillaDetalle MapeoEntidad(PgeneralConfiguracionPlantillaDetalleBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TPgeneralConfiguracionPlantillaDetalle entidad = new TPgeneralConfiguracionPlantillaDetalle();
                entidad = Mapper.Map<PgeneralConfiguracionPlantillaDetalleBO, TPgeneralConfiguracionPlantillaDetalle>(objetoBO,
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
