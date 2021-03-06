using System;
using BlazorDemo.Shared;
using Microsoft.AspNetCore.Blazor.Components;
using Microsoft.JSInterop;

namespace BlazorDemo.AdalClient.Pages
{
    public class EditModel : BaseComponent
    {
        [Parameter]
        protected string Id { get; private set; } = "0";
        protected string PageTitle { get; private set; }
        protected Book CurrentBook { get; set; }

        protected override void OnParametersSet()
        {
            if (Id == null || Id == "0")
            {
                PageTitle = "Add book";
                CurrentBook = new Book();
            }
            else
            {
                PageTitle = "Edit book";
                
                LoadBook(int.Parse(Id));
            }
        }

        protected async void LoadBook(int id)
        {
            Action<string> action = async (token) =>
            {
                BooksClient.Token = token;
                CurrentBook = await BooksClient.GetBook(id);

                StateHasChanged();
            };

            //RegisteredFunction.InvokeUnmarshalled<bool>("executeWithToken", action);
            await JSRuntime.Current.InvokeAsync<bool>("blazorDemoInterop.executeWithToken", action);
        }

        protected async void Save()
        {
            var book = CurrentBook;

            Action<string> action = async (token) =>
            {
                try
                {
                    BooksClient.Token = token;
                    
                    await BooksClient.SaveBook(book);
                }
                catch
                {
                    // Let's suppress weird arbitrary errors
                }

                UriHelper.NavigateTo("/page/1");
            };

            await JSRuntime.Current.InvokeAsync<bool>("blazorDemoInterop.executeWithToken", action);
        }
    }
}