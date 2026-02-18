using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json; //
using AppMovilTrotaMundos.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using System.Diagnostics;
using System.Text.Json;
namespace AppMovilTrotaMundos.Services
{
    public class ApiService
	{
		private readonly HttpClient _httpClient;

		public ApiService()
		{
			_httpClient = new HttpClient();
			_httpClient.BaseAddress = new Uri("http://67.205.148.136:8000/"); // Cambia esto a la URL base de tu API
			_httpClient.Timeout = TimeSpan.FromMinutes(30); 
			_httpClient.DefaultRequestHeaders.Accept.Clear();
			_httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
		}

		public async Task<HttpResponseMessage> GetRawAsync(string url)
		{
			using (var client = new HttpClient())
			{
				client.BaseAddress = new Uri("http://67.205.148.136:8000");
				var response = await client.GetAsync(url);
				response.EnsureSuccessStatusCode(); // Lanza una excepción si el código de estado no es exitoso
				return response;
			}
		}

        public async Task DeleteAsync(string endpoint)
        {
            try
            {
                var response = await _httpClient.DeleteAsync(endpoint);

                if (!response.IsSuccessStatusCode)
                {
                    var errorMessage = await response.Content.ReadAsStringAsync();
                    throw new Exception($"Error al eliminar: {response.StatusCode} - {errorMessage}");
                }
            }
            catch (HttpRequestException ex)
            {
                throw new Exception("No se pudo conectar al servidor. Verifica tu conexión a Internet.", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Ocurrió un error al procesar la solicitud.", ex);
            }
        }

        public async Task PutAsync<T>(string endpoint, T data)
        {
            try
            {
                var json = JsonConvert.SerializeObject(data);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await _httpClient.PutAsync(endpoint, content);

                if (!response.IsSuccessStatusCode)
                {
                    var errorMessage = await response.Content.ReadAsStringAsync();
                    throw new Exception($"Error al actualizar: {response.StatusCode} - {errorMessage}");
                }
            }
            catch (HttpRequestException ex)
            {
                throw new Exception("No se pudo conectar al servidor. Verifica tu conexión a Internet.", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Ocurrió un error al procesar la solicitud.", ex);
            }
        }

        public async Task<T> GetAsyncEmpleados<T>(string endpoint)
		{
			var response = await _httpClient.GetAsync(endpoint);

			if (response.IsSuccessStatusCode)
			{
				var jsonResponse = await response.Content.ReadAsStringAsync();
				return JsonConvert.DeserializeObject<T>(jsonResponse);
			}

			throw new Exception($"Error en la llamada a la API: {response.StatusCode}");
		}

		public async Task<T> GetTVehiculo<T> (string endpoint)
		{
			var response = await _httpClient.GetAsync(endpoint);

			if (response.IsSuccessStatusCode)
			{
				var jsonResponse = await response.Content.ReadAsStringAsync();

				// Configura el convertidor para manejar las propiedades de imágenes
				var settings = new JsonSerializerSettings
				{
					NullValueHandling = NullValueHandling.Ignore,
					Converters = new List<JsonConverter> { new StringToListConverter() }
				};

				return JsonConvert.DeserializeObject<T>(jsonResponse, settings);
			}

			throw new Exception($"Error en la llamada a la API: {response.StatusCode}");
		}

        public async Task<T> GetAsynEmp<T>(string endpoint)
        {
            try
            {
                var response = await _httpClient.GetAsync(endpoint);

                if (response.IsSuccessStatusCode)
                {
                    var jsonResponse = await response.Content.ReadAsStringAsync();

                    try
                    {
                        // Intentar deserializar la respuesta
                        return JsonConvert.DeserializeObject<T>(jsonResponse);
                    }
                    catch (System.Text.Json.JsonException jsonEx)
                    {
                        throw new Exception("Error al deserializar la respuesta JSON", jsonEx);
                    }
                }
                else
                {
                    throw new Exception($"Error en la llamada a la API: {response.StatusCode} - {response.ReasonPhrase}");
                }
            }
            catch (Exception ex)
            {
                // Capturar cualquier otro error (ej. de red)
                throw new Exception("Error en la solicitud HTTP", ex);
            }
        }

        public async Task<T> GetAsync<T>(string endpoint)
		{
			var response = await _httpClient.GetAsync(endpoint);

			if (response.IsSuccessStatusCode)
			{
				var jsonResponse = await response.Content.ReadAsStringAsync();
				return JsonConvert.DeserializeObject<T>(jsonResponse);
			}

			throw new Exception($"Error en la llamada a la API: {response.StatusCode}");
		}

		public async Task ObtenerEmpleados(Models.Empleados empleado)
		{
			var json = JsonConvert.SerializeObject(empleado);
			var content = new StringContent(json, Encoding.UTF8, "application/json");
			var response = await _httpClient.PostAsync("api/empleado", content);

			if (!response.IsSuccessStatusCode)
			{
				throw new Exception($"Error al agregar el cliente: {response.ReasonPhrase}");
			}
		}

		public async Task AddClienteAsync(Models.Clientes cliente)
		{
			var json = JsonConvert.SerializeObject(cliente);
			var content = new StringContent(json, Encoding.UTF8, "application/json");
			var response = await _httpClient.PostAsync("api/cliente", content);

			if (!response.IsSuccessStatusCode)
			{
				throw new Exception($"Error al agregar el cliente: {response.ReasonPhrase}");
			}
		}

        public class ClienteExisteResponse
        {
            [JsonProperty("existe")]
            public bool Existe { get; set; }
        }
        public async Task<bool> ClienteYaExisteAsync(string nombre, string email)
        {
            using (var client = new HttpClient())
            {
                var url = $"http://67.205.148.136:8000/api/clienteexiste?nombre={Uri.EscapeDataString(nombre)}&email={Uri.EscapeDataString(email)}";
                var response = await client.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();

                    // Deserializa como objeto
                    var result = JsonConvert.DeserializeObject<ClienteExisteResponse>(json);
                    return result != null && result.Existe;
                }

                // Si el API falla, asumimos que no existe (o puedes lanzar una excepción)
                return false;
            }
        }



        public async Task AddFlotillaAsync(Models.Flotillas flotilla)
        {
            var json = JsonConvert.SerializeObject(flotilla);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("api/flotilla", content);

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Error al agregar la flotilla: {response.ReasonPhrase}");
            }
        }


        public async Task<bool> UpdateVehiculoAsync(Models.Vehiculos vehiculo)
        {
            try
            {
                // Serializar el objeto vehiculo a JSON
                var json = JsonConvert.SerializeObject(vehiculo);



                // Crear el contenido de la solicitud
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                // Realiza la solicitud PUT
                var response = await _httpClient.PutAsync("api/vehiculo", content); // Asegúrate de que sea PUT, no POST

                // Maneja la respuesta
                if (response.IsSuccessStatusCode)
                {
                    return true; // Si la solicitud fue exitosa, devuelve true
                }
                else
                {
                    // Si no fue exitosa, maneja el error
                    throw new Exception($"Error al actualizar el vehiculo: {response.ReasonPhrase}");
                }
            }
            catch (Exception ex)
            {
                // Si ocurre algún error en la solicitud, maneja la excepción
                Debug.WriteLine($"Error al realizar la solicitud: {ex.Message}");
                return false; // En caso de error, devuelve false
            }
        }


        public async Task<bool> UpdateCheckAsync(Models.CheckList checklist)
        {
            try
            {
                // Serializar el objeto vehiculo a JSON
                var json = JsonConvert.SerializeObject(checklist);

       

                // Crear el contenido de la solicitud
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                // Realiza la solicitud PUT
                var response = await _httpClient.PutAsync("api/checklist", content); // Asegúrate de que sea PUT, no POST

                // Maneja la respuesta
                if (response.IsSuccessStatusCode)
                {
                    return true; // Si la solicitud fue exitosa, devuelve true
                }
                else
                {
                    // Si no fue exitosa, maneja el error
                    throw new Exception($"Error al actualizar el checklist: {response.ReasonPhrase}");
                }
            }
            catch (Exception ex)
            {
                // Si ocurre algún error en la solicitud, maneja la excepción
                Debug.WriteLine($"Error al realizar la solicitud: {ex.Message}");
                return false; // En caso de error, devuelve false
            }
        }

        public async Task<bool> UpdateCheckServicioAsync(Models.CheckListServicio checklist)
        {
            try
            {
                // Serializar el objeto vehiculo a JSON
                var json = JsonConvert.SerializeObject(checklist);



                // Crear el contenido de la solicitud
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                // Realiza la solicitud PUT
                var response = await _httpClient.PutAsync("api/checklist", content); // Asegúrate de que sea PUT, no POST

                // Maneja la respuesta
                if (response.IsSuccessStatusCode)
                {
                    return true; // Si la solicitud fue exitosa, devuelve true
                }
                else
                {
                    // Si no fue exitosa, maneja el error
                    throw new Exception($"Error al actualizar el checklist: {response.ReasonPhrase}");
                }
            }
            catch (Exception ex)
            {
                // Si ocurre algún error en la solicitud, maneja la excepción
                Debug.WriteLine($"Error al realizar la solicitud: {ex.Message}");
                return false; // En caso de error, devuelve false
            }
        }




       




        public async Task AddVehiculoAsync(Models.Vehiculos vehiculo)
        {
            var json = JsonConvert.SerializeObject(vehiculo);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            // Realiza la solicitud POST
            var response = await _httpClient.PostAsync("api/vehiculo", content);

            // Maneja la respuesta
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Error al agregar el vehiculo: {response.ReasonPhrase}");
            }
        }

        public async Task AsignarTecnicoAOrdenAsync(int idOrden, int idTecnico)
        {
            // Crear un objeto con los datos necesarios para la asignación
            var ordenAsignada = new
            {
                IdOrden = idOrden,
                IdTecnico = idTecnico
            };

            // Serializar el objeto a formato JSON
            var json = JsonConvert.SerializeObject(ordenAsignada);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            // Realiza la solicitud POST al endpoint correspondiente
            var response = await _httpClient.PostAsync("api/asignarTecnicoAOrden", content);

            // Manejo de la respuesta
            if (response.IsSuccessStatusCode)
            {
                // Si la respuesta es exitosa, puedes leer el contenido de la respuesta (por ejemplo, un mensaje)
                var responseContent = await response.Content.ReadAsStringAsync();

                // Aquí puedes deserializar la respuesta si es necesario
                var respuesta = JsonConvert.DeserializeObject<ResponseModel>(responseContent);

                // Si quieres hacer algo con la respuesta, como mostrar un mensaje
                if (respuesta.id_resultado == 1)
                {
                    // Lógica para éxito
                    Console.WriteLine("Orden asignada correctamente: " + respuesta.respuesta);
                }
                else
                {
                    // Lógica para manejar un error o respuesta inesperada
                    Console.WriteLine("Hubo un problema con la asignación.");
                }
            }
            else
            {
                // Si la respuesta no es exitosa, lanzar una excepción con el mensaje de error
                throw new Exception($"Error al asignar el técnico a la orden: {response.ReasonPhrase}");
            }
        }


        public async Task AddEmpeladoAsync(Models.Empleados empleados)
		{
			var json = JsonConvert.SerializeObject(empleados);
			var content = new StringContent(json, Encoding.UTF8, "application/json");
			var response = await _httpClient.PostAsync("api/usuarios", content);

			if (!response.IsSuccessStatusCode)
			{
				throw new Exception($"Error al agregar el vehiculo: {response.ReasonPhrase}");
			}
		}



		public async Task<LoginResponse> LoginAsync(string username, string password)
		{
			var loginRequest = new
			{
				usuario = username,
				contrasena = password
			};

			var json = JsonConvert.SerializeObject(loginRequest);
			var content = new StringContent(json, Encoding.UTF8, "application/json");

			var response = await _httpClient.PostAsync("api/seguridad/iniciarsesion", content);

			if (response.IsSuccessStatusCode)
			{
				var jsonResponse = await response.Content.ReadAsStringAsync();
				var loginResponse = JsonConvert.DeserializeObject<LoginResponse>(jsonResponse);

				// Decodifica el token JWT
				var handler = new JwtSecurityTokenHandler();
				var jwtToken = handler.ReadJwtToken(loginResponse.access_token);

				// Extrae el idUsuario del token
				var idUsuarioClaim = jwtToken.Claims.FirstOrDefault(c => c.Type == "idUsuario");
				if (idUsuarioClaim != null)
				{
					loginResponse.idUsuario = int.Parse(idUsuarioClaim.Value);
					AppSettings.IdUsuario = loginResponse.idUsuario;
				}

				return loginResponse;
			}
			else
			{
				var errorResponse = await response.Content.ReadAsStringAsync();
				throw new Exception($"Error en la llamada a la API: {response.StatusCode}, Detalles: {errorResponse}");
			}
		}

		public async Task AddChecklistAsync(Models.CheckList checklist)
		{
			var json = JsonConvert.SerializeObject(checklist);
			var content = new StringContent(json, Encoding.UTF8, "application/json");
			var response = await _httpClient.PostAsync("api/checklist", content);

			if (!response.IsSuccessStatusCode)
			{
				var errorResponse = await response.Content.ReadAsStringAsync();
				throw new Exception($"Error al agregar el checklist: {response.ReasonPhrase}, Detalles: {errorResponse}");
			}
		}

        public async Task AddHistoricoAsync(Historico historico)
        {
            try
            {
                // Serializa el objeto Historico a JSON
                var json = JsonConvert.SerializeObject(historico);

                // Crea el contenido de la solicitud con el JSON
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                // Realiza la solicitud POST al endpoint correspondiente
                var response = await _httpClient.PostAsync("api/insertar_historico", content);

                // Verifica si la respuesta fue exitosa
                if (!response.IsSuccessStatusCode)
                {
                    // Si hubo un error, lee la respuesta de error y lanza una excepción
                    var errorResponse = await response.Content.ReadAsStringAsync();
                    throw new Exception($"Error al insertar el histórico: {response.ReasonPhrase}, Detalles: {errorResponse}");
                }
            }
            catch (Exception ex)
            {
                // Maneja cualquier error en la llamada
                throw new Exception("Error al enviar los datos del histórico", ex);
            }
        }



        public async Task AddServicioAsync(Models.CheckListServicio checklist)
        {
            var json = JsonConvert.SerializeObject(checklist);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("api/servicio", content);

            if (!response.IsSuccessStatusCode)
            {
                var errorResponse = await response.Content.ReadAsStringAsync();
                throw new Exception($"Error al agregar el checklist: {response.ReasonPhrase}, Detalles: {errorResponse}");
            }
        }

        public async Task<T> GetAsync2<T>(string endpoint)
		{
			var response = await _httpClient.GetAsync(endpoint);

			if (response.IsSuccessStatusCode)
			{
				var jsonResponse = await response.Content.ReadAsStringAsync();
				return JsonConvert.DeserializeObject<T>(jsonResponse);
			}

			throw new Exception($"Error en la llamada a la API: {response.StatusCode}");
		}



        public async Task<int> ObtenerNuevoIdOrdenAsync()
        {

			var response = await _httpClient.GetAsync("api/obteneridOrden");
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();

                    // Deserializa el JSON para acceder a "NuevoId"
                    JObject jsonResponse = JObject.Parse(content);
                    int nuevoId = (int)jsonResponse["NuevoId"]; // Accede a la clave "NuevoId"

                    return nuevoId;
                }
                else
                {
                    throw new Exception($"Error al obtener el ID: {response.ReasonPhrase}");
                }
            
        }


        public async Task<List<OrdenDeServicio>> GetOrdenesDeServicioAsync(int idCliente)
        {
            try
            {
                string url = $"obtener_orden_service?idCliente={idCliente}";  // Ajusta la URL a tu API
                var response = await _httpClient.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    string json = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<List<OrdenDeServicio>>(json);
                }
                else
                {
                    Debug.WriteLine($"Error al obtener órdenes: {response.StatusCode}");
                    return new List<OrdenDeServicio>();
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Excepción en GetOrdenesDeServicioAsync: {ex.Message}");
                return new List<OrdenDeServicio>();
            }
        }



        public async Task<byte[]> DescargarOrdenDeServicioAsync(int idCliente)
        {
            try
            {
                var url = $"generate_and_download_orden/?id_cliente={idCliente}";
                var response = await _httpClient.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadAsByteArrayAsync();
                }
                else
                {
                    throw new Exception($"Error: {response.StatusCode}");
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error descargando el archivo: {ex.Message}");
            }
        }

    }
}
