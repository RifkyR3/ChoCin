<template>
    <div class="row">
        <div class="col-12">
            <div class="card">

                <div class="card-header">
                    <h3 class="card-title">
                        Module Input
                    </h3>
                </div>

                <form @submit.prevent="onSubmit">

                    <div class="card-body">
                        <div class="mb-3">
                            <label for="name" class="form-label">Name</label>
                            <input id="name" name="name" type="text" class="form-control" v-model="moduleInput.name"
                                   required />
                        </div>

                        <div class="mb-3">
                            <label for="icon" class="form-label">Icon</label>
                            <div class="input-group">
                                <span class="input-group-text" id="icon-view">
                                    <fas :icon="moduleInput.icon"></fas>
                                </span>
                                <input id="icon" name="name" type="text" class="form-control" v-model="moduleInput.icon"
                                       required aria-describedby="icon-view" />
                            </div>
                        </div>

                        <div class="mb-3">
                            <label for="path" class="form-label">Path</label>
                            <input id="path" name="path" type="text" class="form-control" v-model="moduleInput.path"
                                   required />
                        </div>

                        <div class="mb-3">
                            <label for="order" class="form-label">Order</label>
                            <input id="order" name="order" type="number" class="form-control"
                                   v-model="moduleInput.order" required />
                        </div>

                        <div class="mb-3">
                            <label for="sub_module" class="form-label">Main Module</label>
                            <multiselect id="sub_module" :options="subModuleCombo" :multiple="false" track-by="id"
                                         label="name" v-model="subModuleComboInput" />
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
import { ModuleClient, type ModuleModel, type AddUpdateModule, type DropDownModel } from '@/helpers/webApi';
import { useToast } from 'vue-toastification';
import Multiselect from 'vue-multiselect';

const api: ModuleClient = new ModuleClient();

interface Data {
    module: ModuleModel | null,
    moduleId: string | null,
    moduleInput: AddUpdateModule,
    inputRes: boolean,
    subModuleCombo: DropDownModel[],
    subModuleComboInput: DropDownModel | null
}

export default defineComponent({
    data(): Data {
        return {
            module: null,
            moduleId: null,
            inputRes: false,
            moduleInput: {
                name: '',
                icon: '',
                path: '',
                order: 0,
                subModuleId: undefined
            },
            subModuleCombo: [],
            subModuleComboInput: null
        }
    },
    components: {
        Multiselect
    },
    async mounted() {
        const moduleId = this.$route.params.moduleId;
        if (moduleId) {
            this.moduleId = moduleId.toString();
            await this.getModuleById(this.moduleId);
        }

        await this.getModuleCombo();

        if (this.moduleInput && this.moduleInput.subModuleId) {
            const searchSubId = this.subModuleCombo.find(x => x.id == this.moduleInput.subModuleId);
            if (searchSubId) {
                this.subModuleComboInput = searchSubId;
            }
        }

    },
    methods: {
        async onSubmit() {
            let loader = this.$loading.show();

            let subModuleId = undefined;
            if (this.subModuleComboInput) {
                subModuleId = this.subModuleComboInput.id;
            }
            this.moduleInput.subModuleId = subModuleId;

            console.log(this.moduleInput)

            if (this.moduleId) {
                await this.doUpdate(this.moduleId)
            } else {
                await this.doAdd();
            }

            if (this.inputRes) {
                this.$router.push('/modules/');
            }
            loader.hide();
        },
        async doAdd() {
            try {
                await api.addModule(this.moduleInput);

                useToast().success('Successfully to add Module');
                this.inputRes = true;
            } catch (e) {
                useToast().warning('Failed to add Module');
                this.inputRes = false;
            }
        },
        async doUpdate(moduleId: string) {
            try {
                await api.updateModule(moduleId, this.moduleInput);

                useToast().success('Successfully to update Module');
                this.inputRes = true;
            } catch (e) {
                useToast().warning('Failed to update Module');
                this.inputRes = false;
            }
        },
        async getModuleById(id: string) {
            let loader = this.$loading.show();

            try {
                this.module = await api.getModuleById(id);

                this.moduleInput = {
                    name: this.module.name,
                    icon: this.module.icon,
                    order: this.module.order,
                    path: this.module.path,
                    subModuleId: this.module.subId
                }

            } catch (e) {
                useToast().warning('Failed to get Module');
            }

            loader.hide();
        },
        btnBack() {
            this.$router.push('/modules');
        },
        async getModuleCombo() {
            this.subModuleCombo = await api.getComboMainModule();
        },
    },
})
</script>