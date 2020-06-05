<?php
include "db.php";

$username=$_GET["username"];
$password=$_GET["password"];

$result = mysqli_query($con,"SELECT username from admin WHERE username='".$username."'");
$num= mysqli_num_rows($result);

if((empty($username)))
{
	echo "Please fill out username filed";
}
if((empty($password)))
{
	echo "Please fill out password filed";
}

elseif($num==1)
{
	die ("Username already exist");
}

else{
$sql="INSERT INTO admin(username,password) VALUES('".$username."','".$password."')";

$result_1 = mysqli_query($con,$sql);

if($result_1)
{
	echo "".$username." Registration succesfull";
}

else 
	die ("Registration failed");
}
?>