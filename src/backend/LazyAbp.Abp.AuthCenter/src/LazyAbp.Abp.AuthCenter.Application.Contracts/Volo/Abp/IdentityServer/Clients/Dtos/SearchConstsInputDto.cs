using System;
using System.Collections.Generic;
using System.Text;

namespace LazyAbp.Abp.AuthCenter.Volo.Abp.IdentityServer.Clients.Dtos
{
    public class SearchConstsInputDto
    {
        public string Type { get; set; }
        public string Text { get; set; }
        public int Limit { get; set; }
    }
}
