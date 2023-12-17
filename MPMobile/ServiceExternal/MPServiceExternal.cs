using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;

namespace MPMobile.ServiceExternal
{
    public class MPServiceExternal
    {
      
        
        public async Task<AppResponse> AcessoAsync(string matricula, string type, bool isVisitante = false)
        {
            try
            {
                var url = isVisitante ? "https://7441-187-111-14-135.ngrok.io/Pessoa" : "https://7441-187-111-14-135.ngrok.io/Visitante";

                var body = new { Equipamento = 1, Type = type, Matricula = matricula };

                var strBody = JsonConvert.SerializeObject(body);

                using (HttpClient client = new HttpClient())
                {
                    // Configurando o conteúdo da requisição com os dados e o tipo de mídia
                    HttpContent conteudo = new StringContent(strBody, Encoding.UTF8, "application/json");

                    // Realizando a requisição POST
                    HttpResponseMessage resposta = await client.PostAsync(url, conteudo);

                    // Verificando se a requisição foi bem-sucedida
                    if (resposta.IsSuccessStatusCode)
                    {
                        // Lendo o conteúdo da resposta como uma string
                        string respostaJson = await resposta.Content.ReadAsStringAsync();

                        // Desserializando a resposta para a classe AppResponse
                        AppResponse appResponse = Newtonsoft.Json.JsonConvert.DeserializeObject<AppResponse>(respostaJson);
                        return appResponse;

                    }
                    else if (resposta.StatusCode == System.Net.HttpStatusCode.BadRequest)
                    {
                        string respostaJson = await resposta.Content.ReadAsStringAsync();
                        var erroResponse = Newtonsoft.Json.JsonConvert.DeserializeObject<ErroResponse>(respostaJson);
                        var error = new AppResponse()
                        {
                            Message = erroResponse.Detail
                        };
                        return error;
                    }
                }
                return new AppResponse() { Message = "Erro inesperado, por favor entre em contado." };
            }
            catch (Exception ex)
            {
                return new AppResponse() { Message = "OffLine!" };
            }
        }
    }
}
