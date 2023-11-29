import axios from "./axios";

export default{

    getOrdenes(){
        return axios.get("Producto");
    },

    getOrdenById(id){
        return axios.get("OC/" + id);
    },

    getOrdenByUserId(id){
        return axios.get("OC/User/" + id);
    },

    getOrdenByEmpresaId(id){
        return axios.get("OC/Empresa/" + id);
    },

    createOrden(body){
        return axios.post("Opinion", body);
    },

    editarOrden(body){
        return axios.put("OC", body);
    },

    deleteOrden (id){
        return axios.delete("Producto/" + id);
    },
}