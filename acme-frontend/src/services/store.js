import { createStore } from 'vuex';
import axios from 'axios';

export default createStore({
  state: {
    token: null,
    idUsuario:null,
    email: '',
    rol:'',
    empresaId: null
  },
  mutations: {
    setToken(state, data) {
      state.token = data.token;
      state.idUsuario = data.idUsuario;
      state.email = data.email;
      state.rol = data.roles[0];
      state.empresaId = data.empresaId
      sessionStorage.setItem('token', data.token);
      sessionStorage.setItem('idUsuario', data.idUsuario);
      sessionStorage.setItem('email', data.email);
      sessionStorage.setItem('rol', data.roles[0]);
      sessionStorage.setItem('empresaId', data.empresaId);
      sessionStorage.setItem('direccion', data.direccion);
    },
    clearToken(state) {
      state.token = null;
      state.user = null;
      state.idUsuario = null
      state.email = '';
      state.rol = ''
      state.empresaId = null
      sessionStorage.removeItem('idUsuario');
      sessionStorage.removeItem('email');
      sessionStorage.removeItem('rol');
      sessionStorage.removeItem('empresaId');
      sessionStorage.removeItem('token');
      sessionStorage.removeItem('direccion');
    },
  },
  actions: {
    async loginUser({ commit }, { username, password }) {
      try {
        const response = await axios.post('/api/Auth/Login', { username, password });
        const token = response.data.token;
        commit('setToken', token);
      } catch (error) {
        console.error(error.response.data);
      }
    },//uso store.dispatch('loginUser', { username: 'user', password: 'pass' });
    async logoutUser({ commit }) {
      try {
        // Puedes hacer una llamada a la API para cerrar la sesión si es necesario
        // Aquí, simplemente limpiamos el token
        commit('clearToken');
      } catch (error) {
        console.error('Error de inicio de sesión:', error);
        throw error;}
    },
  },
  getters: {
    isAuthenticated: state => !!state.token,
    userRole: state => state.rol,
  }, // uso: const isAuthenticated = store.getters.isAuthenticated;
});
