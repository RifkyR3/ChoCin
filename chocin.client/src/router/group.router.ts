import { type RouteRecordRaw } from 'vue-router';
import * as views from '@/views';

export const groupRoutes: Array<RouteRecordRaw> = [
    {
        path: '/groups',
        name: 'Groups',
        component: views.GroupView,
        meta: {
            requiresAuth: true
        }
    },
    {
        path: '/groups/input/:groupId(\\d+)?',
        name: 'Group Input',
        component: views.GroupInput,
        meta: {
            requiresAuth: true
        }
    },
]