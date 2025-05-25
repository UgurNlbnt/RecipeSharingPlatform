document.getElementById('registerForm').addEventListener('submit', async function (e) {
  e.preventDefault();

  const username = document.getElementById('username').value.trim();
  const email = document.getElementById('email').value.trim();
  const password = document.getElementById('password').value;

  const passwordRegex = /^(?=.*[a-z])(?=.*[A-Z])(?=.*\W).{12,}$/;
  if (!passwordRegex.test(password)) {
    document.getElementById('registerError').textContent = 'Şifre en az 12 karakter olmalı, büyük/küçük harf ve özel karakter içermeli.';
    return;
  }

  try {
    const response = await fetch('http://localhost:5186/api/account/register', {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json'
      },
      body: JSON.stringify({
        userName: username,
        email: email,
        password: password
      })
    });

    if (response.ok) {
      window.location.href = 'login.html';
    } else {
      const data = await response.json();
      document.getElementById('registerError').textContent = data.message || 'Kayıt sırasında hata oluştu.';
    }
  } catch (error) {
    document.getElementById('registerError').textContent = 'Sunucuya bağlanılamadı.';
  }
});
