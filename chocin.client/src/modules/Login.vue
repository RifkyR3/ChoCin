<script lang="ts" setup>
    import { Field, Form } from 'vee-validate'
    import * as Yup from 'yup'
    import { useAuthStore } from '@/stores'
    import { defineComponent } from 'vue';

    var store = {
        username: '',
        password: ''
    };

    async function onSubmit() {
        const authStore = useAuthStore()

        return await authStore.login(store.username, store.password);
    }
</script>
<template>
    <div class="login-page bg-body-secondary">
        <div class="login-box">
            <div class="login-logo"> <b>Admin</b>LTE </div>

            <div class="card">
                <div class="card-body login-card-body">
                    <p class="login-box-msg">Sign in to start your session</p>

                    <form @submit.prevent="onSubmit">
                        <div class="input-group mb-3">
                            <input type="text" name="username" v-model="store.username" class='form-control' />
                            <div class="input-group-text"> <span class="bi bi-envelope"></span> </div>
                        </div>

                        <div class="input-group mb-3">
                            <input type="password" name="password" v-model="store.password" class='form-control' />
                            <div class="input-group-text"> <span class="bi bi-lock-fill"></span> </div>
                        </div> 
                        
                        <div class="row">
                            <div class="col-8">
                                <div class="form-check">
                                    <input class="form-check-input" type="checkbox" value="" id="flexCheckDefault"> <label class="form-check-label" for="flexCheckDefault">
                                        Remember Me
                                    </label>
                                </div>
                            </div> 
                            
                            <div class="col-4">
                                <div class="d-grid gap-2"> <button type="submit" class="btn btn-primary">Sign In</button> </div>
                            </div> 
                            
                        </div> 
                        
                    </form>
                    <p class="mb-1"> <a href="forgot-password.html">I forgot my password</a> </p>
                    <p class="mb-0">
                        <a href="register.html" class="text-center">
                            Register a new membership
                        </a>
                    </p>
                </div> <!-- /.login-card-body -->
            </div>
        </div>
    </div>
</template>