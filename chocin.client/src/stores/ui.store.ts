import { defineStore } from 'pinia';

const sideBarStatus = {
    open: 'sidebar-open',
    close: 'sidebar-collapse'
};

export const useUiStore = defineStore('ui', {
    state: () => {
        return {
            sideBarState: sideBarStatus.open
        }
    },

    actions: {
        togleSideBarState() {
            if (this.sideBarState != sideBarStatus.close) {
                this.sideBarState = sideBarStatus.close
            } else {
                this.sideBarState = sideBarStatus.open
            }
        }
    }
});
