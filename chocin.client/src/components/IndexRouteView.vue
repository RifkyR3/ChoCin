<template>
    <ContentHeader :title="title" class="mb-2" />
    <div class="app-content">
        <div class="container-fluid">
            <router-view />
        </div>
    </div>
</template>
<script lang="ts">
import { defineComponent } from 'vue';
import ContentHeader from '@components/ContentHeader.vue';
interface Data {
    title: string
}

export default defineComponent({
    data(): Data {
        return {
            title: ''
        }
    },
    components: {
        ContentHeader,
    },
    created() {
        // fetch the data when the view is created and the data is
        // already being observed
        this.checkRouter();
    },
    watch: {
        // call again the method if the route changes
        '$route': 'checkRouter'
    },
    methods: {
        checkRouter() {
            const nameRoute = this.$route.name;
            this.title = nameRoute ? nameRoute.toString() : 'Index';
        }
    }
})
</script>