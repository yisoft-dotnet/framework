// ===============================================================================
// Website: https://yi.team/
// Copyright © Yi.TEAM. All rights reserved.
// ===============================================================================

using System.Collections;
using System.Reflection;

namespace Yisoft.Framework.Collections.Generic
{
    internal class Proxy
    {
        public object Key;
        public ArrayList List;

        public Proxy(IList bla)
        {
            List = new ArrayList(bla);

            var pi = bla.GetType().GetTypeInfo().GetProperty("Key");

            Key = pi?.GetValue(bla, null);
        }
    }
}