using System;
using System.Collections.Generic;
using RecetasApp.Modelo;
using System.Net.Http;
using Newtonsoft.Json;


using Xamarin.Forms;
using System.Collections.ObjectModel;

namespace RecetasApp
{
    public partial class Perfil : ContentPage
    {
        private const string Url = "http://192.168.100.149:8081/apiRecetas/rest/UserService/";
        private readonly HttpClient client = new HttpClient();
        private ObservableCollection<User> usuario;
        private String idUsuario;
        private User usuarioLogeado;

        public Perfil(User usuario)
        {
            InitializeComponent();
            usuarioLogeado = usuario;
            txtNombre.Text = usuario.name;
            txtApellido.Text = usuario.lastName;
            txtUsuario.Text = usuario.userName;
            txtMail.Text = usuario.mail;
            txtUsuarioPassword.Text = usuario.userPassword;
            idUsuario = usuario.id.ToString();


        }

        public async void btnInicio_Clicked(System.Object sender, System.EventArgs e)
        {
            // await Navigation.PushAsync(new Home(usuario));
            await Navigation.PushAsync(new Home(usuarioLogeado));
        }

       public async void btnIngredientes_Clicked(System.Object sender, System.EventArgs e)
        {
            await Navigation.PushAsync(new IngredientePage(usuarioLogeado));
        }

       

        public async void btnActualizar_Clicked(System.Object sender, System.EventArgs e)
        {
            String id = idUsuario;
            String name = txtNombre.Text;
            String lastName = txtApellido.Text;
            String userName = txtUsuario.Text;
            String password = txtUsuarioPassword.Text;


            String llamada = Url + "updateUser/" + id + "/" + name + "/" + lastName + "/" + userName + "/" + password;
            var content = await client.GetStringAsync(llamada);
            String respuesta = content;
            await DisplayAlert("Exito", "Se ha Actualizado sus datos " + respuesta, "ok");
            // ingrediente = new ObservableCollection<Modelo.Ingrediente>(usuarios);
            // MyListViewIngredientes.ItemsSource = ingrediente;
        }
    }
}
