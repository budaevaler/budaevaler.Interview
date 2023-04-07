# Тестовое задание

## Задача 1
Написать SQL запрос, который выведет все возможные сочетания элементов табличной переменной при n = 8 (количество элементов) и k = 5 (количество позиций).

Ответ расположен по пути [src/Task1](https://github.com/budaevaler/budaevaler.Interview/tree/master/src/Task1).

## Задача 2
Реализовать интерфейс таким образом, чтобы он описывал замкнутую колекцию объектов, по которой можно бесконечно перемещаться в прямом и обратном направлении с помощью методов MoveNext и MoveBack. 

Для получения текущего, предыдущего и следующего элемента коллекции интерфейс определяет соответствующие свойства Current, Previous и Next. Свойство Head возвращает элемент коллекции с нулевым индексом. Обращение к данным свойствам пустой коллекции не должно приводить к исключениям. Событие HeadReached должно вызываться при прохождении через элемент с нулевым индексом при движении в любом направлении.

Для проверки корректности работы класса необходимо написать Unit тесты с использованием любого тестового фреймворка.
````
public interface IClosedList<T> : IList<T>
{
    void MoveNext(int step = 1);
    void MoveBack(int step = 1);

    T Head { get; }
    T Current { get; }
    T Previous { get; }
    T Next { get; }

    event EventHandler<T> HeadReached;
}
````
Для выполнения задания было создано [консольное приложение](https://github.com/budaevaler/budaevaler.Interview/tree/master/src/Task2), которое содержит 2 реализации интерфейса:

1. [LinkedClosedList\<T\>](https://github.com/budaevaler/budaevaler.Interview/blob/master/src/Task2/LinkedClosedList.cs) - свзанный кольцевой список c узлами [Node\<T\>](https://github.com/budaevaler/budaevaler.Interview/blob/master/src/Task2/Node.cs).
2. [ClosedList\<T\>](https://github.com/budaevaler/budaevaler.Interview/blob/master/src/Task2/ClosedList.cs) - на основе List\<T\>. 

Для LinkedClosedList\<T\> написаны Unit-тесты с использованием xUnit.

В файле Program.cs добавлена демонстрация бесконечного обхода списка.
