namespace MPMobile;

public partial class Configuration : ContentPage
{
	public Configuration()
	{
		InitializeComponent();
	}

    private void Button_Clicked(object sender, EventArgs e)
    {
        Navigation.PopAsync();
    }
}