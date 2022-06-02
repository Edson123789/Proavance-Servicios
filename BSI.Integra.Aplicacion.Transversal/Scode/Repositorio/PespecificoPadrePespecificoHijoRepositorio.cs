using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using AutoMapper;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using Newtonsoft.Json;

namespace BSI.Integra.Aplicacion.Transversal.Repositorio
{
    /// Repositorio: Transversal/PespecificoPadrePespecificoHijoRepositorio
    /// Autor: Fischer Valdez - Luis Huallpa - Ansoli Espinoza
    /// Fecha: 22/06/2021
    /// <summary>
    /// Gestión de Examenes tabla T_PespecificoPadrePespecificoHijo
    /// </summary>
    public class PespecificoPadrePespecificoHijoRepositorio : BaseRepository<TPespecificoPadrePespecificoHijo, PespecificoPadrePespecificoHijoBO>
    {
        #region Metodos Base
        public PespecificoPadrePespecificoHijoRepositorio() : base()
        {
        }
        public PespecificoPadrePespecificoHijoRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<PespecificoPadrePespecificoHijoBO> GetBy(Expression<Func<TPespecificoPadrePespecificoHijo, bool>> filter)
        {
            IEnumerable<TPespecificoPadrePespecificoHijo> listado = base.GetBy(filter);
            List<PespecificoPadrePespecificoHijoBO> listadoBO = new List<PespecificoPadrePespecificoHijoBO>();
            foreach (var itemEntidad in listado)
            {
                PespecificoPadrePespecificoHijoBO objetoBO = Mapper.Map<TPespecificoPadrePespecificoHijo, PespecificoPadrePespecificoHijoBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public PespecificoPadrePespecificoHijoBO FirstById(int id)
        {
            try
            {
                TPespecificoPadrePespecificoHijo entidad = base.FirstById(id);
                PespecificoPadrePespecificoHijoBO objetoBO = new PespecificoPadrePespecificoHijoBO();
                Mapper.Map<TPespecificoPadrePespecificoHijo, PespecificoPadrePespecificoHijoBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public PespecificoPadrePespecificoHijoBO FirstBy(Expression<Func<TPespecificoPadrePespecificoHijo, bool>> filter)
        {
            try
            {
                TPespecificoPadrePespecificoHijo entidad = base.FirstBy(filter);
                PespecificoPadrePespecificoHijoBO objetoBO = Mapper.Map<TPespecificoPadrePespecificoHijo, PespecificoPadrePespecificoHijoBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(PespecificoPadrePespecificoHijoBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TPespecificoPadrePespecificoHijo entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<PespecificoPadrePespecificoHijoBO> listadoBO)
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

        public bool Update(PespecificoPadrePespecificoHijoBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TPespecificoPadrePespecificoHijo entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<PespecificoPadrePespecificoHijoBO> listadoBO)
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
        private void AsignacionId(TPespecificoPadrePespecificoHijo entidad, PespecificoPadrePespecificoHijoBO objetoBO)
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

        private TPespecificoPadrePespecificoHijo MapeoEntidad(PespecificoPadrePespecificoHijoBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TPespecificoPadrePespecificoHijo entidad = new TPespecificoPadrePespecificoHijo();
                entidad = Mapper.Map<PespecificoPadrePespecificoHijoBO, TPespecificoPadrePespecificoHijo>(objetoBO,
                    opt => opt.ConfigureMap(MemberList.None));

                //mapea los hijos

                return entidad;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public IEnumerable<PespecificoPadrePespecificoHijoBO> GetFiltered<KProperty>(IEnumerable<Expression<Func<TPespecificoPadrePespecificoHijo, bool>>> filters, Expression<Func<TPespecificoPadrePespecificoHijo, KProperty>> orderBy, bool ascending)
        {
            IEnumerable<TPespecificoPadrePespecificoHijo> listado = base.GetFiltered(filters, orderBy, ascending);
            List<PespecificoPadrePespecificoHijoBO> listadoBO = new List<PespecificoPadrePespecificoHijoBO>();

            foreach (var itemEntidad in listado)
            {
                PespecificoPadrePespecificoHijoBO objetoBO = Mapper.Map<TPespecificoPadrePespecificoHijo, PespecificoPadrePespecificoHijoBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }
            return listadoBO;
        }
        #endregion
        /// <summary>
        /// Obtiene Todos Los programas Especificos Hijos
        /// </summary>
        /// <param name="idPespecifico"></param>
        /// <returns></returns>
        public List<DatosProgramaespecificoHijosDTO> ObtenerPespecificosHijos(int idPespecifico)
        {
            string _queryListaPespecificoHijo = "Select PEspecificoHijoId,IdProgramaGeneral,Nombre,Duracion,IdCiudad,TipoAmbiente From pla.V_DetallePespecificoFrecuancia Where Estado=1 and PEspecificoPadreId=@IdPespecifico";
            var queryListaPespecificoHijo = _dapper.QueryDapper(_queryListaPespecificoHijo, new { IdPespecifico = idPespecifico });
            return JsonConvert.DeserializeObject<List<DatosProgramaespecificoHijosDTO>>(queryListaPespecificoHijo);

        }
        /// <summary>
        /// Obtiene toda la informacion de los programas hijos
        /// </summary>
        /// <param name="idPespecifico"></param>
        /// <returns></returns>
        public List<ListainformacionProgramaEspecificoHijoDTO> ObtenerInformacionPespecificosHijos(int idPespecifico)
        {
            string _queryListaPespecificoHijo = "Select Id,Nombre,IdProgramaGeneral,Duracion,IdCiudad,TipoAmbiente,IdAmbiente From pla.V_ListaProgramaEspecificoHijo Where Estado=1 and PEspecificoPadreId=@IdPespecifico";
            var queryListaPespecificoHijo = _dapper.QueryDapper(_queryListaPespecificoHijo, new { IdPespecifico = idPespecifico });
            return JsonConvert.DeserializeObject<List<ListainformacionProgramaEspecificoHijoDTO>>(queryListaPespecificoHijo);
        }
        /// <summary>
        /// Obtiene toda la informacion de los programas hijos
        /// </summary>
        /// <param name="idPespecifico"></param>
        /// <returns></returns>
        public List<ListainformacionProgramaEspecificoHijoDTO> ObtenerInformacionPespecificoSesion(int idPespecifico)
        {
            string _queryListaPespecificoHijo = "Select Id,Nombre,IdProgramaGeneral,Duracion,IdCiudad,TipoAmbiente,IdAmbiente From pla.V_ListaProgramaEspecificoHijo Where Estado=1 and ID=@IdPespecifico";
            var queryListaPespecificoHijo = _dapper.QueryDapper(_queryListaPespecificoHijo, new { IdPespecifico = idPespecifico });
            return JsonConvert.DeserializeObject<List<ListainformacionProgramaEspecificoHijoDTO>>(queryListaPespecificoHijo);
        }
        /// <summary>
        /// Obtiene Informacion  de los Programas Relacionados
        /// </summary>
        /// <param name="idPespecifico"></param>
        /// <returns></returns>
        public List<ListainformacionProgramaEspecificoHijoDTO> ObtenerPespecificosRelacionados(int idPespecifico)
        {
			try
			{
				string _queryListaPespecificoHijo = "Select Id,Nombre,IdProgramaGeneral,Duracion,IdCiudad,TipoAmbiente,IdAmbiente, IdExpositor_Referencia, FechaHoraInicio, IdCentroCosto, IdProveedor, IdEstadoPEspecifico, IdModalidadCurso,IdCursoMoodle,IdCursoMoodlePrueba, Codigo From pla.V_ListaProgramaEspecificoHijo Where Estado=1 and PEspecificoPadreId=@IdPespecifico";
				var queryListaPespecificoHijo = _dapper.QueryDapper(_queryListaPespecificoHijo, new { IdPespecifico = idPespecifico });
				return JsonConvert.DeserializeObject<List<ListainformacionProgramaEspecificoHijoDTO>>(queryListaPespecificoHijo);
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
            
        }
        /// <summary>
        /// Obtiene Id del Programa Especifico Padre
        /// </summary>
        /// <returns></returns>
        public PespecificoPadreDTO ObtenerPespecificoPadre(int idPespecifico)
        {
            string _queryPespecificoPadre = "Select PEspecificoPadreId From pla.V_TPEspecificoPadrePEspecificoHijoPorHijo Where Estado=1 and PEspecificoPadreId=@IdPespecifico";
            var queryPespecificoPadre = _dapper.FirstOrDefault(_queryPespecificoPadre, new { IdPespecifico = idPespecifico });
            return JsonConvert.DeserializeObject<PespecificoPadreDTO>(queryPespecificoPadre);
        }

        /// <summary>
        ///  Obtiene todos los registros sin los campos de auditoría.
        /// </summary>
        /// <returns></returns>
        public List<PespecificoPadrePespecificoHijoDTO> ObtenerTodoGrid()
        {
            try
            {
                var lista = GetBy(x => true, y => new PespecificoPadrePespecificoHijoDTO
                {
                    Id = y.Id,
                    PEspecificoPadreId = y.PespecificoPadreId,
                    PEspecificoHijoId = y.PespecificoHijoId,
                }).OrderByDescending(x => x.Id).ToList();

                return lista;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// <summary>
        /// Obtiene Informacion  de los Programas Relacionados
        /// </summary>
        /// <param name="idPespecifico"></param>
        /// <returns></returns>
        public List<MatriculaProgramaHijoDTO> ObtenerProgramasHijos(string CodigoMatricula)
        {
            try
            {
                string _queryListaPespecificoHijo = "Select IdPespecifico,IdPespecificoHijo,IdCentroCosto,NombreCentroCosto" +
                                                    " From ope.V_ProgramasHijosPorMatricula Where CodigoMatricula = @CodigoMatricula";
                var queryListaPespecificoHijo = _dapper.QueryDapper(_queryListaPespecificoHijo, new { CodigoMatricula });
                return JsonConvert.DeserializeObject<List<MatriculaProgramaHijoDTO>>(queryListaPespecificoHijo);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }
    }
}
