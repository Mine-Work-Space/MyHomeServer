﻿using AntDesign;
using CurrieTechnologies.Razor.SweetAlert2;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Routing;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.JSInterop;

namespace MyHomeServer.Client.Pages.FileUpload
{
    public partial class BigFile
    {
        [CascadingParameter] protected Task<AuthenticationState> AuthenticationState { get; set; }
        bool isAuthenticated { get; set; }
        protected override async Task OnInitializedAsync()
        {
            var user = (await AuthenticationState).User;
            if (user.Identity.IsAuthenticated == true)
            {
                isAuthenticated = true;
            }
        }
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if(firstRender)
                await JSRuntime.InvokeVoidAsync("HideStopLoadingButton");
        }
        private async Task<bool> SelectedFileRemoved(UploadFileItem fileItem)
        {
            _customUploadFileModels.RemoveAll(file => String.Equals(file.File.Name, fileItem.FileName));
            await InvokeAsync(StateHasChanged);
            return true;
        }
        // If files uploading & user wants to navigate on another page
        private async Task OnBeforeInternalNavigation(LocationChangingContext context)
        {
            if (!_canUploading)
            {
                var isConfirmed = await Swal.FireAsync(new SweetAlertOptions()
                {
                    Title = "Увага!",
                    Text = "Завантаження файлу, можливо, буде припинено. Продовжити?",
                    Icon = "warning",
                    ShowCancelButton = true
                });

                string location = context.TargetLocation;
                intercepted = context.IsNavigationIntercepted;
                if (!isConfirmed.IsConfirmed)
                {
                    context.PreventNavigation();
                }
            }
        }
    }
}
