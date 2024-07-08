// import './assets/main.css'
import './index.scss';

import { createApp } from 'vue'
import App from './App.vue'
import router from './router';
import { createPinia } from 'pinia'

import * as bootstrap from 'bootstrap';
import { library } from '@fortawesome/fontawesome-svg-core';
import { FontAwesomeIcon } from '@fortawesome/vue-fontawesome';
import { faEnvelope, faLock } from '@fortawesome/free-solid-svg-icons';

import { ProfabricComponents } from '@profabric/vue-components';
import Toast, { type PluginOptions } from 'vue-toastification';


library.add(faEnvelope, faLock);

const options: PluginOptions = {
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

const pinia = createPinia()
const app = createApp(App);
app.component('font-awesome-icon', FontAwesomeIcon)
    .use(router)
    .use(pinia)
    .use(Toast, options)
    .use(ProfabricComponents);

app.mount('#app');