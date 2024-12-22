using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject
{
    public interface IAttackRangeCheck
    {
        bool ADRange(Entity _target);

        bool APRange(Entity _target);
    }
}
