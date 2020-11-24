using System;
namespace RecetasApp.Modelo
{
    public class User
    {

        public int id { get; set; }
        public string name { get; set; }
        public string lastName { get; set; }
        public string mail { get; set; }
        public string userName { get; set; }
        public string userPassword { get; set; }
        public byte[] userImage { get; set; }


    }
}
