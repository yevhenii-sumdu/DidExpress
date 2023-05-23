using Microsoft.VisualStudio.TestTools.UnitTesting;
using DidExpress;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DidExpress.Tests {
    [TestClass()]
    public class DataAccessTests {
        [TestMethod()]
        public void LoadBagsTest() {
            List<int> expected = new List<int>() { 2, 1, 5 };

            List<int> actual = DataAccess.LoadBags();

            for (int i = 0; i < expected.Count; i++) {
                Assert.AreEqual(expected[i], actual[i]);
            }
        }

        [TestMethod()]
        public void LoadToysFromBagTest() {
            List<Toy> expected = new List<Toy>() {
                new Toy(19, "Машинка", "Червоний", 8, 1),
                new Toy(20, "Літачок", "Жовтий", 10, 1)
            };

            int searchBag = 1;

            List<Toy> actual = DataAccess.LoadToysFromBag(searchBag);

            for (int i = 0; i < expected.Count; i++) {
                Assert.AreEqual(expected[i].Id, actual[i].Id);
                Assert.AreEqual(expected[i].Name, actual[i].Name);
                Assert.AreEqual(expected[i].Color, actual[i].Color);
                Assert.AreEqual(expected[i].Age, actual[i].Age);
                Assert.AreEqual(expected[i].Bag, actual[i].Bag);
            }
        }
    }
}