using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
using Newtonsoft.Json;

using Xamarin.Forms;

namespace RecetasApp
{
    public partial class Login : ContentPage
    {

        private const string Url = "http://192.168.100.149:8081/apiRecetas/rest/UserService/";
        private readonly HttpClient client = new HttpClient();
        //private ObservableCollection<Modelo.User> usuario;

        public Login()
        {
            InitializeComponent();
        }

        public async void btnLogin_Clicked(System.Object sender, System.EventArgs e)
        {

            String llamada = Url + "getUserByEmail/" + txtUsuario.Text;
            var content = await client.GetStringAsync(llamada);
            var obj = JsonConvert.DeserializeObject<Modelo.User>(content);
            Modelo.User usuario = JsonConvert.DeserializeObject<Modelo.User>(content);
            //_post = new ObservableCollection<Modelo.User>(posts);

            
            


            String usuarioAut = usuario.mail; 
            String passAut = usuario.userPassword;


            String user = txtUsuario.Text;
            String password = txtPassword.Text;

            if (usuarioAut.Equals(user))
            {

                if (passAut.Equals(password))
                {

                    DisplayAlert("Bienvenido", "Se ha autenticado Exitosamente " + user, "ok");
                    await Navigation.PushAsync(new Home(usuario));
                }
                else
                {
                    DisplayAlert("Mensaje", "Contraseña incorrecta para usario " + user, "ok");
                }


            }
            else
            {
                DisplayAlert("Mensaje", "Usuario incorrecto" + user, "ok");

            }
        }

        public async void btnRegister_Clicked(System.Object sender, System.EventArgs e)
        {
            await Navigation.PushAsync(new Register() );
        }

        public async void btnFacebook_Clicked(System.Object sender, System.EventArgs e)
        {
            await Navigation.PushAsync(new LoginFB());
        }
    }
}
