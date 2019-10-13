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

            var reader = new StreamReader("output.txt");
            var outputStr = reader.ReadToEnd();

            if(outputStr == null || outputStr == "")
            {
                Console.WriteLine("output‚ª–¢“ü—Í‚Å‚·B");
                Assert.Fail();
                return;
            }

            var exList = outputStr.Split('~');
            exList = exList.Select(ss => ss.Trim()).ToArray();

            using (var vInput = new StreamReader("input.txt"))
            using (var vOutput = new StringWriter())
            {
                Console.SetIn(vInput);
                Console.SetOut(vOutput);

                foreach (var expected in exList)
                {
                    s.Solve();
                    Assert.AreEqual(expected, vOutput.ToString().Trim());
                    vOutput.GetStringBuilder().Clear();
                }
            }
        }
    }
}
