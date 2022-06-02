using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using AutoMapper;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using Newtonsoft.Json;

namespace BSI.Integra.Aplicacion.Transversal.Repositorio
{
    public class FeriadoRepositorio : BaseRepository<TFeriado, FeriadoBO>
    {
        #region Metodos Base
        public FeriadoRepositorio() : base()
        {
        }
        public FeriadoRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<FeriadoBO> GetBy(Expression<Func<TFeriado, bool>> filter)
        {
            IEnumerable<TFeriado> listado = base.GetBy(filter);
            List<FeriadoBO> listadoBO = new List<FeriadoBO>();
            foreach (var itemEntidad in listado)
            {
                FeriadoBO objetoBO = Mapper.Map<TFeriado, FeriadoBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }

        public IEnumerable<FeriadoBO> GetAll()
        {
            IEnumerable<TFeriado> listado = base.GetAll();
            List<FeriadoBO> listadoBO = new List<FeriadoBO>();
            foreach (var itemEntidad in listado)
            {
                FeriadoBO objetoBO = Mapper.Map<TFeriado, FeriadoBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }
            return listadoBO;
        }

        public FeriadoBO FirstById(int id)
        {
            try
            {
                TFeriado entidad = base.FirstById(id);
                FeriadoBO objetoBO = new FeriadoBO();
                Mapper.Map<TFeriado, FeriadoBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public FeriadoBO FirstBy(Expression<Func<TFeriado, bool>> filter)
        {
            try
            {
                TFeriado entidad = base.FirstBy(filter);
                FeriadoBO objetoBO = Mapper.Map<TFeriado, FeriadoBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(FeriadoBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TFeriado entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<FeriadoBO> listadoBO)
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

        public bool Update(FeriadoBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TFeriado entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<FeriadoBO> listadoBO)
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
        private void AsignacionId(TFeriado entidad, FeriadoBO objetoBO)
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

        private TFeriado MapeoEntidad(FeriadoBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TFeriado entidad = new TFeriado();
                entidad = Mapper.Map<FeriadoBO, TFeriado>(objetoBO,
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

        public List<FeriadoBO> ObtenerFeriado(DatosFeriadoDTO entidadDTO)
        {
            List<FeriadoBO> feriadoPrimero = new List<FeriadoBO>();

            if (entidadDTO.Frecuencia == 0)
            {
                var feriado = GetBy(x => x.IdTroncalCiudad == entidadDTO.IdCiudad && x.Tipo == 0 && x.Dia == entidadDTO.Dia).ToList();
                return feriado;
            }
            else if (entidadDTO.Frecuencia == 1)
            {
                var feriado2 = GetBy(x => x.IdTroncalCiudad == entidadDTO.IdCiudad && x.Tipo == 0 && x.Dia.Month == entidadDTO.Dia.Month && x.Dia.Day == entidadDTO.Dia.Day).ToList();
                return feriado2;
            }

            return (feriadoPrimero);

        }

        public List<FeriadoBO> ObtenerFeriadoDiaCiudad(int month, int year, int ciudadId)
        {
            try
            {
                var feriado = GetBy(x => x.IdTroncalCiudad == ciudadId && x.Dia.Month == month).ToList();
                var feriado2 = feriado.Where(x =>
                {
                    if ( x.Frecuencia == 1 && x.Dia.Year != year)
                       return false;
                     return true;
                }).ToList();

                foreach (var item in feriado2)
                {
                    item.Dia = new DateTime(year, item.Dia.Month, item.Dia.Day);
                }
                return feriado2;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public List<DatosFeriadoDTO> ObtenerFeriadoPorTipo(int tipo)
        {
            try
            {
                string _queryFeriado = "Select Id,Dia,Tipo,Motivo,Frecuencia,IdCiudad From pla.V_TFeriadoPorId Where Estado=1 and Tipo=@Tipo";
                var queryFeriado = _dapper.QueryDapper(_queryFeriado, new { Tipo = tipo });
                return JsonConvert.DeserializeObject<List<DatosFeriadoDTO>>(queryFeriado);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            
        }
    }
}
