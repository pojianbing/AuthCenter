﻿using Volo.Abp.AspNetCore.Mvc.UI.Theme.Shared.Components;
using Volo.Abp.DependencyInjection;

namespace LazyAbp.Abp.AuthCenter.Web
{
    [Dependency(ReplaceServices = true)]
    public class AuthCenterBrandingProvider : DefaultBrandingProvider
    {
        public override string AppName => "AuthCenter";
    }
}
