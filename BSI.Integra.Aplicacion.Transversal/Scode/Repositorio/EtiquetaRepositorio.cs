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
    public class EtiquetaRepositorio : BaseRepository<TEtiqueta, EtiquetaBO>
    {
        #region Metodos Base
        public EtiquetaRepositorio() : base()
        {
        }
        public EtiquetaRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<EtiquetaBO> GetBy(Expression<Func<TEtiqueta, bool>> filter)
        {
            IEnumerable<TEtiqueta> listado = base.GetBy(filter);
            List<EtiquetaBO> listadoBO = new List<EtiquetaBO>();
            foreach (var itemEntidad in listado)
            {
                EtiquetaBO objetoBO = Mapper.Map<TEtiqueta, EtiquetaBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public EtiquetaBO FirstById(int id)
        {
            try
            {
                TEtiqueta entidad = base.FirstById(id);
                EtiquetaBO objetoBO = new EtiquetaBO();
                Mapper.Map<TEtiqueta, EtiquetaBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public EtiquetaBO FirstBy(Expression<Func<TEtiqueta, bool>> filter)
        {
            try
            {
                TEtiqueta entidad = base.FirstBy(filter);
                EtiquetaBO objetoBO = Mapper.Map<TEtiqueta, EtiquetaBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(EtiquetaBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TEtiqueta entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<EtiquetaBO> listadoBO)
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

        public bool Update(EtiquetaBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TEtiqueta entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<EtiquetaBO> listadoBO)
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
        private void AsignacionId(TEtiqueta entidad, EtiquetaBO objetoBO)
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

        private TEtiqueta MapeoEntidad(EtiquetaBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TEtiqueta entidad = new TEtiqueta();
                entidad = Mapper.Map<EtiquetaBO, TEtiqueta>(objetoBO,
                    opt => opt.ConfigureMap(MemberList.None));

                //mapea los hijos
                //mapea los hijos
                if (objetoBO.ListaEtiquetaBotonReemplazo != null && objetoBO.ListaEtiquetaBotonReemplazo.Count > 0)
                {
                    foreach (var hijo in objetoBO.ListaEtiquetaBotonReemplazo)
                    {
                        TEtiquetaBotonReemplazo entidadHijo = new TEtiquetaBotonReemplazo();
                        entidadHijo = Mapper.Map<EtiquetaBotonReemplazoBO, TEtiquetaBotonReemplazo>(hijo, opt => opt.ConfigureMap(MemberList.None));
                        entidad.TEtiquetaBotonReemplazo.Add(entidadHijo);
                    }
                }
                return entidad;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
		#endregion
        
        /// <summary>
        /// Obtiene todos los registros sin los campos de auditoría.
        /// </summary>
        /// <returns></returns>
        public List<EtiquetaDTO> ObtenerTodoGrid()
        {
            try
            {
                var etiquetasDB = new List<EtiquetaDBDTO>();
                string _query = @"
                        SELECT Id, 
                               IdTipoEtiqueta, 
                               Nombre, 
                               Descripcion, 
                               CampoDb, 
                               NodoPadre, 
                               IdNodoPadre, 
                               UsuarioCreacion, 
                               UsuarioModificacion, 
                               FechaCreacion, 
                               FechaModificacion, 
                               EtiquetaBotonReemplazoTexto, 
                               EtiquetaBotonReemplazoAbrirEnNuevoTab, 
                               EtiquetaBotonReemplazoUrl, 
                               EtiquetaBotonReemplazoEstilos
                        FROM mkt.V_ObtenerEtiqueta
                        WHERE EstadoEtiqueta = 1
                              AND EstadoTipoEtiqueta = 1
                              AND (EstadoEtiquetaBotonReemplazo = 1
                                   OR EstadoEtiquetaBotonReemplazo IS NULL)
                        ORDER BY FechaCreacion DESC
                        ";
                var _etiquetasDB = _dapper.QueryDapper(_query, null);
                etiquetasDB = JsonConvert.DeserializeObject<List<EtiquetaDBDTO>>(_etiquetasDB);

                return etiquetasDB.Select(x => new EtiquetaDTO (){
                    Id = x.Id,
                    IdTipoEtiqueta = x.IdTipoEtiqueta,
                    Nombre = x.Nombre,
                    Descripcion = x.Descripcion,
                    CampoDb = x.CampoDb,
                    NodoPadre = x.NodoPadre,
                    IdNodoPadre = x.IdNodoPadre,
                    EtiquetaBotonReemplazo = (x.EtiquetaBotonReemplazoTexto is null || x.EtiquetaBotonReemplazoUrl is null) ? null :
                    new EtiquetaBotonReemplazoDTO()
                    {
                        Texto = x.EtiquetaBotonReemplazoTexto,
                        AbrirEnNuevoTab = x.EtiquetaBotonReemplazoAbrirEnNuevoTab.Value,
                        Url = x.EtiquetaBotonReemplazoUrl,
                        Estilos = x.EtiquetaBotonReemplazoEstilos
                    }
                }).ToList();

                //var lista = GetBy(x => true, x => new EtiquetaDTO
                //{
                //    Id = x.Id,
                //    IdTipoEtiqueta = x.IdTipoEtiqueta,
                //    Nombre = x.Nombre,
                //    Descripcion = x.Descripcion,
                //    CampoDb = x.CampoDb,
                //    NodoPadre = x.NodoPadre,
                //    IdNodoPadre = x.IdNodoPadre,
                //    EtiquetaBotonReemplazo = x.TEtiquetaBotonReemplazo.Select(w => new EtiquetaBotonReemplazoDTO(){
                //        Texto = w.Texto,
                //        AbrirEnNuevoTab = w.AbrirEnNuevoTab,
                //        Url = w.Url,
                //        Estilos = w.Estilos
                //    }).FirstOrDefault()
                //}).OrderByDescending(x => x.Id).ToList();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Obtiene todos las etiquetas mediante el Padre.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<EtiquetaDTO> ObtenerCategoriasPorIdPadre(int id) {

            try
            {
                var listaCategorias = GetBy(x => x.IdNodoPadre == id, y => new EtiquetaDTO
                {
                    Id = y.Id,
                    Nombre = y.Nombre,
                    Descripcion = y.Descripcion,
                    CampoDb = y.CampoDb,
                    NodoPadre = y.NodoPadre,
                    IdNodoPadre = y.IdNodoPadre

                }).OrderByDescending(x => x.Id).ThenByDescending(x => x.IdNodoPadre).ToList();

                return listaCategorias;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }

        /// <summary>
        /// Obtiene las etiquetas Padre
        /// </summary>
        /// <returns></returns>
        public List<FiltroIdNombreDTO> ObtenerEtiquetaPadre()
        {
            try
            {
                var lista = GetBy(x => x.NodoPadre == true, y => new FiltroIdNombreDTO
                {
                    Id = y.Id,
                    Nombre = y.Nombre,
                }).ToList();

                return lista;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Obtiene las etiquetas de integra, excluyendo las de malchimp
        /// </summary>
        /// <returns>Lista de cadenas</returns>
        public List<string> ObtenerEtiquetasIntegra() {
            try
            {
                var mailchimpEtiquetas = new List<string> { "*|EMAIL|*" , "*|FNAME|*", "*|LNAME|*"} ;
                return this.GetBy(x => !mailchimpEtiquetas.Contains(x.CampoDb)).Select(x => x.CampoDb).ToList();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
     
}
