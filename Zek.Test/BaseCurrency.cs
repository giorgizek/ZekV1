using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zek.Test
{
    public abstract class BaseCurrency
    {
        public abstract Func.Gender CentsGender { get; }

        public abstract Func.Gender Gender { get; }
    }
}
