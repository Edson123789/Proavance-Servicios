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
    public class ReprogramacionCabeceraPersonalRepositorio : BaseRepository<TReprogramacionCabeceraPersonal, ReprogramacionCabeceraPersonalBO>
    {
        #region Metodos Base
        public ReprogramacionCabeceraPersonalRepositorio() : base()
        {
        }
        public ReprogramacionCabeceraPersonalRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<ReprogramacionCabeceraPersonalBO> GetBy(Expression<Func<TReprogramacionCabeceraPersonal, bool>> filter)
        {
            IEnumerable<TReprogramacionCabeceraPersonal> listado = base.GetBy(filter);
            List<ReprogramacionCabeceraPersonalBO> listadoBO = new List<ReprogramacionCabeceraPersonalBO>();
            foreach (var itemEntidad in listado)
            {
                ReprogramacionCabeceraPersonalBO objetoBO = Mapper.Map<TReprogramacionCabeceraPersonal, ReprogramacionCabeceraPersonalBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public ReprogramacionCabeceraPersonalBO FirstById(int id)
        {
            try
            {
                TReprogramacionCabeceraPersonal entidad = base.FirstById(id);
                ReprogramacionCabeceraPersonalBO objetoBO = new ReprogramacionCabeceraPersonalBO();
                Mapper.Map<TReprogramacionCabeceraPersonal, ReprogramacionCabeceraPersonalBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public ReprogramacionCabeceraPersonalBO FirstBy(Expression<Func<TReprogramacionCabeceraPersonal, bool>> filter)
        {
            try
            {
                TReprogramacionCabeceraPersonal entidad = base.FirstBy(filter);
                ReprogramacionCabeceraPersonalBO objetoBO = Mapper.Map<TReprogramacionCabeceraPersonal, ReprogramacionCabeceraPersonalBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(ReprogramacionCabeceraPersonalBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TReprogramacionCabeceraPersonal entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<ReprogramacionCabeceraPersonalBO> listadoBO)
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

        public bool Update(ReprogramacionCabeceraPersonalBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TReprogramacionCabeceraPersonal entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<ReprogramacionCabeceraPersonalBO> listadoBO)
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
        private void AsignacionId(TReprogramacionCabeceraPersonal entidad, ReprogramacionCabeceraPersonalBO objetoBO)
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

        private TReprogramacionCabeceraPersonal MapeoEntidad(ReprogramacionCabeceraPersonalBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TReprogramacionCabeceraPersonal entidad = new TReprogramacionCabeceraPersonal();
                entidad = Mapper.Map<ReprogramacionCabeceraPersonalBO, TReprogramacionCabeceraPersonal>(objetoBO,
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

        public ReprogramacionCabeceraPersonalBO ObtenerReprogramacionCabeceraPersonal(int idActividadCabecera, int idCategoriaOrigen, int idPersonal, DateTime fechaReprogramacion)
        {
            try
            {
                string _queryOcurrencia = "Select Id,IdActividadCabecera,IdCategoriaOrigen,ReproDia,IdPersonal,FechaReprogramacion From com.V_TReprogramacionCabeceraPersonal_ObtenerTodo Where Estado=1 and IdActividadCabecera=@IdActividadCabecera and IdCategoriaOrigen=@IdCategoriaOrigen and IdPersonal=@IdPersonal and FechaReprogramacion=@FechaReprogramacion ";
                var queryOcurrencia = _dapper.FirstOrDefault(_queryOcurrencia, new { IdActividadCabecera = idActividadCabecera, IdCategoriaOrigen = idCategoriaOrigen, IdPersonal=idPersonal , FechaReprogramacion = fechaReprogramacion });
                return JsonConvert.DeserializeObject<ReprogramacionCabeceraPersonalBO>(queryOcurrencia);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
