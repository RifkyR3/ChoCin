import { createRouter, createWebHistory, type RouteRecordRaw } from 'vue-router';
import { useAuthStore, useUiStore } from '@/stores'

import * as views from '@/views';

import { userRoutes } from './user.router';
import { groupRoutes } from './group.router';
import { moduleRoutes } from './module.router';

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
    ...moduleRoutes,
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
    const auth = useAuthStore();
    const ui = useUiStore();
    const logedin = auth.authenticate;

    if (to.meta.requiresAuth) {
        const toPath = to.matched.map(item => {
            return item.path
        });

        if (!logedin) {
            return next('/login');
        } else if (ui.sideBarNavigation && toPath) {
            const toPathFirst = toPath[0];
            const validation = ui.sideBarNavigation.find(x =>
                (x.path === toPathFirst) || (x.children?.find(y => y.path == toPathFirst))
            )

            if (!validation) {
                return next('/error');
            }

        }
    }

    if (to.meta.requiresUnauth && logedin) {
        return next('/');
    }

    return next();
});

export default router;