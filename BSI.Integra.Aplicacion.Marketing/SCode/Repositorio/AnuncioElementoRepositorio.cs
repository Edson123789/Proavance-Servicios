using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using AutoMapper;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Marketing.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using Newtonsoft.Json;

namespace BSI.Integra.Aplicacion.Transversal.Repositorio
{
    public class AnuncioElementoRepositorio : BaseRepository<TAnuncioElemento, AnuncioElementoBO>
    {
        #region Metodos Base
        public AnuncioElementoRepositorio() : base()
        {
        }
        public AnuncioElementoRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<AnuncioElementoBO> GetBy(Expression<Func<TAnuncioElemento, bool>> filter)
        {
            IEnumerable<TAnuncioElemento> listado = base.GetBy(filter);
            List<AnuncioElementoBO> listadoBO = new List<AnuncioElementoBO>();
            foreach (var itemEntidad in listado)
            {
                AnuncioElementoBO objetoBO = Mapper.Map<TAnuncioElemento, AnuncioElementoBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public AnuncioElementoBO FirstById(int id)
        {
            try
            {
                TAnuncioElemento entidad = base.FirstById(id);
                AnuncioElementoBO objetoBO = new AnuncioElementoBO();
                Mapper.Map<TAnuncioElemento, AnuncioElementoBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public AnuncioElementoBO FirstBy(Expression<Func<TAnuncioElemento, bool>> filter)
        {
            try
            {
                TAnuncioElemento entidad = base.FirstBy(filter);
                AnuncioElementoBO objetoBO = Mapper.Map<TAnuncioElemento, AnuncioElementoBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(AnuncioElementoBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TAnuncioElemento entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<AnuncioElementoBO> listadoBO)
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

        public bool Update(AnuncioElementoBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TAnuncioElemento entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<AnuncioElementoBO> listadoBO)
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
        private void AsignacionId(TAnuncioElemento entidad, AnuncioElementoBO objetoBO)
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

        private TAnuncioElemento MapeoEntidad(AnuncioElementoBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TAnuncioElemento entidad = new TAnuncioElemento();
                entidad = Mapper.Map<AnuncioElementoBO, TAnuncioElemento>(objetoBO,
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
        ///  Obtiene la lista de registros (Estado=1) de T_AnuncioElemento por Id de T_Anuncio
        /// </summary>
        /// <returns></returns>
        public List<AnuncioElementoDTO> ObtenerElementosPorAnuncio(int IdAnuncio)
        {
            try
            {
                List<AnuncioElementoDTO> Registros = new List<AnuncioElementoDTO>();
                var _query = "SELECT Id, IdAnuncio, IdElemento FROM [mkt].[V_TAnuncioElemento] WHERE IdAnuncio=@IdAnuncio";
                var result = _dapper.QueryDapper(_query, new { IdAnuncio=IdAnuncio});
                if (!string.IsNullOrEmpty(result) && !result.Contains("[]"))
                {
                    Registros = JsonConvert.DeserializeObject<List<AnuncioElementoDTO>>(result);
                }
                return Registros;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        
    }
}
