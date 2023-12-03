import axios from "./axios";

export default{

    createEmpresa(body){
        return axios.post("/Empresa", body);
    },

    createCliente(body){
        return axios.post("Auth/RegisterCliente", body);
    },

    Login(body){
        return axios.post("Auth/Login", body);
    },

    getInfoUser(id) {
        return axios.get("Personas/" + id)
    },

    editUser(id, body) {
        return axios.put("Personas/" + id, body)
    },

    editarCategorias(id, body){
        return axios.put("Categoria/" + id, body);
    },

    deleteCategorias(id){
        return axios.delete("Categoria/" + id);
    },

}