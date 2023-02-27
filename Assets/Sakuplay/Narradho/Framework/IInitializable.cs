using System.Collections.Generic;
using UnityEngine;

namespace Sakuplay.Narradho.Framework
{
    public interface IInitializable
    {
        void Initialize(List<object> objects);
    }
}