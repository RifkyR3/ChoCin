import { defineStore } from 'pinia';

import { useUiStore } from './ui.store';
import { useTokenStore } from './token.store';

import router from '@/router';
import { useToast } from 'vue-toastification';

import { AuthClient, type JwtAuthResponse, type JwtLoginFormModel } from '../helpers/webApi';
import { UserClient, type UserModel, type GroupModel } from '../helpers/webApi';

export const useAuthStore = defineStore('auth', {
    state: () => {
        return {
            authenticate: false,
            credential: null as JwtAuthResponse | null,
            user: null as UserModel | null,
            userGroup: null as GroupModel | null,
            userGroupSelected: 0,
            returnUrl: ''
        }
    },
    persist: true,

    actions: {
        async login(username: string, password: string) {
            try {
                const form: JwtLoginFormModel = {
                    userName: username,
                    password: password
                };

                const api: AuthClient = new AuthClient();
                const response: JwtAuthResponse = await api.authenticate(form);

                if (!response.token || !response.id) {
                    useToast().error("Please Try Again");
                    return await router.push('/login');
                }

                this.credential = response;
                this.authenticate = true;

                // set Token
                useTokenStore().token = response.token;

                // set User
                this.user = {
                    userId: response.id,
                    userName: response.username,
                    userFullName: response.fullName,
                    groups: response.groups
                };

                // set Group
                this.setUserGroup(this.userGroupSelected)

                // set Module
                if (response.modules) {
                    useUiStore().sideBarNavigation = response.modules;
                }

                return await router.push(this.returnUrl || '/')
            } catch (error) {
                useToast().error("Please Try Again");
                return error
            }
        },
        async getToken() {
            if (this.authenticate && this.credential != null) {
                return this.credential.token;
            }

            useToast().warning("Your Session Expired. Please Signin Again.");
            return await router.push('/logout')
        },
        async setUser(userId: number, token: string) {
            const apiUser: UserClient = new UserClient();
            apiUser.setAuthToken(token);
            this.user = await apiUser.getUserById(userId);

            this.setUserGroup(this.userGroupSelected)
        },
        setUserGroup(key: number) {
            if (this.user?.groups) {
                this.userGroupSelected = key;
                this.userGroup = this.user.groups[this.userGroupSelected];
            }
        } 
    }
})