-- MySqlBackup.NET 2.0.9.2
-- Dump Time: 2018-03-03 16:52:25
-- --------------------------------------
-- Server version 5.7.19-log MySQL Community Server (GPL)

-- 
-- Create schema skillfight
-- 

CREATE DATABASE IF NOT EXISTS `skillfight` /*!40100 DEFAULT CHARACTER SET utf8 */;
Use `skillfight`;



/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8 */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;


-- 
-- Definition of chat
-- 

DROP TABLE IF EXISTS `chat`;
CREATE TABLE IF NOT EXISTS `chat` (
  `ID` int(255) NOT NULL AUTO_INCREMENT,
  `FromPlayer` varchar(20) COLLATE utf8_unicode_ci NOT NULL,
  `ToPlayer` varchar(20) COLLATE utf8_unicode_ci NOT NULL,
  `Message` varchar(255) COLLATE utf8_unicode_ci NOT NULL,
  UNIQUE KEY `ID` (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

-- 
-- Dumping data for table chat
-- 

/*!40000 ALTER TABLE `chat` DISABLE KEYS */;

/*!40000 ALTER TABLE `chat` ENABLE KEYS */;

-- 
-- Definition of friends
-- 

DROP TABLE IF EXISTS `friends`;
CREATE TABLE IF NOT EXISTS `friends` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `PlayerID` varchar(20) COLLATE utf8_unicode_ci NOT NULL,
  `FriendID` varchar(20) COLLATE utf8_unicode_ci NOT NULL,
  UNIQUE KEY `ID` (`ID`)
) ENGINE=InnoDB AUTO_INCREMENT=53 DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

-- 
-- Dumping data for table friends
-- 

/*!40000 ALTER TABLE `friends` DISABLE KEYS */;
INSERT INTO `friends`(`ID`,`PlayerID`,`FriendID`) VALUES
(43,'Test','Gboy'),
(44,'Gboy','Test'),
(45,'Test','hethjerj'),
(46,'hethjerj','Test'),
(47,'Test','hethjerj'),
(48,'hethjerj','Test'),
(49,'SEIDOM','BIaisde'),
(50,'BIaisde','SEIDOM'),
(51,'SEIDOM','BIaisde'),
(52,'BIaisde','SEIDOM');
/*!40000 ALTER TABLE `friends` ENABLE KEYS */;

-- 
-- Definition of game
-- 

DROP TABLE IF EXISTS `game`;
CREATE TABLE IF NOT EXISTS `game` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `player1` int(11) NOT NULL,
  `player2` int(11) NOT NULL,
  `player3` int(11) NOT NULL,
  `player4` int(11) NOT NULL,
  `player5` int(11) NOT NULL,
  `player6` int(11) NOT NULL,
  `player7` int(11) NOT NULL,
  `player8` int(11) NOT NULL,
  `Accept` int(2) NOT NULL DEFAULT '0',
  `Deny` int(2) NOT NULL DEFAULT '0',
  UNIQUE KEY `ID` (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

-- 
-- Dumping data for table game
-- 

/*!40000 ALTER TABLE `game` DISABLE KEYS */;

/*!40000 ALTER TABLE `game` ENABLE KEYS */;

-- 
-- Definition of leavemessage
-- 

DROP TABLE IF EXISTS `leavemessage`;
CREATE TABLE IF NOT EXISTS `leavemessage` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `Player` varchar(20) NOT NULL,
  PRIMARY KEY (`ID`),
  UNIQUE KEY `ID_UNIQUE` (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- 
-- Dumping data for table leavemessage
-- 

/*!40000 ALTER TABLE `leavemessage` DISABLE KEYS */;

/*!40000 ALTER TABLE `leavemessage` ENABLE KEYS */;

-- 
-- Definition of partygroup
-- 

DROP TABLE IF EXISTS `partygroup`;
CREATE TABLE IF NOT EXISTS `partygroup` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `Player1` varchar(20) COLLATE utf8_unicode_ci DEFAULT 'null',
  `Player2` varchar(20) COLLATE utf8_unicode_ci DEFAULT 'null',
  `Player3` varchar(20) COLLATE utf8_unicode_ci DEFAULT 'null',
  `Player4` varchar(20) COLLATE utf8_unicode_ci DEFAULT 'null',
  UNIQUE KEY `ID` (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

-- 
-- Dumping data for table partygroup
-- 

/*!40000 ALTER TABLE `partygroup` DISABLE KEYS */;

/*!40000 ALTER TABLE `partygroup` ENABLE KEYS */;

-- 
-- Definition of queue
-- 

DROP TABLE IF EXISTS `queue`;
CREATE TABLE IF NOT EXISTS `queue` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `GroupGame` varchar(100) COLLATE utf8_unicode_ci NOT NULL,
  `GroupLenght` int(11) NOT NULL,
  `player1` varchar(20) COLLATE utf8_unicode_ci NOT NULL,
  `player2` varchar(20) COLLATE utf8_unicode_ci NOT NULL,
  `player3` varchar(20) COLLATE utf8_unicode_ci NOT NULL,
  `player4` varchar(20) COLLATE utf8_unicode_ci NOT NULL,
  UNIQUE KEY `ID` (`ID`)
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

-- 
-- Dumping data for table queue
-- 

/*!40000 ALTER TABLE `queue` DISABLE KEYS */;
INSERT INTO `queue`(`ID`,`GroupGame`,`GroupLenght`,`player1`,`player2`,`player3`,`player4`) VALUES
(4,'Random',1,'SEIDOM','','','');
/*!40000 ALTER TABLE `queue` ENABLE KEYS */;

-- 
-- Definition of request
-- 

DROP TABLE IF EXISTS `request`;
CREATE TABLE IF NOT EXISTS `request` (
  `ID` int(255) NOT NULL AUTO_INCREMENT,
  `Type` varchar(11) COLLATE utf8_unicode_ci NOT NULL,
  `FromPlayer` varchar(20) COLLATE utf8_unicode_ci NOT NULL,
  `ToPlayer` varchar(20) COLLATE utf8_unicode_ci NOT NULL,
  `Info` varchar(20) COLLATE utf8_unicode_ci DEFAULT 'null',
  UNIQUE KEY `ID` (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

-- 
-- Dumping data for table request
-- 

/*!40000 ALTER TABLE `request` DISABLE KEYS */;

/*!40000 ALTER TABLE `request` ENABLE KEYS */;

-- 
-- Definition of statues
-- 

DROP TABLE IF EXISTS `statues`;
CREATE TABLE IF NOT EXISTS `statues` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `Username` varchar(20) DEFAULT NULL,
  `Statue` varchar(45) DEFAULT NULL,
  PRIMARY KEY (`ID`),
  UNIQUE KEY `ID_UNIQUE` (`ID`),
  UNIQUE KEY `Username_UNIQUE` (`Username`)
) ENGINE=InnoDB AUTO_INCREMENT=11 DEFAULT CHARSET=utf8;

-- 
-- Dumping data for table statues
-- 

/*!40000 ALTER TABLE `statues` DISABLE KEYS */;
INSERT INTO `statues`(`ID`,`Username`,`Statue`) VALUES
(2,'Gboy','Offline'),
(3,'Test','Offline'),
(5,'Test2','Offline'),
(6,'Diabltica','Offline'),
(7,'Test3','Offline'),
(8,'hethjerj','Offline'),
(9,'BIaisde','Offline'),
(10,'SEIDOM','Offline');
/*!40000 ALTER TABLE `statues` ENABLE KEYS */;

-- 
-- Definition of users
-- 

DROP TABLE IF EXISTS `users`;
CREATE TABLE IF NOT EXISTS `users` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `Username` varchar(10) COLLATE utf8_unicode_ci NOT NULL,
  `eMail` varchar(40) COLLATE utf8_unicode_ci NOT NULL,
  `Password` varchar(20) COLLATE utf8_unicode_ci NOT NULL,
  `IconeID` int(10) NOT NULL,
  UNIQUE KEY `ID` (`ID`),
  UNIQUE KEY `Username` (`Username`)
) ENGINE=InnoDB AUTO_INCREMENT=48 DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

-- 
-- Dumping data for table users
-- 

/*!40000 ALTER TABLE `users` DISABLE KEYS */;
INSERT INTO `users`(`ID`,`Username`,`eMail`,`Password`,`IconeID`) VALUES
(40,'Gboy','Gboy9155@outlook.fr','motdepasse',0),
(41,'Test','test@test.fr','motdepasse',0),
(42,'Test2','test@test.fr','motdepasse',0),
(43,'Diabltica','Diabltdfs@gqeqg.fra','moi44119',0),
(44,'Test3','test@test.fr','motdepasse',1),
(45,'hethjerj','hrze@gzrh.fr','motdepasse',2),
(46,'BIaisde','lulu11012002@gmail.com','karate11',0),
(47,'SEIDOM','paulvassardyt@gmail.com','paulvassard44',0);
/*!40000 ALTER TABLE `users` ENABLE KEYS */;


/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;


-- Dump completed on 2018-03-03 16:52:26
-- Total time: 0:0:0:0:796 (d:h:m:s:ms)
