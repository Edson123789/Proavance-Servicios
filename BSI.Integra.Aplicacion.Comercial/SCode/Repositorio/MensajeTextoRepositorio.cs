using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using AutoMapper;
using BSI.Integra.Aplicacion.Comercial.BO;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.DTOs.Transversal;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using Newtonsoft.Json;

namespace BSI.Integra.Aplicacion.Comercial.Repositorio
{
    public class MensajeTextoRepositorio : BaseRepository<TMensajeTexto, MensajeTextoBO>
    {
        #region Metodos Base
        public MensajeTextoRepositorio() : base()
        {
        }
        public MensajeTextoRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<MensajeTextoBO> GetBy(Expression<Func<TMensajeTexto, bool>> filter)
        {
            IEnumerable<TMensajeTexto> listado = base.GetBy(filter);
            List<MensajeTextoBO> listadoBO = new List<MensajeTextoBO>();
            foreach (var itemEntidad in listado)
            {
                MensajeTextoBO objetoBO = Mapper.Map<TMensajeTexto, MensajeTextoBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public MensajeTextoBO FirstById(int id)
        {
            try
            {
                TMensajeTexto entidad = base.FirstById(id);
                MensajeTextoBO objetoBO = new MensajeTextoBO();
                Mapper.Map<TMensajeTexto, MensajeTextoBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public MensajeTextoBO FirstBy(Expression<Func<TMensajeTexto, bool>> filter)
        {
            try
            {
                TMensajeTexto entidad = base.FirstBy(filter);
                MensajeTextoBO objetoBO = Mapper.Map<TMensajeTexto, MensajeTextoBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(MensajeTextoBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TMensajeTexto entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<MensajeTextoBO> listadoBO)
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

        public bool Update(MensajeTextoBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TMensajeTexto entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<MensajeTextoBO> listadoBO)
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
        private void AsignacionId(TMensajeTexto entidad, MensajeTextoBO objetoBO)
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

        private TMensajeTexto MapeoEntidad(MensajeTextoBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TMensajeTexto entidad = new TMensajeTexto();
                entidad = Mapper.Map<MensajeTextoBO, TMensajeTexto>(objetoBO,
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
        /// Obtener Codigo Matricula segun el Id de una Oportunidad
        /// </summary>
        /// <param name="idOportunidad"></param>
        /// <returns></returns>
        public CodigoMatriculaDocumentoDTO ObtenerCodigoMatricula(int idOportunidad)
        {
            try
            {
                string _query = "SELECT Id, CodigoMatricula FROM com.V_TMontoPagoCronograma_CodigoMatricula WHERE Estado=1 AND IdOportunidad=@IdOportunidad";
                var matriculaDB = _dapper.FirstOrDefault(_query, new { IdOportunidad = idOportunidad });
                var datosMatricula = JsonConvert.DeserializeObject<CodigoMatriculaDocumentoDTO>(matriculaDB);
                if (!datosMatricula.CodigoMatricula.Equals("null"))
                {
                    return datosMatricula;
                }
                else
                {
                    throw new Exception("No Existe un Codigo Matricula para la Oportunidad con Identificador " + idOportunidad);
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// <summary>
        /// Obtiene Accesos del Portal para mandar por mensaje
        /// </summary>
        /// <param name="idOportunidad"></param>
        /// <returns></returns>
        public AccesoPortalWebDTO ObtenerAccesosPorEmail(int IdAlumno)
        {
            try
            {
                string _query = "SELECT UserName,Clave FROM com.V_AccesosPortalWeb_MensajeTexto WHERE Estado=1 AND Id=@IdAlumno";
                var matriculaDB = _dapper.FirstOrDefault(_query, new { IdAlumno });
                
                if (!matriculaDB.Equals("null"))
                {
                    var Accesos = JsonConvert.DeserializeObject<AccesoPortalWebDTO>(matriculaDB);
                    return Accesos;
                }
                else
                {
                    throw new Exception("No se Crearon los accesos para este Alumno " + IdAlumno);
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
      
    }
}
