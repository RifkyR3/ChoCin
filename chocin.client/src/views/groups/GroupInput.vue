<template>
    <ContentHeader title="Groups" class="mb-2" />
    <div class="app-content">
        <div class="container-fluid">
            <div class="row">
                <div class="col-12">
                    <div class="card">

                        <div class="card-header">
                            <h3 class="card-title">
                                Add Group
                            </h3>
                        </div>

                        <form @submit.prevent="onSubmit">

                            <div class="card-body">
                                <div class="mb-3">
                                    <label for="name" class="form-label">Name</label>
                                    <input id="name" name="name" type="text" class="form-control"
                                        v-model="groupInput.groupName" required />
                                </div>
                            </div>

                            <div class="card-footer">
                                <button type="submit" class="btn btn-primary">Submit</button>
                            </div>

                        </form>

                    </div>
                </div>
            </div>
        </div>
    </div>

</template>
<script lang="ts">
import { defineComponent } from 'vue';
import ContentHeader from '@components/ContentHeader.vue';
import { GroupClient, type GroupModel} from '@/helpers/webApi';
import { useToast } from 'vue-toastification';

const groupApi : GroupClient = new GroupClient();

interface Data {
    group: GroupModel | null,
    groupId: number | null,
    groupInput: GroupModel,
    inputRes: boolean,
}

export default defineComponent({
    data(): Data {
        return {
            group: null,
            groupId: null,
            inputRes: false,
            groupInput: {
                groupId: 0,
                groupName: ''
            }
        }
    },
    components: {
        ContentHeader,
    },
    async mounted() {
        const groupId = this.$route.params.groupId;
        if (groupId) {
            this.groupId = Number.parseInt(groupId.toString());
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
                const res = await groupApi.addGroup(this.groupInput);

                useToast().success('Successfully to add Group');
                this.inputRes = true;
            } catch (e) {
                useToast().warning('Failed to add Group');
                this.inputRes = false;
            }
        },
        async doUpdate(groupId: number) {
            try {
                await groupApi.updateGroup(groupId, this.groupInput);
                
                useToast().success('Successfully to update Group');
                this.inputRes = true;
            } catch (e) {
                useToast().warning('Failed to update Group');
                this.inputRes = false;
            }
        },
        async getGroupById(id: number) {
            let loader = this.$loading.show();

            try {
                this.group = await groupApi.getGroupById(id);

                this.groupInput = {
                    groupId: this.group.groupId,
                    groupName: this.group.groupName
                }

            } catch (e) {
                useToast().warning('Failed to get Group');
            }

            loader.hide();
        }
    },
})
</script>