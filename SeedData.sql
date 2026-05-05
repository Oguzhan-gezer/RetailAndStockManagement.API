-- =============================================
-- RETAIL & STOCK MANAGEMENT - SEED DATA
-- =============================================
-- Execution order: Countries → Regions → Stores
-- Run on: (localdb)\MSSQLLocalDB > RetailAndStockManagement
-- =============================================

USE [RetailAndStockManagement];
GO

SET IDENTITY_INSERT [Countries] ON;
INSERT INTO [Countries] ([CountryId], [CountryName]) VALUES
(1, 'Türkiye'),
(2, 'Almanya'),
(3, 'Fransa'),
(4, 'İtalya'),
(5, 'İspanya'),
(6, 'Hollanda'),
(7, 'Polonya'),
(8, 'Romanya');
SET IDENTITY_INSERT [Countries] OFF;
GO

-- =============================================
-- REGIONS (Şehirler / İller)
-- =============================================
SET IDENTITY_INSERT [Regions] ON;

-- TÜRKİYE (CountryId=1) - 81 İl
INSERT INTO [Regions] ([RegionId], [RegionName], [CountryId]) VALUES
(1,  'Adana',          1),
(2,  'Adıyaman',       1),
(3,  'Afyonkarahisar', 1),
(4,  'Ağrı',           1),
(5,  'Amasya',         1),
(6,  'Ankara',         1),
(7,  'Antalya',        1),
(8,  'Artvin',         1),
(9,  'Aydın',          1),
(10, 'Balıkesir',      1),
(11, 'Bilecik',        1),
(12, 'Bingöl',         1),
(13, 'Bitlis',         1),
(14, 'Bolu',           1),
(15, 'Burdur',         1),
(16, 'Bursa',          1),
(17, 'Çanakkale',      1),
(18, 'Çankırı',        1),
(19, 'Çorum',          1),
(20, 'Denizli',        1),
(21, 'Diyarbakır',     1),
(22, 'Edirne',         1),
(23, 'Elazığ',         1),
(24, 'Erzincan',       1),
(25, 'Erzurum',        1),
(26, 'Eskişehir',      1),
(27, 'Gaziantep',      1),
(28, 'Giresun',        1),
(29, 'Gümüşhane',      1),
(30, 'Hakkari',        1),
(31, 'Hatay',          1),
(32, 'Isparta',        1),
(33, 'Mersin',         1),
(34, 'İstanbul',       1),
(35, 'İzmir',          1),
(36, 'Kars',           1),
(37, 'Kastamonu',      1),
(38, 'Kayseri',        1),
(39, 'Kırklareli',     1),
(40, 'Kırşehir',       1),
(41, 'Kocaeli',        1),
(42, 'Konya',          1),
(43, 'Kütahya',        1),
(44, 'Malatya',        1),
(45, 'Manisa',         1),
(46, 'Kahramanmaraş',  1),
(47, 'Mardin',         1),
(48, 'Muğla',          1),
(49, 'Muş',            1),
(50, 'Nevşehir',       1),
(51, 'Niğde',          1),
(52, 'Ordu',           1),
(53, 'Rize',           1),
(54, 'Sakarya',        1),
(55, 'Samsun',         1),
(56, 'Siirt',          1),
(57, 'Sinop',          1),
(58, 'Sivas',          1),
(59, 'Tekirdağ',       1),
(60, 'Tokat',          1),
(61, 'Trabzon',        1),
(62, 'Tunceli',        1),
(63, 'Şanlıurfa',      1),
(64, 'Uşak',           1),
(65, 'Van',            1),
(66, 'Yozgat',         1),
(67, 'Zonguldak',      1),
(68, 'Aksaray',        1),
(69, 'Bayburt',        1),
(70, 'Karaman',        1),
(71, 'Kırıkkale',      1),
(72, 'Batman',         1),
(73, 'Şırnak',         1),
(74, 'Bartın',         1),
(75, 'Ardahan',        1),
(76, 'Iğdır',          1),
(77, 'Yalova',         1),
(78, 'Karabük',        1),
(79, 'Kilis',          1),
(80, 'Osmaniye',       1),
(81, 'Düzce',          1),

-- ALMANYA (CountryId=2)
(82, 'Berlin',         2),
(83, 'Hamburg',        2),
(84, 'Münih',          2),
(85, 'Frankfurt',      2),
(86, 'Köln',           2),

-- FRANSA (CountryId=3)
(87, 'Paris',          3),
(88, 'Lyon',           3),
(89, 'Marsilya',       3),
(90, 'Toulouse',       3),
(91, 'Nice',           3),

-- İTALYA (CountryId=4)
(92, 'Roma',           4),
(93, 'Milano',         4),
(94, 'Napoli',         4),
(95, 'Torino',         4),
(96, 'Palermo',        4),

-- İSPANYA (CountryId=5)
(97,  'Madrid',        5),
(98,  'Barselona',     5),
(99,  'Valensiya',     5),
(100, 'Sevilla',       5),
(101, 'Zaragoza',      5),

-- HOLLANDA (CountryId=6)
(102, 'Amsterdam',     6),
(103, 'Rotterdam',     6),
(104, 'Lahey',         6),
(105, 'Utrecht',       6),
(106, 'Eindhoven',     6),

-- POLONYA (CountryId=7)
(107, 'Varşova',       7),
(108, 'Krakow',        7),
(109, 'Lodz',          7),
(110, 'Wroclaw',       7),
(111, 'Poznan',        7),

-- ROMANYA (CountryId=8)
(112, 'Bükreş',        8),
(113, 'Cluj-Napoca',   8),
(114, 'Timişoara',     8),
(115, 'Yaş',           8),
(116, 'Constanta',     8);

SET IDENTITY_INSERT [Regions] OFF;
GO

-- =============================================
-- STORES (Mağazalar)
-- Format: ÜLKE-ŞEHİR-N  |  StoreLevel: A/B/C
-- =============================================
SET IDENTITY_INSERT [dbo].[Store] ON;

-- TÜRKİYE Mağazaları
-- Büyük şehirlere 3, orta şehirlere 2, küçük şehirlere 1 mağaza
INSERT INTO [dbo].[Store] ([StoreId],[StoreLocation],[StoreLevel],[RegionId]) VALUES
-- Adana (R1)
(1,  'TR-Adana-1',  'A', 1),
(2,  'TR-Adana-2',  'B', 1),
-- Adıyaman (R2)
(3,  'TR-Adiyaman-1', 'C', 2),
-- Afyonkarahisar (R3)
(4,  'TR-Afyon-1', 'C', 3),
-- Ağrı (R4)
(5,  'TR-Agri-1',  'C', 4),
-- Amasya (R5)
(6,  'TR-Amasya-1', 'C', 5),
-- Ankara (R6)
(7,  'TR-Ankara-1', 'A', 6),
(8,  'TR-Ankara-2', 'A', 6),
(9,  'TR-Ankara-3', 'B', 6),
-- Antalya (R7)
(10, 'TR-Antalya-1', 'A', 7),
(11, 'TR-Antalya-2', 'B', 7),
(12, 'TR-Antalya-3', 'C', 7),
-- Artvin (R8)
(13, 'TR-Artvin-1', 'C', 8),
-- Aydın (R9)
(14, 'TR-Aydin-1', 'B', 9),
(15, 'TR-Aydin-2', 'C', 9),
-- Balıkesir (R10)
(16, 'TR-Balikesir-1', 'B', 10),
(17, 'TR-Balikesir-2', 'C', 10),
-- Bilecik (R11)
(18, 'TR-Bilecik-1', 'C', 11),
-- Bingöl (R12)
(19, 'TR-Bingol-1', 'C', 12),
-- Bitlis (R13)
(20, 'TR-Bitlis-1', 'C', 13),
-- Bolu (R14)
(21, 'TR-Bolu-1', 'C', 14),
-- Burdur (R15)
(22, 'TR-Burdur-1', 'C', 15),
-- Bursa (R16)
(23, 'TR-Bursa-1', 'A', 16),
(24, 'TR-Bursa-2', 'B', 16),
(25, 'TR-Bursa-3', 'C', 16),
-- Çanakkale (R17)
(26, 'TR-Canakkale-1', 'B', 17),
-- Çankırı (R18)
(27, 'TR-Cankiri-1', 'C', 18),
-- Çorum (R19)
(28, 'TR-Corum-1', 'C', 19),
-- Denizli (R20)
(29, 'TR-Denizli-1', 'B', 20),
(30, 'TR-Denizli-2', 'C', 20),
-- Diyarbakır (R21)
(31, 'TR-Diyarbakir-1', 'A', 21),
(32, 'TR-Diyarbakir-2', 'B', 21),
-- Edirne (R22)
(33, 'TR-Edirne-1', 'B', 22),
-- Elazığ (R23)
(34, 'TR-Elazig-1', 'B', 23),
-- Erzincan (R24)
(35, 'TR-Erzincan-1', 'C', 24),
-- Erzurum (R25)
(36, 'TR-Erzurum-1', 'B', 25),
(37, 'TR-Erzurum-2', 'C', 25),
-- Eskişehir (R26)
(38, 'TR-Eskisehir-1', 'A', 26),
(39, 'TR-Eskisehir-2', 'B', 26),
-- Gaziantep (R27)
(40, 'TR-Gaziantep-1', 'A', 27),
(41, 'TR-Gaziantep-2', 'B', 27),
(42, 'TR-Gaziantep-3', 'C', 27),
-- Giresun (R28)
(43, 'TR-Giresun-1', 'C', 28),
-- Gümüşhane (R29)
(44, 'TR-Gumushane-1', 'C', 29),
-- Hakkari (R30)
(45, 'TR-Hakkari-1', 'C', 30),
-- Hatay (R31)
(46, 'TR-Hatay-1', 'A', 31),
(47, 'TR-Hatay-2', 'B', 31),
-- Isparta (R32)
(48, 'TR-Isparta-1', 'B', 32),
-- Mersin (R33)
(49, 'TR-Mersin-1', 'A', 33),
(50, 'TR-Mersin-2', 'B', 33),
-- İstanbul (R34)
(51, 'TR-Istanbul-1', 'A', 34),
(52, 'TR-Istanbul-2', 'A', 34),
(53, 'TR-Istanbul-3', 'A', 34),
-- İzmir (R35)
(54, 'TR-Izmir-1', 'A', 35),
(55, 'TR-Izmir-2', 'A', 35),
(56, 'TR-Izmir-3', 'B', 35),
-- Kars (R36)
(57, 'TR-Kars-1', 'C', 36),
-- Kastamonu (R37)
(58, 'TR-Kastamonu-1', 'C', 37),
-- Kayseri (R38)
(59, 'TR-Kayseri-1', 'A', 38),
(60, 'TR-Kayseri-2', 'B', 38),
-- Kırklareli (R39)
(61, 'TR-Kirklareli-1', 'C', 39),
-- Kırşehir (R40)
(62, 'TR-Kirsehir-1', 'C', 40),
-- Kocaeli (R41)
(63, 'TR-Kocaeli-1', 'A', 41),
(64, 'TR-Kocaeli-2', 'B', 41),
(65, 'TR-Kocaeli-3', 'C', 41),
-- Konya (R42)
(66, 'TR-Konya-1', 'A', 42),
(67, 'TR-Konya-2', 'B', 42),
-- Kütahya (R43)
(68, 'TR-Kutahya-1', 'C', 43),
-- Malatya (R44)
(69, 'TR-Malatya-1', 'B', 44),
(70, 'TR-Malatya-2', 'C', 44),
-- Manisa (R45)
(71, 'TR-Manisa-1', 'B', 45),
-- Kahramanmaraş (R46)
(72, 'TR-Kahramanmaras-1', 'B', 46),
(73, 'TR-Kahramanmaras-2', 'C', 46),
-- Mardin (R47)
(74, 'TR-Mardin-1', 'B', 47),
-- Muğla (R48)
(75, 'TR-Mugla-1', 'B', 48),
(76, 'TR-Mugla-2', 'C', 48),
-- Muş (R49)
(77, 'TR-Mus-1', 'C', 49),
-- Nevşehir (R50)
(78, 'TR-Nevsehir-1', 'B', 50),
-- Niğde (R51)
(79, 'TR-Nigde-1', 'C', 51),
-- Ordu (R52)
(80, 'TR-Ordu-1', 'B', 52),
-- Rize (R53)
(81, 'TR-Rize-1', 'C', 53),
-- Sakarya (R54)
(82, 'TR-Sakarya-1', 'B', 54),
(83, 'TR-Sakarya-2', 'C', 54),
-- Samsun (R55)
(84, 'TR-Samsun-1', 'A', 55),
(85, 'TR-Samsun-2', 'B', 55),
-- Siirt (R56)
(86, 'TR-Siirt-1', 'C', 56),
-- Sinop (R57)
(87, 'TR-Sinop-1', 'C', 57),
-- Sivas (R58)
(88, 'TR-Sivas-1', 'B', 58),
-- Tekirdağ (R59)
(89, 'TR-Tekirdag-1', 'B', 59),
(90, 'TR-Tekirdag-2', 'C', 59),
-- Tokat (R60)
(91, 'TR-Tokat-1', 'C', 60),
-- Trabzon (R61)
(92, 'TR-Trabzon-1', 'A', 61),
(93, 'TR-Trabzon-2', 'B', 61),
-- Tunceli (R62)
(94, 'TR-Tunceli-1', 'C', 62),
-- Şanlıurfa (R63)
(95, 'TR-Sanliurfa-1', 'A', 63),
(96, 'TR-Sanliurfa-2', 'B', 63),
-- Uşak (R64)
(97, 'TR-Usak-1', 'C', 64),
-- Van (R65)
(98, 'TR-Van-1', 'B', 65),
(99, 'TR-Van-2', 'C', 65),
-- Yozgat (R66)
(100, 'TR-Yozgat-1', 'C', 66),
-- Zonguldak (R67)
(101, 'TR-Zonguldak-1', 'B', 67),
-- Aksaray (R68)
(102, 'TR-Aksaray-1', 'C', 68),
-- Bayburt (R69)
(103, 'TR-Bayburt-1', 'C', 69),
-- Karaman (R70)
(104, 'TR-Karaman-1', 'C', 70),
-- Kırıkkale (R71)
(105, 'TR-Kirikkale-1', 'C', 71),
-- Batman (R72)
(106, 'TR-Batman-1', 'B', 72),
-- Şırnak (R73)
(107, 'TR-Sirnak-1', 'C', 73),
-- Bartın (R74)
(108, 'TR-Bartin-1', 'C', 74),
-- Ardahan (R75)
(109, 'TR-Ardahan-1', 'C', 75),
-- Iğdır (R76)
(110, 'TR-Igdir-1', 'C', 76),
-- Yalova (R77)
(111, 'TR-Yalova-1', 'B', 77),
-- Karabük (R78)
(112, 'TR-Karabuk-1', 'C', 78),
-- Kilis (R79)
(113, 'TR-Kilis-1', 'C', 79),
-- Osmaniye (R80)
(114, 'TR-Osmaniye-1', 'C', 80),
-- Düzce (R81)
(115, 'TR-Duzce-1', 'C', 81),

-- ALMANYA Mağazaları
(116, 'DE-Berlin-1',    'A', 82),
(117, 'DE-Berlin-2',    'A', 82),
(118, 'DE-Berlin-3',    'B', 82),
(119, 'DE-Hamburg-1',   'A', 83),
(120, 'DE-Hamburg-2',   'B', 83),
(121, 'DE-Munih-1',     'A', 84),
(122, 'DE-Munih-2',     'B', 84),
(123, 'DE-Frankfurt-1', 'A', 85),
(124, 'DE-Frankfurt-2', 'B', 85),
(125, 'DE-Koln-1',      'B', 86),
(126, 'DE-Koln-2',      'C', 86),

-- FRANSA Mağazaları
(127, 'FR-Paris-1',     'A', 87),
(128, 'FR-Paris-2',     'A', 87),
(129, 'FR-Paris-3',     'B', 87),
(130, 'FR-Lyon-1',      'A', 88),
(131, 'FR-Lyon-2',      'B', 88),
(132, 'FR-Marsilya-1',  'B', 89),
(133, 'FR-Marsilya-2',  'C', 89),
(134, 'FR-Toulouse-1',  'B', 90),
(135, 'FR-Nice-1',      'B', 91),
(136, 'FR-Nice-2',      'C', 91),

-- İTALYA Mağazaları
(137, 'IT-Roma-1',      'A', 92),
(138, 'IT-Roma-2',      'A', 92),
(139, 'IT-Roma-3',      'B', 92),
(140, 'IT-Milano-1',    'A', 93),
(141, 'IT-Milano-2',    'B', 93),
(142, 'IT-Napoli-1',    'B', 94),
(143, 'IT-Napoli-2',    'C', 94),
(144, 'IT-Torino-1',    'B', 95),
(145, 'IT-Palermo-1',   'C', 96),

-- İSPANYA Mağazaları
(146, 'ES-Madrid-1',    'A', 97),
(147, 'ES-Madrid-2',    'A', 97),
(148, 'ES-Madrid-3',    'B', 97),
(149, 'ES-Barselona-1', 'A', 98),
(150, 'ES-Barselona-2', 'B', 98),
(151, 'ES-Valensiya-1', 'B', 99),
(152, 'ES-Sevilla-1',   'B', 100),
(153, 'ES-Zaragoza-1',  'C', 101),

-- HOLLANDA Mağazaları
(154, 'NL-Amsterdam-1', 'A', 102),
(155, 'NL-Amsterdam-2', 'B', 102),
(156, 'NL-Rotterdam-1', 'A', 103),
(157, 'NL-Rotterdam-2', 'B', 103),
(158, 'NL-Lahey-1',     'B', 104),
(159, 'NL-Utrecht-1',   'B', 105),
(160, 'NL-Eindhoven-1', 'C', 106),

-- POLONYA Mağazaları
(161, 'PL-Varsova-1',   'A', 107),
(162, 'PL-Varsova-2',   'B', 107),
(163, 'PL-Krakow-1',    'B', 108),
(164, 'PL-Lodz-1',      'B', 109),
(165, 'PL-Wroclaw-1',   'B', 110),
(166, 'PL-Poznan-1',    'C', 111),

-- ROMANYA Mağazaları
(167, 'RO-Bukres-1',    'A', 112),
(168, 'RO-Bukres-2',    'B', 112),
(169, 'RO-Cluj-1',      'B', 113),
(170, 'RO-Timisoara-1', 'B', 114),
(171, 'RO-Yas-1',       'C', 115),
(172, 'RO-Constanta-1', 'C', 116);

SET IDENTITY_INSERT [dbo].[Store] OFF;
GO

PRINT 'Seed data başarıyla eklendi!';
PRINT '  - Ülkeler: 8';
PRINT '  - Bölgeler/Şehirler: 116 (81 TR + 35 diğer)';
PRINT '  - Mağazalar: 172';
GO
