using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Domain.Entities;

namespace LazyAbp.Abp.AuthCenter.IdentityServer
{
    public class SharedApiScope : Entity
    {
        public Guid ApiResourceId { get; set; }
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public string Description { get; set; }
        public bool Required { get; set; }
        public bool Emphasize { get; set; }
        public bool ShowInDiscoveryDocument { get; set; }

        public virtual List<SharedApiScopeClaim> UserClaims { get; protected set; }

        public override object[] GetKeys()
        {
            return new object[] { ApiResourceId, Name };
        }
    }
}
