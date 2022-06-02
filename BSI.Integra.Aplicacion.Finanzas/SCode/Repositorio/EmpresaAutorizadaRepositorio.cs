using AutoMapper;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Finanzas.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace BSI.Integra.Aplicacion.Finanzas.Repositorio
{
    public class EmpresaAutorizadaRepositorio : BaseRepository<TEmpresaAutorizada, EmpresaAutorizadaBO>
    {
        #region Metodos Base
        public EmpresaAutorizadaRepositorio() : base()
        {
        }
        public EmpresaAutorizadaRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<EmpresaAutorizadaBO> GetBy(Expression<Func<TEmpresaAutorizada, bool>> filter)
        {
            IEnumerable<TEmpresaAutorizada> listado = base.GetBy(filter);
            List<EmpresaAutorizadaBO> listadoBO = new List<EmpresaAutorizadaBO>();
            foreach (var itemEntidad in listado)
            {
                EmpresaAutorizadaBO objetoBO = Mapper.Map<TEmpresaAutorizada, EmpresaAutorizadaBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public EmpresaAutorizadaBO FirstById(int id)
        {
            try
            {
                TEmpresaAutorizada entidad = base.FirstById(id);
                EmpresaAutorizadaBO objetoBO = new EmpresaAutorizadaBO();
                Mapper.Map<TEmpresaAutorizada, EmpresaAutorizadaBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public EmpresaAutorizadaBO FirstBy(Expression<Func<TEmpresaAutorizada, bool>> filter)
        {
            try
            {
                TEmpresaAutorizada entidad = base.FirstBy(filter);
                EmpresaAutorizadaBO objetoBO = Mapper.Map<TEmpresaAutorizada, EmpresaAutorizadaBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(EmpresaAutorizadaBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TEmpresaAutorizada entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<EmpresaAutorizadaBO> listadoBO)
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

        public bool Update(EmpresaAutorizadaBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TEmpresaAutorizada entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<EmpresaAutorizadaBO> listadoBO)
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
        private void AsignacionId(TEmpresaAutorizada entidad, EmpresaAutorizadaBO objetoBO)
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

        private TEmpresaAutorizada MapeoEntidad(EmpresaAutorizadaBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TEmpresaAutorizada entidad = new TEmpresaAutorizada();
                entidad = Mapper.Map<EmpresaAutorizadaBO, TEmpresaAutorizada>(objetoBO,
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

        public List<FiltroDTO> ObtenerEmpresas() {
            try
            {
                var listaEmpresas = this.GetBy(x => x.Estado == true, x => new FiltroDTO { Id = x.Id, Nombre = x.RazonSocial.ToUpper() }).ToList();
                return listaEmpresas;
            }
            catch (Exception e) {
                throw new Exception(e.Message);
            }
        }

        public List<FiltroBasicoDTO> ObetenerEmpresasAutirizadasPorCiudad(int IdCiudad) {
            try
            {
                List<FiltroBasicoDTO> EmpresasAurizadas = new List<FiltroBasicoDTO>();
                var Query = "SELECT EA.Id as Id, EA.RazonSocial as Nombre from fin.t_EmpresaAutorizada EA inner join conf.T_Pais P on EA.IdPais=P.Id"+
                 " inner join conf.T_Ciudad C on C.IdPais=P.Id where EA.Estado=1 and  c.id="+ IdCiudad +"group by EA.Id, EA.RazonSocial ";

                var ciudadesDB = _dapper.QueryDapper(Query, null);
                EmpresasAurizadas = JsonConvert.DeserializeObject<List<FiltroBasicoDTO>>(ciudadesDB);
                return EmpresasAurizadas;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }

    }
}
