using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using AutoMapper;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using BSI.Integra.Aplicacion.DTOs;
using Newtonsoft.Json;
using System.Linq;

namespace BSI.Integra.Aplicacion.Transversal.Repositorio
{
    public class PreguntaFrecuentePgeneralRepositorio : BaseRepository<TPreguntaFrecuentePgeneral, PreguntaFrecuentePgeneralBO>
    {
        #region Metodos Base
        public PreguntaFrecuentePgeneralRepositorio() : base()
        {
        }
        public PreguntaFrecuentePgeneralRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<PreguntaFrecuentePgeneralBO> GetBy(Expression<Func<TPreguntaFrecuentePgeneral, bool>> filter)
        {
            IEnumerable<TPreguntaFrecuentePgeneral> listado = base.GetBy(filter);
            List<PreguntaFrecuentePgeneralBO> listadoBO = new List<PreguntaFrecuentePgeneralBO>();
            foreach (var itemEntidad in listado)
            {
                PreguntaFrecuentePgeneralBO objetoBO = Mapper.Map<TPreguntaFrecuentePgeneral, PreguntaFrecuentePgeneralBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public PreguntaFrecuentePgeneralBO FirstById(int id)
        {
            try
            {
                TPreguntaFrecuentePgeneral entidad = base.FirstById(id);
                PreguntaFrecuentePgeneralBO objetoBO = new PreguntaFrecuentePgeneralBO();
                Mapper.Map<TPreguntaFrecuentePgeneral, PreguntaFrecuentePgeneralBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public PreguntaFrecuentePgeneralBO FirstBy(Expression<Func<TPreguntaFrecuentePgeneral, bool>> filter)
        {
            try
            {
                TPreguntaFrecuentePgeneral entidad = base.FirstBy(filter);
                PreguntaFrecuentePgeneralBO objetoBO = Mapper.Map<TPreguntaFrecuentePgeneral, PreguntaFrecuentePgeneralBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(PreguntaFrecuentePgeneralBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TPreguntaFrecuentePgeneral entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<PreguntaFrecuentePgeneralBO> listadoBO)
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

        public bool Update(PreguntaFrecuentePgeneralBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TPreguntaFrecuentePgeneral entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<PreguntaFrecuentePgeneralBO> listadoBO)
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
        private void AsignacionId(TPreguntaFrecuentePgeneral entidad, PreguntaFrecuentePgeneralBO objetoBO)
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

        private TPreguntaFrecuentePgeneral MapeoEntidad(PreguntaFrecuentePgeneralBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TPreguntaFrecuentePgeneral entidad = new TPreguntaFrecuentePgeneral();
                entidad = Mapper.Map<PreguntaFrecuentePgeneralBO, TPreguntaFrecuentePgeneral>(objetoBO,
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

        public List<PreguntaFrecuentePgeneralDTO> ObtenerPreguntaFrecuenteCambio(int idPGeneral, int idArea, int idSubArea, int idTipo)
        {
            try
            {
                string _queryPreguntaFrecuente = "select Id,Pregunta,Respuesta,IdSeccion,Nombre From Pla.V_PreguntaFrecuente where (IdPGeneral = @idPGeneral or IdPGeneral is Null) and (IdArea = @idArea or IdArea=1) and (IdSubArea = @idSubArea or IdSubArea=1) and(IdTipo= @idTipo or IdTipo=3) Order by Nombre";
                var _programaPreguntaFrecuente = _dapper.QueryDapper(_queryPreguntaFrecuente, new { IdPGeneral = idPGeneral, IdArea = idArea, IdSubArea = idSubArea, IdTipo = idTipo});
                return JsonConvert.DeserializeObject<List<PreguntaFrecuentePgeneralDTO>>(_programaPreguntaFrecuente);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public List<PreguntaFrecuentePgeneralDTO> ObtenerPreguntaFrecuente(FiltroProgramaByCentroCostoDTO data)
        {
            try
            {
                string _queryPreguntaFrecuente = "select " + data.IdPGeneral + " AS IdPrograma,Id,Pregunta,Respuesta,IdSeccion,Nombre From Pla.V_PreguntaFrecuente where (IdPGeneral = @idPGeneral or IdPGeneral is Null) and (IdArea = @idArea or IdArea=1) and (IdSubArea = @idSubArea or IdSubArea=1) and(IdTipo= @idTipo or IdTipo=3) Order by Nombre";
                var _programaPreguntaFrecuente = _dapper.QueryDapper(_queryPreguntaFrecuente, new { IdPGeneral = data.IdPGeneral, IdArea = data.IdArea, IdSubArea = data.IdSubArea, IdTipo = data.TipoId });

                return JsonConvert.DeserializeObject<List<PreguntaFrecuentePgeneralDTO>>(_programaPreguntaFrecuente);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Elimina (Actualiza estado a false ) todos los programas PreguntaFrecuentePgeneral asociados a una PreguntaFrecuente
        /// </summary>
        /// <returns></returns>
        public void EliminacionLogicoPorPreguntaFrecuente(int idPreguntaFrecuente, string usuario, List<int> nuevos)
        {
            try
            {
                var listaBorrar = GetBy(x => x.IdPreguntaFrecuente == idPreguntaFrecuente && x.Estado == true).ToList();
                listaBorrar.RemoveAll(x => nuevos.Any(y => y == x.IdPgeneral));
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

        /// <summary>
        /// Obtiene todos los registros sin los campos de auditoría.
        /// </summary>
        /// <returns></returns>
        public List<PreguntaFrecuentePgeneralDatosDTO> ObtenerTodoGrid()
        {
            try
            {
                var lista = GetBy(x => true, y => new PreguntaFrecuentePgeneralDatosDTO
                {
                    Id = y.Id,
                    IdPreguntaFrecuente = y.IdPreguntaFrecuente,
                    IdPGeneral = y.IdPgeneral,
                }).OrderByDescending(x => x.Id).ToList();

                return lista;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public List<PreguntaFrecuentePgeneralDTO> ObtenerPreguntaFrecuentePorPrograma(int idPGeneral)
        {
            try
            {
                string _queryPreguntaFrecuente = "select Id,Pregunta,Respuesta,IdSeccion,Nombre From Pla.V_PreguntaFrecuente where (IdPGeneral = @idPGeneral) Group By Id,Pregunta,Respuesta,IdSeccion,Nombre Order by Nombre";
                var _programaPreguntaFrecuente = _dapper.QueryDapper(_queryPreguntaFrecuente, new { IdPGeneral = idPGeneral});
                return JsonConvert.DeserializeObject<List<PreguntaFrecuentePgeneralDTO>>(_programaPreguntaFrecuente);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }

}
   
