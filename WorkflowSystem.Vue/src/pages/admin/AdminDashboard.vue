<template>
  <div class="big-data-grid-center">
    <div v-if="getLoggedInUser" class="text-center mb-3">
      <h3>
        Panel administratora
      </h3>
      <h4 class="red-color">
        Korzystanie z panelu zabronione dla osób nieupoważnionych!
      </h4>
    </div>
    <div class="primary-border">
      <h4 class="line">Funkcje administracyjne</h4>
      <div class="row mb-4 text-center">
        <div class="col-4 col-md-4 mb-3">
          <DxButton
            text="Zarządzanie użytkownikami"
            type="default"
            @click="routerPushToUsersAdmin"
            width="500px"
            height="150px"
          />
        </div>
        <div class="col-4 col-md-4 mb-3">
          <DxButton
            text="Zarządzanie grupami"
            type="default"
            @click="routerPushToRolesAdmin"
            width="500px"
            height="150px"
          />
        </div>
        <div class="col-4 col-md-4 mb-3">
          <DxButton
            text="Grupuj"
            type="default"
            @click="cluster"
            width="500px"
            height="150px"
          />
        </div>
      </div>
    </div>
    <DxLoadPanel
      v-model:visible="loadingVisible"
      show-indicator="true"
      show-pane="true"
      shading="true"
      close-on-outside-click="false"
      shading-color="rgba(0,0,0,0.4)"
    />
  </div>
</template>
<script>
import { mapGetters, mapActions, mapMutations } from "vuex";
import { userTypeEnum } from "../../enums/userTypeEnum";
import DxButton from "devextreme-vue/button";
import { DxLoadPanel } from "devextreme-vue/load-panel";
import axios from "axios";
export default {
  name: "AdminDashboard",
  data() {
    return {
      userTypeEnum,
      loadingVisible: false,
    };
  },
  computed: {
    ...mapGetters({
      getLoggedInUser: "authentication/getLoggedInUser",
    }),
  },
  methods: {
    ...mapActions({
      getUserDataByUserId: "users/getUserDataByUserId",
    }),
    ...mapMutations({}),
    routerPushToUsersAdmin() {
      this.$router.push({ path: `/admin-users` });
    },
    routerPushToRolesAdmin() {
      this.$router.push({ path: `/admin-roles` });
    },
    async cluster() {
      this.loadingVisible = true;
      await axios.get("https://localhost:44379/api/admins/cluster").then(() => {
        this.loadingVisible = false;
      });
    },
  },
  mounted() {},
  components: {
    DxButton,
    DxLoadPanel,
  },
};
</script>
