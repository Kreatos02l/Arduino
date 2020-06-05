<?php

$server="localhost";
$serveruser="root";
$serverpassword="123456";
$db="admininfo";

$con=new mysqli($server,$serveruser,$serverpassword,$db);

if(mysqli_connect_errno())
{
	die("connection failed!");
}

?>