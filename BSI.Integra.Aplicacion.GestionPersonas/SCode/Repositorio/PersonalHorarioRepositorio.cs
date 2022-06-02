using System;
using System.Collections.Generic;
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
    /// Repositorio: Gestion de Personas/PersonalHorario
    /// Autor: Luis Huallpa - Britsel C. - Edgar S - Jose V.
    /// Fecha: 28/04/2021
    /// <summary>
    /// Repositorio para consultas de gp.T_PersonalHorario
    /// </summary>
    public class PersonalHorarioRepositorio : BaseRepository<TPersonalHorario, PersonalHorarioBO>
    {
        #region Metodos Base
        public PersonalHorarioRepositorio() : base()
        {
        }
        public PersonalHorarioRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<PersonalHorarioBO> GetBy(Expression<Func<TPersonalHorario, bool>> filter)
        {
            IEnumerable<TPersonalHorario> listado = base.GetBy(filter);
            List<PersonalHorarioBO> listadoBO = new List<PersonalHorarioBO>();
            foreach (var itemEntidad in listado)
            {
                PersonalHorarioBO objetoBO = Mapper.Map<TPersonalHorario, PersonalHorarioBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public PersonalHorarioBO FirstById(int id)
        {
            try
            {
                TPersonalHorario entidad = base.FirstById(id);
                PersonalHorarioBO objetoBO = new PersonalHorarioBO();
                Mapper.Map<TPersonalHorario, PersonalHorarioBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public PersonalHorarioBO FirstBy(Expression<Func<TPersonalHorario, bool>> filter)
        {
            try
            {
                TPersonalHorario entidad = base.FirstBy(filter);
                PersonalHorarioBO objetoBO = Mapper.Map<TPersonalHorario, PersonalHorarioBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(PersonalHorarioBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TPersonalHorario entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<PersonalHorarioBO> listadoBO)
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

        public bool Update(PersonalHorarioBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TPersonalHorario entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<PersonalHorarioBO> listadoBO)
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
        private void AsignacionId(TPersonalHorario entidad, PersonalHorarioBO objetoBO)
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

        private TPersonalHorario MapeoEntidad(PersonalHorarioBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TPersonalHorario entidad = new TPersonalHorario();
                entidad = Mapper.Map<PersonalHorarioBO, TPersonalHorario>(objetoBO,
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
 
        ///Repositorio: PersonalHorario
        ///Autor: Jose V.
        ///Fecha: 28/04/2021
        /// <summary>
        /// Obtiene el Horario del Asesor de Lunes a Domingos
        /// </summary>        
        /// <param name="idPersonal"> Id del Personal </param>
        /// <returns> Horario del Personal : HorarioAsesorDTO </returns>
        public HorarioAsesorDTO ObtenerHorarioAsesor(int idPersonal)
        {
            try
            {
                string queryhorario = "Select * from gp.V_TPersonalHorario_HorarioAsesor Where IdPersonal=@IdPersonal";
                var queryhorarioDB = _dapper.FirstOrDefault(queryhorario, new { IdPersonal = idPersonal });
                HorarioAsesorDTO horario = JsonConvert.DeserializeObject<HorarioAsesorDTO>(queryhorarioDB);
                return horario;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
           
        }

        /// <summary>
        /// Obtiene el Horario del Asesor de Lunes a Domingos
        /// </summary>
        /// <param name="idPersonal"></param>
        /// <returns></returns>
        public List<HorarioDTO> ObtenerHorario(int idPersonal)
        {
            try
            {
                string _queryhorario = "Select * from gp.V_TPersonalHorario_HorarioAsesor Where IdPersonal=@IdPersonal";
                var queryhorario = _dapper.QueryDapper(_queryhorario, new { IdPersonal = idPersonal });
                List<HorarioDTO> horario = JsonConvert.DeserializeObject<List<HorarioDTO>>(queryhorario);
                return horario;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }

        }
    }
}
