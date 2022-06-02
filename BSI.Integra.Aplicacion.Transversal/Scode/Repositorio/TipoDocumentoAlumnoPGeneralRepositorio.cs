using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using AutoMapper;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using System.Collections.Generic;
using System.Text;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.DTOs.Scode.DTOs.Transversal;
using System.Linq;
using Newtonsoft.Json;
using BSI.Integra.Aplicacion.Transversal.Scode.BO;

namespace BSI.Integra.Aplicacion.Transversal.Scode.Repositorio
{
    public class TipoDocumentoAlumnoPGeneralRepositorio : BaseRepository<TTipoDocumentoAlumnoPgeneral, TipoDocumentoAlumnoPGeneralBO>
    {
        #region Metodos Base
        public TipoDocumentoAlumnoPGeneralRepositorio() : base()
        {
        }
        public TipoDocumentoAlumnoPGeneralRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<TipoDocumentoAlumnoPGeneralBO> GetBy(Expression<Func<TTipoDocumentoAlumnoPgeneral, bool>> filter)
        {
            IEnumerable<TTipoDocumentoAlumnoPgeneral> listado = base.GetBy(filter);
            List<TipoDocumentoAlumnoPGeneralBO> listadoBO = new List<TipoDocumentoAlumnoPGeneralBO>();
            foreach (var itemEntidad in listado)
            {
                TipoDocumentoAlumnoPGeneralBO objetoBO = Mapper.Map<TTipoDocumentoAlumnoPgeneral, TipoDocumentoAlumnoPGeneralBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public TipoDocumentoAlumnoPGeneralBO FirstById(int id)
        {
            try
            {
                TTipoDocumentoAlumnoPgeneral entidad = base.FirstById(id);
                TipoDocumentoAlumnoPGeneralBO objetoBO = new TipoDocumentoAlumnoPGeneralBO();
                Mapper.Map<TTipoDocumentoAlumnoPgeneral, TipoDocumentoAlumnoPGeneralBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public TipoDocumentoAlumnoPGeneralBO FirstBy(Expression<Func<TTipoDocumentoAlumnoPgeneral, bool>> filter)
        {
            try
            {
                TTipoDocumentoAlumnoPgeneral entidad = base.FirstBy(filter);
                TipoDocumentoAlumnoPGeneralBO objetoBO = Mapper.Map<TTipoDocumentoAlumnoPgeneral, TipoDocumentoAlumnoPGeneralBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(TipoDocumentoAlumnoPGeneralBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TTipoDocumentoAlumnoPgeneral entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<TipoDocumentoAlumnoPGeneralBO> listadoBO)
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

        public bool Update(TipoDocumentoAlumnoPGeneralBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TTipoDocumentoAlumnoPgeneral entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<TipoDocumentoAlumnoPGeneralBO> listadoBO)
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
        private void AsignacionId(TTipoDocumentoAlumnoPgeneral entidad, TipoDocumentoAlumnoPGeneralBO objetoBO)
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

        private TTipoDocumentoAlumnoPgeneral MapeoEntidad(TipoDocumentoAlumnoPGeneralBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TTipoDocumentoAlumnoPgeneral entidad = new TTipoDocumentoAlumnoPgeneral();
                entidad = Mapper.Map<TipoDocumentoAlumnoPGeneralBO, TTipoDocumentoAlumnoPgeneral>(objetoBO,
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

        public void DeleteLogicoPorTipoDocumento(int IdTipoDocumento, string usuario, List<int> nuevos)
        {
            try
            {
                List<EliminacionIdsDTO> listaBorrar = new List<EliminacionIdsDTO>();
                string _query = "SELECT Id FROM  ope.T_TipoDocumentoAlumnoPGeneral WHERE Estado = 1 and IdTipoDocumentoAlumno = @IdTipoDocumento ";
                var query = _dapper.QueryDapper(_query, new { IdTipoDocumento });
                listaBorrar = JsonConvert.DeserializeObject<List<EliminacionIdsDTO>>(query);
                listaBorrar.RemoveAll(x => nuevos.Any(y => y == x.Id));
                foreach (var item in listaBorrar)
                {
                    Delete(item.Id, usuario);
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public List<TipoDocumentoAlumnoPGeneralDTO> ListarTipoDocumentoAlumnoPGeneralPorId(int IdTipoDocumentoAlumno)
        {
            try
            {
                List<TipoDocumentoAlumnoPGeneralDTO> modalidadfiltro = new List<TipoDocumentoAlumnoPGeneralDTO>();
                var _query = "Select IdPGeneral FROM ope.T_TipoDocumentoAlumnoPGeneral where Estado = 1 and IdTipoDocumentoAlumno = @IdTipoDocumentoAlumno";
                var Subfiltro = _dapper.QueryDapper(_query, new { IdTipoDocumentoAlumno });
                if (!string.IsNullOrEmpty(Subfiltro) && !Subfiltro.Contains("[]"))
                {
                    modalidadfiltro = JsonConvert.DeserializeObject<List<TipoDocumentoAlumnoPGeneralDTO>>(Subfiltro);
                }
                return modalidadfiltro;
            }
            catch (Exception E)
            {
                throw new Exception(E.Message);
            }
        }
    }
}
