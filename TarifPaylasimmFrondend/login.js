document.getElementById('loginForm').addEventListener('submit', async function (e) {
  e.preventDefault();

  const username = document.getElementById('username').value.trim();
  const password = document.getElementById('password').value;

  if (!username || !password) 
  {
    document.getElementById('loginError').textContent = 'Kullanıcı adı ve şifre zorunludur.';
    return;
  }

  try {
    const response = await fetch('http://localhost:5186/api/account/login', {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json'
      },
      body: JSON.stringify({
        userName: username,
        password: password
      })
    });

    if (response.ok) {
      const data = await response.json();
      localStorage.setItem('token', data.token);
      localStorage.setItem('username', data.userName);

      window.location.href = 'home.html';
    } 
    
    else 
    {
      document.getElementById('loginError').textContent = 'şifre veya kullanıcı adı hatalı';
    }
  } 
  
  catch (error) 
  {
    document.getElementById('loginError').textContent = 'Sunucuya bağlanılamadı.';
  }
});
