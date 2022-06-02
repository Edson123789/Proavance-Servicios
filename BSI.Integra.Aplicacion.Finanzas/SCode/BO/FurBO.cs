using BSI.Integra.Aplicacion.Base.BO;
using BSI.Integra.Aplicacion.DTOs;
using System;
using System.Collections.Generic;
using System.Text;
using BSI.Integra.Aplicacion.Transversal.Helper;
using BSI.Integra.Aplicacion.Finanzas.Repositorio;
using BSI.Integra.Persistencia.Models;
using System.Globalization;
using BSI.Integra.Aplicacion.Transversal.Repositorio;

namespace BSI.Integra.Aplicacion.Finanzas.BO
{
    public class FurBO : BaseBO
    {
        public string Codigo { get; set; }
        public int? IdPespecifico { get; set; }
        public int? IdPersonalAreaTrabajo { get; set; }
        public int IdCiudad { get; set; }
        public string NumeroFur { get; set; }
        public int? NumeroSemana { get; set; }
        public string UsuarioSolicitud { get; set; }
        public string UsuarioAutoriza { get; set; }
        public string Observaciones { get; set; }
        public int? IdProveedor { get; set; }
        public int? IdProducto { get; set; }
        public decimal Cantidad { get; set; }
        public decimal? Monto { get; set; }
        public int? IdProductoPresentacion { get; set; }
        public int? IdCentroCosto { get; set; }
        public int? IdMonedaProveedor { get; set; }
        public string NumeroCuenta { get; set; }
        public string NumeroRecibo { get; set; }
        public decimal? PagoMonedaOrigen { get; set; }
        public decimal? PagoDolares { get; set; }
        public DateTime? FechaCobroBanco { get; set; }
        public string ResponsableCobro { get; set; }
        public DateTime? FechaPago { get; set; }
        public string Cuenta { get; set; }
        public string Descripcion { get; set; }
        public decimal PrecioUnitarioMonedaOrigen { get; set; }
        public decimal PrecioUnitarioDolares { get; set; }
        public decimal PrecioTotalMonedaOrigen { get; set; }
        public decimal PrecioTotalDolares { get; set; }
        public int IdFurFaseAprobacion1 { get; set; }
        public int? AprobadoFase2 { get; set; }
        public DateTime? FechaLimite { get; set; }
        public int IdFurTipoPedido { get; set; }
        public bool Cancelado { get; set; }
        public int? Antiguo { get; set; }
        public int? IdMonedaPagoReal { get; set; }
        public bool? OcupadoSolicitud { get; set; }
        public bool? OcupadoRendicion { get; set; }
        public bool EstadoAprobadoObservado { get; set; }
        public int? IdMigracion { get; set; }
        public int? IdMonedaPagoRealizado { get; set; }
        public DateTime? FechaAprobacionProcesoCulminado { get; set; }
		public decimal? MontoProyectado { get; set; }
        public bool? EsDiferido { get; set; }
        public int? IdFurSubFaseAprobacion { get; set; }
        public DateTime? FechaLimiteReprogramacion { get; set; }
		public int? IdEmpresa { get; set; }

		/// Autor: Lisbeth Ortogorin
		/// Fecha: 01/06/2021
		/// Version: 1.0
		/// <summary>
		/// Obtiene la lista de los furs segun los parametros de la vista
		/// </summary>
		/// <param name="ParametrosFurDTO">Json que contiene el area, usuario, ciudad, año, semana y estado</param>
		/// <returns>FurDTO</returns>

		public List<FurDTO> ObtenerFursParaGrid(ParametrosFurDTO json) 
		{

            FurRepositorio repFurRep = new FurRepositorio();
            var ListFur=new List<FurDTO>();
            int modo = 1;
            if (json.IdRol == ValorEstatico.IdRolCoordinadoraFinanzas || json.IdRol == ValorEstatico.IdRolAsistenteAdministracionFinanzas) {
                if (json.IdEstadoFaseAprobacion1 == ValorEstatico.IdFurAprobadoPorJefeFinanzas) //aprobados por jefe de finanzas
                {
                    modo = 2; // //mostrar de todas las áreas los FURs aprobados por el jefe de finanzas
                }
            }
            if (json.Usuario == "azorrilla" || json.Usuario == "avillanueva") {
                modo = 10;
            }
            if ( json.Usuario=="acornejo" || json.Usuario=="gchirinos" || json.Usuario=="lcarpio" || json.Usuario=="aarcana" || json.Usuario== "acruzq" 
                || json.Usuario== "esanchez1" || json.Usuario=="cleon" || json.Usuario=="pgaray" || json.Usuario== "rymejia" || json.Usuario=="wruiz" || json.Usuario== "mlara" || json.Usuario=="cavaldivia" )
            {
                modo = 11;
            }

            //if (json.IdEstadoFaseAprobacion1 == ValorEstatico.IdFurEstadoPorAprobar || json.IdEstadoFaseAprobacion1 == ValorEstatico.IdFurTodosPorEliminar)
            //{
            //    json.Codigo = "";
            //}

            if (json.IdEstadoFaseAprobacion1 == ValorEstatico.IdFurTodosPorEliminar)
            {
                ListFur = repFurRep.ObtenerFursParaGrid(json.IdArea,json.Codigo, json.IdRol, json.IdEstadoFaseAprobacion1,json.pageSize,json.skip,"",modo, json.Anio,json.Semana,json.IdCiudad);
                    //ObtenerFursParaGrid(int IdArea, string Codigo, int IdRol, int IdEstadoFaseAprobacion1, int pageSize, int skip, string UserName, int modo)
            }
            else {
                ListFur = repFurRep.ObtenerFursParaGrid(json.IdArea, json.Codigo, json.IdRol, json.IdEstadoFaseAprobacion1, json.pageSize, json.skip, json.Usuario, modo, json.Anio, json.Semana, json.IdCiudad);
            }
            return ListFur;
        }

        public object ObtenerFursPorAprobar(ParametroFurPorAprobarDTO json)
        {
            int rol = 0;
            if (json.IdRol != ValorEstatico.IdRolJefaturaFinanzas)
            {
                rol = 0;
            }
            else {
                if (json.Respuesta == 0)
                {
                    rol = 0;  // Rol como jefe de Area 
                }
                else if (json.Respuesta == 1) {
                    rol = 1; //Rol como Jefe de Finanzas
                }
            }

            FurRepositorio repFurRep = new FurRepositorio();
            return (repFurRep.ObtenerFurPorAprobar(json.IdArea,json.Codigo,json.IdRol,rol));
            
        }
        public bool AprobarObservarFur(FiltroDTO json, int IdRol, int checkedIsFurGeneral, bool isAprobar, string observacion, integraDBContext context)
        {
            try
            {// Se verifica si es jefe de Finanzas para la aprobacion Final de Furs,
                int isJefeFinanzas = 0;
                if (IdRol != ValorEstatico.IdRolJefaturaFinanzas)
                {
                    isJefeFinanzas = 0;
                }
                else
                {
                    if (checkedIsFurGeneral == 0)
                    {
                        isJefeFinanzas = 0;  // Rol como jefe de Area 
                    }
                    else if (checkedIsFurGeneral == 1)
                    {
                        isJefeFinanzas = 1; //Rol como Jefe de Finanzas
                    }
                }

                FurRepositorio repFurRep = new FurRepositorio(context);
                FurBO fur = new FurBO();
                fur = repFurRep.FirstById(json.Id);
                if (isJefeFinanzas == 0) // Si no es Jefe de Finanzas o solo Es Jefe de Algun Area
                {
                    fur.IdFurFaseAprobacion1 = isAprobar ? ValorEstatico.IdFurAprobadoPorJefeArea : isAprobar == false ? ValorEstatico.IdFurObservadoPorJefeArea : fur.IdFurFaseAprobacion1; //Si el Fur ess Aprobado pasa a AprobadoPor Jefe de Area, de lo Contrario pasa a Observado por Jefe de Area
                    fur.UsuarioAutoriza = isAprobar ? json.Nombre : fur.UsuarioAutoriza;
                }
                else
                {
                    if (isJefeFinanzas == 1)
                    {// Si es Jefe de Finanzas
                        //Si el Fur es Aprobado pasa a AprobadoPor Jefe de Area, de lo Contrario pasa a Observado por Jefe de Area
                        fur.IdFurFaseAprobacion1 = isAprobar ? ValorEstatico.IdFurAprobadoPorJefeFinanzas : isAprobar == false ? ValorEstatico.IdFurObservadoPorJefeFinanzas : fur.IdFurFaseAprobacion1;
                        if (isAprobar) { //Solo se llena este campo cuando el jefe de Finanzas aprueba un fur
                            fur.FechaAprobacionProcesoCulminado = DateTime.Now;
                        }
                        fur.EstadoAprobadoObservado = isAprobar ? true : false;
                    }
                    if (isJefeFinanzas == 2)
                    { //Proyectados
                        fur.EstadoAprobadoObservado = false;
                        fur.IdFurFaseAprobacion1 = isAprobar ? ValorEstatico.IdFurEstadoPorAprobar : fur.IdFurFaseAprobacion1;
                    }
                }
                fur.Observaciones = observacion;
                fur.FechaModificacion = DateTime.Now;
                fur.UsuarioModificacion = json.Nombre;


                repFurRep.Update(fur);

                return true;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

		public bool obtenerFurInsertar(FurConfiguracionAutomaticaConProductoDTO furConfiguracionAutomatica, string areaTrabajoAbrev)
		{
			var estado = false;
			try
			{
				HistoricoProductoProveedorRepositorio _repHistoricoProductoProveedor = new HistoricoProductoProveedorRepositorio();
				FurRepositorio _repFur = new FurRepositorio();
				CiudadRepositorio _repCiudad = new CiudadRepositorio();
				var fechaActual = DateTime.Now;
				if (fechaActual >= furConfiguracionAutomatica.FechaInicioConfiguracion && fechaActual <= furConfiguracionAutomatica.FechaFinConfiguracion)
				{
					var detalleFur = _repHistoricoProductoProveedor.ObtenerDetalleFUR(furConfiguracionAutomatica.IdProducto, furConfiguracionAutomatica.IdProveedor);
					int diaMes = 0;
					if (furConfiguracionAutomatica.IdFrecuencia == 3)// Mensual
					{
						diaMes = furConfiguracionAutomatica.FechaGeneracionFur.Day;
						int numeroDias = DateTime.DaysInMonth(fechaActual.Year, fechaActual.Month);
						if (diaMes > numeroDias)
						{
							diaMes = numeroDias;
						}
						if (fechaActual.Day == diaMes)
						{
							string anio = DateTime.Now.Year.ToString().Substring(2, 2);
							int semana = obtenerNumeroSemana(fechaActual) + furConfiguracionAutomatica.AjusteNumeroSemana;
							int numeroSemanas = obtenerNumeroSemana(new DateTime(fechaActual.Year,12,31));

							if(semana > numeroSemanas)
							{
								semana = semana - numeroSemanas;
								anio = (int.Parse(anio) + 1).ToString();
							}
							
							string ciudad = _repCiudad.FirstById(furConfiguracionAutomatica.IdSede).Nombre;
							string codigo = anio + "-" + ciudad.Substring(0,1) + "-" + areaTrabajoAbrev + "-" + (furConfiguracionAutomatica.AjusteNumeroSemana.ToString().Length > 0 ? "" : "0") + semana + "-";
							var correlativo = _repFur.ObtenerCorrelativo(anio, ((semana.ToString().Length > 1 ? "" : "0") + semana.ToString()), areaTrabajoAbrev);

							this.Codigo = codigo + correlativo.Correlativo;
							this.IdPersonalAreaTrabajo = furConfiguracionAutomatica.IdPersonalAreaTrabajo;
							this.IdCiudad = furConfiguracionAutomatica.IdSede;
							this.IdFurTipoPedido = furConfiguracionAutomatica.IdFurTipoPedido;
							this.NumeroSemana = semana;
							this.UsuarioSolicitud = furConfiguracionAutomatica.Usuario;
							this.IdCentroCosto = furConfiguracionAutomatica.IdCentroCosto;
							this.NumeroCuenta = detalleFur.NumeroCuenta;
							this.Cuenta = detalleFur.CuentaDescripcion;
							this.IdProveedor = furConfiguracionAutomatica.IdProveedor;
							this.IdProducto = furConfiguracionAutomatica.IdProducto;
							this.Cantidad = furConfiguracionAutomatica.Cantidad;
							this.IdProductoPresentacion = furConfiguracionAutomatica.IdProductoPresentacion;
							this.Descripcion = detalleFur.Descripcion;
							this.FechaLimite = fechaActual.AddDays(furConfiguracionAutomatica.AjusteNumeroSemana * 7);
							this.PrecioUnitarioMonedaOrigen = detalleFur.PrecioOrigen;
							this.PrecioUnitarioDolares = detalleFur.PrecioDolares;
							this.IdMonedaProveedor = detalleFur.IdMoneda;
							this.IdFurFaseAprobacion1 = 1;
							this.PrecioTotalMonedaOrigen = Convert.ToDecimal(detalleFur.PrecioOrigen * furConfiguracionAutomatica.Cantidad);
							this.MontoProyectado = this.PrecioTotalMonedaOrigen;
							this.Monto = this.PrecioTotalMonedaOrigen;
							this.PrecioTotalDolares = Convert.ToDecimal(detalleFur.PrecioDolares * furConfiguracionAutomatica.Cantidad);
							this.PagoMonedaOrigen = detalleFur.PrecioOrigen * furConfiguracionAutomatica.Cantidad;
							this.PagoDolares = detalleFur.PrecioDolares * furConfiguracionAutomatica.Cantidad;
							this.Cancelado = false;
							this.Antiguo = 0;
							this.IdMonedaPagoReal = furConfiguracionAutomatica.IdMonedaPagoReal;
							this.IdMonedaPagoRealizado = detalleFur.IdMoneda;
							this.EstadoAprobadoObservado = false;
							this.OcupadoSolicitud = false;
							this.OcupadoRendicion = false;
							this.Estado = true;
							this.UsuarioCreacion = "SYSTEM-AUTOMATIC";
							this.UsuarioModificacion = "SYSTEM-AUTOMATIC";
							this.FechaCreacion = DateTime.Now;
							this.FechaModificacion = DateTime.Now;
							estado = true;
						}
					}
					if(furConfiguracionAutomatica.IdFrecuencia == 5) // Bimestral
					{
						var mesGeneracion = furConfiguracionAutomatica.FechaGeneracionFur.Month % 2;

						var mes = fechaActual.Month % 2;
						int numeroDias = DateTime.DaysInMonth(fechaActual.Year, fechaActual.Month);
						var dia = furConfiguracionAutomatica.FechaGeneracionFur.Day;
						if(dia > numeroDias)
						{
							dia = numeroDias;
						}
						if (mesGeneracion == mes && fechaActual.Day == dia)
						{
							string anio = DateTime.Now.Year.ToString().Substring(2, 2);
							int semana = obtenerNumeroSemana(fechaActual) + furConfiguracionAutomatica.AjusteNumeroSemana;
							int numeroSemanas = obtenerNumeroSemana(new DateTime(fechaActual.Year, 12, 31));
							if (semana > numeroSemanas)
							{
								semana = semana - numeroSemanas;
								anio = (int.Parse(anio) + 1).ToString();
							}

							string ciudad = _repCiudad.FirstById(furConfiguracionAutomatica.IdSede).Nombre;
							string codigo = anio + "-" + ciudad.Substring(0, 1) + "-" + areaTrabajoAbrev + "-" + (furConfiguracionAutomatica.AjusteNumeroSemana.ToString().Length > 0 ? "" : "0") + semana + "-";
							var correlativo = _repFur.ObtenerCorrelativo(anio, ((semana.ToString().Length > 1 ? "" : "0") + semana.ToString()), areaTrabajoAbrev);

							this.Codigo = codigo + correlativo.Correlativo;
							this.IdPersonalAreaTrabajo = furConfiguracionAutomatica.IdPersonalAreaTrabajo;
							this.IdCiudad = furConfiguracionAutomatica.IdSede;
							this.IdFurTipoPedido = furConfiguracionAutomatica.IdFurTipoPedido;
							this.NumeroSemana = semana;
							this.UsuarioSolicitud = furConfiguracionAutomatica.Usuario;
							this.IdCentroCosto = furConfiguracionAutomatica.IdCentroCosto;
							this.NumeroCuenta = detalleFur.NumeroCuenta;
							this.Cuenta = detalleFur.CuentaDescripcion;
							this.IdProveedor = furConfiguracionAutomatica.IdProveedor;
							this.IdProducto = furConfiguracionAutomatica.IdProducto;
							this.Cantidad = furConfiguracionAutomatica.Cantidad;
							this.IdProductoPresentacion = furConfiguracionAutomatica.IdProductoPresentacion;
							this.Descripcion = detalleFur.Descripcion;
							this.FechaLimite = fechaActual.AddDays(furConfiguracionAutomatica.AjusteNumeroSemana * 7);
							this.PrecioUnitarioMonedaOrigen = detalleFur.PrecioOrigen;
							this.MontoProyectado = this.PrecioTotalMonedaOrigen;
							this.Monto = this.PrecioTotalMonedaOrigen;
							this.PrecioUnitarioDolares = detalleFur.PrecioDolares;
							this.IdMonedaProveedor = detalleFur.IdMoneda;
							this.IdFurFaseAprobacion1 = 1;
							this.PrecioTotalMonedaOrigen = Convert.ToDecimal(detalleFur.PrecioOrigen * furConfiguracionAutomatica.Cantidad);
							this.PrecioTotalDolares = Convert.ToDecimal(detalleFur.PrecioDolares * furConfiguracionAutomatica.Cantidad);
							this.PagoMonedaOrigen = detalleFur.PrecioOrigen * furConfiguracionAutomatica.Cantidad;
							this.PagoDolares = detalleFur.PrecioDolares * furConfiguracionAutomatica.Cantidad;
							this.Cancelado = false;
							this.Antiguo = 0;
							this.IdMonedaPagoReal = furConfiguracionAutomatica.IdMonedaPagoReal;
							this.IdMonedaPagoRealizado = detalleFur.IdMoneda;
							this.EstadoAprobadoObservado = false;
							this.OcupadoSolicitud = false;
							this.OcupadoRendicion = false;
							this.Estado = true;
							this.UsuarioCreacion = "SYSTEM-AUTOMATIC";
							this.UsuarioModificacion = "SYSTEM-AUTOMATIC";
							this.FechaCreacion = DateTime.Now;
							this.FechaModificacion = DateTime.Now;
							estado = true;
						}
					}
					if (furConfiguracionAutomatica.IdFrecuencia == 6) // Trimestral
					{
						var mesGeneracion = furConfiguracionAutomatica.FechaGeneracionFur.Month % 3;
						var mes = fechaActual.Month % 3;
						int numeroDias = DateTime.DaysInMonth(fechaActual.Year, fechaActual.Month);
						var dia = furConfiguracionAutomatica.FechaGeneracionFur.Day;
						if (dia > numeroDias)
						{
							dia = numeroDias;
						}
						if (mesGeneracion == mes && fechaActual.Day == dia)
						{
							string anio = DateTime.Now.Year.ToString().Substring(2, 2);
							int semana = obtenerNumeroSemana(fechaActual) + furConfiguracionAutomatica.AjusteNumeroSemana;
							int numeroSemanas = obtenerNumeroSemana(new DateTime(fechaActual.Year, 12, 31));
							if (semana > numeroSemanas)
							{
								semana = semana - numeroSemanas;
								anio = (int.Parse(anio) + 1).ToString();
							}

							string ciudad = _repCiudad.FirstById(furConfiguracionAutomatica.IdSede).Nombre;
							string codigo = anio + "-" + ciudad.Substring(0, 1) + "-" + areaTrabajoAbrev + "-" + (furConfiguracionAutomatica.AjusteNumeroSemana.ToString().Length > 0 ? "" : "0") + semana + "-";
							var correlativo = _repFur.ObtenerCorrelativo(anio, ((semana.ToString().Length > 1 ? "" : "0") + semana.ToString()), areaTrabajoAbrev);

							this.Codigo = codigo + correlativo.Correlativo;
							this.IdPersonalAreaTrabajo = furConfiguracionAutomatica.IdPersonalAreaTrabajo;
							this.IdCiudad = furConfiguracionAutomatica.IdSede;
							this.IdFurTipoPedido = furConfiguracionAutomatica.IdFurTipoPedido;
							this.NumeroSemana = semana;
							this.UsuarioSolicitud = furConfiguracionAutomatica.Usuario;
							this.IdCentroCosto = furConfiguracionAutomatica.IdCentroCosto;
							this.NumeroCuenta = detalleFur.NumeroCuenta;
							this.Cuenta = detalleFur.CuentaDescripcion;
							this.IdProveedor = furConfiguracionAutomatica.IdProveedor;
							this.IdProducto = furConfiguracionAutomatica.IdProducto;
							this.Cantidad = furConfiguracionAutomatica.Cantidad;
							this.IdProductoPresentacion = furConfiguracionAutomatica.IdProductoPresentacion;
							this.Descripcion = detalleFur.Descripcion;
							this.FechaLimite = fechaActual.AddDays(furConfiguracionAutomatica.AjusteNumeroSemana * 7);
							this.PrecioUnitarioMonedaOrigen = detalleFur.PrecioOrigen;
							this.MontoProyectado = this.PrecioTotalMonedaOrigen;
							this.Monto = this.PrecioTotalMonedaOrigen;
							this.PrecioUnitarioDolares = detalleFur.PrecioDolares;
							this.IdMonedaProveedor = detalleFur.IdMoneda;
							this.IdFurFaseAprobacion1 = 1;
							this.PrecioTotalMonedaOrigen = Convert.ToDecimal(detalleFur.PrecioOrigen * furConfiguracionAutomatica.Cantidad);
							this.PrecioTotalDolares = Convert.ToDecimal(detalleFur.PrecioDolares * furConfiguracionAutomatica.Cantidad);
							this.PagoMonedaOrigen = detalleFur.PrecioOrigen * furConfiguracionAutomatica.Cantidad;
							this.PagoDolares = detalleFur.PrecioDolares * furConfiguracionAutomatica.Cantidad;
							this.Cancelado = false;
							this.Antiguo = 0;
							this.IdMonedaPagoReal = furConfiguracionAutomatica.IdMonedaPagoReal;
							this.IdMonedaPagoRealizado = detalleFur.IdMoneda;
							this.EstadoAprobadoObservado = false;
							this.OcupadoSolicitud = false;
							this.OcupadoRendicion = false;
							this.Estado = true;
							this.UsuarioCreacion = "SYSTEM-AUTOMATIC";
							this.UsuarioModificacion = "SYSTEM-AUTOMATIC";
							this.FechaCreacion = DateTime.Now;
							this.FechaModificacion = DateTime.Now;
							estado = true;
						}
					}
					if (furConfiguracionAutomatica.IdFrecuencia == 7) // Semestral
					{
						var mesGeneracion = furConfiguracionAutomatica.FechaGeneracionFur.Month % 6;
						var mes = fechaActual.Month % 6;
						int numeroDias = DateTime.DaysInMonth(fechaActual.Year, fechaActual.Month);
						var dia = furConfiguracionAutomatica.FechaGeneracionFur.Day;
						if (dia > numeroDias)
						{
							dia = numeroDias;
						}
						if (mesGeneracion == mes && fechaActual.Day == dia)
						{
							string anio = DateTime.Now.Year.ToString().Substring(2, 2);
							int semana = obtenerNumeroSemana(fechaActual) + furConfiguracionAutomatica.AjusteNumeroSemana;
							int numeroSemanas = obtenerNumeroSemana(new DateTime(fechaActual.Year, 12, 31));
							if (semana > numeroSemanas)
							{
								semana = semana - numeroSemanas;
								anio = (int.Parse(anio) + 1).ToString();
							}

							string ciudad = _repCiudad.FirstById(furConfiguracionAutomatica.IdSede).Nombre;
							string codigo = anio + "-" + ciudad.Substring(0, 1) + "-" + areaTrabajoAbrev + "-" + (furConfiguracionAutomatica.AjusteNumeroSemana.ToString().Length > 0 ? "" : "0") + semana + "-";
							var correlativo = _repFur.ObtenerCorrelativo(anio, ((semana.ToString().Length > 1 ? "" : "0") + semana.ToString()), areaTrabajoAbrev);

							this.Codigo = codigo + correlativo.Correlativo;
							this.IdPersonalAreaTrabajo = furConfiguracionAutomatica.IdPersonalAreaTrabajo;
							this.IdCiudad = furConfiguracionAutomatica.IdSede;
							this.IdFurTipoPedido = furConfiguracionAutomatica.IdFurTipoPedido;
							this.NumeroSemana = semana;
							this.UsuarioSolicitud = furConfiguracionAutomatica.Usuario;
							this.IdCentroCosto = furConfiguracionAutomatica.IdCentroCosto;
							this.NumeroCuenta = detalleFur.NumeroCuenta;
							this.Cuenta = detalleFur.CuentaDescripcion;
							this.IdProveedor = furConfiguracionAutomatica.IdProveedor;
							this.IdProducto = furConfiguracionAutomatica.IdProducto;
							this.Cantidad = furConfiguracionAutomatica.Cantidad;
							this.IdProductoPresentacion = furConfiguracionAutomatica.IdProductoPresentacion;
							this.Descripcion = detalleFur.Descripcion;
							this.FechaLimite = fechaActual.AddDays(furConfiguracionAutomatica.AjusteNumeroSemana * 7);
							this.PrecioUnitarioMonedaOrigen = detalleFur.PrecioOrigen;
							this.MontoProyectado = this.PrecioTotalMonedaOrigen;
							this.Monto = this.PrecioTotalMonedaOrigen;
							this.PrecioUnitarioDolares = detalleFur.PrecioDolares;
							this.IdMonedaProveedor = detalleFur.IdMoneda;
							this.IdFurFaseAprobacion1 = 1;
							this.PrecioTotalMonedaOrigen = Convert.ToDecimal(detalleFur.PrecioOrigen * furConfiguracionAutomatica.Cantidad);
							this.PrecioTotalDolares = Convert.ToDecimal(detalleFur.PrecioDolares * furConfiguracionAutomatica.Cantidad);
							this.PagoMonedaOrigen = detalleFur.PrecioOrigen * furConfiguracionAutomatica.Cantidad;
							this.PagoDolares = detalleFur.PrecioDolares * furConfiguracionAutomatica.Cantidad;
							this.Cancelado = false;
							this.Antiguo = 0;
							this.IdMonedaPagoReal = furConfiguracionAutomatica.IdMonedaPagoReal;
							this.IdMonedaPagoRealizado = detalleFur.IdMoneda;
							this.EstadoAprobadoObservado = false;
							this.OcupadoSolicitud = false;
							this.OcupadoRendicion = false;
							this.Estado = true;
							this.UsuarioCreacion = "SYSTEM-AUTOMATIC";
							this.UsuarioModificacion = "SYSTEM-AUTOMATIC";
							this.FechaCreacion = DateTime.Now;
							this.FechaModificacion = DateTime.Now;
							estado = true;
						}
					}
				}
				return estado;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}
		public bool obtenerFurInsertarAlterno(FurConfiguracionAutomaticaConProductoDTO furConfiguracionAutomatica, string areaTrabajoAbrev, int mes)
		{
			var estado = false;
			try
			{
				HistoricoProductoProveedorRepositorio _repHistoricoProductoProveedor = new HistoricoProductoProveedorRepositorio();
				FurRepositorio _repFur = new FurRepositorio();
				CiudadRepositorio _repCiudad = new CiudadRepositorio();
				var fechaActual = DateTime.Now.AddMonths(mes);
				if (fechaActual >= furConfiguracionAutomatica.FechaInicioConfiguracion && fechaActual <= furConfiguracionAutomatica.FechaFinConfiguracion)
				{
					var detalleFur = _repHistoricoProductoProveedor.ObtenerDetalleFUR(furConfiguracionAutomatica.IdProducto, furConfiguracionAutomatica.IdProveedor);
					int diaMes = 0;
					if (furConfiguracionAutomatica.IdFrecuencia == 3)// Mensual
					{
						diaMes = furConfiguracionAutomatica.FechaGeneracionFur.Day;
						int numeroDias = DateTime.DaysInMonth(fechaActual.Year, fechaActual.Month);
						if (diaMes > numeroDias)
						{
							diaMes = numeroDias;
						}
						if (fechaActual.Day == diaMes)
						{
							DateTime fechaFur = fechaActual;

							fechaFur = fechaFur.AddDays((furConfiguracionAutomatica.AjusteNumeroSemana) * 7);

							int semana = obtenerNumeroSemana(fechaFur);
							string anio = fechaFur.Year.ToString().Substring(2, 2);

							string ciudad = _repCiudad.FirstById(furConfiguracionAutomatica.IdSede).Nombre;
							string codigo = anio + "-" + ciudad.Substring(0, 1) + "-" + areaTrabajoAbrev + "-" + (furConfiguracionAutomatica.AjusteNumeroSemana.ToString().Length > 0 ? "" : "0") + (semana.ToString().Length > 1 ? "" + semana : "0"+ semana)  + "-";
							var correlativo = _repFur.ObtenerCorrelativo(anio, ((semana.ToString().Length > 1 ? "" : "0") + semana.ToString()), areaTrabajoAbrev);

							//this.Codigo = codigo + correlativo.Correlativo;
							this.IdPersonalAreaTrabajo = furConfiguracionAutomatica.IdPersonalAreaTrabajo;
							this.IdCiudad = furConfiguracionAutomatica.IdSede;
							this.IdFurTipoPedido = furConfiguracionAutomatica.IdFurTipoPedido;
							this.NumeroSemana = semana;
							this.UsuarioSolicitud = furConfiguracionAutomatica.Usuario;
							this.IdCentroCosto = furConfiguracionAutomatica.IdCentroCosto;
							this.NumeroCuenta = detalleFur.NumeroCuenta;
							this.Cuenta = detalleFur.CuentaDescripcion;
							this.IdProveedor = furConfiguracionAutomatica.IdProveedor;
							this.IdProducto = furConfiguracionAutomatica.IdProducto;
							this.Cantidad = furConfiguracionAutomatica.Cantidad;
							this.IdProductoPresentacion = furConfiguracionAutomatica.IdProductoPresentacion;
							this.Descripcion = furConfiguracionAutomatica.Descripcion;
							this.FechaLimite = fechaFur;
							this.PrecioUnitarioMonedaOrigen = detalleFur.PrecioOrigen;
							this.PrecioUnitarioDolares = detalleFur.PrecioDolares;
							this.IdMonedaProveedor = detalleFur.IdMoneda;
							this.IdFurFaseAprobacion1 = 1;
							this.PrecioTotalMonedaOrigen = Convert.ToDecimal(detalleFur.PrecioOrigen * furConfiguracionAutomatica.Cantidad);
							this.MontoProyectado = this.PrecioTotalMonedaOrigen;
							this.Monto = this.PrecioTotalMonedaOrigen;
							this.PrecioTotalDolares = Convert.ToDecimal(detalleFur.PrecioDolares * furConfiguracionAutomatica.Cantidad);
							this.PagoMonedaOrigen = detalleFur.PrecioOrigen * furConfiguracionAutomatica.Cantidad;
							this.PagoDolares = detalleFur.PrecioDolares * furConfiguracionAutomatica.Cantidad;
							this.Cancelado = false;
							this.Antiguo = 0;
							this.IdMonedaPagoReal = furConfiguracionAutomatica.IdMonedaPagoReal;
							this.IdMonedaPagoRealizado = detalleFur.IdMoneda;
							this.EstadoAprobadoObservado = false;
							this.OcupadoSolicitud = false;
							this.OcupadoRendicion = false;
							this.Estado = true;
							this.UsuarioCreacion = "SYSTEM-AUTOMATIC";
							this.UsuarioModificacion = "SYSTEM-AUTOMATIC";
							this.FechaCreacion = DateTime.Now;
							this.FechaModificacion = DateTime.Now;
							estado = true;
						}
					}
				}
				return estado;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}

		public int obtenerNumeroSemana(DateTime fecha)
		{
			var d = fecha;
			CultureInfo cul = CultureInfo.CurrentCulture;

			var firstDayWeek = cul.Calendar.GetWeekOfYear(
				d,
				CalendarWeekRule.FirstDay,
				DayOfWeek.Monday);

			int weekNum = cul.Calendar.GetWeekOfYear(
				d,
				CalendarWeekRule.FirstDay,
				DayOfWeek.Monday);

			return weekNum;
		}

        public bool ActivarFurNoEjecutado(FiltroDTO json,  integraDBContext context)
        {
            try
            {// Se verifica si es jefe de Finanzas para la aprobacion Final de Furs,
               
                FurRepositorio repFurRep = new FurRepositorio(context);
                FurBO fur = new FurBO();
                fur = repFurRep.FirstById(json.Id);
                fur.IdFurFaseAprobacion1 = ValorEstatico.IdFurAprobadoPorJefeFinanzas;
                fur.Observaciones = "Fur Activado por Finanzas, antes en Estado Aprobado No Ejecutado";
                fur.FechaModificacion = DateTime.Now;
                fur.UsuarioModificacion = json.Nombre;

                repFurRep.Update(fur);

                return true;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
