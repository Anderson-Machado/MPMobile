namespace MPMobile
{
    public partial class MainPage : ContentPage
    {
        int count = 0;

        public MainPage()
        {
            InitializeComponent();
        }

     

        private void cameraView_BarcodeDetected(object sender, Camera.MAUI.ZXingHelper.BarcodeEventArgs args)
        {
            MainThread.BeginInvokeOnMainThread(() =>
            {
                //txtmatricula.Text = $"{args.Result[0].BarcodeFormat}: {args.Result[0].Text}";
                txtmatricula.Text = $"{args.Result[0].Text}";
                //colocar o código de consulta a API por aqui...
                cameraView.IsVisible = false;
            });
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            cameraView.IsVisible = true;
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
                DisplayAlert("Erro", "Senha de configuração inválida.", "OK");
            }
           

        }
    }

}
