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
			
	$sql = "SELECT friends.FriendID, statues.Statue, statues.Username statusUsername, users.Username User, users.IconeID
	FROM friends, statues, users
	WHERE friends.PlayerID = '$player'";
	
	$query = mysqli_query($conn, $sql);
	
	if(mysqli_num_rows($query) > 0)
	{
		while ($row = mysqli_fetch_assoc($query))
		{
			if($row["statusUsername"] == $row["FriendID"])
			{
				if($row["User"] == $row["FriendID"])
				{
					echo($row["User"]);
					echo("//");
					echo($row["Statue"]);
					echo("--");
					echo($row["IconeID"]);
					echo("||");
				}
			}
		}
	}
	else
	{
		echo("No friend");
	}

	$conn->close();

?>