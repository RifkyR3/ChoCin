<template>
    <ContentHeader title="Users" class="mb-2" />
    <div class="row">
        <div class="col-12">
            <div class="card">
                <div class="card-header">
                    <h3 class="card-title">
                        List User
                    </h3>
                </div>
                <div class="card-body">
                    <table class="table table-bordered">
                        <thead>
                            <tr>
                                <th style="width: 3%;">#</th>
                                <th>Username</th>
                                <th>Name</th>
                                <th>Groups</th>
                            </tr>
                        </thead>
                        <tbody>
                            <tr v-for="value in users" :key="value.userId">
                                <td></td>
                                <td>
                                    {{ value.userName }}
                                </td>
                                <td>
                                    {{ value.userFullName }}
                                </td>
                                <td>
                                    {{ value.groups?.map(e => e.groupName).join(', ') }}
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
import { UserClient, type UserModel } from '@/helpers/webApi';
import { useAuthStore } from '@/stores';

interface Data {
    users: UserModel[] | null
}

export default defineComponent({
    data(): Data {
        return {
            users: null
        }
    },
    components: {
        ContentHeader
    },
    created() {
        // fetch the data when the view is created and the data is
        // already being observed
        this.fetchUsers();
    },
    watch: {
        // call again the method if the route changes
        '$route': 'fetchUsers'
    },
    methods: {
        async fetchUsers() {
            let loader = this.$loading.show();
            const userApi: UserClient = new UserClient();

            userApi.setAuthToken(await useAuthStore().getToken());
            this.users = await userApi.getListUser();

            loader.hide();
        }
    }
})
</script>