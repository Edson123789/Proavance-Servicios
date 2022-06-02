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
using Newtonsoft.Json;

namespace BSI.Integra.Aplicacion.Transversal.Repositorio
{
    public class PgeneralProyectoAplicacionModalidadRepositorio : BaseRepository<TPgeneralProyectoAplicacionModalidad, PgeneralProyectoAplicacionModalidadBO>
    {
        #region Metodos Base
        public PgeneralProyectoAplicacionModalidadRepositorio() : base()
        {
        }
        public PgeneralProyectoAplicacionModalidadRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<PgeneralProyectoAplicacionModalidadBO> GetBy(Expression<Func<TPgeneralProyectoAplicacionModalidad, bool>> filter)
        {
            IEnumerable<TPgeneralProyectoAplicacionModalidad> listado = base.GetBy(filter);
            List<PgeneralProyectoAplicacionModalidadBO> listadoBO = new List<PgeneralProyectoAplicacionModalidadBO>();
            foreach (var itemEntidad in listado)
            {
                PgeneralProyectoAplicacionModalidadBO objetoBO = Mapper.Map<TPgeneralProyectoAplicacionModalidad, PgeneralProyectoAplicacionModalidadBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public PgeneralProyectoAplicacionModalidadBO FirstById(int id)
        {
            try
            {
                TPgeneralProyectoAplicacionModalidad entidad = base.FirstById(id);
                PgeneralProyectoAplicacionModalidadBO objetoBO = Mapper.Map<TPgeneralProyectoAplicacionModalidad, PgeneralProyectoAplicacionModalidadBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public PgeneralProyectoAplicacionModalidadBO FirstBy(Expression<Func<TPgeneralProyectoAplicacionModalidad, bool>> filter)
        {
            try
            {
                TPgeneralProyectoAplicacionModalidad entidad = base.FirstBy(filter);
                PgeneralProyectoAplicacionModalidadBO objetoBO = Mapper.Map<TPgeneralProyectoAplicacionModalidad, PgeneralProyectoAplicacionModalidadBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(PgeneralProyectoAplicacionModalidadBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TPgeneralProyectoAplicacionModalidad entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<PgeneralProyectoAplicacionModalidadBO> listadoBO)
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

        public bool Update(PgeneralProyectoAplicacionModalidadBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TPgeneralProyectoAplicacionModalidad entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<PgeneralProyectoAplicacionModalidadBO> listadoBO)
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
        private void AsignacionId(TPgeneralProyectoAplicacionModalidad entidad, PgeneralProyectoAplicacionModalidadBO objetoBO)
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

        private TPgeneralProyectoAplicacionModalidad MapeoEntidad(PgeneralProyectoAplicacionModalidadBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TPgeneralProyectoAplicacionModalidad entidad = new TPgeneralProyectoAplicacionModalidad();
                entidad = Mapper.Map<PgeneralProyectoAplicacionModalidadBO, TPgeneralProyectoAplicacionModalidad>(objetoBO,
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

        public void DeleteLogicoPorPrograma(int idPGeneral, string usuario, List<PgeneralProyectoAplicacionModalidadDTO> nuevos)
        {
            try
            {
                List<EliminacionIdsDTO> listaBorrar = new List<EliminacionIdsDTO>();
                string _query = "SELECT Id FROM  pla.T_PgeneralProyectoAplicacionModalidad WHERE Estado = 1 and IdPGeneral = @idPGeneral ";
                var query = _dapper.QueryDapper(_query, new { idPGeneral });
                listaBorrar = JsonConvert.DeserializeObject<List<EliminacionIdsDTO>>(query);
                listaBorrar.RemoveAll(x => nuevos.Any(y => y.Id == x.Id));
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
