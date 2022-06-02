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
    public class ProgramaGeneralBeneficioModalidadRepositorio : BaseRepository<TProgramaGeneralBeneficioModalidad, ProgramaGeneralBeneficioModalidadBO>
    {
        #region Metodos Base
        public ProgramaGeneralBeneficioModalidadRepositorio() : base()
        {
        }
        public ProgramaGeneralBeneficioModalidadRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<ProgramaGeneralBeneficioModalidadBO> GetBy(Expression<Func<TProgramaGeneralBeneficioModalidad, bool>> filter)
        {
            IEnumerable<TProgramaGeneralBeneficioModalidad> listado = base.GetBy(filter);
            List<ProgramaGeneralBeneficioModalidadBO> listadoBO = new List<ProgramaGeneralBeneficioModalidadBO>();
            foreach (var itemEntidad in listado)
            {
                ProgramaGeneralBeneficioModalidadBO objetoBO = Mapper.Map<TProgramaGeneralBeneficioModalidad, ProgramaGeneralBeneficioModalidadBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public ProgramaGeneralBeneficioModalidadBO FirstById(int id)
        {
            try
            {
                TProgramaGeneralBeneficioModalidad entidad = base.FirstById(id);
                ProgramaGeneralBeneficioModalidadBO objetoBO = new ProgramaGeneralBeneficioModalidadBO();
                Mapper.Map<TProgramaGeneralBeneficioModalidad, ProgramaGeneralBeneficioModalidadBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public ProgramaGeneralBeneficioModalidadBO FirstBy(Expression<Func<TProgramaGeneralBeneficioModalidad, bool>> filter)
        {
            try
            {
                TProgramaGeneralBeneficioModalidad entidad = base.FirstBy(filter);
                ProgramaGeneralBeneficioModalidadBO objetoBO = Mapper.Map<TProgramaGeneralBeneficioModalidad, ProgramaGeneralBeneficioModalidadBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(ProgramaGeneralBeneficioModalidadBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TProgramaGeneralBeneficioModalidad entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<ProgramaGeneralBeneficioModalidadBO> listadoBO)
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

        public bool Update(ProgramaGeneralBeneficioModalidadBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TProgramaGeneralBeneficioModalidad entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<ProgramaGeneralBeneficioModalidadBO> listadoBO)
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
        private void AsignacionId(TProgramaGeneralBeneficioModalidad entidad, ProgramaGeneralBeneficioModalidadBO objetoBO)
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

        private TProgramaGeneralBeneficioModalidad MapeoEntidad(ProgramaGeneralBeneficioModalidadBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TProgramaGeneralBeneficioModalidad entidad = new TProgramaGeneralBeneficioModalidad();
                entidad = Mapper.Map<ProgramaGeneralBeneficioModalidadBO, TProgramaGeneralBeneficioModalidad>(objetoBO,
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
        /// Elimina (Actualiza estado a false ) todos las Modaliades asociados a un programa y Beneficio segun una lista de beneficios Padre
        /// </summary>
        /// <param name="idPGeneral"></param>
        /// <returns></returns>
        public void EliminacionLogicoPorListaBeneficio(string usuario, List<int> eliminados)
        {
            try
            {
                foreach (var item in eliminados)
                {
                    var listaBorrar = GetBy(x => x.IdProgramaGeneralBeneficio == item && x.Estado == true).ToList();
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
        /// Elimina (Actualiza estado a false ) todas las modalidades asociados a un programa y Beneficio segun un beneficio Padre
        /// </summary>
        /// <returns></returns>
        public void EliminacionLogicoPorBeneficio(int idBeneficio, string usuario, List<ModalidadCursoDTO> nuevos)
        {
            try
            {
                var listaBorrar = GetBy(x => x.IdProgramaGeneralBeneficio == idBeneficio && x.Estado == true).ToList();
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
        public void EliminacionLogicoPorIdBeneficio(int idBeneficio, string usuario)
        {
            try
            {
                var listaBorrar = GetBy(x => x.IdProgramaGeneralBeneficio == idBeneficio && x.Estado == true).ToList();                
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
