using System.Threading.Tasks;
using Xamarin.Forms;
using AppMovilTrotaMundos.Models;
using AppMovilTrotaMundos.Services;
using System;

namespace AppMovilTrotaMundos.ViewModels
{
	public class CheckListServicioDetallesViewModel : BaseViewModel
	{
		private readonly ApiService _apiService;
		private CheckList _checklist;

		public CheckList CheckList
		{
			get => _checklist;
			set
			{
				_checklist = value;
				OnPropertyChanged();
			}
		}

		public CheckListServicioDetallesViewModel(int Id)
		{
			_apiService = new ApiService();
			LoadCheckListAsync(Id);
		}

		private async Task LoadCheckListAsync(int Id)
		{
			try
			{
				// Reemplaza la URL si es necesario
				CheckList = await _apiService.GetAsync<CheckList>($"api/obtenerservicio?Idchecklist={Id}");
			}
			catch (Exception ex)
			{
				await Application.Current.MainPage.DisplayAlert("Error", ex.Message, "OK");
			}
		}
	}
}
