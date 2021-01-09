using System;
using System.Collections.Generic;
using System.Text;

namespace LazyAbp.Abp.AuthCenter.Volo.Abp.IdentityServer.ApiResources.Dtos
{
    public class SharedApiScopeDto
    {
        public Guid ApiResourceId { get; set; }
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public string Description { get; set; }
        public bool Required { get; set; }
        public bool Emphasize { get; set; }
        public bool ShowInDiscoveryDocument { get; set; }
        public List<string> UserClaims { get; set; }
    }
}
