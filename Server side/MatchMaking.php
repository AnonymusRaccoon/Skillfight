<?php


	$dbContainer = file_get_contents('http://skillfight.gear.host/sqlContainer.txt');
	$SQLvalues = explode(" ", $dbContainer);
	
	$servername = $SQLvalues[0];
	$username = $SQLvalues[1];
	$password = $SQLvalues[2];
	$dbname = $SQLvalues[3];

	
	$GroupGame = $_POST["GroupGamePost"];
	$GroupLenght = $_POST["GroupLenghtPost"];
	
	
	$player = [];
	
	
	
	$player1 = $_POST["Player1Post"];
	$player2 = $_POST["Player2Post"];
	$player3 = $_POST["Player3Post"];
	$player4 = $_POST["Player4Post"];
	
	array_push($player, $player1);
	
	if ($player2 != "")
		array_push($player, $player2);
	if ($player3 != "")
		array_push($player, $player3);
	if ($player4 != "")
		array_push($player, $player4);
	
	
	$conn = new mysqli($servername, $username, $password, $dbname);
	
	if ($conn->connect_error) 
	{
		die("Connection failed: " . $conn->connect_error);
	}
	
	$sql = "SELECT * FROM Queue WHERE GroupGame = '$GroupGame' or GroupGame = 'Random'";
	$query = mysqli_query($conn, $sql);
	
	$gameLenght = $GroupLenght;
	if ($i > 7)
	{
		$query = mysqli_query($conn, $sql);
		
		while ($row = mysqli_fetch_assoc($query))
		{
			if ($gameLenght + $row["GroupLenght"] < 9)
			{
				
				$gameLenght += $row["GroupLenght"];
				
				array_push($player, $row["player1"]);
				
				if ($row["player2"] != "")
					array_push($player, $row["player2"]);
				
				if ($row["player3"] != "")
					array_push($player, $row["player3"]);
				
				if ($row["player4"] != "")
					array_push($player, $row["player4"]);
			}
		}
		
		$sql = "INSERT INTO Game (player1, player2, player3, player4, player5, player6, player7, player8)
		VALUES ('$player[0]', '$player[1]', '$player[2]', '$player[3]', '$player[4]', '$player[5]', '$player[6], '$player[7]')";
		$query = mysqli_query($conn, $sql);
		
		$sql = "DELETE * FROM Queue WHERE player1 = '$player[0]' OR player1 = '$player[1]' OR player1 = '$player[2]' OR player1 = '$player[3]' OR player1 = '$player[4]' OR player1 = '$player[5]' OR player1 = '$player[6]' OR player1 = '$player[7]'";
		$query = mysqli_query($conn, $sql);
		
		echo ($player[0]);
		echo ("/1/");
		echo ($player[1]);
		echo ("/2/");
		echo ($player[2]);
		echo ("/3/");
		echo ($player[3]);
		echo ("/4/");
		echo ($player[4]);
		echo ("/5/");
		echo ($player[5]);
		echo ("/6/");
		echo ($player[6]);
		echo ("/7/");
		echo ($player[7]);
		
	}
	else
	{
		$sql = "INSERT INTO Queue (GroupGame, GroupLenght, player1, player2, player3, player4)
		VALUES ('$GroupGame', '$GroupLenght', '$player1', '$player2', '$player3', '$player4')";	
		$query = mysqli_query($conn, $sql);
		
		$i = $GroupLenght;
		while ($row = mysqli_fetch_assoc($query))
		{
			$i += $row["GroupLenght"];
		}
		die ($i);
	}

	
	$conn->close();

?>