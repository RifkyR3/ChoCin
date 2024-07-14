// import './assets/main.css'
import './index.scss';

import { createApp } from 'vue'
import App from './App.vue'
import router from './router';
import { createPinia } from 'pinia';
import piniaPluginPersistedstate from 'pinia-plugin-persistedstate';
import * as bootstrap from 'bootstrap';

import './main-icons';
import { FontAwesomeIcon } from '@fortawesome/vue-fontawesome';

import { ProfabricComponents } from '@profabric/vue-components';

import Toast, { type PluginOptions } from 'vue-toastification';
import { LoadingPlugin, type Props } from 'vue-loading-overlay';

import DefaultLayout from '@layouts/DefaultLayout.vue';
import EmptyLayout from '@layouts/EmptyLayout.vue';

const toastOptions: PluginOptions = {
    timeout: 3000,
    closeOnClick: true,
    pauseOnFocusLoss: true,
    pauseOnHover: true,
    draggable: true,
    draggablePercent: 0.6,
    showCloseButtonOnHover: false,
    hideProgressBar: false,
    closeButton: 'button',
    icon: true,
    rtl: false
};

const loadingOptions: Props = {
    color: 'blue',
    canCancel: false,
    isFullPage: true,
};

const pinia = createPinia();
pinia.use(piniaPluginPersistedstate);

const app = createApp(App);

app.use(router);
app.use(pinia);

app.use(Toast, toastOptions);
app.use(LoadingPlugin, loadingOptions);

app.use(ProfabricComponents);

app.component('fa-icon', FontAwesomeIcon);
app.component('empty-layout', EmptyLayout);
app.component('default-layout', DefaultLayout);
app.mount('#app');