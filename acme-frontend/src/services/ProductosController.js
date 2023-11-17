import axios from "./axios";

export default{

    getCategorias(){
        return axios.get("Categoria");
    },

    createProducto(body){
        return axios.post("Producto", body);
    },

    editarCategorias(id, body){
        return axios.put("Categoria/" + id, body);
    },

    deleteCategorias(id){
        return axios.delete("Categoria/" + id);
    },

}