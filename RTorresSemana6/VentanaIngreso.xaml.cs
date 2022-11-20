using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RTorresSemana6
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class VentanaIngreso : ContentPage
    {

        private const string Url = "http://172.29.32.1/clientes/post.php";
        private readonly HttpClient _client = new HttpClient();
        

        private Datos ItemSeleccionado;




        public VentanaIngreso(Datos itemSeleccionado)
        {
            InitializeComponent();
            this.ItemSeleccionado = itemSeleccionado;
            txtCodigo.Text = itemSeleccionado.codigo.ToString();
            txtNombre.Text = itemSeleccionado.nombre;
            txtApeliido.Text = itemSeleccionado.apellido;
            txtEdad.Text = itemSeleccionado.edad.ToString();
            
        }

        private void btnInsertar_Clicked(object sender, EventArgs e)
        {
            WebClient cliente = new WebClient();
            try
            {
                var parametros = new System.Collections.Specialized.NameValueCollection();
                parametros.Add("codigo", txtCodigo.Text);
                parametros.Add("nombre", txtNombre.Text);
                parametros.Add("apellido", txtApeliido.Text);
                parametros.Add("edad", txtEdad.Text);
                cliente.UploadValues("http://172.29.32.1/clientes/post.php", "POST", parametros);
                DisplayAlert("Alerta", "Datos ingresados correctamente", "Cerrar");


            }
            catch (Exception ex)
            {
                DisplayAlert("Alerta", ex.Message, "Cerrar");
            }

        }


        private async void btnRegresar_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new MainPage());
        }

        private  async void btnActualizar_Clicked(object sender, EventArgs e)
        {
            try
            {

                Datos post = ItemSeleccionado;
            post.codigo = Int32.Parse(txtCodigo.Text);
            post.nombre = txtNombre.Text;
            post.apellido = txtApeliido.Text;
            post.edad = Int32.Parse(txtEdad.Text);
            await _client.PutAsync($"{Url}?codigo={post.codigo}&nombre={post.nombre}&apellido={post.apellido}&edad={post.edad}", null);
            await  DisplayAlert("Alerta", "Datos Actualizado correctamente", "Cerrar");


        }
            catch (Exception ex)
            {
              await   DisplayAlert("Alerta", ex.Message, "Cerrar");
    }

        }
    }
}
