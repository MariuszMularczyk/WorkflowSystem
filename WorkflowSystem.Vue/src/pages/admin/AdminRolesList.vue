<template>
  <div class="big-data-grid-center">
    <h3>Zarządzanie grupami (Administrator)</h3>
    <div class="row">
    </div>
    <div class="row mt-4">
      <div class="col">
        <DxDataGrid
          :data-source="getRoles"
          :remote-operations="false"
          :row-alternation-enabled="true"
          :show-borders="true"
          :hover-state-enabled="true"
          @row-click="showEditPopup"
          :column-auto-width="true"
          width="100%"
        >
          <DxFilterRow :visible="true" />
          <DxLoadPanel :enabled="true" />
          <DxSorting mode="none" />
          <DxColumn data-field="name" caption="Nazwa grupy" width="30%"/>
          <DxColumn data-field="description" caption="Opis" />
        </DxDataGrid>
      </div>
    </div>
  </div>
  <AdminRoleEdit
    :showToEdit="true"
    :roleId="editPopupOptions.roleId"
    v-if="editPopupOptions.isVisible"
    @closed="onEditPopupClose"
  />
</template>

<script>
import {
  DxDataGrid,
  DxLoadPanel,
  DxSorting,
  DxColumn,
  DxFilterRow,
} from "devextreme-vue/data-grid";
import { mapGetters, mapActions, mapMutations } from "vuex";
import AdminRoleEdit from "./AdminRoleEdit";

export default {
  name: "AdminRolesList",
  data() {
    return {
      editPopupOptions: {
        isVisible: false,
        roleId: "",
      },
    };
  },
  computed: {
    ...mapGetters({
      getRoles: "roles/getRoles",
    }),
  },
  methods: {
    ...mapActions({
      setRoles: "roles/setRoles",
    }),
    ...mapMutations({
      RESET_ROLES: "roles/RESET_ROLES",
    }),

    showEditPopup(e) {
      this.editPopupOptions.isVisible = true;
      this.editPopupOptions.roleId = e.data.id;
    },

    onEditPopupClose() {
      this.editPopupOptions.isVisible = false;
      this.editPopupOptions.teamId = "";
    },
  },
  mounted() {
      this.RESET_ROLES();
      this.setRoles();
  },
  components: {
    DxDataGrid,
    DxLoadPanel,
    DxSorting,
    DxColumn,
    DxFilterRow,
    AdminRoleEdit,
  },
  beforeUnmount() {
    this.RESET_ROLES();
  },
};
</script>
