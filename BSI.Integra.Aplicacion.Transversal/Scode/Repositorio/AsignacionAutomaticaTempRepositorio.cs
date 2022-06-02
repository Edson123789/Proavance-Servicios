using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using AutoMapper;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Aplicacion.Transversal.DTO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using Newtonsoft.Json;

namespace BSI.Integra.Aplicacion.Transversal.Repositorio
{
    /// Repositorio: AsignacionAutomaticaTemp
    /// Autor: Ansoli Espinoza - Wilber Choque - Jose Villena - Carlos Crispin
    /// Fecha: 07/05/2021
    /// <summary>
    /// Gestion para los tabs de la agenda
    /// </summary>
    public class AsignacionAutomaticaTempRepositorio : BaseRepository<TAsignacionAutomaticaTemp, AsignacionAutomaticaTempBO>
    {
        #region Metodos Base
        public AsignacionAutomaticaTempRepositorio() : base()
        {
        }
        public AsignacionAutomaticaTempRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<AsignacionAutomaticaTempBO> GetBy(Expression<Func<TAsignacionAutomaticaTemp, bool>> filter)
        {
            IEnumerable<TAsignacionAutomaticaTemp> listado = base.GetBy(filter);
            List<AsignacionAutomaticaTempBO> listadoBO = new List<AsignacionAutomaticaTempBO>();
            foreach (var itemEntidad in listado)
            {
                AsignacionAutomaticaTempBO objetoBO = Mapper.Map<TAsignacionAutomaticaTemp, AsignacionAutomaticaTempBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public AsignacionAutomaticaTempBO FirstById(int id)
        {
            try
            {
                TAsignacionAutomaticaTemp entidad = base.FirstById(id);
                AsignacionAutomaticaTempBO objetoBO = new AsignacionAutomaticaTempBO();
                Mapper.Map<TAsignacionAutomaticaTemp, AsignacionAutomaticaTempBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public AsignacionAutomaticaTempBO FirstBy(Expression<Func<TAsignacionAutomaticaTemp, bool>> filter)
        {
            try
            {
                TAsignacionAutomaticaTemp entidad = base.FirstBy(filter);
                AsignacionAutomaticaTempBO objetoBO = Mapper.Map<TAsignacionAutomaticaTemp, AsignacionAutomaticaTempBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(AsignacionAutomaticaTempBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TAsignacionAutomaticaTemp entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<AsignacionAutomaticaTempBO> listadoBO)
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

        public bool Update(AsignacionAutomaticaTempBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TAsignacionAutomaticaTemp entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<AsignacionAutomaticaTempBO> listadoBO)
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
        private void AsignacionId(TAsignacionAutomaticaTemp entidad, AsignacionAutomaticaTempBO objetoBO)
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

        private TAsignacionAutomaticaTemp MapeoEntidad(AsignacionAutomaticaTempBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TAsignacionAutomaticaTemp entidad = new TAsignacionAutomaticaTemp();
                entidad = Mapper.Map<AsignacionAutomaticaTempBO, TAsignacionAutomaticaTemp>(objetoBO,
                    opt => opt.ConfigureMap(MemberList.None));

                //mapea los hijos

                return entidad;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public AsignacionAutomaticaTempDTO GetNuevosRegistroById(string idRegistroPortalWeb, int idPagina)
        {
            AsignacionAutomaticaTempDTO Registro = new AsignacionAutomaticaTempDTO();
            var RegistroDB = _dapper.QuerySPFirstOrDefault("dbo.SP_GetContactoFaseOportunidadPWById", new { idRegistroPortalWeb, idPagina });
            Registro = JsonConvert.DeserializeObject<AsignacionAutomaticaTempDTO>(RegistroDB);
            return Registro;
        }
        public AsignacionAutomaticaTempDTO ObtenerNuevosRegistroById(string idRegistroPortalWeb, int idPagina)
        {
            AsignacionAutomaticaTempDTO Registro = new AsignacionAutomaticaTempDTO();
            var RegistroDB = _dapper.QuerySPFirstOrDefault("dbo.SP_ObtenerContactoFaseOportunidadNuevoPortal", new { idRegistroPortalWeb, idPagina });
            Registro = JsonConvert.DeserializeObject<AsignacionAutomaticaTempDTO>(RegistroDB);
            return Registro;
        }
        public void MarcarComoProcesado(string Procesado,int IdPaginaWeb)
        {
            _dapper.QuerySPFirstOrDefault("dbo.SP_UpdateFaseOportunidadPortal", new { IdFaseOportunidadPortal = Procesado, IdPagina= IdPaginaWeb });
        }
        public void MarcarComoProcesadoNuevoPortal(string Procesado)
        {
            _dapper.QuerySPFirstOrDefault("dbo.SP_ActualizarProcesadoNuevoPortal", new { IdRegistroPortalWeb = Procesado });
        }

        #endregion

        /// Autor: Jose Villena
        /// Fecha: 03-10-2021
        /// Version: 1.0
        /// <summary>
        /// Obterner el NombreCampania por IdFase Oportunidad
        /// </summary>
        /// <param name="IdFaseOportunidad"> IdFaseOportunidad </param>
        /// <returns></returns> 
        public NombreCampaniaAsiAsignacionAutomaticaTempDTO ObtenerNombreCampaniaPorIdFaseOportunidad(string IdFaseOportunidad)
        {
            try
            {
                string query = "SELECT IdFaseOportunidadPortal,IdConjuntoAnuncio, NombreCampania FROM [mkt].[V_TFaseOportunidadPortal_ObtenerNombreCampania] WHERE  IdFaseOportunidadPortal=@IdFaseOportunidad AND Estado = 1";
                var nombreCampaniaAdwsDB = _dapper.FirstOrDefault(query, new { IdFaseOportunidad });
                if (nombreCampaniaAdwsDB != "null")
                {
                    return JsonConvert.DeserializeObject<NombreCampaniaAsiAsignacionAutomaticaTempDTO>(nombreCampaniaAdwsDB);
                }
                else
                {
                    return null;
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
