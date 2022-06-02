using AutoMapper;
using BSI.Integra.Aplicacion.DTOs;
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
    public class MatriculaCabeceraDatosCertificadoRepositorio : BaseRepository<TMatriculaCabeceraDatosCertificado, MatriculaCabeceraDatosCertificadoBO>
    {
        #region Metodos Base
        public MatriculaCabeceraDatosCertificadoRepositorio() : base()
        {
        }
        public MatriculaCabeceraDatosCertificadoRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<MatriculaCabeceraDatosCertificadoBO> GetBy(Expression<Func<TMatriculaCabeceraDatosCertificado, bool>> filter)
        {
            IEnumerable<TMatriculaCabeceraDatosCertificado> listado = base.GetBy(filter);
            List<MatriculaCabeceraDatosCertificadoBO> listadoBO = new List<MatriculaCabeceraDatosCertificadoBO>();
            foreach (var itemEntidad in listado)
            {
                MatriculaCabeceraDatosCertificadoBO objetoBO = Mapper.Map<TMatriculaCabeceraDatosCertificado, MatriculaCabeceraDatosCertificadoBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public MatriculaCabeceraDatosCertificadoBO FirstById(int id)
        {
            try
            {
                TMatriculaCabeceraDatosCertificado entidad = base.FirstById(id);
                MatriculaCabeceraDatosCertificadoBO objetoBO = new MatriculaCabeceraDatosCertificadoBO();
                Mapper.Map<TMatriculaCabeceraDatosCertificado, MatriculaCabeceraDatosCertificadoBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public MatriculaCabeceraDatosCertificadoBO FirstBy(Expression<Func<TMatriculaCabeceraDatosCertificado, bool>> filter)
        {
            try
            {
                TMatriculaCabeceraDatosCertificado entidad = base.FirstBy(filter);
                MatriculaCabeceraDatosCertificadoBO objetoBO = Mapper.Map<TMatriculaCabeceraDatosCertificado, MatriculaCabeceraDatosCertificadoBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(MatriculaCabeceraDatosCertificadoBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TMatriculaCabeceraDatosCertificado entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<MatriculaCabeceraDatosCertificadoBO> listadoBO)
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

        public bool Update(MatriculaCabeceraDatosCertificadoBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TMatriculaCabeceraDatosCertificado entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<MatriculaCabeceraDatosCertificadoBO> listadoBO)
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
        private void AsignacionId(TMatriculaCabeceraDatosCertificado entidad, MatriculaCabeceraDatosCertificadoBO objetoBO)
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

        private TMatriculaCabeceraDatosCertificado MapeoEntidad(MatriculaCabeceraDatosCertificadoBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TMatriculaCabeceraDatosCertificado entidad = new TMatriculaCabeceraDatosCertificado();
                entidad = Mapper.Map<MatriculaCabeceraDatosCertificadoBO, TMatriculaCabeceraDatosCertificado>(objetoBO,
                    opt => opt.ConfigureMap(MemberList.None));

                //mapea los hijos

                return entidad;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Repositorio: MatriculaCabeceraDatosCertificadoRepositorio
        /// Autor: Miguel Mora
        /// Fecha: 10/09/2021
        /// Version: 1.0
        /// <summary>
        /// Retorna el registro del certificado actual
        /// </summary>
        /// <returns> MatriculaCabeceraDatosCertificadoDTO </returns>        
        public List<MatriculaCabeceraDatosCertificadoDTO> ObtenerDatosCertificadoPorMatricula(int IdMatriculaCabecera)
        {
            try
            {
                List<MatriculaCabeceraDatosCertificadoDTO> certificado = new List<MatriculaCabeceraDatosCertificadoDTO>();
                var query = string.Empty;
                query = "SELECT Id,IdMatriculaCabecera,Duracion,FechaInicio,FechaFinal,NombreCurso,EstadoCambioDatos, UsuarioCreacion AS Usuario,'' AS Mensaje FROM fin.T_MatriculaCabeceraDatosCertificado WHERE  Estado = 1 AND  IdMatriculaCabecera=@IdMatriculaCabecera AND  EstadoCambioDatos=0";
                var cargosDB = _dapper.QueryDapper(query, new { IdMatriculaCabecera });
                if (!string.IsNullOrEmpty(cargosDB) && !cargosDB.Contains("[]"))
                {
                    certificado = JsonConvert.DeserializeObject< List<MatriculaCabeceraDatosCertificadoDTO>>(cargosDB);
                }
                return certificado;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Repositorio: MatriculaCabeceraDatosCertificadoRepositorio
        /// Autor: Miguel Mora
        /// Fecha: 10/09/2021
        /// Version: 1.0
        /// <summary>
        /// traforma una cadena de fecha en detetime
        /// </summary>
        /// <returns> DateTime </returns>        
        public DateTime TranformarCadenaEnFecha(string fecha)
        {
            try
            {
                DateTime FechaMod = new DateTime();
                string[] dates = fecha.Split(' ');
                string mes = "00";
                switch (dates[2].ToUpper())
                {
                    case "ENERO":
                        mes = "01";
                        break;
                    case "FEBRERO":
                        mes = "02";
                        break;
                    case "MARZO":
                        mes = "03";
                        break;
                    case "ABRIL":
                        mes = "04";
                        break;
                    case "MAYO":
                        mes = "05";
                        break;
                    case "JUNIO":
                        mes = "06";
                        break;
                    case "JULIO":
                        mes = "07";
                        break;
                    case "AGOSTO":
                        mes = "08";
                        break;
                    case "SEPTIEMBRE":
                        mes = "09";
                        break;
                    case "SETIEMBRE":
                        mes = "09";
                        break;
                    case "OCTUBRE":
                        mes = "10";
                        break;
                    case "NOVIEMBRE":
                        mes = "11";
                        break;
                    case "DICIEMBRE":
                        mes = "12";
                        break;
                }
                FechaMod = DateTime.Parse(dates[4] + "-" + mes + "-" + dates[0]);
                return FechaMod;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Repositorio: MatriculaCabeceraDatosCertificadoRepositorio
        /// Autor: Miguel Mora
        /// Fecha: 10/09/2021
        /// Version: 1.0
        /// <summary>
        /// traforma una fecha en cadena
        /// </summary>
        /// <returns> string </returns>        
        public string TranformarFechaEnCadena(DateTime fecha)
        {
            try
            {
                string dia = fecha.ToString("dd");
                string mes = fecha.ToString("MM");
                string year = fecha.ToString("yyyy");
                string mesName = "";
                string FechaMod = "";
                switch (mes)
                {
                    case "01":
                        mesName = "Enero";
                        break;
                    case "02":
                        mesName = "Febrero";
                        break;
                    case "03":
                        mesName = "Marzo";
                        break;
                    case "04":
                        mesName = "Abril";
                        break;
                    case "05":
                        mesName = "Mayo";
                        break;
                    case "06":
                        mesName = "Junio";
                        break;
                    case "07":
                        mesName = "Julio";
                        break;
                    case "08":
                        mesName = "Agosto";
                        break;
                    case "09":
                        mesName = "Setiembre";
                        break;
                    case "10":
                        mesName = "Octubre";
                        break;
                    case "11":
                        mesName = "Noviembre";
                        break;
                    case "12":
                        mesName = "Diciembre";
                        break;
                }
                FechaMod = dia + " de " + mesName + " del " + year;
                return FechaMod;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        #endregion
    }
}
