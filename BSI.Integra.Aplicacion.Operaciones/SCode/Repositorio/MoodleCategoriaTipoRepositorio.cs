using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using AutoMapper;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.DTOs.Scode.DTOs.Operaciones;
using BSI.Integra.Aplicacion.Operaciones.BO;
using BSI.Integra.Aplicacion.Operaciones.SCode.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using Newtonsoft.Json;


namespace BSI.Integra.Aplicacion.Operaciones.Repositorio
{
    public class MoodleCategoriaTipoRepositorio : BaseRepository<TMoodleCategoriaTipo, MoodleCategoriaTipoBO>
    {
        #region Metodos Base
        public MoodleCategoriaTipoRepositorio() : base()
        {
        }
        public MoodleCategoriaTipoRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<MoodleCategoriaTipoBO> GetBy(Expression<Func<TMoodleCategoriaTipo, bool>> filter)
        {
            IEnumerable<TMoodleCategoriaTipo> listado = base.GetBy(filter);
            List<MoodleCategoriaTipoBO> listadoBO = new List<MoodleCategoriaTipoBO>();
            foreach (var itemEntidad in listado)
            {
                MoodleCategoriaTipoBO objetoBO = Mapper.Map<TMoodleCategoriaTipo, MoodleCategoriaTipoBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public MoodleCategoriaTipoBO FirstById(int id)
        {
            try
            {
                TMoodleCategoriaTipo entidad = base.FirstById(id);
                MoodleCategoriaTipoBO objetoBO = new MoodleCategoriaTipoBO();
                Mapper.Map<TMoodleCategoriaTipo, MoodleCategoriaTipoBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public MoodleCategoriaTipoBO FirstBy(Expression<Func<TMoodleCategoriaTipo, bool>> filter)
        {
            try
            {
                TMoodleCategoriaTipo entidad = base.FirstBy(filter);
                MoodleCategoriaTipoBO objetoBO = Mapper.Map<TMoodleCategoriaTipo, MoodleCategoriaTipoBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(MoodleCategoriaTipoBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TMoodleCategoriaTipo entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<MoodleCategoriaTipoBO> listadoBO)
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

        public bool Update(MoodleCategoriaTipoBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TMoodleCategoriaTipo entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<MoodleCategoriaTipoBO> listadoBO)
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
        private void AsignacionId(TMoodleCategoriaTipo entidad, MoodleCategoriaTipoBO objetoBO)
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

        private TMoodleCategoriaTipo MapeoEntidad(MoodleCategoriaTipoBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TMoodleCategoriaTipo entidad = new TMoodleCategoriaTipo();
                entidad = Mapper.Map<MoodleCategoriaTipoBO, TMoodleCategoriaTipo>(objetoBO,
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
        /// Este método obtiene una lista de categorias Moodle por nombre mediante Id
        /// </summary>
        /// <returns></returns>
        public List<TipoCategoriaMoodleDTO> ObtenerCategoriasPorNombre()
        {
            try
            {
                return this.GetBy(x => x.Estado == true).Select(x => new TipoCategoriaMoodleDTO
                {
                    Id = x.Id,
                    Nombre = x.Nombre
                }).ToList();

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
