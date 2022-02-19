using Newtonsoft.Json;

namespace Client.Repositories;
public class GenericRepository<T> : IGenericRepository<T> where T : class
{
    private readonly IHttpClientFactory _httpClientFactory;

    public GenericRepository(IHttpClientFactory httpClientFactory)
        => _httpClientFactory = httpClientFactory;

    public async Task<IEnumerable<T>> GetAllAsync(string url)
    {
        var request = new HttpRequestMessage(HttpMethod.Get, url);

        var client = _httpClientFactory.CreateClient();
        HttpResponseMessage response = await client.SendAsync(request);
        if (response.StatusCode == System.Net.HttpStatusCode.OK)
        {
            var jsonString = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<IEnumerable<T>>(jsonString);
        }

        return null;
    }

    public async Task<T> GetAsync(string url, int id)
    {
        var request = new HttpRequestMessage(HttpMethod.Get, url + id);

        var client = _httpClientFactory.CreateClient();

        var response = await client.SendAsync(request);

        if (response.StatusCode == HttpStatusCode.OK)
        {
            var result = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(result);
        }

        return null;
    }

    public bool Create(string url, T entity)
    {
        var request = new HttpRequestMessage(HttpMethod.Post, url);

        if (entity != null)
        {
            request.Content = new StringContent(
                    JsonConvert.SerializeObject(entity), Encoding.UTF8, "application/json");
        }
        else
        {
            return false;
        }

        var client = _httpClientFactory.CreateClient();

        var response = client.Send(request);

        return response.StatusCode == HttpStatusCode.Created;
    }

    public bool Update(string url, T entity)
    {
        var request = new HttpRequestMessage(HttpMethod.Patch, url);

        if (entity != null)
        {
            request.Content = new StringContent(
                JsonConvert.SerializeObject(entity), Encoding.UTF8, "application/json");
        }
        else
        {
            return false;
        }

        var client = _httpClientFactory.CreateClient();

        var response = client.Send(request);

        return response.StatusCode == HttpStatusCode.NoContent;
    }

    public bool Delete(string url, int id)
    {
        var request = new HttpRequestMessage(HttpMethod.Delete, url + id);

        var client = _httpClientFactory.CreateClient();

        var response = client.Send(request);

        return response.StatusCode == HttpStatusCode.NoContent;
    }

}
