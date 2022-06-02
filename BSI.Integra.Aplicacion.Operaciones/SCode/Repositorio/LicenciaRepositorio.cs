using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using AutoMapper;
using BSI.Integra.Aplicacion.Operaciones.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;

namespace BSI.Integra.Aplicacion.Operaciones.Repositorio
{
    public class LicenciaRepositorio : BaseRepository<TLicencia, LicenciaBO>
    {
        #region Metodos Base
        public LicenciaRepositorio() : base()
        {
        }
        public LicenciaRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<LicenciaBO> GetBy(Expression<Func<TLicencia, bool>> filter)
        {
            IEnumerable<TLicencia> listado = base.GetBy(filter);
            List<LicenciaBO> listadoBO = new List<LicenciaBO>();
            foreach (var itemEntidad in listado)
            {
                LicenciaBO objetoBO = Mapper.Map<TLicencia, LicenciaBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public LicenciaBO FirstById(int id)
        {
            try
            {
                TLicencia entidad = base.FirstById(id);
                LicenciaBO objetoBO = new LicenciaBO();
                Mapper.Map<TLicencia, LicenciaBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public LicenciaBO FirstBy(Expression<Func<TLicencia, bool>> filter)
        {
            try
            {
                TLicencia entidad = base.FirstBy(filter);
                LicenciaBO objetoBO = Mapper.Map<TLicencia, LicenciaBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(LicenciaBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TLicencia entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<LicenciaBO> listadoBO)
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

        public bool Update(LicenciaBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TLicencia entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<LicenciaBO> listadoBO)
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
        private void AsignacionId(TLicencia entidad, LicenciaBO objetoBO)
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

        private TLicencia MapeoEntidad(LicenciaBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TLicencia entidad = new TLicencia();
                entidad = Mapper.Map<LicenciaBO, TLicencia>(objetoBO,
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

        /// <summary>
        /// Obtiene una licencia por nombre
        /// </summary>
        /// <param name="nombre"></param>
        /// <returns></returns>
        public LicenciaBO ObtenerPorNombre(string nombre) {
            try
            {
                return this.FirstBy(x => x.Producto == nombre);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
