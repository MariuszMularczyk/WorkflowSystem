<template>
  <div class="big-data-grid-center">
    <div v-if="getPlayerDetails.Id" class="text-center mb-3">
      <h3>
        Witaj, {{ getPlayerDetails.FirstName }} {{ getPlayerDetails.LastName }}!
      </h3>
    </div>
    <div class="primary-border">
      <h4 class="line">Co chcesz zrobić?</h4>
      <div class="row mb-4 text-center">
        <div class="col-3 col-md-4 mb-3">
          <DxButton
            text="Edytuj profil"
            type="default"
            @click="routerPushToEditProfile"
            width="300px"
            height="150px"
          />
        </div>
        <div class="col-3 col-md-4 mb-3">
          <DxButton
            text="Dashboard"
            type="default"
            @click="routerPushToDashboard"
            width="300px"
            height="150px"
          />
        </div>
        <div class="col-3 col-md-4 mb-3">
          <DxButton
            text="Obieg faktur"
            type="default"
            @click="routerPushToInvs"
            width="300px"
            height="150px"
          />
        </div>
      </div>
    </div>
  </div>
</template>
<script>
import { mapGetters, mapActions, mapMutations } from "vuex";
import { userTypeEnum } from "../../enums/userTypeEnum";
import DxButton from "devextreme-vue/button";

export default {
  name: "UserProfile",
  data() {
    return {
      userTypeEnum,
    };
  },
  computed: {
    ...mapGetters({
      getLoggedInUser: "authentication/getLoggedInUser",
      getPlayerDetails: "users/getPlayerDetails",
    }),
  },
  methods: {
    ...mapActions({
      getPlayerDetailsByUserId: "users/getPlayerDetailsByUserId",
      getUserDataByUserId: "users/getUserDataByUserId",
    }),
    ...mapMutations({
      RESET_PLAYER_FORM: "users/RESET_PLAYER_FORM",
    }),
    routerPushToEditProfile() {
      this.$router.push({
        name: "editProfile",
        params: { playerToEditId: this.getPlayerDetails.Id },
      });
    },
    routerPushToInvs() {
      this.$router.push({ path: `/mytasks` });
    },
    routerPushToDashboard() {
      this.$router.push({ path: `/dashboard` });
    },
  },
  mounted() {
      console.log(this.getLoggedInUser);
    if (this.getLoggedInUser) {
      switch (this.getLoggedInUser.userType) {
        case userTypeEnum.USER:
          this.getPlayerDetailsByUserId(this.getLoggedInUser.id);
          break;
        default:
          this.getUserDataByUserId(this.getLoggedInUser.id);
          break;
      }
    }
  },
  components: {
    DxButton,
  },
  beforeUnmount() {
    this.RESET_PLAYER_FORM();
  },
};
</script>
