import axios from "./axios";

export default{

    getCategorias(id){
        return axios.get("Categoria/Empresa/" + id);
    },

    getById(id){
        return axios.get("Categoria/" + id);
    },

    createCategorias(body){
        return axios.post("Categoria", body);
    },

    editarCategorias(id, body){
        return axios.put("Categoria/" + id, body);
    },

    deleteCategorias(id){
        return axios.delete("Categoria/" + id);
    },

}