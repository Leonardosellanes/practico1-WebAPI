import axios from "./axios";

export default{

    getOrdenes(){
        return axios.get("Producto");
    },

    getOrdenById(id){
        return axios.get("OC/" + id);
    },

    createOrden(body){
        return axios.post("Opinion", body);
    },

    editarOrden(id, body){
        return axios.put("Producto/" + id, body);
    },

    deleteOrden (id){
        return axios.delete("Producto/" + id);
    },
}