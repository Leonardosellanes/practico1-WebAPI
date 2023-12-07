import axios from "axios";

const url = "http://localhost:5205/Package/calculate";

export default {
    calcularEnvio(dir){
        const body ={
            package: {
                warehouseId: 1,
                clienteId: sessionStorage.getItem('idUsuario')
            },
            userLocation:{
                direccion: dir
            }
        }
        return axios.post(url, body);
    }
}
