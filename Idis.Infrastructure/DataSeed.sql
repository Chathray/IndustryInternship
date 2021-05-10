-- MySQL dump 10.13  Distrib 8.0.24, for Win64 (x86_64)
--
-- Host: localhost    Database: tmainternship
-- ------------------------------------------------------
-- Server version	8.0.24

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!50503 SET NAMES utf8 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `activities`
--

DROP TABLE IF EXISTS `activities`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `activities` (
  `ActivityId` int NOT NULL AUTO_INCREMENT,
  `ActivityName` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci DEFAULT NULL,
  `ActivityDescription` varchar(200) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci DEFAULT NULL,
  `OtherDetails` varchar(500) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci DEFAULT NULL,
  `CreatedDate` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `UpdatedDate` timestamp NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  `CreatedBy` int DEFAULT NULL,
  PRIMARY KEY (`ActivityId`),
  KEY `FK_activites_users_by_idx` (`CreatedBy`),
  CONSTRAINT `FK_activites_users_by` FOREIGN KEY (`CreatedBy`) REFERENCES `users` (`UserId`) ON DELETE SET NULL ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `activities`
--

LOCK TABLES `activities` WRITE;
/*!40000 ALTER TABLE `activities` DISABLE KEYS */;
/*!40000 ALTER TABLE `activities` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `departments`
--

DROP TABLE IF EXISTS `departments`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `departments` (
  `DepartmentId` int NOT NULL AUTO_INCREMENT,
  `DepName` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
  `DepLocation` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
  `CreatedDate` timestamp NULL DEFAULT CURRENT_TIMESTAMP,
  `UpdatedDate` timestamp NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  `SharedTrainings` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci DEFAULT NULL,
  PRIMARY KEY (`DepartmentId`),
  UNIQUE KEY `Name_UNIQUE` (`DepName`)
) ENGINE=InnoDB AUTO_INCREMENT=6 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `departments`
--

LOCK TABLES `departments` WRITE;
/*!40000 ALTER TABLE `departments` DISABLE KEYS */;
INSERT INTO `departments` VALUES (1,'DC8-Ext','TMA Innovation Park 2','2021-04-01 01:37:49','2021-05-10 00:21:39',','),(2,'DC13-Ext','TMA Innovation Park 1','2021-04-01 01:37:49','2021-05-10 00:21:39',','),(3,'DC8','TMA Innovation Park','2021-04-01 01:37:49','2021-05-10 00:21:39',','),(4,'DC1','LAB 6D','2021-04-01 01:37:49','2021-05-10 00:21:39',','),(5,'DC2','LAB 8','2021-05-10 00:56:04','2021-05-10 00:56:13',NULL);
/*!40000 ALTER TABLE `departments` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `events`
--

DROP TABLE IF EXISTS `events`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `events` (
  `EventId` int NOT NULL AUTO_INCREMENT,
  `Title` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
  `Type` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
  `ClassName` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
  `Start` date NOT NULL,
  `End` date NOT NULL,
  `CreatedBy` int NOT NULL,
  `CreatedDate` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `UpdatedBy` int DEFAULT NULL,
  `UpdatedDate` timestamp NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  `RepeatField` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci DEFAULT NULL,
  `GestsField` json DEFAULT NULL,
  `EventLocationLabel` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci DEFAULT NULL,
  `EventDescriptionLabel` varchar(500) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci DEFAULT NULL,
  `Image` varchar(200) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci DEFAULT '/img/_event.svg',
  PRIMARY KEY (`EventId`),
  UNIQUE KEY `Title_UNIQUE` (`Title`),
  KEY `FK_Events_Type_idx` (`Type`),
  KEY `FK_Events_CreatedBy_idx` (`CreatedBy`),
  KEY `FK_Events_UpdatedBy_idx` (`UpdatedBy`),
  CONSTRAINT `FK_Events_CreatedBy` FOREIGN KEY (`CreatedBy`) REFERENCES `users` (`UserId`) ON UPDATE CASCADE,
  CONSTRAINT `FK_Events_EventTypes_Type` FOREIGN KEY (`Type`) REFERENCES `eventtypes` (`Type`),
  CONSTRAINT `FK_Events_UpdatedBy` FOREIGN KEY (`UpdatedBy`) REFERENCES `users` (`UserId`) ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `events`
--

LOCK TABLES `events` WRITE;
/*!40000 ALTER TABLE `events` DISABLE KEYS */;
/*!40000 ALTER TABLE `events` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `eventtypes`
--

DROP TABLE IF EXISTS `eventtypes`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `eventtypes` (
  `Type` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
  `ClassName` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
  `Color` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
  `CreatedDate` timestamp NULL DEFAULT CURRENT_TIMESTAMP,
  `UpdatedDate` timestamp NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  PRIMARY KEY (`Type`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `eventtypes`
--

LOCK TABLES `eventtypes` WRITE;
/*!40000 ALTER TABLE `eventtypes` DISABLE KEYS */;
INSERT INTO `eventtypes` VALUES ('Holidays','fullcalendar-custom-event-holidays','warning','2021-04-01 02:36:30','2021-04-01 02:36:30'),('Personal','fullcalendar-custom-event-hs-team','primary','2021-04-01 02:36:30','2021-04-01 02:36:30'),('Reminders','fullcalendar-custom-event-reminders','danger','2021-04-01 02:36:30','2021-04-01 02:36:30'),('Tasks','fullcalendar-custom-event-tasks','success','2021-04-01 02:36:30','2021-04-01 02:36:30');
/*!40000 ALTER TABLE `eventtypes` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `interns`
--

DROP TABLE IF EXISTS `interns`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `interns` (
  `InternId` int NOT NULL AUTO_INCREMENT,
  `Email` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
  `FirstName` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
  `LastName` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci DEFAULT NULL,
  `Gender` enum('Male','Female','Unspecified') CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `DateOfBirth` date NOT NULL,
  `Duration` varchar(23) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
  `TrainingId` int NOT NULL,
  `Type` enum('Full time','Part time') CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
  `MentorId` int DEFAULT NULL,
  `UpdatedBy` int DEFAULT NULL,
  `CreatedDate` timestamp NULL DEFAULT CURRENT_TIMESTAMP,
  `UpdatedDate` timestamp NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  `Avatar` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci DEFAULT '/img/intern.svg',
  `Phone` varchar(15) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci DEFAULT NULL,
  `OrganizationId` int DEFAULT NULL,
  `DepartmentId` int DEFAULT NULL,
  `IsDeleted` tinyint DEFAULT NULL,
  `Address1` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci DEFAULT NULL,
  `Address2` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci DEFAULT NULL,
  PRIMARY KEY (`InternId`),
  UNIQUE KEY `Email_UNIQUE` (`Email`),
  KEY `FK_Events_MentorId_idx` (`MentorId`),
  KEY `FK_Interns_UpdatedBy_idx` (`UpdatedBy`),
  KEY `FK_Interns_DepartmentId_idx` (`DepartmentId`),
  KEY `FK_Interns_OrganizationId_idx` (`OrganizationId`),
  KEY `PK_Interns_TrainingId_idx` (`TrainingId`),
  FULLTEXT KEY `FirstName` (`FirstName`,`LastName`,`Email`,`Phone`),
  CONSTRAINT `FK_Interns_Created` FOREIGN KEY (`MentorId`) REFERENCES `users` (`UserId`) ON UPDATE CASCADE,
  CONSTRAINT `FK_Interns_Department` FOREIGN KEY (`DepartmentId`) REFERENCES `departments` (`DepartmentId`) ON UPDATE CASCADE,
  CONSTRAINT `FK_Interns_Organization` FOREIGN KEY (`OrganizationId`) REFERENCES `organizations` (`OrganizationId`) ON UPDATE CASCADE,
  CONSTRAINT `FK_Interns_Updated` FOREIGN KEY (`UpdatedBy`) REFERENCES `users` (`UserId`) ON UPDATE CASCADE,
  CONSTRAINT `PK_Intern_Training` FOREIGN KEY (`TrainingId`) REFERENCES `trainings` (`TrainingId`) ON UPDATE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=93 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `interns`
--

LOCK TABLES `interns` WRITE;
/*!40000 ALTER TABLE `interns` DISABLE KEYS */;
INSERT INTO `interns` VALUES (2,'Lizbeth@73','Lizbeth','Dunkle','Female','1998-07-20','2021-05-16 - 2021-02-02',0,'Part time',4,NULL,'2021-05-10 00:55:40','2021-05-10 00:55:40','_intern.jpg','0943154555',3,1,NULL,NULL,NULL),(3,'Jarvis@72','Jarvis','Trostle','Male','1998-07-20','2021-05-16 - 2021-02-02',0,'Part time',2,NULL,'2021-05-10 00:55:40','2021-05-10 00:55:40','_intern.jpg','0943154555',2,1,NULL,NULL,NULL),(4,'Kendall@71','Kendall','Rae','Female','1998-07-20','2021-05-16 - 2021-02-02',0,'Part time',4,NULL,'2021-05-10 00:55:40','2021-05-10 00:55:40','_intern.jpg','0943154555',3,2,NULL,NULL,NULL),(5,'Lura@70','Lura','Trostle','Male','1998-07-20','2021-05-16 - 2021-02-02',0,'Part time',3,NULL,'2021-05-10 00:55:40','2021-05-10 00:55:40','_intern.jpg','0943154555',2,1,NULL,NULL,NULL),(6,'Alysa@69','Alysa','Kees','Female','1998-07-20','2021-05-16 - 2021-02-02',0,'Part time',4,NULL,'2021-05-10 00:55:40','2021-05-10 00:55:40','_intern.jpg','0943154555',2,4,NULL,NULL,NULL),(7,'Jamel@68','Jamel','Jensen','Male','1998-07-20','2021-05-16 - 2021-02-02',0,'Part time',2,NULL,'2021-05-10 00:55:40','2021-05-10 00:55:40','_intern.jpg','0943154555',1,4,NULL,NULL,NULL),(8,'Alysa@67','Alysa','Adcox','Female','1998-07-20','2021-05-16 - 2021-02-02',0,'Part time',1,NULL,'2021-05-10 00:55:40','2021-05-10 00:55:40','_intern.jpg','0943154555',1,4,NULL,NULL,NULL),(9,'Rolf@66','Rolf','Ringdahl','Male','1998-07-20','2021-05-16 - 2021-02-02',0,'Part time',4,NULL,'2021-05-10 00:55:40','2021-05-10 00:55:40','_intern.jpg','0943154555',2,1,NULL,NULL,NULL),(10,'Tamar@65','Tamar','Enriquez','Female','1998-07-20','2021-05-16 - 2021-02-02',0,'Part time',4,NULL,'2021-05-10 00:55:40','2021-05-10 00:55:40','_intern.jpg','0943154555',3,4,NULL,NULL,NULL),(11,'Kendall@64','Kendall','Kees','Male','1998-07-20','2021-05-16 - 2021-02-02',0,'Part time',4,NULL,'2021-05-10 00:55:40','2021-05-10 00:55:40','_intern.jpg','0943154555',1,3,NULL,NULL,NULL),(12,'Thanh@63','Thanh','Roberson','Female','1998-07-20','2021-05-16 - 2021-02-02',0,'Part time',2,NULL,'2021-05-10 00:55:40','2021-05-10 00:55:40','_intern.jpg','0943154555',3,1,NULL,NULL,NULL),(13,'Theo@62','Theo','Rae','Male','1998-07-20','2021-05-16 - 2021-02-02',0,'Part time',1,NULL,'2021-05-10 00:55:40','2021-05-10 00:55:40','_intern.jpg','0943154555',3,3,NULL,NULL,NULL),(14,'Quoc@61','Quoc','Trostle','Female','1998-07-20','2021-05-16 - 2021-02-02',0,'Part time',1,NULL,'2021-05-10 00:55:40','2021-05-10 00:55:40','_intern.jpg','0943154555',2,2,NULL,NULL,NULL),(15,'Thanh@60','Thanh','Jensen','Male','1998-07-20','2021-05-16 - 2021-02-02',0,'Part time',1,NULL,'2021-05-10 00:55:40','2021-05-10 00:55:40','_intern.jpg','0943154555',1,4,NULL,NULL,NULL),(16,'Reta@59','Reta','Kees','Female','1998-07-20','2021-05-16 - 2021-02-02',0,'Part time',2,NULL,'2021-05-10 00:55:40','2021-05-10 00:55:40','_intern.jpg','0943154555',2,1,NULL,NULL,NULL),(17,'Kendall@58','Kendall','Kees','Male','1998-07-20','2021-05-16 - 2021-02-02',0,'Part time',1,NULL,'2021-05-10 00:55:40','2021-05-10 00:55:40','_intern.jpg','0943154555',2,1,NULL,NULL,NULL),(18,'Rubi@57','Rubi','Adcox','Female','1998-07-20','2021-05-16 - 2021-02-02',0,'Part time',4,NULL,'2021-05-10 00:55:40','2021-05-10 00:55:40','_intern.jpg','0943154555',2,4,NULL,NULL,NULL),(19,'Lura@56','Lura','Eberhard','Male','1998-07-20','2021-05-16 - 2021-02-02',0,'Part time',3,NULL,'2021-05-10 00:55:40','2021-05-10 00:55:40','_intern.jpg','0943154555',2,2,NULL,NULL,NULL),(20,'Thanh@55','Thanh','Jensen','Female','1998-07-20','2021-05-16 - 2021-02-02',0,'Part time',1,NULL,'2021-05-10 00:55:40','2021-05-10 00:55:40','_intern.jpg','0943154555',3,4,NULL,NULL,NULL),(21,'Korey@54','Korey','Mong','Male','1998-07-20','2021-05-16 - 2021-02-02',0,'Part time',4,NULL,'2021-05-10 00:55:40','2021-05-10 00:55:40','_intern.jpg','0943154555',1,3,NULL,NULL,NULL),(22,'Kendall@53','Kendall','Kibler','Female','1998-07-20','2021-05-16 - 2021-02-02',0,'Part time',3,NULL,'2021-05-10 00:55:40','2021-05-10 00:55:40','_intern.jpg','0943154555',2,2,NULL,NULL,NULL),(23,'Viet@74','Viet','Jensen','Male','1998-07-20','2021-05-16 - 2021-02-02',0,'Part time',4,NULL,'2021-05-10 00:55:40','2021-05-10 00:55:40','_intern.jpg','0943154555',1,3,NULL,NULL,NULL),(24,'Quoc@52','Quoc','Trostle','Male','1998-07-20','2021-05-16 - 2021-02-02',0,'Part time',1,NULL,'2021-05-10 00:55:40','2021-05-10 00:55:40','_intern.jpg','0943154555',2,4,NULL,NULL,NULL),(25,'Viet@75','Viet','Turley','Female','1998-07-20','2021-05-16 - 2021-02-02',0,'Part time',4,NULL,'2021-05-10 00:55:40','2021-05-10 00:55:40','_intern.jpg','0943154555',2,4,NULL,NULL,NULL),(26,'Lura@77','Lura','Enriquez','Female','1998-07-20','2021-05-16 - 2021-02-02',0,'Part time',1,NULL,'2021-05-10 00:55:40','2021-05-10 00:55:40','_intern.jpg','0943154555',2,1,NULL,NULL,NULL),(27,'Theo@98','Theo','Dekker','Male','1998-07-20','2021-05-16 - 2021-02-02',0,'Part time',4,NULL,'2021-05-10 00:55:40','2021-05-10 00:55:40','_intern.jpg','0943154555',2,1,NULL,NULL,NULL),(28,'Lizbeth@97','Lizbeth','Diangelo','Female','1998-07-20','2021-05-16 - 2021-02-02',0,'Part time',1,NULL,'2021-05-10 00:55:40','2021-05-10 00:55:40','_intern.jpg','0943154555',2,1,NULL,NULL,NULL),(29,'Dorene@96','Dorene','Rae','Male','1998-07-20','2021-05-16 - 2021-02-02',0,'Part time',4,NULL,'2021-05-10 00:55:40','2021-05-10 00:55:40','_intern.jpg','0943154555',3,4,NULL,NULL,NULL),(30,'Lan@95','Lan','Eberhard','Female','1998-07-20','2021-05-16 - 2021-02-02',0,'Part time',3,NULL,'2021-05-10 00:55:40','2021-05-10 00:55:40','_intern.jpg','0943154555',1,4,NULL,NULL,NULL),(31,'Rolf@94','Rolf','Jenney','Male','1998-07-20','2021-05-16 - 2021-02-02',0,'Part time',3,NULL,'2021-05-10 00:55:40','2021-05-10 00:55:40','_intern.jpg','0943154555',1,2,NULL,NULL,NULL),(32,'Lizbeth@93','Lizbeth','Diangelo','Female','1998-07-20','2021-05-16 - 2021-02-02',0,'Part time',4,NULL,'2021-05-10 00:55:40','2021-05-10 00:55:40','_intern.jpg','0943154555',2,3,NULL,NULL,NULL),(33,'Jamel@92','Jamel','Enriquez','Male','1998-07-20','2021-05-16 - 2021-02-02',0,'Part time',4,NULL,'2021-05-10 00:55:40','2021-05-10 00:55:40','_intern.jpg','0943154555',3,3,NULL,NULL,NULL),(34,'Alysa@91','Alysa','Mong','Female','1998-07-20','2021-05-16 - 2021-02-02',0,'Part time',3,NULL,'2021-05-10 00:55:40','2021-05-10 00:55:40','_intern.jpg','0943154555',1,3,NULL,NULL,NULL),(35,'Jamel@90','Jamel','Demar','Male','1998-07-20','2021-05-16 - 2021-02-02',0,'Part time',4,NULL,'2021-05-10 00:55:40','2021-05-10 00:55:40','_intern.jpg','0943154555',2,2,NULL,NULL,NULL),(36,'Reta@89','Reta','Demar','Female','1998-07-20','2021-05-16 - 2021-02-02',0,'Part time',3,NULL,'2021-05-10 00:55:40','2021-05-10 00:55:40','_intern.jpg','0943154555',2,3,NULL,NULL,NULL),(37,'Tamar@88','Tamar','Rae','Male','1998-07-20','2021-05-16 - 2021-02-02',0,'Part time',4,NULL,'2021-05-10 00:55:40','2021-05-10 00:55:40','_intern.jpg','0943154555',1,2,NULL,NULL,NULL),(38,'Viet@87','Viet','Ringdahl','Female','1998-07-20','2021-05-16 - 2021-02-02',0,'Part time',2,NULL,'2021-05-10 00:55:40','2021-05-10 00:55:40','_intern.jpg','0943154555',1,3,NULL,NULL,NULL),(39,'Quoc@86','Quoc','Demar','Male','1998-07-20','2021-05-16 - 2021-02-02',0,'Part time',1,NULL,'2021-05-10 00:55:40','2021-05-10 00:55:40','_intern.jpg','0943154555',2,1,NULL,NULL,NULL),(40,'Jarvis@85','Jarvis','Enriquez','Female','1998-07-20','2021-05-16 - 2021-02-02',0,'Part time',3,NULL,'2021-05-10 00:55:40','2021-05-10 00:55:40','_intern.jpg','0943154555',3,2,NULL,NULL,NULL),(41,'Alysa@84','Alysa','Demar','Male','1998-07-20','2021-05-16 - 2021-02-02',0,'Part time',3,NULL,'2021-05-10 00:55:40','2021-05-10 00:55:40','_intern.jpg','0943154555',1,1,NULL,NULL,NULL),(42,'Esmeralda@83','Esmeralda','Jenney','Female','1998-07-20','2021-05-16 - 2021-02-02',0,'Part time',4,NULL,'2021-05-10 00:55:40','2021-05-10 00:55:40','_intern.jpg','0943154555',3,4,NULL,NULL,NULL),(43,'Viet@82','Viet','Jensen','Male','1998-07-20','2021-05-16 - 2021-02-02',0,'Part time',2,NULL,'2021-05-10 00:55:40','2021-05-10 00:55:40','_intern.jpg','0943154555',3,2,NULL,NULL,NULL),(44,'Alysa@81','Alysa','Dunkle','Female','1998-07-20','2021-05-16 - 2021-02-02',0,'Part time',1,NULL,'2021-05-10 00:55:40','2021-05-10 00:55:40','_intern.jpg','0943154555',2,3,NULL,NULL,NULL),(45,'Korey@80','Korey','Laborde','Male','1998-07-20','2021-05-16 - 2021-02-02',0,'Part time',2,NULL,'2021-05-10 00:55:40','2021-05-10 00:55:40','_intern.jpg','0943154555',1,1,NULL,NULL,NULL),(46,'Viet@79','Viet','Adcox','Female','1998-07-20','2021-05-16 - 2021-02-02',0,'Part time',4,NULL,'2021-05-10 00:55:40','2021-05-10 00:55:40','_intern.jpg','0943154555',3,2,NULL,NULL,NULL),(47,'Lan@78','Lan','Lafortune','Male','1998-07-20','2021-05-16 - 2021-02-02',0,'Part time',2,NULL,'2021-05-10 00:55:40','2021-05-10 00:55:40','_intern.jpg','0943154555',3,3,NULL,NULL,NULL),(48,'Esmeralda@76','Esmeralda','Dunkle','Male','1998-07-20','2021-05-16 - 2021-02-02',0,'Part time',2,NULL,'2021-05-10 00:55:40','2021-05-10 00:55:40','_intern.jpg','0943154555',3,1,NULL,NULL,NULL),(49,'Madge@51','Madge','Demar','Female','1998-07-20','2021-05-16 - 2021-02-02',0,'Part time',2,NULL,'2021-05-10 00:55:40','2021-05-10 00:55:40','_intern.jpg','0943154555',1,4,NULL,NULL,NULL),(50,'Domitila@50','Domitila','Trostle','Male','1998-07-20','2021-05-16 - 2021-02-02',0,'Part time',2,NULL,'2021-05-10 00:55:40','2021-05-10 00:55:40','_intern.jpg','0943154555',3,4,NULL,NULL,NULL),(51,'Reta@49','Reta','Eberhard','Female','1998-07-20','2021-05-16 - 2021-02-02',0,'Part time',4,NULL,'2021-05-10 00:55:40','2021-05-10 00:55:40','_intern.jpg','0943154555',1,3,NULL,NULL,NULL),(52,'Rolf@22','Rolf','Mong','Male','1998-07-20','2021-05-16 - 2021-02-02',0,'Part time',4,NULL,'2021-05-10 00:55:40','2021-05-10 00:55:40','_intern.jpg','0943154555',3,4,NULL,NULL,NULL),(53,'Domitila@21','Domitila','Dekker','Female','1998-07-20','2021-05-16 - 2021-02-02',0,'Part time',1,NULL,'2021-05-10 00:55:40','2021-05-10 00:55:40','_intern.jpg','0943154555',2,4,NULL,NULL,NULL),(54,'Theo@20','Theo','Kees','Male','1998-07-20','2021-05-16 - 2021-02-02',0,'Part time',1,NULL,'2021-05-10 00:55:40','2021-05-10 00:55:40','_intern.jpg','0943154555',1,1,NULL,NULL,NULL),(55,'Thanh@19','Thanh','Enriquez','Female','1998-07-20','2021-05-16 - 2021-02-02',0,'Part time',4,NULL,'2021-05-10 00:55:40','2021-05-10 00:55:40','_intern.jpg','0943154555',3,1,NULL,NULL,NULL),(56,'Korey@18','Korey','Kibler','Male','1998-07-20','2021-05-16 - 2021-02-02',0,'Part time',2,NULL,'2021-05-10 00:55:40','2021-05-10 00:55:40','_intern.jpg','0943154555',1,2,NULL,NULL,NULL),(57,'Madge@17','Madge','Jensen','Female','1998-07-20','2021-05-16 - 2021-02-02',0,'Part time',2,NULL,'2021-05-10 00:55:40','2021-05-10 00:55:40','_intern.jpg','0943154555',1,1,NULL,NULL,NULL),(58,'Lan@16','Lan','Kibler','Male','1998-07-20','2021-05-16 - 2021-02-02',0,'Part time',1,NULL,'2021-05-10 00:55:40','2021-05-10 00:55:40','_intern.jpg','0943154555',2,1,NULL,NULL,NULL),(59,'Danika@15','Danika','Rae','Female','1998-07-20','2021-05-16 - 2021-02-02',0,'Part time',4,NULL,'2021-05-10 00:55:40','2021-05-10 00:55:40','_intern.jpg','0943154555',2,3,NULL,NULL,NULL),(60,'Lura@14','Lura','Kibler','Male','1998-07-20','2021-05-16 - 2021-02-02',0,'Part time',3,NULL,'2021-05-10 00:55:40','2021-05-10 00:55:40','_intern.jpg','0943154555',1,2,NULL,NULL,NULL),(61,'Quoc@13','Quoc','Jensen','Female','1998-07-20','2021-05-16 - 2021-02-02',0,'Part time',2,NULL,'2021-05-10 00:55:40','2021-05-10 00:55:40','_intern.jpg','0943154555',2,4,NULL,NULL,NULL),(62,'Jamel@12','Jamel','Diangelo','Male','1998-07-20','2021-05-16 - 2021-02-02',0,'Part time',3,NULL,'2021-05-10 00:55:40','2021-05-10 00:55:40','_intern.jpg','0943154555',1,4,NULL,NULL,NULL),(63,'Viet@11','Viet','Kibler','Female','1998-07-20','2021-05-16 - 2021-02-02',0,'Part time',1,NULL,'2021-05-10 00:55:40','2021-05-10 00:55:40','_intern.jpg','0943154555',1,3,NULL,NULL,NULL),(64,'Marquis@10','Marquis','Rae','Male','1998-07-20','2021-05-16 - 2021-02-02',0,'Part time',2,NULL,'2021-05-10 00:55:40','2021-05-10 00:55:40','_intern.jpg','0943154555',2,1,NULL,NULL,NULL),(65,'Madge@9','Madge','Rae','Female','1998-07-20','2021-05-16 - 2021-02-02',0,'Part time',3,NULL,'2021-05-10 00:55:40','2021-05-10 00:55:40','_intern.jpg','0943154555',2,4,NULL,NULL,NULL),(66,'Lan@8','Lan','Jenney','Male','1998-07-20','2021-05-16 - 2021-02-02',0,'Part time',1,NULL,'2021-05-10 00:55:40','2021-05-10 00:55:40','_intern.jpg','0943154555',3,2,NULL,NULL,NULL),(67,'Theo@7','Theo','Kees','Female','1998-07-20','2021-05-16 - 2021-02-02',0,'Part time',4,NULL,'2021-05-10 00:55:40','2021-05-10 00:55:40','_intern.jpg','0943154555',2,3,NULL,NULL,NULL),(68,'Lizbeth@6','Lizbeth','Mong','Male','1998-07-20','2021-05-16 - 2021-02-02',0,'Part time',4,NULL,'2021-05-10 00:55:40','2021-05-10 00:55:40','_intern.jpg','0943154555',3,3,NULL,NULL,NULL),(69,'Alysa@5','Alysa','Mayville','Female','1998-07-20','2021-05-16 - 2021-02-02',0,'Part time',1,NULL,'2021-05-10 00:55:40','2021-05-10 00:55:40','_intern.jpg','0943154555',2,1,NULL,NULL,NULL),(70,'Madge@4','Madge','Jensen','Male','1998-07-20','2021-05-16 - 2021-02-02',0,'Part time',3,NULL,'2021-05-10 00:55:40','2021-05-10 00:55:40','_intern.jpg','0943154555',3,3,NULL,NULL,NULL),(71,'Eldora@3','Eldora','Kees','Female','1998-07-20','2021-05-16 - 2021-02-02',0,'Part time',1,NULL,'2021-05-10 00:55:40','2021-05-10 00:55:40','_intern.jpg','0943154555',1,1,NULL,NULL,NULL),(72,'Tamar@2','Tamar','Laborde','Male','1998-07-20','2021-05-16 - 2021-02-02',0,'Part time',4,NULL,'2021-05-10 00:55:40','2021-05-10 00:55:40','_intern.jpg','0943154555',2,4,NULL,NULL,NULL),(73,'Jarvis@23','Jarvis','Rae','Female','1998-07-20','2021-05-16 - 2021-02-02',0,'Part time',3,NULL,'2021-05-10 00:55:40','2021-05-10 00:55:40','_intern.jpg','0943154555',3,1,NULL,NULL,NULL),(74,'Reta@24','Reta','Dekker','Male','1998-07-20','2021-05-16 - 2021-02-02',0,'Part time',2,NULL,'2021-05-10 00:55:40','2021-05-10 00:55:40','_intern.jpg','0943154555',3,1,NULL,NULL,NULL),(75,'Rubi@25','Rubi','Eberhard','Female','1998-07-20','2021-05-16 - 2021-02-02',0,'Part time',2,NULL,'2021-05-10 00:55:40','2021-05-10 00:55:40','_intern.jpg','0943154555',2,4,NULL,NULL,NULL),(76,'Esmeralda@26','Esmeralda','Jensen','Male','1998-07-20','2021-05-16 - 2021-02-02',0,'Part time',2,NULL,'2021-05-10 00:55:40','2021-05-10 00:55:40','_intern.jpg','0943154555',1,2,NULL,NULL,NULL),(77,'Tamar@48','Tamar','Mong','Male','1998-07-20','2021-05-16 - 2021-02-02',0,'Part time',1,NULL,'2021-05-10 00:55:40','2021-05-10 00:55:40','_intern.jpg','0943154555',3,1,NULL,NULL,NULL),(78,'Lan@47','Lan','Mong','Female','1998-07-20','2021-05-16 - 2021-02-02',0,'Part time',2,NULL,'2021-05-10 00:55:40','2021-05-10 00:55:40','_intern.jpg','0943154555',2,3,NULL,NULL,NULL),(79,'Marquis@46','Marquis','Diangelo','Male','1998-07-20','2021-05-16 - 2021-02-02',0,'Part time',4,NULL,'2021-05-10 00:55:40','2021-05-10 00:55:40','_intern.jpg','0943154555',1,4,NULL,NULL,NULL),(80,'Theo@45','Theo','Mong','Female','1998-07-20','2021-05-16 - 2021-02-02',0,'Part time',4,NULL,'2021-05-10 00:55:40','2021-05-10 00:55:40','_intern.jpg','0943154555',3,4,NULL,NULL,NULL),(81,'Dorene@44','Dorene','Jenney','Male','1998-07-20','2021-05-16 - 2021-02-02',0,'Part time',1,NULL,'2021-05-10 00:55:40','2021-05-10 00:55:40','_intern.jpg','0943154555',1,4,NULL,NULL,NULL),(82,'Esmeralda@43','Esmeralda','Jenney','Female','1998-07-20','2021-05-16 - 2021-02-02',0,'Part time',1,NULL,'2021-05-10 00:55:40','2021-05-10 00:55:40','_intern.jpg','0943154555',3,2,NULL,NULL,NULL),(83,'Jarvis@42','Jarvis','Demar','Male','1998-07-20','2021-05-16 - 2021-02-02',0,'Part time',1,NULL,'2021-05-10 00:55:40','2021-05-10 00:55:40','_intern.jpg','0943154555',2,4,NULL,NULL,NULL),(84,'Tamar@41','Tamar','Dunkle','Female','1998-07-20','2021-05-16 - 2021-02-02',0,'Part time',1,NULL,'2021-05-10 00:55:40','2021-05-10 00:55:40','_intern.jpg','0943154555',3,1,NULL,NULL,NULL),(85,'Kendall@40','Kendall','Mong','Male','1998-07-20','2021-05-16 - 2021-02-02',0,'Part time',4,NULL,'2021-05-10 00:55:40','2021-05-10 00:55:40','_intern.jpg','0943154555',3,2,NULL,NULL,NULL),(86,'Alysa@39','Alysa','Jenney','Female','1998-07-20','2021-05-16 - 2021-02-02',0,'Part time',2,NULL,'2021-05-10 00:55:40','2021-05-10 00:55:40','_intern.jpg','0943154555',1,1,NULL,NULL,NULL),(87,'Kendall@99','Kendall','Demar','Female','1998-07-20','2021-05-16 - 2021-02-02',0,'Part time',2,NULL,'2021-05-10 00:55:40','2021-05-10 00:55:40','_intern.jpg','0943154555',3,2,NULL,NULL,NULL),(88,'Rubi@38','Rubi','Dekker','Male','1998-07-20','2021-05-16 - 2021-02-02',0,'Part time',3,NULL,'2021-05-10 00:55:40','2021-05-10 00:55:40','_intern.jpg','0943154555',2,2,NULL,NULL,NULL),(89,'Thanh@36','Thanh','Kees','Male','1998-07-20','2021-05-16 - 2021-02-02',0,'Part time',3,NULL,'2021-05-10 00:55:40','2021-05-10 00:55:40','_intern.jpg','0943154555',2,3,NULL,NULL,NULL),(90,'Kendall@35','Kendall','Roberson','Female','1998-07-20','2021-05-16 - 2021-02-02',0,'Part time',3,NULL,'2021-05-10 00:55:40','2021-05-10 00:55:40','_intern.jpg','0943154555',3,1,NULL,NULL,NULL),(91,'Lizbeth@34','Lizbeth','Lafortune','Male','1998-07-20','2021-05-16 - 2021-02-02',0,'Part time',4,NULL,'2021-05-10 00:55:40','2021-05-10 00:55:40','_intern.jpg','0943154555',2,3,NULL,NULL,NULL),(92,'Thanh@33','Thanh','Dunkle','Female','1998-07-20','2021-05-16 - 2021-02-02',0,'Part time',4,NULL,'2021-05-10 00:55:40','2021-05-10 00:55:40','_intern.jpg','0943154555',2,2,NULL,NULL,NULL);
/*!40000 ALTER TABLE `interns` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `organizations`
--

DROP TABLE IF EXISTS `organizations`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `organizations` (
  `OrganizationId` int NOT NULL AUTO_INCREMENT,
  `OrgName` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
  `OrgAddress` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
  `OrgPhone` varchar(15) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
  `CreatedDate` timestamp NULL DEFAULT CURRENT_TIMESTAMP,
  `UpdatedDate` timestamp NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  PRIMARY KEY (`OrganizationId`),
  UNIQUE KEY `Name_UNIQUE` (`OrgName`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `organizations`
--

LOCK TABLES `organizations` WRITE;
/*!40000 ALTER TABLE `organizations` DISABLE KEYS */;
INSERT INTO `organizations` VALUES (1,'Quy Nhon University','170 An Dương Vương, Nguyễn Văn Cừ, Thành phố Qui Nhơn, Bình Định','0256 3846 16','2021-04-01 01:36:54',NULL),(2,'Nha Trang University','170 An Dương Vương, Nguyễn Văn Cừ, Thành phố Nha Trang','0256 3846 156','2021-04-01 01:36:54','2021-05-10 00:20:56'),(3,'Tay Nguyen University','170 An Dương Vương, Nguyễn Văn Cừ, Thành phố Tây Nguyên','0256 3846 156','2021-04-01 01:36:54','2021-04-02 08:37:49');
/*!40000 ALTER TABLE `organizations` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `points`
--

DROP TABLE IF EXISTS `points`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `points` (
  `InternId` int NOT NULL,
  `MarkerId` int DEFAULT NULL,
  `TechnicalSkill` decimal(4,2) DEFAULT NULL,
  `SoftSkill` decimal(4,2) DEFAULT NULL,
  `Attitude` decimal(4,2) DEFAULT NULL,
  `Score` decimal(4,2) GENERATED ALWAYS AS ((((`TechnicalSkill` + `SoftSkill`) + `Attitude`) / 3)) STORED,
  `Passed` tinyint GENERATED ALWAYS AS ((`Score` >= 5)) STORED,
  `CreatedDate` timestamp NULL DEFAULT CURRENT_TIMESTAMP,
  `UpdatedDate` timestamp NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  PRIMARY KEY (`InternId`),
  KEY `FK_marker_user_idx` (`MarkerId`),
  CONSTRAINT `FK_marker_user` FOREIGN KEY (`MarkerId`) REFERENCES `users` (`UserId`) ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT `FK_points_intern` FOREIGN KEY (`InternId`) REFERENCES `interns` (`InternId`) ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT `CHK_Attit` CHECK (((`Attitude` >= 0) and (`Attitude` <= 10))),
  CONSTRAINT `CHK_Soft` CHECK (((`SoftSkill` >= 0) and (`SoftSkill` <= 10))),
  CONSTRAINT `CHK_Tech` CHECK (((`TechnicalSkill` >= 0) and (`TechnicalSkill` <= 10)))
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `points`
--

LOCK TABLES `points` WRITE;
/*!40000 ALTER TABLE `points` DISABLE KEYS */;
/*!40000 ALTER TABLE `points` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `questions`
--

DROP TABLE IF EXISTS `questions`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `questions` (
  `QuestionId` int NOT NULL AUTO_INCREMENT,
  `Group` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
  `InData` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `OutData` varchar(5000) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
  `CreatedDate` timestamp NULL DEFAULT CURRENT_TIMESTAMP,
  `UpdatedDate` timestamp NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  PRIMARY KEY (`QuestionId`),
  UNIQUE KEY `InData_UNIQUE` (`InData`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `questions`
--

LOCK TABLES `questions` WRITE;
/*!40000 ALTER TABLE `questions` DISABLE KEYS */;
INSERT INTO `questions` VALUES (1,'Internship','Ngoại ngữ có yêu cầu cao không Ad?','Nếu em đã có các bằng như (TOEIC, TOEFL, IELTS) tương đương TOEIC 450 trở lên thì không phải làm bài test em nhé.','2021-04-01 02:36:57','2021-05-10 00:51:45'),(2,'Internship','Thời gian thực tập yêu cầu là bao nhiêu vậy Ad?','Thời gian thực tập kéo dài 3 tháng, 2.5 ngày/tuần em à.','2021-04-01 02:36:57','2021-05-10 00:58:18'),(3,'Recruit','TMA là viết tắt của từ gì?','Thân Mật Á!','2021-05-09 19:59:38','2021-05-10 06:37:24');
/*!40000 ALTER TABLE `questions` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `trainings`
--

DROP TABLE IF EXISTS `trainings`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `trainings` (
  `TrainingId` int NOT NULL AUTO_INCREMENT,
  `TraName` varchar(45) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `TraData` text CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
  `CreatedDate` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `UpdatedDate` timestamp NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  `CreatedBy` int DEFAULT NULL,
  PRIMARY KEY (`TrainingId`),
  UNIQUE KEY `TraName_UNIQUE` (`TraName`),
  KEY `FK_createdby_user_idx` (`CreatedBy`),
  CONSTRAINT `FK_createdby_user` FOREIGN KEY (`CreatedBy`) REFERENCES `users` (`UserId`) ON UPDATE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `trainings`
--

LOCK TABLES `trainings` WRITE;
/*!40000 ALTER TABLE `trainings` DISABLE KEYS */;
INSERT INTO `trainings` VALUES (0,'None','{\"ops\":[{\"insert\":\"Nothing here\\n\"}]}','2021-03-30 06:29:34','2021-04-05 05:48:01',1),(1,'Javascript Ninja','{\"ops\":[{\"insert\":\"1\"},{\"attributes\":{\"list\":\"bullet\"},\"insert\":\"\\n\"},{\"insert\":\"3\"},{\"attributes\":{\"list\":\"bullet\"},\"insert\":\"\\n\"},{\"insert\":\"4\"},{\"attributes\":{\"list\":\"bullet\"},\"insert\":\"\\n\"},{\"insert\":\"4434\"},{\"attributes\":{\"list\":\"bullet\"},\"insert\":\"\\n\"},{\"insert\":\"33\"},{\"attributes\":{\"list\":\"bullet\"},\"insert\":\"\\n\"},{\"insert\":\"43\"},{\"attributes\":{\"list\":\"bullet\"},\"insert\":\"\\n\"},{\"insert\":\"4434\"},{\"attributes\":{\"list\":\"bullet\"},\"insert\":\"\\n\"},{\"insert\":\"3434\"},{\"attributes\":{\"list\":\"bullet\"},\"insert\":\"\\n\"},{\"insert\":\"34\"},{\"attributes\":{\"list\":\"bullet\"},\"insert\":\"\\n\"},{\"insert\":\"4333\"},{\"attributes\":{\"list\":\"bullet\"},\"insert\":\"\\n\"}]}','2021-04-20 14:00:28','2021-05-10 00:52:18',1);
/*!40000 ALTER TABLE `trainings` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `users`
--

DROP TABLE IF EXISTS `users`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `users` (
  `UserId` int NOT NULL AUTO_INCREMENT,
  `Email` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
  `Phone` varchar(15) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci DEFAULT NULL,
  `FirstName` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
  `LastName` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci DEFAULT NULL,
  `PasswordHash` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
  `Status` enum('success','danger','warning') CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL DEFAULT 'success',
  `Role` enum('admin','mentor','staff') CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL DEFAULT 'staff',
  `DepartmentId` int DEFAULT NULL,
  `Avatar` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci DEFAULT '/img/user.jpg',
  `Address1` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci DEFAULT NULL,
  `Address2` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci DEFAULT NULL,
  `ZipCode` varchar(20) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci DEFAULT NULL,
  `IsDeleted` tinyint NOT NULL DEFAULT '0',
  `HeaderPhoto` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci DEFAULT '/img/header.jpg',
  `CreatedDate` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `UpdatedDate` timestamp NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  `AvatarVisibility` tinyint NOT NULL DEFAULT '1',
  PRIMARY KEY (`UserId`),
  UNIQUE KEY `Email_UNIQUE` (`Email`),
  KEY `FK_users_departments_id_idx` (`DepartmentId`),
  CONSTRAINT `FK_users_departments_id` FOREIGN KEY (`DepartmentId`) REFERENCES `departments` (`DepartmentId`)
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `users`
--

LOCK TABLES `users` WRITE;
/*!40000 ALTER TABLE `users` DISABLE KEYS */;
INSERT INTO `users` VALUES (1,'admin@x','+84 091 553 35','Thanh Le','Huynh','$2a$11$NdNPr/79zMGg/phT9Vyvk.ztV7TJTMYZwGqOkI1xLWtmR6u61RwFG','danger','staff',1,'/img/avatar/_user.jpg','HCM City',NULL,'454',0,'/img/avatar/_header.jpg','2021-03-15 03:39:36','2021-05-10 06:38:00',1),(2,'tan@tma',NULL,'Tân','Trần','$2a$11$Y6RWgY8CxI7zyGHvTqz16eCdcZPSERWFTHHtlQRWlwIWIAhoG4md6','danger','admin',1,'/img/avatar/_user.jpg','Ha Noi',NULL,'5',0,'/img/avatar/_header.jpg','2021-03-16 20:43:28','2021-04-22 01:34:36',1),(3,'thanh@qnu',NULL,'Thanh','Tran Thien','$2a$11$kWeq0c.p4h5ASXdbdnuRweg8TDzumiS1sfkmb.IormcRxpBao7nsu','success','staff',1,'/img/avatar/_user.jpg','Da Nang',NULL,'65',0,'/img/avatar/_header.jpg','2021-03-17 18:41:37','2021-04-20 08:58:21',1),(4,'by@tma',NULL,'By','Le Thi','$2a$11$QmPcqj0ast0KIogZxIvZiesOLfcg/bpOlpx34ZahIyIixMd/OmVTK','warning','staff',2,'/img/avatar/_user.jpg','Quy Nhon',NULL,'65',0,'/img/avatar/_header.jpg','2021-03-17 02:25:34','2021-04-20 14:19:05',1);
/*!40000 ALTER TABLE `users` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Dumping routines for database 'tmainternship'
--
/*!50003 DROP PROCEDURE IF EXISTS `CheckUser` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `CheckUser`(
  inEmail nvarchar(100)
)
BEGIN
  SELECT * FROM users WHERE Email = inEmail;
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `CleanActivitiesData` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `CleanActivitiesData`(createdBy int)
BEGIN
	DELETE FROM tmainternship.activities WHERE createdBy = createdBy;
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `CleanInternData` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `CleanInternData`()
BEGIN
	DELETE FROM tmainternship.interns WHERE internid > 0;
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `EvaluateIntern` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `EvaluateIntern`(id int, marker int, technicalPoint FLOAT(10,2), softPoint FLOAT(10,2), attitudePoint FLOAT(10,2))
BEGIN
	INSERT INTO points (InternId, MarkerId, TechnicalSkill, SoftSkill, Attitude)
    VALUES(id, marker, technicalPoint, softPoint, attitudePoint)
    ON DUPLICATE KEY UPDATE
    MarkerId = marker,
    TechnicalSkill = technicalPoint,
    SoftSkill = softPoint,
    Attitude = attitudePoint;
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `GetAllPointWithName` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `GetAllPointWithName`()
BEGIN
	select
    t1.InternId,
    TechnicalSkill,
    SoftSkill,
    Attitude,
    Score,
    Passed,
    CONCAT(t2.FirstName,' ',t2.LastName) AS Surname,
    CONCAT(t3.FirstName,' ',t3.LastName) AS Marker
    from points as t1
    	JOIN interns t2 ON t2.InternId = t1.InternId
    	JOIN users t3 ON t3.UserId = t1.MarkerId;

END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `GetEventsJson` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `GetEventsJson`()
BEGIN
SELECT 
	JSON_ARRAYAGG(JSON_OBJECT(
			'id', EventId,
			'title', Title,
			'start', events.Start,
			'end', events.End,
			'className',ClassName,
			'eventDescriptionLabel', EventDescriptionLabel,
			'eventLocationLabel', EventLocationLabel,
			'repeatField', RepeatField,
			'allDay', RepeatField = 'everyday',
			'gestsField', GestsField,
            'image',Image            
		))
AS json FROM events;
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `GetInternDetail` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `GetInternDetail`(id int)
BEGIN
SELECT 
	JSON_OBJECT(
			'internid',t1.InternId,
			'email',t1.Email,
			'fullname',CONCAT(t1.FirstName," ",t1.LastName),
			'gender',t1.Gender,
			'birth',t1.DateOfBirth,
			'phone',t1.Phone,
            'address1',t1.Address1,
            'address2',t1.Address2,
			'type',t1.Type,
			'duration',t1.Duration,
			'organization',OrgName,
			'department',DepName,
			'training',TraName,
			'mentor',CONCAT(t5.FirstName," ",t5.LastName),
            'joindate',t1.CreatedDate
		)
AS json
FROM interns t1
	JOIN organizations t2 ON t2.OrganizationId = t1.OrganizationId
	JOIN departments t3 ON t3.DepartmentId = t1.DepartmentId
	JOIN trainings t4 ON t4.TrainingId = t1.TrainingId
	JOIN users t5 ON t5.UserId = t1.MentorId

WHERE t1.InternId = id;
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `GetInternInfo` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `GetInternInfo`(id int)
BEGIN
SELECT 
	JSON_OBJECT(
			'internid', InternId,
			'email', Email,
			'lastname', LastName,
			'firstname', Firstname,
			'gender',Gender,
			'birth',DateOfBirth,
			'phone',Phone,
			'type',Type,
			'duration',Duration,
			'organizationid',OrganizationId,
			'departmentid',DepartmentId,
			'trainingid',TrainingId,
            'avatar',Avatar            
		)
AS json FROM interns
WHERE InternId = id;
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `GetInternList` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `GetInternList`(
	p_type nvarchar(500))
BEGIN

SET @base = "SELECT SQL_CALC_FOUND_ROWS
CONCAT(t1.FirstName,' ',t1.LastName) AS FullName,
CONCAT(t5.FirstName,' ',t5.LastName) AS Mentor,
t1.InternId,t1.Email,t1.Avatar,
t1.CreatedDate,
t1.Phone,t1.DateOfBirth,t1.Gender,
t1.Duration,t1.Type,
t2.OrgName,t3.DepName,t4.TraName,
t4.TrainingId FROM interns t1
JOIN organizations t2 ON t2.OrganizationId = t1.OrganizationId
JOIN departments t3 ON t3.DepartmentId = t1.DepartmentId
JOIN trainings t4 ON t4.TrainingId = t1.TrainingId
JOIN users t5 ON t5.UserId = t1.MentorId";

SET @query = CONCAT(@base, " WHERE ", p_type);

PREPARE stmt FROM @query;
EXECUTE stmt;
DEALLOCATE PREPARE stmt;

SELECT FOUND_ROWS() AS FOUND_ROWS;
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `GetInternListWithFilter` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `GetInternListWithFilter`(
	on_passed INT,

	date_filter INT,
	start_date NVARCHAR(10),
	end_date NVARCHAR(10),
    
	offset_value INT,
	limit_value INT,
	orderby INT,
	search_on INT,
	search_string NVARCHAR(20))
BEGIN

SELECT SQL_CALC_FOUND_ROWS
	CONCAT(t1.FirstName,' ',t1.LastName) AS FullName,
    CONCAT(t5.FirstName,' ',t5.LastName) AS Mentor,
	t1.InternId,
    t1.Email,
    t1.Avatar,
    t1.CreatedDate,
    t1.Phone,
	t1.DateOfBirth,
    t1.Gender,
    t1.Duration, 
    t1.Type,
    t2.OrgName,
	t3.DepName,
    t4.TraName,
    t4.TrainingId
FROM interns t1
	JOIN organizations t2 ON t2.OrganizationId = t1.OrganizationId
	JOIN departments t3 ON t3.DepartmentId = t1.DepartmentId
	JOIN trainings t4 ON t4.TrainingId = t1.TrainingId
	JOIN users t5 ON t5.UserId = t1.MentorId
	LEFT JOIN points t6 ON t6.InternId = t1.InternId
WHERE
CASE
    WHEN search_on = 0 THEN 1
    WHEN search_on = 1 AND search_string LIKE t1.InternId THEN 1
    WHEN search_on = 2 AND search_string LIKE t1.Email THEN 1
    WHEN search_on = 3 AND search_string LIKE t1.FirstName THEN 1
    WHEN search_on = 4 AND search_string LIKE t1.Phone THEN 1
    WHEN search_on = 5 AND search_string LIKE DepName THEN 1
    WHEN search_on = 6 AND search_string LIKE OrgName THEN 1
    WHEN search_on = 7 AND search_string LIKE TraName THEN 1
    WHEN search_on = 8 AND search_string LIKE t5.FirstName THEN 1
END = 1

AND	CASE 
	WHEN date_filter = 0 THEN 1
	WHEN date_filter = 1 AND SUBSTRING(Duration, 1, 10) > start_date THEN 1
	WHEN date_filter = 2 AND SUBSTRING(Duration, 1, 10) = start_date THEN 1
	WHEN date_filter = 3 AND SUBSTRING(Duration, 1, 10) < start_date THEN 1
	WHEN date_filter = 4 AND SUBSTRING(Duration, 1, 10) BETWEEN start_date AND end_date THEN 1
	WHEN date_filter = 5 AND SUBSTRING(Duration, 1, 10) NOT BETWEEN start_date AND end_date THEN 1
END = 1

AND	CASE 
	WHEN on_passed = 2 THEN 1
	WHEN on_passed = 0 AND t6.Passed = FALSE THEN 1
	WHEN on_passed = 1 AND t6.Passed = TRUE THEN 1
END = 1

ORDER BY
	CASE WHEN orderby=1 THEN t1.InternId END,
	CASE WHEN orderby=2 THEN t1.Email END,
	CASE WHEN orderby=3 THEN t1.FirstName END,
	CASE WHEN orderby=4 THEN t1.Phone END,
	CASE WHEN orderby=5 THEN DepName END,
	CASE WHEN orderby=6 THEN OrgName END,
	CASE WHEN orderby=7 THEN TraName END,
	CASE WHEN orderby=8 THEN t5.FirstName END,
	CASE WHEN orderby=9 THEN t1.Duration END,
	CASE WHEN orderby=10 THEN t1.DateOfBirth END
LIMIT offset_value, limit_value;

SELECT FOUND_ROWS() AS FOUND_ROWS;
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `GetJointEvents` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `GetJointEvents`()
BEGIN
  SELECT Title, EventId, JSON_EXTRACT(GestsField, '$**.iid') AS Joined FROM events;
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `GetJointTrainings` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `GetJointTrainings`(internId int)
BEGIN
	SELECT CONCAT(I.TrainingId, ',', D.SharedTrainings) AS trainings
    FROM interns I
    JOIN departments D
    WHERE I.InternId = internId
    AND I.DepartmentId = D.DepartmentId;
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `GetProfileView` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `GetProfileView`(id int)
BEGIN
	SELECT
		CONCAT(U.FirstName,' ',U.LastName) AS FullName,
        U.Email,
        U.Avatar,
        U.HeaderPhoto,
        U.Address1 as ComeFrom,
        DATE_FORMAT(U.CreatedDate, "%M, %Y") as JoinedDate,
        U.Phone,
        D.DepName as Department,
        D.DepLocation as Organization
	FROM users U
    LEFT JOIN departments D ON U.DepartmentId = D.DepartmentId
    WHERE
		U.UserId = id;
	END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `GetTrainingData` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `GetTrainingData`(id int)
BEGIN
  select json_objectagg(TraName,TraData) AS json from trainings where trainingId = id;
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `GetWhitelist` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `GetWhitelist`()
BEGIN
SELECT 
	JSON_ARRAYAGG(JSON_OBJECT(
			'iid', InternId,
			'src', CONCAT('/img/avatar/', Avatar),
			'value', CONCAT(FirstName, ' ', LastName)
		))
AS json FROM interns;
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `HowManyPassed` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `HowManyPassed`()
BEGIN
	SELECT COUNT(*) AS result FROM points
    WHERE passed;
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `InsertIntern` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `InsertIntern`(
	inEmail varchar(500),
	inPhone varchar(500),
	inFirstName varchar(500),
	inLastName varchar(500),
	inDateOfBirth varchar(500),
	inGender varchar(500),
	inDuration varchar(500),
	inType varchar(500),
	inMentor varchar(500),
	inTrainingId varchar(500),
	inOrganizationId varchar(500),
	inDepartmentId varchar(500))
BEGIN
	INSERT INTO interns (Email,Phone,FirstName,LastName,DateOfBirth,Gender,Duration,Type,MentorId,TrainingId,OrganizationId,DepartmentId)
	VALUES (inEmail,inPhone,inFirstName,inLastName,inDateOfBirth,inGender,inDuration,inType,inMentor,inTrainingId,inOrganizationId,inDepartmentId);
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `UpdateEventByTitle` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `UpdateEventByTitle`(
	inImage nvarchar(500),
	inTitle nvarchar(500),
	inType nvarchar(500),
	inClassName nvarchar(500),
	inStart nvarchar(500),
	inEnd nvarchar(500),
	inCreatedBy nvarchar(500),
	inGestsField nvarchar(500),
	inRepeatField nvarchar(500),
	inEventLocationLabel nvarchar(500),
	inEventDescriptionLabel nvarchar(500))
BEGIN
	UPDATE events
    SET 
		Image = inImage,
		Title = inTitle,
		Type = inType,
		ClassName = inClassName,
		Start = inStart,
		End = inEnd,
		CreatedBy = inCreatedBy,
		GestsField = inGestsField,
		RepeatField = inRepeatField,
		EventLocationLabel = inEventLocationLabel,
		EventDescriptionLabel = inEventDescriptionLabel
    WHERE Title = inTitle;
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2021-05-09 23:39:24
