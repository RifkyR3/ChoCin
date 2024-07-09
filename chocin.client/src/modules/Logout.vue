<template>
    Logout
</template>
<script lang="ts">
    import { defineComponent } from 'vue';
    import { useAuthStore } from '@/stores'

    export default defineComponent ({
        created() {
            // fetch the data when the view is created and the data is
            // already being observed
            this.fetchData();
        },

        methods : {

            async fetchData() {
                let auth = useAuthStore();
                await auth.logout();
            }
        }
    })
</script>