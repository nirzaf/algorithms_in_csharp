﻿/* 
 * Copyright (c) 2019 (PiJei) 
 * 
 * This file is part of CSFundamentalAlgorithms project.
 *
 * CSFundamentalAlgorithms is free software: you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation, either version 3 of the License, or
 * (at your option) any later version.
 *
 * CSFundamentalAlgorithms is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 *
 * You should have received a copy of the GNU General Public License
 * along with CSFundamentalAlgorithms.  If not, see <http://www.gnu.org/licenses/>.
 */

using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CSFundamentalAlgorithms.SortingAlgorithms.StabilityCheckableVersions;
using CSFundamentalAlgorithms.SortingAlgorithms;

namespace CSFundamentalAlgorithmsTests.SortingAlgorithmsTests.StabilityCheckableVersionsTests
{
    [TestClass]
    public class UtilsTests
    {
        [TestMethod]
        public void Utils_Convert_Test()
        {
            List<int> values = new List<int>() { 4, 3, 2, 4, 1 };
            List<Element> newValues = Utils.Convert(values);
            Assert.AreEqual(newValues.Count, values.Count);
            for (int i = 0; i < values.Count; i++)
            {
                Assert.AreEqual(values[i], newValues[i].Value);
                Assert.AreEqual(i, newValues[i].FirstArrayIndex);
                Assert.AreEqual(-1, newValues[i].LatestArrayIndex);
            }
        }

        [TestMethod]
        public void Utils_IsMapStable_Test()
        {
            var element1 = new Element(4, 0);
            element1.Move(4);

            var element2 = new Element(4, 3);
            element2.Move(3);

            Dictionary<Element, List<Element>> map = new Dictionary<Element, List<Element>>();

            map.Add(element1, new List<Element> { element1, element2 });
            Assert.IsFalse(Utils.IsMapStable(map));

            element1.Move(3);
            element2.Move(4);

            Assert.IsTrue(Utils.IsMapStable(map));
        }

        [TestMethod]
        public void Utils_HashListToIndexes_Test()
        {
            Dictionary<Element, List<Element>> map = Utils.HashListToIndexes(null);
            Assert.AreEqual(0, map.Keys.Count);

            var element1 = new Element(4, 0);
            var element2 = new Element(2, 1);
            var element3 = new Element(3, 2);
            var element4 = new Element(4, 3);
            var element5 = new Element(1, 4);

            List<Element> values1 = new List<Element> {
                element1,
                element2,
                element3,
                element4,
                element5
            };

            Dictionary<Element, List<Element>> map1 = Utils.HashListToIndexes(values1);

            Assert.AreEqual(4, map1.Keys.Count);
            Assert.AreEqual(2, map1[element1].Count);
            Assert.AreEqual(0, map1[element1][0].FirstArrayIndex);
            Assert.AreEqual(3, map1[element1][1].FirstArrayIndex);

            Assert.AreEqual(1, map1[element2][0].FirstArrayIndex);
            Assert.AreEqual(2, map1[element3][0].FirstArrayIndex);
            Assert.AreEqual(4, map1[element5][0].FirstArrayIndex);
        }

        [TestMethod]
        public void Utils_Swap_Test()
        {
            List<Element> values = new List<Element>();
            var element1 = new Element(10, 0);
            var element2 = new Element(5, 1);
            var element3 = new Element(16, 2);
            var element4 = new Element(3, 3);
            values.Add(element1);
            values.Add(element2);
            values.Add(element3);
            values.Add(element4);

            Utils.Swap(values, 0, 2);

            Assert.AreEqual(10, values[2].Value);
            Assert.AreEqual(0, values[2].FirstArrayIndex);
            Assert.AreEqual(2, values[2].LatestArrayIndex);

            Assert.AreEqual(16, values[0].Value);
            Assert.AreEqual(2, values[0].FirstArrayIndex);
            Assert.AreEqual(0, values[0].LatestArrayIndex);

            Utils.Swap(values, 0, 3);

            Assert.AreEqual(3, values[0].Value);
            Assert.AreEqual(3, values[0].FirstArrayIndex);
            Assert.AreEqual(0, values[0].LatestArrayIndex);

            Assert.AreEqual(16, values[3].Value);
            Assert.AreEqual(2, values[3].FirstArrayIndex);
            Assert.AreEqual(3, values[3].LatestArrayIndex);

        }
    }
}