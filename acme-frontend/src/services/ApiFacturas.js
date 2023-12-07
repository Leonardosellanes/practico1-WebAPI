import axios from "axios";

const url = "https://localhost:7030/reporte";

export default {
    getFactura(factura) {
        const body = {
            fechaInicio: factura.fechaInicio,
            fechaFin: factura.fechaFin,
            totalComisiones: factura.totalComisiones.toString()
        };
        console.log(body);
        return axios.post(url, body);
      },
}
