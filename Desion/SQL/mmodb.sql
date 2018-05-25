/*
Navicat MySQL Data Transfer

Source Server         : localhost_3306
Source Server Version : 50717
Source Host           : localhost:3306
Source Database       : mmodb

Target Server Type    : MYSQL
Target Server Version : 50717
File Encoding         : 65001

Date: 2018-05-25 17:50:17
*/

SET FOREIGN_KEY_CHECKS=0;

-- ----------------------------
-- Table structure for accountinfo
-- ----------------------------
DROP TABLE IF EXISTS `accountinfo`;
CREATE TABLE `accountinfo` (
  `accountid` varchar(255) NOT NULL,
  `accountname` varchar(255) DEFAULT NULL,
  `password` varchar(255) DEFAULT NULL,
  PRIMARY KEY (`accountid`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of accountinfo
-- ----------------------------

-- ----------------------------
-- Table structure for characterinfo
-- ----------------------------
DROP TABLE IF EXISTS `characterinfo`;
CREATE TABLE `characterinfo` (
  `id` int(11) unsigned zerofill NOT NULL AUTO_INCREMENT,
  `accountid` varchar(255) DEFAULT NULL,
  `characterid` int(11) DEFAULT NULL,
  `name` varchar(255) DEFAULT NULL,
  `level` int(11) DEFAULT NULL,
  `curstrength` int(11) DEFAULT NULL COMMENT '体力',
  `curexp` int(11) DEFAULT NULL COMMENT '经验',
  `coin` int(11) DEFAULT NULL COMMENT '金币',
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of characterinfo
-- ----------------------------

-- ----------------------------
-- Table structure for imginfo
-- ----------------------------
DROP TABLE IF EXISTS `imginfo`;
CREATE TABLE `imginfo` (
  `id` int(11) unsigned zerofill NOT NULL AUTO_INCREMENT,
  `accountid` varchar(255) DEFAULT NULL,
  `imgid` int(11) DEFAULT NULL,
  `imgpath` varchar(255) DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of imginfo
-- ----------------------------

-- ----------------------------
-- Table structure for inventoryinfo
-- ----------------------------
DROP TABLE IF EXISTS `inventoryinfo`;
CREATE TABLE `inventoryinfo` (
  `id` int(11) unsigned zerofill NOT NULL AUTO_INCREMENT,
  `accountid` varchar(255) DEFAULT NULL,
  `inventoryid` int(11) DEFAULT NULL,
  `havenum` int(11) DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of inventoryinfo
-- ----------------------------

-- ----------------------------
-- Table structure for roominfo
-- ----------------------------
DROP TABLE IF EXISTS `roominfo`;
CREATE TABLE `roominfo` (
  `roomid` int(11) unsigned zerofill NOT NULL AUTO_INCREMENT,
  `roomname` varchar(255) DEFAULT NULL,
  `playernum` int(11) DEFAULT NULL,
  `totalnum` int(11) DEFAULT NULL,
  PRIMARY KEY (`roomid`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of roominfo
-- ----------------------------
