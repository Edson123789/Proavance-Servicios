using AutoMapper;
using BSI.Integra.Aplicacion.DTOs.Operaciones;
using BSI.Integra.Aplicacion.Operaciones.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace BSI.Integra.Aplicacion.Operaciones.Repositorio
{
    public class PEspecificoSilaboRepositorio : BaseRepository<TPespecificoSilabo, PEspecificoSilaboBO>
    {
        #region Metodos Base
        public PEspecificoSilaboRepositorio() : base()
        {
        }
        public PEspecificoSilaboRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<PEspecificoSilaboBO> GetBy(Expression<Func<TPespecificoSilabo, bool>> filter)
        {
            IEnumerable<TPespecificoSilabo> listado = base.GetBy(filter);
            List<PEspecificoSilaboBO> listadoBO = new List<PEspecificoSilaboBO>();
            foreach (var itemEntidad in listado)
            {
                PEspecificoSilaboBO objetoBO = Mapper.Map<TPespecificoSilabo, PEspecificoSilaboBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public PEspecificoSilaboBO FirstById(int id)
        {
            try
            {
                TPespecificoSilabo entidad = base.FirstById(id);
                PEspecificoSilaboBO objetoBO = new PEspecificoSilaboBO();
                Mapper.Map<TPespecificoSilabo, PEspecificoSilaboBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public PEspecificoSilaboBO FirstBy(Expression<Func<TPespecificoSilabo, bool>> filter)
        {
            try
            {
                TPespecificoSilabo entidad = base.FirstBy(filter);
                PEspecificoSilaboBO objetoBO = Mapper.Map<TPespecificoSilabo, PEspecificoSilaboBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(PEspecificoSilaboBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TPespecificoSilabo entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<PEspecificoSilaboBO> listadoBO)
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

        public bool Update(PEspecificoSilaboBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TPespecificoSilabo entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<PEspecificoSilaboBO> listadoBO)
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
        private void AsignacionId(TPespecificoSilabo entidad, PEspecificoSilaboBO objetoBO)
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

        private TPespecificoSilabo MapeoEntidad(PEspecificoSilaboBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TPespecificoSilabo entidad = new TPespecificoSilabo();
                entidad = Mapper.Map<PEspecificoSilaboBO, TPespecificoSilabo>(objetoBO,
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

        public IEnumerable<DocentePorGrupoListadoDTO> ListadoPendientePorDocente(int idDocente, string filtro)
        {
            try
            {
                var query = "select * from ope.V_PEspecifico_SilaboPendiente_DocentePorGrupo where idExpositor = @idDocente";
                var res = _dapper.QueryDapper(query, new { idDocente = idDocente });
                return JsonConvert.DeserializeObject<List<DocentePorGrupoListadoDTO>>(res);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public List<DocentePorGrupoListadoDTO> ListadoCcPorDocenteFiltrado(CursoPorDocenteFiltroDTO filtro)
        {
            try
            {
                var query = "pla.SP_ObtenerPEspecifico_Silabo_DocentePorGrupo_Filtrado";
                var res = _dapper.QuerySPDapper(query,
                    new
                    {
                        filtro.IdExpositor,
                        filtro.IdProgramaEspecifico,
                        filtro.IdCentroCosto,
                        filtro.IdCodigoBSCiudad,
                        filtro.IdEstadoPEspecifico,
                        filtro.IdModalidadCurso,
                        filtro.IdPGeneral,
                        filtro.IdArea,
                        filtro.IdSubArea,
                        filtro.IdCentroCostoD
                    });

                return JsonConvert.DeserializeObject<List<DocentePorGrupoListadoDTO>>(res);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
