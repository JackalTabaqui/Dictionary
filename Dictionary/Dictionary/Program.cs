using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Dictionary
{
    class Node
    // рекурсивный класс узла
    {
        public double key;
        public Node parent;
        public Node left;
        public Node right;
        public Node(double data, Node left, Node right, Node parent)
        // конструктор
        {
            this.key = data;
            this.left = left;
            this.right = right;
            this.parent = parent;
        }
        public void WriteNode(StreamWriter w)
        {
            if (this != null)
            {
                if (left != null)
                {
                    w.WriteLine(Convert.ToString(key) + "->" + Convert.ToString(left.key) + ";");
                    left.WriteNode(w);
                }
                if (right != null)
                {
                    w.WriteLine(Convert.ToString(key) + "->" + Convert.ToString(right.key) + ";");
                    right.WriteNode(w);
                }
            }
        }
    }
    class DictionaryBT
    // класс дерева
    {
        public Node top; // корень дерева
        public double Maximum()
        // поиск максимального по значению узла
        {
            if (top != null)
            {
                Node p = top;
                while (p.right != null) p = p.right;
                return p.key;
            }
            else throw new InvalidOperationException("You should fill your tree first");
        }
        public double Minimum()
        // поиск минимального по значению узла
        {
            if (top != null)
            {
                Node p = top;
                while (p.left != null) p = p.left;
                return p.key;
            }
            else throw new InvalidOperationException("You should fill your tree first");
        }
        private void Add(Node p, double val)
        // рекурсивная функция добавления элемента со значением val
        {
            if (p.key < val)
            {
                if (p.right == null)
                    p.right = new Node(val, null, null, p);
                else
                    Add(p.right, val);
            }
            else
            {
                if (p.left == null)
                    p.left = new Node(val, null, null, p);
                else
                    Add(p.left, val);
            }
        }
        public void Add(double value)
        // "обёртка" для функции Add
        {
            if (top == null)
            {
                top = new Node(value, null, null, null);
                return;
            }
            Add(top, value);
        }
        private bool SearchBool(ref Node t, double k)
        // рекурсивная функция поиска элемента по значению (возвращает true/false)
        {
            if ((top == null) || (k != t.key)) return false;
            if ((t == null) || (k == t.key))
                return true;
            else
                if (k < t.key)
                    return SearchBool(ref t.left, k);
                else return SearchBool(ref t.right, k);
        }
        public bool SearchBool(double val)
        // "обёртка" для функции SearchBool
        {
            return SearchBool(ref top, val);
        }
        private Node Search(ref Node t, double k)
        // рекурсивная функция поиска элемента по значению (возвращает элемент)
        {
            if ((t == null) || (k == t.key))
                return t;
            else
                if (k < t.key)
                    return Search(ref t.left, k);
                else return Search(ref t.right, k);
        }
        public Node Search(double val)
        // "обёртка" для функции Search
        {
            return Search(ref top, val);
        }
        Node q = new Node(0, null, null, null);
        private void Del(ref Node r)
        {
            if (r.right != null)
                Del(ref r.right);
            else
            {
                q.key = r.key;
                q = r;
                r = r.left;
            }
        }
        private void Del0(int data, ref Node p)
        {
            if (p != null)
                if (data < p.key)
                    Del0(data, ref p.left);
                else
                    if (data > p.key)
                        Del0(data, ref p.right);
                    else
                    {
                        q = p;
                        if (q.right == null)
                            p = q.left;
                        else
                            if (q.left == null)
                                p = q.right;
                            else
                                Del(ref q.left);
                    }
        }
        public void Delete(int data)
        // удаление элемента по значению
        {
            Del0(data, ref top);
        }
        public void WriteTree(string path)
        // запись в текстовый файл
        {
            FileStream f = new FileStream(path, FileMode.Create);
            StreamWriter w = new StreamWriter(f);
            w.WriteLine("digraph G {");
            top.WriteNode(w);
            w.WriteLine("}");
            w.Close();
            f.Close();
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
        }
    }
}
