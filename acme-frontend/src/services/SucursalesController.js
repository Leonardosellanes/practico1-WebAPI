import axios from "./axios";

export default {

    get() {
        return axios.get("Sucursal");
    },

    getById(id) {
        return axios.get('Sucursal/'+id);
    },

    create(body) {
        return axios.post('Sucursal', body);
    },

    update(body) {
        return axios.put('Sucursal', body);
    },

    delete(id) {
        return axios.delete('Sucursal/'+id);
    }
}