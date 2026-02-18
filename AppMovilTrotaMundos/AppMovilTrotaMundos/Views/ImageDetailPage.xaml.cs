using System;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppMovilTrotaMundos.Views
{
	public partial class ImageDetailPage : ContentPage
	{
		public ImageDetailPage(string imageSource)
		{
			InitializeComponent();
			BindingContext = new ImageDetailViewModel(imageSource);
		}
	}


}