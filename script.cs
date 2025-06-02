// Example products - you can update/expand this list!
const products = [
  {
    id: 1,
    name: "LED Bulb",
    price: 75,
    img: "https://img.icons8.com/color/96/000000/light-on.png"
  },
  {
    id: 2,
    name: "Extension Board",
    price: 250,
    img: "https://img.icons8.com/color/96/000000/electrical.png"
  },
  {
    id: 3,
    name: "Electric Kettle",
    price: 900,
    img: "https://img.icons8.com/color/96/000000/electric-kettle.png"
  },
  {
    id: 4,
    name: "Iron",
    price: 1100,
    img: "https://img.icons8.com/color/96/000000/iron.png"
  }
];

const productList = document.getElementById('product-list');
const cartBtn = document.getElementById('cart-btn');
const cartModal = document.getElementById('cart-modal');
const closeCart = document.getElementById('close-cart');
const cartItems = document.getElementById('cart-items');
const cartTotal = document.getElementById('cart-total');
const cartCount = document.getElementById('cart-count');
const checkoutBtn = document.getElementById('checkout-btn');

let cart = [];

// Render products
function renderProducts() {
  productList.innerHTML = '';
  products.forEach(product => {
    const div = document.createElement('div');
    div.className = 'product-card';
    div.innerHTML = `
      <img src="${product.img}" alt="${product.name}">
      <h3>${product.name}</h3>
      <p>₹${product.price}</p>
      <button data-id="${product.id}">Add to Cart</button>
    `;
    productList.appendChild(div);
  });
  // Add event listeners
  document.querySelectorAll('.product-card button').forEach(btn => {
    btn.addEventListener('click', addToCart);
  });
}

// Add product to cart
function addToCart(e) {
  const id = +e.target.getAttribute('data-id');
  const item = cart.find(i => i.id === id);
  if (item) {
    item.qty += 1;
  } else {
    const prod = products.find(p => p.id === id);
    cart.push({ ...prod, qty: 1 });
  }
  updateCartCount();
}

// Update cart count in header
function updateCartCount() {
  cartCount.textContent = cart.reduce((sum, item) => sum + item.qty, 0);
}

// Show cart modal
cartBtn.onclick = () => {
  renderCart();
  cartModal.style.display = 'block';
};

// Hide cart modal
closeCart.onclick = () => {
  cartModal.style.display = 'none';
};

// Render cart items
function renderCart() {
  cartItems.innerHTML = '';
  if (cart.length === 0) {
    cartItems.innerHTML = '<li>Your cart is empty.</li>';
    cartTotal.textContent = '';
    checkoutBtn.style.display = 'none';
    return;
  }
  let total = 0;
  cart.forEach(item => {
    total += item.price * item.qty;
    const li = document.createElement('li');
    li.innerHTML = `
      ${item.name} x${item.qty} 
      <button data-id="${item.id}" class="remove-btn">🗑</button>
    `;
    cartItems.appendChild(li);
  });
  cartTotal.textContent = `Total: ₹${total}`;
  checkoutBtn.style.display = 'block';

  // Remove item from cart
  document.querySelectorAll('.remove-btn').forEach(btn => {
    btn.onclick = function() {
      const id = +this.getAttribute('data-id');
      cart = cart.filter(i => i.id !== id);
      updateCartCount();
      renderCart();
    };
  });
}

// Checkout button
checkoutBtn.onclick = () => {
  alert('Thank you for shopping with Kishore Electricals & Electronic!');
  cart = [];
  updateCartCount();
  renderCart();
  cartModal.style.display = 'none';
};

// Hide modal on click outside
window.onclick = function(event) {
  if (event.target == cartModal) {
    cartModal.style.display = 'none';
  }
};

renderProducts();
updateCartCount();
