using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using AutoMapper;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.GestionPersonas.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using Newtonsoft.Json;

namespace BSI.Integra.Aplicacion.GestionPersonas.Repositorio
{
    /// Repositorio: Gestion de Personas/PersonalAreaTrabajo
    /// Autor: Luis Huallpa - Britsel C. - Edgar S - Jose V.
    /// Fecha: 28/04/2021
    /// <summary>
    /// Repositorio para consultas de gp.T_PersonalAreaTrabajo
    /// </summary>
    public class PersonalAreaTrabajoRepositorio : BaseRepository<TPersonalAreaTrabajo, PersonalAreaTrabajoBO>
    {
        #region Metodos Base
        public PersonalAreaTrabajoRepositorio() : base()
        {
        }
        public PersonalAreaTrabajoRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<PersonalAreaTrabajoBO> GetBy(Expression<Func<TPersonalAreaTrabajo, bool>> filter)
        {
            IEnumerable<TPersonalAreaTrabajo> listado = base.GetBy(filter);
            List<PersonalAreaTrabajoBO> listadoBO = new List<PersonalAreaTrabajoBO>();
            foreach (var itemEntidad in listado)
            {
                PersonalAreaTrabajoBO objetoBO = Mapper.Map<TPersonalAreaTrabajo, PersonalAreaTrabajoBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public PersonalAreaTrabajoBO FirstById(int id)
        {
            try
            {
                TPersonalAreaTrabajo entidad = base.FirstById(id);
                PersonalAreaTrabajoBO objetoBO = new PersonalAreaTrabajoBO();
                Mapper.Map<TPersonalAreaTrabajo, PersonalAreaTrabajoBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public PersonalAreaTrabajoBO FirstBy(Expression<Func<TPersonalAreaTrabajo, bool>> filter)
        {
            try
            {
                TPersonalAreaTrabajo entidad = base.FirstBy(filter);
                PersonalAreaTrabajoBO objetoBO = Mapper.Map<TPersonalAreaTrabajo, PersonalAreaTrabajoBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(PersonalAreaTrabajoBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TPersonalAreaTrabajo entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<PersonalAreaTrabajoBO> listadoBO)
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

        public bool Update(PersonalAreaTrabajoBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TPersonalAreaTrabajo entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<PersonalAreaTrabajoBO> listadoBO)
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
        private void AsignacionId(TPersonalAreaTrabajo entidad, PersonalAreaTrabajoBO objetoBO)
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

        private TPersonalAreaTrabajo MapeoEntidad(PersonalAreaTrabajoBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TPersonalAreaTrabajo entidad = new TPersonalAreaTrabajo();
                entidad = Mapper.Map<PersonalAreaTrabajoBO, TPersonalAreaTrabajo>(objetoBO,
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
        ///Repositorio: PersonalAreaTrabajoRepositorio
        ///Autor: 
        ///Fecha: 06/07/2021
        /// <summary>
        /// Obtiene lista de Area de Trabajo de Personal para Combo
        /// </summary>
        /// <param></param>
        /// <returns> List<FiltroDTO> </returns>
        public List<FiltroDTO> ObtenerAreaTrabajoPersonalNombre()
        {
            try
            {
                var lista = this.GetBy(w => w.Estado == true, w => new FiltroDTO
                {
                    Id = w.Id,
                    Nombre = w.Nombre.ToUpper()
                }).OrderBy(x => x.Id).ToList();

                return lista;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Luis Huallpa - Britsel C. - Edgar S - Jose V.
        /// Fecha: 25/01/2021
        /// Version:1.0
        /// <summary>
        /// Obtiene area de trabajo de personal
        /// </summary>
        /// <returns>Información de Area de Trabajo</returns>
        public List<FiltroDTO> ObtenerAreaTrabajoPersonal()
        {
            try
            {
                var lista = GetBy(w => w.Estado == true, w => new FiltroDTO {
                    Id = w.Id,
                    Nombre = w.Codigo.ToUpper()
                }).OrderBy(x=>x.Id).ToList();
                
                return lista;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        ///Repositorio: PersonalAreTrabajoRepositorio
        ///Autor: Edgar S.
        ///Fecha: 25/01/2021
        /// <summary>
        /// Obtiene el id y nombre
        /// </summary>
        /// <returns>List<FiltroDTO></returns>
        public List<FiltroDTO> ObtenerTodoFiltro()
        {
            try
            {
                return this.GetBy(w => w.Estado == true, w => new FiltroDTO
                {
                    Id = w.Id,
                    Nombre = w.Nombre.ToUpper()
                }).OrderBy(x => x.Id).ToList();

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Luis Huallpa - Britsel C. - Edgar S - Jose V.
        /// Fecha: 25/01/2021
        /// Version:1.0
        /// <summary>
        /// Obtiene area de trabajo de personal para operaciones
        /// </summary>
        /// <returns>Información de Area de Trabajo</returns>
        public List<FiltroDTO> ObtenerAreaTrabajoPersonalParaOperaciones()
        {
            try
            {
                var lista = GetBy(w => w.Estado == true && (w.Codigo.Equals("OP") || w.Codigo.Equals("PL") || w.Codigo.Equals("PO") || w.Codigo.Equals("SO")), w => new FiltroDTO
                {
                    Id = w.Id,
                    Nombre = w.Codigo.ToUpper()
                }).OrderBy(x => x.Id).ToList();

                return lista;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// Autor: Luis Huallpa - Britsel Calluchi. - Edgar Serruto - Jose Villena.
        /// Fecha: 26/08/2021
        /// Version: 1.0
        /// <summary>
        /// Obtiene Id,Nombre para combo
        /// </summary>
        /// <returns>List<FiltroIdNombreDTO></returns>
        public List<FiltroIdNombreDTO> GetFiltroIdNombre()
        {
            var lista = GetBy(x => true, y => new FiltroIdNombreDTO
            {
                Id = y.Id,
                Nombre = y.Nombre+" - "+y.Codigo
            }).ToList();
            return lista;
        }

        ///Repositorio: PersonalAreaTrabajoRepositorio
        ///Autor: Luis Huallpa - Britsel C. - Edgar S - Jose V.
        ///Fecha: 06/07/2021
        /// <summary>
        /// Obtiene lista de personal area de trabajo registrado
        /// </summary>
        /// <returns>List<PersonalAreaTrabajoDTO></returns>
        public List<PersonalAreaTrabajoDTO> ObtenerPersonalAreaTrabajo()
		{
			try
			{
				return this.GetBy(x => true).Select(x => new PersonalAreaTrabajoDTO
				{
					Id = x.Id,
					Nombre = x.Nombre,
					Codigo = x.Codigo
				}).ToList();
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}
        /// Autor: Luis Huallpa - Britsel C. - Edgar S - Jose V.
        /// Fecha: 25/01/2021
        /// Version:1.0
        /// <summary>
        /// Obtiene area de trabajo de personal
        /// </summary>
        /// <param name="id">Id de area</param>
        /// <returns>Información de Area de Trabajo</returns>
        public string getAbreviaturaArea(int id)
        {
            try
            {
                ValorStringDTO abreviatura = new ValorStringDTO();
                var _query = string.Empty;
                _query = "SELECT Valor FROM [gp].[V_PersonalAreaTrabajo] WHERE Id =@id ";
                var tpersonal = _dapper.FirstOrDefault(_query, new { id });
                abreviatura = JsonConvert.DeserializeObject<ValorStringDTO>(tpersonal);
                return abreviatura.Valor;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Jashin Salazar
        /// Fecha: 22/07/2021
        /// Version:1.0
		/// <summary>
		/// Obtiene lista de areas para agenda
		/// </summary>
		/// <returns></returns>
        public List<PersonalAreaTrabajoDTO> ObtenerAreaAgenda()
        {
            try
            {
                List<PersonalAreaTrabajoDTO> areas = new List<PersonalAreaTrabajoDTO>();
                var query = string.Empty;
                query = "SELECT * FROM gp.V_ObtenerAreaAgenda ";
                var queryResultado = _dapper.QueryDapper(query, null);
                if (!queryResultado.Contains("[]") && !string.IsNullOrEmpty(queryResultado))
                {
                    areas = JsonConvert.DeserializeObject<List<PersonalAreaTrabajoDTO>>(queryResultado);
                }
                return areas;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
