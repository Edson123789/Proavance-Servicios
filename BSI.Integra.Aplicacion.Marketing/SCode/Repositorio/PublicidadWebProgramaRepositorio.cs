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
    public class PublicidadWebProgramaRepositorio : BaseRepository<TPublicidadWebPrograma, PublicidadWebProgramaBO>
    {
        #region Metodos Base
        public PublicidadWebProgramaRepositorio() : base()
        {
        }
        public PublicidadWebProgramaRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<PublicidadWebProgramaBO> GetBy(Expression<Func<TPublicidadWebPrograma, bool>> filter)
        {
            IEnumerable<TPublicidadWebPrograma> listado = base.GetBy(filter);
            List<PublicidadWebProgramaBO> listadoBO = new List<PublicidadWebProgramaBO>();
            foreach (var itemEntidad in listado)
            {
                PublicidadWebProgramaBO objetoBO = Mapper.Map<TPublicidadWebPrograma, PublicidadWebProgramaBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public PublicidadWebProgramaBO FirstById(int id)
        {
            try
            {
                TPublicidadWebPrograma entidad = base.FirstById(id);
                PublicidadWebProgramaBO objetoBO = new PublicidadWebProgramaBO();
                Mapper.Map<TPublicidadWebPrograma, PublicidadWebProgramaBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public PublicidadWebProgramaBO FirstBy(Expression<Func<TPublicidadWebPrograma, bool>> filter)
        {
            try
            {
                TPublicidadWebPrograma entidad = base.FirstBy(filter);
                PublicidadWebProgramaBO objetoBO = Mapper.Map<TPublicidadWebPrograma, PublicidadWebProgramaBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(PublicidadWebProgramaBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TPublicidadWebPrograma entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<PublicidadWebProgramaBO> listadoBO)
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

        public bool Update(PublicidadWebProgramaBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TPublicidadWebPrograma entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<PublicidadWebProgramaBO> listadoBO)
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
        private void AsignacionId(TPublicidadWebPrograma entidad, PublicidadWebProgramaBO objetoBO)
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

        private TPublicidadWebPrograma MapeoEntidad(PublicidadWebProgramaBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TPublicidadWebPrograma entidad = new TPublicidadWebPrograma();
                entidad = Mapper.Map<PublicidadWebProgramaBO, TPublicidadWebPrograma>(objetoBO,
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
        /// Obtiene la lista de Programas (activos) asociados a una publicidad Web
        /// </summary>
        /// <returns></returns>
        public List<PublicidadWebProgramaDTO> ObtenerProgramasPorPublicidad(int idPublicidad)
        {
            try
            {
                var lista = GetBy(x => x.Estado == true && x.IdPublicidadWeb == idPublicidad, y => new PublicidadWebProgramaDTO
                {
                    Id = y.Id,
                    IdPublicidadWeb = y.IdPublicidadWeb,
                    IdPgeneral = y.IdPgeneral,
                    Nombre = y.Nombre,
                    OrdenPrograma = y.OrdenPrograma,
                    ModificarInformacion = y.ModificarInformacion,
                    Duracion = y.Duracion,
                    Inicios = y.Inicios,
                    Precios = y.Precios,
                }).ToList();

                return lista;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }
        /// <summary>
        /// Elimina (Actualiza estado a false ) todos los programas Publicidad asociados a una PublicidadWeb
        /// </summary>
        /// <returns></returns>
        public void EliminacionLogicoPorPublicidadWeb(int idPublicidad, string usuario, List<PublicidadWebProgramaDTO> nuevos)
        {
            try
            {
                var listaBorrar = GetBy(x => x.IdPublicidadWeb == idPublicidad && x.Estado == true).ToList();
                listaBorrar.RemoveAll(x => nuevos.Any(y => y.IdPgeneral == x.IdPgeneral));
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
