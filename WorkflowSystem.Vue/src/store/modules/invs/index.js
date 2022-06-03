import axios from "axios";
import { getField, updateField } from "vuex-map-fields";
import authHeader from "../../../services/auth-header";

export default {
  namespaced: true,
  state() {
    return {
      users: [],
      positions: [],
      inv: {
        Id: "",
        ClientName: "",
        NIP: "",
        IntroductionDate: null,
        FvNumber: "",
        IntroducerId: "",
        MeritId: "",
        DateOfReceipt: null,
        DateOfIssue: null,
        SaleDate: null,
        DateOfPayment: null,
        NetValue: 0,
        PaymentType: 1,
        FVType: 1,
        GrossValue: 0,
        Currency: 1,
        PaymentStatus: 1,
        VAT: 0,
        Step: null,
      },
      isAcceptant: false,
    };
  },
  getters: {
      isAnomaly(state) {
          if (state.positions !== null) {
              return state.positions.some(x => x.overPaid == true);
          }
          return false;
    },
    getUsers(state) {
      return state.users;
    },
    getPositions(state) {
      return state.positions;
    },
    getInvDetails(state) {
      return state.inv;
    },
    getInvId(state) {
        return state.inv.Id;
      },
    getStep(state) {
        return state.inv.Step;
    },
    getIntroducer(state) {
        return state.inv.IntroducerId;
    },
    getMeritId(state) {
        return state.inv.MeritId;
    },
    getIsAcceptant(state) {
         return state.isAcceptant;
    },
    getField,
  },
  mutations: {
    SET_USERS(state, payload) {
      state.users = payload;
    },
    SET_POSITIONS(state, payload) {
      state.positions = payload;
    },
    SET_ISACCEPTANT(state, payload) {
      state.isAcceptant = payload;
    },
    RESET_USERS(state) {
      state.users = [];
    },
    RESET_POSITIONS(state) {
      state.positions = [];
    },
    SET_INV_DETAILS(state, payload) {
      state.inv.Id = payload.id;
      state.inv.ClientName = payload.clientName;
      state.inv.NIP = payload.nip;
      state.inv.IntroductionDate = new Date(payload.introductionDate);
      state.inv.FvNumber = payload.fvNumber;
      state.inv.IntroducerId = payload.introducerId;
      state.inv.MeritId = payload.meritId;
      state.inv.DateOfReceipt = new Date(payload.dateOfReceipt);
      state.inv.DateOfIssue = new Date(payload.dateOfIssue);
      state.inv.SaleDate = new Date(payload.saleDate);
      state.inv.DateOfPayment = new Date(payload.dateOfPayment);
      state.inv.NetValue = payload.netValue;
      state.inv.PaymentType = payload.paymentType;
      state.inv.FVType = payload.fvType;
      state.inv.GrossValue = payload.grossValue;
      state.inv.Currency = payload.currency;
      state.inv.PaymentStatus = payload.paymentStatus;
      state.inv.VAT = payload.vat;
      state.positions = payload.positions;
      state.inv.Step = payload.step;

    },
    RESET_INV_FORM(state) {
      state.inv.Id = 0;
      state.inv.ClientName = "";
      state.inv.NIP = "";
      state.inv.IntroductionDate = null;
      state.inv.FvNumber = "";
      state.inv.IntroducerId = 0;
      state.inv.MeritId = 0;
      state.inv.DateOfReceipt = null;
      state.inv.DateOfIssue = null;
      state.inv.SaleDate = null;
      state.inv.DateOfPayment = null;
      state.inv.NetValue = 0;
      state.inv.PaymentType = 1;
      state.inv.FVType = 1;
      state.inv.GrossValue = 0;
      state.inv.Currency = 1;
      state.inv.PaymentStatus = 1;
      state.inv.VAT = 1;
      state.inv.Step = null;
      state.positions = [];
    },
    updateField,
  },

  actions: {
    setUsers({ commit }) {
      axios
        .get("https://localhost:44379/api/users/getUsersSelect", {
          headers: authHeader(),
        })
        .then((response) => {
          commit("SET_USERS", response.data);
        });
    },
    startInv({ commit }, userId) {
      axios
        .get("https://localhost:44379/api/inv/startInv", {
            headers: authHeader(),
            params: { userId: userId },
        })
        .then((response) => {
            commit("SET_INV_DETAILS", response.data);
        });
    },
    getInvDetails({ commit, dispatch }, data) {
      axios
        .get("https://localhost:44379/api/inv/getInvDetails", {
            headers: authHeader(),
            params: { id: data.id },
        })
        .then((response) => {
            commit("SET_INV_DETAILS", response.data);
            if (response.data.step == 5) {
                dispatch("isAcceptantAction", { id: data.userId, intId: 3 });
            }
            else if (response.data.step == 4) {
                dispatch("isAcceptantAction", { id: data.userId, intId: 2 });
            }
            else if (response.data.step == 3) {
                dispatch("isAcceptantAction", { id: data.userId, intId: 1 });
            }
        });
      },
    getInvPositions({ commit }, id) {
      axios
        .get("https://localhost:44379/api/inv/getInvPositions", {
            headers: authHeader(),
            params: { id: id },
        })
        .then((response) => {
            commit("SET_POSITIONS", response.data);
        });
      },
      isAcceptantAction({ commit }, id) {
          let data = JSON.stringify({
              id: id.id, intId: id.intId
          });

      axios
          .post("https://localhost:44379/api/inv/isUserInRole",  data, {
              headers: {
                  'Content-Type': 'application/json',
              },
          })
          .then((response) => {
              commit("SET_ISACCEPTANT", response.data);
          });
    },
    deleteInvPositions({ dispatch }, data) {
      axios
          .post("https://localhost:44379/api/inv/deleteInvPosition", {id : data.id}
          )
          .then(() => {
              dispatch("getInvPositions", data.invId);
          });
    },
    moveInv: async ({ state }) => {
        await axios.post("https://localhost:44379/api/inv/moveInv", state.inv);
    },
    declineInv: async ({ state }) => {
        await axios.post("https://localhost:44379/api/inv/declineInv", state.inv);
    },
    cancelInv: async ({ state }) => {
        await axios.post("https://localhost:44379/api/inv/cancelInv", state.inv);
    },
  },
};
