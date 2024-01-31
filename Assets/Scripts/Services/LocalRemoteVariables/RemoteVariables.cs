using System;
using System.Collections.Generic;

namespace Services.LocalRemoteVariables
{
    [Serializable]
    public class RemoteVariables
    {
        [Serializable]
        public class RemoteVariable
        {
            public string VariableKey;
            public string Value;
        }

        public List<RemoteVariable> data = new();
    }
}