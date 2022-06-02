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
using BSI.Integra.Aplicacion.Transversal.Scode.BO;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.DTOs.Scode.DTOs.Transversal;
using System.Linq;
using Newtonsoft.Json;
namespace BSI.Integra.Aplicacion.Transversal.Repositorio
{
    public class ContenidoDatoAdicionalRepositorio : BaseRepository<TContenidoDatoAdicional, ContenidoDatoAdicionalBO>
    {
        #region Metodos Base
        public ContenidoDatoAdicionalRepositorio() : base()
        {
        }
        public ContenidoDatoAdicionalRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<ContenidoDatoAdicionalBO> GetBy(Expression<Func<TContenidoDatoAdicional, bool>> filter)
        {
            IEnumerable<TContenidoDatoAdicional> listado = base.GetBy(filter);
            List<ContenidoDatoAdicionalBO> listadoBO = new List<ContenidoDatoAdicionalBO>();
            foreach (var itemEntidad in listado)
            {
                ContenidoDatoAdicionalBO objetoBO = Mapper.Map<TContenidoDatoAdicional, ContenidoDatoAdicionalBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public ContenidoDatoAdicionalBO FirstById(int id)
        {
            try
            {
                TContenidoDatoAdicional entidad = base.FirstById(id);
                ContenidoDatoAdicionalBO objetoBO = new ContenidoDatoAdicionalBO();
                Mapper.Map<TContenidoDatoAdicional, ContenidoDatoAdicionalBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public ContenidoDatoAdicionalBO FirstBy(Expression<Func<TContenidoDatoAdicional, bool>> filter)
        {
            try
            {
                TContenidoDatoAdicional entidad = base.FirstBy(filter);
                ContenidoDatoAdicionalBO objetoBO = Mapper.Map<TContenidoDatoAdicional, ContenidoDatoAdicionalBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(ContenidoDatoAdicionalBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TContenidoDatoAdicional entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<ContenidoDatoAdicionalBO> listadoBO)
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

        public bool Update(ContenidoDatoAdicionalBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TContenidoDatoAdicional entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<ContenidoDatoAdicionalBO> listadoBO)
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
        private void AsignacionId(TContenidoDatoAdicional entidad, ContenidoDatoAdicionalBO objetoBO)
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

        private TContenidoDatoAdicional MapeoEntidad(ContenidoDatoAdicionalBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TContenidoDatoAdicional entidad = new TContenidoDatoAdicional();
                entidad = Mapper.Map<ContenidoDatoAdicionalBO, TContenidoDatoAdicional>(objetoBO,
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
    }
}
