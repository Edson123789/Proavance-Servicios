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
    /// Repositorio: Finanzas/NuevoAlumnoCongelado
    /// Autor: Lourdes Priscila Pacsi Gamboa
    /// Fecha: 25/05/2021
    /// <summary>
    /// Repositorio para consultas de fin.NuevoAlumnoCongelado
    /// </summary>
    /// 
    public class NuevoAlumnoCongeladoRepositorio : BaseRepository<TNuevoAlumnoCongelado, NuevoAlumnoCongeladoBO>
    {

        #region Metodos Base
        public NuevoAlumnoCongeladoRepositorio() : base()
        {
        }
        public NuevoAlumnoCongeladoRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<NuevoAlumnoCongeladoBO> GetBy(Expression<Func<TNuevoAlumnoCongelado, bool>> filter)
        {
            IEnumerable<TNuevoAlumnoCongelado> listado = base.GetBy(filter);
            List<NuevoAlumnoCongeladoBO> listadoBO = new List<NuevoAlumnoCongeladoBO>();
            foreach (var itemEntidad in listado)
            {
                NuevoAlumnoCongeladoBO objetoBO = Mapper.Map<TNuevoAlumnoCongelado, NuevoAlumnoCongeladoBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public NuevoAlumnoCongeladoBO FirstById(int id)
        {
            try
            {
                TNuevoAlumnoCongelado entidad = base.FirstById(id);
                NuevoAlumnoCongeladoBO objetoBO = new NuevoAlumnoCongeladoBO();
                Mapper.Map<TNuevoAlumnoCongelado, NuevoAlumnoCongeladoBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public NuevoAlumnoCongeladoBO FirstBy(Expression<Func<TNuevoAlumnoCongelado, bool>> filter)
        {
            try
            {
                TNuevoAlumnoCongelado entidad = base.FirstBy(filter);
                NuevoAlumnoCongeladoBO objetoBO = Mapper.Map<TNuevoAlumnoCongelado, NuevoAlumnoCongeladoBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(NuevoAlumnoCongeladoBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TNuevoAlumnoCongelado entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<NuevoAlumnoCongeladoBO> listadoBO)
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

        public bool Update(NuevoAlumnoCongeladoBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TNuevoAlumnoCongelado entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<NuevoAlumnoCongeladoBO> listadoBO)
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
        private void AsignacionId(TNuevoAlumnoCongelado entidad, NuevoAlumnoCongeladoBO objetoBO)
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

        private TNuevoAlumnoCongelado MapeoEntidad(NuevoAlumnoCongeladoBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TNuevoAlumnoCongelado entidad = new TNuevoAlumnoCongelado();
                entidad = Mapper.Map<NuevoAlumnoCongeladoBO, TNuevoAlumnoCongelado>(objetoBO,
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
        public void DeleteLogicoPorMatricula(string FechaCongelamiento, int IdPeriodo, string usuario)
        {
            try
            {
                List<EliminacionIdsDTO> listaBorrar = new List<EliminacionIdsDTO>();
                string _query = "SELECT Id FROM  fin.T_NuevoAlumnoCongelado WHERE Estado = 1 and FechaCongelamiento <= @FechaCongelamiento and IdPeriodo=@IdPeriodo ";
                var query = _dapper.QueryDapper(_query, new { FechaCongelamiento, IdPeriodo });
                listaBorrar = JsonConvert.DeserializeObject<List<EliminacionIdsDTO>>(query);                
                foreach (var item in listaBorrar)
                {
                    var IdBorrar = item.Id;
                    //Delete(item.Id, usuario);
                    string _queryBorrar = "Update fin.T_NuevoAlumnoCongelado set Estado = 0 where Id=@IdBorrar ";
                    var queryBorrar = _dapper.QueryDapper(_queryBorrar, new { IdBorrar });
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public List<NuevoAlumnoCongeladoDTO> ObtenerListaNuevoAlumnoCongelado()
        {
            try
            {
                List<NuevoAlumnoCongeladoDTO> Filtro = new List<NuevoAlumnoCongeladoDTO>();
                //Select IdPGeneral, Nombre, Titulo, Contenido, NombreTitulo, NroCapitulo From pla.V_ListadoEstructuraProgramaCapitulosV2 Where IdPGeneral = @IdPGeneral
                var _queryfiltro = "Select * FROM [fin].[V_NuevoAlumnoCongelado] order by IdMatriculaCabecera";
                var Subfiltro = _dapper.QueryDapper(_queryfiltro, null);
                if (!string.IsNullOrEmpty(Subfiltro) && !Subfiltro.Contains("[]"))
                {
                    Filtro = JsonConvert.DeserializeObject<List<NuevoAlumnoCongeladoDTO>>(Subfiltro);
                }
                return Filtro;
            }
            catch (Exception E)
            {
                throw new Exception(E.Message);
            }

        }

        public bool InsertarExcelNuevoAlumnoCongelado(object datos,DateTime FechaCongelamiento,int IdPeriodo, string User)
        {
            try
            {
                string Json = JsonConvert.SerializeObject(datos);
                var registroDB = _dapper.QuerySPFirstOrDefault("[fin].[SP_InsertarNuevoAlumnoCongelado]", new { Json, FechaCongelamiento,IdPeriodo,User });
               
                return true;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
