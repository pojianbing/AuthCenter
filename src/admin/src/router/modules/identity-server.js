import Layout from '@/layout'

const identityServerRouter = {
  path: '/identity-server',
  component: Layout,
  redirect: '/identity-server/clients',
  meta: {
    title: 'AbpIdentityServer["Menu:IdentityServer"]',
    icon: 'id4',
    policy: ''
  },
  children: [
    {
      path: 'clients',
      component: () => import('@/views/identity-server/clients'),
      name: 'Id4-Clients',
      meta: { title: 'AbpIdentityServer["Menu:Clients"]', policy: 'AuthCenter.IdentityServerAdmin.Client' }
    },
    {
      path: 'clients/update',
      component: () => import('@/views/identity-server/clientEditor'),
      hidden: true,
      name: 'Id4-ClientEditor',
      meta: { title: '客户端编辑', policy: 'AuthCenter.IdentityServerAdmin.Client' }
    },
    {
      path: 'identityResource',
      component: () => import('@/views/identity-server/identityResource'),
      name: 'Id4-IdentityResource',
      meta: { title: 'AbpIdentityServer["Menu:IdentityResource"]', policy: 'AuthCenter.IdentityServerAdmin.IdentityResource' }
    },
    {
      path: 'apiResource',
      component: () => import('@/views/identity-server/apiResource'),
      name: 'Id4-ApiResource',
      meta: { title: 'AbpIdentityServer["Menu:ApiResource"]', policy: 'AuthCenter.IdentityServerAdmin.ApiResource' }
    },
    {
      path: 'apiResource/update',
      component: () => import('@/views/identity-server/apiResourceEditor'),
      hidden: true,
      name: 'Id4-ApiResourceEditor',
      meta: { title: 'Api资源编辑', policy: 'AuthCenter.IdentityServerAdmin.ApiResource' }
    },
    {
      path: 'persistedGrants',
      component: () => import('@/views/identity-server/persistedGrants'),
      name: 'Id4-PersistedGrants',
      meta: { title: 'AbpIdentityServer["Menu:PersistedGrant"]', policy: 'AuthCenter.IdentityServerAdmin.PersistedGrant' }
    }
  ]
}

export default identityServerRouter
