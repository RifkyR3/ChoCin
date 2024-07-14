<template>
    <ContentHeader title="Groups" class="mb-2" />
    <div class="row">
        <div class="col-12">
            <div class="card">
                <div class="card-header">
                    <h3 class="card-title">
                        List Group
                    </h3>
                </div>
                <div class="card-body">
                    <table class="table table-bordered">
                        <thead>
                            <tr>
                                <th style="width: 3%;">#</th>
                                <th>Name</th>
                            </tr>
                        </thead>
                        <tbody>
                            <tr v-for="value in datas" :key="value.groupId">
                                <td></td>
                                <td>
                                    {{ value.groupName }}
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </div>
                <div class="card-footer">
                </div>
            </div>
        </div>
    </div>
</template>
<script lang="ts">
import { defineComponent } from 'vue';
import ContentHeader from '@components/ContentHeader.vue';
import { GroupClient, type GroupModel } from '@/helpers/webApi';

interface Data {
    datas: GroupModel[] | null
}

export default defineComponent({
    data(): Data {
        return {
            datas: null
        }
    },
    components: {
        ContentHeader
    },
    created() {
        // fetch the data when the view is created and the data is
        // already being observed
        this.fetch();
    },
    watch: {
        // call again the method if the route changes
        '$route': 'fetch'
    },
    methods: {
        async fetch() {
            let loader = this.$loading.show();
            const api: GroupClient = new GroupClient();

            this.datas = await api.getListGroup();

            loader.hide();
        }
    }
})
</script>