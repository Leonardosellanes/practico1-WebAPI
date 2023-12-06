import axios from "axios";

const url = "http://localhost:5243/reporte";

export default {
    getFactura(factura) {
        const body = {
            fechaInicio: factura.fechaInicio,
            fechaFin: factura.fechaFin,
            comisionesTotales: factura.comisionesTotales
        };
        console.log(body);
        return axios.post(url, body);
      },
}
