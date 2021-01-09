using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Domain.Entities;

namespace LazyAbp.Abp.AuthCenter.IdentityServer
{
    public class SharedApiScopeClaim : Entity
    {
        public Guid ApiResourceId { get; set; }
        public string Type { get; set; }
        public string Name { get; set; }

        public override object[] GetKeys()
        {
            return new object[] { ApiResourceId, Name };
        }
    }
}
