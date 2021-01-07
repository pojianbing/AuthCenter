using System;
using System.Collections.Generic;
using System.Text;

namespace LazyAbp.Abp.AuthCenter.Volo.Abp.IdentityServer.ApiResources.Dtos
{
    public class ApiScopeClaimDto
    {
        public Guid ApiResourceId { get;  set; }
        public string Name { get;  set; }
        public  string Type { get;  set; }
    }
}
