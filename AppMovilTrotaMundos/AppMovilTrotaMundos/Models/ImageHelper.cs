using System;
using System.Collections.Generic;
using System.Text;
using SkiaSharp;
using Xamarin.Forms;
using System.IO;

namespace AppMovilTrotaMundos.Models
{


	public static class ImageHelper
	{
		public static byte[] ResizeImage(byte[] imageData, float width, float height)
		{
			// Crear una imagen a partir de los datos originales
			using (var originalImage = SKBitmap.Decode(imageData))
			{
				// Crear una nueva imagen con las dimensiones deseadas
				using (var resizedImage = originalImage.Resize(new SKImageInfo((int)width, (int)height), SKFilterQuality.High))
				{
					// Convertir la imagen redimensionada a formato de imagen
					using (var image = SKImage.FromBitmap(resizedImage))
					{
						using (var data = image.Encode(SKEncodedImageFormat.Jpeg, 75)) // 75 es la calidad de la imagen
						{
							return data.ToArray();
						}
					}
				}
			}
		}
	}

}
