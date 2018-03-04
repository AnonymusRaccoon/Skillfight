<?php

	
	$dbContainer = file_get_contents('http://skillfight.gear.host/sqlContainer.txt');
	$SQLvalues = explode(" ", $dbContainer);
	
	$servername = $SQLvalues[0];
	$username = $SQLvalues[1];
	$password = $SQLvalues[2];
	$dbname = $SQLvalues[3];
	
	
	$player = $_POST["PlayerPost"];
	$friend = $_POST["FriendPost"];
	$partyID = $_POST["PartyIDPost"];
	
	$conn = new mysqli($servername, $username, $password, $dbname);
	
	if ($conn->connect_error) 
	{
		die("Connection failed: " . $conn->connect_error);
	}
	$sql = "SELECT ID FROM Users WHERE Username = '$friend'";
	$query = mysqli_query($conn, $sql);
	
	if (mysqli_num_rows($query) > 0)
	{	
		$sql = "INSERT INTO Request (Type, FromPlayer, ToPlayer, Info)
		VALUES ('Group', '$player', '$friend', '$partyID')";
		$query = mysqli_query($conn, $sql);
	}
	else
	{
		echo("unknow player");
	}
		

	$conn->close();

?>