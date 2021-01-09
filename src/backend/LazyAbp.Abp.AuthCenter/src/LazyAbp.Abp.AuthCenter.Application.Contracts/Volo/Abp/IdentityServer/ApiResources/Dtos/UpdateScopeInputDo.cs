using System;
using System.Collections.Generic;
using System.Text;

namespace LazyAbp.Abp.AuthCenter.Volo.Abp.IdentityServer.ApiResources.Dtos
{
    public class UpdateScopeInputDo
    {
        /// <summary>
        /// 更新前ApiResourceId
        /// </summary>
        public Guid BeforeApiResourceId { get; set; }
        /// <summary>
        /// 更新前名
        /// </summary>
        public string BeforeName { get; set; }
        /// <summary>
        /// 更新后scope
        /// </summary>
        public SharedApiScopeDto Scope { get; set; }
    }
}
