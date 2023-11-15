import axios from "axios";

const url =  "http://localhost:32769/api/Empresa";

export default {

    get() {
        return axios.get(url);
    },

    getById(id) {
        return axios.get(url+'/'+id);
    },

    create(nombre, rut) {
        let body = { Nombre: nombre, RUT: rut };
        return axios.post(url, JSON.stringify(body));
    },

    update(id, nombre, rut) {
        let body = { Id: id, Nombre: nombre, RUT: rut };
        return axios.put(url, JSON.stringify(body));
    },

    delete(id) {
        return axios.delete(url+'/'+id);
    }
}