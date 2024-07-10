import { createRouter, createWebHistory, type RouteRecordRaw } from 'vue-router';
import { useAuthStore } from '@/stores'

import { ErrorView, LoginView, LogoutView } from '../views';
import Base from '../modules/tmp/Base.vue';

const routes: Array<RouteRecordRaw> = [
    {
        path: '/',
        name: '',
        component: Base,
        meta: {
            requiresAuth: true
        }
    },
    {
        path: '/login',
        name: 'Login',
        component: LoginView,
        meta: {
            requiresUnauth: true
        }
    },
    {
        path: '/logout',
        name: 'Logout',
        component: LogoutView,
        meta: {
            requiresAuth: true
        }
    },

    // otherwise redirect to home
    { 
        path: '/:pathMatch(.*)*', 
        component: ErrorView
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