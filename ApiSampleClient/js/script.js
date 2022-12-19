import token from './auth.js'
//console.log('token', token);

// 1. get list of pets
const petsResponse = await fetch('https://localhost:44380/pets', {
  method: 'GET',
  headers: {
    'Content-Type': 'application/json',
    'Authorization': `Bearer ${token}`
  }
});
const pets = await petsResponse.json();
//console.log('pets', pets);

// 2. display the list in the DOM
let html = '';
for (const pet of pets) {
  html += `<li>${pet.name}</li>`;
}
document.querySelector('#petsList').innerHTML = html;
