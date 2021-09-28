using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Amur
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SignUp : ContentPage
    {
        public SignUp()
        {
            InitializeComponent();
        }

        private void signUpClient(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Client());
        }

        private void signUpBusiness(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Provider());   
        }
    }
}