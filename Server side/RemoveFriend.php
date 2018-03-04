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
	
	$sql = "DELETE FROM Friends WHERE PlayerID = '$player' AND FriendID = '$friend'";
	$query = mysqli_query($conn, $sql);
	
	$sql = "DELETE FROM Friends WHERE FriendID = '$player' AND PlayerID = '$friend'";
	$query = mysqli_query($conn, $sql);
	
	
	$conn->close();

?>