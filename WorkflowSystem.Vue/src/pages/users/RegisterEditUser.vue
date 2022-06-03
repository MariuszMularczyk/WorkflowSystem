<template>
  <div class="big-data-grid-center">
    <h3>{{ getTitle() }}</h3>
    <p>
        {{ getSubTitle() }}
    </p>
    <form>
      <DxValidationGroup :ref="groupRefKey">
        <div class="primary-border">
          <h4 class="line">Dane</h4>
          <div class="row">
            <div class="col">
              <label for="firstNameTextBox" class="form-label">Imię</label>
              <DxTextBox v-model="FirstName" id="firstNameTextBox" />
            </div>
            <div class="col">
              <label for="lastNameTextBox" class="form-label">Nazwisko</label>
              <DxTextBox v-model="LastName" id="lastNameTextBox">
                <DxValidator>
                  <DxRequiredRule message="Nazwisko jest wymagane!" />
                </DxValidator>
              </DxTextBox>
            </div>
            <div class="col">
              <label for="dateOfBirthDateBox" class="form-label"
                >Data urodzenia</label
              >
              <DxDateBox
                v-model="DateOfBirth"
                id="dateOfBirthDateBox"
                type="date"
                display-format="dd/MM/yyyy"
                cancel-button-text="Anuluj"
                invalid-date-message="Wartość musi być datą lub czasem"
              >
                <DxValidator>
                  <DxRequiredRule message="Data urodzenia jest wymagana!" />
                  <DxRangeRule
                    :max="maxDate"
                    message="Musisz mieć minimum 18 lat!"
                  />
                </DxValidator>
              </DxDateBox>
            </div>
          </div>
            <div class="row mt-4">
              <div class="col" v-if="!showToEdit">
                <label for="loginUsernameTextBox" class="form-label"
                  >Nazwa użytkownika</label
                >
                <DxTextBox v-model="LoginUsername" id="loginUsernameTextBox">
                  <DxValidator>
                    <DxRequiredRule
                      message="Nazwa użytkownika jest wymagana!"
                    />
                    <DxAsyncRule
                      :validation-callback="validateUsername"
                      message="Taki użytkownik istnieje już w systemie!"
                    />
                  </DxValidator>
                </DxTextBox>
              </div>
              <div class="col">
                <label for="emailTextBox" class="form-label">E-mail</label>
                <DxTextBox v-model="Email" id="emailTextBox">
                  <DxValidator>
                    <DxEmailRule message="E-mail jest nieprawidłowy." />
                  </DxValidator>
                </DxTextBox>
              </div>
            </div>
            <div class="row mt-4" v-if="!showToEdit">
              <div class="col">
                <label for="loginPasswordTextBox" class="form-label"
                  >Hasło</label
                >
                <DxTextBox
                  v-model="LoginPassword"
                  id="loginPasswordTextBox"
                  mode="password"
                >
                  <DxValidator>
                    <DxRequiredRule message="Hasło jest wymagane!" />
                  </DxValidator>
                </DxTextBox>
              </div>
              <div class="col">
                <label for="loginPasswordConfirmationTextBox" class="form-label"
                  >Powtórz hasło</label
                >
                <DxTextBox
                  id="loginPasswordConfirmationTextBox"
                  mode="password"
                >
                  <DxValidator>
                    <DxRequiredRule
                      message="Powtórzenie hasła jest wymagane!"
                    />
                    <DxCompareRule
                      :comparison-target="passwordComparison"
                      message="Hasła się nie zgadzają!"
                    />
                  </DxValidator>
                </DxTextBox>
              </div>
            </div>
            <div class="row mt-4" v-if="!this.$route.params.playerToEditId">
              <div class="px-3">
                <vue-recaptcha
                  v-show="true"
                  siteKey="6LejOUwdAAAAAJYhE8VtA-J0PNrriXDlNyLg9ETw"
                  size="normal"
                  theme="dark"
                  hl="pl"
                  @verify="recaptchaVerified"
                  @expire="recaptchaExpired"
                  @fail="recaptchaFailed"
                  ref="vueRecaptcha"
                  :style="{ border: borderStyle }"
                >
                </vue-recaptcha>
                <DxValidator :adapter="recaptchaValidatorConfig">
                  <DxRequiredRule
                    message="Zaznacz pole 'Nie jestem robotem'."
                  />
                </DxValidator>
              </div>
            </div>
          </div>
          <div class="row mt-4">
            <div class="col text-right">
              <DxButton
                :text="getSubmitText()"
                type="default"
                @click="handleSubmit"
                ref="submitButton"
              />
            </div>
          </div>
        
      </DxValidationGroup>
    </form>
  </div>
</template>
<script>
import DxTextBox from "devextreme-vue/text-box";
import DxButton from "devextreme-vue/button";
import {
  DxValidator,
  DxRequiredRule,
  DxEmailRule,
  DxCompareRule,
  DxAsyncRule,
  DxRangeRule,
} from "devextreme-vue/validator";
import DxValidationGroup from "devextreme-vue/validation-group";

import DxDateBox from "devextreme-vue/date-box";

import { mapActions, mapGetters, mapMutations } from "vuex";
import { createHelpers } from "vuex-map-fields";
const { mapFields } = createHelpers({
  getterType: "users/getField",
  mutationType: "users/updateField",
});

import { useToast } from "vue-toastification";
import vueRecaptcha from "vue3-recaptcha2";
import { userTypeEnum } from "../../enums/userTypeEnum";

export default {
  data() {
    const callbacks = [];
    const recaptchaValidatorConfig = {
      getValue: () => {
        return this.isRecaptchaVerified;
      },
      applyValidationResults: (e) => {
        this.borderStyle = e.isValid ? "none" : ".5px solid #813e3c";
      },
      validationRequestsCallbacks: callbacks,
    };
    const currentDate = new Date();
    return {
      showToEdit: false,
      player: {},
      groupRefKey: "targetGroup",
      isRecaptchaVerified: false,
      recaptchaValidatorConfig,
      borderStyle: "none",
      userTypeEnum,
      maxDate: new Date(
        currentDate.setFullYear(currentDate.getFullYear() - 18)
      ),
    };
  },
  computed: {
    ...mapGetters({
      getLoggedInUser: "authentication/getLoggedInUser",
    }),
    ...mapFields([
      "player.Id",
      "player.FirstName",
      "player.LastName",
      "player.DateOfBirth",
      "player.Email",
      "player.LoginUsername",
      "player.LoginPassword",
    ]),
    validationGroup: function() {
      return this.$refs[this.groupRefKey].instance;
    },
  },
  methods: {
    ...mapActions({
      addPlayer: "users/addPlayer",
      editPlayer: "users/editPlayer",
      validateUsername: "users/validateUsername",
      setPlayerDetails: "users/setPlayerDetails",
      setUserDataDetails: "users/setUserDataDetails",
    }),
    ...mapMutations({
      RESET_PLAYER_FORM: "users/RESET_PLAYER_FORM",
    }),
    getTitle() {
      if (this.showToEdit) {
        return "Edycja profilu";
      } else {
        return "Rejestracja użytkownika";
      }
      },
    getSubTitle() {
          if (!this.showToEdit) {
              return "Ze względów bezpieczeństwa, każdy nowy użytkownik wymaga akceptacji przez administratora serwisu.";
          } else {
              return "";
          }
     },
    passwordComparison() {
      return this.LoginPassword;
    },
    handleSubmit() {
      let validationResult = this.validationGroup.validate();
      const buttonInstance = this.$refs["submitButton"].instance;
      if (
        !this.showToEdit &&
        validationResult.isValid &&
        this.isRecaptchaVerified
      ) {
        validationResult.status === "pending" &&
          validationResult.complete.then((res) => {
            if (res.isValid) {
              buttonInstance.option("icon", "fas fa-circle-notch fa-spin");
              buttonInstance.option("text", "Dodaję zawodnika...");
              this.addPlayer().then(() => {
                buttonInstance.option("icon", "");
                buttonInstance.option("text", this.getSubmitText());
                useToast().success("Użytkownik dodany pomyślnie!");
                this.$router.push({ path: "/" });
              });
            }
          });
      } else if (this.showToEdit && validationResult.isValid) {
        buttonInstance.option("icon", "fas fa-circle-notch fa-spin");
        buttonInstance.option("text", "Edytuję profil");
        this.editPlayer().then(() => {
          buttonInstance.option("icon", "");
          buttonInstance.option("text", this.getSubmitText());
          useToast().success("Edycja zakończona pomyślnie!");
          this.$router.push({ path: "/profile" });
        });
      }
    },
    recaptchaVerified() {
      this.isRecaptchaVerified = true;
    },
    recaptchaExpired() {
      this.isRecaptchaVerified = false;
      this.$refs.vueRecaptcha.reset();
    },
    recaptchaFailed() {
      this.isRecaptchaVerified = false;
    },
    getSubmitText() {
      if (this.showToEdit) {
        return "Edytuj profil";
      } else {
        return "Zarejestruj";
      }
    },
  },
  mounted() {
    if (this.$route.params.playerToEditId) {
      this.showToEdit = true;
    }
    this.RESET_PLAYER_FORM();
    if (this.showToEdit) {
      if (this.getLoggedInUser) {
        switch (this.getLoggedInUser.userType) {
          case userTypeEnum.USER:
            this.setPlayerDetails(this.$route.params.playerToEditId);
            break;
          default:
            this.setUserDataDetails(this.$route.params.playerToEditId);
            break;
        }
      }
    }
  },
  components: {
    DxTextBox,
    DxButton,
    DxValidator,
    DxRequiredRule,
    DxEmailRule,
    DxCompareRule,
    DxValidationGroup,
    DxAsyncRule,
    DxRangeRule,
    vueRecaptcha,
    DxDateBox,
  },
  beforeUnmount() {
    this.RESET_PLAYER_FORM();
  },
};
</script>
