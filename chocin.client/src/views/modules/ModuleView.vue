<template>
    <div class="row">
        <div class="col-12">
            <div class="card">
                <div class="card-header">
                    <h3 class="card-title">
                        List Module
                    </h3>
                    <div class="card-tools">
                        <button type="button" class="btn btn-tool btn-outline-primary" @click="btnAdd">
                            <fas icon="plus"></fas>
                            Add Module
                        </button>
                    </div>
                </div>
                <div class="card-body">
                    <table class="table table-bordered">
                        <thead>
                            <tr>
                                <th style="width: 3%; text-align: center;">#</th>
                                <th style="width: 5%;">&nbsp;</th>
                                <th>Name</th>
                                <th>Icon</th>
                                <th>Path</th>
                                <th>Order</th>
                            </tr>
                        </thead>
                        <tbody>
                            <tr v-for="value in datas" :key="value.id">
                                <td class="align-middle text-center">
                                </td>
                                <td class="align-middle text-center">
                                    <button title="Edit Group" data-bs-toggle="tooltip" data-bs-placement="top"
                                            class="btn btn-outline-success btn-sm" v-on:click="btnEdit(value.id)">
                                        <fas icon="pen-to-square"></fas>
                                    </button>
                                    <button title="Delete Group" data-bs-toggle="tooltip" data-bs-placement="top"
                                            class="btn btn-outline-danger btn-sm pl-1"
                                            v-on:click="btnDelete(value.id)">
                                        <fas icon="trash-can"></fas>
                                    </button>
                                </td>
                                <td>
                                    {{ value.name }}
                                </td>
                                <td>
                                    <fas v-if="value.icon" :icon="value.icon"></fas>&nbsp;{{ value.icon }}
                                </td>
                                <td>
                                    {{ value.path }}
                                </td>
                                <td>
                                    {{ value.order }}
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
import { ModuleClient, type ModuleModel } from '@/helpers/webApi';
import Swal from 'sweetalert2';
import { useToast } from 'vue-toastification';

const api: ModuleClient = new ModuleClient();

interface Data {
    datas: ModuleModel[] | null
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

            this.datas = await api.getListModule();

            loader.hide();
        },
        btnAdd() {
            this.$router.push('/modules/input/');
        },
        async btnDelete(moduleId: string) {
            Swal.fire({
                title: "Are you sure?",
                icon: "warning",
                showCancelButton: true,
                confirmButtonColor: 'red',
                confirmButtonText: "Yes, delete it!"
            }).then(async (result) => {
                if (result.isConfirmed) {
                    await this.doDelete(moduleId);
                }
            });
        },
        async doDelete(moduleId: string) {
            let loader = this.$loading.show();
            let res: boolean = true;
            try {
                await api.deleteModule(moduleId);

            } catch (e) {
                res = false;
            }

            this.datas = await api.getListModule();

            if (res) {
                useToast().success('Successfully to delete Module');
            } else {
                useToast().warning('Failed to delete Module');
            }

            loader.hide();
        },
        btnEdit(moduleId: string) {
            this.$router.push('/modules/input/' + moduleId);
        }
    }
})
</script>