$conn = New-Object System.Data.SqlClient.SqlConnection("Server=(localdb)\MSSQLLocalDB;Database=RetailAndStockManagement;Trusted_Connection=True;")
$conn.Open()
$cmd = $conn.CreateCommand()

function Exec($sql) { $cmd.CommandText = $sql; $cmd.ExecuteNonQuery() | Out-Null }

Write-Host "Countries ekleniyor..."
$countries = @(
    @(1,"Türkiye"), @(2,"Almanya"), @(3,"Fransa"), @(4,"İtalya"),
    @(5,"İspanya"), @(6,"Hollanda"), @(7,"Polonya"), @(8,"Romanya")
)
Exec "SET IDENTITY_INSERT [Countries] ON"
foreach ($c in $countries) {
    Exec "INSERT INTO [Countries]([CountryId],[CountryName]) VALUES($($c[0]),N'$($c[1])')"
}
Exec "SET IDENTITY_INSERT [Countries] OFF"

Write-Host "Regions ekleniyor..."
$regions = @(
    @(1,"Adana",1),@(2,"Adıyaman",1),@(3,"Afyonkarahisar",1),@(4,"Ağrı",1),
    @(5,"Amasya",1),@(6,"Ankara",1),@(7,"Antalya",1),@(8,"Artvin",1),
    @(9,"Aydın",1),@(10,"Balıkesir",1),@(11,"Bilecik",1),@(12,"Bingöl",1),
    @(13,"Bitlis",1),@(14,"Bolu",1),@(15,"Burdur",1),@(16,"Bursa",1),
    @(17,"Çanakkale",1),@(18,"Çankırı",1),@(19,"Çorum",1),@(20,"Denizli",1),
    @(21,"Diyarbakır",1),@(22,"Edirne",1),@(23,"Elazığ",1),@(24,"Erzincan",1),
    @(25,"Erzurum",1),@(26,"Eskişehir",1),@(27,"Gaziantep",1),@(28,"Giresun",1),
    @(29,"Gümüşhane",1),@(30,"Hakkari",1),@(31,"Hatay",1),@(32,"Isparta",1),
    @(33,"Mersin",1),@(34,"İstanbul",1),@(35,"İzmir",1),@(36,"Kars",1),
    @(37,"Kastamonu",1),@(38,"Kayseri",1),@(39,"Kırklareli",1),@(40,"Kırşehir",1),
    @(41,"Kocaeli",1),@(42,"Konya",1),@(43,"Kütahya",1),@(44,"Malatya",1),
    @(45,"Manisa",1),@(46,"Kahramanmaraş",1),@(47,"Mardin",1),@(48,"Muğla",1),
    @(49,"Muş",1),@(50,"Nevşehir",1),@(51,"Niğde",1),@(52,"Ordu",1),
    @(53,"Rize",1),@(54,"Sakarya",1),@(55,"Samsun",1),@(56,"Siirt",1),
    @(57,"Sinop",1),@(58,"Sivas",1),@(59,"Tekirdağ",1),@(60,"Tokat",1),
    @(61,"Trabzon",1),@(62,"Tunceli",1),@(63,"Şanlıurfa",1),@(64,"Uşak",1),
    @(65,"Van",1),@(66,"Yozgat",1),@(67,"Zonguldak",1),@(68,"Aksaray",1),
    @(69,"Bayburt",1),@(70,"Karaman",1),@(71,"Kırıkkale",1),@(72,"Batman",1),
    @(73,"Şırnak",1),@(74,"Bartın",1),@(75,"Ardahan",1),@(76,"Iğdır",1),
    @(77,"Yalova",1),@(78,"Karabük",1),@(79,"Kilis",1),@(80,"Osmaniye",1),
    @(81,"Düzce",1),
    @(82,"Berlin",2),@(83,"Hamburg",2),@(84,"Münih",2),@(85,"Frankfurt",2),@(86,"Köln",2),
    @(87,"Paris",3),@(88,"Lyon",3),@(89,"Marsilya",3),@(90,"Toulouse",3),@(91,"Nice",3),
    @(92,"Roma",4),@(93,"Milano",4),@(94,"Napoli",4),@(95,"Torino",4),@(96,"Palermo",4),
    @(97,"Madrid",5),@(98,"Barselona",5),@(99,"Valensiya",5),@(100,"Sevilla",5),@(101,"Zaragoza",5),
    @(102,"Amsterdam",6),@(103,"Rotterdam",6),@(104,"Lahey",6),@(105,"Utrecht",6),@(106,"Eindhoven",6),
    @(107,"Varşova",7),@(108,"Krakow",7),@(109,"Lodz",7),@(110,"Wroclaw",7),@(111,"Poznan",7),
    @(112,"Bükreş",8),@(113,"Cluj-Napoca",8),@(114,"Timişoara",8),@(115,"Yaş",8),@(116,"Constanta",8)
)
Exec "SET IDENTITY_INSERT [Regions] ON"
foreach ($r in $regions) {
    Exec "INSERT INTO [Regions]([RegionId],[RegionName],[CountryId]) VALUES($($r[0]),N'$($r[1])',$($r[2]))"
}
Exec "SET IDENTITY_INSERT [Regions] OFF"

Write-Host "Stores ekleniyor..."
$stores = @(
    @(1,"TR-Adana-1","A",1),@(2,"TR-Adana-2","B",1),
    @(3,"TR-Adıyaman-1","C",2),@(4,"TR-Afyon-1","C",3),@(5,"TR-Ağrı-1","C",4),
    @(6,"TR-Amasya-1","C",5),
    @(7,"TR-Ankara-1","A",6),@(8,"TR-Ankara-2","A",6),@(9,"TR-Ankara-3","B",6),
    @(10,"TR-Antalya-1","A",7),@(11,"TR-Antalya-2","B",7),@(12,"TR-Antalya-3","C",7),
    @(13,"TR-Artvin-1","C",8),
    @(14,"TR-Aydın-1","B",9),@(15,"TR-Aydın-2","C",9),
    @(16,"TR-Balıkesir-1","B",10),@(17,"TR-Balıkesir-2","C",10),
    @(18,"TR-Bilecik-1","C",11),@(19,"TR-Bingöl-1","C",12),@(20,"TR-Bitlis-1","C",13),
    @(21,"TR-Bolu-1","C",14),@(22,"TR-Burdur-1","C",15),
    @(23,"TR-Bursa-1","A",16),@(24,"TR-Bursa-2","B",16),@(25,"TR-Bursa-3","C",16),
    @(26,"TR-Çanakkale-1","B",17),@(27,"TR-Çankırı-1","C",18),@(28,"TR-Çorum-1","C",19),
    @(29,"TR-Denizli-1","B",20),@(30,"TR-Denizli-2","C",20),
    @(31,"TR-Diyarbakır-1","A",21),@(32,"TR-Diyarbakır-2","B",21),
    @(33,"TR-Edirne-1","B",22),@(34,"TR-Elazığ-1","B",23),@(35,"TR-Erzincan-1","C",24),
    @(36,"TR-Erzurum-1","B",25),@(37,"TR-Erzurum-2","C",25),
    @(38,"TR-Eskişehir-1","A",26),@(39,"TR-Eskişehir-2","B",26),
    @(40,"TR-Gaziantep-1","A",27),@(41,"TR-Gaziantep-2","B",27),@(42,"TR-Gaziantep-3","C",27),
    @(43,"TR-Giresun-1","C",28),@(44,"TR-Gümüşhane-1","C",29),@(45,"TR-Hakkari-1","C",30),
    @(46,"TR-Hatay-1","A",31),@(47,"TR-Hatay-2","B",31),
    @(48,"TR-Isparta-1","B",32),
    @(49,"TR-Mersin-1","A",33),@(50,"TR-Mersin-2","B",33),
    @(51,"TR-İstanbul-1","A",34),@(52,"TR-İstanbul-2","A",34),@(53,"TR-İstanbul-3","A",34),
    @(54,"TR-İzmir-1","A",35),@(55,"TR-İzmir-2","A",35),@(56,"TR-İzmir-3","B",35),
    @(57,"TR-Kars-1","C",36),@(58,"TR-Kastamonu-1","C",37),
    @(59,"TR-Kayseri-1","A",38),@(60,"TR-Kayseri-2","B",38),
    @(61,"TR-Kırklareli-1","C",39),@(62,"TR-Kırşehir-1","C",40),
    @(63,"TR-Kocaeli-1","A",41),@(64,"TR-Kocaeli-2","B",41),@(65,"TR-Kocaeli-3","C",41),
    @(66,"TR-Konya-1","A",42),@(67,"TR-Konya-2","B",42),
    @(68,"TR-Kütahya-1","C",43),
    @(69,"TR-Malatya-1","B",44),@(70,"TR-Malatya-2","C",44),
    @(71,"TR-Manisa-1","B",45),
    @(72,"TR-Kahramanmaraş-1","B",46),@(73,"TR-Kahramanmaraş-2","C",46),
    @(74,"TR-Mardin-1","B",47),
    @(75,"TR-Muğla-1","B",48),@(76,"TR-Muğla-2","C",48),
    @(77,"TR-Muş-1","C",49),@(78,"TR-Nevşehir-1","B",50),@(79,"TR-Niğde-1","C",51),
    @(80,"TR-Ordu-1","B",52),@(81,"TR-Rize-1","C",53),
    @(82,"TR-Sakarya-1","B",54),@(83,"TR-Sakarya-2","C",54),
    @(84,"TR-Samsun-1","A",55),@(85,"TR-Samsun-2","B",55),
    @(86,"TR-Siirt-1","C",56),@(87,"TR-Sinop-1","C",57),@(88,"TR-Sivas-1","B",58),
    @(89,"TR-Tekirdağ-1","B",59),@(90,"TR-Tekirdağ-2","C",59),
    @(91,"TR-Tokat-1","C",60),
    @(92,"TR-Trabzon-1","A",61),@(93,"TR-Trabzon-2","B",61),
    @(94,"TR-Tunceli-1","C",62),
    @(95,"TR-Şanlıurfa-1","A",63),@(96,"TR-Şanlıurfa-2","B",63),
    @(97,"TR-Uşak-1","C",64),
    @(98,"TR-Van-1","B",65),@(99,"TR-Van-2","C",65),
    @(100,"TR-Yozgat-1","C",66),@(101,"TR-Zonguldak-1","B",67),
    @(102,"TR-Aksaray-1","C",68),@(103,"TR-Bayburt-1","C",69),@(104,"TR-Karaman-1","C",70),
    @(105,"TR-Kırıkkale-1","C",71),@(106,"TR-Batman-1","B",72),
    @(107,"TR-Şırnak-1","C",73),@(108,"TR-Bartın-1","C",74),@(109,"TR-Ardahan-1","C",75),
    @(110,"TR-Iğdır-1","C",76),@(111,"TR-Yalova-1","B",77),@(112,"TR-Karabük-1","C",78),
    @(113,"TR-Kilis-1","C",79),@(114,"TR-Osmaniye-1","C",80),@(115,"TR-Düzce-1","C",81),
    @(116,"DE-Berlin-1","A",82),@(117,"DE-Berlin-2","A",82),@(118,"DE-Berlin-3","B",82),
    @(119,"DE-Hamburg-1","A",83),@(120,"DE-Hamburg-2","B",83),
    @(121,"DE-Münih-1","A",84),@(122,"DE-Münih-2","B",84),
    @(123,"DE-Frankfurt-1","A",85),@(124,"DE-Frankfurt-2","B",85),
    @(125,"DE-Köln-1","B",86),@(126,"DE-Köln-2","C",86),
    @(127,"FR-Paris-1","A",87),@(128,"FR-Paris-2","A",87),@(129,"FR-Paris-3","B",87),
    @(130,"FR-Lyon-1","A",88),@(131,"FR-Lyon-2","B",88),
    @(132,"FR-Marsilya-1","B",89),@(133,"FR-Marsilya-2","C",89),
    @(134,"FR-Toulouse-1","B",90),@(135,"FR-Nice-1","B",91),@(136,"FR-Nice-2","C",91),
    @(137,"IT-Roma-1","A",92),@(138,"IT-Roma-2","A",92),@(139,"IT-Roma-3","B",92),
    @(140,"IT-Milano-1","A",93),@(141,"IT-Milano-2","B",93),
    @(142,"IT-Napoli-1","B",94),@(143,"IT-Napoli-2","C",94),
    @(144,"IT-Torino-1","B",95),@(145,"IT-Palermo-1","C",96),
    @(146,"ES-Madrid-1","A",97),@(147,"ES-Madrid-2","A",97),@(148,"ES-Madrid-3","B",97),
    @(149,"ES-Barselona-1","A",98),@(150,"ES-Barselona-2","B",98),
    @(151,"ES-Valensiya-1","B",99),@(152,"ES-Sevilla-1","B",100),@(153,"ES-Zaragoza-1","C",101),
    @(154,"NL-Amsterdam-1","A",102),@(155,"NL-Amsterdam-2","B",102),
    @(156,"NL-Rotterdam-1","A",103),@(157,"NL-Rotterdam-2","B",103),
    @(158,"NL-Lahey-1","B",104),@(159,"NL-Utrecht-1","B",105),@(160,"NL-Eindhoven-1","C",106),
    @(161,"PL-Varşova-1","A",107),@(162,"PL-Varşova-2","B",107),
    @(163,"PL-Krakow-1","B",108),@(164,"PL-Lodz-1","B",109),
    @(165,"PL-Wroclaw-1","B",110),@(166,"PL-Poznan-1","C",111),
    @(167,"RO-Bükreş-1","A",112),@(168,"RO-Bükreş-2","B",112),
    @(169,"RO-Cluj-1","B",113),@(170,"RO-Timişoara-1","B",114),
    @(171,"RO-Yaş-1","C",115),@(172,"RO-Constanta-1","C",116)
)
Exec "SET IDENTITY_INSERT [dbo].[Store] ON"
foreach ($s in $stores) {
    Exec "INSERT INTO [dbo].[Store]([StoreId],[StoreLocation],[StoreLevel],[RegionId]) VALUES($($s[0]),N'$($s[1])',N'$($s[2])',$($s[3]))"
}
Exec "SET IDENTITY_INSERT [dbo].[Store] OFF"

$conn.Close()
Write-Host "✅ Tüm veriler başarıyla eklendi!" -ForegroundColor Green
Write-Host "   - Ülkeler: 8" -ForegroundColor Cyan
Write-Host "   - Bölgeler: 116 (81 TR ili + 35 yabancı şehir)" -ForegroundColor Cyan
Write-Host "   - Mağazalar: 172" -ForegroundColor Cyan
