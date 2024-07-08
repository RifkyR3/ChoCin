import { defineStore } from 'pinia';
import { Client, type JwtAuthResponse, type JwtLoginFormModel } from '../webApi';

let api: Client = new Client();

export const useAuthStore = defineStore('auth', {
    state: () => {
        return {
            authenticate: localStorage.getItem('auth') ? (localStorage.getItem('auth') == '1') : false,
            user: JSON.parse(localStorage.getItem('user') || '{}'),
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
                localStorage.setItem('auth', '1')
                localStorage.setItem('user', JSON.stringify(response))
            } catch (error) {
                return error
            }
        }
    }
})