using Exercise2_2.Models;
using Exercise2_2.Views;
using SignaturePad.Forms;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Exercise2_2
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Principal : ContentPage
    {
        private String encriptado;
        public Principal()
        {
            InitializeComponent();
        }
        private async void btnSave_Clicked(object sender, EventArgs e)
        {
            Stream img = await firmita.GetImageStreamAsync(SignatureImageFormat.Png);
            var memmo = (MemoryStream)img;//casteando a un memory
            byte[] data = memmo.ToArray();//pasando a binarydata
            encriptado = Convert.ToBase64String(data);//por ultimo base64
            if (txtname.Text.Length <= 0 || txtdesc.Text.Length <= 0)
            {
                await DisplayAlert("ALERTA", "TE FALTARON DATOS!", "OK");
            }
            else
            {
                var firma = new Firma
                {
                    imageencripted = encriptado,
                    nombre = txtname.Text,
                    desc = txtdesc.Text
                };
                int respuesta = await App.BaseDatos.guardaFirma(firma);
                if (respuesta == 1){await DisplayAlert("INFO", "FIRMA GUARDADA!", "OK");}
                else{await DisplayAlert("ERROR", "PORFAVOR,  VUELVELO A INTENTAR", "OK");}
            }
        }

        private async void btnDelete_Clicked(object sender, EventArgs e)
        {
            firmita.Clear();
            txtdesc.Text = "";
            txtname.Text = "";
            await DisplayAlert("INFO", "Datos Limpiados..", "OK");
        }

        private async void btnLista_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ListaDeFirmas());
        }
    }
}