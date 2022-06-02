
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

namespace BSI.Integra.Aplicacion.Transversal.Repositorio
{
    public class TipoFormularioRepositorio : BaseRepository<TTipoFormulario, TipoFormularioBO>
    {
        #region Metodos Base
        public TipoFormularioRepositorio() : base()
        {
        }
        public TipoFormularioRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<TipoFormularioBO> GetBy(Expression<Func<TTipoFormulario, bool>> filter)
        {
            IEnumerable<TTipoFormulario> listado = base.GetBy(filter);
            List<TipoFormularioBO> listadoBO = new List<TipoFormularioBO>();
            foreach (var itemEntidad in listado)
            {
                TipoFormularioBO objetoBO = Mapper.Map<TTipoFormulario, TipoFormularioBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public TipoFormularioBO FirstById(int id)
        {
            try
            {
                TTipoFormulario entidad = base.FirstById(id);
                TipoFormularioBO objetoBO = new TipoFormularioBO();
                Mapper.Map<TTipoFormulario, TipoFormularioBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public TipoFormularioBO FirstBy(Expression<Func<TTipoFormulario, bool>> filter)
        {
            try
            {
                TTipoFormulario entidad = base.FirstBy(filter);
                TipoFormularioBO objetoBO = Mapper.Map<TTipoFormulario, TipoFormularioBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(TipoFormularioBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TTipoFormulario entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<TipoFormularioBO> listadoBO)
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

        public bool Update(TipoFormularioBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TTipoFormulario entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<TipoFormularioBO> listadoBO)
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
        private void AsignacionId(TTipoFormulario entidad, TipoFormularioBO objetoBO)
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

        private TTipoFormulario MapeoEntidad(TipoFormularioBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TTipoFormulario entidad = new TTipoFormulario();
                entidad = Mapper.Map<TipoFormularioBO, TTipoFormulario>(objetoBO,
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
        /// Obtiene toda la lista de TipoFormulario  para ser usado en combobox
        /// </summary>
        /// <returns> id, nombre, descripcion, meta</returns>
        public List<TipoFormularioDTO> ObtenerListaTipoFormulario()
        {
            try
            {
                List<TipoFormularioDTO> TipoFormulario = new List<TipoFormularioDTO>();
                var TipoFormularioDB = GetBy(x => x.Estado == true, x => new { x.Id, x.Nombre });
                foreach (var item in TipoFormularioDB)
                {
                    var TipoFormularioTemp = new TipoFormularioDTO()
                    {
                        Id = item.Id,
                        Nombre = item.Nombre
                        
                    };
                    TipoFormulario.Add(TipoFormularioTemp);
                }
                return TipoFormulario;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
