using System;
using System.Collections.Generic;
using System.Linq;
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
    public class TipoContribuyenteRepositorio : BaseRepository<TTipoContribuyente, TipoContribuyenteBO>
    {
        #region Metodos Base
        public TipoContribuyenteRepositorio() : base()
        {
        }
        public TipoContribuyenteRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<TipoContribuyenteBO> GetBy(Expression<Func<TTipoContribuyente, bool>> filter)
        {
            IEnumerable<TTipoContribuyente> listado = base.GetBy(filter);
            List<TipoContribuyenteBO> listadoBO = new List<TipoContribuyenteBO>();
            foreach (var itemEntidad in listado)
            {
                TipoContribuyenteBO objetoBO = Mapper.Map<TTipoContribuyente, TipoContribuyenteBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public TipoContribuyenteBO FirstById(int id)
        {
            try
            {
                TTipoContribuyente entidad = base.FirstById(id);
                TipoContribuyenteBO objetoBO = new TipoContribuyenteBO();
                Mapper.Map<TTipoContribuyente, TipoContribuyenteBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public TipoContribuyenteBO FirstBy(Expression<Func<TTipoContribuyente, bool>> filter)
        {
            try
            {
                TTipoContribuyente entidad = base.FirstBy(filter);
                TipoContribuyenteBO objetoBO = Mapper.Map<TTipoContribuyente, TipoContribuyenteBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(TipoContribuyenteBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TTipoContribuyente entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<TipoContribuyenteBO> listadoBO)
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

        public bool Update(TipoContribuyenteBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TTipoContribuyente entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<TipoContribuyenteBO> listadoBO)
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
        private void AsignacionId(TTipoContribuyente entidad, TipoContribuyenteBO objetoBO)
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

        private TTipoContribuyente MapeoEntidad(TipoContribuyenteBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TTipoContribuyente entidad = new TTipoContribuyente();
                entidad = Mapper.Map<TipoContribuyenteBO, TTipoContribuyente>(objetoBO,
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

        public List<FiltroDTO> ObtenerTipoContribuyente() {
            try
            {
                List<FiltroDTO> listaTipoContribuyente= this.GetBy(x => x.Estado == true, x => new FiltroDTO { Id = x.Id, Nombre = x.Nombre.ToUpper() }).ToList();
                return listaTipoContribuyente;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
