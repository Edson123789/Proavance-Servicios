using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using AutoMapper;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Finanzas.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using Newtonsoft.Json;


namespace BSI.Integra.Aplicacion.Finanzas.Repositorio
{
    public class SunatDocumentoRepositorio : BaseRepository<TSunatDocumento, SunatDocumentoBO>
    {
        #region Metodos Base
        public SunatDocumentoRepositorio() : base()
        {
        }
        public SunatDocumentoRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<SunatDocumentoBO> GetBy(Expression<Func<TSunatDocumento, bool>> filter)
        {
            IEnumerable<TSunatDocumento> listado = base.GetBy(filter);
            List<SunatDocumentoBO> listadoBO = new List<SunatDocumentoBO>();
            foreach (var itemEntidad in listado)
            {
                SunatDocumentoBO objetoBO = Mapper.Map<TSunatDocumento, SunatDocumentoBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public SunatDocumentoBO FirstById(int id)
        {
            try
            {
                TSunatDocumento entidad = base.FirstById(id);
                SunatDocumentoBO objetoBO = new SunatDocumentoBO();
                Mapper.Map<TSunatDocumento, SunatDocumentoBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public SunatDocumentoBO FirstBy(Expression<Func<TSunatDocumento, bool>> filter)
        {
            try
            {
                TSunatDocumento entidad = base.FirstBy(filter);
                SunatDocumentoBO objetoBO = Mapper.Map<TSunatDocumento, SunatDocumentoBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(SunatDocumentoBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TSunatDocumento entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<SunatDocumentoBO> listadoBO)
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

        public bool Update(SunatDocumentoBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TSunatDocumento entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<SunatDocumentoBO> listadoBO)
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
        private void AsignacionId(TSunatDocumento entidad, SunatDocumentoBO objetoBO)
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

        private TSunatDocumento MapeoEntidad(SunatDocumentoBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TSunatDocumento entidad = new TSunatDocumento();
                entidad = Mapper.Map<SunatDocumentoBO, TSunatDocumento>(objetoBO,
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
        /// Obtiene la lista de documentos de la tabla T_SunatDocumentos (usado para llenado de combobox)
        /// </summary>
        /// <returns></returns>
        public List<SunatDocumentoDTO> ObtenerElementosSunatDocumento()
        {
            try
            {
                List<SunatDocumentoDTO> SunatDocumentos = new List<SunatDocumentoDTO>();
                var _query = "SELECT Id, Nombre FROM  fin.T_SunatDocumento WHERE Estado=1";
                var SunatDocumentoFinanzasDB = _dapper.QueryDapper(_query, null);
                if (!SunatDocumentoFinanzasDB.Contains("[]") && !string.IsNullOrEmpty(SunatDocumentoFinanzasDB))
                {
                    SunatDocumentos = JsonConvert.DeserializeObject<List<SunatDocumentoDTO>>(SunatDocumentoFinanzasDB);
                }
                return SunatDocumentos;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Obtiene la lista de documentos de la tabla T_SunatDocumentos (usado para llenado de combobox de FurRegistrarPagosV2 > Crear Comprobante)
        /// </summary>
        /// <returns></returns>
        public List<SunatDocumentoDTO> ObtenerElementosSunatDocumentoConId()
        {
            try
            {
                List<SunatDocumentoDTO> SunatDocumentos = new List<SunatDocumentoDTO>();
                var _query = "SELECT Id, CONCAT(Id, ' - ', Nombre) as Nombre FROM  fin.T_SunatDocumento WHERE Estado=1";
                var SunatDocumentoFinanzasDB = _dapper.QueryDapper(_query, null);
                if (!SunatDocumentoFinanzasDB.Contains("[]") && !string.IsNullOrEmpty(SunatDocumentoFinanzasDB))
                {
                    SunatDocumentos = JsonConvert.DeserializeObject<List<SunatDocumentoDTO>>(SunatDocumentoFinanzasDB);
                }
                return SunatDocumentos;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    } 
}
