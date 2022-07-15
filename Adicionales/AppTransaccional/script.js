console.log("busqueda");
// let lstProducts = fetch('https://localhost:44362/api/v1/HistoryQueryLog/GetTotalQueries')
//   .then(response => response.json())
//   .then(data => console.log(data));

async function postData(url = '', data = {}) {
  let param = JSON.stringify(data);
  const myHeaders = new Headers({
    'Content-Type': 'application/json',
    'Content-Length': param.length.toString()
  });
  const response = await fetch(url, {
    method:'POST',
    mode: 'no-cors',
    // cache: 'no-cache',
    credential: 'omit',
    headers: myHeaders,
    // redirect: 'follow',
    // referrerPolicy: 'no-referrer',
    body: JSON.stringify(data)
  });
  return response;
} 

postData('https://www.dataaccess.com/webservicesserver/NumberConversion.wso/NumberToWords', { "ubiNum": 10 })
  .then(data => {
    console.log('Success: ', data);
  })
  .catch((error) => {
    console.error('Error: ', error);
  });