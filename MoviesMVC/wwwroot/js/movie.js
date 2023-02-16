var dataTable;
var connectionMovie = new signalR.HubConnectionBuilder()
  .withUrl("/hubs/movie")
  .build();

connectionMovie.on(
  "MovieDeleted", (movieId) => {
    const element = document.getElementById(movieId);
    element.remove();
  }
);

connectionMovie.on(
  "NewMovieAdded",
  (poster, title, releaseYear, movieId, genres, members) => {
    AddMovieInCard("moviesCard", poster, title, releaseYear, movieId, genres);
  }
);
connectionMovie.on(
  "NewMovieAdded",
  (poster, title, releaseYear, movieId, genres, members) => {
    AddMovieInTable("tblMovies", poster, title, releaseYear, movieId, genres, members);
  }
);

connectionMovie.start();

function AddMovieInTable(tableId, poster, title, releaseYear, movieId, genres, members) {
  var table = document.getElementById(tableId);
  var row = table.insertRow(1);
  var cell1 = row.insertCell(0);
  var cell2 = row.insertCell(1);
  var cell3 = row.insertCell(2);
  var cell4 = row.insertCell(3);
  var cell5 = row.insertCell(4);
  var cell6 = row.insertCell(5);
  cell1.innerHTML = `<img src="/Images/${poster.toString()}" class="rounded-circle" style="width:40px">`;
  cell2.innerHTML = title;
  cell3.innerHTML = releaseYear;
  cell4.innerHTML = genres.join(', ');
  cell5.innerHTML = members.join(', ');
  cell6.innerHTML = `
  <a href="/Movies/Detail?id=${movieId}" class="btn btn-sec"><i class="fa fa-eye"></i></a>
  <a href="/Movies/Edit?id=${movieId}" class="btn btn-default"><i class="fa fa-edit"></i></a>
  <a onclick="return window.confirm('Are you sure?')" href="/Movies/Delete?id=${movieId}" class="btn btn-danger"><i class="fa fa-trash"></i></a>
  `;
}


function AddMovieInCard(itemId, poster, title, releaseYear, movieId, genres) {
  var ele = document.getElementById(itemId);
  ele.insertAdjacentHTML("beforeend", `
  <div id="${movieId}" class="movie-card" onclick="window.location.href='/Movies/Detail?id=${movieId}'">
    <div class="movie-image">
        <img src="/Images/${poster}" style="width: 200px; object-fit: cover">
    </div>
    <div class="movie-info">
        <h4>${title}</h4>
        <span>${genres.join(', ')}</span>
        <h4>${releaseYear}</h4>
    </div>
  </div>
  `);
}