import { createStore } from "vuex";

import usersModule from "./modules/users/index";
import invsModule from "./modules/invs/index";
import rolesModule from "./modules/roles/index";
import authenticationModule from "./modules/authentication/index";
import messagesModule from "./modules/messages/index";
import invPositionsModule from "./modules/invPositions/index";
import listsModule from "./modules/lists/index";

const store = createStore({
  modules: {
    users: usersModule,
    invs: invsModule,
    roles: rolesModule,
    authentication: authenticationModule,
    messages: messagesModule,
    invPositions: invPositionsModule,
    lists: listsModule,
  },
});

export default store;
