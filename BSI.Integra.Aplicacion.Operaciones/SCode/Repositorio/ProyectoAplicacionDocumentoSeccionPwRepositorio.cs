using AutoMapper;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.DTOs.Operaciones;
using BSI.Integra.Aplicacion.Operaciones.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace BSI.Integra.Aplicacion.Operaciones.Repositorio
{
    public class ProyectoAplicacionDocumentoSeccionPwRepositorio : BaseRepository<TProyectoAplicacionDocumentoSeccionPw, ProyectoAplicacionDocumentoSeccionPwBO>
    {
        #region Metodos Base
        public ProyectoAplicacionDocumentoSeccionPwRepositorio() : base()
        {
        }
        public ProyectoAplicacionDocumentoSeccionPwRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<ProyectoAplicacionDocumentoSeccionPwBO> GetBy(Expression<Func<TProyectoAplicacionDocumentoSeccionPw, bool>> filter)
        {
            IEnumerable<TProyectoAplicacionDocumentoSeccionPw> listado = base.GetBy(filter);
            List<ProyectoAplicacionDocumentoSeccionPwBO> listadoBO = new List<ProyectoAplicacionDocumentoSeccionPwBO>();
            foreach (var itemEntidad in listado)
            {
                ProyectoAplicacionDocumentoSeccionPwBO objetoBO = Mapper.Map<TProyectoAplicacionDocumentoSeccionPw, ProyectoAplicacionDocumentoSeccionPwBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public ProyectoAplicacionDocumentoSeccionPwBO FirstById(int id)
        {
            try
            {
                TProyectoAplicacionDocumentoSeccionPw entidad = base.FirstById(id);
                ProyectoAplicacionDocumentoSeccionPwBO objetoBO = new ProyectoAplicacionDocumentoSeccionPwBO();
                Mapper.Map<TProyectoAplicacionDocumentoSeccionPw, ProyectoAplicacionDocumentoSeccionPwBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public ProyectoAplicacionDocumentoSeccionPwBO FirstBy(Expression<Func<TProyectoAplicacionDocumentoSeccionPw, bool>> filter)
        {
            try
            {
                TProyectoAplicacionDocumentoSeccionPw entidad = base.FirstBy(filter);
                ProyectoAplicacionDocumentoSeccionPwBO objetoBO = Mapper.Map<TProyectoAplicacionDocumentoSeccionPw, ProyectoAplicacionDocumentoSeccionPwBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(ProyectoAplicacionDocumentoSeccionPwBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TProyectoAplicacionDocumentoSeccionPw entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<ProyectoAplicacionDocumentoSeccionPwBO> listadoBO)
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

        public bool Update(ProyectoAplicacionDocumentoSeccionPwBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TProyectoAplicacionDocumentoSeccionPw entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<ProyectoAplicacionDocumentoSeccionPwBO> listadoBO)
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
        private void AsignacionId(TProyectoAplicacionDocumentoSeccionPw entidad, ProyectoAplicacionDocumentoSeccionPwBO objetoBO)
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

        private TProyectoAplicacionDocumentoSeccionPw MapeoEntidad(ProyectoAplicacionDocumentoSeccionPwBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TProyectoAplicacionDocumentoSeccionPw entidad = new TProyectoAplicacionDocumentoSeccionPw();
                entidad = Mapper.Map<ProyectoAplicacionDocumentoSeccionPwBO, TProyectoAplicacionDocumentoSeccionPw>(objetoBO,
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

        public List<ProyectoAplicacionEntregadoDetalleDTO> ObtenerEnviarProyectoAplicacionPorAlumno (int IdAlumno)
        {
            try
            {
                string sql_query = "select Id,Nombre,EnlaceProyecto,Version,FechaEnvio,IdMatriculaCabecera from pla.V_ObtenerProyectoAplicacionPorIdAlumno where IdAlumno = @IdAlumno Order by IdMatriculaCabecera, Version asc";
                var query = _dapper.QueryDapper(sql_query, new { IdAlumno = IdAlumno });

                var res = JsonConvert.DeserializeObject<List<ProyectoAplicacionEntregadoDetalleDTO>>(query);

                return res;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Obtiene el IdMatricula de Operaciones
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int ObtenerIdMatricula(int idMatriculaCabecera)
        {
            try
            {
                var _resultado = new ValorIntDTO();
                var query = $@"
                              SELECT IdMatriculaCabecera as Valor FROM ope.T_PEspecificoMatriculaAlumno where Id = @IdMatriculaCabecera
                            ";
                var resultado = _dapper.FirstOrDefault(query, new { IdMatriculaCabecera = idMatriculaCabecera });

                if (!string.IsNullOrEmpty(resultado))
                {
                    _resultado = JsonConvert.DeserializeObject<ValorIntDTO>(resultado);
                }
                return _resultado.Valor;

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Obtiene el Id de T_ProyectoAplicacionEntregaVersionPw
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int ObtenerIdProyectoAplicacionEntregaVersionPw(int idMatriculaCabecera)
        {
            try
            {
                var _resultado = new ValorIntDTO();
                var query = $@"
                              SELECT top 1 Id as Valor FROM pla.T_ProyectoAplicacionEntregaVersionPw where IdMatriculaCabecera = @IdMatriculaCabecera and Estado = 1 order by FechaCreacion desc
                            ";
                var resultado = _dapper.FirstOrDefault(query, new { IdMatriculaCabecera = idMatriculaCabecera });

                if (!string.IsNullOrEmpty(resultado))
                {
                    _resultado = JsonConvert.DeserializeObject<ValorIntDTO>(resultado);
                }
                return _resultado.Valor;

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Obtiene el IdDocumentoSeccionPw de Operaciones
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int ObtenerIdDocumentoSeccionPw(int idPlantillaPw, int idDocumentoPw,int idSeccionPw, int IdSeccionTipoDestallePw, int numeroFila)
        {
            try
            {
                var _resultado = new ValorIntDTO();
                var query = $@"
                              SELECT Id as Valor FROM pla.T_DocumentoSeccion_PW where IdPlantillaPW = @IdPlantillaPW and Estado = 1 and IdDocumentoPW = @IdDocumentoPW and IdSeccionPW = @IdSeccionPW and IdSeccionTipoDetalle_PW = @IdSeccionTipoDetalle_PW and  NumeroFila = @NumeroFila
                            ";
                var resultado = _dapper.FirstOrDefault(query, new { IdPlantillaPW = idPlantillaPw, IdDocumentoPW = idDocumentoPw, IdSeccionPW = idSeccionPw, IdSeccionTipoDetalle_PW = IdSeccionTipoDestallePw, NumeroFila = numeroFila });

                if (!string.IsNullOrEmpty(resultado))
                {
                    _resultado = JsonConvert.DeserializeObject<ValorIntDTO>(resultado);
                }
                return _resultado.Valor;

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// <summary>
        /// Obtiene el NOmbreSeccionTipoDetallePw de Operaciones
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public string ObtenerNombreSeccionTipoDetallePw(int id)
        {
            try
            {
                var _resultado = new ValorStringDTO();
                var query = $@"
                              SELECT NombreTitulo as Valor FROM pla.T_SeccionTipoDetalle_PW  where Id = @Id and Estado = 1 
                            ";
                var resultado = _dapper.FirstOrDefault(query, new { Id = id });

                if (!string.IsNullOrEmpty(resultado))
                {
                    _resultado = JsonConvert.DeserializeObject<ValorStringDTO>(resultado);
                }
                return _resultado.Valor;

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Obtiene el IdDocumentoSeccionPw de Operaciones
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int ObtenerIdDocumentoSeccionPorTituloPw(int idPlantillaPw, int idDocumentoPw, string titulo)
        {
            try
            {
                var _resultado = new ValorIntDTO();
                var query = $@"
                              SELECT Id as Valor FROM pla.T_DocumentoSeccion_PW where IdPlantillaPW = @IdPlantillaPW and Estado = 1 and IdDocumentoPW = @IdDocumentoPW and Titulo = @Titulo
                            ";
                var resultado = _dapper.FirstOrDefault(query, new { IdPlantillaPW = idPlantillaPw, IdDocumentoPW = idDocumentoPw, Titulo = titulo });

                if (!string.IsNullOrEmpty(resultado))
                {
                    _resultado = JsonConvert.DeserializeObject<ValorIntDTO>(resultado);
                }
                return _resultado.Valor;

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }


    }
}
