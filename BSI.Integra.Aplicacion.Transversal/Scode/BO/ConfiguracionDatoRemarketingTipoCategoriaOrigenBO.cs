using BSI.Integra.Aplicacion.Base.BO;
using BSI.Integra.Aplicacion.Transversal.Repositorio;
using BSI.Integra.Persistencia.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BSI.Integra.Aplicacion.Transversal.BO
{
    /// BO: Marketing/ConfiguracionDatoRemarketingTipoCategoriaOrigen
    /// Autor: Gian Miranda
    /// Fecha: 17/08/2021
    /// <summary>
    /// BO para la logica de Configuracion de datos de Remarketing y su Tipo de Categoria de Origen
    /// </summary>
    public class ConfiguracionDatoRemarketingTipoCategoriaOrigenBO : BaseBO
    {
        /// Propiedades	                        Significado
        /// -----------	                        ------------
        /// IdConfiguracionDatoRemarketing      Id de la configuracion de dato remarketing (PK de la tabla mkt.T_ConfiguracionDatoRemarketing)
        /// IdTipoCategoriaOrigen               Id del tipo de categoria origen (PK de la tabla mkt.T_TipoCategoriaOrigen)
        /// IdMigracion                         Id migracion de V3 (Campo nullable)

        public int IdConfiguracionDatoRemarketing { get; set; }
        public int IdTipoCategoriaOrigen { get; set; }
        public Guid? IdMigracion { get; set; }

        private readonly integraDBContext _integraDBContext;

        private readonly ConfiguracionDatoRemarketingTipoCategoriaOrigenRepositorio _repConfiguracionDatoRemarketingTipoCategoriaOrigen;

        public ConfiguracionDatoRemarketingTipoCategoriaOrigenBO()
        {
            _repConfiguracionDatoRemarketingTipoCategoriaOrigen = new ConfiguracionDatoRemarketingTipoCategoriaOrigenRepositorio();
        }

        public ConfiguracionDatoRemarketingTipoCategoriaOrigenBO(integraDBContext integraDBContext)
        {
            _integraDBContext = integraDBContext;

            _repConfiguracionDatoRemarketingTipoCategoriaOrigen = new ConfiguracionDatoRemarketingTipoCategoriaOrigenRepositorio(_integraDBContext);
        }

        /// <summary>
        /// Actualiza lista de configuracion de remarketing Tipo de Dato
        /// </summary>
        /// <param name="idConfiguracionDatoRemarketing">Lista de objetos de clase AnuncioFacebookMetricaDTO</param>
        /// <param name="listaIdTipoCategoriaOrigen">Lista de id de los tipos de datos (PK de la tabla mkt.T_TipoCategoriaOrigen)</param>
        /// <param name="usuario">Usuario que realiza la accion</param>
        /// <returns>bool</returns>
        public bool ActualizarListaConfiguracionDatoRemarketingTipoCategoriaOrigen(int idConfiguracionDatoRemarketing, List<int> listaIdTipoCategoriaOrigen, string usuario)
        {
            try
            {
                var listaExistente = _repConfiguracionDatoRemarketingTipoCategoriaOrigen.GetBy(x => x.IdConfiguracionDatoRemarketing == idConfiguracionDatoRemarketing);

                // Eliminado de datos no existentes
                var listaAEliminar = listaExistente.Where(x => !listaIdTipoCategoriaOrigen.Contains(x.IdTipoCategoriaOrigen));

                if (listaAEliminar.Any())
                {
                    bool resultadoEliminacion = _repConfiguracionDatoRemarketingTipoCategoriaOrigen.Delete(listaAEliminar.Select(s => s.Id), usuario);

                    if (!resultadoEliminacion)
                        throw new Exception("Fallo en la eliminacion de tipos de categoria origen anteriores");
                }

                var listaAMantenerTipoCategoriaOrigen = listaExistente.Where(x => listaIdTipoCategoriaOrigen.Contains(x.IdTipoCategoriaOrigen)).Select(s => s.IdTipoCategoriaOrigen);

                // Insercion de nuevos datos
                var listaAInsertar = listaIdTipoCategoriaOrigen.Where(x => !listaAMantenerTipoCategoriaOrigen.Contains(x));

                if (listaAInsertar.Any())
                {
                    bool resultadoInsercion = _repConfiguracionDatoRemarketingTipoCategoriaOrigen.Insert(listaAInsertar.Select(s => new ConfiguracionDatoRemarketingTipoCategoriaOrigenBO()
                    {
                        IdConfiguracionDatoRemarketing = idConfiguracionDatoRemarketing,
                        IdTipoCategoriaOrigen = s,
                        Estado = true,
                        UsuarioCreacion = usuario,
                        UsuarioModificacion = usuario,
                        FechaCreacion = DateTime.Now,
                        FechaModificacion = DateTime.Now
                    }));

                    if (!resultadoInsercion)
                        throw new Exception("Fallo en la insercion de tipos de categoria origen nuevos");
                }

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Elimina la lista de configuracion de datos de remarketing de tipo categoria origen
        /// </summary>
        /// <param name="idConfiguracionDatoRemarketing">Id de la configuracion de dato de remarketing (PK de la tabla mkt.T_ConfiguracionDatoRemarketing)</param>
        /// <param name="usuarioResponsable">Usuario que realiza la accion</param>
        /// <returns>bool</returns>
        public bool EliminarListaConfiguracionDatoRemarketingTipoCategoriaOrigen(int idConfiguracionDatoRemarketing, string usuarioResponsable)
        {
            try
            {
                var listaIdAEliminar = _repConfiguracionDatoRemarketingTipoCategoriaOrigen.GetBy(x => x.IdConfiguracionDatoRemarketing == idConfiguracionDatoRemarketing).Select(s => s.Id).ToList();
                bool resultadoEliminado = true;

                if (listaIdAEliminar.Any())
                    resultadoEliminado = _repConfiguracionDatoRemarketingTipoCategoriaOrigen.Delete(listaIdAEliminar, usuarioResponsable);

                if (!resultadoEliminado)
                    throw new Exception("Fallo en el eliminado de tipos de categoria origen");

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
