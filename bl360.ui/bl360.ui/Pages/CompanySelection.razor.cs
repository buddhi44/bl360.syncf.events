using bl360.domain;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bl360.ui.Pages
{
   
        public class CompanySelectionModal
        {
            public int CompanyKey { get; set; } = 1;
            public string ComanyName { get; set; } = "";
        }

    public partial class CompanySelection : ComponentBase
    {
        private string FirstName { get; set; }
        private string SecondName { get; set; }
        private char FirstLetterOfName { get; set; }
        private string ImageDataUrl { get; set; }

        private bool IsdarkMode { get; set; }
        private bool _rightToLeft = false;


        private IList<CompanyResponse> list { get; set; } = new List<CompanyResponse>();
        private CompanySelectionModal _companySelectionModal = new();

        private CompanyResponse selectedCompany = new();




        //for add user card
        private async Task RightToLeftToggle()
        {
            _rightToLeft = false;
        }



        //for add user card
        public async void ToggleDarkMode(bool value)
        {
            //IsdarkMode = value;
            //await OnDarkModeToggle.InvokeAsync(IsdarkMode);
        }



        protected override async Task OnInitializedAsync()
        {
            //var state = await _stateProvider.GetAuthenticationStateAsync2();

            //return base.OnInitializedAsync();


            //for add user card
            var user = await _authenticationManager.GetUserInformation();
            if (user == null)
            {
                await _authenticationManager.Logout();
                return;
            }
            if (user != null)
            {
                FirstName = user.AuthenticatedUser.UserID;
                if (!string.IsNullOrEmpty(FirstName))
                {
                    FirstLetterOfName = FirstName[0];
                }
            }
            await LoadCompanies();

            if (list != null)
            {
                if (list.Count == 1)
                {
                    selectedCompany = list[0];
                    ProcessCompanySelection();
                }
            }
        }


        private async Task LoadCompanies()
        {
            list = await _companyManager.GetUserCompanies();
            if (list == null)
            {
                await _authenticationManager.Logout();
                return;
            }
            if (list.Count > 0)
            {
                selectedCompany = list[0];
            }
            StateHasChanged();
        }
        private void NavigateCompany() { _navigationManager.NavigateTo("/"); }
        private async void ProcessCompanySelection()
        {
            CompanyResponse request = new CompanyResponse();
            request.CompanyKey = selectedCompany.CompanyKey;
            request.CompanyName = selectedCompany.CompanyName;
            await _companyManager.UpdateSelectedCompany(request);

            //TODO:add workstation selection


            await Task.FromResult(0);
        }
        private async void CompanyChange(CompanyResponse response)
        {
            selectedCompany = response;
            ProcessCompanySelection();
            await Task.CompletedTask;
        }
        private async Task<IEnumerable<CompanyResponse>> SearchAsync(string value)
        {
            // In real life use an asynchronous function for fetching data from an api.
            await Task.Delay(5);

            // if text is null or empty, show complete list
            if (string.IsNullOrEmpty(value))
            {
                return list;
            }

            return list.Where(x => x.CompanyName.Contains(value, StringComparison.InvariantCultureIgnoreCase));
        }

        private async void Logout()
        {
            await _authenticationManager.Logout();

        }


        //for add user card
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                await LoadDataAsync();
            }
        }

        private async Task LoadDataAsync()
        {
            if (!string.IsNullOrEmpty(FirstName))
            {
                FirstLetterOfName = FirstName[0];
            }
        }
    }
        
}
