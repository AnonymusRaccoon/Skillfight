<?php

	
	$dbContainer = file_get_contents('http://skillfight.gear.host/sqlContainer.txt');
	$SQLvalues = explode(" ", $dbContainer);
	
	$servername = $SQLvalues[0];
	$username = $SQLvalues[1];
	$password = $SQLvalues[2];
	$dbname = $SQLvalues[3];
	
	
	$partyID = $_POST["PartyIDPost"];
	
	$conn = new mysqli($servername, $username, $password, $dbname);
	
	if ($conn->connect_error) 
	{
		die("Connection failed: " . $conn->connect_error);
	}
	
	$sql = "SELECT * FROM PartyGroup WHERE ID = '$partyID'";
	$query = mysqli_query($conn, $sql);
	
	if (mysqli_num_rows($query) > 0)
	{
		while ($row = mysqli_fetch_assoc($query))
		{
			echo($row["Player1"]);
			echo("//");
			if ($row["Player2"] != "null")
				echo($row["Player2"]);
			echo("//");
			if ($row["Player3"] != "null")
				echo($row["Player3"]);
			echo("//");
			if ($row["Player3"] != "null")
				echo($row["Player4"]);
			break;
		}
	}
	else
	{
		echo("Group removed");
	}

	$conn->close();

?>