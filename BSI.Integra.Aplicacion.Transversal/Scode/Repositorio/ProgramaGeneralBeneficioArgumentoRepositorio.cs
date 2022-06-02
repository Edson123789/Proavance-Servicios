using AutoMapper;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace BSI.Integra.Aplicacion.Transversal.Repositorio
{
    public class ProgramaGeneralBeneficioArgumentoRepositorio : BaseRepository<TProgramaGeneralBeneficioArgumento, ProgramaGeneralBeneficioArgumentoBO>
    {
        #region Metodos Base
        public ProgramaGeneralBeneficioArgumentoRepositorio() : base()
        {
        }
        public ProgramaGeneralBeneficioArgumentoRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<ProgramaGeneralBeneficioArgumentoBO> GetBy(Expression<Func<TProgramaGeneralBeneficioArgumento, bool>> filter)
        {
            IEnumerable<TProgramaGeneralBeneficioArgumento> listado = base.GetBy(filter);
            List<ProgramaGeneralBeneficioArgumentoBO> listadoBO = new List<ProgramaGeneralBeneficioArgumentoBO>();
            foreach (var itemEntidad in listado)
            {
                ProgramaGeneralBeneficioArgumentoBO objetoBO = Mapper.Map<TProgramaGeneralBeneficioArgumento, ProgramaGeneralBeneficioArgumentoBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public ProgramaGeneralBeneficioArgumentoBO FirstById(int id)
        {
            try
            {
                TProgramaGeneralBeneficioArgumento entidad = base.FirstById(id);
                ProgramaGeneralBeneficioArgumentoBO objetoBO = new ProgramaGeneralBeneficioArgumentoBO();
                Mapper.Map<TProgramaGeneralBeneficioArgumento, ProgramaGeneralBeneficioArgumentoBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public ProgramaGeneralBeneficioArgumentoBO FirstBy(Expression<Func<TProgramaGeneralBeneficioArgumento, bool>> filter)
        {
            try
            {
                TProgramaGeneralBeneficioArgumento entidad = base.FirstBy(filter);
                ProgramaGeneralBeneficioArgumentoBO objetoBO = Mapper.Map<TProgramaGeneralBeneficioArgumento, ProgramaGeneralBeneficioArgumentoBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(ProgramaGeneralBeneficioArgumentoBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TProgramaGeneralBeneficioArgumento entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<ProgramaGeneralBeneficioArgumentoBO> listadoBO)
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

        public bool Update(ProgramaGeneralBeneficioArgumentoBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TProgramaGeneralBeneficioArgumento entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<ProgramaGeneralBeneficioArgumentoBO> listadoBO)
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
        private void AsignacionId(TProgramaGeneralBeneficioArgumento entidad, ProgramaGeneralBeneficioArgumentoBO objetoBO)
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

        private TProgramaGeneralBeneficioArgumento MapeoEntidad(ProgramaGeneralBeneficioArgumentoBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TProgramaGeneralBeneficioArgumento entidad = new TProgramaGeneralBeneficioArgumento();
                entidad = Mapper.Map<ProgramaGeneralBeneficioArgumentoBO, TProgramaGeneralBeneficioArgumento>(objetoBO,
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


        public List<ProgramaGeneralBeneficioArgumentoDTO> ObtenerProgramaGeneralBeneficiosArgumentos(int idProgramaGeneralBeneficio){
            try
            {
                List<ProgramaGeneralBeneficioArgumentoDTO> programaGeneralBeneficioArgumentos = new List<ProgramaGeneralBeneficioArgumentoDTO>();
                var _query = "SELECT * FROM com.T_ProgramaGeneralBeneficioArgumento WHERE  IdProgramaGeneralBeneficio = @idProgramaGeneralBeneficio and Estado=1";
                var programaGeneralBeneficioArgumentosDB = _dapper.QueryDapper(_query, new { idProgramaGeneralBeneficio });
                programaGeneralBeneficioArgumentos = JsonConvert.DeserializeObject<List<ProgramaGeneralBeneficioArgumentoDTO>>(programaGeneralBeneficioArgumentosDB);
                return programaGeneralBeneficioArgumentos;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// <summary>
        /// Elimina (Actualiza estado a false ) todos los Argumentos asociados a un programa y Beneficio segun una lista de beneficios Padre
        /// </summary>
        /// <param name="idPGeneral"></param>
        /// <returns></returns>
        public void EliminacionLogicoPorListaBeneficios(string usuario, List<int> eliminados)
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
        /// Elimina (Actualiza estado a false ) todos los Argumentos asociados a un programa y Beneficio segun un beneficio Padre
        /// </summary>
        /// <returns></returns>
        public void EliminacionLogicoPorBeneficio(int idBeneficio, string usuario, List<BeneficioArgumentoDTO> nuevos)
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
        /// Elimina (Actualiza estado a false ) todos los Argumentos asociados a un programa y Beneficio segun un beneficio Padre
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
