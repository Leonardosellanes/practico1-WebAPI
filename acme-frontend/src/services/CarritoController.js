import axios from "./axios";

export default {

    buscarCarritoActual() {
        const id = sessionStorage.getItem('idUsuario');
        return axios.get("OC/carrito/"+id);
    },

    agregarProducto(idOrden, idProducto, cantidad) {
        const id = sessionStorage.getItem('idUsuario');
        const body = {
            "cantidad": cantidad,
            "productoId": idProducto,
            "pOs": {},
            "ocId": idOrden,
            "oCs": {
                "ClienteId": id,
                "cli": {}
            }
        };
        console.log(body);
        return axios.post("CarritoProducto", body)
    },

    deleteProductoCarrito(id){
        return axios.delete("CarritoProducto/" + id);
    },

    editProductoCarrito(body){
        return axios.put("CarritoProducto", body);
    }

}