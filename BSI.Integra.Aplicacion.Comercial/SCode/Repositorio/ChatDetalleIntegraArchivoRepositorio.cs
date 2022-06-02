using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using AutoMapper;
using BSI.Integra.Aplicacion.Comercial.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using Newtonsoft.Json;
using BSI.Integra.Aplicacion.DTOs;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System.IO;
using BSI.Integra.Aplicacion.Comercial.SCode.BO;

namespace BSI.Integra.Aplicacion.Comercial.Repositorio
{
	/// Repositorio: ChatDetalleIntegraArchivo
	/// Autor: Edgar Serruto.
	/// Fecha: 17/07/2021
	/// <summary>
	/// Repositorio para de tabla T_ChatDetalleIntegraArchivo
	/// </summary>
	public class ChatDetalleIntegraArchivoRepositorio : BaseRepository<TChatDetalleIntegraArchivo, ChatDetalleIntegraArchivoBO>
	{
		#region Metodos Base
		public ChatDetalleIntegraArchivoRepositorio() : base()
		{
		}
		public ChatDetalleIntegraArchivoRepositorio(integraDBContext contexto) : base(contexto)
		{
		}
		public IEnumerable<ChatDetalleIntegraArchivoBO> GetBy(Expression<Func<TChatDetalleIntegraArchivo, bool>> filter)
		{
			IEnumerable<TChatDetalleIntegraArchivo> listado = base.GetBy(filter);
			List<ChatDetalleIntegraArchivoBO> listadoBO = new List<ChatDetalleIntegraArchivoBO>();
			foreach (var itemEntidad in listado)
			{
				ChatDetalleIntegraArchivoBO objetoBO = Mapper.Map<TChatDetalleIntegraArchivo, ChatDetalleIntegraArchivoBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
				listadoBO.Add(objetoBO);
			}

			return listadoBO;
		}
		public ChatDetalleIntegraArchivoBO FirstById(int id)
		{
			try
			{
				TChatDetalleIntegraArchivo entidad = base.FirstById(id);
				ChatDetalleIntegraArchivoBO objetoBO = new ChatDetalleIntegraArchivoBO();
				Mapper.Map<TChatDetalleIntegraArchivo, ChatDetalleIntegraArchivoBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

				return objetoBO;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}
		public ChatDetalleIntegraArchivoBO FirstBy(Expression<Func<TChatDetalleIntegraArchivo, bool>> filter)
		{
			try
			{
				TChatDetalleIntegraArchivo entidad = base.FirstBy(filter);
				ChatDetalleIntegraArchivoBO objetoBO = Mapper.Map<TChatDetalleIntegraArchivo, ChatDetalleIntegraArchivoBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

				return objetoBO;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}

		public bool Insert(ChatDetalleIntegraArchivoBO objetoBO)
		{
			try
			{
				//mapeo de la entidad
				TChatDetalleIntegraArchivo entidad = MapeoEntidad(objetoBO);

				bool resultado = base.Insert(entidad);
				if (resultado)
					AsignacionId(entidad, objetoBO);

				return resultado;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}

		public bool Insert(IEnumerable<ChatDetalleIntegraArchivoBO> listadoBO)
		{
			try
			{
				foreach (var objetoBO in listadoBO)
				{
					bool resultado = Insert(objetoBO);
					if (resultado == false)
						return false;
				}

				return true;
			}
			catch (Exception e)
			{
				throw e;
			}
		}

		public bool Update(ChatDetalleIntegraArchivoBO objetoBO)
		{
			try
			{
				if (objetoBO == null)
				{
					throw new ArgumentNullException("Entidad nula");
				}

				//mapeo de la entidad
				TChatDetalleIntegraArchivo entidad = MapeoEntidad(objetoBO);

				bool resultado = base.Update(entidad);
				if (resultado)
					AsignacionId(entidad, objetoBO);

				return resultado;
			}
			catch (Exception e)
			{
				throw e;
			}
		}

		public bool Update(IEnumerable<ChatDetalleIntegraArchivoBO> listadoBO)
		{
			try
			{
				foreach (var objetoBO in listadoBO)
				{
					bool resultado = Update(objetoBO);
					if (resultado == false)
						return false;
				}

				return true;
			}
			catch (Exception e)
			{
				throw e;
			}
		}
		private void AsignacionId(TChatDetalleIntegraArchivo entidad, ChatDetalleIntegraArchivoBO objetoBO)
		{
			try
			{
				if (entidad != null && objetoBO != null)
				{
					objetoBO.Id = entidad.Id;
				}
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}

		private TChatDetalleIntegraArchivo MapeoEntidad(ChatDetalleIntegraArchivoBO objetoBO)
		{
			try
			{
				//crea la entidad padre
				TChatDetalleIntegraArchivo entidad = new TChatDetalleIntegraArchivo();
				entidad = Mapper.Map<ChatDetalleIntegraArchivoBO, TChatDetalleIntegraArchivo>(objetoBO,
					opt => opt.ConfigureMap(MemberList.None));

				//mapea los hijos

				return entidad;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}
		#endregion
		/// Repositorio: ChatDetalleIntegraArchivoRepositorio
		/// Autor: Edgar Serruto
		/// Fecha: 16/07/2021
		/// <summary>
		/// Sube el archivo al blobstorage
		/// </summary>
		/// <param name="archivo"></param>
		/// <param name="tipo"></param>
		/// <param name="nombreArchivo"></param>
		/// <returns>string</returns>
		public string SubirDocumentosChatSoporte(byte[] archivo, string tipo, string nombreArchivo)
		{
			try
			{
				string _nombreLink = string.Empty;

				try
				{
					string _azureStorageConnectionString = "DefaultEndpointsProtocol=https;AccountName=repositorioweb;AccountKey=JurvlnvFAqg4dcGqcDHEj9bkBLoLV3Z/EIxA+8QkdTcuCWTm1iZfgqUOfUOwmDMfnrmrie7Nkkho5mPyVTvIpA==;EndpointSuffix=core.windows.net";
					string _direccionBlob = @"repositorioweb/DocumentosChatAulaVirtual/";

					//Generar entrada al blob storage
					CloudStorageAccount storageAccount = CloudStorageAccount.Parse(_azureStorageConnectionString);
					CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
					CloudBlobContainer container = blobClient.GetContainerReference(_direccionBlob);

					CloudBlockBlob blockBlob = container.GetBlockBlobReference(nombreArchivo);
					blockBlob.Properties.ContentType = tipo;
					blockBlob.Metadata["filename"] = nombreArchivo;
					blockBlob.Metadata["filemime"] = tipo;
					Stream stream = new MemoryStream(archivo);
					var objRegistrado = blockBlob.UploadFromStreamAsync(stream);

					objRegistrado.Wait();
					var correcto = objRegistrado.IsCompletedSuccessfully;

					if (correcto)
					{
						_nombreLink = "https://repositorioweb.blob.core.windows.net/" + _direccionBlob + nombreArchivo.Replace(" ", "%20");
					}
					else
					{
						_nombreLink = "";
					}
					return _nombreLink;
				}
				catch (Exception ex)
				{
					return "";
				}
			}
			catch (Exception e)
			{
				return "";
			}
		}
	}
}
