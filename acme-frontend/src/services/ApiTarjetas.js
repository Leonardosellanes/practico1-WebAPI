import axios from "axios";

const url = "http://localhost:5001/api/procesarpago";

export default {
    validarTarjeta(num) {
        const card = {
            numero: num
        };
    
        return axios.post(url, card);
      },
}
