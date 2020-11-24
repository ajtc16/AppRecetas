using System;
namespace RecetasApp.Modelo
{
    public class Step
    {

        public int id { get; set; }
        public int idRecipe { get; set; }
        public int orderLevel { get; set; }
        public String text { get; set; }
    }
}
