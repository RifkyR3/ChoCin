<template>
    <div class="row">
        <div class="col-12">
            <div class="card">
                <div class="card-header">
                    <h3 class="card-title">
                        List Group
                    </h3>
                    <div class="card-tools">
                        <button type="button" class="btn btn-tool btn-outline-primary" @click="btnAdd">
                            <fas icon="plus"></fas>
                            Add Group
                        </button>
                    </div>
                </div>
                <div class="card-body">
                    <table class="table table-bordered">
                        <thead>
                            <tr>
                                <th style="width: 3%;">#</th>
                                <th style="width: 5%;">&nbsp;</th>
                                <th>Name</th>
                            </tr>
                        </thead>
                        <tbody>
                            <tr v-for="value in datas" :key="value.groupId">
                                <td class="align-middle text-center">
                                </td>
                                <td class="align-middle text-center">
                                    <button title="Edit Group" data-bs-toggle="tooltip" data-bs-placement="top"
                                        class="btn btn-outline-success btn-sm" v-on:click="btnEdit(value.groupId)">
                                        <fas icon="pen-to-square"></fas>
                                    </button>
                                    <button title="Delete Group" data-bs-toggle="tooltip" data-bs-placement="top"
                                        class="btn btn-outline-danger btn-sm pl-1"
                                        v-on:click="btnDelete(value.groupId)">
                                        <fas icon="trash-can"></fas>
                                    </button>
                                </td>
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
import { GroupClient, type GroupModel } from '@/helpers/webApi';
import Swal from 'sweetalert2';
import { useToast } from 'vue-toastification';

const api: GroupClient = new GroupClient();

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

            this.datas = await api.getListGroup();

            loader.hide();
        },
        btnAdd() {
            this.$router.push('/groups/input/');
        },
        async btnDelete(groupId: string) {
            Swal.fire({
                title: "Are you sure?",
                icon: "warning",
                showCancelButton: true,
                confirmButtonColor: 'red',
                confirmButtonText: "Yes, delete it!"
            }).then(async (result) => {
                if (result.isConfirmed) {
                    await this.doDelete(groupId);
                }
            });
        },
        async doDelete(groupId: string) {
            let loader = this.$loading.show();
            let res: boolean = true;
            try {
                await api.deleteGroup(groupId);

            } catch (e) {
                res = false;
            }

            this.datas = await api.getListGroup();

            if (res) {
                useToast().success('Successfully to delete Group');
            } else {
                useToast().warning('Failed to delete Group');
            }

            loader.hide();
        },
        btnEdit(groupId: string) {
            this.$router.push('/groups/input/' + groupId);
        }
    }
})
</script>