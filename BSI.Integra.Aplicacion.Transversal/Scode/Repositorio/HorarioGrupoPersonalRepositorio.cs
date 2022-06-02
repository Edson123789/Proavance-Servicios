using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using AutoMapper;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using System.Collections.Generic;
using System.Text;
using BSI.Integra.Aplicacion.Transversal.Scode.BO;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.DTOs.Scode.DTOs.Transversal;
using System.Linq;
using Newtonsoft.Json;


namespace BSI.Integra.Aplicacion.Transversal.Scode.Repositorio
{
    public class HorarioGrupoPersonalRepositorio : BaseRepository<THorarioGrupoPersonal, HorarioGrupoPersonalBO>
    {
        #region Metodos Base
        public HorarioGrupoPersonalRepositorio() : base()
        {
        }
        public HorarioGrupoPersonalRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<HorarioGrupoPersonalBO> GetBy(Expression<Func<THorarioGrupoPersonal, bool>> filter)
        {
            IEnumerable<THorarioGrupoPersonal> listado = base.GetBy(filter);
            List<HorarioGrupoPersonalBO> listadoBO = new List<HorarioGrupoPersonalBO>();
            foreach (var itemEntidad in listado)
            {
                HorarioGrupoPersonalBO objetoBO = Mapper.Map<THorarioGrupoPersonal, HorarioGrupoPersonalBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public HorarioGrupoPersonalBO FirstById(int id)
        {
            try
            {
                THorarioGrupoPersonal entidad = base.FirstById(id);
                HorarioGrupoPersonalBO objetoBO = new HorarioGrupoPersonalBO();
                Mapper.Map<THorarioGrupoPersonal, HorarioGrupoPersonalBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public HorarioGrupoPersonalBO FirstBy(Expression<Func<THorarioGrupoPersonal, bool>> filter)
        {
            try
            {
                THorarioGrupoPersonal entidad = base.FirstBy(filter);
                HorarioGrupoPersonalBO objetoBO = Mapper.Map<THorarioGrupoPersonal, HorarioGrupoPersonalBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(HorarioGrupoPersonalBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                THorarioGrupoPersonal entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<HorarioGrupoPersonalBO> listadoBO)
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

        public bool Update(HorarioGrupoPersonalBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                THorarioGrupoPersonal entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<HorarioGrupoPersonalBO> listadoBO)
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
        private void AsignacionId(THorarioGrupoPersonal entidad, HorarioGrupoPersonalBO objetoBO)
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

        private THorarioGrupoPersonal MapeoEntidad(HorarioGrupoPersonalBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                THorarioGrupoPersonal entidad = new THorarioGrupoPersonal();
                entidad = Mapper.Map<HorarioGrupoPersonalBO, THorarioGrupoPersonal>(objetoBO,
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

        //Actualiza el estado a false 
        public void DeleteLogicoPorGrupo(int IdGrupo, string usuario, List<int> nuevos)
        {
            try
            {
                List<EliminacionIdsDTO> listaBorrar = new List<EliminacionIdsDTO>();
                string _query = "SELECT Id FROM  gp.T_HorarioGrupoPersonal WHERE Estado = 1 and IdHorarioGrupo = @IdGrupo ";
                var query = _dapper.QueryDapper(_query, new { IdGrupo });
                listaBorrar = JsonConvert.DeserializeObject<List<EliminacionIdsDTO>>(query);
                listaBorrar.RemoveAll(x => nuevos.Any(y => y == x.Id));
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

        public List<HorarioGrupoPersonalDTO> listarGrupoPersonal(int IdGrupo)
        {
            try
            {
                List<HorarioGrupoPersonalDTO> filtro = new List<HorarioGrupoPersonalDTO>();
                var _queryfiltrocriterio = "Select IdPersonal FROM gp.T_HorarioGrupoPersonal where Estado = 1 and IdHorarioGrupo = @IdGrupo";
                var SubfiltroCriterio = _dapper.QueryDapper(_queryfiltrocriterio, new { IdGrupo });
                if (!string.IsNullOrEmpty(SubfiltroCriterio) && !SubfiltroCriterio.Contains("[]"))
                {
                    filtro = JsonConvert.DeserializeObject<List<HorarioGrupoPersonalDTO>>(SubfiltroCriterio);
                }
                return filtro;
            }
            catch (Exception E)
            {
                throw new Exception(E.Message);
            }
        }
    }
}
