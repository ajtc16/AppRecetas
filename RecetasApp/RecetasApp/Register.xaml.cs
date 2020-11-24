using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
using Newtonsoft.Json;

using Xamarin.Forms;

namespace RecetasApp
{
    public partial class Register : ContentPage
    {
        private const string Url = "http://192.168.100.149:8081/apiRecetas/rest/UserService/";
        private readonly HttpClient client = new HttpClient();
        private ObservableCollection<Modelo.User> usuario;

        public Register()
        {
            InitializeComponent();
        }

        public async void btnGuardar_Clicked(System.Object sender, System.EventArgs e)
        {
            String nombre = txtNombre.Text;
            String apellido = txtApellido.Text;
            String usuario = txtUsuario.Text;
            String mail = txtMail.Text;
            String password = txtUsuarioPassword.Text;



            String llamada = Url + "saveUser/" + nombre+"/"+apellido+"/"+ mail + "/"+usuario + "/"+password;
            var content = await client.GetStringAsync(llamada);
            // var obj = JsonConvert.DeserializeObject<String>(content);
            await DisplayAlert("Mensaje", "Usuario Creado Exitosamente", "ok");
            await Navigation.PushAsync(new Login());
        }

       public async void btnRegresar_Clicked(System.Object sender, System.EventArgs e)
        {
            await Navigation.PushAsync(new Login());
        }
    }
}
