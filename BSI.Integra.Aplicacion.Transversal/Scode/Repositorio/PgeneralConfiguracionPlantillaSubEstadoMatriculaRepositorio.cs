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
    public class PgeneralConfiguracionPlantillaSubEstadoMatriculaRepositorio : BaseRepository<TPgeneralConfiguracionPlantillaSubEstadoMatricula, PgeneralConfiguracionPlantillaSubEstadoMatriculaBO>
    {
        #region Metodos Base
        public PgeneralConfiguracionPlantillaSubEstadoMatriculaRepositorio() : base()
        {
        }
        public PgeneralConfiguracionPlantillaSubEstadoMatriculaRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<PgeneralConfiguracionPlantillaSubEstadoMatriculaBO> GetBy(Expression<Func<TPgeneralConfiguracionPlantillaSubEstadoMatricula, bool>> filter)
        {
            IEnumerable<TPgeneralConfiguracionPlantillaSubEstadoMatricula> listado = base.GetBy(filter);
            List<PgeneralConfiguracionPlantillaSubEstadoMatriculaBO> listadoBO = new List<PgeneralConfiguracionPlantillaSubEstadoMatriculaBO>();
            foreach (var itemEntidad in listado)
            {
                PgeneralConfiguracionPlantillaSubEstadoMatriculaBO objetoBO = Mapper.Map<TPgeneralConfiguracionPlantillaSubEstadoMatricula, PgeneralConfiguracionPlantillaSubEstadoMatriculaBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public PgeneralConfiguracionPlantillaSubEstadoMatriculaBO FirstById(int id)
        {
            try
            {
                TPgeneralConfiguracionPlantillaSubEstadoMatricula entidad = base.FirstById(id);
                PgeneralConfiguracionPlantillaSubEstadoMatriculaBO objetoBO = new PgeneralConfiguracionPlantillaSubEstadoMatriculaBO();
                Mapper.Map<TPgeneralConfiguracionPlantillaSubEstadoMatricula, PgeneralConfiguracionPlantillaSubEstadoMatriculaBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public PgeneralConfiguracionPlantillaSubEstadoMatriculaBO FirstBy(Expression<Func<TPgeneralConfiguracionPlantillaSubEstadoMatricula, bool>> filter)
        {
            try
            {
                TPgeneralConfiguracionPlantillaSubEstadoMatricula entidad = base.FirstBy(filter);
                PgeneralConfiguracionPlantillaSubEstadoMatriculaBO objetoBO = Mapper.Map<TPgeneralConfiguracionPlantillaSubEstadoMatricula, PgeneralConfiguracionPlantillaSubEstadoMatriculaBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(PgeneralConfiguracionPlantillaSubEstadoMatriculaBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TPgeneralConfiguracionPlantillaSubEstadoMatricula entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<PgeneralConfiguracionPlantillaSubEstadoMatriculaBO> listadoBO)
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

        public bool Update(PgeneralConfiguracionPlantillaSubEstadoMatriculaBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TPgeneralConfiguracionPlantillaSubEstadoMatricula entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<PgeneralConfiguracionPlantillaSubEstadoMatriculaBO> listadoBO)
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
        private void AsignacionId(TPgeneralConfiguracionPlantillaSubEstadoMatricula entidad, PgeneralConfiguracionPlantillaSubEstadoMatriculaBO objetoBO)
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

        private TPgeneralConfiguracionPlantillaSubEstadoMatricula MapeoEntidad(PgeneralConfiguracionPlantillaSubEstadoMatriculaBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TPgeneralConfiguracionPlantillaSubEstadoMatricula entidad = new TPgeneralConfiguracionPlantillaSubEstadoMatricula();
                entidad = Mapper.Map<PgeneralConfiguracionPlantillaSubEstadoMatriculaBO, TPgeneralConfiguracionPlantillaSubEstadoMatricula>(objetoBO,
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
