const RegisterFunc = async () => {
    let FirstName = document.getElementById("firstName").value;
    let LastName = document.getElementById("lastName").value;
    let Email = document.getElementById("email").value;
    let Password = document.getElementById("password").value;
    const user = { FirstName: FirstName, LastName: LastName, Email: Email, Password: Password }
    var res = await fetch("api/User/register",
        {
            method: "POST",
            headers: {
                'Content-Type': "application/json"
            },
            body: JSON.stringify(user)

        })
    if (res.status >= 400) {
        alert("not success")
    }
    else {
        const userRegister = await res.json();
        alert(`${userRegister.firstName} registered`)
    }
}
const LoginFunc = async () => {

    let userName = document.getElementById("loginEmail").value;
    let password = document.getElementById("loginPassword").value;
    const user = { Email: userName, Password: password };
    const res = await fetch('api/User/login', {

        method: "POST", headers: {
            'Content-Type': "application/json"
        },
        body: JSON.stringify(user)
    })
    if (res.status >= 400) {
        alert("not success")
    }
    else {
        
        const userRegister = await res.json();
        sessionStorage.setItem("userId", userRegister.userId)
        localStorage.setItem("currentUserId", userRegister.userId)
        window.location.replace("/Products.html");
    }
}
const UpdateFunc = async () => {
    const id = sessionStorage.getItem("userId")
    let firstName = document.getElementById("firstName").value;
    let lastName = document.getElementById("lastName").value;
    let email = document.getElementById("email").value;
    let password = document.getElementById("password").value;
    let user = { firstName, lastName, email, password }
    const res = await fetch(`api/User/${id}`, {

        method: "PUT",
        headers: {
            'Content-Type': "application/json"
        },
        body: JSON.stringify(user)
    })
    if (res.status >= 400) {
        alert("not success")
    }
    else {
        const userRegister = await res.json();
        alert(`${userRegister.firstName} updated !!`)
    }
}
const CheckPassword = async () => {
    let password = document.getElementById("password").value;
   
    const res = await fetch(`api/User/password`, {

        method: "POST",
        headers: {
            'Content-Type': "application/json"
        },
        body: JSON.stringify(password)
    })
    const score = await res.json();

    
    if (score == 0) {
        let color = document.getElementById("passwordCheck");
        color.style.setProperty("background-color", "red")
        
    }
   else if (score == 1) {
        let color = document.getElementById("passwordCheck");
        color.style.setProperty("background-color", "orange")
    }
    else if (score >= 2) {
        let color = document.getElementById("passwordCheck");
        color.style.setProperty("background-color", "green")
    }
}