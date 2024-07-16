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
    {
        path: '/users/input/:userId(\\d+)?',
        name: 'User Input',
        component: views.UserInput,
        meta: {
            requiresAuth: true
        }
    },
]