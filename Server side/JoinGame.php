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
	
	$sql = "SELECT ip, port FROM Game WHERE player1 = '$player'";
	$query = mysqli_query($conn, $sql);
	
	while ($row = mysqli_fetch_assoc($query))
	{
		echo $row["ip"];
		echo ("//");
		echo $row["port"];
		break;
	}
		

	$conn->close();

?>