<template>
    <div class="row">
        <div class="col-12">
            <div class="card">

                <div class="card-header">
                    <h3 class="card-title">
                        Group Input
                    </h3>
                </div>

                <form @submit.prevent="onSubmit">

                    <div class="card-body">
                        <div class="mb-3">
                            <label for="name" class="form-label">Name</label>
                            <input id="name" name="name" type="text" class="form-control" v-model="groupInput.groupName"
                                   required />
                        </div>

                        <div class="mb-3">
                            <label for="modules" class="form-label">Modules</label>

                            <div v-for="module in moduleTree" v-bind:key="module.id">
                                <div class="form-check">
                                    <input class="form-check-input" type="checkbox" :id="module.id" v-model="groupInput.moduleIds"
                                           :value="module.id">
                                    <label class="form-check-label" :for="module.id">
                                        <fas :icon="module.icon" v-if="module.icon"></fas> {{ module.name }}
                                    </label>
                                </div>

                                <div class="ms-4" v-if="module.children">
                                    <div class="form-check" v-for="children in module.children"
                                         v-bind:key="children.id">
                                        <input class="form-check-input" type="checkbox" :id="children.id"
                                               v-model="groupInput.moduleIds" :value="children.id">
                                        <label class="form-check-label" :for="children.id">
                                            <fas :icon="children.icon" v-if="children.icon"></fas> {{ children.name }}
                                        </label>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="card-footer">
                        <button type="submit" class="btn btn-primary">Submit</button>
                        &nbsp;
                        <button type="button" v-on:click="btnBack" class="btn btn-warning">Back</button>
                    </div>
                </form>
            </div>
        </div>
    </div>
</template>
<script lang="ts">
import { defineComponent } from 'vue';
import { GroupClient, type GroupModel, type AddUpdateGroup } from '@/helpers/webApi';
import { ModuleClient, type ModuleModel } from '@/helpers/webApi';
import { useToast } from 'vue-toastification';
import { useUiStore } from '@/stores';

const groupApi: GroupClient = new GroupClient();
const moduleApi: ModuleClient = new ModuleClient();

interface Data {
    group: GroupModel | null,
    groupId: string | null,
    groupInput: AddUpdateGroup,
    inputRes: boolean,
    chackbox: string[] | null,
    moduleTree: ModuleModel[] | null
}

export default defineComponent({
    data(): Data {
        return {
            group: null,
            groupId: null,
            inputRes: false,
            groupInput: {
                groupName: '',
                moduleIds: []
            },
            moduleTree: [],
            chackbox: []
        }
    },
    components: {
    },
    async mounted() {
        await this.getModuleTree();

        const groupId = this.$route.params.groupId;
        if (groupId) {
            this.groupId = groupId.toString();
            await this.getGroupById(this.groupId);
        }
    },
    methods: {
        async onSubmit() {
            let loader = this.$loading.show();

            if (this.groupId) {
                await this.doUpdate(this.groupId)
            } else {
                await this.doAdd();
            }

            if (this.inputRes) {
                this.$router.push('/groups/');
            }
            loader.hide();
        },
        async doAdd() {
            try {
                await groupApi.addGroup(this.groupInput);

                useToast().success('Successfully to add Group');
                this.inputRes = true;
            } catch (e) {
                useToast().warning('Failed to add Group');
                this.inputRes = false;
            }
        },
        async doUpdate(groupId: string) {
            try {
                await groupApi.updateGroup(groupId, this.groupInput);

                useUiStore().refreshNavigation = true;

                useToast().success('Successfully to update Group');
                this.inputRes = true;
            } catch (e) {
                useToast().warning('Failed to update Group');
                this.inputRes = false;
            }
        },
        async getGroupById(id: string) {
            let loader = this.$loading.show();

            try {
                this.group = await groupApi.getGroupById(id);

                this.groupInput = {
                    groupName: this.group.groupName,
                    moduleIds: this.group.groupModuleIds
                }

            } catch (e) {
                useToast().warning('Failed to get Group');
            }

            loader.hide();
        },
        btnBack() {
            this.$router.push('/groups');
        },
        async getModuleTree() {
            this.moduleTree = await moduleApi.getModuleTree();
        }
    },
})
</script>