// Ürünler dizisi (örnek veri, gerçek veriyle değiştirilebilir)
const laptops = [
    // ... ürün dizini burada olacak ...
];
// Sepet dizisi
let cart = [];
let currentList = laptops;
// Markalar ve kategoriler
const brands = [...new Set(laptops.map(x => x.brand))];
const kategoriler = [...new Set(laptops.map(x => x.kategori).filter(Boolean))];
const kampanyaUrunler = laptops.slice().sort(() => 0.5 - Math.random()).slice(0, 3);
const firsatUrunler = laptops.slice().sort(() => 0.5 - Math.random()).slice(0, 3);

// Siparişler dizisi (localStorage'dan yüklenir)
window.allOrders = [];
if (localStorage.getItem('orders')) {
    try {
        window.allOrders = JSON.parse(localStorage.getItem('orders'));
    } catch (e) {
        window.allOrders = [];
    }
}

// ====== ÜRÜNLERİ EKLEME – SEPET ======
function renderProducts(list = laptops) {
    currentList = list;
    const productsSection = document.getElementById('products');
    if (!productsSection) return;
    productsSection.innerHTML = '';
    list.forEach((laptop) => {
        let stokYok = laptop.stock === 0;
        let stokVar = typeof laptop.stock === "number" && laptop.stock > 0 && laptop.stock <= 3;
        let sinirsiz = laptop.stock === undefined || laptop.stock === null;
        productsSection.innerHTML += `
        <div class="product-card" onclick="showModal('${laptop.name.replace(/'/g, "\\'")}')">
            <div class="product-imgbox">
                <img src="/images/${laptop.img}" alt="${laptop.name}" class="product-img" 
                     onerror="this.src='https://dummyimage.com/140x90/333/fff&text=YOK';" />
            </div>
            <div class="product-info">
                <h3>${laptop.name}</h3>
                <ul class="spec-list">
                    ${laptop.specs.slice(0, 2).map(s => `<li>${s}</li>`).join('')}
                </ul>
                <div class="price">${laptop.price.toLocaleString('tr-TR')} TL</div>
                ${stokYok ? '<div class="stok-yok">Stokta Yok!</div>' :
                (stokVar ? `<div class="stok-var">${laptop.stock} Adet Kaldı</div>` : "")}
                <button class="buy-btn" 
                        onclick="event.stopPropagation();addToCart('${laptop.name.replace(/'/g, "\\'")}');" 
                        ${stokYok ? 'disabled' : ''}>
                    Sepete Ekle
                </button>
            </div>
        </div>
        `;
    });
}

function addToCart(name) {
    let idx = laptops.findIndex(l => l.name === name);
    if (idx === -1) return;
    let item = laptops[idx];
    let stokYok = item.stock === 0;
    let sinirsiz = item.stock === undefined || item.stock === null;
    if (!stokYok) {
        if (!sinirsiz && typeof item.stock === 'number' && item.stock > 0)
            laptops[idx].stock -= 1;
        let cartIdx = cart.findIndex(ci => ci.name === item.name);
        if (cartIdx !== -1) {
            cart[cartIdx].quantity = (cart[cartIdx].quantity || 1) + 1;
        } else {
            cart.push({ ...item, quantity: 1 });
        }
        renderProducts(currentList);
        updateCartCount();
    }
}

function openCartPage() {
    localStorage.setItem('cart', JSON.stringify(cart));
    window.location.href = '/Home/Sepet';
}

function updateCartCount() {
    const cartCountElement = document.getElementById('cart-count');
    if (cartCountElement) {
        let toplamAdet = cart.reduce((sum, item) => sum + (item.quantity || 1), 0);
        cartCountElement.textContent = toplamAdet;
    }
}

// ====== ÜRÜN DETAY MODALI ======
function showModal(name) {
    let laptop = laptops.find(l => l.name === name);
    if (!laptop) return;
    let stokYok = laptop.stock === 0;
    let stokVar = typeof laptop.stock === "number" && laptop.stock > 0 && laptop.stock <= 3;
    if (document.getElementById('modal-img'))
        document.getElementById('modal-img').src = '/images/' + laptop.img;
    if (document.getElementById('modal-title'))
        document.getElementById('modal-title').innerText = laptop.name;
    if (document.getElementById('modal-specs'))
        document.getElementById('modal-specs').innerHTML = laptop.specs.map(s => `<li>${s}</li>`).join('');
    if (document.getElementById('modal-price'))
        document.getElementById('modal-price').innerHTML = `
            <div class="price">${laptop.price.toLocaleString('tr-TR')} TL</div>
            ${stokYok ? '<div class="stok-yok">Stokta Yok!</div>' :
                (stokVar ? `<div class="stok-var">${laptop.stock} Adet Kaldı</div>` : "")}`;
    if (document.getElementById('modal-addcart'))
        document.getElementById('modal-addcart').disabled = stokYok;
    if (document.getElementById('modal-bg'))
        document.getElementById('modal-bg').style.display = "flex";
    if (document.getElementById('modal-addcart'))
        document.getElementById('modal-addcart').onclick = function () {
            addToCart(laptop.name);
            closeModal();
        }
}
function closeModal() {
    if (document.getElementById('modal-bg'))
        document.getElementById('modal-bg').style.display = "none";
}

// ====== MENÜ ve ARAMA ======
function showSection(section, event) {
    if (event) event.preventDefault();
    let title = "Laptop Modelleri";
    let list = laptops;
    if (section == "kategori") {
        title = "Kategoriler";
        list = [];
        kategoriler.forEach(kat => list.push(...laptops.filter(x => x.kategori == kat)));
    }
    if (section == "kampanya") {
        title = "Kampanyalar"; list = kampanyaUrunler;
    }
    if (section == "marka") {
        title = "Markalarımız";
        list = []; brands.forEach(brand => list.push(...laptops.filter(x => x.brand == brand)));
    }
    if (section == "firsat") {
        title = "Fırsat Köşesi"; list = firsatUrunler;
    }
    if (document.getElementById('main-title'))
        document.getElementById('main-title').innerText = title;
    renderProducts(list);
}

function searchProducts() {
    const searchBox = document.getElementById('searchBox');
    if (!searchBox) return;
    const query = searchBox.value.trim().toLowerCase();
    if (!query) { renderProducts(laptops); return; }
    let results = laptops.filter(l =>
        l.name.toLowerCase().includes(query) ||
        (l.desc && l.desc.toLowerCase().includes(query)) ||
        (l.brand && l.brand.toLowerCase().includes(query)) ||
        (l.kategori && l.kategori.toLowerCase().includes(query))
    );
    if (document.getElementById('main-title'))
        document.getElementById('main-title').innerText = "Arama Sonuçları";
    renderProducts(results);
}

function showMain() {
    if (document.getElementById('main-title'))
        document.getElementById('main-title').innerText = "Laptop Modelleri";
    renderProducts(laptops);
    updateCartCount();
}

if (document.getElementById('products')) {
    showMain();
    updateCartCount();
}

// ====== SEPET SAYFASI ======
if (window.location.pathname.toLowerCase().includes('/home/sepet')) {
    const cartFromStorage = JSON.parse(localStorage.getItem('cart') || "[]");
    const cartItemsContainer = document.getElementById('cart-items');
    let total = 0;
    if (cartItemsContainer) {
        cartItemsContainer.innerHTML = '';
        if (cartFromStorage.length === 0) {
            cartItemsContainer.innerHTML = '<p>Sepetinizde ürün bulunmamaktadır.</p>';
        } else {
            cartFromStorage.forEach(item => {
                const itemTotal = item.price * (item.quantity || 1);
                total += itemTotal;
                cartItemsContainer.innerHTML += `
                    <div class="cart-item sepet-item" data-id="${item.id}">
                        <div class="cart-item-info">
                            <h3>${item.name}</h3>
                            <p>Adet: <input type="number" class="urun-adet" 
                                    value="${item.quantity || 1}" min="1" /></p>
                        </div>
                        <div class="cart-item-price">${itemTotal.toLocaleString('tr-TR')} TL</div>
                    </div>
                `;
            });
        }
        const cartTotalElement = document.getElementById('cart-total');
        if (cartTotalElement)
            cartTotalElement.textContent = `${total.toLocaleString('tr-TR')} TL`;
    }
}

// ====== SİPARİŞ YÖNETİMİ ======
function completeOrder() {
    // Önce Sepet sayfası mı kontrol et
    let payload = [];
    const sepetSatirlari = document.querySelectorAll('.sepet-item');
    if (sepetSatirlari.length > 0) {
        sepetSatirlari.forEach(el => {
            const id = Number(el.dataset.id);
            const adetInput = el.querySelector('.urun-adet');
            const adet = parseInt(adetInput && adetInput.value, 10) || 1;
            payload.push({ UrunId: id, Adet: adet });
        });
    }
    // Eğer sayfa sepet değilse cart dizisinden al
    if (payload.length === 0 && Array.isArray(cart)) {
        payload = cart.map(item => ({
            UrunId: item.id,
            Adet: item.quantity || 1
        }));
    }
    if (!payload.length) {
        alert('Sepetiniz boş. Lütfen sepete ürün ekleyin.');
        return;
    }

    fetch('/Order/CompleteOrder', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify(payload)
    })
        .then(async (res) => {
            if (res.status === 401) { window.location.href = '/Account/Login'; return null; }
            return await res.json();
        })
        .then((data) => {
            if (!data) return;
            if (data.success) {
                const orderCompleteModal = document.getElementById('order-complete-modal');
                const orderProductsList = document.getElementById('order-products-list');
                if (orderCompleteModal && orderProductsList) {
                    orderProductsList.innerHTML = '';
                    cart.forEach(item => {
                        const li = document.createElement('li');
                        li.textContent = `${item.name} (x${item.quantity || 1})`;
                        orderProductsList.appendChild(li);
                    });
                    orderCompleteModal.style.display = 'flex';
                } else {
                    alert(data.message || 'Sipariş başarıyla oluşturuldu!');
                }
                cart = [];
                localStorage.removeItem('cart');
                updateCartCount();
            } else {
                alert(data.message || "Sipariş tamamlanırken hata oluştu.");
            }
        })
        .catch(err => {
            console.error(err);
            alert("Sunucu hatası: Sipariş tamamlanamadı.");
        });
}

// ====== PROFİL MENÜSÜ ======
function updateProfileMenu() {
    const user = localStorage.getItem('loggedInUser');
    if (user) {
        if (document.getElementById('menu-auth'))
            document.getElementById('menu-auth').style.display = "none";
        if (document.getElementById('menu-user'))
            document.getElementById('menu-user').style.display = "block";
    } else {
        if (document.getElementById('menu-auth'))
            document.getElementById('menu-auth').style.display = "block";
        if (document.getElementById('menu-user'))
            document.getElementById('menu-user').style.display = "none";
    }
}

window.userLogin = function () { window.location.href = '/Account/Login'; }
window.userRegister = function () { window.location.href = '/Account/Register'; }
window.userInfo = function () { window.location.href = '/Account/Profile'; }
window.userLogout = function () {
    localStorage.removeItem('loggedInUser');
    updateProfileMenu();
    window.location.href = '/Account/Logout';
}

document.addEventListener('DOMContentLoaded', function () {
    updateProfileMenu();
    const btn = document.getElementById('completeOrderBtn');
    if (btn) btn.addEventListener('click', completeOrder);
});
