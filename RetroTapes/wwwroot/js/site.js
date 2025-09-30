// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
//
document.addEventListener("DOMContentLoaded", () => {
  console.log("Hello World")
  updateStaff();
})

function updateStaff() {

  const selected = document.getElementsByClassName("staff");

  let value = localStorage.getItem("user");
  if (value === null) {
    value = "1";
    localStorage.setItem("user", value)
  }
  console.log("Setting value to " + value)
  for (const select of selected) {
    select.value = value;
  }
}
