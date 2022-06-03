<template>
    <div style="display: flex">
        <div style="margin: 6rem 1rem 1rem auto; ">
            <ul>
                <li><router-link to="/inv" draggable="false">Dodaj nowe</router-link></li>
                <li><router-link to="/mytasks" draggable="false">Moje zadania</router-link></li>
                <li><router-link to="/verification" draggable="false">Weryfikacja</router-link></li>
                <li><router-link to="/supervisorAcceptance" draggable="false">Akceptacja kierownika</router-link></li>
                <li><router-link to="/managerAcceptance" draggable="false">Akceptacja managera</router-link></li>
                <li><router-link to="/managementAcceptance" draggable="false">Akceptacja zarządu</router-link></li>
                <li><router-link to="/accepted" draggable="false">Zaakceptowane</router-link></li>
                <li><router-link to="/recived" draggable="false">Odebrane</router-link></li>
                <li><router-link to="/rejected" draggable="false">Odrzucone</router-link></li>
                <li><router-link to="/closed" draggable="false">Zamknięte</router-link></li>
            </ul>
        </div>
     <div v-bind:class="{ 'big-data-grid': !isProfileView }">
            <h3 v-if="!isProfileView">Zamknięte</h3>
            <DxDataGrid :data-source="getInvs"
                        :remote-operations="false"
                        :row-alternation-enabled="true"
                        :show-borders="true"
                        :hover-state-enabled="true"
                        @row-click="onRowClick"
                        :column-auto-width="true"
                        width="100%"
                        no-data-text="Brak zadań">
                <DxFilterRow :visible="true" />
                <DxLoadPanel :enabled="true" />
                <DxPaging :page-size="10" />
                <DxColumn data-field="fvNumber" caption="Numer faktury" data-type="string" />
                <DxColumn data-field="clientName" caption="Kontrahent" data-type="string" />
                <DxColumn data-field="netValue" caption="Kwota netto" data-type="number" />
                <DxColumn data-field="grossValue" caption="Kwota brutto" data-type="number" />
                <DxColumn data-field="dateOfIssue" caption="Data wystawienia" data-type="date" />
                <DxColumn data-field="id"
                          caption="Akcje"
                          cell-template="actions-template"
                          :allow-sorting="false"
                          :allow-filtering="false"
                          :allow-search="false"
                          alignment="center" />
                <template #actions-template="{ data }">
                    <div>
                        <a class="action"
                           title="szczegóły"
                           @click="handleDetails({ data })">
                            <i class="fas fa-list ml-2"></i>
                        </a>
                    </div>
                </template>
            </DxDataGrid>
        </div>
    </div>
</template>

<script>
import {
  DxDataGrid,
  DxLoadPanel,
  DxColumn,
  DxFilterRow,
  DxPaging,
} from "devextreme-vue/data-grid";

import { mapGetters, mapActions, mapMutations } from "vuex";

export default {
  name: "MyTasks",
  data() {
    return {
      positions: [],
    };
  },
  props: {
    
  },
  computed: {
    ...mapGetters({
        getInvs: "lists/getInvs",
        getLoggedInUser: "authentication/getLoggedInUser",
    }),
  },
  methods: {
    ...mapActions({
      getClosedList: "lists/getClosedList",
    }),
    ...mapMutations({
        RESET_INVS: "lists/RESET_INVS",
    }),
    onRowClick(e) {
      this.$router.push({ path: `/inv/${e.data.id}` });
      },
    handleDetails(e) {
      this.$router.push({ path: `/inv/${e.data.data.id}` });

    },
  },
  mounted() {
    this.getClosedList();
  },
  components: {
    DxDataGrid,
    DxLoadPanel,
    DxColumn,
    DxFilterRow,
    DxPaging,
  },
  beforeUnmount() {
      this.RESET_INVS();
  },
};
</script>
<style scoped>


a {
  text-decoration: none;
  color: whitesmoke;
  display: inline-block;
  padding: 0.2rem 1.2rem;
  border: 1px solid transparent;
  border-radius: 8px;
}

a:active,
a.router-link-active {
  border-bottom: 2px solid #009385;
  border-radius: 2px;
  color: #009385;
}

a:hover {
  color: #009385;
  text-decoration: none;
}

li {
  margin: 0 0.6rem;
  font-size: 110%;
  list-style-type: none; /* Remove bullets */
}

router-link {
  pointer-events: none;
}

</style>
