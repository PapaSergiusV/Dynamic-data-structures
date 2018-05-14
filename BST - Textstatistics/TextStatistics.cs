using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace TextStatistics
{
    class TextStatistics
    {
        private TreeNode<CntString> _mainTree;
        
        /// <summary>
        /// Кол-во слов в тексте
        /// </summary>
        public int Count
        {
            get
            {
                return this._Count(this._mainTree);
            }
        }

        /// <summary>
        /// Индексатор, возвращает кол-во повторений слова в тексте
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public int this[string str]
        {
            get
            {
                var node = this._GetNode(this._mainTree, str);
                return node != null ? node.data.count : 0;
            }
        }

        /// <summary>
        /// Конструктор класса TextStatistics
        /// </summary>
        /// <param name="text">Текст для формирования статистики</param>
        public TextStatistics(string text)
        {
            text = Regex.Replace(text, @"\s{2,}", " ");
            text = Regex.Replace(text, @"^\s{1}", "");
            this._mainTree = null;
            
            foreach (var x in text.Split(' '))
            {
                this.AddWord(x);
            }
        }

        /// <summary>
        /// Рекурсивная вспомогательная функция возвращающая элемент с искомым словом
        /// </summary>
        /// <param name="root">Ссылка на корень дерева</param>
        /// <param name="x">Слово</param>
        private TreeNode<CntString> _GetNode(TreeNode<CntString> root, string x)
        {
            if (root == null)
                return null;
            if (root.data.s == x)
                return root;
            var left = _GetNode(root.left, x);
            if (left != null)
                return left;
            return _GetNode(root.right, x);
        }

        /// <summary>
        /// Рекурсивная функция, возвращает кол-во слов в дереве
        /// </summary>
        /// <param name="root">Ссылка на корень дерева</param>
        /// <returns></returns>
        private int _Count(TreeNode<CntString> root)
        {
            if (root == null)
                return 0;
            else
                return root.data.count + this._Count(root.left) + this._Count(root.right);
        }

        /// <summary>
        /// Функция проверяет, содержится ли слово в БДП статистики слов
        /// </summary>
        /// <param name="str">Слово</param>
        /// <returns></returns>
        public bool Contains(string str)
        {
            return this._ContainsWord(this._mainTree, str);
        }

        /// <summary>
        /// Рекурсивная вспомогательная функция функции Contains
        /// </summary>
        /// <param name="root">Ссылка на корень дерева</param>
        /// <param name="x">Слово</param>
        private bool _ContainsWord(TreeNode<CntString> root, string x)
        {
            if (root == null)
                return false;
            if (x == root.data.s)
                return true;
            return (this._ContainsWord(root.left, x) || this._ContainsWord(root.right, x));
        }

        /// <summary>
        /// Добавление слова в БДП статистики слов
        /// </summary>
        /// <param name="str">Слово</param>
        public void AddWord(string str)
        {
            this._AddWord(ref this._mainTree, str);
        }

        /// <summary>
        /// Рекурсивная вспомогательная функция функции AddWord
        /// </summary>
        /// <param name="root">Ссылка на корень дерева</param>
        /// <param name="x">Слово</param>
        private void _AddWord(ref TreeNode<CntString> root, string x)
        {
            if (root == null)
                root = new TreeNode<CntString>(new CntString(x, 1));
            else
            {
                var compare = x.CompareTo(root.data.s);
                if (compare == 0)
                    root.data.count++;
                else if (compare < 0)
                {
                    if (root.left == null)
                        root.left = new TreeNode<CntString>(new CntString(x, 1));
                    else
                        this._AddWord(ref root.left, x);
                }
                else
                {
                    if (root.right == null)
                        root.right = new TreeNode<CntString>(new CntString(x, 1));
                    else
                        this._AddWord(ref root.right, x);
                }
            }
        }

        /// <summary>
        /// Возвращает одно слово, встречающееся минимальное кол-во раз
        /// </summary>
        /// <param name="noLess">Кол-во повторений - не менее этого значения</param>
        /// <returns></returns>
        public string GetWordMinCount(int noLess = 0)
        {
            var nodeMinCount = this.GetNodeMinCount(this._mainTree, noLess);
            return nodeMinCount != null ? nodeMinCount.data.s : null;
        }

        /// <summary>
        /// Возвращает элемент БДП со словом, встречающимся минимальное кол-во раз
        /// </summary>
        /// <param name="noLess">Кол-во повторений - не менее этого значения</param>
        /// <returns></returns>
        public TreeNode<CntString> GetNodeMinCount(TreeNode<CntString> root, int noLess = 0)
        {
            if (root == null)
                return null;
            var left = GetNodeMinCount(root.left, noLess);
            var right = GetNodeMinCount(root.right, noLess);
            TreeNode<CntString> min = null;

            if (left != null)
            {
                min = left;
                if (right != null)
                    min = min.data.count < right.data.count ? min : right;
                if (root.data.count >= noLess)
                    min = min.data.count < root.data.count ? min : root;
            }
            else if (right != null)
            {
                min = right;
                if (root.data.count >= noLess)
                    min = min.data.count < root.data.count ? min : root;
            }
            else if (root.data.count >= noLess)
                min = root;

            return min;
        }

        /// <summary>
        /// Возвращает одно слово, встречающееся минимальное кол-во раз
        /// </summary>
        /// <param name="noLess">Кол-во повторений - не менее этого значения</param>
        /// <returns></returns>
        public string GetWordMaxCount(int noMore = int.MaxValue)
        {
            var nodeMinCount = this.GetNodeMaxCount(this._mainTree, noMore);
            return nodeMinCount != null ? nodeMinCount.data.s : null;
        }

        /// <summary>
        /// Возвращает элемент БДП со словом, встречающимся минимальное кол-во раз
        /// </summary>
        /// <param name="noLess">Кол-во повторений - не менее этого значения</param>
        /// <returns></returns>
        public TreeNode<CntString> GetNodeMaxCount(TreeNode<CntString> root, int noMore = int.MaxValue)
        {
            if (root == null)
                return null;
            var left = GetNodeMaxCount(root.left, noMore);
            var right = GetNodeMaxCount(root.right, noMore);
            TreeNode<CntString> max = null;

            if (left != null)
            {
                max = left;
                if (right != null)
                    max = max.data.count > right.data.count ? max : right;
                if (root.data.count <= noMore)
                    max = max.data.count > root.data.count ? max : root;
            }
            else if (right != null)
            {
                max = right;
                if (root.data.count <= noMore)
                    max = max.data.count > root.data.count ? max : root;
            }
            else if (root.data.count <= noMore)
                max = root;

            return max;
        }
        /// <summary>
        /// Получение списка с информацией о словах, начинающихся на заданную непустую подстроку
        /// </summary>
        /// <param name="substr">Подстрока, с которой должны начинаться слова для списка</param>
        /// <returns></returns>
        public LinkedList<CntString> WordsList(string substr)
        {
            var list = new LinkedList<CntString>();
            this._WordsList(this._mainTree, ref list, @"^" + substr);
            return list;
        }

        /// <summary>
        /// Рекурсивная вспомогательная функция функции AddWord
        /// </summary>
        /// <param name="root">Ссылка на корень дерева</param>
        /// <param name="x">Слово</param>
        private void _WordsList(TreeNode<CntString> root, ref LinkedList<CntString> list, string x)
        {
            if (root == null)
                return;
            this._WordsList(root.left, ref list, x);
            if (Regex.IsMatch(root.data.s, x))
                list.AddLast(root.data);
            this._WordsList(root.right, ref list, x);
        }

        /// <summary>
        /// Печать содержимого БДП по алфавиту
        /// </summary>
        public void Print()
        {
            this._InfixPrintTree(this._mainTree);
        }
        /// <summary>
        /// Печать содержимого БДП по алфавиту с переходом на следующую строку
        /// </summary>
        public void PrintLn()
        {
            this._InfixPrintlnTree(this._mainTree);
        }
        /// <summary>
        /// Вспомогательная процедура печати содержимого бинарного дерева 
        /// при инфиксном обходе
        /// </summary>
        /// <param name="root">Корень бинарного дерева</param>
        private void _InfixPrintTree<T>(TreeNode<T> root)
        {
            if (root == null) return;
            this._InfixPrintTree<T>(root.left);
            Console.Write(root.data + " ");
            this._InfixPrintTree<T>(root.right);

        }
        /// <summary>
        /// Вспомогательная процедура печати содержимого бинарного дерева 
        /// при инфиксном обходе
        /// </summary>
        /// <param name="root">Корень бинарного дерева</param>
        private void _InfixPrintlnTree<T>(TreeNode<T> root)
        {
            if (root == null) Console.WriteLine("<empty tree>");
            else
            {
                this._InfixPrintTree<T>(root);
                Console.WriteLine();
            }
        }
    }
}