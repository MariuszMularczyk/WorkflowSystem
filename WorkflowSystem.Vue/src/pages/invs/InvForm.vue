<template>
    <div class="big-data-grid-center">
        <div class="primary-border">
            <div class="secondary-border2"  v-if="isAnomaly" >
                <div class="row">
                    <div class="col">
                        <label class="form-label">Wykryto anomalię cenową, sprawdź ponownie ten zakup</label>
                    </div>

                </div>
            </div>
            <div class="secondary-border">
                <div class="row">
                    <div class="col">
                        <label class="form-label">ID faktury: {{Id}}</label>
                    </div>
                    <div class="col">
                        <label class="form-label">Krok: {{getStepText()}}</label>
                    </div>
                </div>
            </div>
            <form>
                <DxValidationGroup :ref="groupRefKey">
                    <div class="primary-border">
                        <h4 class="line">Dane faktury</h4>
                        <div class="row">
                            <div class="col-6">
                                <label for="clientNameTextBox" class="form-label">Kontrahent</label>
                                <DxTextBox v-model="ClientName" id="clientNameTextBox" :read-only="isReadOnly">
                                    <DxValidator>
                                        <DxRequiredRule message="Kontrahent jest wymagany!" />
                                    </DxValidator>
                                </DxTextBox>
                            </div>
                            <div class="col">
                                <label for="NIPTextBox" class="form-label">NIP</label>
                                <DxTextBox v-model="NIP" id="NIPTextBox" :read-only="isReadOnly">
                                    <DxValidator>
                                        <DxRequiredRule message="NIP jest wymagany!" />
                                    </DxValidator>
                                </DxTextBox>
                            </div>
                            <div class="col">
                                <label for="introductionDateDateBox" class="form-label">Data wprowadzenia</label>
                                <DxDateBox v-model="IntroductionDate"
                                           id="introductionDateDateBox"
                                           type="date"
                                           display-format="dd/MM/yyyy"
                                           cancel-button-text="Anuluj"
                                           :read-only="isReadOnly"
                                           invalid-date-message="Wartość musi być datą lub czasem">
                                    <DxValidator>
                                        <DxRequiredRule message="Data urodzenia jest wymagana!" />
                                    </DxValidator>
                                </DxDateBox>
                            </div>
                        </div>
                        <div class="row mt-4">
                            <div class="col-6">
                                <label for="FvNumberTextBox" class="form-label">Numer faktury</label>
                                <DxTextBox v-model="FvNumber" id="FvNumberTextBox" :read-only="isReadOnly" />
                            </div>
                            <div class="col">
                                <label for="peoplesSelectBox" class="form-label">Osoba wprowadzająca</label>
                                <DxSelectBox :dataSource="users"
                                             value-expr="value"
                                             display-expr="text"
                                             v-model="IntroducerId"
                                             id="peoplesSelectBox"
                                             read-only="true" />
                            </div>
                            <div class="col">
                                <label for="peoples2SelectBox" class="form-label">Osoba merytoryczna</label>
                                <DxSelectBox :dataSource="users"
                                             value-expr="value"
                                             display-expr="text"
                                             v-model="MeritId"
                                             :read-only="getStep != 1"
                                             id="peoples2SelectBox">
                                    <DxValidator>
                                        <DxRequiredRule message="Osoba merytoryczna jest wymagana!" />
                                    </DxValidator>
                                </DxSelectBox>
                            </div>
                        </div>
                        <div class="row mt-4">
                            <div class="col">
                                <label for="dateOfReceiptDateBox" class="form-label">Data wpływu</label>
                                <DxDateBox v-model="DateOfReceipt "
                                           id="dateOfReceiptDateBox"
                                           type="date"
                                           display-format="dd/MM/yyyy"
                                           cancel-button-text="Anuluj"
                                           :read-only="isReadOnly"
                                           invalid-date-message="Wartość musi być datą lub czasem">
                                    <DxValidator>
                                        <DxRequiredRule message="Data wpływu jest wymagana!" />

                                    </DxValidator>
                                </DxDateBox>
                            </div>
                            <div class="col">
                                <label for="dateOfIssueDateBox" class="form-label">Data wystawienia</label>
                                <DxDateBox v-model="DateOfIssue"
                                           id="dateOfIssueDateBox"
                                           type="date"
                                           display-format="dd/MM/yyyy"
                                           cancel-button-text="Anuluj"
                                           :read-only="isReadOnly"
                                           invalid-date-message="Wartość musi być datą lub czasem">
                                    <DxValidator>
                                        <DxRequiredRule message="Data wystawienia jest wymagana!" />
                                    </DxValidator>
                                </DxDateBox>
                            </div>
                            <div class="col">
                                <label for="saleDateDateBox" class="form-label">Data sprzedaży</label>
                                <DxDateBox v-model="SaleDate"
                                           id="saleDateDateBox"
                                           type="date"
                                           display-format="dd/MM/yyyy"
                                           cancel-button-text="Anuluj"
                                           :read-only="isReadOnly"
                                           invalid-date-message="Wartość musi być datą lub czasem">
                                    <DxValidator>
                                        <DxRequiredRule message="Data sprzedaży jest wymagana!" />
                                    </DxValidator>
                                </DxDateBox>
                            </div>
                            <div class="col">
                                <label for="dateOfPaymentDateBox" class="form-label">Data płatności</label>
                                <DxDateBox v-model="DateOfPayment"
                                           id="dateOfPaymentDateBox"
                                           type="date"
                                           display-format="dd/MM/yyyy"
                                           cancel-button-text="Anuluj"
                                           :read-only="isReadOnly"
                                           invalid-date-message="Wartość musi być datą lub czasem">
                                </DxDateBox>
                            </div>
                        </div>
                        <div class="row mt-4">
                            <div class="col">
                                <label class="form-label">Cena netto</label>
                                <DxNumberBox v-model="NetValue" format="#,##0.00" :read-only="isReadOnly" />
                            </div>
                            <div class="col">
                                <label for="paymentTypeSelectBox" class="form-label">Forma płatności</label>
                                <DxSelectBox :dataSource="paymentTypes"
                                             value-expr="ID"
                                             display-expr="Name"
                                             v-model="PaymentType"
                                             :read-only="isReadOnly"
                                             id="paymentTypeSelectBox" />
                            </div>
                            <div class="col">
                                <label for="fvTypesSelectBox" class="form-label">Typ faktury</label>
                                <DxSelectBox :dataSource="fvTypes"
                                             value-expr="ID"
                                             display-expr="Name"
                                             v-model="FVType"
                                             :read-only="isReadOnly"
                                             id="fvTypesSelectBox" />
                            </div>
                        </div>
                        <div class="row mt-4">
                            <div class="col">
                                <label class="form-label">Cena brutto</label>
                                <DxNumberBox v-model="GrossValue" format="#,##0.00" :read-only="isReadOnly" />
                            </div>
                            <div class="col">
                                <label for="currencySelectBox" class="form-label">Waluta</label>
                                <DxSelectBox :dataSource="currencies"
                                             value-expr="ID"
                                             display-expr="Name"
                                             v-model="Currency"
                                             :read-only="isReadOnly"
                                             id="currencySelectBox" />
                            </div>
                            <div class="col">
                                <label for="paymentStatusSelectBox" class="form-label">Status płatności</label>
                                <DxSelectBox :dataSource="paymentStatuses"
                                             value-expr="ID"
                                             display-expr="Name"
                                             :read-only="isReadOnly"
                                             v-model="PaymentStatus"
                                             id="paymentStatusSelectBox" />
                            </div>
                        </div>
                        <div class="row mt-4">
                            <div class="col-4">
                                <label class="form-label">Kwota VAT</label>
                                <DxNumberBox v-model="VAT" format="#,##0.00" :read-only="isReadOnly" />
                            </div>
                        </div>

                        <DxButton text="Dodaj" @click="showAddPositionPopup()" class="ml-1 mt-4" v-if="buttonAddVisible" :read-only="isReadOnly" />

                        <RecordAdd v-if="addPositionPopupOptions.isVisible"
                                   :parentInv="getInvId"
                                   @close="onPositionPopupClose" />

                        <RecordEdit v-if="detailsPositionPopupOptions.isVisible"
                                    :positionId="{id: positionDetailsId, step:getStep }"
                                    @close="onPosition2PopupClose" />

                        <DxDataGrid :data-source="getPositions"
                                    :remote-operations="false"
                                    :row-alternation-enabled="true"
                                    :show-borders="true"
                                    :hover-state-enabled="true"
                                    :column-auto-width="true"
                                    width="100%"
                                    no-data-text="Brak pozycji"
                                    class="mt-4">
                            <DxFilterRow :visible="true" />
                            <DxLoadPanel :enabled="true" />
                            <DxPaging :page-size="15" />
                            <DxColumn data-field="descryption"
                                      caption="Opis"
                                      data-type="string"
                                      width="40%" />
                            <DxColumn data-field="netValue"
                                      caption="Wartość netto"
                                      data-type="number"
                                      format="#,##0.00" />

                            <DxColumn data-field="grossValue"
                                      caption="Wartość brutto"
                                      data-type="number"
                                      format="#,##0.00" />
                            <DxColumn data-field="id"
                                      caption="Akcje"
                                      cell-template="actions-template"
                                      :allow-sorting="false"
                                      :allow-filtering="false"
                                      :allow-search="false"
                                      alignment="center" />
                            <template #actions-template="{ data }">
                                <div style="display: flex; justify-content: center">
                                    <div>
                                        <a class="action"
                                           @click="detalisPosition({ data })"
                                           title="Szczegóły">
                                            <i class="fas fa-list"></i>
                                        </a>
                                    </div>
                                    <div v-if="buttonAddVisible">
                                        <a class="action"
                                           title="Usuń"
                                           @click="handleDelete({ data })">
                                            <i class="fas fa-trash ml-2"></i>
                                        </a>
                                    </div>
                                </div>
                            </template>
                        </DxDataGrid>

                    </div>
                    <div>
                        <div class="row mt-4 mb-4" style="display: flex; justify-content: flex-end">
                            <div class="text-right ml-4" v-if="buttonVisible && getStep == 1">
                                <DxButton text="Zamknij"
                                          type="default"
                                          @click="handleCancel"
                                          ref="submitButton" />
                            </div>
                            <div class="text-right ml-4" v-if="buttonVisible && (getStep == 3 || getStep == 4 || getStep == 5)">
                                <DxButton text="Odrzuć"
                                          type="default"
                                          @click="handleDecline"
                                          ref="submitButton" />
                            </div>
                            <div class="text-right ml-4  mr-4" v-if="buttonVisible">
                                <DxButton :text="getSubmitText()"
                                          type="default"
                                          @click="handleSubmit"
                                          ref="submitButton" />
                            </div>
                        </div>
                    </div>
                </DxValidationGroup>
            </form>
        </div>
    </div>
</template>

<script>
import { mapState, mapActions, mapGetters, mapMutations } from "vuex";
import DxButton from "devextreme-vue/button";
import DxNumberBox from 'devextreme-vue/number-box';

import RecordEdit from "../invs/RecordEdit";
import RecordAdd from "../invs/RecordAdd";
import DxSelectBox from "devextreme-vue/select-box";

import DxTextBox from "devextreme-vue/text-box";
import {
  DxValidator,
  DxRequiredRule,
} from "devextreme-vue/validator";
import DxValidationGroup from "devextreme-vue/validation-group";
import DxDateBox from "devextreme-vue/date-box";
import {
  DxDataGrid,
  DxLoadPanel,
  DxColumn,
  DxFilterRow,
} from "devextreme-vue/data-grid";
import { createHelpers } from "vuex-map-fields";
import service from './data.js';
const { mapFields } = createHelpers({
    getterType: "invs/getField",
    mutationType: "invs/updateField",
});
    import service2 from './data2.js';
export default {
  name: "InvForm",
  data() {
      const paymentTypes = service.getPaymentTypes();
      const fvTypes = service.getFvTypes();
      const paymentStatuses = service.getPaymentStatuses();
      const currencies = service.getCurrencies();
      const categories = service.getCategories();

      const vats = service2.getVat();
      const jms = service2.getJM();
      return {
          vats,
          jms,
          paymentTypes,
          fvTypes,
          paymentStatuses,
          currencies,
          categories,
          groupRefKey: "targetGroup",
          areStatsVisible: false,
          showToEdit: false,
          addPositionPopupOptions: {
            isVisible: false,
          },
          detailsPositionPopupOptions: {
              isVisible: false,
          },
          positionDetailsId: null,
    };
  },
  computed: {
    ...mapGetters({
      isLoggedIn: "authentication/isLoggedIn",
      getLoggedInUser: "authentication/getLoggedInUser",
      getPositions: "invs/getPositions",
      isAnomaly: "invs/isAnomaly",
      getInvId: "invs/getInvId",
      getStep: "invs/getStep",
      getIntroducer: "invs/getIntroducer",
      getMeritId: "invs/getMeritId", 
      getIsAcceptant: "invs/getIsAcceptant", 
    }),
    ...mapState("invs", ["invs"]),
    ...mapFields([
        "inv.Id",
        "inv.ClientName",
        "inv.NIP",
        "inv.IntroductionDate",
        "inv.FvNumber",
        "inv.IntroducerId",
        "inv.MeritId",
        "inv.DateOfReceipt",
        "inv.DateOfIssue",
        "inv.SaleDate",
        "inv.DateOfPayment",
        "inv.NetValue",
        "inv.PaymentType",
        "inv.FVType",
        "inv.GrossValue",
        "inv.Currency",
        "inv.PaymentStatus",
        "inv.VAT",
        "users",
    ]),
    isReadOnly() {
        if (this.getStep == 1 || this.getStep == 2) {
            return false;
        }
        else {
            return true;
        }
       
    },
    buttonVisible() {
        if (this.getStep == 2) {
            if (this.getLoggedInUser.id == this.getMeritId) {
                return true;
            }
            else {
                return false;
            }
        }
        else
        if (this.getStep == 1)
        {
            if (this.getLoggedInUser.id == this.getIntroducer)
            {
                return true;
            }
            else {
                return false;
            }
        }
        else
        if (this.getStep == 3 || this.getStep == 4 || this.getStep == 5) {

            return this.getIsAcceptant;
            
        }
        else
        if (this.getStep == 6)
        {
            
            return true;
            
        }
        else
        {
            return false;
        }
    },
    buttonAddVisible() {
        if (this.getStep == 2) {
            if (this.getLoggedInUser.id == this.getMeritId) {
                return true;
            }
            else {
                return false;
            }
        }
        else
        if (this.getStep == 1)
        {
            if (this.getLoggedInUser.id == this.getIntroducer)
            {
                return true;
            }
            else {
                return false;
            }
        }
        else
        {
            return false;
        }
    },
    validationGroup: function() {
      return this.$refs[this.groupRefKey].instance;
    },
  },
  methods: {
    ...mapActions({
      getInvDetails: "invs/getInvDetails",
      startInv: "invs/startInv",
      setUsers: "invs/setUsers",
      getInvPositions: "invs/getInvPositions",
      deleteInvPositions: "invs/deleteInvPositions",
      isAcceptantAction: "invs/isAcceptantAction",
      moveInv: "invs/moveInv",
      declineInv: "invs/declineInv",
      cancelInv: "invs/cancelInv",
    }),
    getSubmitText() {
        switch (this.getStep) {
            case 1:
                return "Zarejestruj";
            case 2:
                return "Akceptuj";
            case 3:
                return "Akceptuj";
            case 4:
                return "Akceptuj";
            case 5:
                return "Akceptuj";
            case 6:
                return "Odbierz";
            default:
                return "";
        }
    },
    getStepText() {
        switch (this.getStep) {
            case 1:
                return "Rejestracja";
            case 2:
                return "Weryfikacja";
            case 3:
                return "Akceptacja Kierownika";
            case 4:
                return "Akceptacja Managera";
            case 5:
                return "Akceptacja Zarządu";
            case 6:
                return "Zaakceptowany";
            case 7:
                return "Odebrany";
            case 10:
                return "Odrzucone";
            case 99:
                return "Zamknięte";
            default:
                return "";
        }
    },
    
    ...mapMutations({
      RESET_INV_FORM: "invs/RESET_INV_FORM",
    }),
    handleSubmit() {
        let validationResult = this.validationGroup.validate();
        if (validationResult.isValid) {
            this.moveInv();
            this.$router.push({ path: "/mytasks" });
        }
      },
    handleDecline() {

        this.declineInv()
        .then(() => {
            this.$router.push({ path: "/rejected" });
        });
        
        
    },
    handleCancel() {

        this.cancelInv().then(() => {
            this.$router.push({ path: "/closed" });
        });
        
        
    },
    handleDelete(data) {
        this.deleteInvPositions(data.data.data);
    },
    detalisPosition(data) {
        this.positionDetailsId = data.data.data.id
        this.detailsPositionPopupOptions.isVisible = true;
    },
    showAddPositionPopup() {
        this.addPositionPopupOptions.isVisible = true;
    },
    onPositionPopupClose() {
        this.getInvPositions(this.Id)
        this.addPositionPopupOptions.isVisible = false;
    },
    onPosition2PopupClose() {
        this.detailsPositionPopupOptions.isVisible = false;
    },
  },
  mounted() {
    this.setUsers();
    if (this.$route.params.id) {
      this.showToEdit = true;
    }
    if (this.showToEdit) {
        this.getInvDetails({ id: this.$route.params.id, userId: this.getLoggedInUser.id } );
    }
    else
    {
        this.startInv(this.getLoggedInUser.id);
    }
    
  },
  components: {
    RecordAdd,
    DxTextBox,
    DxButton,
    DxValidator,
    DxRequiredRule,
    DxValidationGroup,
    DxDateBox,
    DxDataGrid,
    DxLoadPanel,
    DxColumn,
    DxFilterRow,
    DxSelectBox,
    DxNumberBox,
    RecordEdit
  },
  beforeUnmount() {
    this.RESET_INV_FORM();
  },
};
</script>
<style scoped>
.bolder-goals {
  font-weight: bold;
}
</style>
