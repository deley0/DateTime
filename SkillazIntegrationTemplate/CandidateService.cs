using System.Net.Http.Json;
using System.Text.Json;

namespace HttpIntegrationTemplate;

public class CandidateService
{
    private readonly HttpClient _httpClient;

    public CandidateService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public void CheckCandidate(string id)
    {
        var candidate = GetCandidateInfo(id);
        var vacancy = GetVacancyInfo(candidate.VacancyId);

        if (CalculateMatching(candidate, vacancy))
        {
            AddCommentToCandidate(id, "Подходит");
        }
        else
        {
            AddCommentToCandidate(id, "Не подходит");
        }
    }
    
    private bool CalculateMatching(CandidateInfo candidateInfo, VacancyInfo vacancyInfo)
    {
        //todo: алгоритм соответствия кандидата и вакансии
        
        return true;
        
    }

    public CandidateInfo GetCandidateInfo(string id)
    {
        var message = new HttpRequestMessage(HttpMethod.Post, "/open-api/objects/candidates/filtered")
        {
            Content = JsonContent.Create(new CandidateInfoRequest
            {
                Ids = new[] { id }
            }, options: new() { PropertyNamingPolicy = null })
        };

        var response = _httpClient.Send(message);

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception("Ошибка запроса");
        }

        var deserializedResponse = JsonSerializer.Deserialize<CandidateInfoResponse>(response.Content.ReadAsStream());

        return deserializedResponse?.Items.FirstOrDefault() ?? throw new Exception("Не найден кандидат"); //return deserializedResponse?.Items.FirstOrDefault() ?? throw new Exception("Не найден кандидат")
    }

    private VacancyInfo GetVacancyInfo(string vacancyId)
    {
        var message = new HttpRequestMessage(HttpMethod.Get, $"/open-api/objects/vacancies/{vacancyId}")
        {
            Content = JsonContent.Create(new VacancyInfoRequest
            {
                Ids = new[] { vacancyId }
            }, options: new() { PropertyNamingPolicy = null })
        };

        var response = _httpClient.Send(message);

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception("Ошибка запроса");
        }

        
        //todo: используйте _httpClient для получения вакансии
        return new VacancyInfo();
    }

    private void AddCommentToCandidate(string id, string text)
    {
        //todo: используйте _httpClient для отправки комментария 
    }
}

public class CandidateInfoRequest
{
    public string[] Ids { get; set; }
    public CandidateCommonCvInfo CommonCVInfo { get; set; }
}

public class CandidateInfoResponse
{
    public CandidateInfo[] Items { get; set; } = new CandidateInfo[0];
}
public class CandidateInfo
{
    public string VacancyId { get; set; }
    //todo: добавьте остальные поля на основе swagger
}

public class CandidateCommonCvInfo
{
    public string[] Citizenship { get; set; }
    public CandidateWorkExperience[] WorkExperience { get; set; }
    //todo: добавьте остальные поля на основе swagger
}

public class CandidateSkills
{
    public string Skills { get; set; }
}
class Citizenship
{ 
    public string Id { get; set; } 
    public string Name { get; set; }
}
class CandidateCitizenship
{
    public Citizenship[] Items { get; set; } = new Citizenship[0];
}


public class CandidateWorkExperience
{
    public string CompanyName { get; set; }
    private int TotalMonths { get; set; }
    public string Position { get; set; }
    public DateTime StarDate { get; set; }
    public DateTime EndDate { get; set; }
    public string Description { get; set; }
    public string Industries { get; set; }
    public string City { get; set; }
    public string EmploymentType { get; set; }
}

public class CandidateDrivingExperiens
{
    public string HasPersonalCar { get; set; }
    public string DrivingLicense { get; set; }
}


class CandidateNote
{
    public string Id { get; set; }
    public string Text { get; set; }
    public string CreatedById { get; set; }
    public string CreatedByName { get; set; }
    public string CreatedByClientId { get; set; }
    public string UpdateById { get; set; }
    public string UpdateByName { get; set; }
    public string UpdateByClientId { get; set; }
    public string IsInternal { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpldatedAt { get; set; }
    public DateTime NotifyAt { get; set; }
    public string RefCandidatedId { get; set; }
    public string CandidateStatusId { get; set; }

}
public class VacancyCommonCvInfo
{
    public string[] Citizenship { get; set; }
    public CandidateWorkExperience[] WorkExperience { get; set; }
    public CandidateWorkExperience[] TotalMonths { get; set; }
    //todo: добавьте остальные поля на основе swagger
}
public class VacancyInfo
{
    public string VacancyId { get; set; }
}
public class VacancyInfoRequest
{
    public string[] Ids { get; set; }
    public VacancyCommonCvInfo CommonCVInfo { get; set; }
}
public class VacancyInfoResponse
{
    public VacancyInfo[] Items { get; set; }
}
