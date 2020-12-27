using System;
using System.Collections.Generic;
using System.Text;

namespace Volo.Abp.Identity
{
    public class IdentityUserOrgCreateDto: IdentityUserCreateDto
    {
        public List<Guid> OrgIds { get; set; }
    }
}
