<template>
    <ContentHeader title="Users" class="mb-2" />
    <div class="app-content">
        <div class="container-fluid">
            <div class="row">
                <div class="col-12">
                    <div class="card">

                        <div class="card-header">
                            <h3 class="card-title">
                                Add User
                            </h3>
                        </div>

                        <form @submit.prevent="onSubmit">

                            <div class="card-body">
                                <div class="mb-3">
                                    <label for="name" class="form-label">Name</label>
                                    <input id="name" name="name" type="text" class="form-control"
                                        v-model="userInput.name" required />
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
import { UserClient, type AddUpdateUser, type UserModel } from '@/helpers/webApi';
import { useToast } from 'vue-toastification';

const userApi: UserClient = new UserClient();

interface Data {
    user: UserModel | null,
    userId: number | null,
    userInput: AddUpdateUser,
    inputRes: boolean,
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
                userName: ''
            }
        }
    },
    components: {
        ContentHeader,
    },
    async mounted() {
        const userId = this.$route.params.userId;
        if (userId) {
            this.userId = Number.parseInt(userId.toString());
            await this.getUserById(this.userId);
        }
    },
    methods: {
        async onSubmit() {
            let loader = this.$loading.show();

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
                const res = await userApi.addUser(this.userInput);

                console.log(res);
                useToast().success('Successfully to add User');
                this.inputRes = true;
            } catch (e) {
                useToast().warning('Failed to add User');
                this.inputRes = false;
            }
        },
        async doUpdate(userId: number) {
            try {
                await userApi.updateUser(userId, this.userInput);
                
                useToast().success('Successfully to update User');
                this.inputRes = true;
            } catch (e) {
                useToast().warning('Failed to update User');
                this.inputRes = false;
            }
        },
        async getUserById(id: number) {
            let loader = this.$loading.show();

            // console.log(this.userInput);
            try {
                this.user = await userApi.getUserById(id);

                this.userInput = {
                    name: this.user.userFullName == undefined ? '' : this.user.userFullName,
                    userName: this.user.userName,
                    password: ''
                }

            } catch (e) {
                useToast().warning('Failed to get User');
            }

            loader.hide();
        }
    },
})
</script>