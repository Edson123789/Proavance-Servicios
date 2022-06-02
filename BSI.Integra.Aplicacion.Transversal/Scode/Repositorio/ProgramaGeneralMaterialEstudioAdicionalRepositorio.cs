using AutoMapper;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace BSI.Integra.Aplicacion.Transversal.Repositorio
{
    /// Repositorio: Transversal/ProgramaGeneralMaterialEstudioAdicional
    /// Autor : Lourdes Priscila Pacsi Gamboa
    /// Fecha: 19/06/2021
    /// <summary>
    /// Repositorio para consultas de la tabla pla.T_ProgramaGeneralMaterialEstudioAdiciona
    /// </summary>
    public class ProgramaGeneralMaterialEstudioAdicionalRepositorio : BaseRepository<TProgramaGeneralMaterialEstudioAdicional, ProgramaGeneralMaterialEstudioAdicionalBO>
    {
        #region Metodos Base
        public ProgramaGeneralMaterialEstudioAdicionalRepositorio() : base()
        {
        }
        public ProgramaGeneralMaterialEstudioAdicionalRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<ProgramaGeneralMaterialEstudioAdicionalBO> GetBy(Expression<Func<TProgramaGeneralMaterialEstudioAdicional, bool>> filter)
        {
            IEnumerable<TProgramaGeneralMaterialEstudioAdicional> listado = base.GetBy(filter);
            List<ProgramaGeneralMaterialEstudioAdicionalBO> listadoBO = new List<ProgramaGeneralMaterialEstudioAdicionalBO>();
            foreach (var itemEntidad in listado)
            {
                ProgramaGeneralMaterialEstudioAdicionalBO objetoBO = Mapper.Map<TProgramaGeneralMaterialEstudioAdicional, ProgramaGeneralMaterialEstudioAdicionalBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public ProgramaGeneralMaterialEstudioAdicionalBO FirstById(int id)
        {
            try
            {
                TProgramaGeneralMaterialEstudioAdicional entidad = base.FirstById(id);
                ProgramaGeneralMaterialEstudioAdicionalBO objetoBO = new ProgramaGeneralMaterialEstudioAdicionalBO();
                Mapper.Map<TProgramaGeneralMaterialEstudioAdicional, ProgramaGeneralMaterialEstudioAdicionalBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public ProgramaGeneralMaterialEstudioAdicionalBO FirstBy(Expression<Func<TProgramaGeneralMaterialEstudioAdicional, bool>> filter)
        {
            try
            {
                TProgramaGeneralMaterialEstudioAdicional entidad = base.FirstBy(filter);
                ProgramaGeneralMaterialEstudioAdicionalBO objetoBO = Mapper.Map<TProgramaGeneralMaterialEstudioAdicional, ProgramaGeneralMaterialEstudioAdicionalBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(ProgramaGeneralMaterialEstudioAdicionalBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TProgramaGeneralMaterialEstudioAdicional entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<ProgramaGeneralMaterialEstudioAdicionalBO> listadoBO)
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

        public bool Update(ProgramaGeneralMaterialEstudioAdicionalBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TProgramaGeneralMaterialEstudioAdicional entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<ProgramaGeneralMaterialEstudioAdicionalBO> listadoBO)
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
        private void AsignacionId(TProgramaGeneralMaterialEstudioAdicional entidad, ProgramaGeneralMaterialEstudioAdicionalBO objetoBO)
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

        private TProgramaGeneralMaterialEstudioAdicional MapeoEntidad(ProgramaGeneralMaterialEstudioAdicionalBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TProgramaGeneralMaterialEstudioAdicional entidad = new TProgramaGeneralMaterialEstudioAdicional();
                entidad = Mapper.Map<ProgramaGeneralMaterialEstudioAdicionalBO, TProgramaGeneralMaterialEstudioAdicional>(objetoBO,
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

        //obtener Filtros por capitulo
        public List<ListaProgramaGeneralMaterialEstudioAdicionalBO> ListaProgramaGeneralMaterialEstudioAdicional()
        {
            try
            {
                List<ListaProgramaGeneralMaterialEstudioAdicionalBO> capitulosFiltro = new List<ListaProgramaGeneralMaterialEstudioAdicionalBO>();
                //Select IdPGeneral, Nombre, Titulo, Contenido, NombreTitulo, NroCapitulo From pla.V_ListadoEstructuraProgramaCapitulosV2 Where IdPGeneral = @IdPGeneral
                var _queryfiltrocapitulo = "Select IdPGeneral,Nombre FROM pla.V_ProgramaGeneralMaterialEstudioAdicional";
                var SubfiltroCapitulo = _dapper.QueryDapper(_queryfiltrocapitulo, new { });
                if (!string.IsNullOrEmpty(SubfiltroCapitulo) && !SubfiltroCapitulo.Contains("[]"))
                {
                    capitulosFiltro = JsonConvert.DeserializeObject<List<ListaProgramaGeneralMaterialEstudioAdicionalBO>>(SubfiltroCapitulo);
                }
                return capitulosFiltro;
            }
            catch (Exception E)
            {
                throw new Exception(E.Message);
            }

        }

        public List<RegistroProgramaGeneralMaterialEstudioAdicionalBO> RegistroProgramaGeneralMaterialEstudioAdicional(int IdPGeneral)
        {
            try
            {
                List<RegistroProgramaGeneralMaterialEstudioAdicionalBO> capitulosFiltro = new List<RegistroProgramaGeneralMaterialEstudioAdicionalBO>();
                //Select IdPGeneral, Nombre, Titulo, Contenido, NombreTitulo, NroCapitulo From pla.V_ListadoEstructuraProgramaCapitulosV2 Where IdPGeneral = @IdPGeneral
                var _queryfiltrocapitulo = "Select Id,IdPGeneral,NombreArchivo,EnlaceArchivo,EsEnlace FROM pla.T_ProgramaGeneralMaterialEstudioAdicional Where Estado = 1 AND IdPGeneral=@IdPGeneral";
                var SubfiltroCapitulo = _dapper.QueryDapper(_queryfiltrocapitulo, new { IdPGeneral = IdPGeneral });
                if (!string.IsNullOrEmpty(SubfiltroCapitulo) && !SubfiltroCapitulo.Contains("[]"))
                {
                    capitulosFiltro = JsonConvert.DeserializeObject<List<RegistroProgramaGeneralMaterialEstudioAdicionalBO>>(SubfiltroCapitulo);
                }
                return capitulosFiltro;
            }
            catch (Exception E)
            {
                throw new Exception(E.Message);
            }

        }

        public bool EliminarProgramaGeneralMaterialEstudioAdicional(int IdPGeneral, string UsuarioModificacion, DateTime FechaModificacion)
        {
            try
            {
                List<RegistroProgramaGeneralMaterialEstudioAdicionalBO> capitulosFiltro = new List<RegistroProgramaGeneralMaterialEstudioAdicionalBO>();
                //Select IdPGeneral, Nombre, Titulo, Contenido, NombreTitulo, NroCapitulo From pla.V_ListadoEstructuraProgramaCapitulosV2 Where IdPGeneral = @IdPGeneral
                var _queryfiltrocapitulo = "Update pla.T_ProgramaGeneralMaterialEstudioAdicional Set UsuarioModificacion = @UsuarioModificacion, FechaModificacion = @FechaModificacion, Estado = 0 Where Estado = 1 AND IdPGeneral=@IdPGeneral";
                var SubfiltroCapitulo = _dapper.QueryDapper(_queryfiltrocapitulo, new { UsuarioModificacion = UsuarioModificacion, FechaModificacion = FechaModificacion, IdPGeneral = IdPGeneral });
                
                return true;
            }
            catch (Exception E)
            {
                return false;
            }

        }

        public List<RegistroProgramaGeneralMaterialEstudioAdicionalBO> RegistroProgramaGeneralMaterialEstudioAdicionalPEspecifico(int IdPGeneral)
        {
            try
            {
                List<RegistroProgramaGeneralMaterialEstudioAdicionalBO> capitulosFiltro = new List<RegistroProgramaGeneralMaterialEstudioAdicionalBO>();
                //Select IdPGeneral, Nombre, Titulo, Contenido, NombreTitulo, NroCapitulo From pla.V_ListadoEstructuraProgramaCapitulosV2 Where IdPGeneral = @IdPGeneral
                var _queryfiltrocapitulo = "Select IdPEspecifico FROM pla.T_ProgramaGeneralMaterialEstudioAdicionalEspecificos Where Estado = 1 AND IdPGeneral=@IdPGeneral";
                var SubfiltroCapitulo = _dapper.QueryDapper(_queryfiltrocapitulo, new { IdPGeneral = IdPGeneral });
                if (!string.IsNullOrEmpty(SubfiltroCapitulo) && !SubfiltroCapitulo.Contains("[]"))
                {
                    capitulosFiltro = JsonConvert.DeserializeObject<List<RegistroProgramaGeneralMaterialEstudioAdicionalBO>>(SubfiltroCapitulo);
                }
                return capitulosFiltro;
            }
            catch (Exception E)
            {
                throw new Exception(E.Message);
            }

        }
    }
}
