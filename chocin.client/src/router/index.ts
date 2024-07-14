import { createRouter, createWebHistory, type RouteRecordRaw } from 'vue-router';
import { useAuthStore } from '@/stores'

import * as views from '@/views';

import { userRoutes } from './user.router';
import { groupRoutes } from './group.router';

const routes: Array<RouteRecordRaw> = [
    {
        path: '/',
        name: 'Dashboard',
        component: views.DashboardView,
        meta: {
            requiresAuth: true
        }
    },
    ...userRoutes,
    ...groupRoutes,
    {
        path: '/login',
        name: 'Login',
        component: views.LoginView,
        meta: {
            requiresUnauth: true,
            layout: 'empty'
        }
    },
    {
        path: '/logout',
        name: 'Logout',
        component: views.LogoutView,
        meta: {
            requiresAuth: true,
            layout: 'empty'
        }
    },

    {
        path: '/error',
        name: 'Error',
        component: views.ErrorView
    },
    // otherwise redirect to error
    {
        path: '/:pathMatch(.*)*',
        component: views.ErrorView
    }
];

const router = createRouter({
    history: createWebHistory(),
    routes
});

router.beforeEach(async (to, from, next) => {
    const auth = useAuthStore()
    const logedin = auth.authenticate;

    if (to.meta.requiresAuth && !logedin) {
        return next('/login');
    }

    if (to.meta.requiresUnauth && logedin) {
        return next('/');
    }

    return next();
});

export default router;