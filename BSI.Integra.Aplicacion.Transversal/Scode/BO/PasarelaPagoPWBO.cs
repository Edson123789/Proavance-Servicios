using BSI.Integra.Aplicacion.Base.BO;
using BSI.Integra.Aplicacion.Transversal.DTO;
using BSI.Integra.Aplicacion.Transversal.Repositorio;
using BSI.Integra.Persistencia.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.Transversal.BO
{
    /// BO: PasarelaPagoPWBO
    /// Autor: Abelson Quiñones
    /// Fecha: 30/09/2021
    /// <summary>
    /// Funciones para validar y ejecutar los metodos del repositorio PasarelaPagoPwRepositorio
    /// </summary>

    public class PasarelaPagoPWBO : BaseBO
    {
        /// Propiedades	Significado
        /// -----------	------------
        /// Nombre		    Nombre del metodo de pago
        /// IdProveedor		Id del proveedor del medio de pago
        /// IdPais		    Id del paid del medio de pago
        /// Prioridad		Valor de la prioridad del metodod e pago
        public string Nombre { get; set; }
        public int IdProveedor { get; set; }
        public int IdPais { get; set; }
        public int Prioridad { get; set; }

        public ErrorGuiaBO Excepcion { get; set; }

        private readonly integraDBContext _integraDBContext;

        public PasarelaPagoPWBO()
        {
            _integraDBContext = new integraDBContext();
        }

        /// Autor: Abelson Quiñones
        /// Fecha: 30/09/2021
        /// Version: 1.0
        /// <summary>
        /// Registar el metodo de pago
        /// </summary>
        /// <returns>no retorna nada</returns>
        public void RegistrarPasarelaPagoPw()
        {
            try
            {

                PasarelaPagoPwRepositorio _repObjeto = new PasarelaPagoPwRepositorio(_integraDBContext);

                var selectPrioridad = _repObjeto.FirstBy(x => x.IdPais == IdPais && x.Prioridad == Prioridad);
                if (selectPrioridad == null)
                {

                    var respuesta = _repObjeto.Insert(this);

                    if (respuesta)
                    {
                        Excepcion = new ErrorGuiaBO();
                        Excepcion.Error = false;
                    }
                    else
                    {
                        Excepcion = new ErrorGuiaBO();
                        Excepcion.Error = true;
                        Excepcion.Message = "No se puedo registrar el dato";
                        Excepcion.Source = "";
                        Excepcion.Descripcion = "No se puedo registrar el dato";
                    }
                }
                else
                {
                    Excepcion = new ErrorGuiaBO();
                    Excepcion.Error = true;
                    Excepcion.Message = "Error, el país tiene una prioridad ya configurada, seleccione otra.";
                }

            }
            catch (Exception ex)
            {
                Excepcion = new ErrorGuiaBO();
                Excepcion.Error = true;

                if (ex.InnerException == null)
                {
                    Excepcion.InnerException = "";
                }
                else
                {
                    Excepcion.InnerException = ex.InnerException.ToString();
                }

                Excepcion.Message = ex.Message;
                Excepcion.Source = ex.Source;
                Excepcion.Descripcion = ex.ToString();

            }
        }

        /// Autor: Abelson Quiñones
        /// Fecha: 30/09/2021
        /// Version: 1.0
        /// <summary>
        /// Actualizar el metodo de pago
        /// </summary>
        /// <returns>no retorna nada</returns>
        public void ActualizarPasarelaPagoPw()
        {
            try
            {
                PasarelaPagoPwRepositorio _repObjeto = new PasarelaPagoPwRepositorio(_integraDBContext);
                
                var selectPrioridad = _repObjeto.FirstBy(x => x.IdPais == IdPais && x.Prioridad == Prioridad);
                var objetoActualizar = _repObjeto.FirstById(this.Id);
                var estadoActualizar = false;

                if (selectPrioridad != null)
                {
                    if (selectPrioridad.Id == objetoActualizar.Id)
                    {
                        estadoActualizar = true;
                    }
                    else
                    {
                        estadoActualizar = false;
                    }
                }
                else
                {
                    estadoActualizar = true;
                }

                if (estadoActualizar)
                {
                    objetoActualizar.IdPais = this.IdPais;
                    objetoActualizar.IdProveedor = this.IdProveedor;
                    objetoActualizar.Nombre = this.Nombre;
                    objetoActualizar.Prioridad = this.Prioridad;
                    objetoActualizar.FechaModificacion = this.FechaModificacion;
                    objetoActualizar.UsuarioModificacion = this.UsuarioModificacion;

                    var respuesta = _repObjeto.Update(objetoActualizar);

                    if (respuesta)
                    {
                        Excepcion = new ErrorGuiaBO();
                        Excepcion.Error = false;
                    }
                    else
                    {
                        Excepcion = new ErrorGuiaBO();
                        Excepcion.Error = true;
                        Excepcion.Message = "Error, no se relizo la actualizacion del registro.";
                    }
                }
                else
                {
                    Excepcion = new ErrorGuiaBO();
                    Excepcion.Error = true;
                    Excepcion.Message = "Error, el país tiene una prioridad ya configurada, seleccione otra.";
                }
                //else
                //{
                //    Excepcion = new ErrorGuiaBO();
                //    Excepcion.Error = true;
                //    Excepcion.Message = "Error, el registro enviado no existe en la base de datos o el dato enviado es incorrecto.";
                //}

            }
            catch (Exception ex)
            {
                Excepcion = new ErrorGuiaBO();
                Excepcion.Error = true;

                if (ex.InnerException == null)
                {
                    Excepcion.InnerException = "";
                }
                else
                {
                    Excepcion.InnerException = ex.InnerException.ToString();
                }

                Excepcion.Message = ex.Message;
                Excepcion.Source = ex.Source;
                Excepcion.Descripcion = ex.ToString();

            }
        }

        /// Autor: Abelson Quiñones
        /// Fecha: 30/09/2021
        /// Version: 1.0
        /// <summary>
        /// Eliminar el metodo de pago
        /// </summary>
        /// <returns>no retorna nada</returns>
        public void EliminarPasarelaPagoPw()
        {
            try
            {
                PasarelaPagoPwRepositorio _repObjeto = new PasarelaPagoPwRepositorio(_integraDBContext);

                if (_repObjeto.Exist(this.Id))
                {
                    var respuesta = _repObjeto.Delete(this.Id, this.UsuarioModificacion);

                    if (respuesta)
                    {
                        Excepcion = new ErrorGuiaBO();
                        Excepcion.Error = false;
                    }
                    else
                    {
                        Excepcion = new ErrorGuiaBO();
                        Excepcion.Error = true;
                        Excepcion.Message = "Error, no se relizo la eliminacion del registro.";
                    }
                }
                else
                {
                    Excepcion = new ErrorGuiaBO();
                    Excepcion.Error = true;
                    Excepcion.Message = "Error, el registro enviado no existe en la base de datos o el dato enviado es incorrecto";
                }

            }
            catch (Exception ex)
            {
                Excepcion = new ErrorGuiaBO();
                Excepcion.Error = true;

                if (ex.InnerException == null)
                {
                    Excepcion.InnerException = "";
                }
                else
                {
                    Excepcion.InnerException = ex.InnerException.ToString();
                }

                Excepcion.Message = ex.Message;
                Excepcion.Source = ex.Source;
                Excepcion.Descripcion = ex.ToString();

            }
        }

        /// Autor: Abelson Quiñones
        /// Fecha: 30/09/2021
        /// Version: 1.0
        /// <summary>
        /// Obtener la lista de metodos de pago segun el estado
        /// </summary>
        /// <returns>List<RegistroPasarelaPagoPWDTO></returns>
        public List<RegistroPasarelaPagoPWDTO> ListaRegistroPasarelaPagoPw(bool Estado)
        {
            PasarelaPagoPwRepositorio _repObjeto = new PasarelaPagoPwRepositorio(_integraDBContext);

            var objetoRegistro = _repObjeto.ListaRegistroPasarelaPagoPw(Estado);

            return objetoRegistro;
        }

        /// Autor: Abelson Quiñones
        /// Fecha: 30/09/2021
        /// Version: 1.0
        /// <summary>
        /// Obtener el metodo de pago segun el id
        /// </summary>
        /// <returns>RegistroPasarelaPagoPWDTO</returns>
        public RegistroPasarelaPagoPWDTO RegistroPasarelaPagoPwPorId(int Id)
        {
            PasarelaPagoPwRepositorio _repObjeto = new PasarelaPagoPwRepositorio(_integraDBContext);

            var objetoRegistro = _repObjeto.RegistroPasarelaPagoPwPorId(Id);

            return objetoRegistro;
        }

        /// Autor: Abelson Quiñones
        /// Fecha: 30/09/2021
        /// Version: 1.0
        /// <summary>
        /// Obtener la lista de metodos de pago registrados el pais del idAlumno
        /// </summary>
        /// <returns>List<RegistroPasarelaPagoPWDTO></returns>
        public List<RegistroPasarelaPagoPWDTO> ListaMetodoPagoPorIdAlumno(int IdAlumno)
        {
            PasarelaPagoPwRepositorio _repObjeto = new PasarelaPagoPwRepositorio(_integraDBContext);

            var listaMetodos = _repObjeto.ListaMetodoPagoPorIdAlumno(IdAlumno);

            return listaMetodos;
        }

        /// Autor: Abelson Quiñones
        /// Fecha: 30/09/2021
        /// Version: 1.0
        /// <summary>
        /// Obtener el metodo de pago registrado por el idMatricula
        /// </summary>
        /// <returns>MedioPagoMatriculaCronogramaDTO</returns>
        public MedioPagoMatriculaCronogramaDTO MedioPagoMatriculaCronogramaPorIdMatricula(int IdMatricula)
        {
            PasarelaPagoPwRepositorio _repObjeto = new PasarelaPagoPwRepositorio(_integraDBContext);

            var metodoPago = _repObjeto.MedioPagoMatriculaCronogramaPorIdMatricula(IdMatricula);

            return metodoPago;
        }

        /// Autor: Abelson Quiñones
        /// Fecha: 30/09/2021
        /// Version: 1.0
        /// <summary>
        /// registrar el metodo de pago seleccionado para la matricula
        /// </summary>
        /// <returns>bool</returns>
        public bool RegistroMedioPagoMatriculaCronograma(RegistroMedioPagoMatriculaCronogramaDTO model)
        {
            PasarelaPagoPwRepositorio _repObjeto = new PasarelaPagoPwRepositorio(_integraDBContext);
            //Validar que no sea repetido
            var metodoPagoActual = _repObjeto.MedioPagoMatriculaCronogramaPorIdMatricula(model.IdMatriculaCabecera);
            if(metodoPagoActual != null)
            {
                if (metodoPagoActual.IdMedioPago != model.IdMedioPago)
                {
                    var desactivar = _repObjeto.DesactivarMedioPagoMatriculaCronogramaPorMatricula(model.IdMatriculaCabecera);
                    var _metodoPago = _repObjeto.RegistroMedioPagoMatriculaCronograma(model);
                    return true;
                }
            }
            else
            {
                var desactivar = _repObjeto.DesactivarMedioPagoMatriculaCronogramaPorMatricula(model.IdMatriculaCabecera);
                var _metodoPago = _repObjeto.RegistroMedioPagoMatriculaCronograma(model);
                return true;
            }

            return false;
        }

        /// Autor: Abelson Quiñones
        /// Fecha: 30/09/2021
        /// Version: 1.0
        /// <summary>
        /// obtiene id de la matricula por el codigo de matricula 
        /// </summary>
        /// <returns>int</returns>
        public int BuscarIdMatriculaCabeceraPorCodigoMatricula(string codigoMatricula)
        {
            PasarelaPagoPwRepositorio _repObjeto = new PasarelaPagoPwRepositorio(_integraDBContext);
            if (codigoMatricula != "")
            {
                var idMatricula = _repObjeto.BuscarIdMatriculaCabeceraPorCodigoMatricula(codigoMatricula);
                return idMatricula;
            }
            else
            {
                return 0;
            }

        }

        /// Autor: Abelson Quiñones
        /// Fecha: 06/12/2021
        /// Version: 1.0
        /// <summary>
        /// obtiene id de la matricula por el alumno y el centro de costo 
        /// </summary>
        /// <returns>int</returns>
        public int BuscarIdMatriculaCabeceraPorAlumnoCosto(int IdAlumno, int IdCentroCosto)
        {
            PasarelaPagoPwRepositorio _repObjeto = new PasarelaPagoPwRepositorio(_integraDBContext);
            if (IdAlumno != 0 && IdCentroCosto!=0)
            {
                var idMatricula = _repObjeto.BuscarIdMatriculaCabeceraPorAlumnoCosto(IdAlumno,IdCentroCosto);
                return idMatricula;
            }
            else
            {
                return 0;
            }

        }


    }
}
