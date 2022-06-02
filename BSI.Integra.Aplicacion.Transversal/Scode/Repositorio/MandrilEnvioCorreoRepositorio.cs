using System;
using BSI.Integra.Persistencia.Models;
using System.Collections.Generic;
using System.Linq.Expressions;
using AutoMapper;
using BSI.Integra.Persistencia.Repository;
using BSI.Integra.Aplicacion.Transversal.BO;
using Newtonsoft.Json;

namespace BSI.Integra.Aplicacion.Transversal.Repositorio
{
    /// Repositorio: Marketing/MandrilEnvioCorreo
    /// Autor: Joao Benavente - Wilber Choque - Ansoli Espinoza - Gian Miranda
    /// Fecha: 05/02/2021
    /// <summary>
    /// Repositorio para consultas de mkt.T_MandrilEnvioCorreo
    /// </summary>
    public class MandrilEnvioCorreoRepositorio : BaseRepository<TMandrilEnvioCorreo, MandrilEnvioCorreoBO>
    {
        #region Metodos Base
        public MandrilEnvioCorreoRepositorio() : base()
        {
        }
        public MandrilEnvioCorreoRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<MandrilEnvioCorreoBO> GetBy(Expression<Func<TMandrilEnvioCorreo, bool>> filter)
        {
            IEnumerable<TMandrilEnvioCorreo> listado = base.GetBy(filter);
            List<MandrilEnvioCorreoBO> listadoBO = new List<MandrilEnvioCorreoBO>();
            foreach (var itemEntidad in listado)
            {
                MandrilEnvioCorreoBO objetoBO = Mapper.Map<TMandrilEnvioCorreo, MandrilEnvioCorreoBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public MandrilEnvioCorreoBO FirstById(int id)
        {
            try
            {
                TMandrilEnvioCorreo entidad = base.FirstById(id);
                MandrilEnvioCorreoBO objetoBO = new MandrilEnvioCorreoBO();
                Mapper.Map<TMandrilEnvioCorreo, MandrilEnvioCorreoBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public MandrilEnvioCorreoBO FirstBy(Expression<Func<TMandrilEnvioCorreo, bool>> filter)
        {
            try
            {
                TMandrilEnvioCorreo entidad = base.FirstBy(filter);
                MandrilEnvioCorreoBO objetoBO = Mapper.Map<TMandrilEnvioCorreo, MandrilEnvioCorreoBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(MandrilEnvioCorreoBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TMandrilEnvioCorreo entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<MandrilEnvioCorreoBO> listadoBO)
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

        public bool Update(MandrilEnvioCorreoBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TMandrilEnvioCorreo entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<MandrilEnvioCorreoBO> listadoBO)
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
        private void AsignacionId(TMandrilEnvioCorreo entidad, MandrilEnvioCorreoBO objetoBO)
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

        private TMandrilEnvioCorreo MapeoEntidad(MandrilEnvioCorreoBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TMandrilEnvioCorreo entidad = new TMandrilEnvioCorreo();
                entidad = Mapper.Map<MandrilEnvioCorreoBO, TMandrilEnvioCorreo>(objetoBO,
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
        /// Inserta Mandril envio correo ingresando una lista de BO del mismo tipo
        /// </summary>
        /// <param name="listaMandrilEnvioCorreo">Lista de objeto de tipo MandrilEnvioCorreoBO</param>
        /// <returns>Booleano o excepcion segun el resultado de la transaccion</returns>

        public List<MandrilEnvioCorreoBO> InsertarMandrilEnvioCorreo(List<MandrilEnvioCorreoBO> listaMandrilEnvioCorreo)
        {
            List<MandrilEnvioCorreoBO> listaMandrilEnvioCorreoResultado = new List<MandrilEnvioCorreoBO>();

            try
            {
                string spQuery = "[mkt].[SP_InsertarMandrilEnvioCorreo]";

                foreach (var filtro in listaMandrilEnvioCorreo)
                {
                    try
                    {
                        var resultadoIndividual = new MandrilEnvioCorreoBO();
                        var query = _dapper.QuerySPFirstOrDefault(spQuery, new
                        {
                            filtro.IdOportunidad,
                            filtro.IdPersonal,
                            filtro.IdAlumno,
                            filtro.IdCentroCosto,
                            filtro.IdMandrilTipoAsignacion,
                            filtro.EstadoEnvio,
                            filtro.IdMandrilTipoEnvio,
                            filtro.FechaEnvio,
                            filtro.Asunto,
                            filtro.FkMandril,
                            filtro.UsuarioCreacion,
                            filtro.UsuarioModificacion,
                            filtro.EsEnvioMasivo
                        });

                        if (!string.IsNullOrEmpty(query) && !query.Contains("[]"))
                        {
                            resultadoIndividual = JsonConvert.DeserializeObject<MandrilEnvioCorreoBO>(query);
                        }

                        listaMandrilEnvioCorreoResultado.Add(resultadoIndividual);
                    }
                    catch (Exception e)
                    {
                        continue;
                    }
                }

                return listaMandrilEnvioCorreoResultado;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
