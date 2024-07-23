<template>
    <div class="row">
        <div class="col-12">
            <div class="card">

                <div class="card-header">
                    <h3 class="card-title">
                        List User
                    </h3>
                    <div class="card-tools">
                        <button type="button" class="btn btn-tool btn-outline-primary" @click="btnAddUser">
                            <fas icon="plus"></fas>
                            Add User
                        </button>
                    </div>
                </div>

                <div class="card-body">
                    <table class="table table-bordered table-hover">
                        <thead>
                            <tr>
                                <th class="text-center fit">#</th>
                                <th class="fit">&nbsp;</th>
                                <th>Username</th>
                                <th>Name</th>
                                <th>Groups</th>
                            </tr>
                        </thead>
                        <tbody>
                            <tr v-for="value in users" :key="value.userId">
                                <td class="align-middle text-center fit">
                                </td>
                                <td class="align-middle text-center fit">
                                    <button title="Edit User" data-bs-toggle="tooltip" data-bs-placement="top"
                                            class="btn btn-outline-success btn-sm" v-on:click="btnEdit(value.userId)">
                                        <fas icon="pen-to-square"></fas>
                                    </button>
                                    <button title="Delete User" data-bs-toggle="tooltip" data-bs-placement="top"
                                            class="btn btn-outline-danger btn-sm pl-1"
                                            v-on:click="btnDeleteUser(value.userId)">
                                        <fas icon="trash-can"></fas>
                                    </button>
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
</template>
<script lang="ts">
import { defineComponent } from 'vue';
import { UserClient, type UserModel } from '@/helpers/webApi';
import Swal from 'sweetalert2';
import { useToast } from 'vue-toastification';

const userApi: UserClient = new UserClient();

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

            this.users = await userApi.getListUser();

            loader.hide();
        },
        btnAddUser() {
            this.$router.push('/users/input/');
        },
        async btnDeleteUser(userId: string) {
            Swal.fire({
                title: "Are you sure?",
                icon: "warning",
                showCancelButton: true,
                confirmButtonColor: 'red',
                confirmButtonText: "Yes, delete it!"
            }).then(async (result) => {
                if (result.isConfirmed) {
                    await this.doDeleteUser(userId);
                }
            });
        },
        async doDeleteUser(userId: string) {
            let loader = this.$loading.show();
            let res: boolean = true;
            try {
                await userApi.deleteUser(userId);

            } catch (e) {
                res = false;
            }

            this.users = await userApi.getListUser();

            if (res) {
                useToast().success('Successfully to delete User');
            } else {
                useToast().warning('Failed to delete User');
            }

            loader.hide();
        },
        btnEdit(userId: string) {
            this.$router.push('/users/input/' + userId);
        }
    }
})
</script>