using lab;
using LibraryLab10;
using System.Drawing;

namespace TestProject;

[TestClass]
public class UnitTest
{
    // Проверка на пустой список
    [TestMethod]
    public void TestEmptyListCount()
    {
        MyList<int> list = new MyList<int>();
        Assert.AreEqual(0, list.Count);
    }

    // Проверка на добавление элемента в список
    [TestMethod]
    public void TestAddItem()
    {
        MyList<int> list = new MyList<int>();
        list.Add(5);
        Assert.AreEqual(1, list.Count);
    }


    // Проверка на наличие элемента в списке
    [TestMethod]
    public void TestContainsItem()
    {
        MyList<int> list = new MyList<int>();
        list.Add(5);
        list.Add(10);
        Assert.IsTrue(list.Contains(5));
        Assert.IsFalse(list.Contains(15));
    }

    // Проверка на удаление элемента из списка
    [TestMethod]
    public void TestRemoveItem()
    {
        MyList<int> list = new MyList<int>();
        list.Add(5);
        list.Add(10);
        Assert.IsTrue(list.Remove(5));
        Assert.AreEqual(1, list.Count);
        Assert.IsFalse(list.Remove(15)); // Попытка удалить несуществующий элемент
    }

    // Проверка на очистку списка
    [TestMethod]
    public void TestClearList()
    {
        MyList<int> list = new MyList<int>();
        list.Add(5);
        list.Add(10);
        list.Clear();
        Assert.AreEqual(0, list.Count);
    }

    // Проверка на копирование в массив
    [TestMethod]
    public void TestCopyToArray()
    {
        MyList<int> list = new MyList<int>();
        list.Add(5);
        list.Add(10);
        list.Add(15);

        var array = new int[5];
        list.CopyTo(array, 1);

        Assert.AreEqual(0, array[0]); // Индекс 0 не изменен
        Assert.AreEqual(5, array[1]); // Индекс 1 должен быть равен 5
        Assert.AreEqual(10, array[2]);
        Assert.AreEqual(15, array[3]);
    }

    // Проверка на исключения при некорректном добавлении в список
    [TestMethod]
    public void TestAddItemWithInvalidIndex()
    {
        MyList<int> list = new MyList<int>();
        list.Add(5);
        list.Add(1, 10);

        Assert.ThrowsException<Exception>(() => list.Add(5, 20)); // Попытка добавить на недопустимую позицию
    }

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
    public void ResizeMaintainsElements()
    {
        var set = new MyOpenHS<int>(capacity: 2, loadFactor: 0.5);
        set.Add(1);
        set.Add(2); // триггерит Resize()

        Assert.IsTrue(set.Contains(1));
        Assert.IsTrue(set.Contains(2));
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
}