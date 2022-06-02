using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using System;
using System.IO;
namespace BSI.Integra.Aplicacion.Transversal.Helper
{
    public class SubirArchivo
    {
        /// <summary>
        /// Este metodo almacena el archivo en la ruta especificada, 
        /// Caso crear: El archivo se va a insertar validando que no exista algun archivo con el mismo nombre en la ruta especidficada en caso exista retorna error
        /// Caso actualizar: si el archivo existe se va a sobreescribir y si se inserta  
        /// </summary>
        /// <param name="rutaArchivo"></param>
        /// <param name="archivo"></param>
        /// <param name="interaccionArchivo"></param>
        public void AlmacenarArchivo(string rutaArchivo, TipoInteraccionArchivo interaccionArchivo, IFormFile archivo = null)
        {
            try
            {
                switch (interaccionArchivo)
                {
                    case TipoInteraccionArchivo.Crear:
                        if (archivo.Length > 0)
                        {
                            using (var stream = new FileStream(rutaArchivo, FileMode.CreateNew))
                            {
                                archivo.CopyToAsync(stream);
                            }
                        }
                        break;
                    case TipoInteraccionArchivo.Actualizar:
                        if (archivo.Length > 0)
                        {
                            using (var stream = new FileStream(rutaArchivo, FileMode.Create))
                            {
                                archivo.CopyToAsync(stream);
                            }
                        }
                        break;
                    case TipoInteraccionArchivo.Eliminar:
                        File.Delete(rutaArchivo);
                        break;
                    default:
                        break;
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool ExisteArchivo(string rutaArchivo)
        {
            try
            {
                if (System.IO.File.Exists(rutaArchivo))
                {
                    return true;
                }
                return false;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
