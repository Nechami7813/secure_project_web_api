const userId = localStorage.getItem("currentUserId");
var sum = 0
const loadBasket = () => {
    clearScreen()
    let products = JSON.parse(localStorage.getItem(`basket_for_user_${userId}`))

    showOrders(products)
    globalSum()
}
const showOrders = (products) => {
    let template = document.getElementById("temp-row");
    let tbody = document.querySelector("#items");

    products.forEach(product => {
        const newRow = template.content.cloneNode(true);
        newRow.querySelector(".image").src = `images/${product.prodName}`;
        newRow.querySelector(".imageColumn").textContent = product.description;
        newRow.querySelector(".price").textContent = `Price: ${product.price}$`;
        newRow.querySelector("h3").textContent = `amount: ${product.count}`;
        let delBtn = newRow.querySelector(".expandoHeight")
        let decBtn = newRow.querySelector(".dec")
        let incBtn = newRow.querySelector(".inc")

        delBtn.addEventListener("click", function () {
            deleteProd(product);
        })
        decBtn.addEventListener("click", function () {
            decProd(product);
        })
        incBtn.addEventListener("click", function () {
            incProd(product);
        })
        tbody.appendChild(newRow);
    });
}
const deleteProd = async (product) => {
    let products = JSON.parse(localStorage.getItem(`basket_for_user_${userId}`))
    for (var i = 0; i < products.length; i++) {
        if (products[i].prodId == product.prodId) {
            products.splice(i, 1);
            break;
        }
    }

  await  localStorage.setItem(`basket_for_user_${userId}`, JSON.stringify(products))
    loadBasket()
}

const incProd = (product) => {
    let products = JSON.parse(localStorage.getItem(`basket_for_user_${userId}`))
    for (var i = 0; i < products.length; i++) {
        if (products[i].prodId == product.prodId) {
            products[i].count++;
            break;
        }
    }

    localStorage.setItem(`basket_for_user_${userId}`, JSON.stringify(products))
    loadBasket()
}
const decProd = (product) => {
    let products = JSON.parse(localStorage.getItem(`basket_for_user_${userId}`))
    for (var i = 0; i < products.length; i++) {
        if (products[i].prodId == product.prodId) {
            products[i].count--;
            if (products[i].count == 0) {
                products.splice(i, 1);
            }
            break;
        }
    }

    localStorage.setItem(`basket_for_user_${userId}`, JSON.stringify(products))
    loadBasket()
}
const clearScreen = () => {
    const cistGroup = document.getElementById('items');
    while (cistGroup?.firstChild) {
        cistGroup.removeChild(cistGroup.firstChild);
    }
}


const globalSum = () => {
    sum = 0
    let products = JSON.parse(localStorage.getItem(`basket_for_user_${userId}`))
    for (var i = 0; i < products.length; i++) {
        sum += products[i].count * products[i].price
    }
    document.getElementById("totalAmount").innerText = `${sum}$`
}
const placeOrder = async () => {
    if (!localStorage.getItem(`basket_for_user_${userId}`)) {
        alert("😍🤣😂😘עדיין לא הזמנת מוצרים😍🤣😂😘")
        return
    }
    let products = JSON.parse(localStorage.getItem(`basket_for_user_${userId}`))
    if (products.length==0) {
        alert("😍🤣😂😘עדיין לא הזמנת מוצרים😍🤣😂😘")
        return
    }

    let orderProducts = products.map(p => {
        return {
            prodId: p.prodId,
            quantity: p.count
        }
    })
    const order = { userId: userId, OrderItemDTOs: orderProducts, orderSum: sum }
    console.log(order);

    var res = await fetch("api/Order",
        {
            method: "POST",
            headers: {
                'Content-Type': "application/json"
            },
            body: JSON.stringify(order)

        })

    if (res.status == 401) {
        alert("❌❌❌❌לא תגנוב❌❌❌❌")
        return;
    }
    if (res.status >= 400) {
        alert("לא הצלחנו לקבל את הזמנתך")
        return;
    }
    else {
        alert("😍🤣😂😘הקניה הסתיימה😍🤣😂😘")
        await localStorage.setItem(`basket_for_user_${userId}`, [])
        loadBasket()
    }
}

loadBasket()
