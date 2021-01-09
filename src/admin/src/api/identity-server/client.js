import request from '@/utils/request'
import { transformAbpListQuery } from '@/utils/abp'

export function getClient(id) {
  return request({
    url: `/api/identity-server/client/${id}`,
    method: 'get'
  })
}

export function getClients(query) {
  return request({
    url: '/api/identity-server/client',
    method: 'get',
    params: transformAbpListQuery(query)
  })
}

export function createClient(data) {
  return request({
    url: '/api/identity-server/client',
    method: 'post',
    data
  })
}

export function updateClient(data) {
  return request({
    url: `/api/identity-server/client/${data.id}`,
    method: 'put',
    data
  })
}

export function deleteClient(id) {
  return request({
    url: `/api/identity-server/client/${id}`,
    method: 'delete'
  })
}

export function searchConsts(type, text, limit) {
  return request({
    url: `/api/identity-server/client/searchConsts`,
    method: 'post',
    data: { type, text, limit }
  })
}