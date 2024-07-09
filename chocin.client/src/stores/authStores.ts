import { defineStore } from 'pinia';
import router from '@/router';
import { Client, type JwtAuthResponse, type JwtLoginFormModel } from '../helpers/webApi';

let api: Client = new Client();

export const useAuthStore = defineStore('auth', {
    state: () => {
        return {
            authenticate: localStorage.getItem('auth') ? (localStorage.getItem('auth') == '1') : false,
            user: JSON.parse(localStorage.getItem('user') || '{}'),
            token: localStorage.getItem('token') ? localStorage.getItem('token') : '',
            returnUrl: ''
        }
    },

    actions: {
        async login(username: string, password: string) {
            try {
                let form: JwtLoginFormModel = {
                    userName: username,
                    password: password
                };
                let response: JwtAuthResponse = await api.authenticate(form);

                this.user = response
                this.authenticate = true
                this.token = response.token ? response.token : '-';

                localStorage.setItem('auth', '1')
                localStorage.setItem('user', JSON.stringify(response))
                localStorage.setItem('token', this.token)

                return await router.push(this.returnUrl || '/')
            } catch (error) {
                return error
            }
        },
        async logout() {
            this.user = null;
            this.authenticate = false
            this.token = ''

            localStorage.removeItem('auth')
            localStorage.removeItem('user')
            localStorage.removeItem('token')

            return await router.push('/login')
        }
    }
})