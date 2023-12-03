import axios from "./axios";

//const url =  "http://localhost:32769/api/Empresa";

export default {

    get() {
        return axios.get("Empresa");
    },

    getById(id) {
        return axios.get('Empresa/'+1);
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
    },
    
    getEmpleados(id) {
        return axios.get("AppUsers/" + id);
    },

    createEmpleados(body) {
        return axios.post('Auth/RegisterEmpleado' ,body)
    },

    updateEmpleado(id, email, password, nombre, apellido) {
        let body = { 
            email: email,
            password: password,
            name: nombre,
            lName: apellido,
            empresaId: sessionStorage.getItem('empresaId')

        };
        return axios.put('Personas/'+id, body);
    },

    deleteEmpleado(id){
        return axios.delete('Personas/'+id);
    }

}