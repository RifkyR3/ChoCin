import { ModuleClient, type ModuleModel } from '@/helpers/webApi';
import { defineStore } from 'pinia';
import { useAuthStore } from './auth.store';

const sideBarStatus = {
    open: 'sidebar-open',
    close: 'sidebar-collapse'
};

export const useUiStore = defineStore('ui', {
    state: () => {
        return {
            sideBarState: sideBarStatus.open,
            sideBarNavigation: null as ModuleModel[] | null,
            refreshNavigation: false
        }
    },
    persist: {
        paths: [
            'sideBarNavigation',
            'refreshNavigation'
        ]
    },

    actions: {
        togleSideBarState() {
            if (this.sideBarState != sideBarStatus.close) {
                this.sideBarState = sideBarStatus.close
            } else {
                this.sideBarState = sideBarStatus.open
            }
        },
        async getModule() {
            const groupId = useAuthStore().userGroup?.groupId;

            const api: ModuleClient = new ModuleClient();

            if (groupId) {
                this.sideBarNavigation = await api.getModuleByGroup(groupId);
            }
        }
    }
});