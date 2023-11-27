import axios from "./axios";

export default{

    createAdmin(body){
        return axios.post("Auth/RegisterAdmin", body);
    },

    createCliente(body){
        return axios.post("Auth/RegisterCliente", body);
    },

    Login(body){
        return axios.post("Auth/Login", body);
    },

    editarCategorias(id, body){
        return axios.put("Categoria/" + id, body);
    },

    deleteCategorias(id){
        return axios.delete("Categoria/" + id);
    },

}