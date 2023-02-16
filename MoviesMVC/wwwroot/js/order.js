var dataTable;
var connectionMovie = new signalR.HubConnectionBuilder()
  .withUrl("/hubs/order")
  .build();

connectionMovie.on(
  "NewOrderPlaced",
  (poster, title, username, email, orderDate, movieId, userId) => {
    AddOrderInTable("tblData1", poster, title, username, email, orderDate, movieId, userId);
    AddOrderInTable("tblData2", poster, title, username, email, orderDate, movieId, userId);
  }
);

connectionMovie.on(
  "OrderApproved",
  function (poster, title, releaseYear, genres, members) {
    // var img = new Image();
    // img.src = `~/Images/${poster}`;
    // img.style.width = '40px';
    // img.class = "rounded-circle";
    var table = document.getElementById("table3");
    var row = table.insertRow(1);
    var cell1 = row.insertCell(0);
    var cell2 = row.insertCell(1);
    var cell3 = row.insertCell(2);
    var cell4 = row.insertCell(3);
    var cell5 = row.insertCell(4);
    // cell1.appendChild(img);
    cell1.innerHTML = `<img src="/Images/${poster}" class="rounded-circle" style="width:40px">`;
    cell2.innerHTML = title;
    cell3.innerHTML = releaseYear;
    cell4.innerHTML = genres.join(', ');
    cell5.innerHTML = members.join(', ');
    toastr.success(`Your order for watch ${title} movie has been approved. \n you can watcha it now`,
      "Success", { positionClass: 'toast-bottom-right' });
  }
);

function fulfilled() {
  //do something on start
}
function rejected() {
  //rejected logs
}

connectionMovie.start().then(fulfilled, rejected);

function AddOrderInTable(tableId, poster, title, username, email, orderDate, movieId, userId) {
  var table = document.getElementById(tableId);
  var row = table.insertRow(1);
  var cell1 = row.insertCell(0);
  var cell2 = row.insertCell(1);
  var cell3 = row.insertCell(2);
  var cell4 = row.insertCell(3);
  var cell5 = row.insertCell(4);
  if (tableId == "tblData1") var cell6 = row.insertCell(5);
  cell1.innerHTML = `<img src="/Images/${poster.toString()}" class="rounded-circle" style="width:40px">`;
  cell2.innerHTML = title;
  cell3.innerHTML = username;
  cell4.innerHTML = email;
  cell5.innerHTML = new Date(orderDate);
  if (tableId == "tblData1") cell6.innerHTML = `<a href="/Orders/ApproveOrder?movieId=${movieId}&userId=${userId}" class="btn btn-default">Approve</a>`;
}