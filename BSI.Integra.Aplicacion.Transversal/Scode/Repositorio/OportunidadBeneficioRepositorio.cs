using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using AutoMapper;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;

namespace BSI.Integra.Aplicacion.Transversal.Repositorio
{
    public class OportunidadBeneficioRepositorio : BaseRepository<TOportunidadBeneficio, OportunidadBeneficioBO>
    {
        #region Metodos Base
        public OportunidadBeneficioRepositorio() : base()
        {
        }
        public OportunidadBeneficioRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<OportunidadBeneficioBO> GetBy(Expression<Func<TOportunidadBeneficio, bool>> filter)
        {
            IEnumerable<TOportunidadBeneficio> listado = base.GetBy(filter).ToList();
            List<OportunidadBeneficioBO> listadoBO = new List<OportunidadBeneficioBO>();
            foreach (var itemEntidad in listado)
            {
                OportunidadBeneficioBO objetoBO = Mapper.Map<TOportunidadBeneficio, OportunidadBeneficioBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public OportunidadBeneficioBO FirstById(int id)
        {
            try
            {
                TOportunidadBeneficio entidad = base.FirstById(id);
                OportunidadBeneficioBO objetoBO = new OportunidadBeneficioBO();
                Mapper.Map<TOportunidadBeneficio, OportunidadBeneficioBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public OportunidadBeneficioBO FirstBy(Expression<Func<TOportunidadBeneficio, bool>> filter)
        {
            try
            {
                TOportunidadBeneficio entidad = base.FirstBy(filter);
                OportunidadBeneficioBO objetoBO = Mapper.Map<TOportunidadBeneficio, OportunidadBeneficioBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(OportunidadBeneficioBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TOportunidadBeneficio entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<OportunidadBeneficioBO> listadoBO)
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

        public bool Update(OportunidadBeneficioBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TOportunidadBeneficio entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<OportunidadBeneficioBO> listadoBO)
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
        private void AsignacionId(TOportunidadBeneficio entidad, OportunidadBeneficioBO objetoBO)
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

        private TOportunidadBeneficio MapeoEntidad(OportunidadBeneficioBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TOportunidadBeneficio entidad = new TOportunidadBeneficio();
                entidad = Mapper.Map<OportunidadBeneficioBO, TOportunidadBeneficio>(objetoBO,
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
