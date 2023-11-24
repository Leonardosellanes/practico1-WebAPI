import axios from "./axios";

export default{

    getProductos(){
        return axios.get("Producto");
    },

    getProductoById(id){
        return axios.get("Producto/" + id);
    },

    createOpinion(body){
        return axios.post("Opinion", body);
    },

    editarProducto(id, body){
        return axios.put("Producto/" + id, body);
    },

    deleteProducto(id){
        return axios.delete("Producto/" + id);
    },
}