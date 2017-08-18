using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyMath;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMath.Tests
{
    [TestClass()]
    public class ProgramTests
    {
        private TestContext context;
        public TestContext TestContext
        {
            get { return context; }
            set { context = value; }
        }

        [TestMethod()]
        //[DeploymentItem("UnitTestProject1\\TestDataSource.mdb")] //  MyMath_Strings UnitTestProject1\\ UnitTestProject1\\testdatasource.mdb
        [DataSource("MyJetDataSource")]
        public void MyTestMethod()
        {
            int a = Int32.Parse(context.DataRow["Arg1"].ToString());
            int b = Int32.Parse(context.DataRow["Arg2"].ToString());
            Assert.AreNotEqual(a, b, "A value was equal.");
        }

        [TestMethod()]
        //[DeploymentItem("C:\\AccessDB\\data.xlsx")] //UnitTestProject1\\data.xlsx
        [DataSource("MyExcelDataSource")]
        public void MyTestMethod2()
        {
            Assert.AreEqual(context.DataRow["Val1"], context.DataRow["Val2"]);
        }




    }
}