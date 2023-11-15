import axios from "axios";
import router from "../router/index";

const instance = axios.create({
    baseURL: 'http://localhost:5000/api/',
    timeout: 10000
  });

export default instance;