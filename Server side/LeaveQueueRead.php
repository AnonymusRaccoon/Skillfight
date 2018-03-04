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
	
	$sql = "SELECT * FROM Queue WHERE player1 = '$player' OR player2 = '$player' OR player3 = '$player' OR player4 = '$player'";
	$query = mysqli_query($conn, $sql);
	
	if (mysqli_num_rows($query) = 0)
	{
		echo ("Leave");
	}
	

	$conn->close();

?>