<script lang="ts" setup>
import { Form, Field } from 'vee-validate';
import * as Yup from 'yup';

import { useAuthStore } from '@/stores'

const schema = Yup.object().shape({
    username: Yup.string().required('Username is required'),
    password: Yup.string().required('Password is required')
});

async function onSubmit(values: any) {
    const { username, password } = values;

    const authStore = useAuthStore()

    return await authStore.login(username, password);
}
</script>
<template>
    <div class="login-page bg-body-secondary">
        <div class="login-box">
            <div class="card card-outline card-primary">
                <div class="card-header">
                    <div class="login-logo"> <b>Admin</b>LTE </div>
                </div>

                <div class="card-body login-card-body">
                    <p class="login-box-msg">Sign in to start your session</p>

                    <Form @submit="onSubmit" :validation-schema="schema" v-slot="{ errors, isSubmitting }">
                        <div class="input-group mb-3">
                            <Field name="username" type="text" class="form-control" :disabled="isSubmitting"
                                :class="{ 'is-invalid': errors.username }" />
                            <div class="input-group-text">
                                <span><fa-icon icon="fa-user" /></span>
                            </div>
                            <div class="invalid-feedback">{{ errors.username }}</div>
                        </div>

                        <div class="input-group mb-3">
                            <Field name="password" type="password" class="form-control" :disabled="isSubmitting"
                                :class="{ 'is-invalid': errors.password }" />
                            <div class="input-group-text">
                                <span><fa-icon icon="fa-lock" /></span>
                            </div>
                            <div class="invalid-feedback">{{ errors.password }}</div>
                        </div>

                        <div class="row">

                            <div class="col-12 mb-3">
                                <div class="d-grid gap-2">
                                    <button :disabled="isSubmitting" type="submit" class="btn btn-primary">
                                        <span v-show="isSubmitting" class="spinner-border spinner-border-sm"></span>
                                        Sign In
                                    </button>
                                </div>
                            </div>
                        </div>
                    </Form>
                    <p class="mb-1"> <a href="forgot-password.html">I forgot my password</a> </p>
                    <p class="mb-0">
                        <a href="register.html" class="text-center">
                            Register a new membership
                        </a>
                    </p>
                </div>
            </div>
        </div>
    </div>
</template>