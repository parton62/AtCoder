using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace AtCoder.Executors
{
    class Executor
    {
        private Action<string[]> _main;
        protected Probrem Probrem { get; set; }

        public void Initialize(Type type)
        {
            SetProbrem(type);
            InitializeInternal();
        }
        protected virtual void InitializeInternal()
        {

        }

        private void SetProbrem(Type type)
        {
            _main = GetMain(type);
            Probrem = CreateProbrem(type);
        }

        
        public virtual void Execute()
        {
            ExecuteMain();
        }
        protected void ExecuteMain()
        {
            _main.Invoke(Environment.GetCommandLineArgs());
        }

        private Action<string[]> GetMain(Type type)
        {
            var main = type.GetMethod("Main", BindingFlags.Static | BindingFlags.NonPublic);
            return args => main.Invoke(null, new object[] { default(string[]) });
        }

        private Probrem CreateProbrem(Type type)
        {
            return new Probrem()
            {
                Contest = type.Namespace.Replace("AtCoder.Contests.", ""),
                ID = type.Name[0]
            };
        }
    }
}
