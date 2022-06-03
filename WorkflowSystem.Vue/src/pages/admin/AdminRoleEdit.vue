<template>
  <div>
      <DxPopup v-model:visible="popupVisible"
               :drag-enabled="false"
               :close-on-outside-click="false"
               :show-close-button="false"
               :show-title="true"
               :width="600"
               height="auto"

               container=".dx-viewport"
               title="Edytuj grupę"
               :shading="false">
          <form class="mb-4">
              <div class="primary-border">
                  <div class="row">
                      <div class="col">
                          <label for="peoples2SelectBox" class="form-label">Użytkownik</label>
                          <DxSelectBox :dataSource="getUsers"
                                       value-expr="value"
                                       display-expr="text"
                                       v-model="userId">
                          </DxSelectBox>
                      </div>
                  </div>
              </div>
              <div>
                  <div class="row mt-4">
                      <div class="col">
                      </div>
                      <div class="col text-right">
                          <DxButton text="Dodaj"
                                    type="default"
                                    @click="addUser(this.userId)"
                                    :disabled = "!this.userId"
                                    ref="submitButton" />
                      </div>
                  </div>
              </div>
          </form>
          <DxDataGrid class ="mb-3" 
                      :data-source="getUserRoles"
                      :remote-operations="false"
                      :row-alternation-enabled="true"
                      :show-borders="true"
                      :hover-state-enabled="true"
                      :column-auto-width="true"
                      width="100%"
                      no-data-text="Brak użytkowników">
              <DxFilterRow :visible="true" />
              <DxLoadPanel :enabled="true" />
              <DxPaging :page-size="5" />
              <DxColumn data-field="name"
                        caption="Nazwa użytkownika"
                        data-type="string" />
              <DxColumn data-field="id"
                        caption="Akcje"
                        cell-template="actions-template"
                        :allow-sorting="false"
                        :allow-filtering="false"
                        :allow-search="false"
                        alignment="center" />
              <template #actions-template="{ data }">
                  <div>
                      <DxButton icon="remove"
                                type="normal"
                                styling-mode="text"
                                @click="onClick({ data })" />
                  </div>
              </template>
          </DxDataGrid>

          <DxToolbarItem 
                         widget="dxButton"
                         toolbar="bottom"
                         location="before"
                         :options="closeButtonOptions" />

      </DxPopup>
  </div>
</template>
<script>

import { DxPopup, DxToolbarItem } from "devextreme-vue/popup";
import DxSelectBox from "devextreme-vue/select-box";
import {
    DxDataGrid,
    DxLoadPanel,
    DxColumn,
    DxFilterRow,
    DxPaging,
} from "devextreme-vue/data-grid";
    import DxButton from 'devextreme-vue/button';
import { mapGetters, mapActions, mapMutations } from "vuex";
import { createHelpers } from "vuex-map-fields";
const { mapFields } = createHelpers({
  getterType: "roles/getField",
  mutationType: "roles/updateField",
});

import { useToast } from "vue-toastification";
import { custom } from "devextreme/ui/dialog";

export default {
  name: "AdminTeamEdit",
  props: {
    roleId: {
      type: String,
      required: false,
    },
  },
  data() {
    return {
      popupVisible: false,
      leagues: [],
      closeButtonOptions: {
        text: "Zamknij",
        onClick: () => {
          this.popupVisible = false;
          this.$emit("closed");
        },
        type: "normal",
      },
      groupRefKey: "targetGroup",
    };
  },
  computed: {
    ...mapFields(["userId"]),
    ...mapGetters({
      getUserRoles: "roles/getUserRoles",
      getUsers: "roles/getUsers",
    }),
  },
  methods: {
    ...mapActions({
      setUserRoles: "roles/setUserRoles",
      userDelete: "roles/userDelete",
      setUsers: "roles/setUsers",
      addUserToRole: "roles/addUserToRole",
    }),
    ...mapMutations({
        RESET_USERS_IN_ROLE: "roles/RESET_USERS_IN_ROLE",
    }),
    onClick(data) {
      let dialog = custom({
        title: "Potwierdzenie",
        messageHtml: `Czy na pewno chcesz usunąć użytkownika ${data.data.data.name}?`,
        buttons: [
          {
            text: "Tak",
            onClick: () => {
              return true;
            },
          },
          {
            text: "Nie",
            onClick: () => {
              return false;
            },
          },
        ],
      });
      dialog.show().then((dialogResult) => {

        if (dialogResult === true) {
          this.userDelete({ roleId: this.roleId, id: data.data.data.id })
          useToast().success("Użytkownik został usunięty!");
        }
      });
      },
      addUser(data) {
          this.addUserToRole({ roleId: this.roleId, userId: data });
          this.userId = null;
          useToast().success("Użytkownik został dodany!");

      },
  },
  mounted() {
    this.setUserRoles(this.roleId);
    this.setUsers(this.roleId);
    this.popupVisible = true;
  },
  components: {
    DxPopup,
    DxToolbarItem,
    DxDataGrid,
    DxLoadPanel,
    DxColumn,
    DxFilterRow,
    DxPaging,
    DxButton,
    DxSelectBox
  },
  beforeUnmount() {
    this.RESET_USERS_IN_ROLE();
      this.userId = null;
  },
};
</script>
