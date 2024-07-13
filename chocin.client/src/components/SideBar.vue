<template>
    <aside class="app-sidebar bg-body-secondary shadow" data-bs-theme="dark">
        <div class="sidebar-brand">
            <router-link class="brand-link" to='/'>
                <img src="../assets/logo.svg" alt="AdminLTE Logo" class="brand-image opacity-75 shadow">
                <span class="brand-text fw-light">AdminLTE 4</span>
            </router-link>
        </div>

        <div class="sidebar-wrapper">
            <nav class="nav sidebar-menu flex-column" >
                <MenuItem
                    v-for="item in menu"
                    :menuItem="item" :key="item.name"
                ></MenuItem>
            </nav>
        </div>
    </aside>
</template>
<script lang='ts'>
import { defineComponent } from 'vue';
import MenuItem from './SideBarMenuItem.vue';

interface Data {
    menu: any
}

export default defineComponent({
    data(): Data {
        return {
            menu: MENU
        };
    },
    components: {
        MenuItem
    },
    methods: {
    }
});

export const MENU = [
    {
        name: 'Dashboard',
        path: '/',
        icon: 'fa-gauge-high'
    },
    {
        name: 'User Management',
        icon: 'fa-people-roof',
        children: [
            {
                name: 'Users',
                path: '/users',
                icon: 'fa-users'
            },
            {
                name: 'Groups',
                path: '/groups',
                icon: 'fa-user-group'
            }
        ]
    },
];

</script>