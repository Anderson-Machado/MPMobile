using MPMobile.ServiceExternal;


namespace MPMobile
{
    public partial class MainPage : ContentPage
    {
        int count = 0;
        MPServiceExternal externalService;
        public MainPage()
        {
            externalService = new MPServiceExternal();
            InitializeComponent();
        }



        private void cameraView_BarcodeDetected(object sender, Camera.MAUI.ZXingHelper.BarcodeEventArgs args)
        {

            MainThread.BeginInvokeOnMainThread(async () =>
            {
                //txtmatricula.Text = $"{args.Result[0].BarcodeFormat}: {args.Result[0].Text}";
                txtmatricula.Text = $"{args.Result[0].Text}";
                var result = await externalService.AcessoAsync(txtmatricula.Text, lbSentido.Text, txtIsVisitante.IsToggled);
                //colocar o código de consulta a API por aqui...
                cameraView.IsVisible = false;
                foto.IsVisible = true;
                setImage(result.Imagem);
                DisplayAlert("Mensagem", result.Message, "OK");
            });
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            cameraView.IsVisible = true;
            foto.IsVisible = false;
            if (cameraView.Cameras.Count > 0)
            {
                cameraView.Camera = cameraView.Cameras.First();
                MainThread.BeginInvokeOnMainThread(async () =>
                {
                    await cameraView.StopCameraAsync();
                    await cameraView.StartCameraAsync();
                });
            }
        }

        private void OnEntryCompleted(object sender, EventArgs e)
        {
            //acessando Pagina de configuração.
            if (txtmatricula.Text.ToUpper().Equals("ADMIN"))
            {
                Navigation.PushAsync(new Configuration());

            }
            else
            {
                //inserir validação se o campo digitado é apenas numero.

                MainThread.BeginInvokeOnMainThread(async () =>
                {

                    var result = await externalService.AcessoAsync(txtmatricula.Text, lbSentido.Text, txtIsVisitante.IsToggled);
                    DisplayAlert("Mensagem", result.Message, "OK");
                    foto.Opacity = 0;
                    setImage(result.Imagem);
                    await foto.FadeTo(1, 2000);
                   

                });
            }


        }

        private void OnSwitchToggledVisitante(object sender, ToggledEventArgs e)
        {
            if (txtIsVisitante.IsToggled)
            {
                lbVisitante.Text = "Consulta Pessoa";
            }
            else
            {
                lbVisitante.Text = "Consulta Visitante";
            }
        }

        private void OnSwitchToggledSentido(object sender, ToggledEventArgs e)
        {
            if (txtSentido.IsToggled)
            {
                lbSentido.Text = "Entrada";
            }
            else
            {
                lbSentido.Text = "Saída";
            }
        }

        private void setImage(byte[]? imagem)
        {
            if (imagem is not null)
            {
                ImageSource imageSource = ImageSource.FromStream(() => new MemoryStream(imagem));
                foto.Source = imageSource;
                
            }
            foto.Source = "/Resources/Imagens/pessoa.png";
            txtmatricula.Text = string.Empty;
        }

        
       
    }

}
