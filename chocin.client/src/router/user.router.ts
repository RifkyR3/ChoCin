import { type RouteRecordRaw } from 'vue-router';
import * as views from '@/views';

export const userRoutes: Array<RouteRecordRaw> = [
    {
        path: '/users',
        name: 'User',
        component: views.UserView,
        meta: {
            requiresAuth: true
        }
    },
]