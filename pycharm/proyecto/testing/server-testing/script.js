responseArea = document.getElementById("response")

url = "http://127.0.0.1:8000"

getData = async (url) => {
  try {
    res = await fetch(url);
    data = await res.json();
    responseArea.innerHTML = JSON.stringify(data);
  } catch (error) {
    console.log(error.message);
  }
}

setInterval(() => {
  getData(url);
}, 500)
