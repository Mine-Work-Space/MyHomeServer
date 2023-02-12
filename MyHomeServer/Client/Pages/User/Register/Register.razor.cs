using MyHomeServer.Client.Static;
using CurrieTechnologies.Razor.SweetAlert2;
using Microsoft.AspNetCore.Components;
using System;
using System.ComponentModel.DataAnnotations;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using MyHomeServer.Shared.Models;

namespace MyHomeServer.Client.Pages.User
{
    public partial class Register
    {
        [Inject] HttpClient HttpClient { get; set; }
        [Inject] NavigationManager _navigationManager { get; set; }
        [Inject] SweetAlertService Swal { get; set; }
        private UserDTO _userToRegister = new UserDTO();
        private bool _registerSuccessful = false;
        private bool _attemptToRegisterFailed = false;
        private string? _attemptToRegisterFailedErrorMessage = String.Empty;

        private async Task RegisterUser()
        {
            _attemptToRegisterFailed = false;
            HttpResponseMessage httpResponseMessage = await HttpClient.PostAsJsonAsync(APIEndpoints.s_register, _userToRegister);

            if (httpResponseMessage.IsSuccessStatusCode)
            {
                _registerSuccessful = true;
                await Swal.FireAsync($"Hello, {_userToRegister.UserName}!");
                _navigationManager.NavigateTo("/user/login");
            }
            else
            {
                string serverErrorMessages = await httpResponseMessage.Content.ReadAsStringAsync();
                _attemptToRegisterFailedErrorMessage = $"{serverErrorMessages} Change data and try again.";
                await Swal.FireAsync($"{_attemptToRegisterFailedErrorMessage}");
                _attemptToRegisterFailed = true;
            }
        }
    }
}