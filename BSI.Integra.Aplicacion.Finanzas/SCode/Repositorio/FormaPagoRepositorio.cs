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
    public class FormaPagoRepositorio : BaseRepository<TFormaPago, FormaPagoBO>
    {
        #region Metodos Base
        public FormaPagoRepositorio() : base()
        {
        }
        public FormaPagoRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<FormaPagoBO> GetBy(Expression<Func<TFormaPago, bool>> filter)
        {
            IEnumerable<TFormaPago> listado = base.GetBy(filter);
            List<FormaPagoBO> listadoBO = new List<FormaPagoBO>();
            foreach (var itemEntidad in listado)
            {
                FormaPagoBO objetoBO = Mapper.Map<TFormaPago, FormaPagoBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public FormaPagoBO FirstById(int id)
        {
            try
            {
                TFormaPago entidad = base.FirstById(id);
                FormaPagoBO objetoBO = new FormaPagoBO();
                Mapper.Map<TFormaPago, FormaPagoBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public FormaPagoBO FirstBy(Expression<Func<TFormaPago, bool>> filter)
        {
            try
            {
                TFormaPago entidad = base.FirstBy(filter);
                FormaPagoBO objetoBO = Mapper.Map<TFormaPago, FormaPagoBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(FormaPagoBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TFormaPago entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<FormaPagoBO> listadoBO)
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

        public bool Update(FormaPagoBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TFormaPago entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<FormaPagoBO> listadoBO)
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
        private void AsignacionId(TFormaPago entidad, FormaPagoBO objetoBO)
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

        private TFormaPago MapeoEntidad(FormaPagoBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TFormaPago entidad = new TFormaPago();
                entidad = Mapper.Map<FormaPagoBO, TFormaPago>(objetoBO,
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
        /// Obtiene una Lista de FormaPago (utilizado para combobox)
        /// </summary>
        /// <returns></returns>
        public List<FormaPagoDTO> ObtenerListaFormaPago()
        {
            try
            {
                List<FormaPagoDTO> lista = new List<FormaPagoDTO>();
                var _query = "SELECT Id, Descripcion AS Nombre FROM fin.T_FormaPago  where Estado=1";
                var listaDB = _dapper.QueryDapper(_query, null);
                if (!listaDB.Contains("[]") && !string.IsNullOrEmpty(listaDB))
                {
                    lista = JsonConvert.DeserializeObject<List<FormaPagoDTO>>(listaDB);
                }
                return lista;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
