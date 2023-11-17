import axios from "./axios";

export default{

    cargarImagen(data){
        return axios.post("Archivos/subir-imagen", data);
    },

    cargarPdf(data){
        return axios.post("Archivos/subir-pdf", data);
    },


}