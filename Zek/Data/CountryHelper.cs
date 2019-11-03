using System;
using System.Collections.Generic;
using System.Linq;

namespace Zek.Data
{
    public class Country : ICloneable
    {
        public Country()
        {

        }

        public Country(string alpha2Code, string alpha3Code, string numericCode, string georgian, string english, string russian)
        {
            Alpha2Code = alpha2Code;
            Alpha3Code = alpha3Code;
            NumericCode = numericCode;
            Georgian = georgian;
            English = english;
            Russian = russian;
        }

        public string Alpha2Code { get; private set; }
        public string Alpha3Code { get; private set; }
        public string NumericCode { get; private set; }
        public string Georgian { get; private set; }
        public string English { get; private set; }
        public string Russian { get; private set; }


        public Country Copy()
        {
            return new Country(Alpha2Code, Alpha3Code, NumericCode, Georgian, English, Russian);
        }
        public object Clone()
        {
            return Copy();
        }
    }

    public class CountryHelper
    {
        //private const int Alpha2 = 0;
        //private const int Alpha3 = 1;
        //private const int Numeric = 2;
        //private const int Georgian = 3;
        //private const int English = 4;
        //private const int Russian = 5;
        private static readonly object Sync = new object();
        private static Country[] _countries;
        private static bool _cache;
        public static bool Cache
        {
            get { return _cache; }
            set
            {
                if (value == _cache) return;
                lock (Sync)
                {
                    _cache = value;
                    _countries = _cache ? GetCountries() : null;
                }
            }
        }

        /// <summary>
        /// Get countries.
        /// </summary>
        /// <returns>Returns array of countries.</returns>
        public static Country[] GetCountries()
        {
            return new[]
            {
                new Country("AD", "AND", "020", "ანდორა", "Andorra", "Андорра"),
                new Country("AE", "ARE", "784", "არაბთა გაერთიანებული საამიროები", "United Arab Emirates", "ОАЭ"),
                new Country("AF", "AFG", "004", "ავღანეთი", "Afghanistan", "Афганистан"),
                new Country("AG", "ATG", "028", "ანტიგუა და ბარბუდა", "Antigua and Barbuda", "Антигуа и Барбуда"),
                new Country("AI", "AIA", "660", "ანგილია", "Anguilla", "Ангилья"),
                new Country("AL", "ALB", "008", "ალბანეთი", "Albania", "Албания"),
                new Country("AM", "ARM", "051", "სომხეთი", "Armenia", "Армения"),
                new Country("AO", "AGO", "024", "ანგოლა", "Angola", "Ангола"),
                new Country("AQ", "ATA", "010", "ანტარქტიდა", "Antarctica", "Антарктида"),
                new Country("AR", "ARG", "032", "არგენტინა", "Argentina", "Аргентина"),
                new Country("AS", "ASM", "016", "ამერიკის სამოა", "American Samoa", "Американское Самоа"),
                new Country("AT", "AUT", "040", "ავსტრია", "Austria", "Австрия"),
                new Country("AU", "AUS", "036", "ავსტრალია", "Australia", "Австралия"),
                new Country("AW", "ABW", "533", "არუბა", "Aruba", "Аруба"),
                new Country("AX", "ALA", "248", "ალანდის კუნძულები", "Åland Islands", "Аландские острова"),
                new Country("AZ", "AZE", "031", "აზერბაიჯანი", "Azerbaijan", "Азербайджан"),
                new Country("BA", "BIH", "070", "ბოსნია და ჰერცეგოვინა", "Bosnia and Herzegovina", "Босния и Герцеговина"),
                new Country("BB", "BRB", "052", "ბარბადოსი", "Barbados", "Барбадос"),
                new Country("BD", "BGD", "050", "ბანგლადეში", "Bangladesh", "Бангладеш"),
                new Country("BE", "BEL", "056", "ბელგია", "Belgium", "Бельгия"),
                new Country("BF", "BFA", "854", "ბურკინა-ფასო", "Burkina Faso", "Буркина-Фасо"),
                new Country("BG", "BGR", "100", "ბულგარეთი", "Bulgaria", "Болгария"),
                new Country("BH", "BHR", "048", "ბაჰრეინი", "Bahrain", "Бахрейн"),
                new Country("BI", "BDI", "108", "ბურუნდი", "Burundi", "Бурунди"),
                new Country("BJ", "BEN", "204", "ბენინი", "Benin", "Бенин"),
                new Country("BL", "BLM", "652", "სენ-ბართელმი", "Saint Barthélemy", "Сен-Бартелеми"),
                new Country("BM", "BMU", "060", "ბერმუდის კუნძულები", "Bermuda", "Бермуды"),
                new Country("BN", "BRN", "096", "ბრუნეი დარუსალამი", "Brunei Darussalam", "Бруней"),
                new Country("BO", "BOL", "068", "ბოლივია", "Bolivia, Plurinational State of", "Боливия"),
                new Country("BR", "BRA", "076", "ბრაზილია", "Brazil", "Бразилия"),
                new Country("BS", "BHS", "044", "ბაჰამის კუნძულები", "Bahamas", "Багамы"),
                new Country("BT", "BTN", "064", "ბჰუტანი", "Bhutan", "Бутан"),
                new Country("BV", "BVT", "074", "ბუვე", "Bouvet Island", "Остров Буве"),
                new Country("BW", "BWA", "072", "ბოტსვანა", "Botswana", "Ботсвана"),
                new Country("BY", "BLR", "112", "ბელარუსი", "Belarus", "Белоруссия"),
                new Country("BZ", "BLZ", "084", "ბელიზი", "Belize", "Белиз"),
                new Country("CA", "CAN", "124", "კანადა", "Canada", "Канада"),
                new Country("CC", "CCK", "166", "ქოქოსის კუნძულები", "Cocos (Keeling) Islands", "Кокосовые острова"),
                new Country("CD", "COD", "180", "კონგოს დემოკრატიული რესპუბლიკა", "Congo, the Democratic Republic of the", "ДР Конго"),
                new Country("CF", "CAF", "140", "ცენტრალური აფრიკის რესპუბლიკა", "Central African Republic", "ЦАР"),
                new Country("CG", "COG", "178", "კონგო", "Congo", "Республика Конго"),
                new Country("CH", "CHE", "756", "შვეიცარია", "Switzerland", "Швейцария"),
                new Country("CI", "CIV", "384", "კოტ-დ’ივუარი", "Côte d'Ivoire", "Кот-д’Ивуар"),
                new Country("CK", "COK", "184", "კუკის კუნძულები", "Cook Islands", "Острова Кука"),
                new Country("CL", "CHL", "152", "ჩილე", "Chile", "Чили"),
                new Country("CM", "CMR", "120", "კამერუნი", "Cameroon", "Камерун"),
                new Country("CN", "CHN", "156", "ჩინეთი", "China", "КНР"),
                new Country("CO", "COL", "170", "კოლუმბია", "Colombia", "Колумбия"),
                new Country("CR", "CRI", "188", "კოსტა-რიკა", "Costa Rica", "Коста-Рика"),
                new Country("CU", "CUB", "192", "კუბა", "Cuba", "Куба"),
                new Country("CV", "CPV", "132", "კაბო-ვერდე", "Cape Verde", "Кабо-Верде"),
                new Country("CX", "CXR", "162", "შობის კუნძული", "Christmas Island", "Остров Рождества"),
                new Country("CY", "CYP", "196", "კვიპროსი", "Cyprus", "Кипр"),
                new Country("CZ", "CZE", "203", "ჩეხეთი", "Czech Republic", "Чехия"),
                new Country("DE", "DEU", "276", "გერმანია", "Germany", "Германия"),
                new Country("DJ", "DJI", "262", "ჯიბუტი", "Djibouti", "Джибути"),
                new Country("DK", "DNK", "208", "დანია", "Denmark", "Дания"),
                new Country("DM", "DMA", "212", "დომინიკა", "Dominica", "Доминика"),
                new Country("DO", "DOM", "214", "დომინიკელთა რესპუბლიკა", "Dominican Republic", "Доминиканская Республика"),
                new Country("DZ", "DZA", "012", "ალჟირი", "Algeria", "Алжир"),
                new Country("EC", "ECU", "218", "ეკვადორი", "Ecuador", "Эквадор"),
                new Country("EE", "EST", "233", "ესტონეთი", "Estonia", "Эстония"),
                new Country("EG", "EGY", "818", "ეგვიპტე", "Egypt", "Египет"),
                new Country("EH", "ESH", "732", "დასავლეთი საჰარა", "Western Sahara", "Западная Сахара"),
                new Country("ER", "ERI", "232", "ერიტრეა", "Eritrea", "Эритрея"),
                new Country("ES", "ESP", "724", "ესპანეთი", "Spain", "Испания"),
                new Country("ET", "ETH", "231", "ეთიოპია", "Ethiopia", "Эфиопия"),
                new Country("FI", "FIN", "246", "ფინეთი", "Finland", "Финляндия"),
                new Country("FJ", "FJI", "242", "ფიჯი", "Fiji", "Фиджи"),
                new Country("FK", "FLK", "238", "ფოლკლენდის კუნძულები (მალვინები)", "Falkland Islands (Malvinas)", "Фолклендские острова"),
                new Country("FM", "FSM", "583", "მიკრონეზიის ფედერაციული შტატები", "Micronesia, Federated States of", "Микронезия"),
                new Country("FO", "FRO", "234", "ფარერის კუნძულები", "Faroe Islands", "Фарерские острова"),
                new Country("FR", "FRA", "250", "საფრანგეთი", "France", "Франция"),
                new Country("GA", "GAB", "266", "გაბონი", "Gabon", "Габон"),
                new Country("GB", "GBR", "826", "გაერთიანებული სამეფო", "United Kingdom", "Великобритания"),
                new Country("GD", "GRD", "308", "გრენადა", "Grenada", "Гренада"),
                new Country("GE", "GEO", "268", "საქართველო", "Georgia", "Грузия"),
                new Country("GF", "GUF", "254", "საფრანგეთის გვიანა", "French Guiana", "Гвиана"),
                new Country("GG", "GGY", "831", "გერნსი", "Guernsey", "Гернси"),
                new Country("GH", "GHA", "288", "განა", "Ghana", "Гана"),
                new Country("GI", "GIB", "292", "გიბრალტარი", "Gibraltar", "Гибралтар"),
                new Country("GL", "GRL", "304", "გრენლანდია", "Greenland", "Гренландия"),
                new Country("GM", "GMB", "270", "გამბია", "Gambia", "Гамбия"),
                new Country("GN", "GIN", "324", "გვინეა", "Guinea", "Гвинея"),
                new Country("GP", "GLP", "312", "გვადელუპა", "Guadeloupe", "Гваделупа"),
                new Country("GQ", "GNQ", "226", "ეკვატორული გვინეა", "Equatorial Guinea", "Экваториальная Гвинея"),
                new Country("GR", "GRC", "300", "საბერძნეთი", "Greece", "Греция"),
                new Country("GS", "SGS", "239", "სამხრეთი გეორგია და სამხრეთ სენდვიჩის კუნძულები", "South Georgia and the South Sandwich Islands", "Южная Георгия и Южные Сандвичевы острова"),
                new Country("GT", "GTM", "320", "გვატემალა", "Guatemala", "Гватемала"),
                new Country("GU", "GUM", "316", "გუამი", "Guam", "Гуам"),
                new Country("GW", "GNB", "624", "გვინეა-ბისაუ", "Guinea-Bissau", "Гвинея-Бисау"),
                new Country("GY", "GUY", "328", "გაიანა", "Guyana", "Гайана"),
                new Country("HK", "HKG", "344", "ჰონგკონგი", "Hong Kong", "Гонконг"),
                new Country("HM", "HMD", "334", "ჰერდის და მაკდონალდის კუნძულები", "Heard Island and McDonald Islands", "Херд и Макдональд"),
                new Country("HN", "HND", "340", "ჰონდურასი", "Honduras", "Гондурас"),
                new Country("HR", "HRV", "191", "ხორვატია", "Croatia", "Хорватия"),
                new Country("HT", "HTI", "332", "ჰაიტი", "Haiti", "Гаити"),
                new Country("HU", "HUN", "348", "უნგრეთი", "Hungary", "Венгрия"),
                new Country("ID", "IDN", "360", "ინდონეზია", "Indonesia", "Индонезия"),
                new Country("IE", "IRL", "372", "ირლანდია", "Ireland", "Ирландия"),
                new Country("IL", "ISR", "376", "ისრაელი", "Israel", "Израиль"),
                new Country("IM", "IMN", "833", "კუნძული მენი", "Isle of Man", "Остров Мэн"),
                new Country("IN", "IND", "356", "ინდოეთი", "India", "Индия"),
                new Country("IO", "IOT", "086", "ბრიტანეთის ტერიტორიების  ინდოეთის ოკეანეში ბრიტანეთის ტერიტორიები ინდოეთის ოკეანეში", "British Indian Ocean Territory", "Британская территория в Индийском океане"),
                new Country("IQ", "IRQ", "368", "ერაყი", "Iraq", "Ирак"),
                new Country("IR", "IRN", "364", "ირანის მუსულმანური რესპუბლიკა", "Iran, Islamic Republic of", "Иран"),
                new Country("IS", "ISL", "352", "ისლანდია", "Iceland", "Исландия"),
                new Country("IT", "ITA", "380", "იტალია", "Italy", "Италия"),
                new Country("JE", "JEY", "832", "ჯერსი", "Jersey", "Джерси (остров)"),
                new Country("JM", "JAM", "388", "იამაიკა", "Jamaica", "Ямайка"),
                new Country("JO", "JOR", "400", "იორდანია", "Jordan", "Иордания"),
                new Country("JP", "JPN", "392", "იაპონია", "Japan", "Япония"),
                new Country("KE", "KEN", "404", "კენია", "Kenya", "Кения"),
                new Country("KG", "KGZ", "417", "ყირგიზეთი", "Kyrgyzstan", "Киргизия"),
                new Country("KH", "KHM", "116", "კამბოჯა", "Cambodia", "Камбоджа"),
                new Country("KI", "KIR", "296", "კირიბატი", "Kiribati", "Кирибати"),
                new Country("KM", "COM", "174", "კომორის კუნძულების კავშირი", "Comoros", "Коморы"),
                new Country("KN", "KNA", "659", "სენტ-კიტსი და ნევისი", "Saint Kitts and Nevis", "Сент-Китс и Невис"),
                new Country("KP", "PRK", "408", "ჩრდილოეთი კორეა", "Korea, Democratic People's Republic of", "КНДР"),
                new Country("KR", "KOR", "410", "სამხრეთი კორეა", "Korea, Republic of", "Республика Корея"),
                new Country("KW", "KWT", "414", "ქუვეითი", "Kuwait", "Кувейт"),
                new Country("KY", "CYM", "136", "კაიმანის კუნძულები", "Cayman Islands", "Каймановы острова"),
                new Country("KZ", "KAZ", "398", "ყაზახეთი", "Kazakhstan", "Казахстан"),
                new Country("LA", "LAO", "418", "ლაოსის სახალხო დემოკრატიული რესპუბლიკა", "Lao People's Democratic Republic", "Лаос"),
                new Country("LB", "LBN", "422", "ლიბანი", "Lebanon", "Ливан"),
                new Country("LC", "LCA", "662", "სენტ-ლუსია", "Saint Lucia", "Сент-Люсия"),
                new Country("LI", "LIE", "438", "ლიხტენშტაინი", "Liechtenstein", "Лихтенштейн"),
                new Country("LK", "LKA", "144", "შრი-ლანკა", "Sri Lanka", "Шри-Ланка"),
                new Country("LR", "LBR", "430", "ლიბერია", "Liberia", "Либерия"),
                new Country("LS", "LSO", "426", "ლესოთო", "Lesotho", "Лесото"),
                new Country("LT", "LTU", "440", "ლიტვა", "Lithuania", "Литва"),
                new Country("LU", "LUX", "442", "ლუქსემბურგი", "Luxembourg", "Люксембург"),
                new Country("LV", "LVA", "428", "ლატვია", "Latvia", "Латвия"),
                new Country("LY", "LBY", "434", "ლიბიის არაბული ჯამაჰირია", "Libya", "Ливия"),
                new Country("MA", "MAR", "504", "მაროკო", "Morocco", "Марокко"),
                new Country("MC", "MCO", "492", "მონაკო", "Monaco", "Монако"),
                new Country("MD", "MDA", "498", "მოლდოვას რესპუბლიკა", "Moldova, Republic of", "Молдавия"),
                new Country("ME", "MNE", "499", "ჩერნოგორია", "Montenegro", "Черногория"),
                new Country("MF", "MAF", "663", "წმინდა მარტინი (საფრანგეთის ნაწილი)", "Saint Martin (French part)", "Сен-Мартен"),
                new Country("MG", "MDG", "450", "მადაგასკარი", "Madagascar", "Мадагаскар"),
                new Country("MH", "MHL", "584", "მარშალის კუნძულები", "Marshall Islands", "Маршалловы Острова"),
                new Country("MK", "MKD", "807", "მაკედონია", "Macedonia, the former Yugoslav Republic of", "Македония"),
                new Country("ML", "MLI", "466", "მალი", "Mali", "Мали"),
                new Country("MM", "MMR", "104", "მიანმარი", "Myanmar", "Мьянма"),
                new Country("MN", "MNG", "496", "მონღოლეთი", "Mongolia", "Монголия"),
                new Country("MO", "MAC", "446", "მაკაო", "Macao", "Макао"),
                new Country("MP", "MNP", "580", "ჩრდილოეთი მარიანას კუნძულები", "Northern Mariana Islands", "Северные Марианские острова"),
                new Country("MQ", "MTQ", "474", "მარტინიკა", "Martinique", "Мартиника"),
                new Country("MR", "MRT", "478", "მავრიტანია", "Mauritania", "Мавритания"),
                new Country("MS", "MSR", "500", "მონსერატი", "Montserrat", "Монтсеррат"),
                new Country("MT", "MLT", "470", "მალტა", "Malta", "Мальта"),
                new Country("MU", "MUS", "480", "მავრიკი", "Mauritius", "Маврикий"),
                new Country("MV", "MDV", "462", "მალდივი", "Maldives", "Мальдивы"),
                new Country("MW", "MWI", "454", "მალავი", "Malawi", "Малави"),
                new Country("MX", "MEX", "484", "მექსიკა", "Mexico", "Мексика"),
                new Country("MY", "MYS", "458", "მალაიზია", "Malaysia", "Малайзия"),
                new Country("MZ", "MOZ", "508", "მოზამბიკი", "Mozambique", "Мозамбик"),
                new Country("NA", "NAM", "516", "ნამიბია", "Namibia", "Намибия"),
                new Country("NC", "NCL", "540", "ახალი კალედონია", "New Caledonia", "Новая Каледония"),
                new Country("NE", "NER", "562", "ნიგერი", "Niger", "Нигер"),
                new Country("NF", "NFK", "574", "ნორფოლკი (კუნძული)", "Norfolk Island", "Остров Норфолк"),
                new Country("NG", "NGA", "566", "ნიგერია", "Nigeria", "Нигерия"),
                new Country("NI", "NIC", "558", "ნიკარაგუა", "Nicaragua", "Никарагуа"),
                new Country("NL", "NLD", "528", "ნიდერლანდი", "Netherlands", "Нидерланды"),
                new Country("NO", "NOR", "578", "ნორვეგია", "Norway", "Норвегия"),
                new Country("NP", "NPL", "524", "ნეპალი", "Nepal", "Непал"),
                new Country("NR", "NRU", "520", "ნაურუ", "Nauru", "Науру"),
                new Country("NU", "NIU", "570", "ნიუე", "Niue", "Ниуэ"),
                new Country("NZ", "NZL", "554", "ახალი ზელანდია", "New Zealand", "Новая Зеландия"),
                new Country("OM", "OMN", "512", "ომანი", "Oman", "Оман"),
                new Country("PA", "PAN", "591", "პანამა", "Panama", "Панама"),
                new Country("PE", "PER", "604", "პერუ", "Peru", "Перу"),
                new Country("PF", "PYF", "258", "საფრანგეთის პოლინეზია", "French Polynesia", "Французская Полинезия"),
                new Country("PG", "PNG", "598", "პაპუა-ახალი გვინეა", "Papua New Guinea", "Папуа — Новая Гвинея"),
                new Country("PH", "PHL", "608", "ფილიპინები", "Philippines", "Филиппины"),
                new Country("PK", "PAK", "586", "პაკისტანი", "Pakistan", "Пакистан"),
                new Country("PL", "POL", "616", "პოლონეთი", "Poland", "Польша"),
                new Country("PM", "SPM", "666", "სენ-პიერი და მიკელონი", "Saint Pierre and Miquelon", "Сен-Пьер и Микелон"),
                new Country("PN", "PCN", "612", "პიტკერნის კუნძულები", "Pitcairn", "Острова Питкэрн"),
                new Country("PR", "PRI", "630", "პუერტო-რიკო", "Puerto Rico", "Пуэрто-Рико"),
                new Country("PS", "PSE", "275", "პალესტინის ტერიტორიები, ოკუპირებული", "Palestine, State of", "Государство Палестина"),
                new Country("PT", "PRT", "620", "პორტუგალია", "Portugal", "Португалия"),
                new Country("PW", "PLW", "585", "პალაუ", "Palau", "Палау"),
                new Country("PY", "PRY", "600", "პარაგვაი", "Paraguay", "Парагвай"),
                new Country("QA", "QAT", "634", "კატარი", "Qatar", "Катар"),
                new Country("RE", "REU", "638", "რეუნიონი", "Réunion", "Реюньон"),
                new Country("RO", "ROU", "642", "რუმინეთი", "Romania", "Румыния"),
                new Country("RS", "SRB", "688", "სერბეთი", "Serbia", "Сербия"),
                new Country("RU", "RUS", "643", "რუსეთი", "Russian Federation", "Россия"),
                new Country("RW", "RWA", "646", "რუანდა", "Rwanda", "Руанда"),
                new Country("SA", "SAU", "682", "საუდის არაბეთი", "Saudi Arabia", "Саудовская Аравия"),
                new Country("SB", "SLB", "090", "სოლომონის კუნძულები", "Solomon Islands", "Соломоновы Острова"),
                new Country("SC", "SYC", "690", "სეიშელი", "Seychelles", "Сейшельские Острова"),
                new Country("SD", "SDN", "729", "სუდანი", "Sudan", "Судан"),
                new Country("SE", "SWE", "752", "შვედეთი", "Sweden", "Швеция"),
                new Country("SG", "SGP", "702", "სინგაპური", "Singapore", "Сингапур"),
                new Country("SH", "SHN", "654", "წმინდა ელენეს კუნძული", "Saint Helena, Ascension and Tristan da Cunha", "Острова Святой Елены, Вознесения и Тристан-да-Кунья"),
                new Country("SI", "SVN", "705", "სლოვენია", "Slovenia", "Словения"),
                new Country("SJ", "SJM", "744", "სვალბარდი და იან-მაიენი", "Svalbard and Jan Mayen", "Шпицберген и Ян-Майен"),
                new Country("SK", "SVK", "703", "სლოვაკეთი", "Slovakia", "Словакия"),
                new Country("SL", "SLE", "694", "სიერა-ლეონე", "Sierra Leone", "Сьерра-Леоне"),
                new Country("SM", "SMR", "674", "სან-მარინო", "San Marino", "Сан-Марино"),
                new Country("SN", "SEN", "686", "სენეგალი", "Senegal", "Сенегал"),
                new Country("SO", "SOM", "706", "სომალი", "Somalia", "Сомали"),
                new Country("SR", "SUR", "740", "სურინამი", "Suriname", "Суринам"),
                new Country("ST", "STP", "678", "სან-ტომე და პრინსიპი", "Sao Tome and Principe", "Сан-Томе и Принсипи"),
                new Country("SV", "SLV", "222", "სალვადორი", "El Salvador", "Сальвадор"),
                new Country("SY", "SYR", "760", "სირიის არაბთა რესპუბლიკა", "Syrian Arab Republic", "Сирия"),
                new Country("SZ", "SWZ", "748", "სვაზილენდი", "Swaziland", "Свазиленд"),
                new Country("TC", "TCA", "796", "ტერქსისა და კაიკოსის კუნძულები", "Turks and Caicos Islands", "Тёркс и Кайкос"),
                new Country("TD", "TCD", "148", "ჩადი", "Chad", "Чад"),
                new Country("TF", "ATF", "260", "საფრანგეთის სამხრეთული და ანტარქტიდული ტერიტორია", "French Southern Territories", "Французские Южные и Антарктические Территории"),
                new Country("TG", "TGO", "768", "ტოგო", "Togo", "Того"),
                new Country("TH", "THA", "764", "ტაილანდი", "Thailand", "Таиланд"),
                new Country("TJ", "TJK", "762", "ტაჯიკეთი", "Tajikistan", "Таджикистан"),
                new Country("TK", "TKL", "772", "ტოკელაუ", "Tokelau", "Токелау"),
                new Country("TL", "TLS", "626", "აღმოსავლეთი ტიმორი", "Timor-Leste", "Восточный Тимор"),
                new Country("TM", "TKM", "795", "თურქმენეთი", "Turkmenistan", "Туркмения"),
                new Country("TN", "TUN", "788", "ტუნისი", "Tunisia", "Тунис"),
                new Country("TO", "TON", "776", "ტონგა", "Tonga", "Тонга"),
                new Country("TR", "TUR", "792", "თურქეთი", "Turkey", "Турция"),
                new Country("TT", "TTO", "780", "ტრინიდადი და ტობაგო", "Trinidad and Tobago", "Тринидад и Тобаго"),
                new Country("TV", "TUV", "798", "ტუვალუ", "Tuvalu", "Тувалу"),
                new Country("TW", "TWN", "158", "ტაივანი", "Taiwan, Province of China", "Китайская Республика"),
                new Country("TZ", "TZA", "834", "ტანზანიის გაერთიანებული რესპუბლიკა", "Tanzania, United Republic of", "Танзания"),
                new Country("UA", "UKR", "804", "უკრაინა", "Ukraine", "Украина"),
                new Country("UG", "UGA", "800", "უგანდა", "Uganda", "Уганда"),
                new Country("UM", "UMI", "581", "აშშ-ის საგარეო პატარა კუნძულები", "United States Minor Outlying Islands", "Внешние малые острова (США)"),
                new Country("US", "USA", "840", "აშშ", "United States", "США"),
                new Country("UY", "URY", "858", "ურუგვაი", "Uruguay", "Уругвай"),
                new Country("UZ", "UZB", "860", "უზბეკეთი", "Uzbekistan", "Узбекистан"),
                new Country("VA", "VAT", "336", "ვატიკანი (წმინდა საყდარი)", "Holy See (Vatican City State)", "Ватикан"),
                new Country("VC", "VCT", "670", "სენტ-ვინსენტი და გრენადინები", "Saint Vincent and the Grenadines", "Сент-Винсент и Гренадины"),
                new Country("VE", "VEN", "862", "ვენესუელა", "Venezuela, Bolivarian Republic of", "Венесуэла"),
                new Country("VG", "VGB", "092", "ვირჯინის კუნძულები (ბრიტანეთის)", "Virgin Islands, British", "Британские Виргинские острова"),
                new Country("VI", "VIR", "850", "ვირჯინის კუნძულები (აშშ-ის)", "Virgin Islands, U.S.", "Американские Виргинские острова"),
                new Country("VN", "VNM", "704", "ვიეტნამი", "Viet Nam", "Вьетнам"),
                new Country("VU", "VUT", "548", "ვანუატუ", "Vanuatu", "Вануату"),
                new Country("WF", "WLF", "876", "უოლისი და ფუტუნა", "Wallis and Futuna", "Уоллис и Футуна"),
                new Country("WS", "WSM", "882", "სამოა", "Samoa", "Самоа"),
                new Country("YE", "YEM", "887", "იემენი", "Yemen", "Йемен"),
                new Country("YT", "MYT", "175", "მაიოტა", "Mayotte", "Майотта"),
                new Country("ZA", "ZAF", "710", "სამხრეთ აფრიკა", "South Africa", "ЮАР"),
                new Country("ZM", "ZMB", "894", "ზამბია", "Zambia", "Замбия"),
                new Country("ZW", "ZWE", "716", "ზიმბაბვე", "Zimbabwe", "Зимбабве")
            };
        }

        private static IEnumerable<Country> InternalGetCountries()
        {
            return _countries ?? GetCountries();
        }

        public static Country Find(string code)
        {
            if (string.IsNullOrWhiteSpace(code)) return null;

            var tmp = InternalGetCountries().FirstOrDefault(c =>
                c.Alpha2Code.Equals(code, StringComparison.InvariantCultureIgnoreCase) ||
                c.Alpha3Code.Equals(code, StringComparison.InvariantCultureIgnoreCase) ||
                c.NumericCode.Equals(code, StringComparison.InvariantCultureIgnoreCase)
                );
            return tmp != null ? tmp.Copy() : null;
        }


        public static string[] GetAlpha2Codes()
        {
            return InternalGetCountries().Select(c => c.Alpha2Code).ToArray();
        }
        public static string[] GetAlpha3Codes()
        {
            return InternalGetCountries().Select(c => c.Alpha3Code).ToArray();
        }
        public static string[] GetNumericCodes()
        {
            return InternalGetCountries().Select(c => c.NumericCode).ToArray();
        }

        public static string[] GetGeorgianNames()
        {
            return InternalGetCountries().Select(c => c.Georgian).ToArray();
        }
        public static string[] GetEnglishNames()
        {
            return InternalGetCountries().Select(c => c.English).ToArray();
        }
        public static string[] GetRussianNames()
        {
            return InternalGetCountries().Select(c => c.Russian).ToArray();
        }
    }
}
