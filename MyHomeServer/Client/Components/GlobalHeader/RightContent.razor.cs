using AntDesign;
using AntDesign.ProLayout;
using Blazored.LocalStorage;
using MyHomeServer.Client.Providers;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using System.Net.Http;
using System.Threading.Tasks;
using System.IO;

namespace MyHomeServer.Client.Components
{
    public partial class RightContent
    {
        [CascadingParameter] protected Task<AuthenticationState> AuthenticationState { get; set; }
        [Inject] HttpClient Http { get; set; }
        private string _userName { get; set; } = string.Empty;
        //
        public AvatarMenuItem[] AvatarMenuItems { get; set; }
        [Inject] ILocalStorageService LocalStorageService { get; set; }
        [Inject] AuthenticationStateProvider AuthenticationStateProvider { get; set; }
        [Inject] NavigationManager NavigationManager { get; set; }
        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
            SetClassMap();
            //
            var user = (await AuthenticationState).User;
            if (user.Identity.IsAuthenticated == true)
            {
                _userName = user.Identity.Name;
                AvatarMenuItems = new AvatarMenuItem[]
                {
                    new() { Key = "next-version", IconType = "profile", Option = "Next Version"},
                    new() { IsDivider = true },
                    new() { Key = "logout", IconType = "logout", Option = "logout" }
                };
            }
        }

        protected void SetClassMap()
        {
            ClassMapper
                .Clear()
                .Add("right");
        }

        public async void HandleSelectUser(MenuItem item)
        {
            switch (item.Key)
            {
                case "my-products":
                    NavigationManager.NavigateTo("/next-version");
                    break;
                case "logout":
                    await SignOut();
                    break;
            }
        }
        private async Task SignOut()
        {
            if (await LocalStorageService.ContainKeyAsync("bearerToken"))
            {
                await LocalStorageService.RemoveItemAsync("bearerToken");
                ((AppAuthenticationStateProvider)AuthenticationStateProvider).SignOut();
            }
            StateHasChanged();
            NavigationManager.NavigateTo("/", true);
        }
    }
}