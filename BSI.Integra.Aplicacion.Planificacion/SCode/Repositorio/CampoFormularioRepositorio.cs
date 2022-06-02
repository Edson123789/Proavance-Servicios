using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using AutoMapper;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Planificacion.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using Newtonsoft.Json;

namespace BSI.Integra.Aplicacion.Planificacion.Repositorio
{
    public class CampoFormularioRepositorio : BaseRepository<TCampoFormulario, CampoFormularioBO>
    {
        #region Metodos Base
        public CampoFormularioRepositorio() : base()
        {
        }
        public CampoFormularioRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<CampoFormularioBO> GetBy(Expression<Func<TCampoFormulario, bool>> filter)
        {
            IEnumerable<TCampoFormulario> listado = base.GetBy(filter);
            List<CampoFormularioBO> listadoBO = new List<CampoFormularioBO>();
            foreach (var itemEntidad in listado)
            {
                CampoFormularioBO objetoBO = Mapper.Map<TCampoFormulario, CampoFormularioBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public CampoFormularioBO FirstById(int id)
        {
            try
            {
                TCampoFormulario entidad = base.FirstById(id);
                CampoFormularioBO objetoBO = new CampoFormularioBO();
                Mapper.Map<TCampoFormulario, CampoFormularioBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public CampoFormularioBO FirstBy(Expression<Func<TCampoFormulario, bool>> filter)
        {
            try
            {
                TCampoFormulario entidad = base.FirstBy(filter);
                CampoFormularioBO objetoBO = Mapper.Map<TCampoFormulario, CampoFormularioBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(CampoFormularioBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TCampoFormulario entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<CampoFormularioBO> listadoBO)
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

        public bool Update(CampoFormularioBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TCampoFormulario entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<CampoFormularioBO> listadoBO)
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
        private void AsignacionId(TCampoFormulario entidad, CampoFormularioBO objetoBO)
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

        private TCampoFormulario MapeoEntidad(CampoFormularioBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TCampoFormulario entidad = new TCampoFormulario();
                entidad = Mapper.Map<CampoFormularioBO, TCampoFormulario>(objetoBO,
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

        public List<CampoFormularioSeleccionadoDTO> ObtenerCampoFormulario(int idFormularioSolicitud)
        {
            string _queryCampo = "Select Id,IdFormularioSolicitud,IdCampoContacto,NroVisitas,Codigo,Nombre,Siempre,Inteligente,Probabilidad From mkt.V_TCampoFormulario_ObtenerInformacion where Estado=1 and IdFormularioSolicitud=@IdFormularioSolicitud Order by NroVisitas";
            string queryCampo = _dapper.QueryDapper(_queryCampo, new { IdFormularioSolicitud=idFormularioSolicitud});
            return JsonConvert.DeserializeObject<List<CampoFormularioSeleccionadoDTO>>(queryCampo);
        }
    }
}
