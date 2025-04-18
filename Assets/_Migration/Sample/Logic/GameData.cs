using System;
using System.Collections.Generic;

namespace _Migration
{
    [Serializable]
    public class GameData
    {
        public string Version = "0.0.0";
        public string FirstName;
        public string LastName;
        public List<Resource> Resources = new();
    }
    
    [Serializable]
    public class Resource
    {
        public string Name;
        public int Amount;
    }
}
