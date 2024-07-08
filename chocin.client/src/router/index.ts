import { createRouter, createWebHistory, type RouteRecordRaw } from 'vue-router';
import { useAuthStore } from '@/stores'

import Login from '../modules/Login.vue';
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
        component: Login,
        meta: {
            requiresUnauth: true
        }
    }
];


const router = createRouter({
    history: createWebHistory(),
    routes
});

router.beforeEach(async (to, from, next) => {
    /*let storedAuthentication = store.getters['auth/currentUser'];

    if (!storedAuthentication) {
        try {
            storedAuthentication = await checkSession();
            store.dispatch('auth/setCurrentUser', storedAuthentication);
        } catch (error) {
            console.error('Error checking session:', error);
        }
    }*/

    const auth = useAuthStore()
    let logedin = auth.authenticate;

    if (to.meta.requiresAuth && !logedin) {
        return next('/login');
    }

    if (to.meta.requiresUnauth && logedin) {
        return next('/');
    }

    return next();
});

export default router;