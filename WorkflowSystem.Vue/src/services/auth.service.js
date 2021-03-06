import axios from 'axios';

const API_URL = 'https://localhost:44379/api/users/';

class AuthService {
  login(user) {
    return axios
      .post(API_URL + 'login', user)
      .then(response => {
        if (response.data.token) {
          localStorage.setItem('user', JSON.stringify(response.data));
        }
        return response.data;
      });
  }

  logout() {
    localStorage.removeItem('user');
  }

}

export default new AuthService();
