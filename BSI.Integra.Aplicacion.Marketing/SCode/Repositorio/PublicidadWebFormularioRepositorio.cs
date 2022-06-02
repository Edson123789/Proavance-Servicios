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
    public class PublicidadWebFormularioRepositorio : BaseRepository<TPublicidadWebFormulario, PublicidadWebFormularioBO>
    {
        #region Metodos Base
        public PublicidadWebFormularioRepositorio() : base()
        {
        }
        public PublicidadWebFormularioRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<PublicidadWebFormularioBO> GetBy(Expression<Func<TPublicidadWebFormulario, bool>> filter)
        {
            IEnumerable<TPublicidadWebFormulario> listado = base.GetBy(filter);
            List<PublicidadWebFormularioBO> listadoBO = new List<PublicidadWebFormularioBO>();
            foreach (var itemEntidad in listado)
            {
                PublicidadWebFormularioBO objetoBO = Mapper.Map<TPublicidadWebFormulario, PublicidadWebFormularioBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public PublicidadWebFormularioBO FirstById(int id)
        {
            try
            {
                TPublicidadWebFormulario entidad = base.FirstById(id);
                PublicidadWebFormularioBO objetoBO = new PublicidadWebFormularioBO();
                Mapper.Map<TPublicidadWebFormulario, PublicidadWebFormularioBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public PublicidadWebFormularioBO FirstBy(Expression<Func<TPublicidadWebFormulario, bool>> filter)
        {
            try
            {
                TPublicidadWebFormulario entidad = base.FirstBy(filter);
                PublicidadWebFormularioBO objetoBO = Mapper.Map<TPublicidadWebFormulario, PublicidadWebFormularioBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(PublicidadWebFormularioBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TPublicidadWebFormulario entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<PublicidadWebFormularioBO> listadoBO)
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

        public bool Update(PublicidadWebFormularioBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TPublicidadWebFormulario entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<PublicidadWebFormularioBO> listadoBO)
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
        private void AsignacionId(TPublicidadWebFormulario entidad, PublicidadWebFormularioBO objetoBO)
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

        private TPublicidadWebFormulario MapeoEntidad(PublicidadWebFormularioBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TPublicidadWebFormulario entidad = new TPublicidadWebFormulario();
                entidad = Mapper.Map<PublicidadWebFormularioBO, TPublicidadWebFormulario>(objetoBO,
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
        /// Obtiene el Formularios (activos) asociados a una publicidad Web
        /// </summary>
        /// <returns></returns>
        public PublicidadWebFormularioDTO ObtenerFormulariosPorPublicidad(int idPublicidad)
        {
            try
            {
                var lista = GetBy(x => x.Estado == true && x.IdPublicidadWeb == idPublicidad, y => new PublicidadWebFormularioDTO
                {
                    Id = y.Id,
                    IdPublicidadWeb = y.IdPublicidadWeb,
                    IdFormularioSolicitudTextoBoton = y.IdFormularioSolicitudTextoBoton,
                    Nombre = y.Nombre,
                    Titulo = y.Titulo,
                    Descripcion = y.Descripcion,
                    TextoBoton = y.TextoBoton
                }).FirstOrDefault();

                return lista;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }
        /// <summary>
        /// Elimina (Actualiza estado a false ) todos los formularios Publicidad asociados a una PublicidadWeb
        /// </summary>
        /// <returns></returns>
        public void EliminacionLogicoPorPublicidadWeb(int idPublicidad, string usuario, List<PublicidadWebFormularioDTO> nuevos)
        {
            try
            {
                var listaBorrar = GetBy(x => x.IdPublicidadWeb == idPublicidad && x.Estado == true).ToList();
                listaBorrar.RemoveAll(x => nuevos.Any(y => y.IdFormularioSolicitudTextoBoton == x.IdFormularioSolicitudTextoBoton));
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
