<template>
    Logout
</template>
<script lang="ts">
import { defineComponent } from 'vue';
import { useAuthStore, useUiStore } from '@/stores'

export default defineComponent({
    created() {
        // fetch the data when the view is created and the data is
        // already being observed
        this.fetchData();
    },

    methods: {

        async fetchData() {
            const auth = useAuthStore();
            const ui = useUiStore();

            ui.sideBarNavigation = null;

            auth.credential = null;
            auth.authenticate = false;

            auth.user = null;
            auth.userGroup = null;
            auth.userGroupSelected = 0;

            return await this.$router.push('/login')
        }
    }
})
</script>