using System;

namespace SquidCraft.MoLang
{
    public class MoLangObject
    {
        private MoLangAccess _access;

        public MoLangObject(MoLangAccess access)
        {
            _access = access;
        }

        public void AddFunction<TResult, T1>(string name, Func<TResult, T1> action)
        {
        }

        public void AddFunction<TResult, T1, T2>(string name, Func<TResult, T1, T2> action)
        {
        }

        public void AddFunction<TResult, T1, T2, T3>(string name, Func<TResult, T1, T2, T3> action)
        {
        }
    }
}