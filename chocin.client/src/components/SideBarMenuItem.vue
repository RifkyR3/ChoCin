<template>
    <li v-if="menuItem" class="nav-item" :class="{ 'menu-open': isMenuExtended }">
        <a class="nav-link" :class="{ 'active': isMainActive || isOneOfChildrenActive }" @click="handleMainMenuAction">
            <fa-icon class="nav-icon mt-1" :icon=menuItem.icon v-if=menuItem.icon></fa-icon>
            <p>
                {{ (menuItem.name) }}
                <fa-icon v-if="isExpandable" class="nav-arrow" icon="chevron-right"></fa-icon>
            </p>
        </a>
        <ul class="nav nav-treeview" v-for="item in menuItem.children" :key="item.name">
            <li class="nav-item">
                <router-link :to="item.path" class="nav-link" exact exact-active-class="active">
                    <fa-icon class="nav-icon mt-1" :icon=item.icon v-if=item.icon></fa-icon>
                    <p>{{ (item.name) }}</p>
                </router-link>
            </li>
        </ul>
    </li>
</template>
<script lang='ts'>
interface DataMenuItem {
    isMenuExtended: boolean,
    isExpandable: boolean,
    isMainActive: boolean,
    isOneOfChildrenActive: boolean,
}
export default {
    data(): DataMenuItem {
        return {
            isMenuExtended: false,
            isExpandable: false,
            isMainActive: false,
            isOneOfChildrenActive: false,
        }
    },
    props: ['menuItem'],
    mounted() {
        this.isExpandable =
            this.menuItem &&
            this.menuItem.children &&
            this.menuItem.children.length > 0;
        if (this.$router.currentRoute && this.$router.currentRoute.value) {
            this.calculateIsActive(this.$router.currentRoute.value.path);
        }
        this.$router.afterEach((to) => {
            this.calculateIsActive(to.path);
        });
    },
    methods: {
        handleMainMenuAction() {
            if (this.isExpandable) {
                this.toggleMenu();
                return;
            }
            this.$router.replace(this.menuItem.path);
        },

        toggleMenu() {
            this.isMenuExtended = !this.isMenuExtended;
        },

        calculateIsActive(url: string) {
            this.isMainActive = false;
            this.isOneOfChildrenActive = false;
            if (this.isExpandable) {
                this.menuItem.children.forEach((item: any) => {
                    if (item.path === url) {
                        this.isOneOfChildrenActive = true;
                        this.isMenuExtended = true;
                    }
                });
            } else if (this.menuItem?.path === url) {
                this.isMainActive = true;
            }
            if (!this.isMainActive && !this.isOneOfChildrenActive) {
                this.isMenuExtended = false;
            }
        },
    }
}
</script>