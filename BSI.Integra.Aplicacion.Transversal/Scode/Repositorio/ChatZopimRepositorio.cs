using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using AutoMapper;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;

namespace BSI.Integra.Aplicacion.Transversal.Repositorio
{
    public class ChatZopimRepositorio : BaseRepository<TChatZopim, ChatZopimBO>
    {
        #region Metodos Base
        public ChatZopimRepositorio() : base()
        {
        }
        public ChatZopimRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<ChatZopimBO> GetBy(Expression<Func<TChatZopim, bool>> filter)
        {
            IEnumerable<TChatZopim> listado = base.GetBy(filter);
            List<ChatZopimBO> listadoBO = new List<ChatZopimBO>();
            foreach (var itemEntidad in listado)
            {
                ChatZopimBO objetoBO = Mapper.Map<TChatZopim, ChatZopimBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public ChatZopimBO FirstById(int id)
        {
            try
            {
                TChatZopim entidad = base.FirstById(id);
                ChatZopimBO objetoBO = new ChatZopimBO();
                Mapper.Map<TChatZopim, ChatZopimBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public ChatZopimBO FirstBy(Expression<Func<TChatZopim, bool>> filter)
        {
            try
            {
                TChatZopim entidad = base.FirstBy(filter);
                ChatZopimBO objetoBO = Mapper.Map<TChatZopim, ChatZopimBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(ChatZopimBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TChatZopim entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<ChatZopimBO> listadoBO)
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

        public bool Update(ChatZopimBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TChatZopim entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<ChatZopimBO> listadoBO)
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
        private void AsignacionId(TChatZopim entidad, ChatZopimBO objetoBO)
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

        private TChatZopim MapeoEntidad(ChatZopimBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TChatZopim entidad = new TChatZopim();
                entidad = Mapper.Map<ChatZopimBO, TChatZopim>(objetoBO,
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
        /// Obtiene el Id, Nombre de los Chat Zopin(activos) registrado en el sistema
        /// </summary>
        /// <returns></returns>
        public List<ChatZopimFiltroDTO> ObtenerChatZopinFiltro()
        {
            try
            {
                var lista = GetBy(x => x.Estado == true, y => new ChatZopimFiltroDTO
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
    }
}
