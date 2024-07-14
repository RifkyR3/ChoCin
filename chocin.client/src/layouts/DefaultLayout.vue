<template>
    <div class="layout-fixed sidebar-expand-lg sidebar-mini bg-body-tertiary " :class="useUiStore().sideBarState">
        <div class="app-wrapper">
            <TopBar />
            <SideBar />
            <main class="app-main">
                <slot />
            </main>
        </div>
    </div>
</template>
<script setup lang='ts'>
import TopBar from "./TopBar.vue";
import SideBar from "./SideBar.vue";

import { useUiStore } from '@/stores'
</script>