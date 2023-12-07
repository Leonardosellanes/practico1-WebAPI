import axios from "./axios";

export default{

    createCliente(body){
        return axios.post("Auth/RegisterCliente", body);
    },
  
    createManager(body){
        return axios.post('Auth/RegisterAdmin', body)
    },

    Login(body){
        return axios.post("Auth/Login", body);
    },

    getInfoUser(id) {
        return axios.get("Personas/" + id)
    },

    editUser(id, body) {
        return axios.put("Personas/" + id, body)
    },

    changePass(body) {
        return axios.post("Auth/ChangePassword", body);
    }
}