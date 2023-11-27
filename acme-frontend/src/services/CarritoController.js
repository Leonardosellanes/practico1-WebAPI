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
                "cli": {}
            }
        };
        console.log(body);
        return axios.post("CarritoProducto", body)
    }



}