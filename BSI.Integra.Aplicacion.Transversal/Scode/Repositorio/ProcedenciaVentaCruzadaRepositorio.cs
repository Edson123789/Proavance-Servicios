using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using AutoMapper;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using Newtonsoft.Json;

namespace BSI.Integra.Aplicacion.Transversal.Repositorio
{
    public class ProcedenciaVentaCruzadaRepositorio : BaseRepository<TProcedenciaVentaCruzada, ProcedenciaVentaCruzadaBO>
    {
        #region Metodos Base
        public ProcedenciaVentaCruzadaRepositorio() : base()
        {
        }
        public ProcedenciaVentaCruzadaRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<ProcedenciaVentaCruzadaBO> GetBy(Expression<Func<TProcedenciaVentaCruzada, bool>> filter)
        {
            IEnumerable<TProcedenciaVentaCruzada> listado = base.GetBy(filter);
            List<ProcedenciaVentaCruzadaBO> listadoBO = new List<ProcedenciaVentaCruzadaBO>();
            foreach (var itemEntidad in listado)
            {
                ProcedenciaVentaCruzadaBO objetoBO = Mapper.Map<TProcedenciaVentaCruzada, ProcedenciaVentaCruzadaBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public ProcedenciaVentaCruzadaBO FirstById(int id)
        {
            try
            {
                TProcedenciaVentaCruzada entidad = base.FirstById(id);
                ProcedenciaVentaCruzadaBO objetoBO = new ProcedenciaVentaCruzadaBO();
                Mapper.Map<TProcedenciaVentaCruzada, ProcedenciaVentaCruzadaBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public ProcedenciaVentaCruzadaBO FirstBy(Expression<Func<TProcedenciaVentaCruzada, bool>> filter)
        {
            try
            {
                TProcedenciaVentaCruzada entidad = base.FirstBy(filter);
                ProcedenciaVentaCruzadaBO objetoBO = Mapper.Map<TProcedenciaVentaCruzada, ProcedenciaVentaCruzadaBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(ProcedenciaVentaCruzadaBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TProcedenciaVentaCruzada entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<ProcedenciaVentaCruzadaBO> listadoBO)
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

        public bool Update(ProcedenciaVentaCruzadaBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TProcedenciaVentaCruzada entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<ProcedenciaVentaCruzadaBO> listadoBO)
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
        private void AsignacionId(TProcedenciaVentaCruzada entidad, ProcedenciaVentaCruzadaBO objetoBO)
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

        private TProcedenciaVentaCruzada MapeoEntidad(ProcedenciaVentaCruzadaBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TProcedenciaVentaCruzada entidad = new TProcedenciaVentaCruzada();
                entidad = Mapper.Map<ProcedenciaVentaCruzadaBO, TProcedenciaVentaCruzada>(objetoBO,
                    opt => opt.ConfigureMap(MemberList.None));

                //mapea los hijos

                return entidad;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public IEnumerable<ProcedenciaVentaCruzadaBO> GetFiltered<KProperty>(IEnumerable<Expression<Func<TProcedenciaVentaCruzada, bool>>> filters, Expression<Func<TProcedenciaVentaCruzada, KProperty>> orderBy, bool ascending)
        {
            IEnumerable<TProcedenciaVentaCruzada> listado = base.GetFiltered(filters, orderBy, ascending);
            List<ProcedenciaVentaCruzadaBO> listadoBO = new List<ProcedenciaVentaCruzadaBO>();

            foreach (var itemEntidad in listado)
            {
                ProcedenciaVentaCruzadaBO objetoBO = Mapper.Map<TProcedenciaVentaCruzada, ProcedenciaVentaCruzadaBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }
            return listadoBO;
        }
        #endregion
        public bool InsertarProcedenciaVentaCruzada(int IdOportunidadActual, int IdOportunidadNueva)
        {
            try
            {
                string _queryInsert = "com.SP_TProcedenciaVentaCruzada_Insertar";
                var queryInsert = _dapper.QuerySPFirstOrDefault(_queryInsert, new { idOportunidadActual = IdOportunidadActual, idoportunidadNuevo = IdOportunidadNueva });
                JsonConvert.DeserializeObject<Dictionary<string,int>>(queryInsert);

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
