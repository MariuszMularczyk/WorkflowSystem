import axios from "axios";
import { getField, updateField } from "vuex-map-fields";
import authHeader from "../../../services/auth-header";

export default {
  namespaced: true,
  state() {
    return {
      invsCount: 0,
      invs: [],
    };
  },
  getters: {
    getInvs(state) {
      return state.invs;
    },
    getField,
  },
  mutations: {
    SET_INVS(state, payload) {
      state.invs = payload;
    },
    RESET_INVS(state) {
      state.invs = [];
    },
    updateField,
  },

  actions: {
    getMyTasksList({ commit }, userId) {
      axios
        .get("https://localhost:44379/api/inv/getMyTasksList", {
          params: { id: userId },
          headers: authHeader(),
        })
        .then((response) => {
            commit("SET_INVS", response.data);
        });
      },
    getVerificationList({ commit }) {
      axios
        .get("https://localhost:44379/api/inv/getVerificationList", {
          headers: authHeader(),
        })
        .then((response) => {
            commit("SET_INVS", response.data);
        });
    },
    getSupervisorAcceptanceList({ commit }) {
      axios
        .get("https://localhost:44379/api/inv/getSupervisorAcceptanceList", {
          headers: authHeader(),
        })
        .then((response) => {
            commit("SET_INVS", response.data);
        });
    },
    getManagerAcceptanceList({ commit }) {
      axios
        .get("https://localhost:44379/api/inv/getManagerAcceptanceList", {
          headers: authHeader(),
        })
        .then((response) => {
            commit("SET_INVS", response.data);
        });
    },
    getManagementAcceptanceList({ commit }) {
      axios
        .get("https://localhost:44379/api/inv/getManagementAcceptanceList", {
          headers: authHeader(),
        })
        .then((response) => {
            commit("SET_INVS", response.data);
        });
    },
    getAcceptedList({ commit }) {
      axios
        .get("https://localhost:44379/api/inv/getAcceptedList", {
          headers: authHeader(),
        })
        .then((response) => {
            commit("SET_INVS", response.data);
        });
    },
    getRecivedList({ commit }) {
      axios
        .get("https://localhost:44379/api/inv/getRecivedList", {
          headers: authHeader(),
        })
        .then((response) => {
            commit("SET_INVS", response.data);
      });
    },
    getRejectedList({ commit }) {
      axios
        .get("https://localhost:44379/api/inv/getRejectedList", {
          headers: authHeader(),
        })
        .then((response) => {
            commit("SET_INVS", response.data);
      });
    },
    getClosedList({ commit }) {
      axios
        .get("https://localhost:44379/api/inv/getClosedList", {
          headers: authHeader(),
        })
        .then((response) => {
            commit("SET_INVS", response.data);
        });
    },
  },
};
