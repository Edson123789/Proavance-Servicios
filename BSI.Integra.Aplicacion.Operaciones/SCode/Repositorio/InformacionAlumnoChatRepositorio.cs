using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using AutoMapper;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Operaciones.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using Newtonsoft.Json;

namespace BSI.Integra.Aplicacion.Operaciones.Repositorio
{
    /// Repositorio: InformacionAlumnoChatRepositorio
    /// Autor: Jose Villena
    /// Fecha: 16/07/2021
    /// <summary>
    /// Gestion con la base de datos de la tabla ope.T_InformacionAlumnoChat
    /// </summary>
    public class InformacionAlumnoChatRepositorio : BaseRepository<TInformacionAlumnoChat, InformacionAlumnoChatBO>
    {
        #region Metodos Base
        public InformacionAlumnoChatRepositorio() : base()
        {
        }
        public InformacionAlumnoChatRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<InformacionAlumnoChatBO> GetBy(Expression<Func<TInformacionAlumnoChat, bool>> filter)
        {
            IEnumerable<TInformacionAlumnoChat> listado = base.GetBy(filter);
            List<InformacionAlumnoChatBO> listadoBO = new List<InformacionAlumnoChatBO>();
            foreach (var itemEntidad in listado)
            {
                InformacionAlumnoChatBO objetoBO = Mapper.Map<TInformacionAlumnoChat, InformacionAlumnoChatBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public InformacionAlumnoChatBO FirstById(int id)
        {
            try
            {
                TInformacionAlumnoChat entidad = base.FirstById(id);
                InformacionAlumnoChatBO objetoBO = new InformacionAlumnoChatBO();
                Mapper.Map<TInformacionAlumnoChat, InformacionAlumnoChatBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public InformacionAlumnoChatBO FirstBy(Expression<Func<TInformacionAlumnoChat, bool>> filter)
        {
            try
            {
                TInformacionAlumnoChat entidad = base.FirstBy(filter);
                InformacionAlumnoChatBO objetoBO = Mapper.Map<TInformacionAlumnoChat, InformacionAlumnoChatBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(InformacionAlumnoChatBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TInformacionAlumnoChat entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<InformacionAlumnoChatBO> listadoBO)
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

        public bool Update(InformacionAlumnoChatBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TInformacionAlumnoChat entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<InformacionAlumnoChatBO> listadoBO)
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
        private void AsignacionId(TInformacionAlumnoChat entidad, InformacionAlumnoChatBO objetoBO)
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

        private TInformacionAlumnoChat MapeoEntidad(InformacionAlumnoChatBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TInformacionAlumnoChat entidad = new TInformacionAlumnoChat();
                entidad = Mapper.Map<InformacionAlumnoChatBO, TInformacionAlumnoChat>(objetoBO,
                    opt => opt.ConfigureMap(MemberList.None));

                //mapea los hijos

                return entidad;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public IEnumerable<InformacionAlumnoChatBO> GetFiltered<KProperty>(IEnumerable<Expression<Func<TInformacionAlumnoChat, bool>>> filters, Expression<Func<TInformacionAlumnoChat, KProperty>> orderBy, bool ascending)
        {
            IEnumerable<TInformacionAlumnoChat> listado = base.GetFiltered(filters, orderBy, ascending);
            List<InformacionAlumnoChatBO> listadoBO = new List<InformacionAlumnoChatBO>();

            foreach (var itemEntidad in listado)
            {
                InformacionAlumnoChatBO objetoBO = Mapper.Map<TInformacionAlumnoChat, InformacionAlumnoChatBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }
            return listadoBO;
        }
        #endregion

        
        public InformacionChatSoporteAlumnoDTO obtenerInformacionAlumnoChatSoporte(int Id)
        {
            try
            {
                //string _queryPrograma = "Select CodigoMatricula, ProgramaGeneralSoporte, ProgramaGeneralCurso,CentroCosto,Coordinadora,Capitulo,Sesion From [ope].[V_ObtenerInformacionAlumnoChatSoporte] Where IdMatriculaCabecera=@IdMatriculaCabecera";
                string _queryPrograma = "Select CodigoMatricula, ProgramaGeneralSoporte,CentroCosto,Coordinadora From [ope].[V_ObtenerInformacionAlumnoChatSoporte] Where Id=@Id";
                var queryPrograma = _dapper.FirstOrDefault(_queryPrograma, new { Id });
                var informacion = JsonConvert.DeserializeObject<InformacionChatSoporteAlumnoDTO>(queryPrograma);

                return informacion;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        public InformacionChatSoporteAlumnoDTO obtenerUltimaInformacionAlumno(int IdAlumno)
        {
            try
            {                
                string _queryPrograma = "Select Top 1 CodigoMatricula, ProgramaGeneralSoporte,CentroCosto,Coordinadora From [ope].[V_ObtenerUltimaInformacionAlumnoChatSoporte] Where IdAlumno=@IdAlumno";
                var queryPrograma = _dapper.FirstOrDefault(_queryPrograma, new { IdAlumno });
                var informacion = JsonConvert.DeserializeObject<InformacionChatSoporteAlumnoDTO>(queryPrograma);

                return informacion;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        public InformacionLogChatSoporteAlumnoDTO obtenerLogInformacion(int IdAlumno)
        {
            try
            {
                string _queryPrograma = "Select top 1 IdMatriculaCabecera,IdAlumno,IdProgramaGeneral_Padre,IdProgramaGeneral_Hijo,IdCentroCosto,IdCapitulo,IdSesion From ope.T_informacionAlumnoChatLog Where IdAlumno=@IdAlumno and IdProgramaGeneral_Padre !=0 and IdProgramaGeneral_Hijo!=0 ORDER BY FechaCreacion DESC";
                var queryPrograma = _dapper.FirstOrDefault(_queryPrograma, new { IdAlumno });
                var informacion = JsonConvert.DeserializeObject<InformacionLogChatSoporteAlumnoDTO>(queryPrograma);

                return informacion;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        public InformacionChatSoporteAlumnoDTO obtenerCapituloAlumnoChatSoporte(int IdPGeneral, int IdCapitulo)
        {
            try
            {                
                string _queryPrograma = "Select Capitulo From [ope].[V_ObtenerCapituloAlumnoChatSoporte] Where IdPGeneral=@IdPGeneral and IdCapitulo=@IdCapitulo";
                var queryPrograma = _dapper.FirstOrDefault(_queryPrograma, new { IdPGeneral= IdPGeneral, IdCapitulo= IdCapitulo });
                var informacion = JsonConvert.DeserializeObject<InformacionChatSoporteAlumnoDTO>(queryPrograma);

                return informacion;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        public InformacionChatSoporteAlumnoDTO obtenerSesionAlumnoChatSoporte(int IdPGeneral, int IdSesion)
        {
            try
            {
                string _queryPrograma = "Select Sesion From [ope].[V_ObtenerSesionAlumnoChatSoporte] Where IdPGeneral=@IdPGeneral and IdSesion=@IdSesion";
                var queryPrograma = _dapper.FirstOrDefault(_queryPrograma, new { IdPGeneral = IdPGeneral, IdSesion = IdSesion });
                var informacion = JsonConvert.DeserializeObject<InformacionChatSoporteAlumnoDTO>(queryPrograma);

                return informacion;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        public InformacionChatSoporteAlumnoDTO obtenerNombreAlumnoChatSoporte(int IdAlumno)
        {
            try
            {
                string _queryPrograma = "SELECT CONCAT(ISNULL(Nombre1,Nombre2),' ',ISNULL(ApellidoPaterno,'')) AS NombreAlumno,Celular AS Celular, email1 AS Correo FROM mkt.T_Alumno WHERE Id=@IdAlumno and Estado=1";
                var queryPrograma = _dapper.FirstOrDefault(_queryPrograma, new { IdAlumno});
                var informacion = JsonConvert.DeserializeObject<InformacionChatSoporteAlumnoDTO>(queryPrograma);

                return informacion;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

    }
}
