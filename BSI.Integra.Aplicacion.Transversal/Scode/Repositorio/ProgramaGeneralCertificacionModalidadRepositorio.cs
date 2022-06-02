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
    public class ProgramaGeneralCertificacionModalidadRepositorio : BaseRepository<TProgramaGeneralCertificacionModalidad, ProgramaGeneralCertificacionModalidadBO>
    {
        #region Metodos Base
        public ProgramaGeneralCertificacionModalidadRepositorio() : base()
        {
        }
        public ProgramaGeneralCertificacionModalidadRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<ProgramaGeneralCertificacionModalidadBO> GetBy(Expression<Func<TProgramaGeneralCertificacionModalidad, bool>> filter)
        {
            IEnumerable<TProgramaGeneralCertificacionModalidad> listado = base.GetBy(filter);
            List<ProgramaGeneralCertificacionModalidadBO> listadoBO = new List<ProgramaGeneralCertificacionModalidadBO>();
            foreach (var itemEntidad in listado)
            {
                ProgramaGeneralCertificacionModalidadBO objetoBO = Mapper.Map<TProgramaGeneralCertificacionModalidad, ProgramaGeneralCertificacionModalidadBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public ProgramaGeneralCertificacionModalidadBO FirstById(int id)
        {
            try
            {
                TProgramaGeneralCertificacionModalidad entidad = base.FirstById(id);
                ProgramaGeneralCertificacionModalidadBO objetoBO = new ProgramaGeneralCertificacionModalidadBO();
                Mapper.Map<TProgramaGeneralCertificacionModalidad, ProgramaGeneralCertificacionModalidadBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public ProgramaGeneralCertificacionModalidadBO FirstBy(Expression<Func<TProgramaGeneralCertificacionModalidad, bool>> filter)
        {
            try
            {
                TProgramaGeneralCertificacionModalidad entidad = base.FirstBy(filter);
                ProgramaGeneralCertificacionModalidadBO objetoBO = Mapper.Map<TProgramaGeneralCertificacionModalidad, ProgramaGeneralCertificacionModalidadBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(ProgramaGeneralCertificacionModalidadBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TProgramaGeneralCertificacionModalidad entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<ProgramaGeneralCertificacionModalidadBO> listadoBO)
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

        public bool Update(ProgramaGeneralCertificacionModalidadBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TProgramaGeneralCertificacionModalidad entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<ProgramaGeneralCertificacionModalidadBO> listadoBO)
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
        private void AsignacionId(TProgramaGeneralCertificacionModalidad entidad, ProgramaGeneralCertificacionModalidadBO objetoBO)
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

        private TProgramaGeneralCertificacionModalidad MapeoEntidad(ProgramaGeneralCertificacionModalidadBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TProgramaGeneralCertificacionModalidad entidad = new TProgramaGeneralCertificacionModalidad();
                entidad = Mapper.Map<ProgramaGeneralCertificacionModalidadBO, TProgramaGeneralCertificacionModalidad>(objetoBO,
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
        /// Elimina (Actualiza estado a false ) todas las modalidades asociados a un programa y Beneficio segun un beneficio Padre
        /// </summary>
        /// <returns></returns>
        public void EliminacionLogicoPorCertificacion(int idCertificacion, string usuario, List<ModalidadCursoDTO> nuevos)
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
        /// Elimina (Actualiza estado a false ) todas las modalidades asociados a un programa y Beneficio segun un beneficio Padre
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
