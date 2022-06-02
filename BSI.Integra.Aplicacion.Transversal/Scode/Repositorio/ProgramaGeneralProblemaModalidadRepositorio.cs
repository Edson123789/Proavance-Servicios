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
    public class ProgramaGeneralProblemaModalidadRepositorio : BaseRepository<TProgramaGeneralProblemaModalidad, ProgramaGeneralProblemaModalidadBO>
    {
        #region Metodos Base
        public ProgramaGeneralProblemaModalidadRepositorio() : base()
        {
        }
        public ProgramaGeneralProblemaModalidadRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<ProgramaGeneralProblemaModalidadBO> GetBy(Expression<Func<TProgramaGeneralProblemaModalidad, bool>> filter)
        {
            IEnumerable<TProgramaGeneralProblemaModalidad> listado = base.GetBy(filter);
            List<ProgramaGeneralProblemaModalidadBO> listadoBO = new List<ProgramaGeneralProblemaModalidadBO>();
            foreach (var itemEntidad in listado)
            {
                ProgramaGeneralProblemaModalidadBO objetoBO = Mapper.Map<TProgramaGeneralProblemaModalidad, ProgramaGeneralProblemaModalidadBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public ProgramaGeneralProblemaModalidadBO FirstById(int id)
        {
            try
            {
                TProgramaGeneralProblemaModalidad entidad = base.FirstById(id);
                ProgramaGeneralProblemaModalidadBO objetoBO = new ProgramaGeneralProblemaModalidadBO();
                Mapper.Map<TProgramaGeneralProblemaModalidad, ProgramaGeneralProblemaModalidadBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public ProgramaGeneralProblemaModalidadBO FirstBy(Expression<Func<TProgramaGeneralProblemaModalidad, bool>> filter)
        {
            try
            {
                TProgramaGeneralProblemaModalidad entidad = base.FirstBy(filter);
                ProgramaGeneralProblemaModalidadBO objetoBO = Mapper.Map<TProgramaGeneralProblemaModalidad, ProgramaGeneralProblemaModalidadBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(ProgramaGeneralProblemaModalidadBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TProgramaGeneralProblemaModalidad entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<ProgramaGeneralProblemaModalidadBO> listadoBO)
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

        public bool Update(ProgramaGeneralProblemaModalidadBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TProgramaGeneralProblemaModalidad entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<ProgramaGeneralProblemaModalidadBO> listadoBO)
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
        private void AsignacionId(TProgramaGeneralProblemaModalidad entidad, ProgramaGeneralProblemaModalidadBO objetoBO)
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

        private TProgramaGeneralProblemaModalidad MapeoEntidad(ProgramaGeneralProblemaModalidadBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TProgramaGeneralProblemaModalidad entidad = new TProgramaGeneralProblemaModalidad();
                entidad = Mapper.Map<ProgramaGeneralProblemaModalidadBO, TProgramaGeneralProblemaModalidad>(objetoBO,
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
        public void EliminacionLogicoPorProblema(int idProblema, string usuario, List<ModalidadCursoDTO> nuevos)
        {
            try
            {
                var listaBorrar = GetBy(x => x.IdProgramaGeneralProblema == idProblema && x.Estado == true).ToList();
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
        public void EliminacionLogicoPorIdProblema(int idProblema, string usuario)
        {
            try
            {
                var listaBorrar = GetBy(x => x.IdProgramaGeneralProblema == idProblema && x.Estado == true).ToList();
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
