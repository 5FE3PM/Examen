using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Amur
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Provider : ContentPage
    {
        ObservableCollection<Category> categories;
        HttpClient client = new HttpClient();
        private bool categoryAdded = false;
        public Provider()
        {
            InitializeComponent();
        }
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await FetchCategories();
        }
        public async Task FetchCategories()
        {
            string url = "http://142.93.3.253:3000/categories";
            Uri uri = new Uri(string.Format(url, string.Empty));
            HttpResponseMessage response = await client.GetAsync(uri);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                categories = JsonConvert.DeserializeObject<ObservableCollection<Category>>(content);
                categoriesList.ItemsSource = categories;
            }
        }
        //Registrarse
        async private void signUp(object sender, EventArgs e)
        {
            Category categorySelected = categoriesList.SelectedItem as Category;
            string fullname = $"{firstName.Text.Trim()} {lastName.Text.Trim()}";
            int categoryId = categorySelected.Id;
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
            string card = cardCheck.IsChecked ? "true" : "false";
            string cash = cashCheck.IsChecked ? "true" : "false";
            string products = productsCheck.IsChecked ? "true" : "false";
            string services = servicesCheck.IsChecked ? "true" : "false";
            string jsonData = $"{{\"card\": \"{card}\", \"cash\": \"{cash}\", \"sellProducts\": \"{products}\", \"offersServices\": \"{services}\", \"fullname\": \"{fullname}\", \"phone\": \"{phoneNumber.Text}\", \"email\": \"{email.Text}\", \"password\": \"{password.Text}\", \"firstAddress\": \"{street.Text}\", \"secondAddress\": \"{neighborhood.Text}\", \"regionId\": {regionId}, \"businessName\": \"{businessName.Text}\", \"businessLogo\": \"{businessLogo.Text}\", \"validationImage\": \"{ine.Text}\", \"businessCategoryId\": {categoryId} }}";
            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PostAsync("http://142.93.3.253:3000/providers", content);
            string result = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                await DisplayAlert("Bienvenido", "Iniciaste sesion", "Ok");
                Home homePage = new Home();
                await Navigation.PushAsync(homePage);
            }
            else
            {
                Console.WriteLine("===========================================");
                await DisplayAlert("Error", result.ToString(), "Ok");
            }
        }
        
        //Credencial del IFE Galería
        /*async private void Button_Clicked_1(object sender, EventArgs e)
        {
            var result = await MediaPicker.PickPhotoAsync(new MediaPickerOptions
            {
                Title = "Selecciona tu IFE"
            });
            if (result != null)
            {
                var stream = await result.OpenReadAsync();
                resultImage.Source = ImageSource.FromStream(() => stream);
            }
        }

        async private void Button_Clicked_3(object sender, EventArgs e)
        {
            var result = await MediaPicker.CapturePhotoAsync(new MediaPickerOptions
            {
                Title = "Captura tu IFE"
            });
            if (result != null)
            {
                var stream = await result.OpenReadAsync();
                resultImage.Source = ImageSource.FromStream(() => stream);
            }
        }

        //Logo del negocio
        async private void Button_Clicked_2(object sender, EventArgs e)
        {
            var result = await MediaPicker.PickPhotoAsync(new MediaPickerOptions
            {
                Title = "Selecciona tu logo"
            });
            if (result != null)
            {
                var stream = await result.OpenReadAsync();
                resultLogo.Source = ImageSource.FromStream(() => stream);
            }
        } */

		async private void Button_Clicked(object sender, EventArgs e)
		{
            if(categoryAdded == false)
			{
                string jsonData = $"{{ \"name\": \"{newCategory.Text}\" }}";
                var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync("http://142.93.3.253:3000/categories", content);
                string result = await response.Content.ReadAsStringAsync();
                Console.WriteLine(result);
                if (response.IsSuccessStatusCode)
                {
                    await DisplayAlert("Añadida", "Categoría añadida", "Ok");
                    await FetchCategories();
                    categoryAdded = true;
                    btnCategory.IsEnabled = false;
                }
                else
                {
                    await DisplayAlert("Error", "Esa categoría ya existe", "Ok");
                }
            }
        }
	}
}