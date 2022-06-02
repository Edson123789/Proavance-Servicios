using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using AutoMapper;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;

namespace BSI.Integra.Aplicacion.Transversal.Repositorio
{
    public class OportunidadPrerequisitoGeneralRepositorio : BaseRepository<TOportunidadPrerequisitoGeneral, OportunidadPrerequisitoGeneralBO>
    {
        #region Metodos Base
        public OportunidadPrerequisitoGeneralRepositorio() : base()
        {
        }
        public OportunidadPrerequisitoGeneralRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<OportunidadPrerequisitoGeneralBO> GetBy(Expression<Func<TOportunidadPrerequisitoGeneral, bool>> filter)
        {
            IEnumerable<TOportunidadPrerequisitoGeneral> listado = base.GetBy(filter);
            List<OportunidadPrerequisitoGeneralBO> listadoBO = new List<OportunidadPrerequisitoGeneralBO>();
            foreach (var itemEntidad in listado)
            {
                OportunidadPrerequisitoGeneralBO objetoBO = Mapper.Map<TOportunidadPrerequisitoGeneral, OportunidadPrerequisitoGeneralBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public OportunidadPrerequisitoGeneralBO FirstById(int id)
        {
            try
            {
                TOportunidadPrerequisitoGeneral entidad = base.FirstById(id);
                OportunidadPrerequisitoGeneralBO objetoBO = new OportunidadPrerequisitoGeneralBO();
                Mapper.Map<TOportunidadPrerequisitoGeneral, OportunidadPrerequisitoGeneralBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public OportunidadPrerequisitoGeneralBO FirstBy(Expression<Func<TOportunidadPrerequisitoGeneral, bool>> filter)
        {
            try
            {
                TOportunidadPrerequisitoGeneral entidad = base.FirstBy(filter);
                OportunidadPrerequisitoGeneralBO objetoBO = Mapper.Map<TOportunidadPrerequisitoGeneral, OportunidadPrerequisitoGeneralBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(OportunidadPrerequisitoGeneralBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TOportunidadPrerequisitoGeneral entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<OportunidadPrerequisitoGeneralBO> listadoBO)
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

        public bool Update(OportunidadPrerequisitoGeneralBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TOportunidadPrerequisitoGeneral entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<OportunidadPrerequisitoGeneralBO> listadoBO)
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
        private void AsignacionId(TOportunidadPrerequisitoGeneral entidad, OportunidadPrerequisitoGeneralBO objetoBO)
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

        private TOportunidadPrerequisitoGeneral MapeoEntidad(OportunidadPrerequisitoGeneralBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TOportunidadPrerequisitoGeneral entidad = new TOportunidadPrerequisitoGeneral();
                entidad = Mapper.Map<OportunidadPrerequisitoGeneralBO, TOportunidadPrerequisitoGeneral>(objetoBO,
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
