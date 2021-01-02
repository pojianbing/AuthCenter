import request from '@/utils/request'
import { transformAbpListQuery } from '@/utils/abp'

export function getIdentityResources(query) {
  return request({
    url: '/api/app/identityResource',
    method: 'get',
    params: transformAbpListQuery(query)
  })
}

export function createIdentityResource(data) {
  return request({
    url: '/api/app/identityResource',
    method: 'post',
    data
  })
}

export function updateIdentityResource(data) {
  return request({
    url: `/api/app/identityResource/${data.id}`,
    method: 'put',
    data
  })
}

export function deleteIdentityResource(id) {
  return request({
    url: `/api/app/identityResource/${id}`,
    method: 'delete'
  })
}

export function getIdentityResource(id) {
  return request({
    url: `/api/app/identityResource/${id}`,
    method: 'get'
  })
}