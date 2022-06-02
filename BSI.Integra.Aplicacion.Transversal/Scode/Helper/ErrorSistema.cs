using BSI.Integra.Aplicacion.Base.Classes;
using BSI.Integra.Aplicacion.Base.DTO;
using BSI.Integra.Aplicacion.Transversal.Repositorio;
using System;
using System.Collections.Generic;

namespace BSI.Integra.Aplicacion.Transversal.Helper
{
    public class ErrorSistema
    {
        private static readonly Lazy<ErrorSistema> instance = new Lazy<ErrorSistema>(() => new ErrorSistema());

        protected static Dictionary<int, Error> Errores;
        private static ErrorRepositorio _repoError;
        public static ErrorSistema Instance
        {
            get
            {
                return instance.Value;
            }
        }
        private ErrorSistema()
        {
            _repoError = new ErrorRepositorio();
            Errores = new Dictionary<int, Error>();
            var errores = _repoError.ObtenerTodosErroresSistema();
            CargarErrores(errores);

        }
        private void CargarErrores(List<ErrorDTO> errores)
        {
            foreach (var error in errores)
            {
                var mensajeReal = error.Descripcion;
                if (!string.IsNullOrEmpty(error.DescripcionPersonalizada))
                    mensajeReal = error.DescripcionPersonalizada;

                Error newError = new Error();
                newError.Codigo = error.Codigo;
                newError.Mensaje = mensajeReal;
                newError.Tipo = error.IdErrorTipo;
                Errores.Add(error.Codigo, newError);
            }
        } 

        public string MensajeError(int codigo)
        {
            return Errores[codigo].Mensaje;
        }
    }
}
