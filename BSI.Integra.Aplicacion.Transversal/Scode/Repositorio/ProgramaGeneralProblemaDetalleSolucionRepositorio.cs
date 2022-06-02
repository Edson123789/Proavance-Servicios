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
    public class ProgramaGeneralProblemaDetalleSolucionRepositorio : BaseRepository<TProgramaGeneralProblemaDetalleSolucion, ProgramaGeneralProblemaDetalleSolucionBO>
    {
        #region Metodos Base
        public ProgramaGeneralProblemaDetalleSolucionRepositorio() : base()
        {
        }
        public ProgramaGeneralProblemaDetalleSolucionRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<ProgramaGeneralProblemaDetalleSolucionBO> GetBy(Expression<Func<TProgramaGeneralProblemaDetalleSolucion, bool>> filter)
        {
            IEnumerable<TProgramaGeneralProblemaDetalleSolucion> listado = base.GetBy(filter);
            List<ProgramaGeneralProblemaDetalleSolucionBO> listadoBO = new List<ProgramaGeneralProblemaDetalleSolucionBO>();
            foreach (var itemEntidad in listado)
            {
                ProgramaGeneralProblemaDetalleSolucionBO objetoBO = Mapper.Map<TProgramaGeneralProblemaDetalleSolucion, ProgramaGeneralProblemaDetalleSolucionBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public ProgramaGeneralProblemaDetalleSolucionBO FirstById(int id)
        {
            try
            {
                TProgramaGeneralProblemaDetalleSolucion entidad = base.FirstById(id);
                ProgramaGeneralProblemaDetalleSolucionBO objetoBO = new ProgramaGeneralProblemaDetalleSolucionBO();
                Mapper.Map<TProgramaGeneralProblemaDetalleSolucion, ProgramaGeneralProblemaDetalleSolucionBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public ProgramaGeneralProblemaDetalleSolucionBO FirstBy(Expression<Func<TProgramaGeneralProblemaDetalleSolucion, bool>> filter)
        {
            try
            {
                TProgramaGeneralProblemaDetalleSolucion entidad = base.FirstBy(filter);
                ProgramaGeneralProblemaDetalleSolucionBO objetoBO = Mapper.Map<TProgramaGeneralProblemaDetalleSolucion, ProgramaGeneralProblemaDetalleSolucionBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(ProgramaGeneralProblemaDetalleSolucionBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TProgramaGeneralProblemaDetalleSolucion entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<ProgramaGeneralProblemaDetalleSolucionBO> listadoBO)
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

        public bool Update(ProgramaGeneralProblemaDetalleSolucionBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TProgramaGeneralProblemaDetalleSolucion entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<ProgramaGeneralProblemaDetalleSolucionBO> listadoBO)
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
        private void AsignacionId(TProgramaGeneralProblemaDetalleSolucion entidad, ProgramaGeneralProblemaDetalleSolucionBO objetoBO)
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

        private TProgramaGeneralProblemaDetalleSolucion MapeoEntidad(ProgramaGeneralProblemaDetalleSolucionBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TProgramaGeneralProblemaDetalleSolucion entidad = new TProgramaGeneralProblemaDetalleSolucion();
                entidad = Mapper.Map<ProgramaGeneralProblemaDetalleSolucionBO, TProgramaGeneralProblemaDetalleSolucion>(objetoBO,
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
        public void EliminacionLogicoPorProblema(int idProblema, string usuario, List<ProblemaArgumentoDTO> nuevos)
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
        /// Elimina (Actualiza estado a false ) todos los Argumentos asociados a un programa y Beneficio segun un beneficio Padre
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
