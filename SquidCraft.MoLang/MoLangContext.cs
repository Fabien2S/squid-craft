using System.Collections.Generic;

namespace SquidCraft.MoLang
{
    public class MoLangContext
    {
        private readonly Dictionary<string, MoLangObject> _objects = new Dictionary<string, MoLangObject>();

        public MoLangObject GetObject(string name)
        {
            return _objects[name];
        }

        public void AddObject(string name, MoLangObject o)
        {
            _objects[name] = o;
        }
    }
}