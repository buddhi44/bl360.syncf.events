using bl360.clientInfrastructure.Services;
using bl360.domain;
using bl360.shared.Constants.Storage;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components;
using Syncfusion.Blazor.Inputs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.JSInterop;

namespace bl360.ui.Pages
{
	public partial class Login : ComponentBase
	{

		private TokenRequest _tokenModel = new();
		private bool IsLoginSuccessFull = false;
		private string Message = "Login with your Credentials.";
		private string className = "";
		private bool isLoginSaved = false;
		private IList<CompanyResponse> list { get; set; } = new List<CompanyResponse>();
		CompanyResponse selectedCompany = new CompanyResponse();
		protected override async Task OnInitializedAsync()
		{
			var state = await _stateProvider.GetAuthenticationStateAsync();
			if (state != new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity())))
			{
				_navigationManager.NavigateTo("/");
			}

			string username = await _storageService.GetItem("login_username");
			string password = await _storageService.GetItem("login_password");

			if (username != null && password != null)
			{
				if (!username.Equals("") && !password.Equals(""))
				{
					_tokenModel.UserName = username;
					_tokenModel.Password = password;
					isLoginSaved = true;
				}
			}

			//if (await _localStorage.ContainKeyAsync(StorageConstants.Local._SHOULD_GOTO_COMPANY_SELECTION_))
			//{
			//    await _localStorage.SetItemAsync(StorageConstants.Local._SHOULD_GOTO_COMPANY_SELECTION_, false);
			//}
		}


		private async Task SubmitAsync()
		{
			Message = "Login with your Credentials.";
			className = "";
			StateHasChanged();
			if (string.IsNullOrEmpty(_tokenModel.UserName) || string.IsNullOrEmpty(_tokenModel.Password))
			{
				//_snackBar.Add("Username or Password Empty!");
			}
			else
			{
				if (isLoginSaved)
				{
					await _storageService.SetItem<string>("login_username", _tokenModel.UserName);
					await _storageService.SetItem<string>("login_password", _tokenModel.Password);
				}
				else
				{
					await _storageService.SetItem<string>("login_username", "");
					await _storageService.SetItem<string>("login_password", "");
				}
				try
				{
					var result = await _authenticationManager.Login(_tokenModel);

					if (result.IsSuccess)
					{

							await _authenticationManager.GetUserInformation();
					}
					else
					{
						//_snackBar.Add("Username or Password Incorrect!");
					}
				}
				catch
				{
					//_snackBar.Add("Internet Connection may dropped!");
				}
			}
			StateHasChanged();
		}

		private bool _passwordVisibility;
		private InputType _passwordInput = InputType.Password;
		//private string _passwordInputIcon = Icons.Material.Filled.VisibilityOff;

		void TogglePasswordVisibility()
		{
			//if (_passwordVisibility)
			//{
			//	_passwordVisibility = false;
			//	_passwordInputIcon = Icons.Material.Filled.VisibilityOff;
			//	_passwordInput = InputType.Password;
			//}
			//else
			//{
			//	_passwordVisibility = true;
			//	_passwordInputIcon = Icons.Material.Filled.Visibility;
			//	_passwordInput = InputType.Text;

			//}
		}

		private async void OnUsernameInputKeyDown(KeyboardEventArgs e)
		{
			if (e != null && e.Code != null)
			{
				if (e.Code.Equals("Enter") || e.Code.Equals("NumpadEnter"))
				{
					if (!string.IsNullOrEmpty(_tokenModel.Password))
					{
						await SubmitAsync();
					}
					else
					{
						//await pwdSingleLineReference.FocusAsync();
					}

				}
			}
		}

		private async void OnPasswordInputKeyDown(KeyboardEventArgs e)
		{
			if (e != null && e.Code != null)
			{
				if (e.Code.Equals("Enter") || e.Key.Equals("Enter") || e.Code.Equals("NumpadEnter") || e.Key.Equals("NumpadEnter"))
				{
					if (!string.IsNullOrEmpty(_tokenModel.UserName))
					{
						await SubmitAsync();
					}
					else
					{
						//await usrnmSingleLineReference.FocusAsync();
					}

				}
			}



		}

		private async Task CheckCompanyCountAndGo()
		{
			list = await _companyManager.GetUserCompanies();
			if (list != null)
			{
				if (list.Count == 1)
				{
					selectedCompany = list[0];
					ProcessCompanySelection();

				}
				else
				{
					if (await _localStorage.ContainKeyAsync(StorageConstants.Local._SHOULD_GOTO_COMPANY_SELECTION_))
					{
						await _localStorage.SetItemAsync(StorageConstants.Local._SHOULD_GOTO_COMPANY_SELECTION_, true);
					}
				}
			}


			StateHasChanged();
		}

		private async void ProcessCompanySelection()
		{
			CompanyResponse request = new CompanyResponse();
			request.CompanyKey = selectedCompany.CompanyKey;
			request.CompanyName = selectedCompany.CompanyName;
			await _companyManager.UpdateSelectedCompany(request);

			await Task.FromResult(0);
		}

	}
}
