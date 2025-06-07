-- Criar banco
CREATE DATABASE `bank` /*!40100 DEFAULT CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci */ /*!80016 DEFAULT ENCRYPTION='N' */;

-- Usar banco
USE `bank`;

-- Criar tabelas
-- clients
CREATE TABLE `clients` (
  `id` int NOT NULL AUTO_INCREMENT,
  `name` varchar(100) NOT NULL,
  `document` varchar(11) NOT NULL,
  `phone` varchar(45) NOT NULL,
  `passwordHash` varchar(500) NOT NULL,
  PRIMARY KEY (`id`),
  UNIQUE KEY `document_UNIQUE` (`document`),
  UNIQUE KEY `id_UNIQUE` (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=9 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- accounts
CREATE TABLE `accounts` (
  `id` int NOT NULL AUTO_INCREMENT,
  `number` varchar(100) NOT NULL,
  `idClient` int NOT NULL,
  `creditLimit` decimal(10,2) unsigned zerofill NOT NULL,
  PRIMARY KEY (`id`,`number`),
  UNIQUE KEY `id_UNIQUE` (`id`),
  KEY `idClientFK_idx` (`idClient`),
  CONSTRAINT `idClientFK` FOREIGN KEY (`idClient`) REFERENCES `clients` (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=25 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- transactions
CREATE TABLE `transactions` (
  `id` int NOT NULL AUTO_INCREMENT,
  `value` decimal(10,2) NOT NULL,
  `type` tinyint NOT NULL,
  `idAccount` int DEFAULT NULL,
  `operation` tinyint DEFAULT NULL,
  `creationDate` datetime DEFAULT NULL,
  PRIMARY KEY (`id`),
  KEY `idAccountFK_idx` (`idAccount`),
  CONSTRAINT `idAccountFK` FOREIGN KEY (`idAccount`) REFERENCES `accounts` (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=78 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
