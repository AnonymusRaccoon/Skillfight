<?php

	$dbContainer = file_get_contents('http://skillfight.gear.host/sqlContainer.txt');
	$SQLvalues = explode(" ", $dbContainer);
	
	$servername = $SQLvalues[0];
	$username = $SQLvalues[1];
	$password = $SQLvalues[2];
	$dbname = $SQLvalues[3];

	$player_username = $_POST["usernamePost"];
	$statue = $_POST["StatuePost"];
	
	$conn = new mysqli($servername, $username, $password, $dbname);
	
	if ($conn->connect_error) 
	{
		die("Connection failed: " . $conn->connect_error);
	}
	
	$sql = "UPDATE statues SET Statue = '$statue' WHERE Username = '$player_username'";
	$query = mysqli_query($conn, $sql);
	
	echo("done");
	
	$conn->close();
?>