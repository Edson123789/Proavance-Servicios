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
    public class PgeneralProyectoAplicacionProveedorRepositorio : BaseRepository<TPgeneralProyectoAplicacionProveedor, PgeneralProyectoAplicacionProveedorBO>
    {
        #region Metodos Base
        public PgeneralProyectoAplicacionProveedorRepositorio() : base()
        {
        }
        public PgeneralProyectoAplicacionProveedorRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<PgeneralProyectoAplicacionProveedorBO> GetBy(Expression<Func<TPgeneralProyectoAplicacionProveedor, bool>> filter)
        {
            IEnumerable<TPgeneralProyectoAplicacionProveedor> listado = base.GetBy(filter);
            List<PgeneralProyectoAplicacionProveedorBO> listadoBO = new List<PgeneralProyectoAplicacionProveedorBO>();
            foreach (var itemEntidad in listado)
            {
                PgeneralProyectoAplicacionProveedorBO objetoBO = Mapper.Map<TPgeneralProyectoAplicacionProveedor, PgeneralProyectoAplicacionProveedorBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public PgeneralProyectoAplicacionProveedorBO FirstById(int id)
        {
            try
            {
                TPgeneralProyectoAplicacionProveedor entidad = base.FirstById(id);
                PgeneralProyectoAplicacionProveedorBO objetoBO = Mapper.Map<TPgeneralProyectoAplicacionProveedor, PgeneralProyectoAplicacionProveedorBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public PgeneralProyectoAplicacionProveedorBO FirstBy(Expression<Func<TPgeneralProyectoAplicacionProveedor, bool>> filter)
        {
            try
            {
                TPgeneralProyectoAplicacionProveedor entidad = base.FirstBy(filter);
                PgeneralProyectoAplicacionProveedorBO objetoBO = Mapper.Map<TPgeneralProyectoAplicacionProveedor, PgeneralProyectoAplicacionProveedorBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(PgeneralProyectoAplicacionProveedorBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TPgeneralProyectoAplicacionProveedor entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<PgeneralProyectoAplicacionProveedorBO> listadoBO)
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

        public bool Update(PgeneralProyectoAplicacionProveedorBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TPgeneralProyectoAplicacionProveedor entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<PgeneralProyectoAplicacionProveedorBO> listadoBO)
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
        private void AsignacionId(TPgeneralProyectoAplicacionProveedor entidad, PgeneralProyectoAplicacionProveedorBO objetoBO)
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

        private TPgeneralProyectoAplicacionProveedor MapeoEntidad(PgeneralProyectoAplicacionProveedorBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TPgeneralProyectoAplicacionProveedor entidad = new TPgeneralProyectoAplicacionProveedor();
                entidad = Mapper.Map<PgeneralProyectoAplicacionProveedorBO, TPgeneralProyectoAplicacionProveedor>(objetoBO,
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
        public void DeleteLogicoPorPrograma(int idPGeneral, string usuario, List<PgeneralProyectoAplicacionProveedorDTO> nuevos)
        {
            try
            {
                List<EliminacionIdsDTO> listaBorrar = new List<EliminacionIdsDTO>();
                string _query = "SELECT Id FROM  pla.T_PgeneralProyectoAplicacionProveedor WHERE Estado = 1 and IdPGeneral = @idPGeneral ";
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
