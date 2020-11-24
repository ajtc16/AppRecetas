using System;
using System.Collections.Generic;
using RecetasApp.Modelo;
using System.Net.Http;
using Newtonsoft.Json;


using Xamarin.Forms;
using System.Collections.ObjectModel;

namespace RecetasApp
{
    public partial class Receta : ContentPage
    {
        private const string Url = "http://192.168.100.149:8081/apiRecetas/rest/RecipeService/";
        private readonly HttpClient client = new HttpClient();
        private ObservableCollection<Modelo.Step> nota;
        private ObservableCollection<Modelo.Ingrediente> ingrediente;
        private User usuarioLogeado;

        public Receta(Recipe recipe, User user)
        {
            InitializeComponent();
            usuarioLogeado = user;
            txtNombreReceta.Text = recipe.name;
            txtSkillReceta.Text = recipe.skillLevel.ToString();
            recuperaIngredientes(recipe.id);
            recuperaPasos(recipe.id);
        }

        public async void recuperaIngredientes(int idReceta)
        {
            String llamada = Url + "getIngredientsByRecipeID/" + idReceta;
            var content = await client.GetStringAsync(llamada);
            List<Modelo.Ingrediente> ingredientes = JsonConvert.DeserializeObject<List<Modelo.Ingrediente>>(content);
            ingrediente = new ObservableCollection<Modelo.Ingrediente>(ingredientes);
            MyListViewIngredientes.ItemsSource = ingrediente;
        }
        public async void recuperaPasos(int idReceta)
        {
            String llamada = Url + "getStepsByRecipeId/" + idReceta;
            var content = await client.GetStringAsync(llamada);
            List<Modelo.Step> notas = JsonConvert.DeserializeObject<List<Modelo.Step>>(content);
            nota = new ObservableCollection<Modelo.Step>(notas);
            MyListViewPasos.ItemsSource = nota;
        }

       public async void btnInicio_Clicked(System.Object sender, System.EventArgs e)
        {
            // await Navigation.PushAsync(new Home(usuario));
            await Navigation.PushAsync(new Home(usuarioLogeado));
        }

        public async void btnPerfil_Clicked(System.Object sender, System.EventArgs e)
        {
            await Navigation.PushAsync(new Perfil(usuarioLogeado));
        }

        public async void btnIngredientes_Clicked(System.Object sender, System.EventArgs e)
        {
            await Navigation.PushAsync(new IngredientePage(usuarioLogeado));
        }

        
    }
}
