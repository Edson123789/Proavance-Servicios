using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using AutoMapper;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using System.Collections.Generic;
using System.Text;
using BSI.Integra.Aplicacion.Transversal.Scode.BO;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.DTOs.Scode.DTOs.Transversal;
using System.Linq;
using Newtonsoft.Json;
using BSI.Integra.Aplicacion.Transversal.DTO;
namespace BSI.Integra.Aplicacion.Transversal.Repositorio
{
    /// Repositorio: Transversal/MaterialAdicionalAulaVirtualPEspecifico
    /// Autor : Lourdes Priscila Pacsi Gamboa
    /// Fecha: 19/06/2021
    /// <summary>
    /// Repositorio para consultas de la tabla pla.T_MaterialAdicionalAulaVirtualPespecifico
    /// </summary>
    public class MaterialAdicionalAulaVirtualPEspecificoRepositorio : BaseRepository<TMaterialAdicionalAulaVirtualPespecifico, MaterialAdicionalAulaVirtualPEspecificoBO>
    {
        #region Metodos Base
        public MaterialAdicionalAulaVirtualPEspecificoRepositorio() : base()
        {
        }
        public MaterialAdicionalAulaVirtualPEspecificoRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<MaterialAdicionalAulaVirtualPEspecificoBO> GetBy(Expression<Func<TMaterialAdicionalAulaVirtualPespecifico, bool>> filter)
        {
            IEnumerable<TMaterialAdicionalAulaVirtualPespecifico> listado = base.GetBy(filter);
            List<MaterialAdicionalAulaVirtualPEspecificoBO> listadoBO = new List<MaterialAdicionalAulaVirtualPEspecificoBO>();
            foreach (var itemEntidad in listado)
            {
                MaterialAdicionalAulaVirtualPEspecificoBO objetoBO = Mapper.Map<TMaterialAdicionalAulaVirtualPespecifico, MaterialAdicionalAulaVirtualPEspecificoBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public MaterialAdicionalAulaVirtualPEspecificoBO FirstById(int id)
        {
            try
            {
                TMaterialAdicionalAulaVirtualPespecifico entidad = base.FirstById(id);
                MaterialAdicionalAulaVirtualPEspecificoBO objetoBO = new MaterialAdicionalAulaVirtualPEspecificoBO();
                Mapper.Map<TMaterialAdicionalAulaVirtualPespecifico, MaterialAdicionalAulaVirtualPEspecificoBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public MaterialAdicionalAulaVirtualPEspecificoBO FirstBy(Expression<Func<TMaterialAdicionalAulaVirtualPespecifico, bool>> filter)
        {
            try
            {
                TMaterialAdicionalAulaVirtualPespecifico entidad = base.FirstBy(filter);
                MaterialAdicionalAulaVirtualPEspecificoBO objetoBO = Mapper.Map<TMaterialAdicionalAulaVirtualPespecifico, MaterialAdicionalAulaVirtualPEspecificoBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(MaterialAdicionalAulaVirtualPEspecificoBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TMaterialAdicionalAulaVirtualPespecifico entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<MaterialAdicionalAulaVirtualPEspecificoBO> listadoBO)
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

        public bool Update(MaterialAdicionalAulaVirtualPEspecificoBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TMaterialAdicionalAulaVirtualPespecifico entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<MaterialAdicionalAulaVirtualPEspecificoBO> listadoBO)
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
        private void AsignacionId(TMaterialAdicionalAulaVirtualPespecifico entidad, MaterialAdicionalAulaVirtualPEspecificoBO objetoBO)
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

        private TMaterialAdicionalAulaVirtualPespecifico MapeoEntidad(MaterialAdicionalAulaVirtualPEspecificoBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TMaterialAdicionalAulaVirtualPespecifico entidad = new TMaterialAdicionalAulaVirtualPespecifico();
                entidad = Mapper.Map<MaterialAdicionalAulaVirtualPEspecificoBO, TMaterialAdicionalAulaVirtualPespecifico>(objetoBO,
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
        /// Repositorio : MaterialAdicionalAulaVirtualPEspecificoRepositorio
        /// Autor: Lourdes Priscila Pacsi Gamboa
        /// Fecha: 19/06/2021
        /// <summary>
        /// Lista los programas especificos segun el Id de configuracion de la tabla T_MaterialAdicionalAulaVirtual
        /// </summary>
        /// <param name="id">Id de configuracion de la tabla T_MaterialAdicionalAulaVirtual</param>
        /// <returns>Lista de objetos del tipo MaterialAdicionalAulaVirtualPespecificoDTO</returns>
        public List<MaterialAdicionalAulaVirtualPespecificoDTO> ListaMaterialAdicionalAulaVirtualPEspecifico(int id)
        {
            try
            {
                List<MaterialAdicionalAulaVirtualPespecificoDTO> materialAdicional = new List<MaterialAdicionalAulaVirtualPespecificoDTO>();
                //Select IdPGeneral, Nombre, Titulo, Contenido, NombreTitulo, NroCapitulo From pla.V_ListadoEstructuraProgramaCapitulosV2 Where IdPGeneral = @IdPGeneral
                var _query = "Select Id,IdPespecifico FROM pla.T_MaterialAdicionalAulaVirtualPespecifico Where Estado = 1 AND IdMaterialAdicionalAulaVirtual=@id";
                var subQuery = _dapper.QueryDapper(_query, new { id });
                if (!string.IsNullOrEmpty(subQuery) && !subQuery.Contains("[]"))
                {
                    materialAdicional = JsonConvert.DeserializeObject<List<MaterialAdicionalAulaVirtualPespecificoDTO>>(subQuery);
                }
                return materialAdicional;
            }
            catch (Exception E)
            {
                throw new Exception(E.Message);
            }

        }
        /// Repositorio : MaterialAdicionalAulaVirtualPEspecificoRepositorio
        /// Autor: Lourdes Priscila Pacsi Gamboa
        /// Fecha: 19/06/2021
        /// <summary>
        /// Elimina de manera logica una lista de objetos
        /// </summary>
        /// <param name="IdMaterialAdicional">Id de la tabla T_MaterialAdicionalAulaVirtual</param>
        /// <param name="usuario">Usuario del integra</param>
        /// <param name="nuevos">Lista de enteros</param>
        public void DeleteLogicoPorMaterial(int IdMaterialAdicional, string usuario, List<int> nuevos)
        {
            try
            {
                List<EliminacionIdsDTO> listaBorrar = new List<EliminacionIdsDTO>();
                string _query = "SELECT Id FROM  pla.T_MaterialAdicionalAulaVirtualPespecifico WHERE Estado = 1 and IdMaterialAdicionalAulaVirtual = @IdMaterialAdicional ";
                var query = _dapper.QueryDapper(_query, new { IdMaterialAdicional });
                listaBorrar = JsonConvert.DeserializeObject<List<EliminacionIdsDTO>>(query);
                listaBorrar.RemoveAll(x => nuevos.Any(y => y == x.Id));
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
