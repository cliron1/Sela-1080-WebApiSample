const storageKey = "myToken";

const tokenFromStorage = sessionStorage.getItem(storageKey);

let token;

if (tokenFromStorage) {
  console.debug('retrieved token from storage')
  token = tokenFromStorage;

} else {
  const loginData = {
    username: "liron",
    pwd: "123456"
  };

  const options = {
    method: 'POST',
    headers: {
      'Content-Type': 'application/json'
    },
    body: JSON.stringify(loginData)
  };

  const response = await fetch('https://localhost:44380/auth/login', options);
  token = await response.text();

  console.debug('retrieved token from api')
  sessionStorage.setItem(storageKey, token)
}

export default token;