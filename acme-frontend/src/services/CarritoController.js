import axios from "./axios";

export default {

    buscarCarritoActual() {
        const idUsuario = sessionStorage.getItem('idUsuario');
        return axios.get("OC/carrito/"+idUsuario);
    },

    agregarProducto(idOrden, idProducto, cantidad) {
        const body = {
            cantidad: cantidad,
            productoId: idProducto,
            pOs: {},
            ocId: idOrden
        };
        return axios.post("CarritoProducto", body)
    },

    deleteProductoCarrito(id){
        return axios.delete("CarritoProducto/" + id);
    },

    editProductoCarrito(body){
        return axios.put("CarritoProducto", body);
    },

    actualizarOrden(orden){
        const body = {
            id: orden.id,
            medioDePago:orden.medioDePago,
            direccionDeEnvio: orden.direccionDeEnvio,
            fechaEstimadaEntrega: orden.fechaEstimadaEntrega,
            total: orden.total,
            estadoOrden: orden.estadoOrden,
            fecha: orden.fecha,
            ClienteId: sessionStorage.getItem('idUsuario'),
            sucursalId: orden.sucursalId
        }
        return axios.put("OC", body);
    }

}