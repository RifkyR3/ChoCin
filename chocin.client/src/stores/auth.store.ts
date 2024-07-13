import { defineStore } from 'pinia';
import router from '@/router';
import { AuthClient, type JwtAuthResponse, type JwtLoginFormModel } from '../helpers/webApi';
import { useToast } from 'vue-toastification';

export const useAuthStore = defineStore('auth', {
    state: () => {
        return {
            authenticate: false,
            credential: null as JwtAuthResponse | null,
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

                const api: AuthClient = new AuthClient();
                const response: JwtAuthResponse = await api.authenticate(form);

                this.credential = response
                this.authenticate = true

                localStorage.setItem('auth', '1')
                localStorage.setItem('credential', JSON.stringify(response))

                return await router.push(this.returnUrl || '/')
            } catch (error) {
                useToast().error("Please Try Again");
                return error
            }
        },
        async logout() {
            this.credential = null;
            this.authenticate = false

            localStorage.removeItem('auth')
            localStorage.removeItem('credential')

            return await router.push('/login')
        },
        async getToken() {
            if (this.authenticate && this.credential != null) {
                return this.credential.token;
            }

            useToast().warning("Your Session Expired. Please Signin Again.");
            return await this.logout();
        }
    }
})