var dataTable;
var connectionGenre = new signalR.HubConnectionBuilder()
  .withUrl("/hubs/genre")
  .build();

connectionGenre.on(
  "GenreDeleted", (genreId) => {
    const element = document.getElementById('genre' + genreId);
    if (element) element.remove();
  }
);

connectionGenre.on(
  "NewGenreAdded",
  (genreName, genreId) => {
    AddRowIntoTable("tblGenres", genreName, genreId);
  }
);

connectionGenre.start();

function AddRowIntoTable(tableId, genreName, genreId) {
  var table = document.getElementById(tableId);
  var row = table.insertRow(1);
  var cell1 = row.insertCell(0);
  var cell2 = row.insertCell(1);
  cell1.innerHTML = genreName;
  cell2.innerHTML = `
    <a onclick="return window.confirm('Are you sure?')" 
    href="/Genres/Delete?id=${genreId}" class="btn btn-danger"><i class="fa fa-trash"></i></a>
  `;
}