using lab;
using LibraryLab10;
using System.Drawing;

namespace TestProject;

[TestClass]
public class UnitTest
{
    [TestMethod]
    public void TestPointConstraction()
    {
        PointList<int> point = new PointList<int>();
        Assert.AreEqual(0, point.data);
        Assert.AreEqual(null, point.next);
        Assert.AreEqual(null, point.pred);
    }

    // ----------------------------------------------------

    [TestMethod]
    public void AddElementIncreasesCount()
    {
        var set = new MyOpenHS<string>();
        set.Add("test");

        Assert.IsTrue(set.Contains("test"));
        Assert.AreEqual(1, set.Count);
    }

    [TestMethod]
    public void AddDuplicateElementDoesNotIncreaseCount()
    {
        var set = new MyOpenHS<string>();
        set.Add("test");
        set.Add("test");

        Assert.AreEqual(1, set.Count);
    }

    [TestMethod]
    public void ClearResetsSet()
    {
        var set = new MyOpenHS<string>();
        set.Add("a");
        set.Add("b");

        set.Clear();

        Assert.IsFalse(set.Contains("a"));
        Assert.IsFalse(set.Contains("b"));
    }

    [TestMethod]
    public void FindByKey_ElementAtPrimaryIndex_ReturnsValue()
    {
        var set = new MyOpenHS<string>(capacity: 10);
        string item = "findMe";
        set.Add(item);

        int key = Math.Abs(item.GetHashCode()) % 10;
        var result = set.FindByKey(key);

        Assert.AreEqual(item, result);
    }

    [TestMethod]
    public void FindByKey_ElementInChain_ReturnsValue()
    {
        var set = new MyOpenHS<string>(capacity: 3);

        // Строки с одинаковыми хешами (в .NET у "Aa" и "BB" одинаковый hashCode)
        string str1 = "Aa";
        string str2 = "BB";

        set.Add(str1);
        set.Add(str2);

        int key = Math.Abs(str2.GetHashCode()) % 3;
        var result = set.FindByKey(key);

        // Возможно, вернёт str1 (если он по ключу), но один из них должен быть найден
        Assert.IsNotNull(result);
        Assert.IsTrue(result == str1 || result == str2);
    }

    [TestMethod]
    public void FindByKey_NonExistentKey_ReturnsDefault()
    {
        var set = new MyOpenHS<string>(capacity: 10);
        set.Add("hello");

        int badKey = 999; // Слишком большой, за пределами индексов
        var result = set.FindByKey(badKey);

        Assert.IsNull(result);
    }

    [TestMethod]
    public void FindByKey_DeletedElement_ReturnsDefault()
    {
        var set = new MyOpenHS<string>(capacity: 10);
        string item = "deleteMe";
        set.Add(item);

        int key = Math.Abs(item.GetHashCode()) % 10;
        set.Remove(item);

        var result = set.FindByKey(key);

        Assert.IsNull(result);
    }

    [TestMethod]
    public void FindByKey_EmptyTable_ReturnsDefault()
    {
        var set = new MyOpenHS<string>(capacity: 10);

        var result = set.FindByKey(0);

        Assert.IsNull(result);
    }

    [TestMethod]
    public void ConstructorInvalidCapacityThrowsException()
    {
        Assert.ThrowsException<Exception>(() => new MyOpenHS<string>(-1));
    }

    [TestMethod]
    public void ConstructorInvalidLoadFactorThrowsException()
    {
        Assert.ThrowsException<Exception>(() => new MyOpenHS<string>(10, 1.5));
    }

    [TestMethod]
    public void TestPointHSConstraction()
    {
        PointHS<int> point = new PointHS<int>();
        Assert.AreEqual(0, point.data);
        Assert.AreEqual(false, point.isDeleted);
    }

    [TestMethod]
    public void RemoveElementAtPrimaryIndexSuccessfullyRemoves()
    {
        var set = new MyOpenHS<string>();
        set.Add("alpha");

        bool result = set.Remove("alpha");

        Assert.IsTrue(result);
        Assert.IsFalse(set.Contains("alpha"));
        Assert.AreEqual(0, set.Count);
    }

    [TestMethod]
    public void RemoveElementInCollisionChainSuccessfullyRemoves()
    {
        var set = new MyOpenHS<string>(capacity: 3); // Меньшая емкость для вероятности коллизий

        // Добавим два элемента с одинаковыми хешами (вручную "насильно")
        string str1 = "Aa"; // Эти строки имеют одинаковые хеши в .NET
        string str2 = "BB";

        set.Add(str1);
        set.Add(str2);

        bool result = set.Remove(str2);

        Assert.IsTrue(result);
        Assert.IsFalse(set.Contains(str2));
        Assert.IsTrue(set.Contains(str1));
    }

    [TestMethod]
    public void RemoveNullElementReturnsFalse()
    {
        var set = new MyOpenHS<string>();

        bool result = set.Remove(null);

        Assert.IsFalse(result);
    }

    [TestMethod]
    public void RemoveNonExistentElementReturnsFalse()
    {
        var set = new MyOpenHS<string>();
        set.Add("present");

        bool result = set.Remove("absent");

        Assert.IsFalse(result);
    }

    [TestMethod]
    public void RemoveByKeyElementAtPrimaryIndexSuccessfullyRemoves()
    {
        var set = new MyOpenHS<string>(capacity: 10);
        string item = "keyTest";
        set.Add(item);
        int key = Math.Abs(item.GetHashCode()) % 10;

        bool result = set.RemoveByKey(key);

        Assert.IsTrue(result);
        Assert.IsFalse(set.Contains(item));
    }

    [TestMethod]
    public void RemoveByKeyElementInChainSuccessfullyRemoves()
    {
        var set = new MyOpenHS<string>(capacity: 3);

        string str1 = "Aa"; // одинаковый хеш
        string str2 = "BB"; // одинаковый хеш

        set.Add(str1);
        set.Add(str2);

        int key = Math.Abs(str2.GetHashCode()) % 3;

        bool result = set.RemoveByKey(key);

        // Возможно, str1 окажется по ключу, а str2 дальше — тогда это не удалит str2
        // Проверяем, что по факту один из них удалён
        Assert.IsTrue(result);
        Assert.IsTrue(set.Count < 2);
    }

    [TestMethod]
    public void RemoveByKeyNonExistentKeyReturnsFalse()
    {
        var set = new MyOpenHS<string>(capacity: 10);
        set.Add("exists");

        int wrongKey = 999; // заведомо неправильный ключ

        bool result = set.RemoveByKey(wrongKey);

        Assert.IsFalse(result);
    }

    [TestMethod]
    public void RemoveByKeyAlreadyDeletedElementReturnsFalse()
    {
        var set = new MyOpenHS<string>(capacity: 10);
        string item = "deleteMe";
        set.Add(item);
        int key = Math.Abs(item.GetHashCode()) % 10;

        set.RemoveByKey(key); // Удаляем
        bool result = set.RemoveByKey(key); // Пробуем удалить снова

        Assert.IsFalse(result);
    }

    // --------------------------------------------

    // Тестируем конструктор по умолчанию
    [TestMethod]
    public void Constructor_Default_InitializesDataAndChildren()
    {
        var pointTree = new PointTree();

        Assert.IsNotNull(pointTree.data);   // Проверяем, что данные инициализированы
        Assert.IsNull(pointTree.left);      // Левый потомок равен null
        Assert.IsNull(pointTree.right);     // Правый потомок равен null
    }

    // Тестируем создание дерева с заданными данными
    [TestMethod]
    public void Constructor_ClockParameter_SetsCorrectly()
    {
        var clock = new Clock();  // Предположим, что класс Clock доступен
        var pointTree = new PointTree(clock);

        Assert.AreSame(clock, pointTree.data); // Данные совпадают с переданным объектом
        Assert.IsNull(pointTree.left);         // Нет левого потомка
        Assert.IsNull(pointTree.right);        // Нет правого потомка
    }

    // ------------------------------

    private MyList<Clock> myList;

    [TestInitialize]
    public void Setup()
    {
        myList = new MyList<Clock>();
    }

    #region Constructors tests

    [TestMethod]
    public void Constructor_EmptyConstructor_InitializesEmptyEnumerator()
    {
        var enumerator = new MyCollection<Clock>();
        Assert.IsNull(enumerator.begin);
        Assert.IsNull(enumerator.current);
    }
    #endregion

    #region Behavioral tests
    [TestMethod]
    public void Reset_AfterMoveNext_ResetsPositionBackToStart()
    {
        const int listSize = 3;
        var enumerator = new MyCollection<Clock>(listSize);
        enumerator.MoveNext(); // переходим вперёд
        enumerator.Reset();    // выполняем сброс
        Assert.AreSame(enumerator.begin, enumerator.current); // проверяем положение
    }

    #endregion

    #region Exception handling tests

    [TestMethod]
    public void Constructor_NegativeLengthThrowsArgumentOutOfRangeException()
    {
        Assert.ThrowsException<ArgumentOutOfRangeException>(() => new MyCollection<Clock>(-1));
    }

    #endregion

    #region IndexOf Method Tests

    [TestMethod]
    public void IndexOf_Method_NotImplemented_ThrowsNotImplementedException()
    {
        // Arrange
        Clock clock = new Clock { brand = "Rolex", year = 2020 };
        myList.Add(clock);

        // Act & Assert
        Assert.ThrowsException<NotImplementedException>(() => ((IList<Clock>)myList).IndexOf(clock));
    }

    #endregion

    #region Insert Method Tests

    [TestMethod]
    public void Insert_Method_NotImplemented_ThrowsNotImplementedException()
    {
        // Arrange
        Clock clock = new Clock { brand = "Rolex", year = 2020 };

        // Act & Assert
        Assert.ThrowsException<NotImplementedException>(() => ((IList<Clock>)myList).Insert(0, clock));
    }

    #endregion

    #region RemoveAt Method Tests

    [TestMethod]
    public void RemoveAt_Method_NotImplemented_ThrowsNotImplementedException()
    {
        // Arrange
        myList.Add(new Clock { brand = "Rolex", year = 2020 });

        // Act & Assert
        Assert.ThrowsException<NotImplementedException>(() => ((IList<Clock>)myList).RemoveAt(0));
    }

    #endregion
}