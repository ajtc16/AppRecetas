using System;
using System.Collections.Generic;
using RecetasApp.Modelo;
using System.Net.Http;
using Newtonsoft.Json;


using Xamarin.Forms;
using System.Collections.ObjectModel;

namespace RecetasApp
{
    public partial class IngredientePage : ContentPage
    {
        private const string Url = "http://192.168.100.149:8081/apiRecetas/rest/RecipeService/";
        private readonly HttpClient client = new HttpClient();
        
        private ObservableCollection<Modelo.Ingrediente> ingrediente;
        private User usuarioLogeado;

        public IngredientePage(User user)
        {
            InitializeComponent();
            usuarioLogeado = user;
            recuperaIngredientes();
        }



        public async void recuperaIngredientes()
        {
            String llamada = Url + "getAllIngredients" ;
            var content = await client.GetStringAsync(llamada);
            List<Modelo.Ingrediente> ingredientes = JsonConvert.DeserializeObject<List<Modelo.Ingrediente>>(content);
            ingrediente = new ObservableCollection<Modelo.Ingrediente>(ingredientes);
            MyListView.ItemsSource = ingrediente;
        }

        public async void btnActualizar_Clicked(System.Object sender, System.EventArgs e)
        {

            var ingre = (sender as Button).CommandParameter as Ingrediente;

            String name = ingre.name;
            String unit = ingre.unit;
            String id = ingre.id.ToString();


            String llamada = Url + "updateIngredient/" + id + "/" + name + "/" + unit;
            var content = await client.GetStringAsync(llamada);
            //var obj = JsonConvert.DeserializeObject<String>(content);

            await DisplayAlert("Mensaje", "Ingrediente Actualizado", "ok");
            recuperaIngredientes();

        }
        public async void btnCrear_Clicked(System.Object sender, System.EventArgs e)
        {
            // var ingre = (sender as Button).CommandParameter as Ingrediente;
            String name = txtNewName.Text;
            String unit = txtNewUnit.Text;

            if (name != "" && unit != "")
            {
                String llamada = Url + "saveIngredient/" + name + "/" + unit;
                var content = await client.GetStringAsync(llamada);
                //var obj = JsonConvert.DeserializeObject<String>(content);

                txtNewName.Text = "";
                txtNewUnit.Text = "";


               await  DisplayAlert("Mensaje", "Ingrediente creado", "ok");
                recuperaIngredientes();
            }
            else {

               await DisplayAlert("Mensaje", "Por favor ingrese los datos para crear un ingrediente" , "ok");

            }




        }
       



    }
}
