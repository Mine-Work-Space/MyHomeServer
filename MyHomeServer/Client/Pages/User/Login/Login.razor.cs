using AntDesign;
using Blazored.LocalStorage;
using MyHomeServer.Client.Providers;
using MyHomeServer.Client.Static;
using CurrieTechnologies.Razor.SweetAlert2;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using MyHomeServer.Shared.Models;

namespace MyHomeServer.Client.Pages.User
{
    public partial class Login
    {
        [Inject] HttpClient HttpClient { get; set; }
        [Inject] ILocalStorageService LocalStorageService { get; set; }
        [Inject] AuthenticationStateProvider AuthenticationStateProvider { get; set; }
        [Inject] SweetAlertService Swal { get; set; }
        [Inject] NavigationManager _navigationManager { get; set; }

        private UserToSignIn _userToSignIn = new UserToSignIn();
        private bool _signInSuccessful = false;
        private bool _attemptToSignInFailed = false;

        private async Task SignInUser()
        {
            _attemptToSignInFailed = false;
            HttpResponseMessage httpResponseMessage = await HttpClient.PostAsJsonAsync(APIEndpoints.s_signIn, _userToSignIn);
            if (httpResponseMessage.IsSuccessStatusCode)
            {
                string jsonWebToken = await httpResponseMessage.Content.ReadAsStringAsync();
                await LocalStorageService.SetItemAsync("bearerToken", jsonWebToken);
                await ((AppAuthenticationStateProvider)AuthenticationStateProvider).SignIn();

                HttpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("bearer", jsonWebToken);
                _signInSuccessful = true;
                await Swal.FireAsync($"Hello, {_userToSignIn.UserName}!");
                _navigationManager.NavigateTo("/");
            }
            else
            {
                _attemptToSignInFailed = true;
                await Swal.FireAsync($"Opps, no users founded!");
            }
        }
    }
}