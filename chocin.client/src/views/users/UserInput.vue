<template>
    <div class="row">
        <div class="col-12">
            <div class="card">

                <div class="card-header">
                    <h3 class="card-title">
                        User Input
                    </h3>
                </div>

                <form @submit.prevent="onSubmit">

                    <div class="card-body">
                        <div class="mb-3">
                            <label for="name" class="form-label">Name</label>
                            <input id="name" name="name" type="text" class="form-control" v-model="userInput.name"
                                   required />
                        </div>

                        <div class="mb-3">
                            <label for="Username" class="form-label">Username</label>
                            <input id="username" name="username" type="text" class="form-control"
                                   v-model="userInput.userName" required />
                        </div>

                        <div class="mb-3">
                            <label for="Password" class="form-label">Password</label>
                            <input id="password" name="password" type="password" class="form-control"
                                   v-model="userInput.password" required />
                        </div>

                        <div class="mb-3">
                            <label for="Groups" class="form-label">Groups</label>
                            <multiselect :options="groupCombo" :multiple="true" :taggable="true" track-by="id"
                                         label="name" v-model="groupComboInput" />
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
import { UserClient, type AddUpdateUser, type GroupModel, type UserModel } from '@/helpers/webApi';
import { GroupClient, type DropDownModel } from '@/helpers/webApi';
import { useToast } from 'vue-toastification';
import Multiselect from 'vue-multiselect';

const userApi: UserClient = new UserClient();
const groupApi: GroupClient = new GroupClient();

interface Data {
    user: UserModel | null,
    userId: string | null,
    userInput: AddUpdateUser,
    inputRes: boolean,
    groupCombo: DropDownModel[],
    groupComboInput: DropDownModel[]
}

export default defineComponent({
    data(): Data {
        return {
            user: null,
            userId: null,
            inputRes: false,
            userInput: {
                name: '',
                password: '',
                userName: '',
                groups: []
            },
            groupCombo: [],
            groupComboInput: []
        }
    },
    components: {
        Multiselect
    },
    async mounted() {
        let loader = this.$loading.show();

        const userId = this.$route.params.userId;
        if (userId) {
            this.userId = userId.toString();
            await this.getUserById(this.userId);
        }

        await this.getGroupCombo();

        loader.hide();
    },
    methods: {
        async onSubmit() {
            let loader = this.$loading.show();

            let groups: GroupModel[] = [];

            if (this.groupComboInput.length > 0) {

                this.groupComboInput.forEach(input => {
                    const tmpGroup:GroupModel = {
                        groupId: input.id,
                        groupName: input.name,
                        groupModuleIds: undefined
                    };

                    groups.push(tmpGroup);
                });
            }

            this.userInput.groups = groups;

            if (this.userId) {
                await this.doUpdate(this.userId)
            } else {
                await this.doAdd();
            }

            if (this.inputRes) {
                this.$router.push('/users/');
            }
            loader.hide();
        },
        async doAdd() {
            try {
                await userApi.addUser(this.userInput);

                useToast().success('Successfully to add User');
                this.inputRes = true;
            } catch (e) {
                useToast().warning('Failed to add User');
                this.inputRes = false;
            }
        },
        async doUpdate(userId: string) {
            try {
                await userApi.updateUser(userId, this.userInput);

                useToast().success('Successfully to update User');
                this.inputRes = true;
            } catch (e) {
                useToast().warning('Failed to update User');
                this.inputRes = false;
            }
        },
        async getUserById(id: string) {
            try {
                this.user = await userApi.getUserById(id);

                this.userInput = {
                    name: this.user.userFullName == undefined ? '' : this.user.userFullName,
                    userName: this.user.userName,
                    password: '',
                    groups: this.user.groups
                }

                if (this.user.groups) {
                    this.user.groups.forEach(val => {
                        this.groupComboInput.push({
                            id: val.groupId,
                            name: val.groupName
                        });
                    });
                }

            } catch (e) {
                useToast().warning('Failed to get User');
            }
        },
        async getGroupCombo() {
            this.groupCombo = await groupApi.getComboGroup();
        },
        btnBack() {
            this.$router.push('/users');
        },
    },
})
</script>