using BSI.Integra.Aplicacion.Base.BO;
using BSI.Integra.Aplicacion.Transversal.Repositorio;
using BSI.Integra.Persistencia.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BSI.Integra.Aplicacion.Transversal.BO
{
    /// BO: Marketing/ConfiguracionDatoRemarketingTipoDato
    /// Autor: Gian Miranda
    /// Fecha: 17/08/2021
    /// <summary>
    /// BO para la logica de Configuracion de datos de Remarketing y su Tipo de Dato
    /// </summary>
    public class ConfiguracionDatoRemarketingTipoDatoBO : BaseBO
    {
        /// Propiedades	                        Significado
        /// -----------	                        ------------
        /// IdConfiguracionDatoRemarketing      Id de la configuracion de dato remarketing (PK de la tabla mkt.T_ConfiguracionDatoRemarketing)
        /// IdTipoDato                          Id del tipo de dato (PK de la tabla mkt.T_TipoDato)
        /// IdMigracion                         Id migracion de V3 (Campo nullable)

        public int IdConfiguracionDatoRemarketing { get; set; }
        public int IdTipoDato { get; set; }
        public Guid? IdMigracion { get; set; }

        private readonly integraDBContext _integraDBContext;

        private readonly ConfiguracionDatoRemarketingTipoDatoRepositorio _repConfiguracionDatoRemarketingTipoDato;

        public ConfiguracionDatoRemarketingTipoDatoBO()
        {
            _repConfiguracionDatoRemarketingTipoDato = new ConfiguracionDatoRemarketingTipoDatoRepositorio();
        }

        public ConfiguracionDatoRemarketingTipoDatoBO(integraDBContext integraDBContext)
        {
            _integraDBContext = integraDBContext;

            _repConfiguracionDatoRemarketingTipoDato = new ConfiguracionDatoRemarketingTipoDatoRepositorio(_integraDBContext);
        }

        /// <summary>
        /// Actualiza lista de configuracion de remarketing Tipo de Dato
        /// </summary>
        /// <param name="idConfiguracionDatoRemarketing">Lista de objetos de clase AnuncioFacebookMetricaDTO</param>
        /// <param name="listaIdTipoDato">Lista de id de los tipos de datos (PK de la tabla mkt.T_TipoDato)</param>
        /// <param name="usuario">Usuario que realiza la accion</param>
        /// <returns>bool</returns>
        public bool ActualizarListaConfiguracionDatoRemarketingTipoDato(int idConfiguracionDatoRemarketing, List<int> listaIdTipoDato, string usuario)
        {
            try
            {
                var listaExistente = _repConfiguracionDatoRemarketingTipoDato.GetBy(x => x.IdConfiguracionDatoRemarketing == idConfiguracionDatoRemarketing);

                // Eliminado de datos no existentes
                var listaAEliminar = listaExistente.Where(x => !listaIdTipoDato.Contains(x.IdTipoDato));

                if (listaAEliminar.Any())
                {
                    bool resultadoEliminacion = _repConfiguracionDatoRemarketingTipoDato.Delete(listaAEliminar.Select(s => s.Id), usuario);

                    if (!resultadoEliminacion)
                        throw new Exception("Fallo en la eliminacion de tipos de datos anteriores");
                }

                var listaAMantenerTipoDato = listaExistente.Where(x => listaIdTipoDato.Contains(x.IdTipoDato)).Select(s => s.IdTipoDato);

                // Insercion de nuevos datos
                var listaAInsertar = listaIdTipoDato.Where(x => !listaAMantenerTipoDato.Contains(x)).ToList();

                if (listaAInsertar.Any())
                {
                    bool resultadoInsercion = _repConfiguracionDatoRemarketingTipoDato.Insert(listaAInsertar.Select(s => new ConfiguracionDatoRemarketingTipoDatoBO()
                    {
                        IdConfiguracionDatoRemarketing = idConfiguracionDatoRemarketing,
                        IdTipoDato = s,
                        Estado = true,
                        UsuarioCreacion = usuario,
                        UsuarioModificacion = usuario,
                        FechaCreacion = DateTime.Now,
                        FechaModificacion = DateTime.Now
                    }));

                    if (!resultadoInsercion)
                        throw new Exception("Fallo en la insercion de tipos de datos nuevos");
                }

                return true;
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
        public bool EliminarListaConfiguracionDatoRemarketingTipoDato(int idConfiguracionDatoRemarketing, string usuarioResponsable)
        {
            try
            {
                var listaIdAEliminar = _repConfiguracionDatoRemarketingTipoDato.GetBy(x => x.IdConfiguracionDatoRemarketing == idConfiguracionDatoRemarketing).Select(s => s.Id).ToList();
                bool resultadoEliminado = true;

                if (listaIdAEliminar.Any())
                    resultadoEliminado = _repConfiguracionDatoRemarketingTipoDato.Delete(listaIdAEliminar, usuarioResponsable);

                if (!resultadoEliminado)
                    throw new Exception("Fallo en el eliminado de tipo de dato");

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
