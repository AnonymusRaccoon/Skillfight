<?php

	$dbContainer = file_get_contents('http://skillfight.gear.host/sqlContainer.txt');
	$SQLvalues = explode(" ", $dbContainer);
	
	$servername = $SQLvalues[0];
	$username = $SQLvalues[1];
	$password = $SQLvalues[2];
	$dbname = $SQLvalues[3];

	$player = $_POST["usernamePost"];
	
	$conn = new mysqli($servername, $username, $password, $dbname);
	
	if ($conn->connect_error) 
	{
		die("Connection failed: " . $conn->connect_error);
	}
		
	$sql = "SELECT Player FROM leavemessage WHERE Player = '$player'";
	$query = mysqli_query($conn, $sql);
	
	if(mysqli_num_rows($query) > 0)
	{
		$sql = "DELETE FROM leavemessage WHERE Player = '$player'";
		$query = mysqli_query($conn, $sql);
		die("need leave");
	}
	else
	{
		echo("no need");
	}
	
	
	$conn->close();
?>