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
using Newtonsoft.Json;

namespace BSI.Integra.Aplicacion.Marketing.Repositorio
{
    public class PublicidadWebFormularioCampoRepositorio : BaseRepository<TPublicidadWebFormularioCampo, PublicidadWebFormularioCampoBO>
    {
        #region Metodos Base
        public PublicidadWebFormularioCampoRepositorio() : base()
        {
        }
        public PublicidadWebFormularioCampoRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<PublicidadWebFormularioCampoBO> GetBy(Expression<Func<TPublicidadWebFormularioCampo, bool>> filter)
        {
            IEnumerable<TPublicidadWebFormularioCampo> listado = base.GetBy(filter);
            List<PublicidadWebFormularioCampoBO> listadoBO = new List<PublicidadWebFormularioCampoBO>();
            foreach (var itemEntidad in listado)
            {
                PublicidadWebFormularioCampoBO objetoBO = Mapper.Map<TPublicidadWebFormularioCampo, PublicidadWebFormularioCampoBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public PublicidadWebFormularioCampoBO FirstById(int id)
        {
            try
            {
                TPublicidadWebFormularioCampo entidad = base.FirstById(id);
                PublicidadWebFormularioCampoBO objetoBO = new PublicidadWebFormularioCampoBO();
                Mapper.Map<TPublicidadWebFormularioCampo, PublicidadWebFormularioCampoBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public PublicidadWebFormularioCampoBO FirstBy(Expression<Func<TPublicidadWebFormularioCampo, bool>> filter)
        {
            try
            {
                TPublicidadWebFormularioCampo entidad = base.FirstBy(filter);
                PublicidadWebFormularioCampoBO objetoBO = Mapper.Map<TPublicidadWebFormularioCampo, PublicidadWebFormularioCampoBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(PublicidadWebFormularioCampoBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TPublicidadWebFormularioCampo entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<PublicidadWebFormularioCampoBO> listadoBO)
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

        public bool Update(PublicidadWebFormularioCampoBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TPublicidadWebFormularioCampo entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<PublicidadWebFormularioCampoBO> listadoBO)
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
        private void AsignacionId(TPublicidadWebFormularioCampo entidad, PublicidadWebFormularioCampoBO objetoBO)
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

        private TPublicidadWebFormularioCampo MapeoEntidad(PublicidadWebFormularioCampoBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TPublicidadWebFormularioCampo entidad = new TPublicidadWebFormularioCampo();
                entidad = Mapper.Map<PublicidadWebFormularioCampoBO, TPublicidadWebFormularioCampo>(objetoBO,
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
        /// Obtiene la lista de CamposFormulario (activos) asociados a un Formulario publicidad 
        /// </summary>
        /// <returns></returns>
        public List<PublicidadWebFormularioCampoDTO> ObtenerCamposFormularioPorPublicidadFormulario(int idPublicidad)
        {
            try
            {
                var lista = GetBy(x => x.Estado == true && x.IdPublicidadWebFormulario == idPublicidad, y => new PublicidadWebFormularioCampoDTO
                {
                    Id = y.Id,
                    IdPublicidadWebFormulario = y.IdPublicidadWebFormulario,
                    IdCampoContacto = y.IdCampoContacto,
                    Nombre = y.Nombre,
                    Siempre = y.Siempre,
                    Inteligente = y.Inteligente,
                    Probabilidad = y.Probabilidad,
                    Orden = y.Orden
                }).ToList();

                return lista;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }
        /// <summary>
        /// Elimina (Actualiza estado a false ) todos los Campos de Formulario Publicidad asociados a una PublicidadWeb
        /// </summary>
        /// <returns></returns>
        public void EliminacionLogicoPorPublicidadFormulario(int idPublicidadFormulario, string usuario, List<PublicidadWebFormularioCampoDTO> nuevos)
        {
            try
            {
                var listaBorrar = GetBy(x => x.IdPublicidadWebFormulario == idPublicidadFormulario && x.Estado == true).ToList();
                listaBorrar.RemoveAll(x => nuevos.Any(y => y.IdCampoContacto == x.IdCampoContacto));
                foreach (var item in listaBorrar)
                {
                    Delete(item.Id, usuario);
                }

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

    }
}
