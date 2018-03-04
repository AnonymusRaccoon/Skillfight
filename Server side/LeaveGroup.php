<?php

	
	$dbContainer = file_get_contents('http://skillfight.gear.host/sqlContainer.txt');
	$SQLvalues = explode(" ", $dbContainer);
	
	$servername = $SQLvalues[0];
	$username = $SQLvalues[1];
	$password = $SQLvalues[2];
	$dbname = $SQLvalues[3];
	
	
	$partyID = $_POST["PartyIDPost"];
	$player = $_POST["PlayerPost"];
	$playerNumber = $_POST["PlayerNumberPost"];
	
	$conn = new mysqli($servername, $username, $password, $dbname);
	
	if ($conn->connect_error) 
	{
		die("Connection failed: " . $conn->connect_error);
	}
	
	if ($playerNumber = 0)
		$sql = "DELETE FROM PartyGroup WHERE ID = '$partyID'";
	
	if ($playerNumber = 1)
		$sql = "UPDATE PartyGroup SET Player2 = 'null' WHERE ID = '$partyID'";
	if ($playerNumber = 2)
		$sql = "UPDATE PartyGroup SET Player3 = 'null' WHERE ID = '$partyID'";
	if ($playerNumber = 3)
		$sql = "UPDATE PartyGroup SET Player4 = 'null' WHERE ID = '$partyID'";
	
	$query = mysqli_query($conn, $sql);

	$conn->close();

?>