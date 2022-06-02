using BSI.Integra.Aplicacion.Base.BO;
using BSI.Integra.Aplicacion.DTOs.Scode.DTOs.GestionPersonas;
using BSI.Integra.Aplicacion.GestionPersonas.Repositorio;
using BSI.Integra.Persistencia.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BSI.Integra.Aplicacion.GestionPersonas.BO
{
    /// BO: Gestion de Personas / PuestoTrabajoRemuneracionBO
    /// Autor: Jashin Salazar
    /// Fecha: 07/07/2021
    /// <summary>
    /// Columnas y funciones de la tabla gp.T_PuestoTrabajoRemuneracion
    /// </summary>

    public class PuestoTrabajoRemuneracionBO : BaseBO
	{
        /// Propiedades		                        Significado
        /// -------------	                        -----------------------
        /// IdPersonalAreaTrabajo                   Id del area de trabajo de personal
        /// IdPuestoTrabajo                         Id del puesto de trabajo
        /// IdPais                                  Id de pais
        /// IdTableroComercialCategoriaAsesor       Id de Categoria de asesor en el tablero comercial
        /// _integraDBContext                       _integraDBContext
        /// _repPuestoTrabajoRemuneracion           Repositorio de PuestoTrabajoRemuneracion
        /// _repPuestoTrabajoRemuneracionDetalle    Repositorio de PuestoTrabajoRemuneracionDetalle

        public int IdPersonalAreaTrabajo { get; set; }
		public int IdPuestoTrabajo { get; set; }
		public int IdPais { get; set; }
		public int IdTableroComercialCategoriaAsesor { get; set; }
        private readonly integraDBContext _integraDBContext;
        private readonly PuestoTrabajoRemuneracionRepositorio _repPuestoTrabajoRemuneracion;
        private readonly PuestoTrabajoRemuneracionDetalleRepositorio _repPuestoTrabajoRemuneracionDetalle;
        public PuestoTrabajoRemuneracionBO()
        {
            _repPuestoTrabajoRemuneracion = new PuestoTrabajoRemuneracionRepositorio();
        }
        
        public PuestoTrabajoRemuneracionBO(integraDBContext integraDBContext)
        {
            _integraDBContext = integraDBContext;
            _repPuestoTrabajoRemuneracion = new PuestoTrabajoRemuneracionRepositorio(_integraDBContext);
            _repPuestoTrabajoRemuneracionDetalle = new PuestoTrabajoRemuneracionDetalleRepositorio(_integraDBContext);
        }
        /// Autor: Jashin Salazar
        /// Fecha: 07/07/2021
        /// Version: 1.0
        /// <summary>
        /// Inserta registros nuevos.
        /// </summary>
        /// <param name="Detalle">Detalle del PuestoTrabajoRemuneracion</param>
        /// <returns>bool</returns>
        public bool InsertarRegistro(List<PuestoTrabajoRemuneracionDetalleDTO> Detalle)
        {
            try
            {
                bool respuesta = false;
                bool estado = this.ValidarPuestoTrabajoRemuneracion();
                bool validacionDetalle= false;

                if (!estado)
                {
                    bool insercion = _repPuestoTrabajoRemuneracion.Insert(this);
                    if (insercion)
                    {
                        if (Detalle != null)
                        {
                            validacionDetalle = this.ValidarPuestoTrabajoRemuneracionDetalle(Detalle);
                            //var res1 = _repPuestoTrabajoRemuneracionDetalle.Insert(validacionDetalle);
                            respuesta = validacionDetalle;
                        }
                        else
                        {
                            respuesta = true;
                        }
                    }
                }
                return respuesta;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Jashin Salazar
        /// Fecha: 07/07/2021
        /// Version: 1.0
        /// <summary>
        /// Actualiza registros.
        /// </summary>
        /// <param name="Detalle">Detalle del PuestoTrabajoRemuneracion</param>
        /// <returns>bool</returns>
        public bool ActualizarRegistro(List<PuestoTrabajoRemuneracionDetalleDTO> Detalle)
        {
            try
            {
                //PuestoTrabajoRemuneracionDetalleRepositorio _repPuestoTrabajoRemuneracionDetalle2 = new PuestoTrabajoRemuneracionDetalleRepositorio(_integraDBContext);
                bool respuesta = false;
                //bool estado = this.ValidarPuestoTrabajoRemuneracion();
                bool validacionDetalle=false;

                //if (!estado)
                //{
                    bool actualizacion = _repPuestoTrabajoRemuneracion.Update(this);
                    if (actualizacion)
                    {
                        if (Detalle != null)
                        {
                            var ptrv = this._repPuestoTrabajoRemuneracionDetalle.GetBy(x => x.IdPuestoTrabajoRemuneracion == this.Id).ToList();

                            foreach (var item in ptrv)
                            {
                                if (!Detalle.Any(x => x.Id == item.Id))
                                {
                                    _repPuestoTrabajoRemuneracionDetalle.Delete(item.Id, this.UsuarioModificacion);
                                }
                            }
                            validacionDetalle = this.ValidarPuestoTrabajoRemuneracionDetalle(Detalle);
                            respuesta = validacionDetalle;
                        }
                        else
                        {
                            respuesta = true;
                        }
                    }
                //}
                return respuesta;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Jashin Salazar
        /// Fecha: 07/07/2021
        /// Version: 1.0
        /// <summary>
        /// Valida las reglas de negocio del detalle del PuestoTrabajoRemuneracion.
        /// </summary>
        /// <param name="Detalle">Detalle del PuestoTrabajoRemuneracion</param>
        /// <returns>bool</returns>
        public bool ValidarPuestoTrabajoRemuneracionDetalle(List<PuestoTrabajoRemuneracionDetalleDTO> Detalle)
        {
            try
            {
                var res1 = false;
                var accion = 0;
                List<PuestoTrabajoRemuneracionDetalleBO> listaResultado = new List<PuestoTrabajoRemuneracionDetalleBO>();
                if (Detalle != null)
                {
                    var fila = 1;
                    foreach (var item in Detalle)
                    {
                        PuestoTrabajoRemuneracionDetalleBO puestoTrabajoRemuneracionVariable = new PuestoTrabajoRemuneracionDetalleBO();
                        if (item.Id>0)
                        {
                            accion = 1; //Update
                            
                            puestoTrabajoRemuneracionVariable = _repPuestoTrabajoRemuneracionDetalle.FirstById(item.Id);
                            puestoTrabajoRemuneracionVariable.IdPuestoTrabajoRemuneracion = (int)item.IdPuestoTrabajoRemuneracion;
                            puestoTrabajoRemuneracionVariable.IdRemuneracionTipo = (int)item.IdRemuneracion;
                            puestoTrabajoRemuneracionVariable.IdRemuneracionTipoCobro = (int)item.IdTipoRemuneracion;
                            puestoTrabajoRemuneracionVariable.IdRemuneracionFormaCobro = (int)item.IdClaseRemuneracion;
                            puestoTrabajoRemuneracionVariable.IdRemuneracionPeriodoCobro = (int)item.IdPeriodoRemuneracion;
                            puestoTrabajoRemuneracionVariable.EsTasa = (bool)item.Tasa;
                            puestoTrabajoRemuneracionVariable.MontoFijo = item.Monto;
                            puestoTrabajoRemuneracionVariable.IdMonedaMontoFijo = item.IdMoneda;
                            puestoTrabajoRemuneracionVariable.PorcentajeTasa = item.PorcentajeTasa;
                            puestoTrabajoRemuneracionVariable.DescripcionEquipo = item.DescripcionEquipo;
                            puestoTrabajoRemuneracionVariable.TieneCondicion = (bool)item.TieneCondicion;
                            puestoTrabajoRemuneracionVariable.IdRemuneracionDescripcionMonetaria = item.IdDescripcionMonetaria;
                            puestoTrabajoRemuneracionVariable.RangoValorMinimo = item.ValorMinimo;
                            puestoTrabajoRemuneracionVariable.RangoValorMaximo = item.ValorMaximo;
                            puestoTrabajoRemuneracionVariable.IdMonedaRangoValor = item.IdMonedaValorVariable;
                            puestoTrabajoRemuneracionVariable.IngresoMensual = item.IngresoMensual;
                            puestoTrabajoRemuneracionVariable.UsuarioModificacion = this.UsuarioModificacion;
                            puestoTrabajoRemuneracionVariable.FechaModificacion = DateTime.Now;
                        }
                        else
                        {
                            accion = 2;//Insert
                            puestoTrabajoRemuneracionVariable = new PuestoTrabajoRemuneracionDetalleBO()
                            {
                                IdPuestoTrabajoRemuneracion = this.Id,
                                IdRemuneracionTipo = (int)item.IdRemuneracion,
                                IdRemuneracionTipoCobro = (int)item.IdTipoRemuneracion,
                                IdRemuneracionFormaCobro = (int)item.IdClaseRemuneracion,
                                IdRemuneracionPeriodoCobro = (int)item.IdPeriodoRemuneracion,
                                EsTasa = (bool)item.Tasa,
                                MontoFijo = item.Monto,
                                IdMonedaMontoFijo = item.IdMoneda,
                                PorcentajeTasa = (int)item.PorcentajeTasa,
                                DescripcionEquipo = item.DescripcionEquipo,
                                TieneCondicion = (bool)item.TieneCondicion,
                                IdRemuneracionDescripcionMonetaria = item.IdDescripcionMonetaria,
                                RangoValorMinimo = item.ValorMinimo,
                                RangoValorMaximo = item.ValorMaximo,
                                IdMonedaRangoValor = item.IdMonedaValorVariable,
                                IngresoMensual = item.IngresoMensual,
                                Estado = true,
                                UsuarioCreacion = this.UsuarioCreacion,
                                UsuarioModificacion = this.UsuarioModificacion,
                                FechaCreacion = DateTime.Now,
                                FechaModificacion = DateTime.Now
                            };
                        }
                        
                        //var res1 = _repPuestoTrabajoRemuneracionVariable.Insert(puestoTrabajoRemuneracionVariable);
                        if (puestoTrabajoRemuneracionVariable.IdRemuneracionTipoCobro == 2)
                        {
                            if (puestoTrabajoRemuneracionVariable.MontoFijo == null || puestoTrabajoRemuneracionVariable.MontoFijo == 0)
                            {
                                throw new Exception("El monto fijo no puede estar vacio. En la fila: "+fila);
                            }
                            else if (puestoTrabajoRemuneracionVariable.IdMonedaMontoFijo == null || puestoTrabajoRemuneracionVariable.IdMonedaMontoFijo == 0)
                            {
                                throw new Exception("Debe escoger una moneda. En la fila: " + fila);
                            }
                            //else
                            //{
                            //    //resultado = true;
                            //    listaResultado.Add(puestoTrabajoRemuneracionVariable);
                            //}
                        }
                        if (puestoTrabajoRemuneracionVariable.IdRemuneracionTipo == 4)
                        {
                            if (puestoTrabajoRemuneracionVariable.IngresoMensual.Equals("") || puestoTrabajoRemuneracionVariable.IngresoMensual == null || puestoTrabajoRemuneracionVariable.IngresoMensual == 0)
                            {
                                throw new Exception("El Ingreso del mes no puede ser vacio. En la fila: " + fila);
                            }
                        }
                        else
                        {
                            if (puestoTrabajoRemuneracionVariable.IngresoMensual != null && puestoTrabajoRemuneracionVariable.IngresoMensual != 0)
                            {
                                throw new Exception("El Ingreso del mes debe ser vacio. En la fila: " + fila);
                            }
                        }
                        if (puestoTrabajoRemuneracionVariable.IdRemuneracionFormaCobro == 1)
                        {
                            if (puestoTrabajoRemuneracionVariable.DescripcionEquipo != null && !puestoTrabajoRemuneracionVariable.DescripcionEquipo.Equals(""))
                            {
                                throw new Exception("La descripcion del equipo debe estar vacia. En la fila: " + fila);
                            }
                        } else if (puestoTrabajoRemuneracionVariable.IdRemuneracionFormaCobro == 2)
                        { 
                            if (puestoTrabajoRemuneracionVariable.IdMonedaMontoFijo != null && puestoTrabajoRemuneracionVariable.IdMonedaMontoFijo != 0)
                            {
                                throw new Exception("No debe escoger una moneda. En la fila: " + fila);
                            }
                        }
                        if (puestoTrabajoRemuneracionVariable.EsTasa == true)
                        {
                            if (puestoTrabajoRemuneracionVariable.PorcentajeTasa == null || puestoTrabajoRemuneracionVariable.PorcentajeTasa==0)
                            {
                                throw new Exception("El porcentaje de tasa no puede estar vacia. En la fila: " + fila);
                            }
                            else if (puestoTrabajoRemuneracionVariable.MontoFijo != null && puestoTrabajoRemuneracionVariable.MontoFijo != 0)
                            {
                                throw new Exception("El monto fijo debe estar vacio. En la fila: " + fila);
                            }
                            else if (puestoTrabajoRemuneracionVariable.IdMonedaMontoFijo != null && puestoTrabajoRemuneracionVariable.IdMonedaMontoFijo != 0)
                            {
                                throw new Exception("No debe escoger una moneda. En la fila: " + fila);
                            }
                        }
                        else
                        {
                            if(puestoTrabajoRemuneracionVariable.IdRemuneracionTipo != 4)
                            {
                                if (puestoTrabajoRemuneracionVariable.MontoFijo == null || puestoTrabajoRemuneracionVariable.MontoFijo == 0)
                                {
                                    throw new Exception("El monto fijo no puede estar vacio. En la fila: " + fila);
                                }
                                else if (puestoTrabajoRemuneracionVariable.IdMonedaMontoFijo == null || puestoTrabajoRemuneracionVariable.IdMonedaMontoFijo == 0)
                                {
                                    throw new Exception("Debe escoger una moneda. En la fila: " + fila);
                                }
                                else if (puestoTrabajoRemuneracionVariable.PorcentajeTasa != null && puestoTrabajoRemuneracionVariable.PorcentajeTasa != 0)
                                {
                                    throw new Exception("El porcentaje de tasa debe estar vacia. En la fila: " + fila);
                                }
                            }
                        }
                        if (puestoTrabajoRemuneracionVariable.TieneCondicion==true)
                        {
                            if(puestoTrabajoRemuneracionVariable.IdRemuneracionDescripcionMonetaria == null || puestoTrabajoRemuneracionVariable.IdRemuneracionDescripcionMonetaria == 0)
                            {
                                throw new Exception("Debe escoger una descripcion monetaria. En la fila: " + fila);
                            } 
                            else if (puestoTrabajoRemuneracionVariable.RangoValorMinimo==null|| puestoTrabajoRemuneracionVariable.RangoValorMinimo == 0)
                            {
                                throw new Exception("Debe escoger un rango minimo. En la fila: " + fila);
                            }
                            else if (puestoTrabajoRemuneracionVariable.IdMonedaRangoValor == null || puestoTrabajoRemuneracionVariable.IdMonedaRangoValor == 0)
                            {
                                throw new Exception("Debe escoger una moneda. En la fila: " + fila);
                            }
                        }
                        else
                        {
                            if (puestoTrabajoRemuneracionVariable.IdRemuneracionDescripcionMonetaria != null && puestoTrabajoRemuneracionVariable.IdRemuneracionDescripcionMonetaria != 0)
                            {
                                throw new Exception("No debe escoger una descripcion monetaria. En la fila: " + fila);
                            }
                            else if (puestoTrabajoRemuneracionVariable.RangoValorMinimo != null && puestoTrabajoRemuneracionVariable.RangoValorMinimo != 0)
                            {
                                throw new Exception("No debe escoger un rango minimo. En la fila: " + fila);
                            }
                            else if (puestoTrabajoRemuneracionVariable.RangoValorMaximo != null && puestoTrabajoRemuneracionVariable.RangoValorMaximo != 0)
                            {
                                throw new Exception("No debe escoger un rango minimo. En la fila: " + fila);
                            }
                            else if (puestoTrabajoRemuneracionVariable.IdMonedaRangoValor != null && puestoTrabajoRemuneracionVariable.IdMonedaRangoValor != 0)
                            {
                                throw new Exception("No debe escoger una moneda. En la fila: " + fila);
                            }
                        }
                        if (accion == 2)
                        {
                            res1 = _repPuestoTrabajoRemuneracionDetalle.Insert(puestoTrabajoRemuneracionVariable);
                        }
                        else if (accion == 1)
                        {
                            res1 = _repPuestoTrabajoRemuneracionDetalle.Update(puestoTrabajoRemuneracionVariable);
                        }
                        //listaResultado.Add(puestoTrabajoRemuneracionVariable);
                        fila++;
                    }
                }
                return res1;
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
			
        }
        /// Autor: Jashin Salazar
        /// Fecha: 07/07/2021
        /// Version: 1.0
        /// <summary>
        /// Valida las reglas de negocio del PuestoTrabajoRemuneracion.
        /// </summary>
        /// <returns>bool</returns>
		public bool ValidarPuestoTrabajoRemuneracion()
        {
            try
            {
                //PuestoTrabajoRemuneracionRepositorio _repPuestoTrabajoRemuneracion = new PuestoTrabajoRemuneracionRepositorio(_integraDBContext);
                var remuneracion = _repPuestoTrabajoRemuneracion.GetBy(x => x.IdPersonalAreaTrabajo == this.IdPersonalAreaTrabajo && x.IdPuestoTrabajo == this.IdPuestoTrabajo && x.IdPais == this.IdPais && x.IdTableroComercialCategoriaAsesor == this.IdTableroComercialCategoriaAsesor).ToList();
                if (remuneracion.Count > 0)
                {
                    throw new Exception("Error al insertar: Registro ya existente");
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
		}
        /// Autor: Jashin Salazar
        /// Fecha: 07/07/2021
        /// Version: 1.0
        /// <summary>
        /// Valida las reglas de negocio del PuestoTrabajoRemuneracion.
        /// </summary>
        /// <returns>bool</returns>
		public bool ValidarExistente()
        {
            try
            {
                //PuestoTrabajoRemuneracionRepositorio _repPuestoTrabajoRemuneracion = new PuestoTrabajoRemuneracionRepositorio(_integraDBContext);
                var remuneracion = _repPuestoTrabajoRemuneracion.GetBy(x => x.IdPersonalAreaTrabajo == this.IdPersonalAreaTrabajo && x.IdPuestoTrabajo == this.IdPuestoTrabajo && x.IdPais == this.IdPais && x.IdTableroComercialCategoriaAsesor == this.IdTableroComercialCategoriaAsesor).ToList();
                if (remuneracion.Count > 0)
                {
                    var Id = remuneracion.ElementAt(0).Id;
                    _repPuestoTrabajoRemuneracion.EliminarExistentes(Id);
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
