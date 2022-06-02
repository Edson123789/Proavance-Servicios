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

namespace BSI.Integra.Aplicacion.Transversal.Scode.Repositorio
{
    public class WhatsAppNumeroValidoRepositorio : BaseRepository<TWhatsAppNumeroValidado, WhatsAppNumeroValidadoBO>
    {
        #region Metodos Base
        public WhatsAppNumeroValidoRepositorio() : base()
        {
        }
        public WhatsAppNumeroValidoRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<WhatsAppNumeroValidadoBO> GetBy(Expression<Func<TWhatsAppNumeroValidado, bool>> filter)
        {
            IEnumerable<TWhatsAppNumeroValidado> listado = base.GetBy(filter);
            List<WhatsAppNumeroValidadoBO> listadoBO = new List<WhatsAppNumeroValidadoBO>();
            foreach (var itemEntidad in listado)
            {
                WhatsAppNumeroValidadoBO objetoBO = Mapper.Map<TWhatsAppNumeroValidado, WhatsAppNumeroValidadoBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public WhatsAppNumeroValidadoBO FirstById(int id)
        {
            try
            {
                TWhatsAppNumeroValidado entidad = base.FirstById(id);
                WhatsAppNumeroValidadoBO objetoBO = new WhatsAppNumeroValidadoBO();
                Mapper.Map<TWhatsAppNumeroValidado, WhatsAppNumeroValidadoBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public WhatsAppNumeroValidadoBO FirstBy(Expression<Func<TWhatsAppNumeroValidado, bool>> filter)
        {
            try
            {
                TWhatsAppNumeroValidado entidad = base.FirstBy(filter);
                WhatsAppNumeroValidadoBO objetoBO = Mapper.Map<TWhatsAppNumeroValidado, WhatsAppNumeroValidadoBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(WhatsAppNumeroValidadoBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TWhatsAppNumeroValidado entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<WhatsAppNumeroValidadoBO> listadoBO)
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

        public bool Update(WhatsAppNumeroValidadoBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TWhatsAppNumeroValidado entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<WhatsAppNumeroValidadoBO> listadoBO)
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
        private void AsignacionId(TWhatsAppNumeroValidado entidad, WhatsAppNumeroValidadoBO objetoBO)
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

        private TWhatsAppNumeroValidado MapeoEntidad(WhatsAppNumeroValidadoBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TWhatsAppNumeroValidado entidad = new TWhatsAppNumeroValidado();
                entidad = Mapper.Map<WhatsAppNumeroValidadoBO, TWhatsAppNumeroValidado>(objetoBO,
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
        /// 
        /// </summary>
        /// <param name="Numero"></param>
        /// <returns></returns>
        public bool VerificarNumeroValidado(string Numero)
        {
            try
            {
                return Exist(w=> w.NumeroCelular == Numero);
                
            }
            catch(Exception e){

                throw new Exception(e.Message);
            }
        }
    }
}

