using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using AutoMapper;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;

namespace BSI.Integra.Aplicacion.Transversal.Repositorio
{
    public class ProgramaGeneralPrerequisitoModalidadRepositorio : BaseRepository<TProgramaGeneralPrerequisitoModalidad, ProgramaGeneralPrerequisitoModalidadBO>
    {
        #region Metodos Base
        public ProgramaGeneralPrerequisitoModalidadRepositorio() : base()
        {
        }
        public ProgramaGeneralPrerequisitoModalidadRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<ProgramaGeneralPrerequisitoModalidadBO> GetBy(Expression<Func<TProgramaGeneralPrerequisitoModalidad, bool>> filter)
        {
            IEnumerable<TProgramaGeneralPrerequisitoModalidad> listado = base.GetBy(filter);
            List<ProgramaGeneralPrerequisitoModalidadBO> listadoBO = new List<ProgramaGeneralPrerequisitoModalidadBO>();
            foreach (var itemEntidad in listado)
            {
                ProgramaGeneralPrerequisitoModalidadBO objetoBO = Mapper.Map<TProgramaGeneralPrerequisitoModalidad, ProgramaGeneralPrerequisitoModalidadBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public ProgramaGeneralPrerequisitoModalidadBO FirstById(int id)
        {
            try
            {
                TProgramaGeneralPrerequisitoModalidad entidad = base.FirstById(id);
                ProgramaGeneralPrerequisitoModalidadBO objetoBO = new ProgramaGeneralPrerequisitoModalidadBO();
                Mapper.Map<TProgramaGeneralPrerequisitoModalidad, ProgramaGeneralPrerequisitoModalidadBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public ProgramaGeneralPrerequisitoModalidadBO FirstBy(Expression<Func<TProgramaGeneralPrerequisitoModalidad, bool>> filter)
        {
            try
            {
                TProgramaGeneralPrerequisitoModalidad entidad = base.FirstBy(filter);
                ProgramaGeneralPrerequisitoModalidadBO objetoBO = Mapper.Map<TProgramaGeneralPrerequisitoModalidad, ProgramaGeneralPrerequisitoModalidadBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(ProgramaGeneralPrerequisitoModalidadBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TProgramaGeneralPrerequisitoModalidad entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<ProgramaGeneralPrerequisitoModalidadBO> listadoBO)
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

        public bool Update(ProgramaGeneralPrerequisitoModalidadBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TProgramaGeneralPrerequisitoModalidad entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<ProgramaGeneralPrerequisitoModalidadBO> listadoBO)
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
        private void AsignacionId(TProgramaGeneralPrerequisitoModalidad entidad, ProgramaGeneralPrerequisitoModalidadBO objetoBO)
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

        private TProgramaGeneralPrerequisitoModalidad MapeoEntidad(ProgramaGeneralPrerequisitoModalidadBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TProgramaGeneralPrerequisitoModalidad entidad = new TProgramaGeneralPrerequisitoModalidad();
                entidad = Mapper.Map<ProgramaGeneralPrerequisitoModalidadBO, TProgramaGeneralPrerequisitoModalidad>(objetoBO,
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
        /// Elimina (Actualiza estado a false ) todos los Argumentos asociados a un programa y PreRequisito segun una lista de PreRequisitos Padre
        /// </summary>
        /// <param name="idPGeneral"></param>
        /// <returns></returns>
        public void EliminacionLogicoPorListaPrerequisito(string usuario, List<int> eliminados)
        {
            try
            {
                foreach (var item in eliminados)
                {
                    var listaBorrar = GetBy(x => x.IdProgramaGeneralPrerequisito == item && x.Estado == true).ToList();
                    foreach (var Subitem in listaBorrar)
                    {
                        Delete(Subitem.Id, usuario);
                    }
                }

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// <summary>
        /// Elimina (Actualiza estado a false ) todas las modalidades asociados a un PreRequisito segun un PreRequisito Padre
        /// </summary>
        /// <returns></returns>
        public void EliminacionLogicoPorBeneficio(int idPreRequisito, string usuario, List<ModalidadCursoDTO> nuevos)
        {
            try
            {
                var listaBorrar = GetBy(x => x.IdProgramaGeneralPrerequisito == idPreRequisito && x.Estado == true).ToList();
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
    }
}
