import axios from "axios";
import { getField, updateField } from "vuex-map-fields";
import authHeader from "../../../services/auth-header";

export default {
  namespaced: true,
  state() {
    return {
      roles: [],
      userRoles: [],
      users: [],
      userId: null
    };
  },

  getters: {
    getRoles(state) {
      return state.roles;
    },
    getUsers(state) {
      return state.users;
    },
    getUserRoles(state) {
      return state.userRoles;
    },
    getField,
  },
  mutations: {
    SET_ROLES(state, payload) {
      state.roles = payload;
    },
    SET_USERS(state, payload) {
      state.users = payload;
    },
    RESET_USERS(state) {
      state.users = [];
    },
    RESET_ROLES(state) {
      state.roles = [];
    },
    SET_USERS_IN_ROLE(state, payload) {
      state.userRoles = payload;
    },
    RESET_USERS_IN_ROLE(state) {
      state.userRoles = [];
    },
    updateField,
  },
  actions: {
    setRoles({ commit }) {
      axios.get("https://localhost:44379/api/roles/getRoles").then((response) => {
        commit("SET_ROLES", response.data);
      });
    },
    setUsers({ commit }, roleId) {
      axios
        .get("https://localhost:44379/api/users/getUsersNotFromGroupSelect", {
          headers: authHeader(),
          params: { roleId: roleId },
        })
        .then((response) => {
          commit("SET_USERS", response.data);
        });
    },
    setUserRoles: async ({ commit }, roleId) => {
        console.log("store " + roleId)
      await axios
        .get("https://localhost:44379/api/players/getUsersByRole", {
          params: { roleId: roleId },
        })
        .then(function(response) {
          commit("SET_USERS_IN_ROLE", response.data);
        });
    },
      userDelete: async ({ dispatch }, { id, roleId }) => {
      await axios
        .post("https://localhost:44379/api/roles/deleteRole", {
           Id: id ,
        })
        .then(function() {
          dispatch('setUserRoles', roleId);
        });
    },
      addUserToRole: async ({ dispatch }, { roleId, userId }) => {
      await axios
          .post("https://localhost:44379/api/roles/addUserToRole", {
              Id: userId,
              IntId: roleId,
        })
        .then(function() {
          dispatch('setUserRoles',roleId );
        });
    },
  },
};
