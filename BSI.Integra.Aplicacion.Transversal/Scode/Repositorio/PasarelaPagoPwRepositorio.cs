using AutoMapper;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Aplicacion.Transversal.DTO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace BSI.Integra.Aplicacion.Transversal.Repositorio
{
    /// Repositorio: PasarelaPagoPwRepositorio
    /// Autor: Abelson Quiñones
    /// Fecha: 30/09/2021
    /// <summary>
    /// Funciones para administrar el registro, actualizacion y eliminacion de los metodos de pago
    /// </summary>

    public class PasarelaPagoPwRepositorio : BaseRepository<TPasarelaPagoPw, PasarelaPagoPWBO>
    {
        #region Metodos Base
        public PasarelaPagoPwRepositorio() : base()
        {
        }
        public PasarelaPagoPwRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<PasarelaPagoPWBO> GetBy(Expression<Func<TPasarelaPagoPw, bool>> filter)
        {
            IEnumerable<TPasarelaPagoPw> listado = base.GetBy(filter);
            List<PasarelaPagoPWBO> listadoBO = new List<PasarelaPagoPWBO>();
            foreach (var itemEntidad in listado)
            {
                PasarelaPagoPWBO objetoBO = Mapper.Map<TPasarelaPagoPw, PasarelaPagoPWBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public PasarelaPagoPWBO FirstById(int id)
        {
            try
            {
                TPasarelaPagoPw entidad = base.FirstById(id);
                PasarelaPagoPWBO objetoBO = new PasarelaPagoPWBO();
                Mapper.Map<TPasarelaPagoPw, PasarelaPagoPWBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public PasarelaPagoPWBO FirstBy(Expression<Func<TPasarelaPagoPw, bool>> filter)
        {
            try
            {
                TPasarelaPagoPw entidad = base.FirstBy(filter);
                PasarelaPagoPWBO objetoBO = Mapper.Map<TPasarelaPagoPw, PasarelaPagoPWBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(PasarelaPagoPWBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TPasarelaPagoPw entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<PasarelaPagoPWBO> listadoBO)
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

        public bool Update(PasarelaPagoPWBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TPasarelaPagoPw entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<PasarelaPagoPWBO> listadoBO)
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
        private void AsignacionId(TPasarelaPagoPw entidad, PasarelaPagoPWBO objetoBO)
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

        private TPasarelaPagoPw MapeoEntidad(PasarelaPagoPWBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TPasarelaPagoPw entidad = new TPasarelaPagoPw();
                entidad = Mapper.Map<PasarelaPagoPWBO, TPasarelaPagoPw>(objetoBO,
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

        /// Autor: Abelson Quiñones
        /// Fecha: 30/09/2021
        /// Version: 1.0
        /// <summary>
        /// Obtiene los métodos de pago registrados según el valor del estado
        /// </summary>
        /// <returns>List<RegistroPasarelaPagoPWDTO></returns>
        public List<RegistroPasarelaPagoPWDTO> ListaRegistroPasarelaPagoPw(bool Estado)
        {
            try
            {
                List<RegistroPasarelaPagoPWDTO> Registros = new List<RegistroPasarelaPagoPWDTO>();

                string _query = "Select Id, IdProveedor, RazonSocial, Nombre, IdPais, NombrePais, Prioridad From pla.V_ListaRegistroPasarelaPagoPW Where Estado=@Estado";
                var listaRegistros = _dapper.QueryDapper(_query, new { Estado = Estado });

                if (!string.IsNullOrEmpty(listaRegistros) && !listaRegistros.Contains("[]"))
                {
                    Registros = JsonConvert.DeserializeObject<List<RegistroPasarelaPagoPWDTO>>(listaRegistros);
                }

                return Registros;

            }
            catch (Exception e)
            {
                return null;
            }
        }

        /// Autor: Abelson Quiñones
        /// Fecha: 30/09/2021
        /// Version: 1.0
        /// <summary>
        /// Obtiene el método de pago registrado según el id
        /// </summary>
        /// <returns>RegistroPasarelaPagoPWDTO</returns>
        public RegistroPasarelaPagoPWDTO RegistroPasarelaPagoPwPorId(int id)
        {
            try
            {
                RegistroPasarelaPagoPWDTO Registros = new RegistroPasarelaPagoPWDTO();

                string _query = "Select Id, IdProveedor, RazonSocial, Nombre, IdPais, NombrePais, Prioridad From pla.V_ListaRegistroPasarelaPagoPW Where Estado=1 AND Id=@Id";
                var Registro = _dapper.FirstOrDefault(_query, new { Id = id });

                if (!string.IsNullOrEmpty(Registro) && !Registro.Contains("[]"))
                {
                    Registros = JsonConvert.DeserializeObject<RegistroPasarelaPagoPWDTO>(Registro);
                }

                return Registros;

            }
            catch (Exception e)
            {
                return null;
            }
        }

        /// Autor: Abelson Quiñones
        /// Fecha: 30/09/2021
        /// Version: 1.0
        /// <summary>
        /// Obtiene los métodos de pago registrado según el país del idAlumno
        /// </summary>
        /// <returns>List<RegistroPasarelaPagoPWDTO></returns>
        public List<RegistroPasarelaPagoPWDTO> ListaMetodoPagoPorIdAlumno(int idAlumno)
        {
            try
            {
                List<RegistroPasarelaPagoPWDTO> registros = new List<RegistroPasarelaPagoPWDTO>();

                string _query = "select TPP.Id,TPP.Nombre,TPP.IdProveedor,TPP.IdPais,TPP.Prioridad from [pla].[T_PasarelaPago_PW] AS TPP inner join mkt.T_Alumno AS TA ON TPP.idpais = TA.IdCodigoPais where TPP.Estado=1 AND TA.Id=@idAlumno order by TPP.Prioridad asc";
                var listaRegistros = _dapper.QueryDapper(_query, new { idAlumno });

                if (!string.IsNullOrEmpty(listaRegistros) && !listaRegistros.Contains("[]"))
                {
                    registros = JsonConvert.DeserializeObject<List<RegistroPasarelaPagoPWDTO>>(listaRegistros);
                }

                return registros;

            }
            catch (Exception e)
            {
                return null;
            }
        }

        /// Autor: Abelson Quiñones
        /// Fecha: 30/09/2021
        /// Version: 1.0
        /// <summary>
        /// Obtiene el método de pago registrado según el IdMatricula
        /// </summary>
        /// <returns>MedioPagoMatriculaCronogramaDTO</returns>
        public MedioPagoMatriculaCronogramaDTO MedioPagoMatriculaCronogramaPorIdMatricula(int idMatriculaCabecera)
        {
            try
            {
                MedioPagoMatriculaCronogramaDTO registro = new MedioPagoMatriculaCronogramaDTO();

                string _query = "select top 1 * from [fin].[T_MedioPagoMatriculaCronograma] where Estado=1 and Activo=1 and IdMatriculaCabecera=@idMatriculaCabecera";
                var registroQuery = _dapper.FirstOrDefault(_query, new { idMatriculaCabecera });

                if (!string.IsNullOrEmpty(registroQuery) && !registroQuery.Contains("[]"))
                {
                    registro = JsonConvert.DeserializeObject<MedioPagoMatriculaCronogramaDTO>(registroQuery);
                }

                return registro;

            }
            catch (Exception e)
            {
                return null;
            }
        }

        /// Autor: Abelson Quiñones
        /// Fecha: 30/09/2021
        /// Version: 1.0
        /// <summary>
        /// Registra el método de pago seleccionado para la matrícula
        /// </summary>
        /// <returns>RegistroMedioPagoMatriculaCronogramaDTO</returns>
        public RegistroMedioPagoMatriculaCronogramaDTO RegistroMedioPagoMatriculaCronograma(RegistroMedioPagoMatriculaCronogramaDTO medioPagoMatricula)
        {
            try
            {
                string _query = "exec[fin].[SP_InsertarMedioPagoMatriculaCronograma] @IdMatriculaCabecera ="+ medioPagoMatricula.IdMatriculaCabecera+",@IdMedioPago ="+ medioPagoMatricula.IdMedioPago+",@Activo = 1,@UsuarioCreacion = '"+ medioPagoMatricula.Usuario+"',@UsuarioModificacion = '"+ medioPagoMatricula.Usuario+ "'";
                var registros = _dapper.QueryDapper(_query, new {  });

                if (!string.IsNullOrEmpty(registros) && !registros.Contains("[]"))
                {
                    return medioPagoMatricula;
                }
                return medioPagoMatricula;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        /// Autor: Abelson Quiñones
        /// Fecha: 30/09/2021
        /// Version: 1.0
        /// <summary>
        /// Cambia el método de pago registrado para la matrícula
        /// </summary>
        /// <returns>bool</returns>
        public bool DesactivarMedioPagoMatriculaCronogramaPorMatricula(int idMatriculaCabecera)
        {
            try
            {
                string _query = "update  [fin].[T_MedioPagoMatriculaCronograma] set Activo=0 where Estado=1 and Activo=1 and IdMatriculaCabecera=@idMatriculaCabecera;";
                var registros = _dapper.QueryDapper(_query, new { idMatriculaCabecera });

                if (!string.IsNullOrEmpty(registros) && !registros.Contains("[]"))
                {
                    return true;
                }
                return false;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        /// Autor: Abelson Quiñones
        /// Fecha: 30/09/2021
        /// Version: 1.0
        /// <summary>
        /// Obtiene el método de pago registrado según el código de matricula
        /// </summary>
        /// <returns>int</returns>
        public int BuscarIdMatriculaCabeceraPorCodigoMatricula(string codigoMatricula)
        {
            try
            {
                MedioPagoMatriculaCronogramaDTO registro = new MedioPagoMatriculaCronogramaDTO();

                string _query = "select top 1 Id as IdMatriculaCabecera from fin.T_MatriculaCabecera where Estado=1 and  CodigoMatricula=@codigoMatricula";
                var registroQuery = _dapper.FirstOrDefault(_query, new { codigoMatricula });

                if (!string.IsNullOrEmpty(registroQuery) && !registroQuery.Contains("[]"))
                {
                    registro = JsonConvert.DeserializeObject<MedioPagoMatriculaCronogramaDTO>(registroQuery);
                }

                return registro.IdMatriculaCabecera;

            }
            catch (Exception e)
            {
                return 0;
            }
        }

        /// Autor: Abelson Quiñones
        /// Fecha: 06/12/2021
        /// Version: 1.0
        /// <summary>
        /// Obtiene el id de la matriculaCabecera por el alumno y el centro de costo
        /// </summary>
        /// <returns>int</returns>
        public int BuscarIdMatriculaCabeceraPorAlumnoCosto(int IdAlumno, int IdCentroCosto)
        {
            try
            {
                MedioPagoMatriculaCronogramaDTO registro = new MedioPagoMatriculaCronogramaDTO();

                string _query = "  select top 1 TMC.Id as IdMatriculaCabecera from fin.T_MatriculaCabecera AS TMC inner join pla.T_PEspecifico AS TPE ON TPE.IdCentroCosto = @IdCentroCosto where TMC.Estado = 1 and TMC.IdAlumno = @IdAlumno ";
                var registroQuery = _dapper.FirstOrDefault(_query, new { IdCentroCosto,IdAlumno });

                if (!string.IsNullOrEmpty(registroQuery) && !registroQuery.Contains("[]"))
                {
                    registro = JsonConvert.DeserializeObject<MedioPagoMatriculaCronogramaDTO>(registroQuery);
                }

                return registro.IdMatriculaCabecera;

            }
            catch (Exception e)
            {
                return 0;
            }
        }


    }
}
