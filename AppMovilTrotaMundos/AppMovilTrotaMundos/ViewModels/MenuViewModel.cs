using Xamarin.Forms;
using Xamarin.Essentials;
using System.Windows.Input;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System;

using AppMovilTrotaMundos.Models;
using AppMovilTrotaMundos.Views;
using AppMovilTrotaMundos.Services;

namespace AppMovilTrotaMundos.ViewModels
{
    public class MenuViewModel : BaseViewModel
    {
        private readonly ApiService _apiService;
        private Models.Empleados _empleado;

        public Models.Empleados Empleado
        {
            get => _empleado;
            set
            {
                _empleado = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<Models.MenuItem> MenuItems { get; } = new ObservableCollection<Models.MenuItem>();
        public ICommand LogoutCommand { get; }

        public MenuViewModel()
        {
            _apiService = new ApiService();
            LogoutCommand = new Command(OnLogout);
        }

        public async Task CargarMenu(int idUsuario)
        {
            string rolUsuario = await ObtenerRolEmpleadoAsync(idUsuario) ?? "Desconocido";

            var opcionesPorRol = new Dictionary<string, List<Models.MenuItem>>
            {
                ["Administrador"] = new List<Models.MenuItem>
                {
                    new Models.MenuItem { Title = "Empleados", TargetType = typeof(Views.Empleados), IconUnicode = "\uf0c0" },
                    new Models.MenuItem { Title = "Clientes", TargetType = typeof(Views.Clientes), IconUnicode = "\uf007" },
                    new Models.MenuItem { Title = "Vehiculos", TargetType = typeof(Views.Vehiculos), IconUnicode = "\uf1b9" },
                    new Models.MenuItem { Title = "CheckList", TargetType = typeof(Views.CheckList), IconUnicode = "\uf00c" },
                    new Models.MenuItem { Title = "Servicio", TargetType = typeof(Views.CheckListServicio), IconUnicode = "\uf013" },
                    new Models.MenuItem { Title = "Flotillas", TargetType = typeof(Views.Flotilla), IconUnicode = "\uf1b9" },
                    new Models.MenuItem { Title = "Asignar Orden Servicio", TargetType = typeof(Views.FormularioAsignarOrden), IconUnicode = "\uf0ae" }
                },

                ["Almacén"] = new List<Models.MenuItem>
                {
                    new Models.MenuItem { Title = "Clientes", TargetType = typeof(Views.Clientes), IconUnicode = "\uf007" },
                    new Models.MenuItem { Title = "Vehiculos", TargetType = typeof(Views.Vehiculos), IconUnicode = "\uf1b9" },
                },
                ["Técnicos"] = new List<Models.MenuItem>
                {
                    new Models.MenuItem { Title = "CheckList", TargetType = typeof(Views.CheckList), IconUnicode = "\uf00c" },
                    new Models.MenuItem { Title = "Servicio", TargetType = typeof(Views.CheckListServicio), IconUnicode = "\uf013" },
                },
                ["Jefe de Taller"] = new List<Models.MenuItem>
                {
                    new Models.MenuItem { Title = "Empleados", TargetType = typeof(Views.Empleados), IconUnicode = "\uf0c0" },
                    new Models.MenuItem { Title = "CheckList", TargetType = typeof(Views.CheckList), IconUnicode = "\uf00c" },
                    new Models.MenuItem { Title = "Servicio", TargetType = typeof(Views.CheckListServicio), IconUnicode = "\uf013" },
                    new Models.MenuItem { Title = "Asignar Orden Servicio", TargetType = typeof(Views.FormularioAsignarOrden), IconUnicode = "\uf0ae" }
                }
            };

            MenuItems.Clear();

            if (opcionesPorRol.TryGetValue(rolUsuario, out var opciones))
            {
                foreach (var item in opciones)
                    MenuItems.Add(item);
            }
            else
            {
                MenuItems.Add(new Models.MenuItem
                {
                    Title = "Sin permisos",
                    TargetType = typeof(ContentPage),
                    
                });
            }
        }

        public async Task<string> ObtenerRolEmpleadoAsync(int idUsuario)
        {
            try
            {
                var empleados = await _apiService.GetAsync<List<Models.Empleados>>($"api/empleado?IdUsuario={idUsuario}");

                Empleado = empleados?.FirstOrDefault();

                if (Empleado?.Rol != null)
                {
                    switch (Empleado.Rol)
                    {
                        case 1: return "Administrador";
                        case 2: return "Almacén";
                        case 3: return "Técnicos";
                        case 4: return "Jefe de Taller";
                        default: return "Desconocido";
                    }
                }
                else
                {
                    return "Desconocido";
                }
    
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener el rol del empleado: {ex.Message}");
                return "Desconocido";
            }
        }

        private void OnLogout()
        {
            SecureStorage.Remove("access_token");
            SecureStorage.Remove("idUsuario");

            Application.Current.MainPage = new NavigationPage(new LoginPage());
        }
    }
}
