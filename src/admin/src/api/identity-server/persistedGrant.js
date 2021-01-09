import request from '@/utils/request'
import { transformAbpListQuery } from '@/utils/abp'

export function getPersistedGrants(query) {
  return request({
    url: '/api/app/persistedGrant',
    method: 'get',
    params: transformAbpListQuery(query)
  })
}

export function getPersistedGrant(id) {
    return request({
      url: `/api/app/persistedGrant/${id}`,
      method: 'get'
    })
}

export function deletePersistedGrant(id) {
  return request({
    url: `/api/app/persistedGrant/${id}`,
    method: 'delete'
  })
}