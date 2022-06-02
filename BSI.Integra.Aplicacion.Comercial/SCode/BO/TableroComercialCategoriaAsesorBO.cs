using BSI.Integra.Aplicacion.Base.BO;
using BSI.Integra.Aplicacion.Base.Classes;
using BSI.Integra.Aplicacion.Comercial.Repositorio;
using BSI.Integra.Persistencia.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.Comercial.BO
{
    /// BO: TableroComercialCategoriaAsesorBO
    /// Autor: Jashin Salazar.
    /// Fecha: 31/05/2021
    /// <summary>
    /// BO para informacion del tablero comercial categoria asesor
    /// </summary>

    public class TableroComercialCategoriaAsesorBO : BaseBO
    {
        /// Propiedades	                        Significado
        /// -----------	                        ------------
        /// Nombre		                        Nombre de la categoria del asesor
        /// MontoVenta                          Monto de venta de la categoria
        /// IdMonedaVenta		                Id de moneda de venta
        /// IdTableroComercialUnidadVenta		Id de la unidad del tablero comercial
        /// MontoPremio		                    Monto de premio de la categoria
        /// IdMonedaPremio                      Id de moneda del premio
        /// VisualizarMonedaLocal               Visualizar en moneda local

        public string Nombre { get; set; }
        public decimal MontoVenta { get; set; }
        public int IdMonedaVenta { get; set; }
        public int IdTableroComercialUnidadVenta { get; set; }
        public decimal MontoPremio { get; set; }
        public int IdMonedaPremio { get; set; }
        public bool VisualizarMonedaLocal { get; set; }

        private TableroComercialCategoriaAsesorRepositorio _repCategoriaAsesor;

        public TableroComercialCategoriaAsesorBO()
        {
            ActualesErrores = new Dictionary<string, List<ErrorInfo>>();
        }

        public TableroComercialCategoriaAsesorBO(integraDBContext contexto)
        {
            ActualesErrores = new Dictionary<string, List<ErrorInfo>>();
            _repCategoriaAsesor = new TableroComercialCategoriaAsesorRepositorio(contexto);
        }

        public TableroComercialCategoriaAsesorBO(int id, integraDBContext contexto)
        {
            ActualesErrores = new Dictionary<string, List<ErrorInfo>>();
            _repCategoriaAsesor = new TableroComercialCategoriaAsesorRepositorio(contexto);
            var categoriaAsesor = _repCategoriaAsesor.FirstById(id);
            this.Id = categoriaAsesor.Id;
            this.Nombre = categoriaAsesor.Nombre;
            this.MontoVenta = categoriaAsesor.MontoVenta;
            this.IdMonedaVenta = categoriaAsesor.IdMonedaVenta;
            this.IdTableroComercialUnidadVenta = categoriaAsesor.IdTableroComercialUnidadVenta;
            this.MontoPremio = categoriaAsesor.MontoPremio;
            this.IdMonedaPremio = categoriaAsesor.IdMonedaPremio;
            this.VisualizarMonedaLocal = categoriaAsesor.VisualizarMonedaLocal;
            this.Estado = categoriaAsesor.Estado;
            this.FechaCreacion = categoriaAsesor.FechaCreacion;
            this.FechaModificacion = categoriaAsesor.FechaModificacion;
            this.UsuarioCreacion = categoriaAsesor.UsuarioCreacion;
            this.UsuarioModificacion = categoriaAsesor.UsuarioModificacion;
            this.RowVersion = categoriaAsesor.RowVersion;
        }

    }
}
