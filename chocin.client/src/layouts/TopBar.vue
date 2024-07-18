<template>
    <nav class="app-header navbar navbar-expand bg-body">
        <div class="container-fluid">
            <!-- Kiri -->
            <ul class="navbar-nav">
                <li class="nav-item">
                    <button class="nav-link" @click="uiStore.togleSideBarState()">
                        <fas icon="fa-bars" />
                    </button>
                </li>
                <li class="nav-item d-none d-md-block">
                    <router-link class="nav-link" to='/'>Home</router-link>
                </li>
            </ul>

            <!-- Kanan -->
            <ul class="navbar-nav ms-auto">
                <!--begin::User Menu Dropdown-->
                <li class="nav-item dropdown user-menu">
                    <a href="#" class="nav-link dropdown-toggle" data-bs-toggle="dropdown">
                        <img src="../assets/avatar.png" class="user-image rounded-circle shadow" alt="User Image">
                        <span class="d-none d-md-inline">{{ userLogin }}</span>
                    </a>
                    <ul class="dropdown-menu dropdown-menu-lg dropdown-menu-end">
                        <!--begin::User Image-->
                        <li class="user-header text-bg-primary">
                            <img src="../assets/avatar.png" class="rounded-circle shadow" alt="User Image">
                            <p>
                                {{ userLogin }}
                                <small>{{ userGroupName }}</small>
                            </p>
                        </li>
                        <!--end::User Image-->
                        <!--begin::Menu Footer-->
                        <li class="user-footer">
                            <router-link class="btn btn-default btn-flat float-end" to='/logout'>Sign out</router-link>
                        </li>
                        <!--end::Menu Footer-->
                    </ul>
                </li> <!--end::User Menu Dropdown-->
            </ul>
        </div>
    </nav>
</template>
<script setup lang='ts'>
import { useUiStore, useAuthStore } from '@/stores'

const authStore = useAuthStore();
const uiStore = useUiStore();

const userLogin = authStore.credential?.fullName;
const userGroupName = authStore.userGroup?.groupName;
</script>