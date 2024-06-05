let categoriesToFilter = ""
const userId = localStorage.getItem("currentUserId");

const filterProducts = async () => {

    clearScreen()

    let min = document.getElementById("minPrice")?.value;
    let max = document.getElementById("maxPrice")?.value;
    let descripition = document.getElementById("nameSearch")?.value;
    if (min==null) {
        min = 0
    }
    if (max == null) {
        max = 1000
    }
    if (descripition == null) {
        descripition = ""
    }

    
    const productsRes = await fetch(`api/Product?min=${min}&max=${max}&descripition=${descripition}&categoriesIds=${categoriesToFilter}`);
    const products = await productsRes.json()
    console.log("products", products);
    
    showProducts(products);

   

}
const loadCategories = async () => {
    const categriesRes = await fetch(`api/category`);
    const categories = await categriesRes.json()
    console.log("categories", categories);

    showCategories(categories);
}
const showProducts = (products) => {
    const productListDiv = document.getElementById('ProductList');
    const template = document.querySelector('#temp-card');
    products.forEach(product => {
        const clone = document.importNode(template.content, true);

        clone.querySelector('img').src = `images/${product.prodName}`;
        clone.querySelector('h1').textContent = product.description;
        clone.querySelector('.price').textContent = `Price: $${product.price}`;
        clone.querySelector('.description').textContent = product.categoryName;
        const btn = clone.querySelector('button')
        btn.addEventListener("click", function () {
            addProduct(product);
        })
        productListDiv.appendChild(clone);
    });
}
const addProduct=(product) => {
    console.log(product);
    if (localStorage.getItem(`basket_for_user_${userId}`)) {
        var i = 0
        let basket = JSON.parse(localStorage.getItem(`basket_for_user_${userId}`))
        for (; i < basket.length; i++) {
            if (basket[i].prodId == product.prodId) {
                basket[i].count++;
                break;
            }
        }
        if (i == basket.length) {
            product = { ...product, count: 1 }
            basket = [...basket, product]
        }
        localStorage.setItem(`basket_for_user_${userId}`, JSON.stringify(basket))
    }
    else {
        
        localStorage.setItem(`basket_for_user_${userId}`, JSON.stringify([{ ...product, count: 1 }]))
    }
 
    globalSum()
}
const showCategories = (categories) => {
    const categoryListDiv = document.getElementById('categoryList');
    const template = document.querySelector('#temp-category');
    categories.forEach(category => {
        const clone = document.importNode(template.content, true);
        clone.querySelector('.OptionName').textContent = category.categoryName;
        clone.querySelector('.opt').value = category.categoryId;
        const opt = clone.querySelector('.opt')
       
        opt.addEventListener("change", changeHandler);
        categoryListDiv.appendChild(clone);
    });
}
const changeHandler = (event) => {
    console.log("event", event)
    if (event.target.checked) {
        categoriesToFilter += event.target.value + " ";
    }
    else {
        v=""
        categoriesToFilter.split(" ").forEach(c => {
            c != event.target.value ? v+=c+" ":""
        })
        if (v == ""|| v==" ") 
            categoriesToFilter = v.trim();
        
        else
        categoriesToFilter = v.trim()+" ";
        
    }
    console.log(categoriesToFilter)
    filterProducts()
}
const clearScreen = () => {
    const productListDiv = document.getElementById('ProductList');
    while (productListDiv?.firstChild) {
        productListDiv.removeChild(productListDiv.firstChild);
    } 

}

const globalSum = () => {
    var sum = 0
    let products = JSON.parse(localStorage.getItem(`basket_for_user_${userId}`))
    for (var i = 0; i < products.length; i++) {
        sum += products[i].count 
    }
    document.getElementById("ItemsCountText").innerText = sum
}
filterProducts()
globalSum()
loadCategories()

