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
    public class ProgramaGeneralMotivacionArgumentoRepositorio : BaseRepository<TProgramaGeneralMotivacionArgumento, ProgramaGeneralMotivacionArgumentoBO>
    {
        #region Metodos Base
        public ProgramaGeneralMotivacionArgumentoRepositorio() : base()
        {
        }
        public ProgramaGeneralMotivacionArgumentoRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<ProgramaGeneralMotivacionArgumentoBO> GetBy(Expression<Func<TProgramaGeneralMotivacionArgumento, bool>> filter)
        {
            IEnumerable<TProgramaGeneralMotivacionArgumento> listado = base.GetBy(filter);
            List<ProgramaGeneralMotivacionArgumentoBO> listadoBO = new List<ProgramaGeneralMotivacionArgumentoBO>();
            foreach (var itemEntidad in listado)
            {
                ProgramaGeneralMotivacionArgumentoBO objetoBO = Mapper.Map<TProgramaGeneralMotivacionArgumento, ProgramaGeneralMotivacionArgumentoBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public ProgramaGeneralMotivacionArgumentoBO FirstById(int id)
        {
            try
            {
                TProgramaGeneralMotivacionArgumento entidad = base.FirstById(id);
                ProgramaGeneralMotivacionArgumentoBO objetoBO = new ProgramaGeneralMotivacionArgumentoBO();
                Mapper.Map<TProgramaGeneralMotivacionArgumento, ProgramaGeneralMotivacionArgumentoBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public ProgramaGeneralMotivacionArgumentoBO FirstBy(Expression<Func<TProgramaGeneralMotivacionArgumento, bool>> filter)
        {
            try
            {
                TProgramaGeneralMotivacionArgumento entidad = base.FirstBy(filter);
                ProgramaGeneralMotivacionArgumentoBO objetoBO = Mapper.Map<TProgramaGeneralMotivacionArgumento, ProgramaGeneralMotivacionArgumentoBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(ProgramaGeneralMotivacionArgumentoBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TProgramaGeneralMotivacionArgumento entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<ProgramaGeneralMotivacionArgumentoBO> listadoBO)
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

        public bool Update(ProgramaGeneralMotivacionArgumentoBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TProgramaGeneralMotivacionArgumento entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<ProgramaGeneralMotivacionArgumentoBO> listadoBO)
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
        private void AsignacionId(TProgramaGeneralMotivacionArgumento entidad, ProgramaGeneralMotivacionArgumentoBO objetoBO)
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

        private TProgramaGeneralMotivacionArgumento MapeoEntidad(ProgramaGeneralMotivacionArgumentoBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TProgramaGeneralMotivacionArgumento entidad = new TProgramaGeneralMotivacionArgumento();
                entidad = Mapper.Map<ProgramaGeneralMotivacionArgumentoBO, TProgramaGeneralMotivacionArgumento>(objetoBO,
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
        public void EliminacionLogicoPorMotivacion(int idMotivacion, string usuario, List<MotivacionArgumentoDTO> nuevos)
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
        /// Elimina (Actualiza estado a false ) todos los Argumentos asociados a un programa y Beneficio segun un beneficio Padre
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
