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
    public class HorarioGrupoRepositorio : BaseRepository<THorarioGrupo, HorarioGrupoBO>
    {
        #region Metodos Base
        public HorarioGrupoRepositorio() : base()
        {
        }
        public HorarioGrupoRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<HorarioGrupoBO> GetBy(Expression<Func<THorarioGrupo, bool>> filter)
        {
            IEnumerable<THorarioGrupo> listado = base.GetBy(filter);
            List<HorarioGrupoBO> listadoBO = new List<HorarioGrupoBO>();
            foreach (var itemEntidad in listado)
            {
                HorarioGrupoBO objetoBO = Mapper.Map<THorarioGrupo, HorarioGrupoBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public HorarioGrupoBO FirstById(int id)
        {
            try
            {
                THorarioGrupo entidad = base.FirstById(id);
                HorarioGrupoBO objetoBO = new HorarioGrupoBO();
                Mapper.Map<THorarioGrupo, HorarioGrupoBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public HorarioGrupoBO FirstBy(Expression<Func<THorarioGrupo, bool>> filter)
        {
            try
            {
                THorarioGrupo entidad = base.FirstBy(filter);
                HorarioGrupoBO objetoBO = Mapper.Map<THorarioGrupo, HorarioGrupoBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(HorarioGrupoBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                THorarioGrupo entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<HorarioGrupoBO> listadoBO)
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

        public bool Update(HorarioGrupoBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                THorarioGrupo entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<HorarioGrupoBO> listadoBO)
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
        private void AsignacionId(THorarioGrupo entidad, HorarioGrupoBO objetoBO)
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

        private THorarioGrupo MapeoEntidad(HorarioGrupoBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                THorarioGrupo entidad = new THorarioGrupo();
                entidad = Mapper.Map<HorarioGrupoBO, THorarioGrupo>(objetoBO,
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

        public List<HorarioGrupoDTO> getGrupos()
        {
            try
            {

                List<HorarioGrupoDTO> grupos = new List<HorarioGrupoDTO>();
                var _query = string.Empty;
                _query = "SELECT Id,Nombre FROM [gp].[V_HorarioGrupo]  where Estado= 1";
                var horariogrupo = _dapper.QueryDapper(_query, null);
                if (!string.IsNullOrEmpty(horariogrupo) && !horariogrupo.Contains("[]"))
                {
                    grupos = JsonConvert.DeserializeObject<List<HorarioGrupoDTO>>(horariogrupo);
                }
                return grupos;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }
    }
}
