<?php

	$dbContainer = file_get_contents('http://skillfight.gear.host/sqlContainer.txt');
	$SQLvalues = explode(" ", $dbContainer);
	
	$servername = $SQLvalues[0];
	$username = $SQLvalues[1];
	$password = $SQLvalues[2];
	$dbname = $SQLvalues[3];

	$player_username = $_POST["usernamePost"];
	$player_password = $_POST["passwordPost"];
	
	$conn = new mysqli($servername, $username, $password, $dbname);
	
	if ($conn->connect_error) 
	{
		die("Connection failed: " . $conn->connect_error);
	}
	
	$sql = "SELECT users.Password, users.ID, users.IconeID,statues.Statue, statues.Username
	FROM users, statues
	WHERE users.Username = '$player_username'";
	
	$query = mysqli_query($conn, $sql);
	
	if($query->num_rows > 0)
	{
		while ($row = mysqli_fetch_assoc($query))
		{
			if($row["Username"] === $player_username)
			{
				if($row["Statue"] == "Offline")
				{
					if ($row["Password"] === $player_password)
					{
						die ("Login Successful.||".$row["ID"]."//".$row["IconeID"]);
					}
					else
						die ("Username and Password don't match");
				}
				else
				{
					$sql = "INSERT INTO leavemessage (Player)
					VALUES ('$player_username')";
					$query = mysqli_query($conn, $sql);
					echo("Player already connected.||".$row["ID"]."//".$row["IconeID"]);
				}
			}
		}
	}
	else
	{
		echo ("Invalid Username or Password");
	}
	
	$conn->close();
?>