<template>
    <aside v-if="menu" class="app-sidebar bg-body-secondary shadow" data-bs-theme="dark">
        <div class="sidebar-brand">
            <router-link class="brand-link align-center" to='/'>
                <img src="../assets/logo.png" alt="Logo" class="brand-image opacity-75 shadow">
                <span class="brand-text fw-light">AdminLTE 4</span>
            </router-link>
        </div>

        <div class="sidebar-wrapper">
            <nav class="nav sidebar-menu flex-column">
                <MenuItem v-for="item in menu" :menuItem="item" :key="item?.name">
                </MenuItem>
            </nav>
        </div>
    </aside>
</template>
<script lang='ts'>
import { defineComponent } from 'vue';
import MenuItem from './SideBarMenuItem.vue';
import type { ModuleModel } from '@/helpers/webApi';
import { useAuthStore, useUiStore } from '@/stores';

interface Data {
    menu: null | ModuleModel[]
}

export default defineComponent({
    data(): Data {
        return {
            menu: null
        };
    },
    created() {
        this.checkMenuItem();
    },
    mounted() {
        this.checkMenuItem();
    },
    watch: {
        // call again the method if the route changes
        '$route': 'checkMenuItem'
    },
    components: {
        MenuItem
    },
    methods: {
        async checkMenuItem() {
            const uiStore = useUiStore();

            if ((!this.menu && useAuthStore().authenticate) || uiStore.refreshNavigation) {

                if (!uiStore.sideBarNavigation || uiStore.refreshNavigation) {
                    let loader = this.$loading.show({
                        color: 'red'
                    });

                    await uiStore.getModule();

                    uiStore.refreshNavigation = false;

                    loader.hide();
                }
            }

            this.menu = uiStore.sideBarNavigation;
        }
    }
});
</script>