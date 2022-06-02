using BSI.Integra.Aplicacion.Base.BO;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Planificacion.Repositorio;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Aplicacion.Transversal.Repositorio;
using BSI.Integra.Persistencia.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;

namespace BSI.Integra.Aplicacion.Planificacion.BO
{
    /// BO: Planificacion/ProgramaGeneralPuntoCorte
    /// Autor: Gian Miranda
    /// Fecha: 06/07/2021
    /// <summary>
    /// BO para la logica de los puntos de corte para los programas generales
    /// </summary>
    public class ProgramaGeneralPuntoCorteBO : BaseBO
    {
        /// Propiedades	                        Significado
        /// -----------	                        ------------
        /// IdProgramaGeneral                   Id del programa general (PK de la tabla pla.T_PGeneral)
        /// PuntoCorteMedia                     Punto de corte para media
        /// PuntoCorteAlta                      Punto de corte para alta
        /// PuntoCorteMuyAlta                   Punto de corte para muy alta
        /// RowVersion                          Version del registro
        /// IdMigracion                         Id de migracion de V3 (nullable)

        public int? IdProgramaGeneral { get; set; }
        public decimal? PuntoCorteMedia { get; set; }
        public decimal? PuntoCorteAlta { get; set; }
        public decimal? PuntoCorteMuyAlta { get; set; }
        public byte[] RowVersion { get; set; }
        public int? IdMigracion { get; set; }

        private readonly integraDBContext _integraDBContext;
        private readonly ProgramaGeneralPuntoCorteRepositorio _repProgramaGeneralPuntoCorte;
        private readonly PgeneralRepositorio _repPgeneral;
        private readonly PgeneralBO PGeneralBO;

        public ProgramaGeneralPuntoCorteBO()
        {
            _repProgramaGeneralPuntoCorte = new ProgramaGeneralPuntoCorteRepositorio();
            _repPgeneral = new PgeneralRepositorio();
        }

        public ProgramaGeneralPuntoCorteBO(integraDBContext integraDBContext)
        {
            _integraDBContext = integraDBContext;

            _repProgramaGeneralPuntoCorte = new ProgramaGeneralPuntoCorteRepositorio(_integraDBContext);
            _repPgeneral = new PgeneralRepositorio(_integraDBContext);
        }

        /// <summary>
        /// OBtener Por Id de la configuracion del punto de corte
        /// </summary>
        /// <param name="idProgramaGeneralPuntoCorte">Id del programa de punto de corte (PK de la tabla pla.T_PGeneral)</param>
        /// <returns>Objeto de clase ProgramaGeneralPuntoCorteBO</returns>
        public ProgramaGeneralPuntoCorteBO ObtenerPorId(int idProgramaGeneralPuntoCorte)
        {
            try
            {
                if (_repProgramaGeneralPuntoCorte.Exist(idProgramaGeneralPuntoCorte))
                {
                    return _repProgramaGeneralPuntoCorte.FirstById(idProgramaGeneralPuntoCorte);
                }
                else
                {
                    throw new Exception("La configuracion ingresada a actualizar no existe");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Valida el PGeneral por el IdPGeneral
        /// </summary>
        /// <param name="idPGeneral">Id del PGeneral (PK de la tabla pla.T_PGeneral)</param>
        /// <returns>Bool</returns>
        public bool ValidarPGeneral(int idPGeneral)
        {
            try
            {
                if (_repPgeneral.Exist(x => x.Id == idPGeneral))
                    return true;
                else
                    throw new Exception("El programa general no existe");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Actualiza el programa general con el punto de corte de forma individual
        /// </summary>
        /// <param name="objetoActualizar">Objeto de clase ProgramaGeneralPuntoCorteDTO</param>
        /// <returns>Bool</returns>
        public bool ActualizarProgramaGeneralPuntoCorte(ProgramaGeneralPuntoCorteDTO objetoActualizar)
        {
            try
            {
                if (!objetoActualizar.IdProgramaGeneral.HasValue)
                    throw new Exception("El programa general no es valido");

                ProgramaGeneralPuntoCorteBO programaGeneralPuntoCorteNuevo = new ProgramaGeneralPuntoCorteBO(_integraDBContext);
                ProgramaGeneralPuntoCorteDetalleBO programaGeneralPuntoCorteDetalleNuevo = new ProgramaGeneralPuntoCorteDetalleBO();
                ProgramaGeneralPuntoCorteDetalleRepositorio _repProgramaGeneralPuntoCorteDetalle = new ProgramaGeneralPuntoCorteDetalleRepositorio(_integraDBContext);

                programaGeneralPuntoCorteNuevo.IdProgramaGeneral = objetoActualizar.IdProgramaGeneral;
                programaGeneralPuntoCorteNuevo.PuntoCorteMedia = objetoActualizar.PuntoCorteMedia;
                programaGeneralPuntoCorteNuevo.PuntoCorteAlta = objetoActualizar.PuntoCorteAlta;
                programaGeneralPuntoCorteNuevo.PuntoCorteMuyAlta = objetoActualizar.PuntoCorteMuyAlta;
                programaGeneralPuntoCorteNuevo.UsuarioModificacion = objetoActualizar.Usuario;
                programaGeneralPuntoCorteNuevo.FechaModificacion = DateTime.Now;
                programaGeneralPuntoCorteNuevo.FechaCreacion = DateTime.Now;
                programaGeneralPuntoCorteNuevo.UsuarioCreacion = objetoActualizar.Usuario;
                programaGeneralPuntoCorteNuevo.Estado = true;

                List<int> listaConfiguracionMismoPGeneral = _repProgramaGeneralPuntoCorte.GetBy(x => x.IdProgramaGeneral == objetoActualizar.IdProgramaGeneral.Value).Select(s => s.Id).ToList();
                List<int> listaDetalle = new List<int>();
                if (listaConfiguracionMismoPGeneral.Any()) { 
                    for(var i=0; i< listaConfiguracionMismoPGeneral.Count(); i++)
                    {
                        listaDetalle = _repProgramaGeneralPuntoCorteDetalle.GetBy(x => x.IdProgramaGeneralPuntoCorte == listaConfiguracionMismoPGeneral.ElementAt(0)).Select(s => s.Id).ToList();
                        if (listaDetalle.Any())
                        {
                            _repProgramaGeneralPuntoCorteDetalle.Delete(listaDetalle, objetoActualizar.Usuario);
                        }
                    }
                }

                using (TransactionScope scope = new TransactionScope())
                {
                    if (listaConfiguracionMismoPGeneral.Any())
                        _repProgramaGeneralPuntoCorte.Delete(listaConfiguracionMismoPGeneral, objetoActualizar.Usuario);


                    _repProgramaGeneralPuntoCorte.Insert(programaGeneralPuntoCorteNuevo);


                    foreach (var item in objetoActualizar.ListaPuntoCorteMedia)
                    {
                        programaGeneralPuntoCorteDetalleNuevo.IdProgramaGeneralPuntoCorte = programaGeneralPuntoCorteNuevo.Id;
                        programaGeneralPuntoCorteDetalleNuevo.IdPuntoCorte = 1;
                        programaGeneralPuntoCorteDetalleNuevo.Tipo = item.Tipo;
                        programaGeneralPuntoCorteDetalleNuevo.Descripcion = item.Descripcion;
                        programaGeneralPuntoCorteDetalleNuevo.ValorMinimo = item.ValorMinimo;
                        programaGeneralPuntoCorteDetalleNuevo.ValorMaximo = item.ValorMaximo;
                        programaGeneralPuntoCorteDetalleNuevo.Estado = true;
                        programaGeneralPuntoCorteDetalleNuevo.UsuarioCreacion = objetoActualizar.Usuario;
                        programaGeneralPuntoCorteDetalleNuevo.UsuarioModificacion = objetoActualizar.Usuario;
                        programaGeneralPuntoCorteDetalleNuevo.FechaCreacion = DateTime.Now;
                        programaGeneralPuntoCorteDetalleNuevo.FechaModificacion = DateTime.Now;
                        _repProgramaGeneralPuntoCorteDetalle.Insert(programaGeneralPuntoCorteDetalleNuevo);
                        programaGeneralPuntoCorteDetalleNuevo.Id = 0;
                    }
                    foreach (var item in objetoActualizar.ListaPuntoCorteAlta)
                    {
                        programaGeneralPuntoCorteDetalleNuevo.IdProgramaGeneralPuntoCorte = programaGeneralPuntoCorteNuevo.Id;
                        programaGeneralPuntoCorteDetalleNuevo.IdPuntoCorte = 2;
                        programaGeneralPuntoCorteDetalleNuevo.Tipo = item.Tipo;
                        programaGeneralPuntoCorteDetalleNuevo.Descripcion = item.Descripcion;
                        programaGeneralPuntoCorteDetalleNuevo.ValorMinimo = item.ValorMinimo;
                        programaGeneralPuntoCorteDetalleNuevo.ValorMaximo = item.ValorMaximo;
                        programaGeneralPuntoCorteDetalleNuevo.Estado = true;
                        programaGeneralPuntoCorteDetalleNuevo.UsuarioCreacion = objetoActualizar.Usuario;
                        programaGeneralPuntoCorteDetalleNuevo.UsuarioModificacion = objetoActualizar.Usuario;
                        programaGeneralPuntoCorteDetalleNuevo.FechaCreacion = DateTime.Now;
                        programaGeneralPuntoCorteDetalleNuevo.FechaModificacion = DateTime.Now;
                        _repProgramaGeneralPuntoCorteDetalle.Insert(programaGeneralPuntoCorteDetalleNuevo);
                        programaGeneralPuntoCorteDetalleNuevo.Id = 0;
                    }
                    foreach (var item in objetoActualizar.ListaPuntoCorteMuyAlta)
                    {
                        programaGeneralPuntoCorteDetalleNuevo.IdProgramaGeneralPuntoCorte = programaGeneralPuntoCorteNuevo.Id;
                        programaGeneralPuntoCorteDetalleNuevo.IdPuntoCorte = 3;
                        programaGeneralPuntoCorteDetalleNuevo.Tipo = item.Tipo;
                        programaGeneralPuntoCorteDetalleNuevo.Descripcion = item.Descripcion;
                        programaGeneralPuntoCorteDetalleNuevo.ValorMinimo = item.ValorMinimo;
                        programaGeneralPuntoCorteDetalleNuevo.ValorMaximo = item.ValorMaximo;
                        programaGeneralPuntoCorteDetalleNuevo.Estado = true;
                        programaGeneralPuntoCorteDetalleNuevo.UsuarioCreacion = objetoActualizar.Usuario;
                        programaGeneralPuntoCorteDetalleNuevo.UsuarioModificacion = objetoActualizar.Usuario;
                        programaGeneralPuntoCorteDetalleNuevo.FechaCreacion = DateTime.Now;
                        programaGeneralPuntoCorteDetalleNuevo.FechaModificacion = DateTime.Now;
                        _repProgramaGeneralPuntoCorteDetalle.Insert(programaGeneralPuntoCorteDetalleNuevo);
                        programaGeneralPuntoCorteDetalleNuevo.Id = 0;
                    }
                    scope.Complete();
                }                

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Actualiza el programa general con el punto de corte masivo
        /// </summary>
        /// <param name="listaActualizar">Objeto de clase ProgramaGeneralPuntoCorteMasivoDTO</param>
        /// <returns>Bool</returns>
        public bool ActualizarProgramaGeneralPuntoCorteMasivo(ProgramaGeneralPuntoCorteMasivoDTO listaActualizar)
        {
            try
            {
                bool resultadoPrevalidacion = PrevalidarLista(listaActualizar);

                using (TransactionScope scope = new TransactionScope())
                {
                    bool resultadoEliminacionAnterior = EliminarRegistrosAntiguos(listaActualizar.ListaIdPGeneral, listaActualizar.Usuario);
                    bool resultadoInsercionNuevosRegistros = InsertarPuntoCorteMasivo(listaActualizar);
                    scope.Complete();
                }

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Prevalida la lista enviada
        /// </summary>
        /// <param name="listaActualizar">Objeto de clase ProgramaGeneralPuntoCorteMasivoDTO</param>
        /// <returns>Bool</returns>
        public bool PrevalidarLista(ProgramaGeneralPuntoCorteMasivoDTO listaActualizar)
        {
            try
            {
                if (listaActualizar.ListaIdPGeneral.Where(x => x >= 0).Count() == listaActualizar.ListaIdPGeneral.Count())
                    return true;
                else
                    throw new Exception("Falta el programa general de un elemento de la lista");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Elimina configuraciones de la lista de PGeneral enviados como parametros
        /// </summary>
        /// <param name="listaPGeneralAEliminarConfiguracion">Lista de los Ids de PGeneral a eliminar la configuracion</param>
        /// <param name="usuario">Usuario responsable del eliminado</param>
        /// <returns>Bool</returns>
        public bool EliminarRegistrosAntiguos(List<int> listaPGeneralAEliminarConfiguracion, string usuario)
        {
            try
            {
                ProgramaGeneralPuntoCorteDetalleRepositorio _repProgramaGeneralPuntoCorteDetalle = new ProgramaGeneralPuntoCorteDetalleRepositorio(_integraDBContext);
                List<int> listaIdConfiguracionMismoPGeneral = _repProgramaGeneralPuntoCorte.GetBy(x => listaPGeneralAEliminarConfiguracion.Contains(x.IdProgramaGeneral.Value)).Select(s => s.Id).ToList();

                if (listaIdConfiguracionMismoPGeneral.Any())
                    _repProgramaGeneralPuntoCorte.Delete(listaIdConfiguracionMismoPGeneral, usuario);

                List<int> listaDetalle = new List<int>();
                if (listaIdConfiguracionMismoPGeneral.Any())
                {
                    for (var i = 0; i < listaIdConfiguracionMismoPGeneral.Count(); i++)
                    {
                        listaDetalle = _repProgramaGeneralPuntoCorteDetalle.GetBy(x => x.IdProgramaGeneralPuntoCorte == listaIdConfiguracionMismoPGeneral.ElementAt(i)).Select(s => s.Id).ToList();
                        if (listaDetalle.Any())
                        {
                            _repProgramaGeneralPuntoCorteDetalle.Delete(listaDetalle, usuario);
                        }
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Inserta el punto de corte masivo
        /// </summary>
        /// <param name="listaActualizar">Objeto de clase ProgramaGeneralPuntoCorteMasivoDTO</param>
        /// <returns>Bool</returns>
        public bool InsertarPuntoCorteMasivo(ProgramaGeneralPuntoCorteMasivoDTO listaActualizar)
        {
            try
            {
                List<ProgramaGeneralPuntoCorteBO> listaInsercion = new List<ProgramaGeneralPuntoCorteBO>();
                ProgramaGeneralPuntoCorteDetalleRepositorio _repProgramaGeneralPuntoCorteDetalle = new ProgramaGeneralPuntoCorteDetalleRepositorio(_integraDBContext);
                ProgramaGeneralPuntoCorteDetalleBO programaGeneralPuntoCorteDetalleNuevo = new ProgramaGeneralPuntoCorteDetalleBO();

                foreach (var objetoActualizable in listaActualizar.ListaIdPGeneral)
                {
                    ProgramaGeneralPuntoCorteBO programaGeneralPuntoCorteBO = new ProgramaGeneralPuntoCorteBO(_integraDBContext)
                    {
                        IdProgramaGeneral = objetoActualizable,
                        PuntoCorteMedia = listaActualizar.PuntoCorteMedia,
                        PuntoCorteAlta = listaActualizar.PuntoCorteAlta,
                        PuntoCorteMuyAlta = listaActualizar.PuntoCorteMuyAlta,
                        UsuarioCreacion = listaActualizar.Usuario,
                        UsuarioModificacion = listaActualizar.Usuario,
                        FechaCreacion = DateTime.Now,
                        FechaModificacion = DateTime.Now,
                        Estado = true,
                    };
                    _repProgramaGeneralPuntoCorte.Insert(programaGeneralPuntoCorteBO);


                    foreach (var item in listaActualizar.ListaPuntoCorteMedia)
                    {
                        programaGeneralPuntoCorteDetalleNuevo.IdProgramaGeneralPuntoCorte = programaGeneralPuntoCorteBO.Id;
                        programaGeneralPuntoCorteDetalleNuevo.IdPuntoCorte = 1;
                        programaGeneralPuntoCorteDetalleNuevo.Tipo = item.Tipo;
                        programaGeneralPuntoCorteDetalleNuevo.Descripcion = item.Descripcion;
                        programaGeneralPuntoCorteDetalleNuevo.ValorMinimo = item.ValorMinimo;
                        programaGeneralPuntoCorteDetalleNuevo.ValorMaximo = item.ValorMaximo;
                        programaGeneralPuntoCorteDetalleNuevo.Estado = true;
                        programaGeneralPuntoCorteDetalleNuevo.UsuarioCreacion = listaActualizar.Usuario;
                        programaGeneralPuntoCorteDetalleNuevo.UsuarioModificacion = listaActualizar.Usuario;
                        programaGeneralPuntoCorteDetalleNuevo.FechaCreacion = DateTime.Now;
                        programaGeneralPuntoCorteDetalleNuevo.FechaModificacion = DateTime.Now;
                        _repProgramaGeneralPuntoCorteDetalle.Insert(programaGeneralPuntoCorteDetalleNuevo);
                        programaGeneralPuntoCorteDetalleNuevo.Id = 0;
                    }
                    foreach (var item in listaActualizar.ListaPuntoCorteAlta)
                    {
                        programaGeneralPuntoCorteDetalleNuevo.IdProgramaGeneralPuntoCorte = programaGeneralPuntoCorteBO.Id;
                        programaGeneralPuntoCorteDetalleNuevo.IdPuntoCorte = 2;
                        programaGeneralPuntoCorteDetalleNuevo.Tipo = item.Tipo;
                        programaGeneralPuntoCorteDetalleNuevo.Descripcion = item.Descripcion;
                        programaGeneralPuntoCorteDetalleNuevo.ValorMinimo = item.ValorMinimo;
                        programaGeneralPuntoCorteDetalleNuevo.ValorMaximo = item.ValorMaximo;
                        programaGeneralPuntoCorteDetalleNuevo.Estado = true;
                        programaGeneralPuntoCorteDetalleNuevo.UsuarioCreacion = listaActualizar.Usuario;
                        programaGeneralPuntoCorteDetalleNuevo.UsuarioModificacion = listaActualizar.Usuario;
                        programaGeneralPuntoCorteDetalleNuevo.FechaCreacion = DateTime.Now;
                        programaGeneralPuntoCorteDetalleNuevo.FechaModificacion = DateTime.Now;
                        _repProgramaGeneralPuntoCorteDetalle.Insert(programaGeneralPuntoCorteDetalleNuevo);
                        programaGeneralPuntoCorteDetalleNuevo.Id = 0;
                    }
                    foreach (var item in listaActualizar.ListaPuntoCorteMuyAlta)
                    {
                        programaGeneralPuntoCorteDetalleNuevo.IdProgramaGeneralPuntoCorte = programaGeneralPuntoCorteBO.Id;
                        programaGeneralPuntoCorteDetalleNuevo.IdPuntoCorte = 3;
                        programaGeneralPuntoCorteDetalleNuevo.Tipo = item.Tipo;
                        programaGeneralPuntoCorteDetalleNuevo.Descripcion = item.Descripcion;
                        programaGeneralPuntoCorteDetalleNuevo.ValorMinimo = item.ValorMinimo;
                        programaGeneralPuntoCorteDetalleNuevo.ValorMaximo = item.ValorMaximo;
                        programaGeneralPuntoCorteDetalleNuevo.Estado = true;
                        programaGeneralPuntoCorteDetalleNuevo.UsuarioCreacion = listaActualizar.Usuario;
                        programaGeneralPuntoCorteDetalleNuevo.UsuarioModificacion = listaActualizar.Usuario;
                        programaGeneralPuntoCorteDetalleNuevo.FechaCreacion = DateTime.Now;
                        programaGeneralPuntoCorteDetalleNuevo.FechaModificacion = DateTime.Now;
                        _repProgramaGeneralPuntoCorteDetalle.Insert(programaGeneralPuntoCorteDetalleNuevo);
                        programaGeneralPuntoCorteDetalleNuevo.Id = 0;
                    }

                    //listaInsercion.Add(programaGeneralPuntoCorteBO);
                }

                //_repProgramaGeneralPuntoCorte.Insert(listaInsercion);

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
