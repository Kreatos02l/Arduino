<?php
include "db.php";


$score=$_GET["score"];
$id=$_GET["id"];


$result = mysqli_query($con,"SELECT id from patient_info WHERE id='".$id."'");
$num= mysqli_num_rows($result);

if($num==1)
{
$sql="UPDATE patient_info SET score = " .$score. " WHERE id='".$id."'";


$result_1 = mysqli_query($con,$sql);

	echo "0\t";
}

?>