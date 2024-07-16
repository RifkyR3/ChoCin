<template>
    <ContentHeader title="Users" class="mb-2" />
    <div class="app-content">
        <div class="container-fluid">
            <div class="row">
                <div class="col-12">
                    <div class="card">

                        <div class="card-header">
                            <h3 class="card-title">
                                List User
                            </h3>
                            <div class="card-tools">
                                <button type="button" class="btn btn-tool btn-outline-primary" @click="btnAddUser">
                                    <fa-icon icon="plus"></fa-icon>
                                    Add User
                                </button>
                            </div>
                        </div>

                        <div class="card-body">
                            <table class="table table-bordered">
                                <thead>
                                    <tr>
                                        <th style="width: 3%;">#</th>
                                        <th style="width: 3%;">&nbsp;</th>
                                        <th>Username</th>
                                        <th>Name</th>
                                        <th>Groups</th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <tr v-for="value in users" :key="value.userId">
                                        <td class="align-middle text-center">
                                        </td>
                                        <td class="align-middle text-center">
                                            <RouterLink :to="'/users/input/'+value.userId">
                                                <fa-icon icon="pen-to-square"></fa-icon>
                                            </RouterLink>
                                        </td>
                                        <td class="align-middle">
                                            {{ value.userName }}
                                        </td>
                                        <td class="align-middle">
                                            {{ value.userFullName }}
                                        </td>
                                        <td class="align-middle">
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
        </div>

    </div>

</template>
<script lang="ts">
import { defineComponent } from 'vue';
import ContentHeader from '@components/ContentHeader.vue';
import { UserClient, type UserModel } from '@/helpers/webApi';

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

            this.users = await userApi.getListUser();

            loader.hide();
        },
        btnAddUser() {
            this.$router.push('/users/input/');
        },
        btnDeleteUser(userId:number) {
            this.
        }
    }
})
</script>