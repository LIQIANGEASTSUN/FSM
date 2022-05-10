using System;

namespace GraphicTree
{
    public interface ICustomIdentification<out T> where T : AbstractNode
    {
        T Create();

        string IdentificationName
        {
            get;
            set;
        }

        string Name
        {
            get;
            set;
        }

        int NodeType
        {
            get;
            set;
        }
    }

    public class CustomIdentification<T> : ICustomIdentification<T> where T : AbstractNode, new()
    {
        public CustomIdentification(string name, Type t)
        {
            Name = name;
            IdentificationName = t.Name;
            SetNodeType(t);
        }

        public CustomIdentification(string name, Type t, int nodeType)
        {
            Name = name;
            IdentificationName = t.Name;
            NodeType = nodeType;
        }

        public T Create()
        {
            return new T();
        }

        public string IdentificationName
        {
            get;
            set;
        }

        public string Name
        {
            get;
            set;
        }

        public int NodeType
        {
            get;
            set;
        }

        private void SetNodeType(Type t)
        {
            NodeType = Create().NodeType();
        }
    }
}