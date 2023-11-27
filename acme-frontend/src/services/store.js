import { createStore } from 'vuex';
import axios from 'axios';

export default createStore({
  state: {
    token: null,
  },
  mutations: {
    setToken(state, token) {
      state.token = token;
    },
    clearToken(state) {
      state.token = null;
      state.user = null;
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
  }, // uso: const isAuthenticated = store.getters.isAuthenticated;
});

/*<script setup>
import { useStore } from 'vuex';

const store = useStore();
const token = store.state.token;
</script>LLamado*/
