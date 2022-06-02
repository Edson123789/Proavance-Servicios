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
    public class TipoCuentaBancoRepositorio : BaseRepository<TTipoCuentaBanco, TipoCuentaBancoBO>
    {
        #region Metodos Base
        public TipoCuentaBancoRepositorio() : base()
        {
        }
        public TipoCuentaBancoRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<TipoCuentaBancoBO> GetBy(Expression<Func<TTipoCuentaBanco, bool>> filter)
        {
            IEnumerable<TTipoCuentaBanco> listado = base.GetBy(filter);
            List<TipoCuentaBancoBO> listadoBO = new List<TipoCuentaBancoBO>();
            foreach (var itemEntidad in listado)
            {
                TipoCuentaBancoBO objetoBO = Mapper.Map<TTipoCuentaBanco, TipoCuentaBancoBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public TipoCuentaBancoBO FirstById(int id)
        {
            try
            {
                TTipoCuentaBanco entidad = base.FirstById(id);
                TipoCuentaBancoBO objetoBO = new TipoCuentaBancoBO();
                Mapper.Map<TTipoCuentaBanco, TipoCuentaBancoBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public TipoCuentaBancoBO FirstBy(Expression<Func<TTipoCuentaBanco, bool>> filter)
        {
            try
            {
                TTipoCuentaBanco entidad = base.FirstBy(filter);
                TipoCuentaBancoBO objetoBO = Mapper.Map<TTipoCuentaBanco, TipoCuentaBancoBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(TipoCuentaBancoBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TTipoCuentaBanco entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<TipoCuentaBancoBO> listadoBO)
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

        public bool Update(TipoCuentaBancoBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TTipoCuentaBanco entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<TipoCuentaBancoBO> listadoBO)
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
        private void AsignacionId(TTipoCuentaBanco entidad, TipoCuentaBancoBO objetoBO)
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

        private TTipoCuentaBanco MapeoEntidad(TipoCuentaBancoBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TTipoCuentaBanco entidad = new TTipoCuentaBanco();
                entidad = Mapper.Map<TipoCuentaBancoBO, TTipoCuentaBanco>(objetoBO,
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
        public List<FiltroDTO> ObtenerTipoCuentaBanco()
        {
            try
            {
                List<FiltroDTO> listaTipoCuenta = this.GetBy(x => x.Estado == true, x => new FiltroDTO { Id = x.Id, Nombre = x.Nombre.ToUpper() }).ToList();
                return listaTipoCuenta;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
