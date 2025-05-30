﻿using System.Runtime.InteropServices;
using System.Collections.Generic;
using System;
using LibraryLab10;

namespace LibraryLab10
{
    public class Clock : IInit, IComparable<Clock>
    {
        // Вводим публичные переменные для этого класса и всех его производных
        public string? brand { get; set; } // бренд часов
        protected int Year; // год выпуска

        public IdNumber ID { get; set; }
        public int year
        {
            get => Year;
            set
            {
                if (value < 725)
                {
                    throw new ArgumentOutOfRangeException("Первые механические часы изобрели в 725 году");
                }
                else if (value > 2025)
                {
                    throw new ArgumentOutOfRangeException("Часы не могут быть созданы позже нынешнего года");
                }
                Year = value;
            }
        }

        protected static Random rnd = new Random();

        public static string[] Brand = {
       "Rolex", "Patek Philippe", "Audemars Piguet", "Vacheron Constantin", "Tag Heuer",
      "Omega", "Breitling", "Jaeger-LeCoultre", "IWC Schaffhausen", "Panerai",
      "Seiko", "Casio", "Breguet", "Richard Mille", "Hublot",
       "Bulgari", "Zenith", "Longines", "Girard-Perregaux", "Tissot",
        "Chopard", "Blancpain", "Montblanc", "Baume & Mercier", "Bell & Ross",
        "Frédérique Constant", "Citizen", "Grand Seiko", "Fossil", "Movado",
       "Hamilton", "Maurice Lacroix", "Mido", "Rado", "Sinn",
       "Arnold & Son", "Luminox", "Corum", "Ulysse Nardin", "Nomos Glashütte",
       "Alpina", "Raymond Weil", "Oris", "MeisterSinger", "H. Moser & Cie",
       "Parmigiani Fleurier", "A. Lange & Söhne", "Piaget", "Glashütte Original", "MB&F",
      "Laco", "Vulcain", "Tudor", "Eberhard & Co.", "Chanel",
      "F.P. Journe", "Chopard", "Jacob & Co.", "Laurent Ferrier", "AkriviA",
      "De Bethune", "Vacheron Constantin", "Blancpain", "Piaget", "Jaquet Droz",
      "Linde Werdelin", "Tiffany & Co.", "Bremont", "Bell & Ross", "Vulcain",
      "Breguet", "Zenith", "MB&F", "Richard Mille", "Arnold & Son",
      "Sarpaneva", "Ralph Lauren", "Girard-Perregaux", "Hublot", "Cartier",
     "Montblanc", "Maurice Lacroix", "Raymond Weil", "Nomos", "F.P. Journe",
      "Luminox", "Corum", "Alpina", "Sinn", "Ulysse Nardin",
      "Franck Muller", "Breitling", "Jaeger-LeCoultre", "Audemars Piguet", "Bvlgari"};

        public static int[] YearRelease = { 2025, 2024, 2023, 2022, 2021, 2020, 2019, 2018, 2017, 2016, 2015, 2014, 2013, 2012, 2011, 2010, 2009, 2008, 2007, 2006,
        2005, 2004, 2003, 2002, 2001, 2000, 1999, 1998, 1997, 1996, 1995, 1994, 1993, 1992, 1991, 1990, 1989, 1988, 1987, 1986, 1985, 1984, 1983, 1982, 1981, 1980,
        1979, 1978, 1977, 1976, 1975, 1974, 1973, 1972, 1971, 1970, 1969, 1968, 1967, 1966, 1965, 1964, 1963, 1962, 1961, 1960, 1959, 1958, 1957, 1956, 1955, 1954,
        1953, 1952, 1951, 1950, 1949, 1948, 1947, 1946, 1945, 1944, 1943, 1942, 1941, 1940, 1939, 1938, 1937, 1936, 1935, 1934, 1933, 1932, 1931, 1930, 1929, 1928,
        1927, 1926, 1925, 1924, 1923, 1922, 1921, 1920, 1919, 1918, 1917, 1916, 1915, 1914, 1913, 1912, 1911, 1910, 1909, 1908, 1907, 1906, 1905, 1904, 1903, 1902,
        1901, 1900, 1899, 1898, 1897, 1896, 1895, 1894, 1893, 1892, 1891, 1890, 1889, 1888, 1887, 1886, 1885, 1884, 1883, 1882, 1881, 1880, 1879, 1878, 1877, 1876,
        1875, 1874, 1873, 1872, 1871, 1870, 1869, 1868, 1867, 1866, 1865, 1864, 1863, 1862, 1861, 1860, 1859, 1858, 1857, 1856, 1855, 1854, 1853, 1852, 1851, 1850,
        1849, 1848, 1847, 1846, 1845, 1844, 1843, 1842, 1841, 1840, 1839, 1838, 1837, 1836, 1835, 1834, 1833, 1832, 1831, 1830, 1829, 1828, 1827, 1826, 1825, 1824,
        1823, 1822, 1821, 1820, 1819, 1818, 1817, 1816, 1815, 1814, 1813, 1812, 1811, 1810, 1809, 1808, 1807, 1806, 1805, 1804, 1803, 1802, 1801, 1800, 1799, 1798,
        1797, 1796, 1795, 1794, 1793, 1792, 1791, 1790, 1789, 1788, 1787, 1786, 1785, 1784, 1783, 1782, 1781, 1780, 1779, 1778, 1777, 1776, 1775, 1774, 1773, 1772,
        1771, 1770, 1769, 1768, 1767, 1766, 1765, 1764, 1763, 1762, 1761, 1760, 1759, 1758, 1757, 1756, 1755, 1754, 1753, 1752, 1751, 1750, 1749, 1748, 1747, 1746,
        1745, 1744, 1743, 1742, 1741, 1740, 1739, 1738, 1737, 1736, 1735, 1734, 1733, 1732, 1731, 1730, 1729, 1728, 1727, 1726, 1725, 1724, 1723, 1722, 1721, 1720,
        1719, 1718, 1717, 1716, 1715, 1714, 1713, 1712, 1711, 1710, 1709, 1708, 1707, 1706, 1705, 1704, 1703, 1702, 1701, 1700, 1699, 1698, 1697, 1696, 1695, 1694,
        1693, 1692, 1691, 1690, 1689, 1688, 1687, 1686, 1685, 1684, 1683, 1682, 1681, 1680, 1679, 1678, 1677, 1676, 1675, 1674, 1673, 1672, 1671, 1670, 1669, 1668,
        1667, 1666, 1665, 1664, 1663, 1662, 1661, 1660, 1659, 1658, 1657, 1656, 1655, 1654, 1653, 1652, 1651, 1650, 1649, 1648, 1647, 1646, 1645, 1644, 1643, 1642,
        1641, 1640, 1639, 1638, 1637, 1636, 1635, 1634, 1633, 1632, 1631, 1630, 1629, 1628, 1627, 1626, 1625, 1624, 1623, 1622, 1621, 1620, 1619, 1618, 1617, 1616,
        1615, 1614, 1613, 1612, 1611, 1610, 1609, 1608, 1607, 1606, 1605, 1604, 1603, 1602, 1601, 1600, 1599, 1598, 1597, 1596, 1595, 1594, 1593, 1592, 1591, 1590,
        1589, 1588, 1587, 1586, 1585, 1584, 1583, 1582, 1581, 1580, 1579, 1578, 1577, 1576, 1575, 1574, 1573, 1572, 1571, 1570, 1569, 1568, 1567, 1566, 1565, 1564,
        1563, 1562, 1561, 1560, 1559, 1558, 1557, 1556, 1555, 1554, 1553, 1552, 1551, 1550, 1549, 1548, 1547, 1546, 1545, 1544, 1543, 1542, 1541, 1540, 1539, 1538,
        1537, 1536, 1535, 1534, 1533, 1532, 1531, 1530, 1529, 1528, 1527, 1526, 1525, 1524, 1523, 1522, 1521, 1520, 1519, 1518, 1517, 1516, 1515, 1514, 1513, 1512,
        1511, 1510, 1509, 1508, 1507, 1506, 1505, 1504, 1503, 1502, 1501, 1500 };

        // Прописываем конструкторы
        public Clock()
        {
            brand = "NoBrand";
            year = 2000;
            ID = new IdNumber(0);
        }

        public Clock(string brand, int year, int id)
        {
            this.brand=brand;
            this.year=year;
            ID = new IdNumber(id);
        }

        // Метод для вывода информации
        public virtual void Show()
        {
            Console.WriteLine($"Бренд часов: {brand}\n " +
                              $"Год выпуска: {year}\n ");
        }

        public virtual void Init()
        {
            string buffer;
            bool isChecked;

            Console.WriteLine("Введите бренд часов");
            brand = Console.ReadLine();

            do
            {
                Console.WriteLine("Введите год выпуска часов");
                buffer = Console.ReadLine();
                isChecked = int.TryParse(buffer, out Year);
            } while (!isChecked);
        }

        public virtual void RandomInit()
        {
            brand = Brand[rnd.Next(Brand.Length)];
            year = YearRelease[rnd.Next(YearRelease.Length)];
        }

        public virtual bool Equals(object? obj)
        {
            if (obj == null) return false;
            if (obj == this) return true;
            if (obj is SmartClock clock)
            {
                return this.brand == clock.brand && this.year == clock.year;
            }
            else
            {
                return false;
            }
        }

        public virtual void IRandomInit()
        {
            ID.Id = rnd.Next(0, 100);
            brand = Brand[rnd.Next(Brand.Length)];
            year = YearRelease[rnd.Next(YearRelease.Length)];
        }

        public override string ToString()
        {
            return "Бренд: " + brand + " Год выпуска: " + year.ToString();
        }
        public override int GetHashCode()
        {
            return brand.GetHashCode()^Year.GetHashCode()^ID.Id.GetHashCode();
        }

        public int CompareTo(Clock? other)
        {
            if (other == null) return -1;
            return brand.CompareTo(other.brand);
        }

        public Clock Clone()
        {
            return new Clock(this.brand, this.year, this.ID.Id);
        }
        public object ShallowCopy()
        {
            return this.MemberwiseClone();
        }

        public static bool operator <(Clock? firstClock, Clock? secondClock)
        {
            return firstClock.year < secondClock.year;
        }

        public static bool operator >(Clock? firstClock, Clock? secondClock)
        {
            return firstClock.year > secondClock.year;
        }
    }
}