<template>
    <li v-if="menuItem" class="nav-item" :class="{ 'menu-open': isMenuExtended }">
        <a class="nav-link" :class="{ 'active': isMainActive || isOneOfChildrenActive }" @click="handleMainMenuAction">
            <fas class="nav-icon mt-1" :icon=menuItem.icon v-if=menuItem.icon></fas>
            <p>
                {{ (menuItem.name) }}
                <fas v-if="isExpandable" class="nav-arrow" icon="chevron-right"></fas>
            </p>
        </a>
        <ul class="nav nav-treeview" v-for="item in menuItem.children" :key="item.name">
            <li class="nav-item">
                <router-link :to="item.path" class="nav-link" active-class="active">
                    <fas class="nav-icon mt-1" :icon=item.icon v-if=item.icon></fas>
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
            const path = this.$router.currentRoute.value.path;
            this.calculateIsActive(path);
        }
        this.$router.afterEach((to) => {
            const path = to.path;
            this.calculateIsActive(path);
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
                    if (this.isChild(item.path)) {
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

        isChild(path: string) {
            return this.$route.matched.map((item) => {
                return item.path;
            }).includes(path);
        }
    }
}
</script>