document.getElementById('registerForm').addEventListener('submit', function(e) {
    e.preventDefault(); // 阻止表单默认提交

    let username = document.getElementById('username').value;
    let phonenumber = document.getElementById('phonenumber').value;
    let password = document.getElementById('pwd').value;
    let confirmPassword = document.getElementById('c_pwd').value;
    let agree = document.querySelector('.checkbox').checked;

    if (!agree) {
        alert("请先阅读并同意《用户注册协议》！");
        return;
    }
    if (password !== confirmPassword) {
        alert("两次密码输入不一样！");
        return;
    }

    fetch('http://121.40.112.220:8086/api/Auth/register', {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json'
      },
      body: JSON.stringify({ username, phonenumber, password})
    })
    .then(response => {
      if (!response.ok) {
        throw new Error('网络响应错误');
      }
      return response.json();  // 解析返回的 JSON 数据
    })
    .then(data => {
      if(data.Message=="注册成功"){
        alert('注册成功！');
        window.location.href = "LogIn.html";
      }
    })
    .catch(error => {
      console.error('发生错误：', error);
      alert('注册失败，请重试');
    });
  });