<?php

include "db.php";


$id=$_GET["id"];

try
{
	$vt = new PDO("mysql:host=localhost;dbname=admininfo;charset=utf8","root","123456");
}
catch(PDOException $hata)
{
	echo $hata->getMessage();
}
	if(
	$_REQUEST['komut']=='veriCekme')
	{
		$almak=$vt->query("SELECT * FROM patient_info WHERE id='".$id."' ORDER BY score DESC limit 0,10");
		if($almak->rowCount())
		{
			foreach($almak as $row)
			{
				echo $row['name'].";";
				echo $row['surname'].";";
				echo $row['score'].";";
				echo $row['id'].";";
				echo $row['age'].";";
				echo $row['type'].";";
			}
		}

	}
?>