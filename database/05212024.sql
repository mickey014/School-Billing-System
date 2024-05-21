-- phpMyAdmin SQL Dump
-- version 5.2.0
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1:3306
-- Generation Time: May 21, 2024 at 11:25 AM
-- Server version: 8.0.31
-- PHP Version: 8.0.26

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `school_billing`
--

-- --------------------------------------------------------

--
-- Table structure for table `tblay`
--

DROP TABLE IF EXISTS `tblay`;
CREATE TABLE IF NOT EXISTS `tblay` (
  `ay` varchar(50) NOT NULL,
  `status` varchar(50) NOT NULL DEFAULT 'OPEN',
  PRIMARY KEY (`ay`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `tblay`
--

INSERT INTO `tblay` (`ay`, `status`) VALUES
('2020-2021', 'CLOSE'),
('2021-2022', 'OPEN');

-- --------------------------------------------------------

--
-- Table structure for table `tblbilling`
--

DROP TABLE IF EXISTS `tblbilling`;
CREATE TABLE IF NOT EXISTS `tblbilling` (
  `id` int NOT NULL AUTO_INCREMENT,
  `lrn` varchar(50) NOT NULL,
  `ay` varchar(50) NOT NULL,
  `details` varchar(100) NOT NULL,
  `fee` decimal(10,2) NOT NULL DEFAULT '0.00',
  `less` decimal(10,2) NOT NULL DEFAULT '0.00',
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=21 DEFAULT CHARSET=latin1;

--
-- Dumping data for table `tblbilling`
--

INSERT INTO `tblbilling` (`id`, `lrn`, `ay`, `details`, `fee`, `less`) VALUES
(9, '189583459340', '2021-2022', 'Tuition', '5000.00', '0.00'),
(10, '189583459340', '2021-2022', 'School ID', '350.00', '0.00'),
(11, '189583459340', '2021-2022', 'PTA', '300.00', '0.00'),
(12, '189583459340', '2021-2022', 'Registration', '500.00', '0.00'),
(13, '189583459340', '2021-2022', 'VOUCHER', '0.00', '3000.00'),
(14, '1000001112', '2021-2022', 'Tuition', '5000.00', '0.00'),
(15, '11451511', '2021-2022', 'Tuition', '5000.00', '0.00'),
(16, '11451511', '2021-2022', 'School ID', '350.00', '0.00'),
(17, '11451511', '2021-2022', 'PTA', '300.00', '0.00'),
(18, '11451511', '2021-2022', 'VOUCHER', '0.00', '3000.00'),
(19, '11451511', '2021-2022', 'Registration', '500.00', '0.00'),
(20, '155454', '2021-2022', 'Tuition', '5000.00', '0.00');

-- --------------------------------------------------------

--
-- Table structure for table `tblfee`
--

DROP TABLE IF EXISTS `tblfee`;
CREATE TABLE IF NOT EXISTS `tblfee` (
  `id` int NOT NULL AUTO_INCREMENT,
  `details` varchar(100) NOT NULL,
  `amount` decimal(10,2) NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=latin1;

--
-- Dumping data for table `tblfee`
--

INSERT INTO `tblfee` (`id`, `details`, `amount`) VALUES
(1, 'Tuition', '5000.00'),
(2, 'School ID', '350.00'),
(3, 'PTA', '300.00'),
(4, 'Registration', '500.00');

-- --------------------------------------------------------

--
-- Table structure for table `tblpayment`
--

DROP TABLE IF EXISTS `tblpayment`;
CREATE TABLE IF NOT EXISTS `tblpayment` (
  `id` int NOT NULL AUTO_INCREMENT,
  `refno` varchar(50) NOT NULL,
  `lrn` varchar(50) NOT NULL,
  `ay` varchar(50) NOT NULL,
  `period` varchar(50) NOT NULL,
  `particular` varchar(255) NOT NULL,
  `payment` decimal(10,2) NOT NULL,
  `pdate` date NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=12 DEFAULT CHARSET=latin1;

--
-- Dumping data for table `tblpayment`
--

INSERT INTO `tblpayment` (`id`, `refno`, `lrn`, `ay`, `period`, `particular`, `payment`, `pdate`) VALUES
(11, '2507017', '11451511', '2021-2022', 'SECOND GRADING', 'Any', '3150.00', '2024-05-20');

-- --------------------------------------------------------

--
-- Table structure for table `tblscholar`
--

DROP TABLE IF EXISTS `tblscholar`;
CREATE TABLE IF NOT EXISTS `tblscholar` (
  `id` int NOT NULL AUTO_INCREMENT,
  `details` varchar(100) NOT NULL,
  `amount` decimal(10,2) NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=latin1;

--
-- Dumping data for table `tblscholar`
--

INSERT INTO `tblscholar` (`id`, `details`, `amount`) VALUES
(1, 'VOUCHER', '3000.00');

-- --------------------------------------------------------

--
-- Table structure for table `tblsection`
--

DROP TABLE IF EXISTS `tblsection`;
CREATE TABLE IF NOT EXISTS `tblsection` (
  `id` int NOT NULL AUTO_INCREMENT,
  `grade` varchar(50) NOT NULL,
  `section` varchar(50) NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=latin1;

--
-- Dumping data for table `tblsection`
--

INSERT INTO `tblsection` (`id`, `grade`, `section`) VALUES
(1, 'GRADE 7', 'EARTH'),
(2, 'GRADE 7', 'MERCURY');

-- --------------------------------------------------------

--
-- Table structure for table `tblstaff`
--

DROP TABLE IF EXISTS `tblstaff`;
CREATE TABLE IF NOT EXISTS `tblstaff` (
  `id` int NOT NULL AUTO_INCREMENT,
  `name` varchar(255) NOT NULL,
  `username` varchar(255) NOT NULL,
  `password` varchar(255) NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=MyISAM AUTO_INCREMENT=8 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

--
-- Dumping data for table `tblstaff`
--

INSERT INTO `tblstaff` (`id`, `name`, `username`, `password`) VALUES
(7, 'Kirk Mendoza', 'kmendoza06', 'a2lyazEyMzQ1'),
(6, 'test test', 'test', 'dGVzdA==');

-- --------------------------------------------------------

--
-- Table structure for table `tblstudent`
--

DROP TABLE IF EXISTS `tblstudent`;
CREATE TABLE IF NOT EXISTS `tblstudent` (
  `lrn` varchar(50) NOT NULL,
  `lname` varchar(50) NOT NULL,
  `fname` varchar(50) NOT NULL,
  `mname` varchar(255) NOT NULL,
  `address` text NOT NULL,
  `contact` varchar(50) NOT NULL,
  `email` varchar(50) NOT NULL,
  `age` int NOT NULL,
  `sex` varchar(255) NOT NULL,
  `grade` varchar(50) NOT NULL,
  `section` varchar(50) NOT NULL,
  PRIMARY KEY (`lrn`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `tblstudent`
--

INSERT INTO `tblstudent` (`lrn`, `lname`, `fname`, `mname`, `address`, `contact`, `email`, `age`, `sex`, `grade`, `section`) VALUES
('11451511', 'Johnson', 'Jack', 'L', 'blk 55 lot 105', '093503146908', 'jack_jack@gmail.com', 25, 'Male', 'GRADE 7', 'MERCURY'),
('155454', 'Doe', 'John', 'D', 'test', '12321321', 'Test@test.com', 16, 'Male', 'GRADE 7', 'EARTH');

-- --------------------------------------------------------

--
-- Stand-in structure for view `vwbalance`
-- (See below for the actual view)
--
DROP VIEW IF EXISTS `vwbalance`;
CREATE TABLE IF NOT EXISTS `vwbalance` (
`lrn` varchar(50)
,`fullname` varchar(358)
,`total` decimal(33,2)
,`payment` decimal(32,2)
,`balance` decimal(34,2)
,`ay` varchar(50)
);

-- --------------------------------------------------------

--
-- Stand-in structure for view `vwstudent`
-- (See below for the actual view)
--
DROP VIEW IF EXISTS `vwstudent`;
CREATE TABLE IF NOT EXISTS `vwstudent` (
`lrn` varchar(50)
,`fullname` varchar(358)
,`address` text
,`contact` varchar(50)
,`email` varchar(50)
,`grade` varchar(50)
,`section` varchar(50)
);

-- --------------------------------------------------------

--
-- Stand-in structure for view `vwtuition`
-- (See below for the actual view)
--
DROP VIEW IF EXISTS `vwtuition`;
CREATE TABLE IF NOT EXISTS `vwtuition` (
`lrn` varchar(50)
,`fullname` varchar(358)
,`gradesection` varchar(103)
,`fee` decimal(32,2)
,`less` decimal(32,2)
,`total` decimal(33,2)
,`ay` varchar(50)
);

-- --------------------------------------------------------

--
-- Structure for view `vwbalance`
--
DROP TABLE IF EXISTS `vwbalance`;

DROP VIEW IF EXISTS `vwbalance`;
CREATE ALGORITHM=UNDEFINED DEFINER=`root`@`localhost` SQL SECURITY DEFINER VIEW `vwbalance`  AS SELECT `vwtuition`.`lrn` AS `lrn`, `vwtuition`.`fullname` AS `fullname`, `vwtuition`.`total` AS `total`, (select ifnull(sum(`tblpayment`.`payment`),0) from `tblpayment` where ((`tblpayment`.`lrn` = `vwtuition`.`lrn`) and (`tblpayment`.`ay` = `vwtuition`.`ay`))) AS `payment`, (`vwtuition`.`total` - (select ifnull(sum(`tblpayment`.`payment`),0) from `tblpayment` where ((`tblpayment`.`lrn` = `vwtuition`.`lrn`) and (`tblpayment`.`ay` = `vwtuition`.`ay`)))) AS `balance`, `vwtuition`.`ay` AS `ay` FROM `vwtuition``vwtuition`  ;

-- --------------------------------------------------------

--
-- Structure for view `vwstudent`
--
DROP TABLE IF EXISTS `vwstudent`;

DROP VIEW IF EXISTS `vwstudent`;
CREATE ALGORITHM=UNDEFINED DEFINER=`root`@`localhost` SQL SECURITY DEFINER VIEW `vwstudent`  AS SELECT `tblstudent`.`lrn` AS `lrn`, concat(`tblstudent`.`lname`,', ',`tblstudent`.`fname`,' ',`tblstudent`.`mname`) AS `fullname`, `tblstudent`.`address` AS `address`, `tblstudent`.`contact` AS `contact`, `tblstudent`.`email` AS `email`, `tblstudent`.`grade` AS `grade`, `tblstudent`.`section` AS `section` FROM `tblstudent``tblstudent`  ;

-- --------------------------------------------------------

--
-- Structure for view `vwtuition`
--
DROP TABLE IF EXISTS `vwtuition`;

DROP VIEW IF EXISTS `vwtuition`;
CREATE ALGORITHM=UNDEFINED DEFINER=`root`@`localhost` SQL SECURITY DEFINER VIEW `vwtuition`  AS SELECT `s`.`lrn` AS `lrn`, concat(`s`.`lname`,', ',`s`.`fname`,' ',`s`.`mname`) AS `fullname`, concat(`s`.`grade`,' - ',`s`.`section`) AS `gradesection`, ifnull(sum(`b`.`fee`),0) AS `fee`, ifnull(sum(`b`.`less`),0) AS `less`, ifnull((sum(`b`.`fee`) - sum(`b`.`less`)),0) AS `total`, ifnull(`b`.`ay`,'') AS `ay` FROM (`tblstudent` `s` left join `tblbilling` `b` on((`s`.`lrn` = `b`.`lrn`))) GROUP BY `s`.`lrn`, `fullname``fullname`  ;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
