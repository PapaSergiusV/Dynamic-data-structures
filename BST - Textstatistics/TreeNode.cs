using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextStatistics
{
    // --------------------------- Узел бинарного дерева ---------------------------
    /// <summary>
    /// Узел бинарного дерева
    /// </summary>
    public class TreeNode<T>

    {
        /// <summary>
        /// Поле данных
        /// </summary>
        public T data;

        /// <summary>
        /// Левое поддерево
        /// </summary>
        public TreeNode<T> left;

        /// <summary>
        /// Правое поддерево
        /// </summary>
        public TreeNode<T> right;

        /// <summary>
        /// Инициализирует узел бинарного дерева значением data поля данных
        /// и поддеревьями left, right
        /// </summary>
        /// <param name="data">Значение поля данных узла</param>
        /// <param name="left">Левое поддерево</param>
        /// <param name="right">Правое поддерево</param>
        public TreeNode(T data, TreeNode<T> left = null, TreeNode<T> right = null)
        {
            this.data = data;
            this.left = left;
            this.right = right;
        }

    }



}
