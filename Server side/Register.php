 <?php
 
	$dbContainer = file_get_contents('http://skillfight.gear.host/sqlContainer.txt');
	$SQLvalues = explode(" ", $dbContainer);
	
	$servername = $SQLvalues[0];
	$username = $SQLvalues[1];
	$password = $SQLvalues[2];
	$dbname = $SQLvalues[3];
	
	
	$playerUsername = $_POST["usernamePost"];
	$playerEmail = $_POST["eMailPost"];
	$playerPassword = $_POST["passwordPost"];
	$playerIconID = $_POST["iconIDPost"];
	
	
	$conn = new mysqli($servername, $username, $password, $dbname);
	
	if ($conn->connect_error) 
	{
		die("Connection failed: " . $conn->connect_error);
	}
	
	$sql = "INSERT INTO users (Username, eMail, Password, IconeID)
	VALUES ('".$playerUsername."', '".$playerEmail."', '".$playerPassword."', '".$playerIconID."')";
	
	if ($conn->query($sql) === TRUE) 
	{
		$sql = "SELECT Username, Id FROM Users WHERE Username = '".$playerUsername."'";
		$query = mysqli_query($conn, $sql);
		while ($row = mysqli_fetch_assoc($query))
		{
			if ($row["Username"] === $playerUsername)
			{
				$id = $row["Id"];
			}
		}
		echo "Successfully Register.||". $id;
		$sql = "INSERT INTO statues (Username, Statue)
		VALUES ('$playerUsername', 'Online')";
		$query = mysqli_query($conn, $sql);
	} 
	else 
	{
		echo "Error: " . $sql . "<br>" . $conn->error;
	}
	
	$conn->close();
?> 