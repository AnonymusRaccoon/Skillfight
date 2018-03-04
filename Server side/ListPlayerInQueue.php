<?php


	$dbContainer = file_get_contents('http://skillfight.gear.host/sqlContainer.txt');
	$SQLvalues = explode(" ", $dbContainer);
	
	$servername = $SQLvalues[0];
	$username = $SQLvalues[1];
	$password = $SQLvalues[2];
	$dbname = $SQLvalues[3];
	
	
	$player = $_POST["PlayerPost"];
	$GroupGame = $_POST["GroupGamePost"];
	
	$conn = new mysqli($servername, $username, $password, $dbname);
	
	if ($conn->connect_error) 
	{
		die("Connection failed: " . $conn->connect_error);
	}
	
	$sql = "SELECT * FROM Game WHERE player1 = '$player' OR player2 = '$player' OR player3 = '$player' OR player4 = '$player' OR player5 = '$player' OR player6 = '$player' OR player7 = '$player' OR player8 = '$player'";
	$query = mysqli_query($conn, $sql);
	
	if (mysqli_num_rows($query) > 0)
	{
		while ($row = mysqli_fetch_assoc($query))
		{
			$player1 = $row["player1"];
			$player2 = $row["player2"];
			$player3 = $row["player3"];
			$player4 = $row["player4"];
			$player5 = $row["player5"];
			$player6 = $row["player6"];
			$player7 = $row["player7"];
			$player8 = $row["player8"];
			break;
		}
		
		echo ($player1);
		echo ("/1/");
		echo ($player2);
		echo ("/2/");
		echo ($player3);
		echo ("/3/");
		echo ($player4);
		echo ("/4/");
		echo ($player5);
		echo ("/5/");
		echo ($player6);
		echo ("/6/");
		echo ($player7);
		echo ("/7/");
		echo ($player8);
	}
	else
	{
		$sql = "SELECT * FROM Queue WHERE GroupGame = '$GroupGame' OR GroupGame = 'Random'";
		$query = mysqli_query($conn, $sql);
		
		$i = 0;
		while ($row = mysqli_fetch_assoc($query))
		{
			$i += $row["GroupLenght"];
		}
		echo ($i);
	}
	
	
	$conn->close();
	
?>