<?php
include "db.php";

$username = $_POST["username"];
$password = $_POST["password"];

$passcheckquery ="SELECT password,username FROM admin WHERE password='".$password."' and username='".$username."' ";
$passcheck = mysqli_query($con, $passcheckquery) or die ("Password check query failed");
  
  if(mysqli_num_rows($passcheck) != 1)
  {
	  echo "Wrong username or password!";
	  exit();
  }


  
  echo "0\t";

    
  



 
?>
