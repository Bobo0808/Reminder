import './assets/main.css'

import { createApp } from 'vue'
import App from './App.vue'
import router from './router'
import setAuthHeader from './setAuthHeader'

export const baseAddress = "https://localhost:7068";


if (localStorage.jwtToken) {
    setAuthHeader(localStorage.jwtToken);

} else {
    setAuthHeader(false);
}

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


app.use(router)

app.mount('#app')
