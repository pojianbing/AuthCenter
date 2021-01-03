using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Domain.Entities;

namespace LazyAbp.Abp.AuthCenter.IdentityServer
{
    public class SharedIdentityResource : Entity<Guid>
    {
        public string Name { get; set; }
    }
}
