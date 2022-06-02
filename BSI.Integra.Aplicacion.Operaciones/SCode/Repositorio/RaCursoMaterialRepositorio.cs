using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using AutoMapper;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Operaciones.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using Newtonsoft.Json;

namespace BSI.Integra.Aplicacion.Operaciones.Repositorio
{
    public class RaCursoMaterialRepositorio : BaseRepository<TRaCursoMaterial, RaCursoMaterialBO>
    {
        #region Metodos Base
        public RaCursoMaterialRepositorio() : base()
        {
        }
        public RaCursoMaterialRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<RaCursoMaterialBO> GetBy(Expression<Func<TRaCursoMaterial, bool>> filter)
        {
            IEnumerable<TRaCursoMaterial> listado = base.GetBy(filter);
            List<RaCursoMaterialBO> listadoBO = new List<RaCursoMaterialBO>();
            foreach (var itemEntidad in listado)
            {
                RaCursoMaterialBO objetoBO = Mapper.Map<TRaCursoMaterial, RaCursoMaterialBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public RaCursoMaterialBO FirstById(int id)
        {
            try
            {
                TRaCursoMaterial entidad = base.FirstById(id);
                RaCursoMaterialBO objetoBO = new RaCursoMaterialBO();
                Mapper.Map<TRaCursoMaterial, RaCursoMaterialBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public RaCursoMaterialBO FirstBy(Expression<Func<TRaCursoMaterial, bool>> filter)
        {
            try
            {
                TRaCursoMaterial entidad = base.FirstBy(filter);
                RaCursoMaterialBO objetoBO = Mapper.Map<TRaCursoMaterial, RaCursoMaterialBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(RaCursoMaterialBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TRaCursoMaterial entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<RaCursoMaterialBO> listadoBO)
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

        public bool Update(RaCursoMaterialBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TRaCursoMaterial entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<RaCursoMaterialBO> listadoBO)
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
        private void AsignacionId(TRaCursoMaterial entidad, RaCursoMaterialBO objetoBO)
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

        private TRaCursoMaterial MapeoEntidad(RaCursoMaterialBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TRaCursoMaterial entidad = new TRaCursoMaterial();
                entidad = Mapper.Map<RaCursoMaterialBO, TRaCursoMaterial>(objetoBO,
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
        /// Obtiene el listado minimo de materialCurso por curso
        /// </summary>
        /// <param name="idRaCurso"></param>
        /// <returns></returns>
        public List<MaterialPresencialMininimoDTO> ObtenerListadoMinimoPorCurso(int idRaCurso)
        {
            try
            {
                List<MaterialPresencialMininimoDTO> materialPresencial = new List<MaterialPresencialMininimoDTO>();
                var query = "SELECT Id, NombreArchivo, Grupo, TipoCursoMaterial, Cantidad, CursoMaterialEstado, FechaSubida, ComentarioSubidaArchivo  FROM ope.V_ObtenerListadoMinimoCursoMaterial WHERE IdRaCurso = @idRaCurso";
                var materialPresencialDB = _dapper.QueryDapper(query, new { idRaCurso });
                if (!string.IsNullOrEmpty(materialPresencialDB) && !materialPresencialDB.Contains("[]"))
                {
                    materialPresencial = JsonConvert.DeserializeObject<List<MaterialPresencialMininimoDTO>>(materialPresencialDB);
                }
                return materialPresencial;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
