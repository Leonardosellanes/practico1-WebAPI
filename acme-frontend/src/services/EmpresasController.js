import axios from "./axios";

//const url =  "http://localhost:32769/api/Empresa";

export default {

    get() {
        return axios.get("Empresa");
    },

    getById(id) {
        return axios.get('Empresa/'+15);
    },

    create(nombre, rut) {
        let body = { Nombre: nombre, RUT: rut };
        return axios.post('Empresa', body);
    },

    update(id, nombre, rut) {
        let body = { Id: id, Nombre: nombre, RUT: rut };
        return axios.put('Empresa', body);
    },

    delete(id) {
        return axios.delete('Empresa/'+id);
    }
}