<template>
    Logout
</template>
<script lang="ts">
import { defineComponent } from 'vue';
import { mapStores } from 'pinia'
import { useAuthStore, useTokenStore, useUiStore } from '@/stores'

export default defineComponent({
    computed: {
        ...mapStores(useAuthStore, useUiStore, useTokenStore)
    },
    created() {
        // fetch the data when the view is created and the data is
        // already being observed
        this.fetchData();
    },

    methods: {

        async fetchData() {
            this.uiStore.sideBarNavigation = null;

            this.authStore.credential = null;
            this.authStore.authenticate = false;

            this.tokenStore.token = '';

            this.authStore.user = null;
            this.authStore.userGroup = null;
            this.authStore.userGroupSelected = 0;

            return await this.$router.push('/login')
        }
    }
})
</script>