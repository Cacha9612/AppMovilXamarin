using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

public class ImageDetailViewModel : INotifyPropertyChanged
{
	private string _imageSource;

	public string ImageSource
	{
		get => _imageSource;
		set
		{
			_imageSource = value;
			OnPropertyChanged(nameof(ImageSource));
		}
	}

	public ImageDetailViewModel(string imageSource)
	{
		ImageSource = imageSource ?? throw new ArgumentNullException(nameof(imageSource));
	}

	public event PropertyChangedEventHandler PropertyChanged;

	protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
	{
		PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
	}
}
