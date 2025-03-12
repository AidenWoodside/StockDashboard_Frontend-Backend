using System.Net.Http.Headers;
using System.Text;
using Newtonsoft.Json;

namespace StockDashboard.Infrastructure.Providers;

public abstract class RestApiBase
{
    protected readonly HttpClient _httpClient;

    // Constructor takes an instance of HttpClient, which can be injected
    protected RestApiBase(HttpClient httpClient)
    {
        _httpClient = httpClient;

        // Optionally configure your HttpClient instance here
        _httpClient.DefaultRequestHeaders.Accept.Clear();
        _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
    }

    private void AddHeaders(HttpRequestMessage request, IDictionary<string, string> headers)
    {
        foreach (var header in headers)
        {
            request.Headers.Add(header.Key, header.Value);
        }
    }

    // Generic GET method that returns a deserialized object of type T
    protected async Task<T> GetAsync<T>(string requestUri, IDictionary<string, string>? headers = null)
    {
        using var request = new HttpRequestMessage(HttpMethod.Get, requestUri);
        
        if(headers != null) AddHeaders(request, headers);
        
        var response = await _httpClient.SendAsync(request);
        response.EnsureSuccessStatusCode();
        var jsonData = await response.Content.ReadAsStringAsync();
        var test = JsonConvert.DeserializeObject<T>(jsonData)!;
        
        return JsonConvert.DeserializeObject<T>(jsonData)!;
    }

    // Generic POST method that sends data and returns a deserialized response of type TResponse
    protected async Task<TResponse> PostAsync<TRequest, TResponse>(string requestUri, TRequest data, IDictionary<string, string>? headers = null)
    {
        using var request = new HttpRequestMessage(HttpMethod.Post, requestUri);
        if(headers != null) AddHeaders(request, headers);
        var jsonData = JsonConvert.SerializeObject(data);
        request.Content = new StringContent(jsonData, Encoding.UTF8, "application/json");

        var response = await _httpClient.SendAsync(request);
        response.EnsureSuccessStatusCode();
        var responseData = await response.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<TResponse>(responseData)!;
    }
    
    // Generic POST method that sends data and returns a deserialized response of type TResponse
    protected async Task<TResponse> PostFormUrlEncodedAsync<TResponse>(string requestUri, IDictionary<string,string> data, IDictionary<string, string>? headers = null)
    {
        using var request = new HttpRequestMessage(HttpMethod.Post, requestUri);
        if(headers != null) AddHeaders(request, headers);
        
        request.Content = new FormUrlEncodedContent(data);
        
        request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/x-www-form-urlencoded");

        var response = await _httpClient.SendAsync(request);
        response.EnsureSuccessStatusCode();
        var responseData = await response.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<TResponse>(responseData)!;
    }

    // You can similarly add methods for PUT, DELETE, etc.
}