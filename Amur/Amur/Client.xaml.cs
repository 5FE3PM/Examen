using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Amur
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Client : ContentPage
    {
        HttpClient client = new HttpClient();
        public Client()
        {
            InitializeComponent();
        }

        private async void signUp(object sender, EventArgs e)
        {
            String fullname = $"{firstName.Text.Trim()} {lastName.Text.Trim()}";
            int regionId = 1;
            switch (region.SelectedIndex)
            {
                case 0:
                    {
                        regionId = 1;
                        break;
                    }
                case 1:
                    {
                        regionId = 2;
                        break;
                    }
            }
            string jsonData = $"{{\"fullname\": \"{fullname}\", \"phone\": \"{phoneNumber.Text}\", \"email\": \"{email.Text}\", \"password\": \"{password.Text}\", \"firstAddress\": \"{street.Text}\", \"secondAddress\": \"{neighborhood.Text}\", \"regionId\": {regionId}, \"validationImage\": \"{ine.Text}\" }}";
            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
            Console.WriteLine("=================================");
            Console.WriteLine(jsonData);
            Console.WriteLine("=================================");
            HttpResponseMessage response = await client.PostAsync("http://142.93.3.253:3000/clients", content);
            string result = await response.Content.ReadAsStringAsync();
            Console.WriteLine(result);
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

        /*async private void accessGallery(object sender, EventArgs e)
        {
            var result = await MediaPicker.PickPhotoAsync(new MediaPickerOptions
            { 
                Title = "Selecciona tu IFE"
            });
            if(result != null)
            {
                var stream = await result.OpenReadAsync();
                resultImage.Source = ImageSource.FromStream(() => stream);
            }
        }
        
        async private void capturePhoto(object sender, EventArgs e)
        {
            var result = await MediaPicker.CapturePhotoAsync(new MediaPickerOptions
            {
                Title = "Captura tu IFE"
            });
            if (result != null)
            {
                var newFile = Path.Combine(FileSystem.CacheDirectory, result.FileName);
                Console.WriteLine("======================================");
                Console.WriteLine(result.FileName);
            }
        }*/
    }
}