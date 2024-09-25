using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using UniversalNotifier.NET.Models;
using UniversalNotifier.NET.Models.EmailModels;

namespace UniversalNotifier.NET.Mailgun
{
    public class MailgunNotifier : EmailNotifier
    {
        protected override string FromAccount { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        private readonly string _apiKey;
        private readonly string _domain;
        private readonly HttpClient _client;

        public MailgunNotifier(string apiKey, string domain)
        {
            _apiKey = apiKey;
            _domain = domain;
            _client = new HttpClient();
        }

        public override async Task<NotifierResponse> SendAsync(EmailNotifierContent notificationContent, EmailNotifierReceiver receiver) =>
            await sendAsync(notificationContent, new List<EmailNotifierReceiver>() { receiver });

        public override async Task<NotifierResponse> SendManyAsync(EmailNotifierContent notifierContent, IEnumerable<EmailNotifierReceiver> receivers) =>
            await sendAsync(notifierContent, receivers);

        private async Task<NotifierResponse> sendAsync(EmailNotifierContent notificationContent, IEnumerable<EmailNotifierReceiver> receivers)
        {
            var request = createHttpRequest(notificationContent, receivers);
            var response = await _client.SendAsync(request);
            return await handleResponse(response);
        }

        private async Task<NotifierResponse> handleResponse(HttpResponseMessage response) => response.IsSuccessStatusCode ? await handleSuccessResponse(response) : await handleErrorResponse(response);

        private async Task<NotifierResponse> handleErrorResponse(HttpResponseMessage response)
        {
            var notifierResponse = new NotifierResponse();
            if (response.StatusCode == HttpStatusCode.Forbidden)
                return CreateErrorNotifierResponse("Forbidden");

            var message = await GetJsonProperty(response, "message") ?? "Unknown error";
            return notifierResponse;
        }

        private NotifierResponse CreateErrorNotifierResponse(string errorMessage)
        {
            return new NotifierResponse
            {
                Errors = new List<string> { errorMessage }
            };
        }
        private async Task<NotifierResponse> handleSuccessResponse(HttpResponseMessage response)
        {
            return new NotifierResponse() { Id = await GetJsonProperty(response, "id"), Message = await GetJsonProperty(response, "message") };
        }

        private async Task<string> GetJsonProperty(HttpResponseMessage response, string propertyName)
        {
            var json = await ParseJsonContent(response);
            if (json.ValueKind != JsonValueKind.Null && json.TryGetProperty(propertyName, out var propertyElement))
            {
                return propertyElement.GetString();
            }

            return string.Empty;

        }
        private async Task<JsonElement> ParseJsonContent(HttpResponseMessage response)
        {
            if (response.Content == null)
            {
                return new JsonElement();
            }

            var responseContent = await response.Content.ReadAsStringAsync();

            if (string.IsNullOrWhiteSpace(responseContent))
            {
                return new JsonElement();
            }

            try
            {
                // Parse the content and return the root element
                return JsonDocument.Parse(responseContent).RootElement;
            }
            catch (JsonException)
            {
                // Return null in case of invalid JSON
                return new JsonElement();
            }
        }

        private HttpRequestMessage createHttpRequest(EmailNotifierContent notificationContent, IEnumerable<EmailNotifierReceiver> receivers)
        {
            var requestUrl = $"https://api.mailgun.net/v3/{_domain}/messages";
            var request = new HttpRequestMessage(HttpMethod.Post, requestUrl);
            request.Headers.Authorization = getAutorization();
            request.Content = createFormData(notificationContent, receivers);
            return request;
        }

        private AuthenticationHeaderValue getAutorization()
        {
            var getApiKeyBytes = Encoding.ASCII.GetBytes($"api:{_apiKey}");
            var getAuthKey = Convert.ToBase64String(getApiKeyBytes);
            return new AuthenticationHeaderValue("Basic", getAuthKey);
        }

        private MultipartFormDataContent createFormData(EmailNotifierContent notificationContent, IEnumerable<EmailNotifierReceiver> receivers)
        {
            var content = new MultipartFormDataContent();
            content.Add(new StringContent(FromAccount), "from");
            foreach (var account in receivers)
            {
                content.Add(new StringContent(account.ParseReceiver()), "to");
            }
            content.Add(new StringContent(notificationContent.Subject), "subject");
            content.Add(new StringContent(notificationContent.Content), notificationContent.IsHtml ? "html" : "text");
            return content;
        }


    }
}