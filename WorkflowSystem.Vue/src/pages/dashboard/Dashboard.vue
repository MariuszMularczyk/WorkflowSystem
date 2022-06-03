<template>
    <div class="big-data-grid-center">
        <h3>Dashboard</h3>
        <div class="primary-border">
            <div class="row">
                <div class="col">
                    <h3>Faktury zarejestrowane w ostatnich dwunastu miesiącach</h3>
                    <DxChart id="chart"
                             :data-source="dataSource">
                        <DxSeries argument-field="day"
                                  value-field="oranges"
                                  type="bar"
                                  color="#ffaa66" />
                        <DxLegend :visible="false" />
                    </DxChart>
                </div>
                <div class="col">
                    <h3>Faktury na krokach</h3>
                    <DxPieChart id="pie"
                                :data-source="dataSource2"
                                palette="Bright"
                                @point-click="pointClickHandler($event)"
                                @legend-click="legendClickHandler($event)">
                        <DxSeries argument-field="day"
                                  value-field="oranges" />
                    </DxPieChart>
                </div>
            </div>
            <h3 class="mt-5">Top 3 opóznienia na krokach</h3>
            <div class="row">
                <div class="col">
                    <h3>Weryfikacja</h3>
                    <DxDataGrid :data-source="dataSource3"
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
                <div class="col">
                    <h3>Akceptacja kierownika</h3>
                    <DxDataGrid :data-source="dataSource3"
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
            <div class="row mt-4">
                <div class="col">
                    <h3>Akceprtacja managera</h3>
                    <DxDataGrid :data-source="dataSource3"
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
                <div class="col">
                    <h3>Akceptacja zarządu</h3>
                    <DxDataGrid :data-source="dataSource3"
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
            <div class="row mt-4">
                <div class="col">
                    <h3>Akceprtacja managera</h3>
                    <DxDataGrid :data-source="dataSource3"
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
                <div class="col">
                    <h3>Akceptacja zarządu</h3>
                    <DxDataGrid :data-source="dataSource3"
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
        </div>
        

    </div>
</template>
<script>


import { mapGetters, mapActions } from "vuex";
import { userTypeEnum } from "../../enums/userTypeEnum";
import { DxChart, DxSeries, DxLegend } from 'devextreme-vue/chart';
    import DxPieChart from 'devextreme-vue/pie-chart';
    import {
        DxDataGrid,
        DxLoadPanel,
        DxColumn,
        DxFilterRow,
        DxPaging,
    } from "devextreme-vue/data-grid";
export default {
  name: "JobAdvertisements",
  data() {
    return {
      userTypeEnum,
        dataSource:[{
            day: 'Maj',
            oranges: 10,
        }, {
            day: 'Czerwiec',
            oranges: 16,
        }, {
            day: 'Lipiec',
            oranges: 18,
        }, {
            day: 'Sierpień',
            oranges: 13,
        }, {
            day: 'Wrzesień',
            oranges: 11,
        }, {
            day: 'Pazdziernik',
            oranges: 9,
        }, {
            day: 'Listopad',
            oranges: 15,
        }, {
            day: 'Grudzień',
            oranges: 12,
        }, {
            day: 'Styczeń',
            oranges: 20,
        }, {
            day: 'Luty',
            oranges: 16,
        }, {
            day: 'Marzec',
            oranges: 12,
        }, {
            day: 'Kwiecień',
            oranges: 4,
        }],
        dataSource2: [{
            day: 'Rejestracja',
            oranges: 3,
        }, {
            day: 'Weryfikacja',
            oranges: 5,
        }, {
            day: 'Akceptacja kierownika',
            oranges: 8,
        }, {
            day: 'Akceptacja managera',
            oranges: 6,
        }, {
            day: 'Akceptacja zarządu',
            oranges: 5,
        }, {
            day: 'Zaakceptowany',
            oranges: 19,
        }, {
            day: 'Odebrane',
            oranges: 29,
        }, {
            day: 'Odrzucone',
            oranges: 12,
        }, {
            day: 'Zamknięte',
            oranges: 20,
            }],
        dataSource3: [{ "id": 3, "fvNumber": "test/0", "clientName": "testowy", "netValue": 0.00, "grossValue": 0.00, "dateOfIssue": null }, { "id": 4, "fvNumber": "test/1", "clientName": "test", "netValue": 0.00, "grossValue": 0.00, "dateOfIssue": "1970-01-01T00:00:00" },{ "id": 3, "fvNumber": "test/2", "clientName": "test", "netValue": 0.00, "grossValue": 0.00, "dateOfIssue": "1970-01-01T00:00:00" }],
    };
  },
  computed: {
    ...mapGetters({
      getLoggedInUser: "authentication/getLoggedInUser",
    }),
  },
  methods: {
    ...mapActions({
      getUserTeamId: "authentication/getUserTeamId",
    }),
  },
  mounted() {

  },
  components: {
      DxChart,
      DxSeries,
      DxLegend,
      DxPieChart,
      DxDataGrid,
      DxLoadPanel,
      DxColumn,
      DxFilterRow,
      DxPaging,
  },
};
</script>
