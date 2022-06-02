using BSI.Integra.Aplicacion.Base.BO;
using BSI.Integra.Aplicacion.DTOs.Scode.DTOs.Marketing;
using BSI.Integra.Aplicacion.Transversal.Repositorio;
using BSI.Integra.Persistencia.Models;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Transactions;

namespace BSI.Integra.Aplicacion.Transversal.BO
{
    /// BO: Marketing/ConfiguracionDatoRemarketing
    /// Autor: Gian Miranda
    /// Fecha: 17/08/2021
    /// <summary>
    /// BO para la logica de Configuracion de datos de Remarketing
    /// </summary>
    public class ConfiguracionDatoRemarketingBO : BaseBO
    {
        /// Propiedades	                        Significado
        /// -----------	                        ------------
        /// IdAgendaTab                         Id del tab de la agenda (PK de la tabla com.T_AgendaTab)
        /// FechaInicio                         Fecha de inicio de vigencia de la configuracion
        /// FechaFin                            Fecha de fin de vigencia de la configuracion
        /// IdMigracion                         Id migracion de V3 (Campo nullable)

        public int IdAgendaTab { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public Guid? IdMigracion { get; set; }

        private readonly integraDBContext _integraDBContext;
        private readonly ConfiguracionDatoRemarketingRepositorio _repConfiguracionDatoRemarketing;
        private readonly ConfiguracionDatoRemarketingTipoDatoBO ConfiguracionDatoRemarketingTipoDato;
        private readonly ConfiguracionDatoRemarketingTipoCategoriaOrigenBO ConfiguracionDatoRemarketingTipoCategoriaOrigen;
        private readonly ConfiguracionDatoRemarketingCategoriaOrigenBO ConfiguracionDatoRemarketingCategoriaOrigen;
        private readonly ConfiguracionDatoRemarketingProbabilidadRegistroBO ConfiguracionDatoRemarketingProbabilidadRegistro;

        private readonly TipoDatoRepositorio _repTipoDato;
        private readonly TipoCategoriaOrigenRepositorio _repTipoCategoriaOrigen;
        private readonly CategoriaOrigenRepositorio _repCategoriaOrigen;
        private readonly ProbabilidadRegistroPwRepositorio _repProbabilidadRegistroPw;

        public ConfiguracionDatoRemarketingBO()
        {
            _repConfiguracionDatoRemarketing = new ConfiguracionDatoRemarketingRepositorio();
            ConfiguracionDatoRemarketingTipoDato = new ConfiguracionDatoRemarketingTipoDatoBO();
            ConfiguracionDatoRemarketingTipoCategoriaOrigen = new ConfiguracionDatoRemarketingTipoCategoriaOrigenBO();
            ConfiguracionDatoRemarketingCategoriaOrigen = new ConfiguracionDatoRemarketingCategoriaOrigenBO();
            ConfiguracionDatoRemarketingProbabilidadRegistro = new ConfiguracionDatoRemarketingProbabilidadRegistroBO();

            _repTipoDato = new TipoDatoRepositorio();
            _repTipoCategoriaOrigen = new TipoCategoriaOrigenRepositorio();
            _repCategoriaOrigen = new CategoriaOrigenRepositorio();
            _repProbabilidadRegistroPw = new ProbabilidadRegistroPwRepositorio();
        }

        public ConfiguracionDatoRemarketingBO(integraDBContext integraDBContext)
        {
            _integraDBContext = integraDBContext;

            _repConfiguracionDatoRemarketing = new ConfiguracionDatoRemarketingRepositorio(_integraDBContext);
            ConfiguracionDatoRemarketingTipoDato = new ConfiguracionDatoRemarketingTipoDatoBO(_integraDBContext);
            ConfiguracionDatoRemarketingTipoCategoriaOrigen = new ConfiguracionDatoRemarketingTipoCategoriaOrigenBO(_integraDBContext);
            ConfiguracionDatoRemarketingCategoriaOrigen = new ConfiguracionDatoRemarketingCategoriaOrigenBO(_integraDBContext);
            ConfiguracionDatoRemarketingProbabilidadRegistro = new ConfiguracionDatoRemarketingProbabilidadRegistroBO(_integraDBContext);

            _repTipoDato = new TipoDatoRepositorio(_integraDBContext);
            _repTipoCategoriaOrigen = new TipoCategoriaOrigenRepositorio(_integraDBContext);
            _repCategoriaOrigen = new CategoriaOrigenRepositorio(_integraDBContext);
            _repProbabilidadRegistroPw = new ProbabilidadRegistroPwRepositorio(_integraDBContext);
        }

        /// <summary>
        /// Actualiza lista de configuracion de remarketing Tipo de Dato
        /// </summary>
        /// <param name="configuracionDatoRemarketingAActualizar">Objeto de clase ConfiguracionDatoRemarketingDTO</param>
        /// <returns>bool</returns>
        public bool ActualizarListaConfiguracionDatoRemarketingGeneral(ConfiguracionDatoRemarketingDTO configuracionDatoRemarketingAActualizar)
        {
            try
            {
                bool resultadoActualizacionConfiguracion = false;

                using (TransactionScope scope = new TransactionScope())
                {
                    configuracionDatoRemarketingAActualizar.Id = ActualizarListaConfiguracionDatoRemarketing(configuracionDatoRemarketingAActualizar);

                    resultadoActualizacionConfiguracion = ConfiguracionDatoRemarketingTipoDato.ActualizarListaConfiguracionDatoRemarketingTipoDato(configuracionDatoRemarketingAActualizar.Id, configuracionDatoRemarketingAActualizar.ListaIdTipoDato, configuracionDatoRemarketingAActualizar.Usuario);
                    resultadoActualizacionConfiguracion = ConfiguracionDatoRemarketingTipoCategoriaOrigen.ActualizarListaConfiguracionDatoRemarketingTipoCategoriaOrigen(configuracionDatoRemarketingAActualizar.Id, configuracionDatoRemarketingAActualizar.ListaIdTipoCategoriaOrigen, configuracionDatoRemarketingAActualizar.Usuario);
                    resultadoActualizacionConfiguracion = ConfiguracionDatoRemarketingCategoriaOrigen.ActualizarListaConfiguracionDatoRemarketingCategoriaOrigen(configuracionDatoRemarketingAActualizar.Id, configuracionDatoRemarketingAActualizar.ListaCategoriaOrigen, configuracionDatoRemarketingAActualizar.Usuario);
                    resultadoActualizacionConfiguracion = ConfiguracionDatoRemarketingProbabilidadRegistro.ActualizarListaConfiguracionDatoRemarketingProbabilidadRegistro(configuracionDatoRemarketingAActualizar.Id, configuracionDatoRemarketingAActualizar.ListaProbabilidadRegistro, configuracionDatoRemarketingAActualizar.Usuario);

                    scope.Complete();
                }

                return resultadoActualizacionConfiguracion;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Gian Miranda
        /// Fecha: 20/08/2021
        /// Version: 1.0
        /// <summary>
        /// Obtiene la informacion de las configuraciones guardadas
        /// </summary>
        /// <returns>Lista de objetos de clase ConfiguracionDatoRemarketingAgrupadoGrillaDTO</returns>
        public List<ConfiguracionDatoRemarketingAgrupadoGrillaDTO> ObtenerConfiguracionesDatoRemarketing()
        {
            try
            {
                List<ConfiguracionDatoRemarketingAgrupadoGrillaDTO> resultadoAgrupado = new List<ConfiguracionDatoRemarketingAgrupadoGrillaDTO>();

                List<ConfiguracionDatoRemarketingGrillaDTO> listaAFormatear = _repConfiguracionDatoRemarketing.ObtenerConfiguracionesDatoRemarketing();

                resultadoAgrupado = listaAFormatear.GroupBy(g => new
                {
                    g.Id,
                    g.IdAgendaTab,
                    g.NombreAgendaTab,
                    g.FechaInicio,
                    g.FechaFin,
                    g.Vigente
                }).Select(s => new ConfiguracionDatoRemarketingAgrupadoGrillaDTO()
                {
                    Id = s.Key.Id,
                    IdAgendaTab = s.Key.IdAgendaTab,
                    NombreAgendaTab = s.Key.NombreAgendaTab,
                    FechaInicio = s.Key.FechaInicio,
                    FechaFin = s.Key.FechaFin,
                    Vigente = s.Key.Vigente,
                    ListaTipoDato = listaAFormatear.Select(ss => new { ss.Id, ss.IdTipoDato, ss.NombreTipoDato })
                                                    .Where(x => x.Id == s.Key.Id && x.IdTipoDato != null).Distinct()
                                                    .Select(sss => new ConfiguracionDatoRemarketingTipoDatoGrillaDTO() { IdTipoDato = sss.IdTipoDato.Value, NombreTipoDato = sss.NombreTipoDato })
                                                    .ToList(),
                    ListaTipoCategoriaOrigen = listaAFormatear
                                                    .Select(ss => new { ss.Id, ss.IdTipoCategoriaOrigen, ss.NombreTipoCategoriaOrigen })
                                                    .Where(x => x.Id == s.Key.Id && x.IdTipoCategoriaOrigen != null).Distinct()
                                                    .Select(sss => new ConfiguracionDatoRemarketingTipoCategoriaOrigenGrillaDTO() { IdTipoCategoriaOrigen = sss.IdTipoCategoriaOrigen.Value, NombreTipoCategoriaOrigen = sss.NombreTipoCategoriaOrigen })
                                                    .ToList(),
                    ListaCategoriaOrigen = listaAFormatear
                                                    .Select(ss => new { ss.Id, ss.IdCategoriaOrigen, ss.NombreCategoriaOrigen })
                                                    .Where(x => x.Id == s.Key.Id && x.IdCategoriaOrigen != null).Distinct()
                                                    .Select(sss => new ConfiguracionDatoRemarketingCategoriaOrigenGrillaDTO() { IdCategoriaOrigen = sss.IdCategoriaOrigen.Value, NombreCategoriaOrigen = sss.NombreCategoriaOrigen })
                                                    .ToList(),
                    ListaProbabilidadRegistroPw = listaAFormatear
                                                    .Select(ss => new { ss.Id, ss.IdProbabilidadRegistroPw, ss.NombreProbabilidadRegistroPw })
                                                    .Where(x => x.Id == s.Key.Id && x.IdProbabilidadRegistroPw != null).Distinct()
                                                    .Select(sss => new ConfiguracionDatoRemarketingProbabilidadRegistroPwGrillaDTO() { IdProbabilidadRegistroPw = sss.IdProbabilidadRegistroPw.Value, NombreProbabilidadRegistroPw = sss.NombreProbabilidadRegistroPw })
                                                    .ToList()
                }).ToList();

                return resultadoAgrupado;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Actualiza lista de configuracion de remarketing Tipo de Dato
        /// </summary>
        /// <param name="configuracionDatoRemarketingAActualizar">Objeto de clase ConfiguracionDatoRemarketingDTO</param>
        /// <returns>bool</returns>
        public int ActualizarListaConfiguracionDatoRemarketing(ConfiguracionDatoRemarketingDTO configuracionDatoRemarketingAActualizar)
        {
            try
            {
                ConfiguracionDatoRemarketingBO configuracionAActualizar = _repConfiguracionDatoRemarketing.FirstBy(x => x.Id == configuracionDatoRemarketingAActualizar.Id);

                if (configuracionAActualizar == null)
                {
                    configuracionAActualizar = new ConfiguracionDatoRemarketingBO(_integraDBContext)
                    {
                        IdAgendaTab = configuracionDatoRemarketingAActualizar.IdAgendaTab,
                        FechaInicio = configuracionDatoRemarketingAActualizar.FechaInicio,
                        FechaFin = configuracionDatoRemarketingAActualizar.FechaFin,
                        Estado = true,
                        UsuarioCreacion = configuracionDatoRemarketingAActualizar.Usuario,
                        UsuarioModificacion = configuracionDatoRemarketingAActualizar.Usuario,
                        FechaCreacion = DateTime.Now,
                        FechaModificacion = DateTime.Now
                    };
                }
                else
                {
                    configuracionAActualizar.IdAgendaTab = configuracionDatoRemarketingAActualizar.IdAgendaTab;
                    configuracionAActualizar.FechaInicio = configuracionDatoRemarketingAActualizar.FechaInicio;
                    configuracionAActualizar.FechaFin = configuracionDatoRemarketingAActualizar.FechaFin;
                    configuracionAActualizar.Estado = true;
                    configuracionAActualizar.UsuarioModificacion = configuracionDatoRemarketingAActualizar.Usuario;
                    configuracionAActualizar.FechaModificacion = DateTime.Now;
                }

                bool resultadoActualizacion = _repConfiguracionDatoRemarketing.Update(configuracionAActualizar);

                if (!resultadoActualizacion)
                    throw new Exception("Fallo en la actualizacion de la configuracion base de remarketing");

                return configuracionAActualizar.Id;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Elimina una configuracion de dato de remarketing
        /// </summary>
        /// <param name="configuracionDatoRemarketingAEliminar">Objeto de clase ConfiguracionDatoRemarketingAEliminarDTO</param>
        /// <returns>bool</returns>
        public bool EliminarConfiguracionDatoRemarketingGeneral(ConfiguracionDatoRemarketingAEliminarDTO configuracionDatoRemarketingAEliminar)
        {
            try
            {
                bool resultadoEliminadoConfiguracion = false;

                using (TransactionScope scope = new TransactionScope())
                {
                    resultadoEliminadoConfiguracion = ConfiguracionDatoRemarketingTipoDato.EliminarListaConfiguracionDatoRemarketingTipoDato(configuracionDatoRemarketingAEliminar.Id, configuracionDatoRemarketingAEliminar.Usuario);
                    resultadoEliminadoConfiguracion = ConfiguracionDatoRemarketingTipoCategoriaOrigen.EliminarListaConfiguracionDatoRemarketingTipoCategoriaOrigen(configuracionDatoRemarketingAEliminar.Id, configuracionDatoRemarketingAEliminar.Usuario);
                    resultadoEliminadoConfiguracion = ConfiguracionDatoRemarketingCategoriaOrigen.EliminarListaConfiguracionDatoRemarketingCategoriaOrigen(configuracionDatoRemarketingAEliminar.Id, configuracionDatoRemarketingAEliminar.Usuario);
                    resultadoEliminadoConfiguracion = ConfiguracionDatoRemarketingProbabilidadRegistro.EliminarListaConfiguracionDatoRemarketingProbabilidadRegistro(configuracionDatoRemarketingAEliminar.Id, configuracionDatoRemarketingAEliminar.Usuario);
                    resultadoEliminadoConfiguracion = EliminarListaConfiguracionDatoRemarketing(configuracionDatoRemarketingAEliminar.Id, configuracionDatoRemarketingAEliminar.Usuario);

                    scope.Complete();
                }

                return resultadoEliminadoConfiguracion;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Elimina la lista de configuracion de datos de remarketing de probabilidad de registro
        /// </summary>
        /// <param name="idConfiguracionDatoRemarketing">Id de la configuracion de dato de remarketing (PK de la tabla mkt.T_ConfiguracionDatoRemarketing)</param>
        /// <param name="usuarioResponsable">Usuario que realiza la accion</param>
        /// <returns>bool</returns>
        public bool EliminarListaConfiguracionDatoRemarketing(int idConfiguracionDatoRemarketing, string usuarioResponsable)
        {
            try
            {
                var listaIdAEliminar = _repConfiguracionDatoRemarketing.GetBy(x => x.Id == idConfiguracionDatoRemarketing).Select(s => s.Id).ToList();
                bool resultadoEliminado = true;

                if (listaIdAEliminar.Any())
                    resultadoEliminado = _repConfiguracionDatoRemarketing.Delete(listaIdAEliminar, usuarioResponsable);

                if (!resultadoEliminado)
                    throw new Exception("Fallo en el eliminado de la base de configuracion de dato remarketing");

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Obtiene los combos para configuracion de dato Remarketing
        /// </summary>
        /// <returns>Objeto de clase ComboConfiguracionDatoRemarketingDTO</returns>
        public ComboConfiguracionDatoRemarketingDTO ObtenerCombosParaConfiguracionDatoRemarketing()
        {
            try
            {
                var comboAgendaTab = _repConfiguracionDatoRemarketing.ObtenerAgendaTabVentasParaConfiguracion();
                var comboTipoDato = _repTipoDato.GetAll().Select(s => new ConfiguracionDatoRemarketingTipoDatoGrillaDTO { IdTipoDato = s.Id, NombreTipoDato = s.Nombre }).ToList();
                var comboCategoriaOrigen = _repCategoriaOrigen.GetBy(x => x.Nombre.Contains("remarketing")).Select(s => new ConfiguracionDatoRemarketingCategoriaOrigenGrillaDTO { IdCategoriaOrigen = s.Id, NombreCategoriaOrigen = s.Nombre, IdTipoCategoriaOrigen = s.IdTipoCategoriaOrigen }).ToList();
                var comboTipoCategoriaOrigen = _repTipoCategoriaOrigen.GetBy(x => comboCategoriaOrigen.Select(s => s.IdTipoCategoriaOrigen).Contains(x.Id)).Select(s => new ConfiguracionDatoRemarketingTipoCategoriaOrigenGrillaDTO { IdTipoCategoriaOrigen = s.Id, NombreTipoCategoriaOrigen = s.Nombre }).ToList();
                var comboProbabilidadRegistroPw = _repProbabilidadRegistroPw.GetAll().Select(s => new ConfiguracionDatoRemarketingProbabilidadRegistroPwGrillaDTO { IdProbabilidadRegistroPw = s.Id, NombreProbabilidadRegistroPw = s.Nombre }).ToList();

                return new ComboConfiguracionDatoRemarketingDTO
                {
                    ListaComboConfiguracionDatoRemarketingAgendaTab = comboAgendaTab,
                    ListaComboConfiguracionDatoRemarketingTipoDato = comboTipoDato,
                    ListaComboConfiguracionDatoRemarketingTipoCategoriaOrigen = comboTipoCategoriaOrigen,
                    ListaComboConfiguracionDatoRemarketingCategoriaOrigen = comboCategoriaOrigen,
                    ListaComboConfiguracionDatoRemarketingProbabilidadRegistroPw = comboProbabilidadRegistroPw
                };
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
