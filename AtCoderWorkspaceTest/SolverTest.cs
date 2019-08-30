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

            // output�擾
            var outputStr = new StreamReader("output.txt");
            var ansList = outputStr.ReadToEnd().Split('~');
            ansList = ansList.Select(ss => ss.Trim()).ToArray();

            // �A�T�[�g�p�^���W�����o��
            using (var vInput = new StreamReader("input.txt"))
            using (var vOutput = new StringWriter())
            {
                Console.SetIn(vInput);
                Console.SetOut(vOutput);

                foreach (var expected in ansList)
                {
                    s.Solve();
                    Assert.AreEqual(expected, vOutput.ToString().Trim());
                    vOutput.GetStringBuilder().Clear();
                }
            }
        }
    }
}
