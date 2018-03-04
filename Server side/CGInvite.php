<?php

	
	$dbContainer = file_get_contents('http://skillfight.gear.host/sqlContainer.txt');
	$SQLvalues = explode(" ", $dbContainer);
	
	$servername = $SQLvalues[0];
	$username = $SQLvalues[1];
	$password = $SQLvalues[2];
	$dbname = $SQLvalues[3];
	
	
	$player = $_POST["PlayerPost"];
	$friend = $_POST["FriendPost"];
	$ip = $_POST["ipPost"];
	
	
	$conn = new mysqli($servername, $username, $password, $dbname);
	
	if ($conn->connect_error) 
	{
		die("Connection failed: " . $conn->connect_error);
	}
	
	$sql = "SELECT ID FROM Users WHERE Username = '$friend'";
	$query = mysqli_query($conn, $sql);
	
	if (mysqli_num_rows($query) > 0)
	{
		$sql = "SELECT Type, FromPlayer, ToPlayer FROM request WHERE FromPlayer = '$player' AND ToPlayer = '$friend'";
		$query = mysqli_query($conn, $sql);
		if (mysqli_num_rows($query) > 0)
		{
			die("Player already invited");
		}
		
		$sql = "INSERT INTO request (Type, FromPlayer, ToPlayer, Info)
		VALUES ('CustomGame', '$player', '$friend', '$ip')";
		$query = mysqli_query($conn, $sql);
	}
	else
	{
		echo("unknow player//");
		echo($friend);
	}
			
	$conn->close();

?>