import axios from "axios";
import router from "../router/index";

const instance = axios.create({
    baseURL: 'http://localhost:5000/api/',
    timeout: 10000
});

//errores de autentificacion
instance.interceptors.response.use(
    (response) => response,
    (error) => {
        if (error.response && error.response.status === 401) {
            router.push('/login');
        }
        return Promise.reject(error);
    }
);

export default instance;
