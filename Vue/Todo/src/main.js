import './assets/main.css'

import { createApp } from 'vue'
import App from './App.vue'
import router from './router'
export const baseAddress = "https://localhost:7068";


/* import the fontawesome core */
import { library } from '@fortawesome/fontawesome-svg-core'

/* import font awesome icon component */
import { FontAwesomeIcon } from '@fortawesome/vue-fontawesome'

/* import specific icons */
import { faEnvelope, faLock, faPenToSquare, faUser, faUserSecret, faXmark } from '@fortawesome/free-solid-svg-icons'
import { faGoogle } from '@fortawesome/free-brands-svg-icons'

/* add icons to the library */
library.add(faUserSecret, faPenToSquare, faXmark, faUser, faLock, faEnvelope)


const app = createApp(App)
app.config.globalProperties.$token = null;
app.component('font-awesome-icon', FontAwesomeIcon)

const setToken = (token) => {
    app.config.globalProperties.$token = token;
    axios.defaults.headers.common['Authorization'] = `Bearer ${token}`;
};

const login = async () => {
    try {
        const response = await axios.post(`${API_URL}/api/User/login`, login.value);
        const token = response.data.token;
        setToken(token);
        console.log(response.data);
        app.mount('#app');
    } catch (error) {
        console.log(error);
    }
};

app.use(router)

app.mount('#app')
