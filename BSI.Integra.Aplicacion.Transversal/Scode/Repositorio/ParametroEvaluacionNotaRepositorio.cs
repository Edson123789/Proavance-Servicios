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
    public class ParametroEvaluacionNotaRepositorio : BaseRepository<TParametroEvaluacionNota, ParametroEvaluacionNotaBO>
    {
        #region Metodos Base
        public ParametroEvaluacionNotaRepositorio() : base()
        {
        }
        public ParametroEvaluacionNotaRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<ParametroEvaluacionNotaBO> GetBy(Expression<Func<TParametroEvaluacionNota, bool>> filter)
        {
            IEnumerable<TParametroEvaluacionNota> listado = base.GetBy(filter);
            List<ParametroEvaluacionNotaBO> listadoBO = new List<ParametroEvaluacionNotaBO>();
            foreach (var itemEntidad in listado)
            {
                ParametroEvaluacionNotaBO objetoBO = Mapper.Map<TParametroEvaluacionNota, ParametroEvaluacionNotaBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public ParametroEvaluacionNotaBO FirstById(int id)
        {
            try
            {
                TParametroEvaluacionNota entidad = base.FirstById(id);
                ParametroEvaluacionNotaBO objetoBO = new ParametroEvaluacionNotaBO();
                Mapper.Map<TParametroEvaluacionNota, ParametroEvaluacionNotaBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public ParametroEvaluacionNotaBO FirstBy(Expression<Func<TParametroEvaluacionNota, bool>> filter)
        {
            try
            {
                TParametroEvaluacionNota entidad = base.FirstBy(filter);
                ParametroEvaluacionNotaBO objetoBO = Mapper.Map<TParametroEvaluacionNota, ParametroEvaluacionNotaBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(ParametroEvaluacionNotaBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TParametroEvaluacionNota entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<ParametroEvaluacionNotaBO> listadoBO)
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

        public bool Update(ParametroEvaluacionNotaBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TParametroEvaluacionNota entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<ParametroEvaluacionNotaBO> listadoBO)
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
        private void AsignacionId(TParametroEvaluacionNota entidad, ParametroEvaluacionNotaBO objetoBO)
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

        private TParametroEvaluacionNota MapeoEntidad(ParametroEvaluacionNotaBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TParametroEvaluacionNota entidad = new TParametroEvaluacionNota();
                entidad = Mapper.Map<ParametroEvaluacionNotaBO, TParametroEvaluacionNota>(objetoBO,
                    opt => opt.ConfigureMap(MemberList.None));

                //mapea los hijos

                return entidad;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public IEnumerable<ParametroEvaluacionNotaBO> GetFiltered<KProperty>(IEnumerable<Expression<Func<TParametroEvaluacionNota, bool>>> filters, Expression<Func<TParametroEvaluacionNota, KProperty>> orderBy, bool ascending)
        {
            IEnumerable<TParametroEvaluacionNota> listado = base.GetFiltered(filters, orderBy, ascending);
            List<ParametroEvaluacionNotaBO> listadoBO = new List<ParametroEvaluacionNotaBO>();

            foreach (var itemEntidad in listado)
            {
                ParametroEvaluacionNotaBO objetoBO = Mapper.Map<TParametroEvaluacionNota, ParametroEvaluacionNotaBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }
            return listadoBO;
        }
        #endregion

        public bool RegistrarNotaPortal(int idTareaEvaluacionTarea, decimal nota)
        {
            try
            {
                var query = "ope.SP_EsquemaEvaluacion_RegistrarCalificacionPortal";
                var res = _dapper.QuerySPFirstOrDefault(query,
                    new
                    {
                        idTareaEvaluacionTarea, nota
                    });
                //return JsonConvert.DeserializeObject<bool>(res);
                return true;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
