using AutoMapper;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.DTOs.Scode.DTOs.Operaciones;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace BSI.Integra.Aplicacion.Transversal.Scode.Repositorio
{
    public class MatriculaCabeceraDatosCertificadoMensajesRepositorio : BaseRepository<TMatriculaCabeceraDatosCertificadoMensajes, MatriculaCabeceraDatosCertificadoMensajesBO>
    {
        public MatriculaCabeceraDatosCertificadoMensajesRepositorio() : base()
        {
        }
        public MatriculaCabeceraDatosCertificadoMensajesRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<MatriculaCabeceraDatosCertificadoMensajesBO> GetBy(Expression<Func<TMatriculaCabeceraDatosCertificadoMensajes, bool>> filter)
        {
            IEnumerable<TMatriculaCabeceraDatosCertificadoMensajes> listado = base.GetBy(filter);
            List<MatriculaCabeceraDatosCertificadoMensajesBO> listadoBO = new List<MatriculaCabeceraDatosCertificadoMensajesBO>();
            foreach (var itemEntidad in listado)
            {
                MatriculaCabeceraDatosCertificadoMensajesBO objetoBO = Mapper.Map<TMatriculaCabeceraDatosCertificadoMensajes, MatriculaCabeceraDatosCertificadoMensajesBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public MatriculaCabeceraDatosCertificadoMensajesBO FirstById(int id)
        {
            try
            {
                TMatriculaCabeceraDatosCertificadoMensajes entidad = base.FirstById(id);
                MatriculaCabeceraDatosCertificadoMensajesBO objetoBO = new MatriculaCabeceraDatosCertificadoMensajesBO();
                Mapper.Map<TMatriculaCabeceraDatosCertificadoMensajes, MatriculaCabeceraDatosCertificadoMensajesBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public MatriculaCabeceraDatosCertificadoMensajesBO FirstBy(Expression<Func<TMatriculaCabeceraDatosCertificadoMensajes, bool>> filter)
        {
            try
            {
                TMatriculaCabeceraDatosCertificadoMensajes entidad = base.FirstBy(filter);
                MatriculaCabeceraDatosCertificadoMensajesBO objetoBO = Mapper.Map<TMatriculaCabeceraDatosCertificadoMensajes, MatriculaCabeceraDatosCertificadoMensajesBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(MatriculaCabeceraDatosCertificadoMensajesBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TMatriculaCabeceraDatosCertificadoMensajes entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<MatriculaCabeceraDatosCertificadoMensajesBO> listadoBO)
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

        public bool Update(MatriculaCabeceraDatosCertificadoMensajesBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TMatriculaCabeceraDatosCertificadoMensajes entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<MatriculaCabeceraDatosCertificadoMensajesBO> listadoBO)
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
        private void AsignacionId(TMatriculaCabeceraDatosCertificadoMensajes entidad, MatriculaCabeceraDatosCertificadoMensajesBO objetoBO)
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

        private TMatriculaCabeceraDatosCertificadoMensajes MapeoEntidad(MatriculaCabeceraDatosCertificadoMensajesBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TMatriculaCabeceraDatosCertificadoMensajes entidad = new TMatriculaCabeceraDatosCertificadoMensajes();
                entidad = Mapper.Map<MatriculaCabeceraDatosCertificadoMensajesBO, TMatriculaCabeceraDatosCertificadoMensajes>(objetoBO,
                    opt => opt.ConfigureMap(MemberList.None));

                //mapea los hijos

                return entidad;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Repositorio: MatriculaCabeceraDatosCertificadoMensajesRepositorio
        /// Autor: Miguel Mora
        /// Fecha: 10/09/2021
        /// Version: 1.0
        /// <summary>
        /// Retorna los mensajes pendientes
        /// </summary>
        /// <returns> MatriculaCabeceraDatosCertificadoMensajesDTO </returns>        
        public List<MatriculaCabeceraDatosCertificadoMensajesDTO> ObtenerMensajesPendientes(int IdPersonalReceptor)
        {
            try
            {
                List<MatriculaCabeceraDatosCertificadoMensajesDTO> certificado = new List<MatriculaCabeceraDatosCertificadoMensajesDTO>();
                var query = string.Empty;
                query = "select mccm.*,CONCAT(p.Nombres,' ',p.Apellidos) AS  Remitente ,CONCAT(ptr.Nombres,' ',ptr.Apellidos) AS  Receptor  from ope.T_MatriculaCabeceraDatosCertificadoMensajes as mccm " +
                "INNER JOIN gp.T_Personal as p on mccm.IdPersonalRemitente = p.Id "+
                "INNER JOIN gp.T_Personal as ptr on mccm.IdPersonalReceptor = ptr.Id "+
                "WHERE mccm.EstadoMensaje = 1 AND mccm.Estado=1 AND mccm.IdPersonalReceptor = @IdPersonalReceptor AND mccm.ValorAntiguo <>'-'";
                var cargosDB = _dapper.QueryDapper(query, new { IdPersonalReceptor });
                if (!string.IsNullOrEmpty(cargosDB) && !cargosDB.Contains("[]"))
                {
                    certificado = JsonConvert.DeserializeObject<List<MatriculaCabeceraDatosCertificadoMensajesDTO>>(cargosDB);
                }
                return certificado;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Repositorio: MatriculaCabeceraDatosCertificadoMensajesRepositorio
        /// Autor: Miguel Mora
        /// Fecha: 10/09/2021
        /// Version: 1.0
        /// <summary>
        /// Retorna los mensajes leidos
        /// </summary>
        /// <returns> MatriculaCabeceraDatosCertificadoMensajesDTO </returns>        
        public List<MatriculaCabeceraDatosCertificadoMensajesDTO> ObtenerMensajesLeidos(int IdPersonalReceptor)
        {
            try
            {
                List<MatriculaCabeceraDatosCertificadoMensajesDTO> certificado = new List<MatriculaCabeceraDatosCertificadoMensajesDTO>();
                var query = string.Empty;
                query = "select mccm.*,CONCAT(p.Nombres,' ',p.Apellidos) AS  Remitente ,CONCAT(ptr.Nombres,' ',ptr.Apellidos) AS  Receptor  from ope.T_MatriculaCabeceraDatosCertificadoMensajes as mccm " +
                "INNER JOIN gp.T_Personal as p on mccm.IdPersonalRemitente = p.Id " +
                "INNER JOIN gp.T_Personal as ptr on mccm.IdPersonalReceptor = ptr.Id " +
                "WHERE  mccm.EstadoMensaje = 0 AND mccm.Estado=1 AND mccm.IdPersonalReceptor = @IdPersonalReceptor AND mccm.ValorAntiguo <>'-'";
                var cargosDB = _dapper.QueryDapper(query, new { IdPersonalReceptor });
                if (!string.IsNullOrEmpty(cargosDB) && !cargosDB.Contains("[]"))
                {
                    certificado = JsonConvert.DeserializeObject<List<MatriculaCabeceraDatosCertificadoMensajesDTO>>(cargosDB);
                }
                return certificado;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
