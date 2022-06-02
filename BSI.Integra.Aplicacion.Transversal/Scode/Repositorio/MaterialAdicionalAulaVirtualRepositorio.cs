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
    /// Repositorio: Transversal/MaterialAdicionalAulaVirtual
    /// Autor : Lourdes Priscila Pacsi Gamboa
    /// Fecha: 19/06/2021
    /// <summary>
    /// Repositorio para consultas de la tabla pla.T_MaterialAdicionalAulaVirtual
    /// </summary>
    public class MaterialAdicionalAulaVirtualRepositorio : BaseRepository<TMaterialAdicionalAulaVirtual, MaterialAdicionalAulaVirtualBO>
    {
        #region Metodos Base
        public MaterialAdicionalAulaVirtualRepositorio() : base()
        {
        }
        public MaterialAdicionalAulaVirtualRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<MaterialAdicionalAulaVirtualBO> GetBy(Expression<Func<TMaterialAdicionalAulaVirtual, bool>> filter)
        {
            IEnumerable<TMaterialAdicionalAulaVirtual> listado = base.GetBy(filter);
            List<MaterialAdicionalAulaVirtualBO> listadoBO = new List<MaterialAdicionalAulaVirtualBO>();
            foreach (var itemEntidad in listado)
            {
                MaterialAdicionalAulaVirtualBO objetoBO = Mapper.Map<TMaterialAdicionalAulaVirtual, MaterialAdicionalAulaVirtualBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public MaterialAdicionalAulaVirtualBO FirstById(int id)
        {
            try
            {
                TMaterialAdicionalAulaVirtual entidad = base.FirstById(id);
                MaterialAdicionalAulaVirtualBO objetoBO = new MaterialAdicionalAulaVirtualBO();
                Mapper.Map<TMaterialAdicionalAulaVirtual, MaterialAdicionalAulaVirtualBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public MaterialAdicionalAulaVirtualBO FirstBy(Expression<Func<TMaterialAdicionalAulaVirtual, bool>> filter)
        {
            try
            {
                TMaterialAdicionalAulaVirtual entidad = base.FirstBy(filter);
                MaterialAdicionalAulaVirtualBO objetoBO = Mapper.Map<TMaterialAdicionalAulaVirtual, MaterialAdicionalAulaVirtualBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(MaterialAdicionalAulaVirtualBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TMaterialAdicionalAulaVirtual entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<MaterialAdicionalAulaVirtualBO> listadoBO)
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

        public bool Update(MaterialAdicionalAulaVirtualBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TMaterialAdicionalAulaVirtual entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<MaterialAdicionalAulaVirtualBO> listadoBO)
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
        private void AsignacionId(TMaterialAdicionalAulaVirtual entidad, MaterialAdicionalAulaVirtualBO objetoBO)
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

        private TMaterialAdicionalAulaVirtual MapeoEntidad(MaterialAdicionalAulaVirtualBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TMaterialAdicionalAulaVirtual entidad = new TMaterialAdicionalAulaVirtual();
                entidad = Mapper.Map<MaterialAdicionalAulaVirtualBO, TMaterialAdicionalAulaVirtual>(objetoBO,
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

        /// Repositorio : MaterialAdicionalAulaVirtualRepositorio
        /// Autor: Lourdes Priscila Pacsi Gamboa
        /// Fecha: 19/06/2021
        /// <summary>
        /// Retorna la lista de los programas generales a los cuales se les agrego su material adicional
        /// </summary>
        /// <returns>Lista de objetos del tipo EncuestaFinalNuevaAulaDTO</returns>
        public List<MaterialAdicionalAulaVirtualPGeneralDTO> ListaMaterialAdicionalAulaVirtualProgramaGeneral()
         {
            try
            {
                List<MaterialAdicionalAulaVirtualPGeneralDTO> capitulosFiltro = new List<MaterialAdicionalAulaVirtualPGeneralDTO>();
                //Select IdPGeneral, Nombre, Titulo, Contenido, NombreTitulo, NroCapitulo From pla.V_ListadoEstructuraProgramaCapitulosV2 Where IdPGeneral = @IdPGeneral
                var _queryfiltrocapitulo = "Select Id,NombreConfiguracion,IdPGeneral,Nombre FROM pla.V_ProgramaGeneralMaterialAdicionalAulaVirtual";
                var SubfiltroCapitulo = _dapper.QueryDapper(_queryfiltrocapitulo, new { });
                if (!string.IsNullOrEmpty(SubfiltroCapitulo) && !SubfiltroCapitulo.Contains("[]"))
                {
                    capitulosFiltro = JsonConvert.DeserializeObject<List<MaterialAdicionalAulaVirtualPGeneralDTO>>(SubfiltroCapitulo);
                }
                return capitulosFiltro;
            }
            catch (Exception E)
            {
                throw new Exception(E.Message);
            }

        }
        /// Repositorio : MaterialAdicionalAulaVirtualRepositorio
        /// Autor: Lourdes Priscila Pacsi Gamboa
        /// Fecha: 19/06/2021
        /// <summary>
        /// Retorna los datos de registro segun el Id enviado
        /// </summary>
        /// <param name="id">El id de la configuracion</param>
        /// <returns>Objeto del tipo MaterialAdicionalAulaVirtualDTO</returns>
        public MaterialAdicionalAulaVirtualDTO DatosMaterialAdicional(int id)
        {
            try
            {
                MaterialAdicionalAulaVirtualDTO materialAdicional = new MaterialAdicionalAulaVirtualDTO();
                //Select IdPGeneral, Nombre, Titulo, Contenido, NombreTitulo, NroCapitulo From pla.V_ListadoEstructuraProgramaCapitulosV2 Where IdPGeneral = @IdPGeneral
                var _query = "Select Id,NombreConfiguracion,EsOnline,IdPgeneral FROM pla.T_MaterialAdicionalAulaVirtual Where Estado = 1 AND Id=@id";
                var SubQuery = _dapper.FirstOrDefault(_query, new {id });
                if (!string.IsNullOrEmpty(SubQuery) && !SubQuery.Contains("[]"))
                {
                    materialAdicional = JsonConvert.DeserializeObject<MaterialAdicionalAulaVirtualDTO>(SubQuery);
                }
                return materialAdicional;
            }
            catch (Exception E)
            {
                throw new Exception(E.Message);
            }

        }
    }
}
