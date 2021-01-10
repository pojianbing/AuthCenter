import request from '@/utils/request'
import { transformAbpListQuery } from '@/utils/abp'

export function getApiResources(query) {
  return request({
    url: '/api/identity-server/apiResource',
    method: 'get',
    params: transformAbpListQuery(query)
  })
}

export function createApiResource(data) {
  return request({
    url: '/api/identity-server/apiResource',
    method: 'post',
    data
  })
}

export function updateApiResource(data) {
  return request({
    url: `/api/identity-server/apiResource/${data.id}`,
    method: 'put',
    data
  })
}

export function deleteApiResource(id) {
  return request({
    url: `/api/identity-server/apiResource/${id}`,
    method: 'delete'
  })
}

export function getApiResource(id) {
  return request({
    url: `/api/identity-server/apiResource/${id}`,
    method: 'get'
  })
}

export function getApiResourceScope(id) {
  return request({
    url: `/api/identity-server/apiResource/${id}/scopes`,
    method: 'get'
  })
}

export function addApiResourceScope(data) {
  return request({
    url: `/api/identity-server/apiResource/scopes`,
    method: 'post',
    data
  })
}

export function deleteApiResourceScope(id, name) {
  return request({
    url: `/api/identity-server/apiResource/${id}/scopes?name=${name}`,
    method: 'delete'
  })
}

export function updateApiResourceScope(beforeApiResourceId, beforeName, scope) {
  return request({
    url: `/api/identity-server/apiResource/scopes`,
    method: 'put',
    data: {
      beforeApiResourceId,
      beforeName,
      scope
    }
  })
}
