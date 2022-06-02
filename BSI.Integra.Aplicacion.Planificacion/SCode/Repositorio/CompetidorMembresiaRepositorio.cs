using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using AutoMapper;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Planificacion.BO;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using Newtonsoft.Json;

namespace BSI.Integra.Aplicacion.Transversal.Repositorio
{
    public class CompetidorMembresiaRepositorio : BaseRepository<TCompetidorMembresia, CompetidorMembresiaBO>
    {
        #region Metodos Base
        public CompetidorMembresiaRepositorio() : base()
        {
        }
        public CompetidorMembresiaRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<CompetidorMembresiaBO> GetBy(Expression<Func<TCompetidorMembresia, bool>> filter)
        {
            IEnumerable<TCompetidorMembresia> listado = base.GetBy(filter);
            List<CompetidorMembresiaBO> listadoBO = new List<CompetidorMembresiaBO>();
            foreach (var itemEntidad in listado)
            {
                CompetidorMembresiaBO objetoBO = Mapper.Map<TCompetidorMembresia, CompetidorMembresiaBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public CompetidorMembresiaBO FirstById(int id)
        {
            try
            {
                TCompetidorMembresia entidad = base.FirstById(id);
                CompetidorMembresiaBO objetoBO = new CompetidorMembresiaBO();
                Mapper.Map<TCompetidorMembresia, CompetidorMembresiaBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public CompetidorMembresiaBO FirstBy(Expression<Func<TCompetidorMembresia, bool>> filter)
        {
            try
            {
                TCompetidorMembresia entidad = base.FirstBy(filter);
                CompetidorMembresiaBO objetoBO = Mapper.Map<TCompetidorMembresia, CompetidorMembresiaBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(CompetidorMembresiaBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TCompetidorMembresia entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<CompetidorMembresiaBO> listadoBO)
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

        public bool Update(CompetidorMembresiaBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TCompetidorMembresia entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<CompetidorMembresiaBO> listadoBO)
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
        private void AsignacionId(TCompetidorMembresia entidad, CompetidorMembresiaBO objetoBO)
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

        private TCompetidorMembresia MapeoEntidad(CompetidorMembresiaBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TCompetidorMembresia entidad = new TCompetidorMembresia();
                entidad = Mapper.Map<CompetidorMembresiaBO, TCompetidorMembresia>(objetoBO,
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
        /// Obtiene los CompetidorMembresias dado un Id de Competidor
        /// </summary>
        /// <returns></returns>
        public List<CompetidorMembresiaDTO> ObtenerTodoCompetidorMembresiaPorIdCompetidor(int IdCompetidor)
        {
            try
            {
                List<CompetidorMembresiaDTO> ListaCompetidorMembresia = new List<CompetidorMembresiaDTO>();
                var _query = string.Empty;
                _query = "Select Id,  IdCompetidor, IdCertificacion FROM pla.T_CompetidorMembresia WHERE  Estado = 1 AND IdCompetidor=" + IdCompetidor;
                var _resultado = _dapper.QueryDapper(_query, null);
                if (!string.IsNullOrEmpty(_resultado) && !_resultado.Contains("[]"))
                {
                    ListaCompetidorMembresia = JsonConvert.DeserializeObject<List<CompetidorMembresiaDTO>>(_resultado);
                }
                return ListaCompetidorMembresia;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }



    }


}
