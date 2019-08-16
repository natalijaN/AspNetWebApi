let getAllBtn = document.getElementById("btn1");
let getByIdBtn = document.getElementById("btn2");
let addUserBtn = document.getElementById("btn3");
let getByIdInput = document.getElementById("input2");
let addUserFirstName = document.getElementById("input31");
let addUserLastName = document.getElementById("input32");
let addUserAge = document.getElementById("input33");

let port = "50204"
let getAllUsers = async () => {
    let url = "http://localhost:" + port + "/api/users";

    let response = await fetch(url);
    let data = await response.json();
    console.log(data);
};

let getUserById = async () => {
    let url = "http://localhost:" + port + "/api/users/" + getByIdInput.value;

    let response = await fetch(url);
    let data = await response.text();
    console.log(data);
};

let addUser = async () => {
    let url = "http://localhost:" + port + "/api/users";
    let user = { firstName: addUserFirstName.value, lastName: addUserLastName.value, age: addUserAge.value }
    await fetch(url, {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(user)
    });
}

getAllBtn.addEventListener("click", getAllUsers);
getByIdBtn.addEventListener("click", getUserById);
addUserBtn.addEventListener("click", addUser);