using System;
using System.Collections.Generic;
using System.Net.Http;
using Newtonsoft.Json;
using RecetasApp.Modelo;
using Xamarin.Forms;

namespace RecetasApp
{
    public partial class LoginFB : ContentPage
    {
        private const string Url = "http://192.168.100.149:8081/apiRecetas/rest/UserService/";
        private readonly HttpClient client = new HttpClient();
        //private ObservableCollection<Modelo.User> usuario;

        public LoginFB()
        {
            InitializeComponent();
        }

        void btnLoginFb_Clicked(System.Object sender, System.EventArgs e)
        {
            webView.Source = "https://www.facebook.com/v7.0/dialog/oauth?client_id=225478619005006&response_type=token&redirect_uri=https://www.facebook.com/connect/login_success.html";
            webView.Navigated += WebView_Navigated;
        }

        private async void WebView_Navigated(object sender, WebNavigatedEventArgs e)
        {
            var AccessURL = e.Url;
            if (AccessURL.Contains("access_token"))
            { 
                AccessURL = AccessURL.Replace("https://www.facebook.com/connect/login_success.html#access_token=",string.Empty);
                var accesstoken = AccessURL.Split('&')[0].ToString();
                //HttpClient client = new HttpClient();
               var response= client.GetStringAsync("https://graph.facebook.com/me?fields=email,name,picture&access_token=" + accesstoken).Result;

                var Data = JsonConvert.DeserializeObject<FacebookProfile>(response);

                imgPic.Source = Data.picture.Data.Url;
                Name.Text = Data.Name;
                Email.Text = Data.Email;
                String defecto = "Default";

                String nombre = Data.Name;
                String apellido = defecto;
                String usuario = Data.Email;
                String mail = Data.Email;
                String password = "123456";

                if ("".Equals(nombre) || nombre == null) { nombre = defecto; }
                if ("".Equals(usuario) || usuario==null) { usuario = defecto; }
                if ("".Equals(mail) || mail == null) { mail = defecto; }
                

                String llamada = Url + "saveUser/" + nombre + "/" + apellido + "/" + mail + "/" + usuario + "/" + password;
                var content1 = client.GetStringAsync(llamada).Result;

                if (mail.Equals(defecto))
                {
                    DisplayAlert("Error", "Estimado Usuario ponerse en contacto con el ADministrador, para actulizar sus datos ", "ok");
                }
                else {
                    DisplayAlert("Exito", "Estimado Usuario su contraseña para ingresar es " + password +" por favor cambiarla en el perfil", "ok");
                }
            }


                await Navigation.PushAsync(new Login());

            
        }
    }
}
