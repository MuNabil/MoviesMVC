var dataTable;
var connectionMember = new signalR.HubConnectionBuilder()
  .withUrl("/hubs/member")
  .build();

connectionMember.on(
  "MemberDeleted", (memberId) => {
    const element = document.getElementById(memberId);
    element.remove();
  }
);

connectionMember.on(
  "NewMemberAdded",
  (memberImage, memberName, dateOfBirth, memberRole, memberId) => {
    AddRowIntoTable("tblMembers", memberImage, memberName, dateOfBirth, memberRole, memberId);
  }
);

connectionMember.start();

function AddRowIntoTable(tableId, memberImage, memberName, dateOfBirth, memberRole, memberId) {
  var table = document.getElementById(tableId);
  var row = table.insertRow(1);
  var cell1 = row.insertCell(0);
  var cell2 = row.insertCell(1);
  var cell3 = row.insertCell(2);
  var cell4 = row.insertCell(3);
  var cell5 = row.insertCell(4);
  cell1.innerHTML = `<img src="/Images/${memberImage.toString()}" class="rounded-circle" style="width:40px">`;
  cell2.innerHTML = memberName;
  cell3.innerHTML = dateOfBirth;
  cell4.innerHTML = memberRole;
  cell5.innerHTML = `
  <a href="/Members/Detail?id=${memberId}" class="btn btn-sec"><i class="fa fa-eye"></i></a>
  <a href="/Members/Edit?id=${memberId}" class="btn btn-default"><i class="fa fa-edit"></i></a>
  <a onclick="return window.confirm('Are you sure?')" href="/Members/Delete?id=${memberId}" class="btn btn-danger"><i class="fa fa-trash"></i></a>
  `;
}