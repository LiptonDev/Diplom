/*
 Navicat Premium Data Transfer

 Source Server         : localhost
 Source Server Type    : MySQL
 Source Server Version : 50525
 Source Host           : localhost:3306
 Source Schema         : diplom

 Target Server Type    : MySQL
 Target Server Version : 50525
 File Encoding         : 65001

 Date: 08/06/2020 09:47:53
*/

SET NAMES utf8mb4;
SET FOREIGN_KEY_CHECKS = 0;

-- ----------------------------
-- Table structure for drivers
-- ----------------------------
DROP TABLE IF EXISTS `drivers`;
CREATE TABLE `drivers`  (
  `id` int(11) NOT NULL AUTO_INCREMENT COMMENT 'ID',
  `driverLicense` int(11) NOT NULL COMMENT 'В/У',
  `lastName` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL COMMENT 'Фамилия',
  `firstName` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL COMMENT 'Имя',
  `middleName` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL COMMENT 'Отчество',
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 10 CHARACTER SET = utf8 COLLATE = utf8_general_ci ROW_FORMAT = Compact;

-- ----------------------------
-- Table structure for routes
-- ----------------------------
DROP TABLE IF EXISTS `routes`;
CREATE TABLE `routes`  (
  `id` int(11) NOT NULL AUTO_INCREMENT COMMENT 'Id',
  `truckId` int(11) NULL DEFAULT NULL COMMENT 'Машина на маршруте',
  `from` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL COMMENT 'Город отправления',
  `to` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL COMMENT 'Город назначения',
  `description` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL COMMENT 'Краткое описание',
  `status` tinyint(4) NOT NULL DEFAULT 0 COMMENT 'Статус (0 - активный, 1 - завершен)',
  PRIMARY KEY (`id`) USING BTREE,
  INDEX `truckId`(`truckId`) USING BTREE,
  CONSTRAINT `routes_ibfk_1` FOREIGN KEY (`truckId`) REFERENCES `trucks` (`id`) ON DELETE SET NULL ON UPDATE NO ACTION
) ENGINE = InnoDB AUTO_INCREMENT = 3 CHARACTER SET = utf8 COLLATE = utf8_general_ci ROW_FORMAT = Compact;

-- ----------------------------
-- Table structure for semitrails
-- ----------------------------
DROP TABLE IF EXISTS `semitrails`;
CREATE TABLE `semitrails`  (
  `id` int(11) NOT NULL AUTO_INCREMENT COMMENT 'ID',
  `model` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL COMMENT 'Модель полуприцепа',
  `semitrailerNumber` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL COMMENT 'Номер полуприцепа в формате \"АА0000\"',
  `semitrailerNumberRegion` int(11) NOT NULL COMMENT 'Регион на номере в формате \"(0)00\"',
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 9 CHARACTER SET = utf8 COLLATE = utf8_general_ci ROW_FORMAT = Compact;

-- ----------------------------
-- Table structure for staff
-- ----------------------------
DROP TABLE IF EXISTS `staff`;
CREATE TABLE `staff`  (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `lastName` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL,
  `firstName` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL,
  `middleName` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL,
  `position` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL,
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 7 CHARACTER SET = utf8 COLLATE = utf8_general_ci ROW_FORMAT = Compact;

-- ----------------------------
-- Table structure for trucks
-- ----------------------------
DROP TABLE IF EXISTS `trucks`;
CREATE TABLE `trucks`  (
  `id` int(11) NOT NULL AUTO_INCREMENT COMMENT 'ID',
  `series` int(11) NOT NULL COMMENT 'Серия (01, 02 и т.п.)',
  `carModel` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL COMMENT 'Модель машины',
  `carNumber` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL COMMENT 'Номер машины в формате \"А000АА\"',
  `carNumberRegion` int(11) NOT NULL COMMENT 'Регион на номере в формате \"(0)00\"',
  `driverId` int(11) NULL DEFAULT NULL COMMENT 'Водитель',
  `semitrailerId` int(11) NULL DEFAULT NULL COMMENT 'ID прицепа',
  PRIMARY KEY (`id`) USING BTREE,
  INDEX `driverId`(`driverId`) USING BTREE,
  INDEX `semitrailerId`(`semitrailerId`) USING BTREE,
  CONSTRAINT `trucks_ibfk_1` FOREIGN KEY (`driverId`) REFERENCES `drivers` (`id`) ON DELETE SET NULL ON UPDATE NO ACTION,
  CONSTRAINT `trucks_ibfk_2` FOREIGN KEY (`semitrailerId`) REFERENCES `semitrails` (`id`) ON DELETE SET NULL ON UPDATE NO ACTION
) ENGINE = InnoDB AUTO_INCREMENT = 11 CHARACTER SET = utf8 COLLATE = utf8_general_ci ROW_FORMAT = Compact;

-- ----------------------------
-- Table structure for users
-- ----------------------------
DROP TABLE IF EXISTS `users`;
CREATE TABLE `users`  (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `login` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL,
  `password` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL,
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 2 CHARACTER SET = utf8 COLLATE = utf8_general_ci ROW_FORMAT = Compact;

SET FOREIGN_KEY_CHECKS = 1;
