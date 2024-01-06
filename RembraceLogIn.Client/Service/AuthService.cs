using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using RembraceLogIn.Client.Utility;
using RembraceLogIn.Shared.Models;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;

namespace RembraceLogIn.Client.Service
{
    public class AuthService : IAuthService
    {
        private readonly HttpClient _httpClient;
        private readonly AuthenticationStateProvider _authenticationStateProvider;
        private readonly ILocalStorageService _localStorage;

        public AuthService(HttpClient httpClient, AuthenticationStateProvider authenticationStateProvider, ILocalStorageService localStorage)
        {
            _httpClient = httpClient;
            _authenticationStateProvider = authenticationStateProvider;
            _localStorage = localStorage;
        }

        public async Task<RegisterResult> Register(RegisterModel registerModel) //Await server response for account creation and notify status
        {
            var result = await _httpClient.PostAsJsonAsync("api/Account", registerModel);
            var reg_result = JsonSerializer.Deserialize<RegisterResult>(await result.Content.ReadAsStringAsync(),
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true }); // get Error messages from json
            if (!result.IsSuccessStatusCode)
            {
                return new RegisterResult { Successful = false, Errors = reg_result.Errors };
            }
            else
            {
                return new RegisterResult { Successful = true, Errors = new List<string>() { "Account created successfully" } }; //not yet displayed to user
            }
        }

        public async Task<LoginResult> Login(LoginModel loginModel)//Login to the site if the information existed in the linked DB. If successful create a token to keep the user logged in
        {
            var loginAsJson = JsonSerializer.Serialize(loginModel);
            var response = await _httpClient.PostAsync("api/Login",
                new StringContent(loginAsJson, Encoding.UTF8,"application/json")); //wait for server response

            var loginResult = JsonSerializer.Deserialize<LoginResult>(await response.Content.ReadAsStringAsync(),
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true}); // get error message

            if (!response.IsSuccessStatusCode)
            {
                return loginResult!;
            }

            await _localStorage.SetItemAsync("authToken", loginResult!.Token);
            ((CustomAuthenticationStateProvider)_authenticationStateProvider).MarkUserAuthenticated(loginResult.Token!);
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", loginResult.Token);//Token stored in header as bearer

            return loginResult;
        }

        public async Task Logout()//Remove the token and log out the user
        {
            await _localStorage.RemoveItemAsync("authToken");
            ((CustomAuthenticationStateProvider)_authenticationStateProvider).MarkUserAsLoggedOut();
            _httpClient.DefaultRequestHeaders.Authorization = null;
        }
    }
}
