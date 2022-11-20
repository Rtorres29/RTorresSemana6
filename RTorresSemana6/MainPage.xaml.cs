using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace RTorresSemana6
{
    public partial class MainPage : ContentPage
    {

        private const string Url = "http://172.29.32.1/clientes/post.php";
        private readonly HttpClient client = new HttpClient();
        private ObservableCollection<Datos> _post;



         public MainPage()
        {
            InitializeComponent();
            GetData();
        }

        private async void GetData()
        {
            var content = await client.GetStringAsync(Url);

            List<Datos> posts = JsonConvert.DeserializeObject<List<Datos>>(content);

            _post = new ObservableCollection<Datos>(posts);

            MyListView.ItemsSource = _post;

        }

        private async void btnGet_Clicked(object sender, EventArgs e)
        {
            var itemSeleccionado = (Datos)MyListView.SelectedItem;
            await Navigation.PushAsync(new VentanaIngreso(itemSeleccionado));
        
    }

        private async void btnEliminar_Clicked(object sender, EventArgs e)
        {

            try
            {
                Datos post = _post[0];
            await client.DeleteAsync($"{Url}?codigo={post.codigo}");
            _post.Remove(post);

                await DisplayAlert("Alerta", "Dato eliminado ", "Aceptar");


            }
            catch (Exception ex)
            {
                await DisplayAlert("Alerta", ex.Message, "Aceptar");
            }

        }
    }
}

