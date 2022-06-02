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
    /// Repositorio: Transversal/MaterialAdicionalAulaVirtualRegistro
    /// Autor : Lourdes Priscila Pacsi Gamboa
    /// Fecha: 19/06/2021
    /// <summary>
    /// Repositorio para consultas de la tabla pla.T_MaterialAdicionalAulaVirtualRegistro
    /// </summary>
    public class MaterialAdicionalAulaVirtualRegistroRepositorio : BaseRepository<TMaterialAdicionalAulaVirtualRegistro, MaterialAdicionalAulaVirtualRegistroBO>
    {
        
        #region Metodos Base
        public MaterialAdicionalAulaVirtualRegistroRepositorio() : base()
        {
        }
        public MaterialAdicionalAulaVirtualRegistroRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<MaterialAdicionalAulaVirtualRegistroBO> GetBy(Expression<Func<TMaterialAdicionalAulaVirtualRegistro, bool>> filter)
        {
            IEnumerable<TMaterialAdicionalAulaVirtualRegistro> listado = base.GetBy(filter);
            List<MaterialAdicionalAulaVirtualRegistroBO> listadoBO = new List<MaterialAdicionalAulaVirtualRegistroBO>();
            foreach (var itemEntidad in listado)
            {
                MaterialAdicionalAulaVirtualRegistroBO objetoBO = Mapper.Map<TMaterialAdicionalAulaVirtualRegistro, MaterialAdicionalAulaVirtualRegistroBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public MaterialAdicionalAulaVirtualRegistroBO FirstById(int id)
        {
            try
            {
                TMaterialAdicionalAulaVirtualRegistro entidad = base.FirstById(id);
                MaterialAdicionalAulaVirtualRegistroBO objetoBO = new MaterialAdicionalAulaVirtualRegistroBO();
                Mapper.Map<TMaterialAdicionalAulaVirtualRegistro, MaterialAdicionalAulaVirtualRegistroBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public MaterialAdicionalAulaVirtualRegistroBO FirstBy(Expression<Func<TMaterialAdicionalAulaVirtualRegistro, bool>> filter)
        {
            try
            {
                TMaterialAdicionalAulaVirtualRegistro entidad = base.FirstBy(filter);
                MaterialAdicionalAulaVirtualRegistroBO objetoBO = Mapper.Map<TMaterialAdicionalAulaVirtualRegistro, MaterialAdicionalAulaVirtualRegistroBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(MaterialAdicionalAulaVirtualRegistroBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TMaterialAdicionalAulaVirtualRegistro entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<MaterialAdicionalAulaVirtualRegistroBO> listadoBO)
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

        public bool Update(MaterialAdicionalAulaVirtualRegistroBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TMaterialAdicionalAulaVirtualRegistro entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<MaterialAdicionalAulaVirtualRegistroBO> listadoBO)
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
        private void AsignacionId(TMaterialAdicionalAulaVirtualRegistro entidad, MaterialAdicionalAulaVirtualRegistroBO objetoBO)
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

        private TMaterialAdicionalAulaVirtualRegistro MapeoEntidad(MaterialAdicionalAulaVirtualRegistroBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TMaterialAdicionalAulaVirtualRegistro entidad = new TMaterialAdicionalAulaVirtualRegistro();
                entidad = Mapper.Map<MaterialAdicionalAulaVirtualRegistroBO, TMaterialAdicionalAulaVirtualRegistro>(objetoBO,
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
        /// Repositorio : MaterialAdicionalAulaVirtualRegistroRepositorio
        /// Autor: Lourdes Priscila Pacsi Gamboa
        /// Fecha: 19/06/2021
        /// <summary>
        /// Obtiene los datos de la encuensta final realizada en el nuevo aula virtual para el reporte
        /// </summary>
        /// <param name="filtro">Datos traidos desde la interfaz para el filtro en el sp</param>
        /// <returns>Lista de objetos del tipo EncuestaFinalNuevaAulaDTO</returns>
        public List<MaterialAdicionalAulaVirtualRegistroDTO> ListaMaterialAdicionalAulaVirtualRegistro(int id)
        {
            try
            {
                List<MaterialAdicionalAulaVirtualRegistroDTO> materialAdicional = new List<MaterialAdicionalAulaVirtualRegistroDTO>();
                //Select IdPGeneral, Nombre, Titulo, Contenido, NombreTitulo, NroCapitulo From pla.V_ListadoEstructuraProgramaCapitulosV2 Where IdPGeneral = @IdPGeneral
                var _query = "Select Id,NombreArchivo,RutaArchivo,EsEnlace FROM pla.T_MaterialAdicionalAulaVirtualRegistro Where Estado = 1 AND IdMaterialAdicionalAulaVirtual=@id";
                var subQuery= _dapper.QueryDapper(_query, new { id });
                if (!string.IsNullOrEmpty(subQuery) && !subQuery.Contains("[]"))
                {
                    materialAdicional = JsonConvert.DeserializeObject<List<MaterialAdicionalAulaVirtualRegistroDTO>>(subQuery);
                }
                return materialAdicional;
            }
            catch (Exception E)
            {
                throw new Exception(E.Message);
            }

        }
        /// Repositorio : MaterialAdicionalAulaVirtualRegistroRepositorio
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
                string _query = "SELECT Id FROM  pla.T_MaterialAdicionalAulaVirtualRegistro WHERE Estado = 1 and IdMaterialAdicionalAulaVirtual = @IdMaterialAdicional ";
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
