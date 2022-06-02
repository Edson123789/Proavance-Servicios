using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using AutoMapper;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using Newtonsoft.Json;

namespace BSI.Integra.Aplicacion.Transversal.Repositorio
{
    public class AsignacionAutomaticaConfiguracionRepositorio : BaseRepository<TAsignacionAutomaticaConfiguracion, AsignacionAutomaticaConfiguracionBO>
    {
        #region Metodos Base
        public AsignacionAutomaticaConfiguracionRepositorio() : base()
        {
        }
        public AsignacionAutomaticaConfiguracionRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<AsignacionAutomaticaConfiguracionBO> GetBy(Expression<Func<TAsignacionAutomaticaConfiguracion, bool>> filter)
        {
            IEnumerable<TAsignacionAutomaticaConfiguracion> listado = base.GetBy(filter);
            List<AsignacionAutomaticaConfiguracionBO> listadoBO = new List<AsignacionAutomaticaConfiguracionBO>();
            foreach (var itemEntidad in listado)
            {
                AsignacionAutomaticaConfiguracionBO objetoBO = Mapper.Map<TAsignacionAutomaticaConfiguracion, AsignacionAutomaticaConfiguracionBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public AsignacionAutomaticaConfiguracionBO FirstById(int id)
        {
            try
            {
                TAsignacionAutomaticaConfiguracion entidad = base.FirstById(id);
                AsignacionAutomaticaConfiguracionBO objetoBO = new AsignacionAutomaticaConfiguracionBO();
                Mapper.Map<TAsignacionAutomaticaConfiguracion, AsignacionAutomaticaConfiguracionBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public AsignacionAutomaticaConfiguracionBO FirstBy(Expression<Func<TAsignacionAutomaticaConfiguracion, bool>> filter)
        {
            try
            {
                TAsignacionAutomaticaConfiguracion entidad = base.FirstBy(filter);
                AsignacionAutomaticaConfiguracionBO objetoBO = Mapper.Map<TAsignacionAutomaticaConfiguracion, AsignacionAutomaticaConfiguracionBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(AsignacionAutomaticaConfiguracionBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TAsignacionAutomaticaConfiguracion entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<AsignacionAutomaticaConfiguracionBO> listadoBO)
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

        public bool Update(AsignacionAutomaticaConfiguracionBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TAsignacionAutomaticaConfiguracion entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<AsignacionAutomaticaConfiguracionBO> listadoBO)
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
        private void AsignacionId(TAsignacionAutomaticaConfiguracion entidad, AsignacionAutomaticaConfiguracionBO objetoBO)
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

        public List<AsignacionAutomaticaConfiguracionBO> ObtenerConfiguracionAsignacionAutomatica()
        {
            try
            {
                List<AsignacionAutomaticaConfiguracionBO> configuraciones = new List<AsignacionAutomaticaConfiguracionBO>();
                var _query = string.Empty;
                _query = "SELECT C.Id,(SELECT Iif(codigo IS NULL, 0, id) FROM pla.T_FaseOportunidad WHERE  Id = C.IdFaseOportunidad) AS FaseOportunidad, IIF(IdTipoDato IS NULL,0,IdTipoDato) AS IdTipoDato,IIF(IdOrigen Is Null, 0, IdOrigen) AS IdOrigen ,Inclusivo,Habilitado FROM   mkt.T_AsignacionAutomaticaConfiguracion AS C WHERE  c.Habilitado = 1 AND c.Estado = 1 ";
                var ConfiguracionesDB = _dapper.QueryDapper(_query,null);
                configuraciones = JsonConvert.DeserializeObject<List<AsignacionAutomaticaConfiguracionBO>>(ConfiguracionesDB);
                return configuraciones;

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        private TAsignacionAutomaticaConfiguracion MapeoEntidad(AsignacionAutomaticaConfiguracionBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TAsignacionAutomaticaConfiguracion entidad = new TAsignacionAutomaticaConfiguracion();
                entidad = Mapper.Map<AsignacionAutomaticaConfiguracionBO, TAsignacionAutomaticaConfiguracion>(objetoBO,
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
        /// Retorna todos las configuraciones de asignacion automatica habilitados
        /// </summary>
        /// <returns></returns>
        public List<AsignacionAutomaticaConfiguracionBO> GetAllHabilitado()
        {
            var _query = "SELECT C.Id,(SELECT Iif(codigo IS NULL, 0, id) FROM pla.T_FaseOportunidad WHERE  Id = C.IdFaseOportunidad) AS FaseOportunidad, IIF(IdTipoDato IS NULL,0,IdTipoDato) AS IdTipoDato,IIF(IdOrigen Is Null, 0, IdOrigen) AS IdOrigen ,Inclusivo,Habilitado FROM   mkt.T_AsignacionAutomaticaConfiguracion AS C WHERE  c.Habilitado = 1 AND c.Estado = 1 ";
            var ConfiguracionesDB = _dapper.QueryDapper(_query,null);
            var Configuraciones = new List<AsignacionAutomaticaConfiguracionBO>();
            Configuraciones = JsonConvert.DeserializeObject<List<AsignacionAutomaticaConfiguracionBO>>(ConfiguracionesDB);
            return Configuraciones;
        }
    }
}
