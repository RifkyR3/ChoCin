/*
SQLyog Ultimate v13.1.1 (64 bit)
MySQL - 8.0.38 : Database - chocin
*********************************************************************
*/

/*!40101 SET NAMES utf8 */;

/*!40101 SET SQL_MODE=''*/;

/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;
CREATE DATABASE /*!32312 IF NOT EXISTS*/`chocin` /*!40100 DEFAULT CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci */ /*!80016 DEFAULT ENCRYPTION='N' */;

USE `chocin`;

/*Table structure for table `c_group` */

DROP TABLE IF EXISTS `c_group`;

CREATE TABLE `c_group` (
  `GroupId` int NOT NULL AUTO_INCREMENT,
  `GroupName` varchar(100) NOT NULL,
  PRIMARY KEY (`GroupId`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

/*Data for the table `c_group` */

insert  into `c_group`(`GroupId`,`GroupName`) values 
(1,'Admin');

/*Table structure for table `c_group_module` */

DROP TABLE IF EXISTS `c_group_module`;

CREATE TABLE `c_group_module` (
  `GroupId` int NOT NULL,
  `ModuleId` int NOT NULL,
  PRIMARY KEY (`GroupId`,`ModuleId`),
  KEY `GroupId` (`GroupId`),
  KEY `ModuleId` (`ModuleId`),
  CONSTRAINT `c_group_module_ibfk_1` FOREIGN KEY (`GroupId`) REFERENCES `c_group` (`GroupId`) ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT `c_group_module_ibfk_2` FOREIGN KEY (`ModuleId`) REFERENCES `c_module` (`ModuleId`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

/*Data for the table `c_group_module` */

insert  into `c_group_module`(`GroupId`,`ModuleId`) values 
(1,1),
(1,2),
(1,3),
(1,4);

/*Table structure for table `c_module` */

DROP TABLE IF EXISTS `c_module`;

CREATE TABLE `c_module` (
  `ModuleId` int NOT NULL AUTO_INCREMENT,
  `ModuleSubId` int DEFAULT NULL,
  `ModuleName` varchar(100) NOT NULL,
  `ModuleIcon` varchar(100) DEFAULT NULL,
  `ModulePath` varchar(100) NOT NULL,
  `ModuleOrder` int NOT NULL DEFAULT '0',
  PRIMARY KEY (`ModuleId`),
  KEY `ModuleSubId` (`ModuleSubId`),
  CONSTRAINT `c_module_ibfk_1` FOREIGN KEY (`ModuleSubId`) REFERENCES `c_module` (`ModuleId`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

/*Data for the table `c_module` */

insert  into `c_module`(`ModuleId`,`ModuleSubId`,`ModuleName`,`ModuleIcon`,`ModulePath`,`ModuleOrder`) values 
(1,NULL,'Dashboard','fa-gauge-high','/',1),
(2,NULL,'User Management','fa-people-roof','',10),
(3,2,'Users','fa-users','/users',11),
(4,2,'Groups','fa-user-group','/groups',12);

/*Table structure for table `c_user` */

DROP TABLE IF EXISTS `c_user`;

CREATE TABLE `c_user` (
  `UserId` int NOT NULL AUTO_INCREMENT,
  `UserName` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `UserPassword` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `UserFullName` varchar(255) DEFAULT NULL,
  PRIMARY KEY (`UserId`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

/*Data for the table `c_user` */

insert  into `c_user`(`UserId`,`UserName`,`UserPassword`,`UserFullName`) values 
(1,'admin','$2a$11$JsO38D42sVE.T3GNAlEvkeQApxjbf9vGYURBxWNnwUHpP/nKKEype','Admin');

/*Table structure for table `c_user_group` */

DROP TABLE IF EXISTS `c_user_group`;

CREATE TABLE `c_user_group` (
  `UserId` int NOT NULL,
  `GroupId` int NOT NULL,
  PRIMARY KEY (`UserId`,`GroupId`),
  KEY `UserId` (`UserId`),
  KEY `GroupId` (`GroupId`),
  CONSTRAINT `c_user_group_ibfk_3` FOREIGN KEY (`UserId`) REFERENCES `c_user` (`UserId`) ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT `c_user_group_ibfk_4` FOREIGN KEY (`GroupId`) REFERENCES `c_group` (`GroupId`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

/*Data for the table `c_user_group` */

insert  into `c_user_group`(`UserId`,`GroupId`) values 
(1,1);

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;
