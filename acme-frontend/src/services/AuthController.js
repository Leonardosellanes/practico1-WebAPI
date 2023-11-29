import axios from "./axios";

export default{

    createEmpresa(body){
        return axios.post("/Empresa/RegistrarEmpresa", body);
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