using Microsoft.VisualStudio.TestTools.UnitTesting;
using DidExpress;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DidExpress.Tests {
    [TestClass()]
    public class SelectToyTests {
        [TestMethod()]
        public void SelectByAgeTest() {
            List<Toy> expected = new List<Toy>() {
                new Toy(18, "Ведмедик", "Коричневий", 8, 2),
                new Toy(19, "Машинка", "Червоний", 8, 1),
                new Toy(21, "Трактор", "Жовтий", 8, 2),
                new Toy(22, "Бумеранг", "Білий", 8, 5)
            };

            int searchAge = 8;

            List<Toy> actual = SelectToy.SelectByAge(searchAge);

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