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
    public class OportunidadPrerequisitoEspecificoRepositorio : BaseRepository<TOportunidadPrerequisitoEspecifico, OportunidadPrerequisitoEspecificoBO>
    {
        #region Metodos Base
        public OportunidadPrerequisitoEspecificoRepositorio() : base()
        {
        }
        public OportunidadPrerequisitoEspecificoRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<OportunidadPrerequisitoEspecificoBO> GetBy(Expression<Func<TOportunidadPrerequisitoEspecifico, bool>> filter)
        {
            IEnumerable<TOportunidadPrerequisitoEspecifico> listado = base.GetBy(filter);
            List<OportunidadPrerequisitoEspecificoBO> listadoBO = new List<OportunidadPrerequisitoEspecificoBO>();
            foreach (var itemEntidad in listado)
            {
                OportunidadPrerequisitoEspecificoBO objetoBO = Mapper.Map<TOportunidadPrerequisitoEspecifico, OportunidadPrerequisitoEspecificoBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public OportunidadPrerequisitoEspecificoBO FirstById(int id)
        {
            try
            {
                TOportunidadPrerequisitoEspecifico entidad = base.FirstById(id);
                OportunidadPrerequisitoEspecificoBO objetoBO = new OportunidadPrerequisitoEspecificoBO();
                Mapper.Map<TOportunidadPrerequisitoEspecifico, OportunidadPrerequisitoEspecificoBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public OportunidadPrerequisitoEspecificoBO FirstBy(Expression<Func<TOportunidadPrerequisitoEspecifico, bool>> filter)
        {
            try
            {
                TOportunidadPrerequisitoEspecifico entidad = base.FirstBy(filter);
                OportunidadPrerequisitoEspecificoBO objetoBO = Mapper.Map<TOportunidadPrerequisitoEspecifico, OportunidadPrerequisitoEspecificoBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(OportunidadPrerequisitoEspecificoBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TOportunidadPrerequisitoEspecifico entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<OportunidadPrerequisitoEspecificoBO> listadoBO)
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

        public bool Update(OportunidadPrerequisitoEspecificoBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TOportunidadPrerequisitoEspecifico entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<OportunidadPrerequisitoEspecificoBO> listadoBO)
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
        private void AsignacionId(TOportunidadPrerequisitoEspecifico entidad, OportunidadPrerequisitoEspecificoBO objetoBO)
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

        private TOportunidadPrerequisitoEspecifico MapeoEntidad(OportunidadPrerequisitoEspecificoBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TOportunidadPrerequisitoEspecifico entidad = new TOportunidadPrerequisitoEspecifico();
                entidad = Mapper.Map<OportunidadPrerequisitoEspecificoBO, TOportunidadPrerequisitoEspecifico>(objetoBO,
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
