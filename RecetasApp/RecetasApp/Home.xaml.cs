using System;
using System.Collections.Generic;
using RecetasApp.Modelo;
using System.Net.Http;
using Newtonsoft.Json;


using Xamarin.Forms;
using System.Collections.ObjectModel;

namespace RecetasApp
{
    public partial class Home : ContentPage
    {

        private const string Url = "http://192.168.100.149:8081/apiRecetas/rest/RecipeService/";
        private readonly HttpClient client = new HttpClient();
        private ObservableCollection<Modelo.Recipe> receta;
        private User usuarioLogeado;


        public Home(User user)
        {
            InitializeComponent();
            usuarioLogeado = user;
            recuperaRecetas();
            
        }
        public async void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            Console.WriteLine("ingreso tap");


            var selectedItem = (Recipe)MyListView.SelectedItem;
            Recipe recipe = selectedItem;
            int idReceta = (int)selectedItem.id;

            await Navigation.PushAsync(new Receta(recipe, usuarioLogeado));

            // txtCodigo.Text = selectedItem.codigo.ToString();
            // txtNombre.Text = selectedItem.nombre.ToString();
            // txtApellido.Text = selectedItem.apellido.ToString();
            // txtEdad.Text = selectedItem.edad.ToString();

        }

        public async void recuperaRecetas() {
            String llamada = Url + "getAllRecipes/";
            var content = await client.GetStringAsync(llamada);
            List<Modelo.Recipe> recetas = JsonConvert.DeserializeObject<List<Modelo.Recipe>>(content);
            receta = new ObservableCollection<Modelo.Recipe>(recetas);
            MyListView.ItemsSource = receta;

        }

        public async void btnPerfil_Clicked(System.Object sender, System.EventArgs e)
        {
            await Navigation.PushAsync(new Perfil(usuarioLogeado));
        }

        public async void btnIngredientes_Clicked(System.Object sender, System.EventArgs e)
        {
            await Navigation.PushAsync(new IngredientePage(usuarioLogeado));
        }

        public async void btnBuscar_Clicked(System.Object sender, System.EventArgs e)
        {
            String llamada = Url + "getRecipeByName/"+txtBuscarReceta.Text;
            var content = await client.GetStringAsync(llamada);
            List<Modelo.Recipe> recetas = JsonConvert.DeserializeObject<List<Modelo.Recipe>>(content);
            receta = new ObservableCollection<Modelo.Recipe>(recetas);
            MyListView.ItemsSource = receta;
        }



    }
}
