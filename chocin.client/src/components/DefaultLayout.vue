<template>
    <div class="layout-fixed sidebar-expand-lg bg-body-tertiary">
        <div class="app-wrapper">
            <TopBar />
            <SideBar />
            <slot />

        </div>
    </div>
</template>
<script setup lang='ts'>
import TopBar from "./TopBar.vue";
import SideBar from "./SideBar.vue";
</script>