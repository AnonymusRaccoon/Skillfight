<?php

	
	$dbContainer = file_get_contents('http://skillfight.gear.host/sqlContainer.txt');
	$SQLvalues = explode(" ", $dbContainer);
	
	$servername = $SQLvalues[0];
	$username = $SQLvalues[1];
	$password = $SQLvalues[2];
	$dbname = $SQLvalues[3];

	
	$GroupGame = $_POST["GroupGamePost"];
	$GroupLenght = $_POST["GroupLenghtPost"];
	
	$players[];
	
	$conn = new mysqli($servername, $username, $password, $dbname);
	
	if ($conn->connect_error) 
	{
		die("Connection failed: " . $conn->connect_error);
	}
	
	$gameLenght = $GroupLenght;
	
	$sql = "SELECT * FROM Queue WHERE GroupGame = '$GroupGame' OR GroupGame = 'Random'";
	$query = mysqli_query($conn, $sql);
	
	while ($row = mysqli_fetch_assoc($query))
	{
		if ($gameLenght + $row["GroupLenght"] < 9)
		{
			
			$gameLenght += $row["GroupLenght"];
			
			array_push($players, $row["player1"]);
			
			if ($row["player2"] != "")
				array_push($players, $row["player2"]);
			
			if ($row["player3"] != "")
				array_push($players, $row["player3"]);
			
			if ($row["player4"] != "")
				array_push($players, $row["player4"]);
		}
	}
	
	while ($players.Lenght < 8)
	{
		array_push($players, "null");
	}
	
	$sql = "INSERT INTO Game (player1, player2, player3, player4, player6, player7, player8)
	VALUES ('$players[0]', '$players[1]', '$players[2]', '$players[3]', '$players[4]', '$players[5]', '$players[6]', '$players[7]')";
	$query = mysqli_query($conn, $sql);
		

	$conn->close();

?>