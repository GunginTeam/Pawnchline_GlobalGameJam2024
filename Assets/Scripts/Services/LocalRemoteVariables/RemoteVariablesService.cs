using System.Collections.Generic;
using UnityEngine;

namespace Services.LocalRemoteVariables
{
    public class RemoteVariablesService : IRemoteVariablesService
    {
        private readonly Dictionary<string, string> _remoteVariables = new();

        public RemoteVariablesService(TextAsset dependencies)
        {
            var serializedData = JsonUtility.FromJson<RemoteVariables>(dependencies.ToString());

            foreach (var remoteVariable in serializedData.data)
            {
                _remoteVariables.Add(remoteVariable.VariableKey, remoteVariable.Value);
            }
        }
        
        public string GetString(string variableKey)
        { 
            if (!_remoteVariables.ContainsKey(variableKey))
            {
                Debug.LogError($"Remote Variable with key {variableKey} is not defined!");
                return null;
            }

            return _remoteVariables[variableKey];
        }
    }
}
