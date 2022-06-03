import axios from "axios";
import { getField, updateField } from "vuex-map-fields";

export default {
  namespaced: true,
  state() {
    return {
      position: {
        Id: null,
        descryption: "",
        quantity: 0,
        jm: 1,
        netValueUnit: 0,
        netValue: 0,
        grossValue: 0,
        vat: 1,
        invId: null,
        overPaid: false
      },
    };
  },

  getters: {
    getPositionDetails(state) {
      return state.position;
    },
    getField,
  },
  mutations: {
      SET_POSITION_DETAILS(state, payload) {
      state.position.Id = payload.id;
      state.position.descryption = payload.descryption;
      state.position.quantity = payload.quantity;
      state.position.jm = payload.jm;
      state.position.netValueUnit = payload.netValueUnit;
      state.position.netValue = payload.netValue;
      state.position.grossValue = payload.grossValue;
      state.position.vat = payload.vat;
      state.position.invId = payload.invId;
      state.position.overPaid = payload.overPaid;
    }, 
    RESET_POSITION_DETAILS(state) {
        state.position.Id = null;
        state.position.descryption = "";
        state.position.quantity = 0;
        state.position.jm = 1;
        state.position.netValueUnit = 0;
        state.position.netValue = 0;
        state.position.grossValue = 0;
        state.position.vat = 1;
        state.position.invId = null;
        state.position.overPaid = false;
    },
    SET_INV_ID(state, payload){
        state.position.invId = payload;
    },
    updateField,
  },

  actions: {
      setPositionDetails({ commit }, positionId) {
          console.log(positionId);
      axios
        .get(
          "https://localhost:44379/api/inv/getInvPositionDetails",
          {
              params: { id: positionId },
          }
        )
        .then((response) => {
          commit("SET_POSITION_DETAILS", response.data);
      });
    },
      addPosition: async ({  state }) => {
      await axios.post(
        "https://localhost:44379/api/inv/addInvPosition",
        state.position
      ).then(function () {
          
      });
    },
      updatePosition: async ({  state }) => {
      await axios.post(
        "https://localhost:44379/api/inv/updateInvPosition",
        state.position
      ).then(function () {
          
      });
    },
  },
};
