using AutoMapper;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Finanzas.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace BSI.Integra.Aplicacion.Finanzas.Repositorio
{
    public class TipoRemuneracionAdicionalRepositorio : BaseRepository<TTipoRemuneracionAdicional, TipoRemuneracionAdicionalBO>
    {
        #region Metodos Base
        public TipoRemuneracionAdicionalRepositorio() : base()
        {
        }
        public TipoRemuneracionAdicionalRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<TipoRemuneracionAdicionalBO> GetBy(Expression<Func<TTipoRemuneracionAdicional, bool>> filter)
        {
            IEnumerable<TTipoRemuneracionAdicional> listado = base.GetBy(filter);
            List<TipoRemuneracionAdicionalBO> listadoBO = new List<TipoRemuneracionAdicionalBO>();
            foreach (var itemEntidad in listado)
            {
                TipoRemuneracionAdicionalBO objetoBO = Mapper.Map<TTipoRemuneracionAdicional, TipoRemuneracionAdicionalBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public TipoRemuneracionAdicionalBO FirstById(int id)
        {
            try
            {
                TTipoRemuneracionAdicional entidad = base.FirstById(id);
                TipoRemuneracionAdicionalBO objetoBO = new TipoRemuneracionAdicionalBO();
                Mapper.Map<TTipoRemuneracionAdicional, TipoRemuneracionAdicionalBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public TipoRemuneracionAdicionalBO FirstBy(Expression<Func<TTipoRemuneracionAdicional, bool>> filter)
        {
            try
            {
                TTipoRemuneracionAdicional entidad = base.FirstBy(filter);
                TipoRemuneracionAdicionalBO objetoBO = Mapper.Map<TTipoRemuneracionAdicional, TipoRemuneracionAdicionalBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(TipoRemuneracionAdicionalBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TTipoRemuneracionAdicional entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<TipoRemuneracionAdicionalBO> listadoBO)
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

        public bool Update(TipoRemuneracionAdicionalBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TTipoRemuneracionAdicional entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<TipoRemuneracionAdicionalBO> listadoBO)
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
        private void AsignacionId(TTipoRemuneracionAdicional entidad, TipoRemuneracionAdicionalBO objetoBO)
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

        private TTipoRemuneracionAdicional MapeoEntidad(TipoRemuneracionAdicionalBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TTipoRemuneracionAdicional entidad = new TTipoRemuneracionAdicional();
                entidad = Mapper.Map<TipoRemuneracionAdicionalBO, TTipoRemuneracionAdicional>(objetoBO,
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

        public List<TipoRemuneracionAdicionalDTO> ObtenerTipoRemuneracionAdicional()
        {
            try
            {
                var lista = GetBy(x => true, y => new TipoRemuneracionAdicionalDTO
                {
                    Id = y.Id,
                    Nombre = y.Nombre,
                    Visualizar = y.Visualizar,
                    Usuario = y.UsuarioCreacion
                }).OrderByDescending(x => x.Id).ToList();

                return lista;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public List<FiltroTipoRemuneracionAdicionalDTO> FiltroTipoRemuneracionAdicional()
        {
            try
            {
                var lista = GetBy(x => true, y => new FiltroTipoRemuneracionAdicionalDTO
                {
                    Id = y.Id,
                    Nombre = y.Nombre
                }).OrderByDescending(x => x.Id).ToList();

                return lista;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
