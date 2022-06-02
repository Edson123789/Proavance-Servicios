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
    public class ProgramaGeneralMotivacionModalidadRepositorio : BaseRepository<TProgramaGeneralMotivacionModalidad, ProgramaGeneralMotivacionModalidadBO>
    {
        #region Metodos Base
        public ProgramaGeneralMotivacionModalidadRepositorio() : base()
        {
        }
        public ProgramaGeneralMotivacionModalidadRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<ProgramaGeneralMotivacionModalidadBO> GetBy(Expression<Func<TProgramaGeneralMotivacionModalidad, bool>> filter)
        {
            IEnumerable<TProgramaGeneralMotivacionModalidad> listado = base.GetBy(filter);
            List<ProgramaGeneralMotivacionModalidadBO> listadoBO = new List<ProgramaGeneralMotivacionModalidadBO>();
            foreach (var itemEntidad in listado)
            {
                ProgramaGeneralMotivacionModalidadBO objetoBO = Mapper.Map<TProgramaGeneralMotivacionModalidad, ProgramaGeneralMotivacionModalidadBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public ProgramaGeneralMotivacionModalidadBO FirstById(int id)
        {
            try
            {
                TProgramaGeneralMotivacionModalidad entidad = base.FirstById(id);
                ProgramaGeneralMotivacionModalidadBO objetoBO = new ProgramaGeneralMotivacionModalidadBO();
                Mapper.Map<TProgramaGeneralMotivacionModalidad, ProgramaGeneralMotivacionModalidadBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public ProgramaGeneralMotivacionModalidadBO FirstBy(Expression<Func<TProgramaGeneralMotivacionModalidad, bool>> filter)
        {
            try
            {
                TProgramaGeneralMotivacionModalidad entidad = base.FirstBy(filter);
                ProgramaGeneralMotivacionModalidadBO objetoBO = Mapper.Map<TProgramaGeneralMotivacionModalidad, ProgramaGeneralMotivacionModalidadBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(ProgramaGeneralMotivacionModalidadBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TProgramaGeneralMotivacionModalidad entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<ProgramaGeneralMotivacionModalidadBO> listadoBO)
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

        public bool Update(ProgramaGeneralMotivacionModalidadBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TProgramaGeneralMotivacionModalidad entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<ProgramaGeneralMotivacionModalidadBO> listadoBO)
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
        private void AsignacionId(TProgramaGeneralMotivacionModalidad entidad, ProgramaGeneralMotivacionModalidadBO objetoBO)
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

        private TProgramaGeneralMotivacionModalidad MapeoEntidad(ProgramaGeneralMotivacionModalidadBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TProgramaGeneralMotivacionModalidad entidad = new TProgramaGeneralMotivacionModalidad();
                entidad = Mapper.Map<ProgramaGeneralMotivacionModalidadBO, TProgramaGeneralMotivacionModalidad>(objetoBO,
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
        public void EliminacionLogicoPorMotivacion(int idMotivacion, string usuario, List<ModalidadCursoDTO> nuevos)
        {
            try
            {
                var listaBorrar = GetBy(x => x.IdProgramaGeneralMotivacion == idMotivacion && x.Estado == true).ToList();
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
        public void EliminacionLogicoPorIdMotivacion(int idMotivacion, string usuario)
        {
            try
            {
                var listaBorrar = GetBy(x => x.IdProgramaGeneralMotivacion == idMotivacion && x.Estado == true).ToList();
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
