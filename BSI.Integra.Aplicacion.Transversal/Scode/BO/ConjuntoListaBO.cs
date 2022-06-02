using BSI.Integra.Aplicacion.Base.BO;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Servicios;
using BSI.Integra.Aplicacion.Servicios.DTOs;
using BSI.Integra.Aplicacion.Transversal.Helper;
using BSI.Integra.Aplicacion.Transversal.Repositorio;
using BSI.Integra.Persistencia.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;

namespace BSI.Integra.Aplicacion.Transversal.BO
{
    public partial class ConjuntoListaBO : BaseBO
    {
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public int IdCategoriaObjetoFiltro { get; set; }
        public int IdFiltroSegmento { get; set; }
        public byte NroListasRepeticionContacto { get; set; }
        public bool? ConsiderarYaEnviados { get; set; }
        public int? IdMigracion { get; set; }
        public DateTime? HoraEjecucion { get; set; }

        public List<ConjuntoListaDetalleBO> ListaConjuntoListaDetalle { get; set; }

        //repositorios
        private readonly integraDBContext _integraDBContext;

        private ConjuntoListaRepositorio _repConjuntoLista;
        private ConjuntoListaDetalleRepositorio _repConjuntoListaDetalle;
        private FiltroSegmentoRepositorio _repFiltroSegmento;
        private ConjuntoListaDetalleValorRepositorio _repConjuntoListaDetalleValor;
        private ConjuntoListaResultadoRepositorio _repConjuntoListaResultado;

        //BOs
        private FiltroSegmentoBO _filtroSegmento;

        public ConjuntoListaBO() {
            _integraDBContext = new integraDBContext();
            ListaConjuntoListaDetalle = new List<ConjuntoListaDetalleBO>();
            ListaConjuntoListaDetalle = new List<ConjuntoListaDetalleBO>();
            _repConjuntoLista = new ConjuntoListaRepositorio(_integraDBContext);
            _repFiltroSegmento = new FiltroSegmentoRepositorio(_integraDBContext);
            _filtroSegmento = new FiltroSegmentoBO(_integraDBContext);
            _repConjuntoListaDetalle = new ConjuntoListaDetalleRepositorio(_integraDBContext);
            _repConjuntoListaDetalleValor = new ConjuntoListaDetalleValorRepositorio(_integraDBContext);
            _repConjuntoListaResultado = new ConjuntoListaResultadoRepositorio(_integraDBContext);
        }

        public ConjuntoListaBO(integraDBContext integraDBContext) {
            _integraDBContext = integraDBContext;

            ListaConjuntoListaDetalle = new List<ConjuntoListaDetalleBO>();
            _repConjuntoLista = new ConjuntoListaRepositorio(_integraDBContext);
            _repFiltroSegmento = new FiltroSegmentoRepositorio(_integraDBContext);
            _filtroSegmento = new FiltroSegmentoBO(_integraDBContext);
            _repConjuntoListaDetalle = new ConjuntoListaDetalleRepositorio(_integraDBContext);
            _repConjuntoListaDetalleValor = new ConjuntoListaDetalleValorRepositorio(_integraDBContext);
            _repConjuntoListaResultado = new ConjuntoListaResultadoRepositorio(_integraDBContext);
        }

        public string GenerarPlantillaNotificacionProcesamientoCorrecto(List<MensajeProcesarDTO> listas)
        {

            var texto = @"
                <HTML><STYLE TYPE='text/css'><!-@page { margin: 1in }P { margin-bottom: 0.08in; line-height: 107%; text-align: left }P.western { font-family: 'Arial', sans-serif; font-size: 16pt; so-language: en-US }P.cjk { font-family: 'Calibri', sans-serif; font-size: 11pt; so-language: en-US }P.ctl { font-family: 'Calibri', sans-serif; font-size: 11pt; so-language: ar-SA }A:link { color: #0563c1 }A.ctl:link { font-family: 'Times New Roman', serif }	--></STYLE>
                <head> <style type='text/css' media='screen'> body{font-family: Arial, sans-serif; font-size: 12pt; line-height: 20px;} </style> </head>";
            texto += $@"
                <BODY LANG='en-US' LINK='#0563c1' DIR='LTR' STYLE='border: none; padding: 0in'>";
            foreach (var item in listas)
            {
                texto += $@"<p style='font-size:10pt;'> { item.Nombre }</p>
                <table style='border: 1px solid #e6e6e6;border-collapse:collapse' border='' cellspacing='0' cellpadding='2'>
                    <tbody>
                        <tr>
                            <th style='border: 1px solid #e6e6e6' bgcolor='#FAFAFA'>Nombre Conjunto Lista</th>
                            <th style='border: 1px solid #e6e6e6' bgcolor='#FAFAFA'>Lista</th>
                            <th style='border: 1px solid #e6e6e6' bgcolor='#FAFAFA'>Nro intentos</th>
                        </tr>";
                foreach (var detalle in item.ListaDetalle)
                {
                    texto += $@"
                        <tr>
                            <th style='border: 1px solid #e6e6e6; font-weight: 100;'> {detalle.NombreCampania} </th>
                            <th style='border: 1px solid #e6e6e6; font-weight: 100;'> {detalle.NombreLista} </th>
                            <th style='border: 1px solid #e6e6e6; font-weight: 100;'> {detalle.NroIntentos} </th>
                        </tr>";
                }
                texto += $@"
                    </tbody>
                </table> ";
            }
            texto += "</BODY></HTML>";
            return texto;
        }

        /// <summary>
        /// Obtiene los resultados del conjunto de lista
        /// </summary>
        /// <returns></returns>
        public List<ConjuntoListaCompuestoDTO> ObtenerResultados()
        {
            try
            {
                if (!_repFiltroSegmento.Exist(this.IdFiltroSegmento))
                {
                    throw new Exception("No existe filtro segmento");
                }
                _filtroSegmento = _repFiltroSegmento.FirstById(this.IdFiltroSegmento);
                return _repConjuntoLista.ObtenerResultado(this.Id, _filtroSegmento.IdFiltroSegmentoTipoContacto.Value);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }


        public class ListResult{
            public List<ConjuntoListaDetalleBO> Listado { get; set; }
            public bool EsCorrecto { get; set; }
        }
        /// <summary>
        /// Ejecuta las listas 
        /// </summary>
        /// <returns></returns>
        public List<ListResult> EjecutarConjuntoLista()
        {
            try
            {
                // Llenamos los valores de conjunto lista
                var conjuntoListaActual = _repConjuntoLista.GetBy(x => x.Id == this.Id).FirstOrDefault();

                this.IdFiltroSegmento = conjuntoListaActual.IdFiltroSegmento;
                this.NroListasRepeticionContacto = conjuntoListaActual.NroListasRepeticionContacto;
                this.ConsiderarYaEnviados = conjuntoListaActual.ConsiderarYaEnviados;
                this.HoraEjecucion = conjuntoListaActual.HoraEjecucion;
                
                var conjuntoListaDetalleEjecutar = _repConjuntoListaDetalle.GetBy(x => x.IdConjuntoLista == this.Id).ToList();

                conjuntoListaDetalleEjecutar = conjuntoListaDetalleEjecutar.OrderBy(x => x.Prioridad).ToList();
                _repConjuntoListaResultado.EliminarPorConjuntoLista(this.Id, this.UsuarioCreacion);

                var filtroEjecutadoCorrectamente = false;

                var _lista = new List<ListResult>();
                var listaCorrecta = new List<ConjuntoListaDetalleBO>();
                var listaError = new List<ConjuntoListaDetalleBO>();

                var nroEjecucion = _repConjuntoLista.ObtenerProximoNroEjecucion(this.Id);

                foreach (var item in conjuntoListaDetalleEjecutar)
                {
                    //eliminamos los detalles antiguos
                    filtroEjecutadoCorrectamente = false;

                    _filtroSegmento.Id = this.IdFiltroSegmento;
                    _filtroSegmento.UsuarioCreacion = this.UsuarioCreacion;
                    //_filtroSegmento.ObtenerFiltroValorPorIdFiltroSegmento();

                    var listaArea = _repConjuntoListaDetalleValor.GetBy
                        (   x => x.IdConjuntoListaDetalle == item.Id && x.IdCategoriaObjetoFiltro == ValorEstatico.IdCategoriaObjetoFiltroArea,
                            x => new FiltroSegmentoValorTipoDTO { Valor = x.Valor, IdCategoriaObjetoFiltro = x.IdCategoriaObjetoFiltro } 
                        ).ToList();

                    var listaSubArea = _repConjuntoListaDetalleValor.GetBy
                        (x => x.IdConjuntoListaDetalle == item.Id && x.IdCategoriaObjetoFiltro == ValorEstatico.IdCategoriaObjetoFiltroSubArea,
                            x => new FiltroSegmentoValorTipoDTO { Valor = x.Valor, IdCategoriaObjetoFiltro = x.IdCategoriaObjetoFiltro }
                        ).ToList();

                    var listaProgramaGeneral = _repConjuntoListaDetalleValor.GetBy
                        (x => x.IdConjuntoListaDetalle == item.Id && x.IdCategoriaObjetoFiltro == ValorEstatico.IdCategoriaObjetoFiltroProgramaGeneral,
                            x => new FiltroSegmentoValorTipoDTO { Valor = x.Valor, IdCategoriaObjetoFiltro = x.IdCategoriaObjetoFiltro }
                        ).ToList();

                    //while (!filtroEjecutadoCorrectamente) {
                    while (filtroEjecutadoCorrectamente is false)
                    {
                        try
                        {
                            _filtroSegmento.EjecutarFiltroSegmentoConjuntoLista(listaArea, listaSubArea, listaProgramaGeneral, item.Id, this.NroListasRepeticionContacto, nroEjecucion, this.ConsiderarYaEnviados.GetValueOrDefault());
                            listaCorrecta.Add(item);
                            filtroEjecutadoCorrectamente = true;
                        }
                        catch (Exception e)
                        {
                            listaError.Add(item);
                            filtroEjecutadoCorrectamente = false;
                        }
                    }
                    ///Ejecutamos el calculo
                }
                _lista.Add(new ListResult() { Listado = listaCorrecta, EsCorrecto = true });
                _lista.Add(new ListResult() { Listado = listaError, EsCorrecto = false });

                return _lista;
                //return true;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }


        /// <summary>
        /// Ejecuta la lista de manera individual
        /// </summary>
        /// <param name="idConjuntoListaDetalle"></param>
        /// <returns></returns>
        public bool EjecutarConjuntoLista(int idConjuntoListaDetalle)
        {
            try
            {
                //llenamos los valores de conjunto lista
                var conjuntoListaActual = _repConjuntoLista.GetBy(x => x.Id == this.Id).FirstOrDefault();


                this.IdFiltroSegmento = conjuntoListaActual.IdFiltroSegmento;
                this.NroListasRepeticionContacto = conjuntoListaActual.NroListasRepeticionContacto;

                var conjuntoListaDetalleEjecutar = _repConjuntoListaDetalle.GetBy(x => x.IdConjuntoLista == this.Id && x.Id == idConjuntoListaDetalle).ToList();

                conjuntoListaDetalleEjecutar = conjuntoListaDetalleEjecutar.OrderBy(x => x.Prioridad).ToList();
                //_repConjuntoListaResultado.EliminarPorConjuntoLista(this.Id);

                var nroEjecucion = _repConjuntoLista.ObtenerProximoNroEjecucion(this.Id);
                foreach (var item in conjuntoListaDetalleEjecutar)
                {
                    //eliminamos los detalles antiguos si el primero
                    if (item.Prioridad == 1)
                    {
                        _repConjuntoListaResultado.EliminarPorConjuntoLista(this.Id, "SYSTEM");
                    }

                    _filtroSegmento.Id = this.IdFiltroSegmento;
                    //_filtroSegmento.ObtenerFiltroValorPorIdFiltroSegmento();

                    var listaArea = _repConjuntoListaDetalleValor.GetBy
                        (x => x.IdConjuntoListaDetalle == item.Id && x.IdCategoriaObjetoFiltro == ValorEstatico.IdCategoriaObjetoFiltroArea,
                            x => new FiltroSegmentoValorTipoDTO { Valor = x.Valor, IdCategoriaObjetoFiltro = x.IdCategoriaObjetoFiltro }
                        ).ToList();

                    var listaSubArea = _repConjuntoListaDetalleValor.GetBy
                        (x => x.IdConjuntoListaDetalle == item.Id && x.IdCategoriaObjetoFiltro == ValorEstatico.IdCategoriaObjetoFiltroSubArea,
                            x => new FiltroSegmentoValorTipoDTO { Valor = x.Valor, IdCategoriaObjetoFiltro = x.IdCategoriaObjetoFiltro }
                        ).ToList();

                    var listaProgramaGeneral = _repConjuntoListaDetalleValor.GetBy
                        (x => x.IdConjuntoListaDetalle == item.Id && x.IdCategoriaObjetoFiltro == ValorEstatico.IdCategoriaObjetoFiltroProgramaGeneral,
                            x => new FiltroSegmentoValorTipoDTO { Valor = x.Valor, IdCategoriaObjetoFiltro = x.IdCategoriaObjetoFiltro }
                        ).ToList();

                    ///Ejecutamos el calculo
                    _filtroSegmento.EjecutarFiltroSegmentoConjuntoLista(listaArea, listaSubArea, listaProgramaGeneral, item.Id, this.NroListasRepeticionContacto, nroEjecucion, this.ConsiderarYaEnviados.GetValueOrDefault());
                }
                return true;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insertar(ConjuntoListaDetalleCompletoDTO ConjuntoListaDetalleCompleto)
        {
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    var conjuntoLista = new ConjuntoListaBO()
                    {
                        Nombre = ConjuntoListaDetalleCompleto.ConjuntoLista.Nombre,
                        Descripcion = ConjuntoListaDetalleCompleto.ConjuntoLista.Descripcion,
                        IdCategoriaObjetoFiltro = ConjuntoListaDetalleCompleto.ConjuntoLista.IdCategoriaObjetoFiltro,
                        IdFiltroSegmento = ConjuntoListaDetalleCompleto.ConjuntoLista.IdFiltroSegmento,
                        NroListasRepeticionContacto = ConjuntoListaDetalleCompleto.ConjuntoLista.NroListasRepeticionContacto,
                        ConsiderarYaEnviados = ConjuntoListaDetalleCompleto.ConjuntoLista.ConsiderarYaEnviados,
                        UsuarioCreacion = ConjuntoListaDetalleCompleto.NombreUsuario,
                        UsuarioModificacion = ConjuntoListaDetalleCompleto.NombreUsuario,
                        FechaCreacion = DateTime.Now,
                        FechaModificacion = DateTime.Now,
                        Estado = true
                    };

                    foreach (var item in ConjuntoListaDetalleCompleto.ConjuntoListaDetalle)
                    {
                        var conjuntoListaDetalle = new ConjuntoListaDetalleBO()
                        {
                            Nombre = item.Nombre,
                            Descripcion = item.Descripcion,
                            Prioridad = item.Prioridad,
                            UsuarioCreacion = ConjuntoListaDetalleCompleto.NombreUsuario,
                            UsuarioModificacion = ConjuntoListaDetalleCompleto.NombreUsuario,
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now,
                            Estado = true
                        };

                        ///llenamos hijos
                        var conjuntoListaDetalleValor = new List<ConjuntoListaDetalleValorBO>();

                        foreach (var area in item.ListaArea)
                        {
                            var _new = new ConjuntoListaDetalleValorBO
                            {
                                IdCategoriaObjetoFiltro = ValorEstatico.IdCategoriaObjetoFiltroArea,
                                Valor = area.Valor,
                                Estado = true,
                                UsuarioCreacion = ConjuntoListaDetalleCompleto.NombreUsuario,
                                UsuarioModificacion = ConjuntoListaDetalleCompleto.NombreUsuario,
                                FechaCreacion = DateTime.Now,
                                FechaModificacion = DateTime.Now
                            };
                            conjuntoListaDetalleValor.Add(_new);
                        }
                        foreach (var subArea in item.ListaSubArea)
                        {
                            var _new = new ConjuntoListaDetalleValorBO
                            {
                                IdCategoriaObjetoFiltro = ValorEstatico.IdCategoriaObjetoFiltroSubArea,
                                Valor = subArea.Valor,
                                Estado = true,
                                UsuarioCreacion = ConjuntoListaDetalleCompleto.NombreUsuario,
                                UsuarioModificacion = ConjuntoListaDetalleCompleto.NombreUsuario,
                                FechaCreacion = DateTime.Now,
                                FechaModificacion = DateTime.Now
                            };
                            conjuntoListaDetalleValor.Add(_new);
                        }
                        foreach (var pGeneral in item.ListaProgramaGeneral)
                        {
                            var _new = new ConjuntoListaDetalleValorBO
                            {
                                IdCategoriaObjetoFiltro = ValorEstatico.IdCategoriaObjetoFiltroProgramaGeneral,
                                Valor = pGeneral.Valor,
                                Estado = true,
                                UsuarioCreacion = ConjuntoListaDetalleCompleto.NombreUsuario,
                                UsuarioModificacion = ConjuntoListaDetalleCompleto.NombreUsuario,
                                FechaCreacion = DateTime.Now,
                                FechaModificacion = DateTime.Now
                            };
                            conjuntoListaDetalleValor.Add(_new);
                        }
                        //hijos de detalle
                        conjuntoListaDetalle.ListaConjuntoListaDetalleValor.AddRange(conjuntoListaDetalleValor);
                        conjuntoLista.ListaConjuntoListaDetalle.Add(conjuntoListaDetalle);
                    }
                    _repConjuntoLista.Insert(conjuntoLista);
                    scope.Complete();
                }
                return true;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// Obtiene el detalle de todo el conjunto lista
        /// </summary>
        /// <returns></returns>
        public ConjuntoListaDetalleCompletoDTO ObtenerDetalle() {
            try
            {
                ConjuntoListaDetalleRepositorio repConjuntoListaDetalle = new ConjuntoListaDetalleRepositorio(_integraDBContext);
                ConjuntoListaDetalleValorRepositorio repConjuntoListaDetalleValor = new ConjuntoListaDetalleValorRepositorio(_integraDBContext);

                var listaDetalle = repConjuntoListaDetalle.Obtener(this.Id);

                foreach (var item in listaDetalle)
                {
                    var conjuntoListaDetalleValor = repConjuntoListaDetalleValor.ObtenerConjuntoListaDetalleValor(item.Id);
                    item.ListaArea = conjuntoListaDetalleValor.Where(x => x.IdCategoriaObjetoFiltro == ValorEstatico.IdCategoriaObjetoFiltroArea).ToList();
                    item.ListaSubArea = conjuntoListaDetalleValor.Where(x => x.IdCategoriaObjetoFiltro == ValorEstatico.IdCategoriaObjetoFiltroSubArea).ToList();
                    item.ListaProgramaGeneral = conjuntoListaDetalleValor.Where(x => x.IdCategoriaObjetoFiltro == ValorEstatico.IdCategoriaObjetoFiltroProgramaGeneral).ToList();
                }

                var conjuntoListaDetalleCompleto = new ConjuntoListaDetalleCompletoDTO()
                {
                    ConjuntoLista = _repConjuntoLista.Obtener(this.Id),
                    NombreUsuario = "",
                    ConjuntoListaDetalle = listaDetalle
                };
                return conjuntoListaDetalleCompleto;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
