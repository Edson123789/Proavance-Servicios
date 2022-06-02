using AutoMapper;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace BSI.Integra.Aplicacion.Transversal.Repositorio
{
    public class PgeneralConfiguracionPlantillaEstadoMatriculaRepositorio : BaseRepository<TPgeneralConfiguracionPlantillaEstadoMatricula, PgeneralConfiguracionPlantillaEstadoMatriculaBO>
    {
        #region Metodos Base
        public PgeneralConfiguracionPlantillaEstadoMatriculaRepositorio() : base()
        {
        }
        public PgeneralConfiguracionPlantillaEstadoMatriculaRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<PgeneralConfiguracionPlantillaEstadoMatriculaBO> GetBy(Expression<Func<TPgeneralConfiguracionPlantillaEstadoMatricula, bool>> filter)
        {
            IEnumerable<TPgeneralConfiguracionPlantillaEstadoMatricula> listado = base.GetBy(filter);
            List<PgeneralConfiguracionPlantillaEstadoMatriculaBO> listadoBO = new List<PgeneralConfiguracionPlantillaEstadoMatriculaBO>();
            foreach (var itemEntidad in listado)
            {
                PgeneralConfiguracionPlantillaEstadoMatriculaBO objetoBO = Mapper.Map<TPgeneralConfiguracionPlantillaEstadoMatricula, PgeneralConfiguracionPlantillaEstadoMatriculaBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public PgeneralConfiguracionPlantillaEstadoMatriculaBO FirstById(int id)
        {
            try
            {
                TPgeneralConfiguracionPlantillaEstadoMatricula entidad = base.FirstById(id);
                PgeneralConfiguracionPlantillaEstadoMatriculaBO objetoBO = new PgeneralConfiguracionPlantillaEstadoMatriculaBO();
                Mapper.Map<TPgeneralConfiguracionPlantillaEstadoMatricula, PgeneralConfiguracionPlantillaEstadoMatriculaBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public PgeneralConfiguracionPlantillaEstadoMatriculaBO FirstBy(Expression<Func<TPgeneralConfiguracionPlantillaEstadoMatricula, bool>> filter)
        {
            try
            {
                TPgeneralConfiguracionPlantillaEstadoMatricula entidad = base.FirstBy(filter);
                PgeneralConfiguracionPlantillaEstadoMatriculaBO objetoBO = Mapper.Map<TPgeneralConfiguracionPlantillaEstadoMatricula, PgeneralConfiguracionPlantillaEstadoMatriculaBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(PgeneralConfiguracionPlantillaEstadoMatriculaBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TPgeneralConfiguracionPlantillaEstadoMatricula entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<PgeneralConfiguracionPlantillaEstadoMatriculaBO> listadoBO)
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

        public bool Update(PgeneralConfiguracionPlantillaEstadoMatriculaBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TPgeneralConfiguracionPlantillaEstadoMatricula entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<PgeneralConfiguracionPlantillaEstadoMatriculaBO> listadoBO)
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
        private void AsignacionId(TPgeneralConfiguracionPlantillaEstadoMatricula entidad, PgeneralConfiguracionPlantillaEstadoMatriculaBO objetoBO)
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

        private TPgeneralConfiguracionPlantillaEstadoMatricula MapeoEntidad(PgeneralConfiguracionPlantillaEstadoMatriculaBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TPgeneralConfiguracionPlantillaEstadoMatricula entidad = new TPgeneralConfiguracionPlantillaEstadoMatricula();
                entidad = Mapper.Map<PgeneralConfiguracionPlantillaEstadoMatriculaBO, TPgeneralConfiguracionPlantillaEstadoMatricula>(objetoBO,
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
