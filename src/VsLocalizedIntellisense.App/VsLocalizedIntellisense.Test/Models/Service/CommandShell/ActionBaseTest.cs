using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using VsLocalizedIntellisense.Models.Service.CommandShell;

namespace VsLocalizedIntellisense.Test.Models.Service.CommandShell
{
    [TestClass]
    public class ActionBaseTest
    {
        private class TestAction : ActionBase
        {
            public TestAction()
            {
                CommandName = "test";
            }

            public override string GetStatement()
            {
                return CommandName;
            }
        }

        #region function

        [TestMethod]
        public void GetStatement_normal_Test()
        {
            var test = new TestAction();
            var actual = test.GetStatement();
            Assert.AreEqual("test", actual);
        }

        [TestMethod]
        public void GetStatement_input_Test()
        {
            var test = new TestAction()
            {
                Input = "INPUT",
            };

            var actual1 = test.GetStatement();
            Assert.AreEqual("test", actual1);

            var actual2 = test.ToStatement(new IndentContext());
            Assert.AreEqual("test < INPUT", actual2);
        }

        [TestMethod]
        public void GetStatement_pipe_Test()
        {
            var test = new TestAction()
            {
                Pipe = new TestAction()
            };
            var actual1 = test.GetStatement();
            Assert.AreEqual("test", actual1);

            var actual2 = test.ToStatement(new IndentContext());
            Assert.AreEqual("test | test", actual2);
        }


        [TestMethod]
        public void GetStatement_null_redirect_Test()
        {
            var test = new TestAction
            {
                Redirect = Redirect.Null
            };
            var actual1 = test.GetStatement();
            Assert.AreEqual("test", actual1);

            var actual2 = test.ToStatement(new IndentContext());
            Assert.AreEqual("test > NUL", actual2);
        }

        [TestMethod]
        public void GetStatement_no_append_redirect_Test()
        {
            var test = new TestAction
            {
                Redirect = new Redirect()
                {
                    Target = "TARGET"
                }
            };
            var actual = test.ToStatement(new IndentContext());
            Assert.AreEqual("test > TARGET", actual);
        }

        [TestMethod]
        public void GetStatement_append_redirect_Test()
        {
            var test = new TestAction()
            {
                Redirect = new Redirect()
                {
                    Append = true,
                    Target = "TARGET"
                }
            };
            var actual = test.ToStatement(new IndentContext());
            Assert.AreEqual("test >> TARGET", actual);
        }

        [TestMethod]
        public void GetStatement_pipe_redirect_Test()
        {
            var test = new TestAction()
            {
                Redirect = new Redirect()
                {
                    Target = "SELF"
                },
                Pipe = new TestAction()
                {
                    Redirect = new Redirect()
                    {
                        Target = "PIPE"
                    }
                }
            };
            var actual = test.ToStatement(new IndentContext());
            Assert.AreEqual("test | test > PIPE", actual);
        }

        [TestMethod]
        public void GetStatement_input_pipe_redirect_Test()
        {
            var test = new TestAction()
            {
                Input = "INPUT",
                Redirect = new Redirect()
                {
                    Target = "SELF"
                },
                Pipe = new TestAction()
                {
                    Redirect = new Redirect()
                    {
                        Target = "PIPE"
                    }
                }
            };
            var actual = test.ToStatement(new IndentContext());
            Assert.AreEqual("test < INPUT | test > PIPE", actual); //実際に動くかは知らん
        }

        #endregion
    }
}
