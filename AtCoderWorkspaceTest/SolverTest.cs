using Microsoft.VisualStudio.TestTools.UnitTesting;
using AtCoderWorkspace;
using System.IO;
using System;
using System.Linq;

namespace AtCoderWorkspaceTest
{
    [TestClass]
    public class SolverTest
    {
        [TestMethod]
        public void TestMethod()
        {
            var s = new Solver();

            // output取得
            var outputStr = new StreamReader("output.txt");
            var ansList = outputStr.ReadToEnd().Split('~');
            ansList = ansList.Select(ss => ss.Trim()).ToArray();

            // アサート用疑似標準入出力
            using (var vInput = new StreamReader("input.txt"))
            using (var vOutput = new StringWriter())
            {
                Console.SetIn(vInput);
                Console.SetOut(vOutput);

                s.Input();
                Assert.AreEqual(ansList[0], vOutput.ToString().Trim());
                vOutput.GetStringBuilder().Clear();

                s.Input();
                Assert.AreEqual(ansList[1], vOutput.ToString().Trim());
                vOutput.GetStringBuilder().Clear();

                if (ansList.Length > 2)
                {
                    s.Input();
                    Assert.AreEqual(ansList[2], vOutput.ToString().Trim());
                    vOutput.GetStringBuilder().Clear();
                }
            }
        }
    }
}
