using Xunit;
using VsLocalizedIntellisense.Models.Service.CommandShell;
using VsLocalizedIntellisense.Models.Service.CommandShell.Redirect;

namespace VsLocalizedIntellisense.Test.Models.Service.CommandShell
{
    public class ActionBaseTest
    {
        private class TestAction: ActionBase
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

        [Fact]
        public void GetStatement_normal_Test()
        {
            var test = new TestAction();
            var actual = test.GetStatement();
            Assert.Equal("test", actual);
        }

        [Fact]
        public void GetStatement_input_Test()
        {
            var test = new TestAction() {
                Input = "INPUT",
            };

            var actual1 = test.GetStatement();
            Assert.Equal("test", actual1);

            var actual2 = test.ToStatement(new IndentContext());
            Assert.Equal("test < INPUT", actual2);
        }

        [Fact]
        public void GetStatement_pipe_Test()
        {
            var test = new TestAction() {
                Pipe = new TestAction()
            };
            var actual1 = test.GetStatement();
            Assert.Equal("test", actual1);

            var actual2 = test.ToStatement(new IndentContext());
            Assert.Equal("test | test", actual2);
        }

        [Fact]
        public void GetStatement_pipe2_Test()
        {
            var test = new TestAction() {
                Pipe = new TestAction() {
                    Pipe = new TestAction(),
                }
            };
            var actual1 = test.GetStatement();
            Assert.Equal("test", actual1);

            var actual2 = test.ToStatement(new IndentContext());
            Assert.Equal("test | test | test", actual2);
        }


        [Fact]
        public void GetStatement_null_redirect_Test()
        {
            var test = new TestAction {
                Redirect = OutputRedirect.Null
            };
            var actual1 = test.GetStatement();
            Assert.Equal("test", actual1);

            var actual2 = test.ToStatement(new IndentContext());
            Assert.Equal("test > NUL", actual2);
        }

        [Fact]
        public void GetStatement_null_null_redirect_Test()
        {
            var test = new TestAction {
                Redirect = OutputRedirect.NullWithError
            };
            var actual1 = test.GetStatement();
            Assert.Equal("test", actual1);

            var actual2 = test.ToStatement(new IndentContext());
            Assert.Equal("test > NUL 2>&1", actual2);
        }

        [Fact]
        public void GetStatement_no_append_redirect_Test()
        {
            var test = new TestAction {
                Redirect = new OutputRedirect() {
                    Target = "TARGET"
                }
            };
            var actual = test.ToStatement(new IndentContext());
            Assert.Equal("test > TARGET", actual);
        }

        [Fact]
        public void GetStatement_append_redirect_Test()
        {
            var test = new TestAction() {
                Redirect = new OutputRedirect() {
                    Append = true,
                    Target = "TARGET"
                }
            };
            var actual = test.ToStatement(new IndentContext());
            Assert.Equal("test >> TARGET", actual);
        }

        [Fact]
        public void GetStatement_pipe_redirect_Test()
        {
            var test = new TestAction() {
                Redirect = new OutputRedirect() {
                    Target = "SELF"
                },
                Pipe = new TestAction() {
                    Redirect = new OutputRedirect() {
                        Target = "PIPE"
                    }
                }
            };
            var actual = test.ToStatement(new IndentContext());
            Assert.Equal("test | test > PIPE", actual);
        }

        [Fact]
        public void GetStatement_input_pipe_redirect_Test()
        {
            var test = new TestAction() {
                Input = "INPUT",
                Redirect = new OutputRedirect() {
                    Target = "SELF"
                },
                Pipe = new TestAction() {
                    Redirect = new OutputRedirect() {
                        Target = "PIPE"
                    }
                }
            };
            var actual = test.ToStatement(new IndentContext());
            Assert.Equal("test < INPUT | test > PIPE", actual); //実際に動くかは知らん
        }

        #endregion
    }
}
