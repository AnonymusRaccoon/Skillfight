<?php

	
	$dbContainer = file_get_contents('http://skillfight.gear.host/sqlContainer.txt');
	$SQLvalues = explode(" ", $dbContainer);
	
	$servername = $SQLvalues[0];
	$username = $SQLvalues[1];
	$password = $SQLvalues[2];
	$dbname = $SQLvalues[3];
	
	
	$player = $_POST["PlayerPost"];
	$friend = $_POST["FriendPost"];
	
	$conn = new mysqli($servername, $username, $password, $dbname);
	
	if ($conn->connect_error) 
	{
		die("Connection failed: " . $conn->connect_error);
	}
	
	$sql = "INSERT INTO Friends (PlayerID, FriendID)
	VALUES ('$player', '$friend')";
	$query = mysqli_query($conn, $sql);
	
	$sql = "INSERT INTO Friends (PlayerID, FriendID)
	VALUES ('$friend', '$player')";
	$query = mysqli_query($conn, $sql);

	$conn->close();

?>