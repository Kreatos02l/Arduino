<?php
include "db.php";

$name=$_GET["name"];
$surname=$_GET["surname"];
$score=$_GET["score"];
$id=$_GET["id"];
$age=$_GET["age"];
$type=$_GET["type"];

$result = mysqli_query($con,"SELECT id from patient_info WHERE id='".$id."'");
$num= mysqli_num_rows($result);

if((empty($id)))
{
	echo "Please fill out id filed";
}

elseif($num==1)
{
	die ("Id already exist");
}

else{
$sql="INSERT INTO patient_info(name,surname,score,id,age,type) VALUES('".$name."','".$surname."','".$score."','".$id."','".$age."','".$type."')";

$result_1 = mysqli_query($con,$sql);

if($result_1)
{
	echo "Patient ".$id." added";
}

else 
	die ("Patient not added");
}
?>