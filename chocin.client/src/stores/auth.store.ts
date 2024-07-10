import { defineStore } from 'pinia';
import router from '@/router';
import { Client, type JwtAuthResponse, type JwtLoginFormModel } from '../helpers/webApi';
import { useToast } from 'vue-toastification';

const api: Client = new Client();

export const useAuthStore = defineStore('auth', {
    state: () => {
        return {
            authenticate: localStorage.getItem('auth') ? (localStorage.getItem('auth') == '1') : false,
            user: localStorage.getItem('user') ? JSON.parse(localStorage.getItem('user') || '{}') : null,
            returnUrl: ''
        }
    },

    actions: {
        async login(username: string, password: string) {
            try {
                const form: JwtLoginFormModel = {
                    userName: username,
                    password: password
                };
                const response: JwtAuthResponse = await api.authenticate(form);

                this.user = response
                this.authenticate = true

                localStorage.setItem('auth', '1')
                localStorage.setItem('user', JSON.stringify(response))

                return await router.push(this.returnUrl || '/')
            } catch (error) {
                useToast().error("Please Try Again");
                return error
            }
        },
        async logout() {
            this.user = null;
            this.authenticate = false

            localStorage.removeItem('auth')
            localStorage.removeItem('user')

            return await router.push('/login')
        },
        async getToken() {
            if (this.authenticate && this.user != null) {
                return this.user.token;
            }

            useToast().warning("Your Session Expired. Please Signin Again.");
            return await this.logout();
        }
    }
})