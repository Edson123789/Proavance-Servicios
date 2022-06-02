using AutoMapper;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.DTOs.Operaciones;
using BSI.Integra.Aplicacion.Operaciones.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace BSI.Integra.Aplicacion.Operaciones.Repositorio
{
    public class ProyectoAplicacionEntregaVersionPwRepositorio : BaseRepository<TProyectoAplicacionEntregaVersionPw, ProyectoAplicacionEntregaVersionPwBO>
    {
        #region Metodos Base
        public ProyectoAplicacionEntregaVersionPwRepositorio() : base()
        {
        }
        public ProyectoAplicacionEntregaVersionPwRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<ProyectoAplicacionEntregaVersionPwBO> GetBy(Expression<Func<TProyectoAplicacionEntregaVersionPw, bool>> filter)
        {
            IEnumerable<TProyectoAplicacionEntregaVersionPw> listado = base.GetBy(filter);
            List<ProyectoAplicacionEntregaVersionPwBO> listadoBO = new List<ProyectoAplicacionEntregaVersionPwBO>();
            foreach (var itemEntidad in listado)
            {
                ProyectoAplicacionEntregaVersionPwBO objetoBO = Mapper.Map<TProyectoAplicacionEntregaVersionPw, ProyectoAplicacionEntregaVersionPwBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public ProyectoAplicacionEntregaVersionPwBO FirstById(int id)
        {
            try
            {
                TProyectoAplicacionEntregaVersionPw entidad = base.FirstById(id);
                ProyectoAplicacionEntregaVersionPwBO objetoBO = new ProyectoAplicacionEntregaVersionPwBO();
                Mapper.Map<TProyectoAplicacionEntregaVersionPw, ProyectoAplicacionEntregaVersionPwBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public ProyectoAplicacionEntregaVersionPwBO FirstBy(Expression<Func<TProyectoAplicacionEntregaVersionPw, bool>> filter)
        {
            try
            {
                TProyectoAplicacionEntregaVersionPw entidad = base.FirstBy(filter);
                ProyectoAplicacionEntregaVersionPwBO objetoBO = Mapper.Map<TProyectoAplicacionEntregaVersionPw, ProyectoAplicacionEntregaVersionPwBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(ProyectoAplicacionEntregaVersionPwBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TProyectoAplicacionEntregaVersionPw entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<ProyectoAplicacionEntregaVersionPwBO> listadoBO)
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

        public bool Update(ProyectoAplicacionEntregaVersionPwBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TProyectoAplicacionEntregaVersionPw entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<ProyectoAplicacionEntregaVersionPwBO> listadoBO)
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
        private void AsignacionId(TProyectoAplicacionEntregaVersionPw entidad, ProyectoAplicacionEntregaVersionPwBO objetoBO)
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

        private TProyectoAplicacionEntregaVersionPw MapeoEntidad(ProyectoAplicacionEntregaVersionPwBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TProyectoAplicacionEntregaVersionPw entidad = new TProyectoAplicacionEntregaVersionPw();
                entidad = Mapper.Map<ProyectoAplicacionEntregaVersionPwBO, TProyectoAplicacionEntregaVersionPw>(objetoBO,
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

        public List<ProyectoAplicacionEntregadoDetalleDTO> ObtenerEnviarProyectoAplicacionPorAlumno (int IdAlumno)
        {
            try
            {
                string sql_query = "select Id,Nombre,EnlaceProyecto,Version,FechaEnvio,IdMatriculaCabecera,TieneCalificacion from pla.V_ObtenerProyectoAplicacionPorIdAlumno where IdAlumno = @IdAlumno Order by IdMatriculaCabecera, Version asc";
                var query = _dapper.QueryDapper(sql_query, new { IdAlumno = IdAlumno });

                var res = JsonConvert.DeserializeObject<List<ProyectoAplicacionEntregadoDetalleDTO>>(query);

                return res;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

    }
}
