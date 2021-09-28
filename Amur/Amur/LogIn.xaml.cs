using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Amur
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LogIn : ContentPage
    {
        HttpClient client = new HttpClient();
        public LogIn()
        {
            InitializeComponent();
            selectionView.ItemsSource = new[]
            {
                "Cliente", "Proveedor"
            };
            selectionView.SelectedIndex = 0;
        }

        async private void logIn(object sender, EventArgs e)
        {
            string url;
            if(selectionView.SelectedIndex == 0)
			{
                url = "http://142.93.3.253:3000/auth/client";
            } else
			{
                url = "http://142.93.3.253:3000/auth/provider";

            }
            string jsonData = $"{{ \"email\": \"{email.Text}\", \"password\": \"{password.Text}\" }}";
            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PostAsync(url, content);
            string result = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                await DisplayAlert("Bienvenido", "Iniciaste sesion", "Ok");
                Home homePage = new Home();
                await Navigation.PushAsync(homePage);
            }
            else
            {
                await DisplayAlert("Error", "Revisa tus datos", "Ok");
            }
        }

        private void signUp(object sender, EventArgs e)
        {
            Navigation.PushAsync(new SignUp());
        }
    }
}