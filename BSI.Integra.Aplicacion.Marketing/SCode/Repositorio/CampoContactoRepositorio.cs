using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using AutoMapper;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Marketing.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;

namespace BSI.Integra.Aplicacion.Marketing.Repositorio
{
    public class CampoContactoRepositorio : BaseRepository<TCampoContacto, CampoContactoBO>
    {
        #region Metodos Base
        public CampoContactoRepositorio() : base()
        {
        }
        public CampoContactoRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<CampoContactoBO> GetBy(Expression<Func<TCampoContacto, bool>> filter)
        {
            IEnumerable<TCampoContacto> listado = base.GetBy(filter);
            List<CampoContactoBO> listadoBO = new List<CampoContactoBO>();
            foreach (var itemEntidad in listado)
            {
                CampoContactoBO objetoBO = Mapper.Map<TCampoContacto, CampoContactoBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public CampoContactoBO FirstById(int id)
        {
            try
            {
                TCampoContacto entidad = base.FirstById(id);
                CampoContactoBO objetoBO = new CampoContactoBO();
                Mapper.Map<TCampoContacto, CampoContactoBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public CampoContactoBO FirstBy(Expression<Func<TCampoContacto, bool>> filter)
        {
            try
            {
                TCampoContacto entidad = base.FirstBy(filter);
                CampoContactoBO objetoBO = Mapper.Map<TCampoContacto, CampoContactoBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(CampoContactoBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TCampoContacto entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<CampoContactoBO> listadoBO)
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

        public bool Update(CampoContactoBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TCampoContacto entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<CampoContactoBO> listadoBO)
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
        private void AsignacionId(TCampoContacto entidad, CampoContactoBO objetoBO)
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

        private TCampoContacto MapeoEntidad(CampoContactoBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TCampoContacto entidad = new TCampoContacto();
                entidad = Mapper.Map<CampoContactoBO, TCampoContacto>(objetoBO,
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
        /// Obtiene el Id, Nombre de los Campo Contacto(activos) registrados en el sistema
        /// </summary>
        /// <returns></returns>
        public List<CampoContactoFiltroDTO> ObtenerCamposContactoFiltro()
        {
            try
            {
                var lista = GetBy(x => x.Estado == true, y => new CampoContactoFiltroDTO
                {
                    Id = y.Id,
                    Nombre = y.Nombre
                }).ToList();

                return lista;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }
        public List<CampoContactoDTO> ObtenerCamposContacto()
        {
            try
            {
                var lista = GetBy(x => x.Estado == true, y => new CampoContactoDTO
                {
                    Id = y.Id,
                    Nombre = y.Nombre,
                    TipoControl= y.TipoControl,
                    ValoresPreEstablecidos=y.ValoresPreEstablecidos,
                    Procedimiento=y.Procedimiento
                }).ToList();

                return lista;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }
    }
}
