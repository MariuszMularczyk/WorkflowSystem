<template>
    <div>
        <DxPopup v-model:visible="popupVisible"
                 :drag-enabled="false"
                 :close-on-outside-click="false"
                 :show-close-button="false"
                 :show-title="true"
                 :width="650"
                 :height="580"
                 container=".dx-viewport"
                 title="Szczegóły pozycji"
                 :shading="false">
            <div :v-if="!isReadOnly">
                <DxToolbarItem widget="dxButton"
                               toolbar="bottom"
                               
                               location="after"
                               :options="sendButtonOptions" />
            </div>
            <DxToolbarItem widget="dxButton"
                           toolbar="bottom"
                           location="before"
                           :options="closeButtonOptions" />
            <form>
                <DxValidationGroup :ref="groupRefKey">
                    <div class="secondary-border2" v-if="overPaid">
                        <div class="row">
                            <div class="col">
                                <label class="form-label">Wykryto anomalię cenową, sprawdź ponownie ten zakup</label>
                            </div>

                        </div>
                    </div>
                    <div class="row">
                        <div class="col">
                            <label for="descTextArea" class="form-label">Opis</label>
                            <DxTextArea :height="90"
                                        id="descTextArea"
                                        :read-only="isReadOnly"
                                        :max-length="maxLength"
                                        v-model="descryption" />
                        </div>


                    </div>
                    <div class="row mt-4">
                        <div class="col">
                            <label class="form-label">Ilość</label>
                            <DxNumberBox v-model="quantity" format="#,##0.00" :read-only="isReadOnly" />
                        </div>
                        <div class="col">
                            <label class="form-label">Jednostka miary</label>
                            <DxSelectBox :dataSource="jms"
                                         value-expr="ID"
                                         display-expr="Name"
                                         v-model="jm"
                                         :read-only="isReadOnly" />
                        </div>

                    </div>
                    <div class="row mt-4">
                        <div class="col">
                            <label class="form-label">Cena jednostkowa netto</label>
                            <DxNumberBox v-model="netValueUnit" format="#,##0.00" :read-only="isReadOnly" />
                        </div>
                        <div class="col">
                            <label for="titleTextBox" class="form-label">Wartość netto</label>
                            <DxNumberBox v-model="netValue" format="#,##0.00" :read-only="isReadOnly" />
                        </div>

                    </div>
                    <div class="row mt-4">
                        <div class="col">
                            <label for="toUserIdSelectBox" class="form-label">Vat</label>
                            <DxSelectBox :dataSource="vats"
                                         value-expr="ID"
                                         :read-only="isReadOnly"
                                         display-expr="Name"
                                         v-model="vat" />
                        </div>
                        <div class="col">
                            <label for="titleTextBox" class="form-label">Wartość brutto</label>
                            <DxNumberBox v-model="grossValue" format="#,##0.00" :read-only="isReadOnly" />
                        </div>
                    </div>
                    <div class="row">
                        <div class="col">
                            <DxValidationSummary />
                        </div>
                    </div>
                </DxValidationGroup>
            </form>
        </DxPopup>
    </div>
</template>
<script>
    import DxTextArea from 'devextreme-vue/text-area';
    import { DxPopup, DxToolbarItem } from "devextreme-vue/popup";

    import DxSelectBox from "devextreme-vue/select-box";
    import DxNumberBox from 'devextreme-vue/number-box';
    import { mapActions, mapGetters, mapMutations } from "vuex";
    import { createHelpers } from "vuex-map-fields";
    const { mapFields } = createHelpers({
        getterType: "invPositions/getField",
        mutationType: "invPositions/updateField",
    });
    import DxValidationGroup from "devextreme-vue/validation-group";
    import DxValidationSummary from "devextreme-vue/validation-summary";
    import { useToast } from "vue-toastification";
    import service from './data2.js';

    export default {
        name: "RecordAdd",
        props: {
            positionId: {
                type: Object,
                required: true,
            },
        },
        data() {
            const vats = service.getVat();
            const jms = service.getJM();
            return {
                vats,
                jms,
                users: [],
                popupVisible: false,
                isUserSelectorReadOnly: false,
                stepId: null,
                sendButtonOptions: {
                    icon: "save",
                    text: "Zapisz",
                    onClick: () => this.handleSubmit(),
                },
                closeButtonOptions: {
                    text: "Zamknij",
                    onClick: () => {
                        this.popupVisible = false;
                        this.$emit("close");
                    },
                    type: "normal",
                },
                groupRefKey: "targetGroup",
            };
        },
        computed: {
            ...mapGetters({
                getLoggedInUser: "authentication/getLoggedInUser",
                getPositionDetails: "invPositions/getPositionDetails",
            }),
            ...mapFields([
                "position.descryption",
                "position.jm",
                "position.netValueUnit",
                "position.netValue",
                "position.grossValue",
                "position.quantity",
                "position.vat",
                "position.invId",
                "position.overPaid",
            ]),
            validationGroup: function () {
                return this.$refs[this.groupRefKey].instance;
            },
            isReadOnly: function () {
                if (this.stepId == 1 || this.stepId == 2) {
                    return false;
                }
                else {
                    return true;
                }
            },
        },
        methods: {
            ...mapActions({
                setPositionDetails: "invPositions/setPositionDetails",
                addPosition: "invPositions/addPosition",
                updatePosition: "invPositions/updatePosition",

            }),

            ...mapMutations({
                SET_INV_ID: "invPositions/SET_INV_ID",
                RESET_POSITION_DETAILS: "invPositions/RESET_POSITION_DETAILS",
            }),
            async handleSubmit() {
                let validationResult = this.validationGroup.validate();
                if (validationResult.isValid) {
                    await this.updatePosition();
                    useToast().success("Pozycja została dodana pomyślmie");
                    this.popupVisible = false;
                    this.$emit("close");
                }
            },

        },
        components: {
            DxPopup,
            DxToolbarItem,
            DxSelectBox,
            DxValidationGroup,
            DxValidationSummary,
            DxTextArea,
            DxNumberBox
        },
        mounted() {
            this.stepId = this.positionId.step;
            this.popupVisible = true;
            console.log(this.positionId);
            console.log(this.stepId);
            this.setPositionDetails(this.positionId.id);
        },
        beforeUnmount() {
            this.RESET_POSITION_DETAILS();
        },
    };
</script>
