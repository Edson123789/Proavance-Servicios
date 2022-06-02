using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using AutoMapper;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Finanzas.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using Newtonsoft.Json;

namespace BSI.Integra.Aplicacion.Finanzas.Repositorio
{
    public class OrigenIngresoCajaRepositorio : BaseRepository<TOrigenIngresoCaja, OrigenIngresoCajaBO>
    {
        #region Metodos Base
        public OrigenIngresoCajaRepositorio() : base()
        {
        }
        public OrigenIngresoCajaRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<OrigenIngresoCajaBO> GetBy(Expression<Func<TOrigenIngresoCaja, bool>> filter)
        {
            IEnumerable<TOrigenIngresoCaja> listado = base.GetBy(filter);
            List<OrigenIngresoCajaBO> listadoBO = new List<OrigenIngresoCajaBO>();
            foreach (var itemEntidad in listado)
            {
                OrigenIngresoCajaBO objetoBO = Mapper.Map<TOrigenIngresoCaja, OrigenIngresoCajaBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public OrigenIngresoCajaBO FirstById(int id)
        {
            try
            {
                TOrigenIngresoCaja entidad = base.FirstById(id);
                OrigenIngresoCajaBO objetoBO = new OrigenIngresoCajaBO();
                Mapper.Map<TOrigenIngresoCaja, OrigenIngresoCajaBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public OrigenIngresoCajaBO FirstBy(Expression<Func<TOrigenIngresoCaja, bool>> filter)
        {
            try
            {
                TOrigenIngresoCaja entidad = base.FirstBy(filter);
                OrigenIngresoCajaBO objetoBO = Mapper.Map<TOrigenIngresoCaja, OrigenIngresoCajaBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(OrigenIngresoCajaBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TOrigenIngresoCaja entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<OrigenIngresoCajaBO> listadoBO)
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

        public bool Update(OrigenIngresoCajaBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TOrigenIngresoCaja entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<OrigenIngresoCajaBO> listadoBO)
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
        private void AsignacionId(TOrigenIngresoCaja entidad, OrigenIngresoCajaBO objetoBO)
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

        private TOrigenIngresoCaja MapeoEntidad(OrigenIngresoCajaBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TOrigenIngresoCaja entidad = new TOrigenIngresoCaja();
                entidad = Mapper.Map<OrigenIngresoCajaBO, TOrigenIngresoCaja>(objetoBO,
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
        public List<FiltroDTO> ObtenerOrigen() {
            try
            {                
                List<FiltroDTO> listaOrigenIngreso = this.GetBy(x => x.Estado == true, x => new FiltroDTO { Id = x.Id, Nombre = x.Nombre.ToUpper() }).ToList();
                return listaOrigenIngreso;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
