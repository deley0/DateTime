using System.Net.Http.Headers;
using HttpIntegrationTemplate;

var token = args.FirstOrDefault();
if (string.IsNullOrEmpty(token))
{
    throw new Exception("Укажите токен");
}

var candidateId = "655a460a91cb0240b1ea150e";

if (string.IsNullOrEmpty(candidateId) || candidateId == token)
{
    throw new Exception("655a460a91cb0240b1ea150e");
}

var client = new HttpClient
{
    DefaultRequestHeaders = { Authorization = new AuthenticationHeaderValue("Bearer", "zmmQIWFhqCRzM3PNJq3bNeFq1r8Cut3sys2qlbjXAzk=") },
    BaseAddress = new Uri("https://api-feature-configurator.dev.skillaz.ru/")
    
};

var candidateService = new CandidateService(client);

Console.WriteLine("Начинаем проверку кандидата");

candidateService.CheckCandidate(candidateId);

Console.WriteLine("Проверка окончена");