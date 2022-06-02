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
    public class PgeneralDocumentoPwRepositorio : BaseRepository<TPgeneralDocumentoPw, PgeneralDocumentoPwBO>
    {
        #region Metodos Base
        public PgeneralDocumentoPwRepositorio() : base()
        {
        }
        public PgeneralDocumentoPwRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<PgeneralDocumentoPwBO> GetBy(Expression<Func<TPgeneralDocumentoPw, bool>> filter)
        {
            IEnumerable<TPgeneralDocumentoPw> listado = base.GetBy(filter);
            List<PgeneralDocumentoPwBO> listadoBO = new List<PgeneralDocumentoPwBO>();
            foreach (var itemEntidad in listado)
            {
                PgeneralDocumentoPwBO objetoBO = Mapper.Map<TPgeneralDocumentoPw, PgeneralDocumentoPwBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public PgeneralDocumentoPwBO FirstById(int id)
        {
            try
            {
                TPgeneralDocumentoPw entidad = base.FirstById(id);
                PgeneralDocumentoPwBO objetoBO = new PgeneralDocumentoPwBO();
                Mapper.Map<TPgeneralDocumentoPw, PgeneralDocumentoPwBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public PgeneralDocumentoPwBO FirstBy(Expression<Func<TPgeneralDocumentoPw, bool>> filter)
        {
            try
            {
                TPgeneralDocumentoPw entidad = base.FirstBy(filter);
                PgeneralDocumentoPwBO objetoBO = Mapper.Map<TPgeneralDocumentoPw, PgeneralDocumentoPwBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(PgeneralDocumentoPwBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TPgeneralDocumentoPw entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<PgeneralDocumentoPwBO> listadoBO)
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

        public bool Update(PgeneralDocumentoPwBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TPgeneralDocumentoPw entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<PgeneralDocumentoPwBO> listadoBO)
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
        private void AsignacionId(TPgeneralDocumentoPw entidad, PgeneralDocumentoPwBO objetoBO)
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

        private TPgeneralDocumentoPw MapeoEntidad(PgeneralDocumentoPwBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TPgeneralDocumentoPw entidad = new TPgeneralDocumentoPw();
                entidad = Mapper.Map<PgeneralDocumentoPwBO, TPgeneralDocumentoPw>(objetoBO,
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
        /// <summary>
        /// Elimina (Actualiza estado a false ) todos los documentos asociados a un programa
        /// </summary>
        /// <param name="idPGeneral"></param>
        /// <returns></returns>
        public List<int> EliminacionLogicoPorPrograma(int idPGeneral, string usuario, List<DocumentoAsociadoDTO> nuevos)
        {
            try
            {
                var listaBorrar = GetBy(x => x.IdPgeneral == idPGeneral && x.Estado == true).ToList();
                listaBorrar.RemoveAll(x => nuevos.Any(y => y.IdPGeneralDocumento == x.Id));
                foreach (var item in listaBorrar)
                {
                    Delete(item.Id, usuario);
                }
                List<int> result = new List<int>();
                result = listaBorrar.Select(x => x.IdDocumento).ToList();
                return result;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public PgeneralDocumentoPwBO DocumentoSilabov2(int? idProgramaGeneral)
        {
            try
            {
                var query = @"SELECT TOP 1 T_PGeneralDocumento_PW.* FROM pla.T_PGeneralDocumento_PW 
                    JOIN pla.T_Documento_PW ON T_Documento_PW.Id = T_PGeneralDocumento_PW.IdDocumento
                    WHERE IdPlantillaPW = 10 AND IdPGeneral = @idProgramaGeneral";
                var res = _dapper.FirstOrDefault(query,
                    new
                    {
                        idProgramaGeneral = idProgramaGeneral
                    });

                return JsonConvert.DeserializeObject<PgeneralDocumentoPwBO>(res);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
