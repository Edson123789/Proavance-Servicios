using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using AutoMapper;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using BSI.Integra.Aplicacion.DTOs;
using Newtonsoft.Json;
using System.Linq;
using BSI.Integra.Aplicacion.Transversal.Scode.BO;

namespace BSI.Integra.Aplicacion.Transversal.Scode.Repositorio
{
    class TipoPromedioRepositorio : BaseRepository<TTipoPromedio, TipoPromedioBO>
    {
        #region Metodos Base
        public TipoPromedioRepositorio() : base()
        {
        }
        public TipoPromedioRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<TipoPromedioBO> GetBy(Expression<Func<TTipoPromedio, bool>> filter)
        {
            IEnumerable<TTipoPromedio> listado = base.GetBy(filter);
            List<TipoPromedioBO> listadoBO = new List<TipoPromedioBO>();
            foreach (var itemEntidad in listado)
            {
                TipoPromedioBO objetoBO = Mapper.Map<TTipoPromedio, TipoPromedioBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public TipoPromedioBO FirstById(int id)
        {
            try
            {
                TTipoPromedio entidad = base.FirstById(id);
                TipoPromedioBO objetoBO = new TipoPromedioBO();
                Mapper.Map<TTipoPromedio, TipoPromedioBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public TipoPromedioBO FirstBy(Expression<Func<TTipoPromedio, bool>> filter)
        {
            try
            {
                TTipoPromedio entidad = base.FirstBy(filter);
                TipoPromedioBO objetoBO = Mapper.Map<TTipoPromedio, TipoPromedioBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(TipoPromedioBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TTipoPromedio entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<TipoPromedioBO> listadoBO)
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

        public bool Update(TipoPromedioBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TTipoPromedio entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<TipoPromedioBO> listadoBO)
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
        private void AsignacionId(TTipoPromedio entidad, TipoPromedioBO objetoBO)
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

        private TTipoPromedio MapeoEntidad(TipoPromedioBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TTipoPromedio entidad = new TTipoPromedio();
                entidad = Mapper.Map<TipoPromedioBO, TTipoPromedio>(objetoBO,
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
