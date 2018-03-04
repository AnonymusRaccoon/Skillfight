<?php

	$dbContainer = file_get_contents('http://skillfight.gear.host/sqlContainer.txt');
	$SQLvalues = explode(" ", $dbContainer);
	
	$servername = $SQLvalues[0];
	$username = $SQLvalues[1];
	$password = $SQLvalues[2];
	$dbname = $SQLvalues[3];

	
	$player = $_POST["PlayerPost"];
	
	$conn = new mysqli($servername, $username, $password, $dbname);
	
	if ($conn->connect_error) 
	{
		die("Connection failed: " . $conn->connect_error);
	}
	
	$sql = "SELECT Accept, Deny FROM Game WHERE Player1 = '$player'";
	$query = mysql_num_rows($sql);
	
	while ($row = mysqli_fetch_assoc($query))
	{
		echo($row["Accept"]);
		echo("//");
		echo($row["Deny"]);
		break;
	}
	
	$conn->close();
?>