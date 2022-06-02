using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using AutoMapper;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Aplicacion.Transversal.Helper;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;

namespace BSI.Integra.Aplicacion.Transversal.Repositorio
{
    public class TipoInteraccionRepositorio : BaseRepository<TTipoInteracccion, TipoInteraccionBO>
    {
        #region Metodos Base
        public TipoInteraccionRepositorio() : base()
        {
        }
        public TipoInteraccionRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<TipoInteraccionBO> GetBy(Expression<Func<TTipoInteracccion, bool>> filter)
        {
            IEnumerable<TTipoInteracccion> listado = base.GetBy(filter);
            List<TipoInteraccionBO> listadoBO = new List<TipoInteraccionBO>();
            foreach (var itemEntidad in listado)
            {
                TipoInteraccionBO objetoBO = Mapper.Map<TTipoInteracccion, TipoInteraccionBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public TipoInteraccionBO FirstById(int id)
        {
            try
            {
                TTipoInteracccion entidad = base.FirstById(id);
                TipoInteraccionBO objetoBO = new TipoInteraccionBO();
                Mapper.Map<TTipoInteracccion, TipoInteraccionBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public TipoInteraccionBO FirstBy(Expression<Func<TTipoInteracccion, bool>> filter)
        {
            try
            {
                TTipoInteracccion entidad = base.FirstBy(filter);
                TipoInteraccionBO objetoBO = Mapper.Map<TTipoInteracccion, TipoInteraccionBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(TipoInteraccionBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TTipoInteracccion entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<TipoInteraccionBO> listadoBO)
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

        public bool Update(TipoInteraccionBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TTipoInteracccion entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<TipoInteraccionBO> listadoBO)
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
        private void AsignacionId(TTipoInteracccion entidad, TipoInteraccionBO objetoBO)
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

        private TTipoInteracccion MapeoEntidad(TipoInteraccionBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TTipoInteracccion entidad = new TTipoInteracccion();
                entidad = Mapper.Map<TipoInteraccionBO, TTipoInteracccion>(objetoBO,
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
        /// Obtiene toda la lista de TipoInteraccion de canal formulario  para ser usado en combobox
        /// </summary>
        /// <returns> id, nombre, descripcion, meta</returns>
        public List<FiltroDTO> ObtenerPorTipoInteraccionGeneralFormulario()
        {
            try
            {
                return this.GetBy(x => x.Estado == true &&  x.IdTipoInteraccionGeneral == ValorEstatico.IdTipoInteraccionGeneralFormulario, x => new FiltroDTO {Id =  x.Id, Nombre = x.Nombre }).ToList();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Obtiene todos los registros 
        /// </summary>
        /// <returns></returns>
        public List<TipoInteraccionDatosDTO> ObtenerTodoGrid()
        {
            try
            {
                var listaAmbiente = GetBy(x => true, y => new TipoInteraccionDatosDTO
                {
                    Id = y.Id,
                    Nombre = y.Nombre,
                    Canal = y.Canal,
                }).OrderByDescending(x => x.Id).ToList();

                return listaAmbiente;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
