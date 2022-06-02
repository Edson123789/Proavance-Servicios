using AutoMapper;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace BSI.Integra.Aplicacion.Transversal.Repositorio
{
    public class ProgramaGeneralCertificacionArgumentoRepositorio : BaseRepository<TProgramaGeneralCertificacionArgumento, ProgramaGeneralCertificacionArgumentoBO>
    {
        #region Metodos Base
        public ProgramaGeneralCertificacionArgumentoRepositorio() : base()
        {
        }
        public ProgramaGeneralCertificacionArgumentoRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<ProgramaGeneralCertificacionArgumentoBO> GetBy(Expression<Func<TProgramaGeneralCertificacionArgumento, bool>> filter)
        {
            IEnumerable<TProgramaGeneralCertificacionArgumento> listado = base.GetBy(filter);
            List<ProgramaGeneralCertificacionArgumentoBO> listadoBO = new List<ProgramaGeneralCertificacionArgumentoBO>();
            foreach (var itemEntidad in listado)
            {
                ProgramaGeneralCertificacionArgumentoBO objetoBO = Mapper.Map<TProgramaGeneralCertificacionArgumento, ProgramaGeneralCertificacionArgumentoBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public ProgramaGeneralCertificacionArgumentoBO FirstById(int id)
        {
            try
            {
                TProgramaGeneralCertificacionArgumento entidad = base.FirstById(id);
                ProgramaGeneralCertificacionArgumentoBO objetoBO = new ProgramaGeneralCertificacionArgumentoBO();
                Mapper.Map<TProgramaGeneralCertificacionArgumento, ProgramaGeneralCertificacionArgumentoBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public ProgramaGeneralCertificacionArgumentoBO FirstBy(Expression<Func<TProgramaGeneralCertificacionArgumento, bool>> filter)
        {
            try
            {
                TProgramaGeneralCertificacionArgumento entidad = base.FirstBy(filter);
                ProgramaGeneralCertificacionArgumentoBO objetoBO = Mapper.Map<TProgramaGeneralCertificacionArgumento, ProgramaGeneralCertificacionArgumentoBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(ProgramaGeneralCertificacionArgumentoBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TProgramaGeneralCertificacionArgumento entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<ProgramaGeneralCertificacionArgumentoBO> listadoBO)
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

        public bool Update(ProgramaGeneralCertificacionArgumentoBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TProgramaGeneralCertificacionArgumento entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<ProgramaGeneralCertificacionArgumentoBO> listadoBO)
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
        private void AsignacionId(TProgramaGeneralCertificacionArgumento entidad, ProgramaGeneralCertificacionArgumentoBO objetoBO)
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

        private TProgramaGeneralCertificacionArgumento MapeoEntidad(ProgramaGeneralCertificacionArgumentoBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TProgramaGeneralCertificacionArgumento entidad = new TProgramaGeneralCertificacionArgumento();
                entidad = Mapper.Map<ProgramaGeneralCertificacionArgumentoBO, TProgramaGeneralCertificacionArgumento>(objetoBO,
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
        /// Elimina (Actualiza estado a false ) todos los Argumentos asociados a un programa y Beneficio segun un beneficio Padre
        /// </summary>
        /// <returns></returns>
        public void EliminacionLogicoPorCertificacion(int idCertificacion, string usuario, List<CertificacionArgumentoDTO> nuevos)
        {
            try
            {
                var listaBorrar = GetBy(x => x.IdProgramaGeneralCertificacion == idCertificacion && x.Estado == true).ToList();
                listaBorrar.RemoveAll(x => nuevos.Any(y => y.Id == x.Id));
                foreach (var item in listaBorrar)
                {
                    Delete(item.Id, usuario);
                }

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// <summary>
        /// Elimina (Actualiza estado a false ) todos los Argumentos asociados a un programa y Beneficio segun un beneficio Padre
        /// </summary>
        /// <returns></returns>
        public void EliminacionLogicoPorIdCertificacion(int idCertificacion, string usuario)
        {
            try
            {
                var listaBorrar = GetBy(x => x.IdProgramaGeneralCertificacion == idCertificacion && x.Estado == true).ToList();
                foreach (var item in listaBorrar)
                {
                    Delete(item.Id, usuario);
                }

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
