import axios from "./axios";

export default {

    buscarCarritoActual() {
        return axios.get("OC/carrito/1");
    },

    agregarProducto(idOrden, idProducto, cantidad) {
        const body = {
            "cantidad": cantidad,
            "productoId": idProducto,
            "pOs": {},
            "ocId": idOrden,
            "oCs": {
                "ClienteId": "1",
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