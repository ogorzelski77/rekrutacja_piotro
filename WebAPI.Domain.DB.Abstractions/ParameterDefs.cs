using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Domain.Db.Abstrasctions
{
    public abstract class UpdateParameter<T>
    {
        private T value;
        public T Value 
        { 
            get => value;
        }
        public UpdateParameter(T value)
        {
            this.value = value;
        }
    }
    public class AddParameter<T> : UpdateParameter<T>
    {
        public AddParameter(T value) : base(value)
        {}
    }

    public class SubtractParameter<T> : UpdateParameter<T>
    {
        public SubtractParameter(T value) : base(value)
        {}
    }

    public class SubtractColumn<T> : UpdateParameter<T>
    {
        public SubtractColumn(T value) : base(value)
        {}
    }

    public class MultiplyByParameter<T> : UpdateParameter<T>
    {
        public MultiplyByParameter(T value) : base(value)
        {}
    }

    public class DivideByParameter<T> : UpdateParameter<T>
    {
        public DivideByParameter(T value) : base(value)
        {}
    }

    public class DivideByColumn<T> : UpdateParameter<T>
    {
        public DivideByColumn(T value) : base(value)
        {}
    }
}