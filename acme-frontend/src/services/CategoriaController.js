import axios from "./axios";

export default{

    getCategorias(){
        return axios.get("Categoria");
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