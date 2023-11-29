import axios from "./axios";

//const url =  "http://localhost:32769/api/Empresa";

export default {

    get() {
        return axios.get("Empresa");
    },

    getEmpleados(id) {
        return axios.get("AppUsers/" + id);
    },
    
    getById(id) {
        return axios.get('Empresa/'+1);
    },

    create(nombre, rut) {
        let body = { Nombre: nombre, RUT: rut };
        return axios.post('Empresa', body);
    },

    createEmpleados(body) {
        return axios.post('Auth/RegisterEmpleado' ,body)
    },

    update(id, nombre, rut) {
        let body = { Id: id, Nombre: nombre, RUT: rut };
        return axios.put('Empresa', body);
    },

    delete(id) {
        return axios.delete('Empresa/'+id);
    }
}