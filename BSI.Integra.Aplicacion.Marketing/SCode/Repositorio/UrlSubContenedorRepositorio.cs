using AutoMapper;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Marketing.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace BSI.Integra.Aplicacion.Marketing.SCode.Repositorio
{
    public class UrlSubContenedorRepositorio : BaseRepository<TUrlSubContenedor, UrlSubContenedorBO>
    {
        #region Metodos Base
        public UrlSubContenedorRepositorio() : base()
        {
        }
        public UrlSubContenedorRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<UrlSubContenedorBO> GetBy(Expression<Func<TUrlSubContenedor, bool>> filter)
        {
            IEnumerable<TUrlSubContenedor> listado = base.GetBy(filter);
            List<UrlSubContenedorBO> listadoBO = new List<UrlSubContenedorBO>();
            foreach (var itemEntidad in listado)
            {
                UrlSubContenedorBO objetoBO = Mapper.Map<TUrlSubContenedor, UrlSubContenedorBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public UrlSubContenedorBO FirstById(int id)
        {
            try
            {
                TUrlSubContenedor entidad = base.FirstById(id);
                UrlSubContenedorBO objetoBO = new UrlSubContenedorBO();
                Mapper.Map<TUrlSubContenedor, UrlSubContenedorBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public UrlSubContenedorBO FirstBy(Expression<Func<TUrlSubContenedor, bool>> filter)
        {
            try
            {
                TUrlSubContenedor entidad = base.FirstBy(filter);
                UrlSubContenedorBO objetoBO = Mapper.Map<TUrlSubContenedor, UrlSubContenedorBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(UrlSubContenedorBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TUrlSubContenedor entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<UrlSubContenedorBO> listadoBO)
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

        public bool Update(UrlSubContenedorBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TUrlSubContenedor entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<UrlSubContenedorBO> listadoBO)
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
        private void AsignacionId(TUrlSubContenedor entidad, UrlSubContenedorBO objetoBO)
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

        private TUrlSubContenedor MapeoEntidad(UrlSubContenedorBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TUrlSubContenedor entidad = new TUrlSubContenedor();
                entidad = Mapper.Map<UrlSubContenedorBO, TUrlSubContenedor>(objetoBO,
                    opt => opt.ConfigureMap(MemberList.None));

                //mapea los hijos

                return entidad;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public IEnumerable<UrlSubContenedorBO> GetFiltered<KProperty>(IEnumerable<Expression<Func<TUrlSubContenedor, bool>>> filters, Expression<Func<TUrlSubContenedor, KProperty>> orderBy, bool ascending)
        {
            IEnumerable<TUrlSubContenedor> listado = base.GetFiltered(filters, orderBy, ascending);
            List<UrlSubContenedorBO> listadoBO = new List<UrlSubContenedorBO>();

            foreach (var itemEntidad in listado)
            {
                UrlSubContenedorBO objetoBO = Mapper.Map<TUrlSubContenedor, UrlSubContenedorBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }
            return listadoBO;
        }
        #endregion

        public List<UrlSubContenedorDTO> ObtenerRutaSubContenedor(int idSubContenedor)
        {
            try
            {
                List<UrlSubContenedorDTO> listado = new List<UrlSubContenedorDTO>();
                var _query = string.Empty;

                _query = $@"SELECT TOP 1 *
                                FROM [mkt].[V_T_UrlBlockStorage_ObtenerUrl]
                                WHERE V_T_UrlBlockStorage_ObtenerUrl.Id = @idSubContenedor";

                var actividadesDB = _dapper.QueryDapper(_query,
                    new
                    {
                        idSubContenedor = idSubContenedor
                    });
                listado = JsonConvert.DeserializeObject<List<UrlSubContenedorDTO>>(actividadesDB);

                return listado;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
