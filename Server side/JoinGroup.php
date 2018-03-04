<?php

	
	$dbContainer = file_get_contents('http://skillfight.gear.host/sqlContainer.txt');
	$SQLvalues = explode(" ", $dbContainer);
	
	$servername = $SQLvalues[0];
	$username = $SQLvalues[1];
	$password = $SQLvalues[2];
	$dbname = $SQLvalues[3];
	
	
	$player = $_POST["PlayerPost"];
	$partyID = $_POST["PartyIDPost"];
	
	
	$conn = new mysqli($servername, $username, $password, $dbname);
	
	if ($conn->connect_error) 
	{
		die("Connection failed: " . $conn->connect_error);
	}
	
	$sql = "SELECT * FROM PartyGroup WHERE ID = '$partyID'";
	$query = mysqli_query($conn, $sql);
	
	
	while($row = mysqli_fetch_assoc($query))
	{		
		if ($row["Player2"] = "null")
		{
			$sql = "UPDATE PartyGroup SET Player2 = '$player' WHERE ID = '$partyID'";
			break;
		}
		if ($row["Player3"] = "null")
		{
			$sql = "UPDATE PartyGroup SET Player3 = '$player' WHERE ID = '$partyID'";
			break;
		}
		if ($row["Player4"] = "null")
		{
			$sql = "UPDATE PartyGroup SET Player4 = '$player' WHERE ID = '$partyID'";
			break;
		}
	}

	$query = mysqli_query($conn, $sql);
	
	

	$conn->close();

?>