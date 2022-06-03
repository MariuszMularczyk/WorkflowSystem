import { createRouter, createWebHistory } from "vue-router";
import store from "./store/index";

//Authentication
import UserProfile from "./pages/authentication/UserProfile.vue";

//Players
import RegisterEditUser from "./pages/users/RegisterEditUser.vue";

import MyTasksList from "./pages/invs/MyTasksList.vue";
import ClosedList from "./pages/invs/ClosedList.vue";
import AcceptedList from "./pages/invs/AcceptedList.vue";
import ManagementAcceptanceList from "./pages/invs/ManagementAcceptanceList.vue";
import ManagerAcceptanceList from "./pages/invs/ManagerAcceptanceList.vue";
import SupervisorAcceptanceList from "./pages/invs/SupervisorAcceptanceList.vue";
import RecivedList from "./pages/invs/RecivedList.vue";
import RejectedList from "./pages/invs/RejectedList.vue";
import VerificationList from "./pages/invs/VerificationList.vue";
import InvForm from "./pages/invs/InvForm.vue";



//Teams
import AdminUsersList from "./pages/admin/AdminUsersList.vue";
import AdminRolesList from "./pages/admin/AdminRolesList.vue";


//Messages
import MessagesList from "./pages/messages/MessagesList.vue";

//Dashboard
import Dashboard from "./pages/dashboard/Dashboard.vue";


//Others
import HomePage from "./pages/HomePage.vue";
import PageNotFound from "./pages/PageNotFound.vue";
import NoPermissionPage from "./pages/NoPermissionPage.vue";
import AdminDashboard from "./pages/admin/AdminDashboard.vue";

const router = createRouter({
  history: createWebHistory(),
  routes: [
    { path: "/", component: HomePage, meta: { guest: true } },
    { path: "/profile", component: UserProfile, meta: { guest: false } },
    { path: "/closed", component: ClosedList, meta: { admin: true } },
    { path: "/accepted", component: AcceptedList, meta: { admin: true } },
    { path: "/managementAcceptance", component: ManagementAcceptanceList, meta: { admin: true } },
    { path: "/managerAcceptance", component: ManagerAcceptanceList, meta: { admin: true } },
    { path: "/supervisorAcceptance", component: SupervisorAcceptanceList, meta: { admin: true } },
    { path: "/recived", component: RecivedList, meta: { admin: true } },
    { path: "/rejected", component: RejectedList, meta: { admin: true } },
    { path: "/verification", component: VerificationList, meta: { admin: true } },
    { path: "/mytasks", component: MyTasksList, meta: { admin: true } },
    {
      path: "/inv/:id?",
      component: InvForm,
      name: "invForm",
      meta: { guest: false }
    },
    { path: "/register", component: RegisterEditUser, meta: { guest: true } },
    {
      path: "/profile-edit",
      name: "editProfile",
      component: RegisterEditUser,
      params: true,
      meta: { guest: false },
    },
    { path: "/admin-users", component: AdminUsersList, meta: { admin: true, adminOnly: true } },
    { path: "/admin-roles", component: AdminRolesList, meta: { admin: true, adminOnly: true } },


    { path: "/messages", component: MessagesList, meta: { guest: false } },
    {
      path: "/dashboard",
      component: Dashboard,
      meta: { guest: false },
    },
    { path: "/:pageNotFound(.*)", component: PageNotFound },
    { path: "/no-permission", component: NoPermissionPage },
    { path: "/admin", component: AdminDashboard, meta: { admin: true, adminOnly: true } },
  ],
});

router.beforeEach((to, from, next) => {
  const isAuthenticated = store.getters["authentication/isLoggedIn"];
  const getUserType = store.getters["authentication/getLoggedInUserType"];
  if (to.meta.guest != null && !to.meta.guest) {
    if (!isAuthenticated) {
      return next({ path: "/no-permission" });
    }
  }

  if (
    to.meta.guest != null &&
    !to.meta.guest &&
    to.meta.player != null &&
    !to.meta.player
  ) {
    if (getUserType != 2) {
      return next({ path: "/no-permission" });
    }
  }

  if (to.meta.adminOnly) {
    if (getUserType != 10) {
      return next({ path: "/no-permission" });
    }
  }

  if (to.meta.guest != null && to.meta.guest) {
    if (isAuthenticated) {
      return next({ path: "/profile" });
    }
  }

  if (getUserType == 10 && (to.meta.admin == null || to.meta.admin == false)) {
    return next({ path: "/admin" });
  }
  return next();
});

export default router;
