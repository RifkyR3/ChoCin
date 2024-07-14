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
            sideBarNavigation: null as ModuleModel[] | null
        }
    },
    persist: {
        paths: [
            'sideBarNavigation'
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
            const token = await useAuthStore().getToken();
            const groupId = useAuthStore().userGroup?.groupId;

            const api: ModuleClient = new ModuleClient();
            api.setAuthToken(token);

            this.sideBarNavigation = await api.getModuleByGroup(groupId);
        }
    }
});