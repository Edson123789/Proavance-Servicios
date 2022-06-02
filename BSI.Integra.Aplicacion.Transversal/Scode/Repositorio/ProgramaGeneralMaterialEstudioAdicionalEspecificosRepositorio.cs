using AutoMapper;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace BSI.Integra.Aplicacion.Transversal.Repositorio
{
    public class ProgramaGeneralMaterialEstudioAdicionalEspecificosRepositorio : BaseRepository<TProgramaGeneralMaterialEstudioAdicionalEspecificos, ProgramaGeneralMaterialEstudioAdicionalEspecificosBO>
    {
        #region Metodos Base
        public ProgramaGeneralMaterialEstudioAdicionalEspecificosRepositorio() : base()
        {
        }
        public ProgramaGeneralMaterialEstudioAdicionalEspecificosRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<ProgramaGeneralMaterialEstudioAdicionalEspecificosBO> GetBy(Expression<Func<TProgramaGeneralMaterialEstudioAdicionalEspecificos, bool>> filter)
        {
            IEnumerable<TProgramaGeneralMaterialEstudioAdicionalEspecificos> listado = base.GetBy(filter);
            List<ProgramaGeneralMaterialEstudioAdicionalEspecificosBO> listadoBO = new List<ProgramaGeneralMaterialEstudioAdicionalEspecificosBO>();
            foreach (var itemEntidad in listado)
            {
                ProgramaGeneralMaterialEstudioAdicionalEspecificosBO objetoBO = Mapper.Map<TProgramaGeneralMaterialEstudioAdicionalEspecificos, ProgramaGeneralMaterialEstudioAdicionalEspecificosBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public ProgramaGeneralMaterialEstudioAdicionalEspecificosBO FirstById(int id)
        {
            try
            {
                TProgramaGeneralMaterialEstudioAdicionalEspecificos entidad = base.FirstById(id);
                ProgramaGeneralMaterialEstudioAdicionalEspecificosBO objetoBO = new ProgramaGeneralMaterialEstudioAdicionalEspecificosBO();
                Mapper.Map<TProgramaGeneralMaterialEstudioAdicionalEspecificos, ProgramaGeneralMaterialEstudioAdicionalEspecificosBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public ProgramaGeneralMaterialEstudioAdicionalEspecificosBO FirstBy(Expression<Func<TProgramaGeneralMaterialEstudioAdicionalEspecificos, bool>> filter)
        {
            try
            {
                TProgramaGeneralMaterialEstudioAdicionalEspecificos entidad = base.FirstBy(filter);
                ProgramaGeneralMaterialEstudioAdicionalEspecificosBO objetoBO = Mapper.Map<TProgramaGeneralMaterialEstudioAdicionalEspecificos, ProgramaGeneralMaterialEstudioAdicionalEspecificosBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(ProgramaGeneralMaterialEstudioAdicionalEspecificosBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TProgramaGeneralMaterialEstudioAdicionalEspecificos entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<ProgramaGeneralMaterialEstudioAdicionalEspecificosBO> listadoBO)
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

        public bool Update(ProgramaGeneralMaterialEstudioAdicionalEspecificosBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TProgramaGeneralMaterialEstudioAdicionalEspecificos entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<ProgramaGeneralMaterialEstudioAdicionalEspecificosBO> listadoBO)
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
        private void AsignacionId(TProgramaGeneralMaterialEstudioAdicionalEspecificos entidad, ProgramaGeneralMaterialEstudioAdicionalEspecificosBO objetoBO)
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

        private TProgramaGeneralMaterialEstudioAdicionalEspecificos MapeoEntidad(ProgramaGeneralMaterialEstudioAdicionalEspecificosBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TProgramaGeneralMaterialEstudioAdicionalEspecificos entidad = new TProgramaGeneralMaterialEstudioAdicionalEspecificos();
                entidad = Mapper.Map<ProgramaGeneralMaterialEstudioAdicionalEspecificosBO, TProgramaGeneralMaterialEstudioAdicionalEspecificos>(objetoBO,
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

        public List<RegistroProgramaGeneralMaterialEstudioAdicionalEspecificosBO> RegistroProgramaGeneralMaterialEstudioAdicional(int IdProgramaGeneralMaterialEstudioAdicional)
        {
            try
            {
                List<RegistroProgramaGeneralMaterialEstudioAdicionalEspecificosBO> capitulosFiltro = new List<RegistroProgramaGeneralMaterialEstudioAdicionalEspecificosBO>();
                //Select IdPGeneral, Nombre, Titulo, Contenido, NombreTitulo, NroCapitulo From pla.V_ListadoEstructuraProgramaCapitulosV2 Where IdPGeneral = @IdPGeneral
                var _queryfiltrocapitulo = "Select IdPEspecifico FROM pla.T_ProgramaGeneralMaterialEstudioAdicionalEspecificos Where Estado = 1 AND MaterialEstudioAdicionalPorPGeneralId=@IdProgramaGeneralMaterialEstudioAdicional";
                var SubfiltroCapitulo = _dapper.QueryDapper(_queryfiltrocapitulo, new { IdProgramaGeneralMaterialEstudioAdicional = IdProgramaGeneralMaterialEstudioAdicional });
                if (!string.IsNullOrEmpty(SubfiltroCapitulo) && !SubfiltroCapitulo.Contains("[]"))
                {
                    capitulosFiltro = JsonConvert.DeserializeObject<List<RegistroProgramaGeneralMaterialEstudioAdicionalEspecificosBO>>(SubfiltroCapitulo);
                }
                return capitulosFiltro;
            }
            catch (Exception E)
            {
                throw new Exception(E.Message);
            }

        }
    }
}
