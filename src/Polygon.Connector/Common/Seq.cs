using System;
using System.Collections.Generic;

namespace Polygon.Connector.Common
{
    /// <summary>
    /// Вспомогательный класс для работы с <see cref="IEnumerable{T}"/>
    /// </summary>
    public static class Seq
    {
        /// <summary>
        /// Возвращает <see cref="IEnumerable{T}"/> из списка параметров
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="items"></param>
        /// <returns></returns>
        public static IEnumerable<T> Enumerable<T>(params T[] items)
        {
            return items;
        }

        /// <summary>
        /// Создает <see cref="KeyValuePair{TX, TY}"/> из пары ключ-значение.
        /// За счет вывода типов можно не указывать параметры TX и TY
        /// </summary>
        /// <typeparam name="TX"></typeparam>
        /// <typeparam name="TY"></typeparam>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public static KeyValuePair<TX, TY> KeyValue<TX, TY>(TX x, TY y)
        {
            return new KeyValuePair<TX, TY>(x, y);
        }

        /// <summary>
        /// Объединяет две последовательности попарно.
        /// </summary>
        /// <typeparam name="TX"></typeparam>
        /// <typeparam name="TY"></typeparam>
        /// <typeparam name="TR"></typeparam>
        /// <param name="xs"></param>
        /// <param name="ys"></param>
        /// <param name="selector"></param>
        /// <returns></returns>
        public static IEnumerable<TR> ZipWith<TX, TY, TR>(this IEnumerable<TX> xs, IEnumerable<TY> ys, Func<TX, TY, TR> selector)
        {
            var iteratorX = xs.GetEnumerator();
            var iteratorY = ys.GetEnumerator();

            while (iteratorX.MoveNext() && iteratorY.MoveNext())
            {
                yield return selector(iteratorX.Current, iteratorY.Current);
            }
        }

        /// <summary>
        /// Исполняет указанную операцию на всех элементах списка
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="xs"></param>
        /// <param name="operation"></param>
        public static void ForEach<T>(this IEnumerable<T> xs, Action<T> operation)
        {
            foreach (var x in xs)
            {
                operation(x);
            }
        }

        /// <summary>
        /// Выкидывает элемент с индексом <paramref name="index"/>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="xs"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public static IEnumerable<T> SkipByIndex<T>(this IEnumerable<T> xs, int index)
        {
            var i = 0;
            foreach (var x in xs)
            {
                if (i != index)
                {
                    yield return x;
                }

                i++;
            }
        }

        /// <summary>
        /// Поиск позиции для вставки элемента в список с сохранением упорядоченности
        /// </summary>
        /// <typeparam name="TElement"></typeparam>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="elements">
        /// Список элементов
        /// </param>
        /// <param name="keySelector">
        /// Выборка ключа для сравнения
        /// </param>
        /// <param name="key">
        /// Значение ключа для сравнения
        /// </param>
        /// <returns>
        /// Позицию для вставки элемента
        /// </returns>
        public static int FindIndex<TElement, TKey>(this IList<TElement> elements, Func<TElement, TKey> keySelector, TKey key)
            where TKey : IComparable
        {
            return FindIndexImpl(elements, keySelector, key, 0, elements.Count - 1);
            // return elements.TakeWhile(element => keySelector(element).CompareTo(key) <= 0).Count();
        }
        
        private static int FindIndexImpl<TElement, TKey>(
            IList<TElement> elements, 
            Func<TElement, TKey> keySelector, 
            TKey key,
            int low, 
            int high)
                where TKey : IComparable
        {
            if (high - low < 0)
            {
                return low;
            }

            var middle = (high + low) / 2;
            var middleKey = keySelector(elements[middle]);
            if (middleKey.CompareTo(key) <= 0)
            {
                return FindIndexImpl(elements,keySelector, key, middle + 1, high);
            }

            return FindIndexImpl(elements,keySelector, key, low, middle - 1);
        }

        /// <summary>
        /// Поиск индекса элемента
        /// </summary>
        /// <typeparam name="T">
        /// Тип элемента
        /// </typeparam>
        /// <param name="items">
        /// Список элементов
        /// </param>
        /// <param name="condition">
        /// Условие поиска
        /// </param>
        /// <returns>
        /// Индекс элемента, если он найден. -1 - в противном случае.
        /// </returns>
        public static int IndexOf<T>(this IEnumerable<T> items, Func<T, bool> condition)
        {
            var index = 0;
            foreach (var item in items)
            {
                if (condition(item))
                {
                    return index;
                }

                index++;
            }

            return -1;
        }

        /// <summary>
        ///     Применить фильтр к списку
        /// </summary>
        /// <typeparam name="T">
        ///     Тип элемента
        /// </typeparam>
        /// <param name="items">
        ///     Список элементов
        /// </param>
        /// <param name="filter">
        ///     Функция фильтрации списка
        /// </param>
        /// <returns>
        ///     Отфильтрованный список
        /// </returns>
        public static IEnumerable<T> Filter<T>(this IEnumerable<T> items, Func<IEnumerable<T>, IEnumerable<T>> filter)
        {
            return filter(items);
        }

        /// <summary>
        ///     Удалить из массива i-й элемент
        /// </summary>
        public static T[] RemoveElementAt<T>(this T[] array, int index)
        {
            if (array == null)
            {
                throw new ArgumentNullException(nameof(array));
            }

            if (index < 0 || index >= array.Length)
            {
                throw new ArgumentOutOfRangeException(nameof(index));
            }

            var result = new T[array.Length - 1];
            var j = 0;
            for (var i = 0; i < result.Length; i++)
            {
                if (j == index)
                {
                    j++;
                }

                result[i] = array[j];
                j++;
            }

            return result;
        }
    }
}

