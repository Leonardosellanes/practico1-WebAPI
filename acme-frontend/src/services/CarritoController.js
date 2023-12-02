import axios from "./axios";

export default {

    buscarCarritoActual() {
        const id = sessionStorage.getItem('idUsuario');
        return axios.get("OC/carrito/"+id);
    },

    agregarProducto(idOrden, idProducto, cantidad) {
        const body = {
            "cantidad": cantidad,
            "productoId": idProducto,
            "pOs": {},
            "ocId": idOrden
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