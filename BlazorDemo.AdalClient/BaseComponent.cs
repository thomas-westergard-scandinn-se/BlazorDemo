﻿using BlazorDemo.Shared;
using Microsoft.AspNetCore.Blazor.Components;
using Microsoft.AspNetCore.Blazor.Services;

namespace BlazorDemo.AdalClient
{
    public abstract class BaseComponent : BlazorComponent
    {
        [Inject]
        protected IUriHelper UriHelper { get; set; }

        [Inject]
        protected IBooksClient BooksClient { get; set; }
    }
}
