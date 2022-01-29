using Microsoft.VisualStudio.TestTools.UnitTesting;
using OOPClassBasicsTesterLibrary;
using System;

namespace Bibliotheek.Tests
{
    [TestClass]
    public class UnitTest1
    {
        TimsEpicClassAnalyzer tester = new TimsEpicClassAnalyzer(new BibBoek());

        [TestMethod]
        public void TestUitgeleend()
        {

            tester.CheckFullProperty("Uitgeleend", typeof(System.DateTime),propType: TimsEpicClassAnalyzer.PropertyTypes.PublicSetPrivateGet);
          
            DateTime current = (DateTime)tester.GetProp("Uitgeleend");
            TimeSpan diff =  DateTime.Now - current;
            Assert.IsTrue(diff < new TimeSpan(0, 0, 2), "Uitgeleend werd niet op de huidige tijd gezet tijdens objectcreatie");
        }

        [TestMethod]
        public void TestInleverDatum()
        {
            tester.CheckFullProperty("InleverDatum", typeof(System.DateTime), propType: TimsEpicClassAnalyzer.PropertyTypes.NoSet);

            DateTime correct = DateTime.Now.AddDays(14).Date;

            Assert.AreEqual(correct, ((DateTime)tester.GetProp("InleverDatum")).Date, "Inleverdatum geeft niet de datum 14 dagen na de Uitgeleend datum terug");
            
        }

        [TestMethod]
        public void TestOntlener()
        {
   
            tester.CheckAutoProperty("Ontlener", typeof(string));
            Assert.AreEqual("onbekend", tester.GetProp("Ontlener"), $"Ontlener prop moet op onbekend staan aan de start");
        }

        [TestMethod]
        public void TestVerlengTermijn()
        {

            tester.CheckMethod("VerlengTermijn", typeof(void), new Type[] { typeof(int) }); ;
           
            tester.TestMethod("VerlengTermijn", new object[] { 5 });
            DateTime current = DateTime.Now.AddDays(5);
            Assert.AreEqual(current.Date, ((DateTime)tester.GetProp("Uitgeleend")).Date, "Verlengtermijn lijkt niet te werken. Ik vroeg 5 dagen extra verlengtermijn, maar Uitgeleend gaf vervolgens verkeerde waarde.");
        }
    }
}
