using System;
using System.Collections.Generic;
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
    public class ProgramaGeneralPerfilInterceptoRepositorio : BaseRepository<TProgramaGeneralPerfilIntercepto, ProgramaGeneralPerfilInterceptoBO>
    {
        #region Metodos Base
        public ProgramaGeneralPerfilInterceptoRepositorio() : base()
        {
        }
        public ProgramaGeneralPerfilInterceptoRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<ProgramaGeneralPerfilInterceptoBO> GetBy(Expression<Func<TProgramaGeneralPerfilIntercepto, bool>> filter)
        {
            IEnumerable<TProgramaGeneralPerfilIntercepto> listado = base.GetBy(filter);
            List<ProgramaGeneralPerfilInterceptoBO> listadoBO = new List<ProgramaGeneralPerfilInterceptoBO>();
            foreach (var itemEntidad in listado)
            {
                ProgramaGeneralPerfilInterceptoBO objetoBO = Mapper.Map<TProgramaGeneralPerfilIntercepto, ProgramaGeneralPerfilInterceptoBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public ProgramaGeneralPerfilInterceptoBO FirstById(int id)
        {
            try
            {
                TProgramaGeneralPerfilIntercepto entidad = base.FirstById(id);
                ProgramaGeneralPerfilInterceptoBO objetoBO = new ProgramaGeneralPerfilInterceptoBO();
                Mapper.Map<TProgramaGeneralPerfilIntercepto, ProgramaGeneralPerfilInterceptoBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public ProgramaGeneralPerfilInterceptoBO FirstBy(Expression<Func<TProgramaGeneralPerfilIntercepto, bool>> filter)
        {
            try
            {
                TProgramaGeneralPerfilIntercepto entidad = base.FirstBy(filter);
                ProgramaGeneralPerfilInterceptoBO objetoBO = Mapper.Map<TProgramaGeneralPerfilIntercepto, ProgramaGeneralPerfilInterceptoBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(ProgramaGeneralPerfilInterceptoBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TProgramaGeneralPerfilIntercepto entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<ProgramaGeneralPerfilInterceptoBO> listadoBO)
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

        public bool Update(ProgramaGeneralPerfilInterceptoBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TProgramaGeneralPerfilIntercepto entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<ProgramaGeneralPerfilInterceptoBO> listadoBO)
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
        private void AsignacionId(TProgramaGeneralPerfilIntercepto entidad, ProgramaGeneralPerfilInterceptoBO objetoBO)
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

        private TProgramaGeneralPerfilIntercepto MapeoEntidad(ProgramaGeneralPerfilInterceptoBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TProgramaGeneralPerfilIntercepto entidad = new TProgramaGeneralPerfilIntercepto();
                entidad = Mapper.Map<ProgramaGeneralPerfilInterceptoBO, TProgramaGeneralPerfilIntercepto>(objetoBO,
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
        /// Obtiene el intercepto (activo) para el perfil contacto Programa por programa 
        /// registradas en el sistema
        /// </summary>
        /// <param name="idPGeneral"></param>
        /// <returns></returns>
        public PerfilContactoInterceptoDTO ObtenerInterceptoPorPrograma(int idPGeneral)
        {

            try
            {
                PerfilContactoInterceptoDTO resultadoDTO = new PerfilContactoInterceptoDTO();
                var _query = string.Empty;
                _query = "SELECT Id,PerfilEstado,PerfilIntercepto FROM pla.V_TProgramaGeneralPerfilIntercepto WHERE " +
                    "Estado = 1 and IdPGeneral = @IdPGeneral";
                var respuestaDapper = _dapper.FirstOrDefault(_query, new { IdPGeneral = idPGeneral });
                if (!respuestaDapper.Equals("null"))
                {
                    resultadoDTO = JsonConvert.DeserializeObject<PerfilContactoInterceptoDTO>(respuestaDapper);
                }
                return resultadoDTO;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}
