<template>
  <div class="home-background">
    <div class="main-text">
      <div class="glass">
        <h3 class="mb-4 header">Dołącz do WorkFlow</h3>
        <div class="row">
          <div class="col mt-2">
            <DxButton
              text="Dołącz"
              @click="routerPushToRegister"
              type="default"
            />
          </div>
        </div>
        <div class="row mt-5">
          <p>
            W przypadku wystąpienia problemów z funkcjonowaniem serwisu, zaleca
            się kontakt ze wsparciem
            <a href="mailto:mariuszmu97@gmail.com">tutaj</a>.
          </p>
        </div>
      </div>
    </div>
  </div>
</template>
<script>
import DxButton from "devextreme-vue/button";
import router from "../router";

export default {
  components: {
    DxButton,
  },
  methods: {
    routerPushToRegister() {
      router.push({ path: "/register" });
    },
  },
};
</script>
<style scoped>
.header {
  color: whitesmoke;
  font-size: 1.6rem;
}
</style>
