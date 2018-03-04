<?php

	$dbContainer = file_get_contents('http://skillfight.gear.host/sqlContainer.txt');
	$SQLvalues = explode(" ", $dbContainer);
	
	$servername = $SQLvalues[0];
	$username = $SQLvalues[1];
	$password = $SQLvalues[2];
	$dbname = $SQLvalues[3];

	$ToPlayer = $_POST["ToPost"];
	$FromPlayer = $_POST["FromPost"];
	$Type = $_POST["TypePost"];
	
	$conn = new mysqli($servername, $username, $password, $dbname);
	
	if ($conn->connect_error) 
	{
		die("Connection failed: " . $conn->connect_error);
	}
	
	$sql = "DELETE FROM Request WHERE Type = '$Type' AND ToPlayer = '$ToPlayer' AND FromPlayer = '$FromPlayer'";
	$query = mysqli_query($conn, $sql);
	
	$conn->close();
?>