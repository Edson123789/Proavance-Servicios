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
    public class AscensoMembresiaRepositorio : BaseRepository<TAscensoMembresia, AscensoMembresiaBO>
    {
        #region Metodos Base
        public AscensoMembresiaRepositorio() : base()
        {
        }
        public AscensoMembresiaRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<AscensoMembresiaBO> GetBy(Expression<Func<TAscensoMembresia, bool>> filter)
        {
            IEnumerable<TAscensoMembresia> listado = base.GetBy(filter);
            List<AscensoMembresiaBO> listadoBO = new List<AscensoMembresiaBO>();
            foreach (var itemEntidad in listado)
            {
                AscensoMembresiaBO objetoBO = Mapper.Map<TAscensoMembresia, AscensoMembresiaBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public AscensoMembresiaBO FirstById(int id)
        {
            try
            {
                TAscensoMembresia entidad = base.FirstById(id);
                AscensoMembresiaBO objetoBO = new AscensoMembresiaBO();
                Mapper.Map<TAscensoMembresia, AscensoMembresiaBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public AscensoMembresiaBO FirstBy(Expression<Func<TAscensoMembresia, bool>> filter)
        {
            try
            {
                TAscensoMembresia entidad = base.FirstBy(filter);
                AscensoMembresiaBO objetoBO = Mapper.Map<TAscensoMembresia, AscensoMembresiaBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(AscensoMembresiaBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TAscensoMembresia entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<AscensoMembresiaBO> listadoBO)
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

        public bool Update(AscensoMembresiaBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TAscensoMembresia entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<AscensoMembresiaBO> listadoBO)
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
        private void AsignacionId(TAscensoMembresia entidad, AscensoMembresiaBO objetoBO)
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

        private TAscensoMembresia MapeoEntidad(AscensoMembresiaBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TAscensoMembresia entidad = new TAscensoMembresia();
                entidad = Mapper.Map<AscensoMembresiaBO, TAscensoMembresia>(objetoBO,
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
        /// Obtiene los AscensoMembresias dado un IdAscenso
        /// </summary>
        /// <returns></returns>
        public List<AscensoCertificacionDTO> ObtenerTodoAscensoMembresiaPorIdAscenso(int IdAscenso)
        {
            try
            {
                List<AscensoCertificacionDTO> AscensoMembresias = new List<AscensoCertificacionDTO>();
                var _query = string.Empty;
                _query = "Select Id,  IdAscenso, IdCertificacion FROM pla.T_AscensoMembresia WHERE  Estado = 1 AND IdAscenso="+IdAscenso;
                var _resultado = _dapper.QueryDapper(_query, null);
                if (!string.IsNullOrEmpty(_resultado) && !_resultado.Contains("[]"))
                {
                    AscensoMembresias = JsonConvert.DeserializeObject<List<AscensoCertificacionDTO>>(_resultado);
                }
                return AscensoMembresias;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

		
    }


}
