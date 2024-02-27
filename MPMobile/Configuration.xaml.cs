using MPMobile.Data;
using MPMobile.Entity;
using MPMobile.ServiceExternal;

namespace MPMobile;

public partial class Configuration : ContentPage
{
    MPServiceExternal _externalService;
    DatabaseContext _database;
    public Configuration(DatabaseContext database, MPServiceExternal externalService)
    {
        _externalService = externalService;
        InitializeComponent();
        _database = database;
        this.Appearing += MainPage_Appeared;
    }

    private void Button_Clicked(object sender, EventArgs e)
    {


        MainThread.BeginInvokeOnMainThread(async () =>
        {
            var result = await _database.GetAllAsync<ConfigurationEntity>();
            if (result.Any())
            {
                //update
                var entityForUpdate = result.FirstOrDefault();
                entityForUpdate.Equipamento = int.Parse(txtEquipamento.Text);
                entityForUpdate.UrlBase = txtUrlBase.Text;
                await _database.UpdateItemAsync<ConfigurationEntity>(entityForUpdate);
            }
            else
            {
                var entityConfiguration = new ConfigurationEntity()
                {
                    Equipamento = int.Parse(txtEquipamento.Text),
                    UrlBase = txtUrlBase.Text
                };
                await _database.AddItemAsync(entityConfiguration);
            }
        });

        Navigation.PopAsync();
    }

    private void OnSwitchToggledOffLine(object sender, ToggledEventArgs e)
    {
        MainThread.BeginInvokeOnMainThread(async () =>
        {
            var offs = await _database.GetAllAsync<OffLineEntity>();

            if (offs.Any())
            {
              var result = await _externalService.AcessoInBacthAsync(offs);
                if(result.Message == "Inserido com sucesso.")
                {
                    foreach (var item in offs)
                    {
                     await _database.DeleteItemAsync(item);
                    }
                }
            }
            lbQuantidade.Text = offs.Count() > 0 ? $"{offs.Count()} Registros Off." : $"{offs.Count()} Registros Off.";

        });
    }


    private void MainPage_Appeared(object sender, EventArgs e)
    {
        MainThread.BeginInvokeOnMainThread(async () =>
        {
            var result = await _database.GetAllAsync<OffLineEntity>();
            lbQuantidade.Text = result.Count() > 0 ? $"{result.Count()} Registros Off." : string.Empty;

            //lendo info dos equipamentos
            var config = await _database.GetAllAsync<ConfigurationEntity>();

            var firistConfiguration = config.FirstOrDefault();
            if (firistConfiguration is not null)
            {
                txtEquipamento.Text = firistConfiguration.Equipamento.ToString();
                txtUrlBase.Text = firistConfiguration.UrlBase;
            }
        });
    }
}