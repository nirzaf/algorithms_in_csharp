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

using Microsoft.VisualStudio.TestTools.UnitTesting;
using CSFundamentalAlgorithms.StringDataStructures;
using System.Collections.Generic;

// TODO: Add more tests with other strings. 

namespace CSFundamentalAlgorithmsTests.StringDataStructuresTests
{
    [TestClass]
    public class SuffixTreeTests
    {
        [TestMethod]
        public void Test1()
        {
            string text = "banana";
            SuffixTreeNode root = SuffixTree.Build(text);
            CheckSuffixTreeProperties(root, text);

            Assert.IsTrue(root.IsRoot);
            Assert.IsFalse(root.IsLeaf);
            Assert.IsFalse(root.IsIntermediate);
            Assert.AreEqual(string.Empty, root.StringValue);
            Assert.AreEqual(-1, root.StartIndex);
            Assert.AreEqual(3, root.Children.Count);

            SuffixTreeNode rootChild1 = root.Children[0]; /* a */
            SuffixTreeNode rootChild2 = root.Children[1]; /* na */
            SuffixTreeNode rootChild3 = root.Children[2];/* banana$ */

            Assert.IsTrue(rootChild1.IsIntermediate);
            Assert.IsFalse(rootChild1.IsRoot);
            Assert.IsFalse(rootChild1.IsLeaf);
            Assert.AreEqual("a", rootChild1.StringValue);
            Assert.AreEqual(-1, rootChild1.StartIndex);
            Assert.AreEqual(2, rootChild1.Children.Count);
            SuffixTreeNode childA1 = rootChild1.Children[0]; /* $ */
            SuffixTreeNode childA2 = rootChild1.Children[1]; /* na */
            Assert.IsFalse(childA1.IsIntermediate);
            Assert.IsFalse(childA1.IsRoot);
            Assert.IsTrue(childA1.IsLeaf);
            Assert.AreEqual("$", childA1.StringValue);
            Assert.AreEqual(5, childA1.StartIndex);
            Assert.AreEqual(0, childA1.Children.Count);
            Assert.IsTrue(childA2.IsIntermediate);
            Assert.IsFalse(childA2.IsRoot);
            Assert.IsFalse(childA2.IsLeaf);
            Assert.AreEqual("na", childA2.StringValue);
            Assert.AreEqual(-1, childA2.StartIndex);
            Assert.AreEqual(2, childA2.Children.Count);
            SuffixTreeNode childA2NA1 = childA2.Children[0]; /* $ */
            SuffixTreeNode childA2NA2 = childA2.Children[1]; /* na$ */
            Assert.IsFalse(childA2NA1.IsIntermediate);
            Assert.IsFalse(childA2NA1.IsRoot);
            Assert.IsTrue(childA2NA1.IsLeaf);
            Assert.AreEqual("$", childA2NA1.StringValue);
            Assert.AreEqual(3, childA2NA1.StartIndex);
            Assert.AreEqual(0, childA2NA1.Children.Count);
            Assert.IsFalse(childA2NA2.IsIntermediate);
            Assert.IsFalse(childA2NA2.IsRoot);
            Assert.IsTrue(childA2NA2.IsLeaf);
            Assert.AreEqual("na$", childA2NA2.StringValue);
            Assert.AreEqual(1, childA2NA2.StartIndex);
            Assert.AreEqual(0, childA2NA2.Children.Count);



            Assert.IsTrue(rootChild2.IsIntermediate);
            Assert.IsFalse(rootChild2.IsRoot);
            Assert.IsFalse(rootChild2.IsLeaf);
            Assert.AreEqual("na", rootChild2.StringValue);
            Assert.AreEqual(-1, rootChild2.StartIndex);
            Assert.AreEqual(2, rootChild2.Children.Count);
            SuffixTreeNode childNA1 = rootChild2.Children[0]; /* $ */
            SuffixTreeNode childNA2 = rootChild2.Children[1]; /* na$ */
            Assert.IsFalse(childNA1.IsIntermediate);
            Assert.IsFalse(childNA1.IsRoot);
            Assert.IsTrue(childNA1.IsLeaf);
            Assert.AreEqual("$", childNA1.StringValue);
            Assert.AreEqual(4, childNA1.StartIndex);
            Assert.AreEqual(0, childNA1.Children.Count);
            Assert.IsFalse(childNA2.IsIntermediate);
            Assert.IsFalse(childNA2.IsRoot);
            Assert.IsTrue(childNA2.IsLeaf);
            Assert.AreEqual("na$", childNA2.StringValue);
            Assert.AreEqual(2, childNA2.StartIndex);
            Assert.AreEqual(0, childNA2.Children.Count);


            Assert.IsFalse(rootChild3.IsIntermediate);
            Assert.IsFalse(rootChild3.IsRoot);
            Assert.IsTrue(rootChild3.IsLeaf);
            Assert.AreEqual("banana$", rootChild3.StringValue);
            Assert.AreEqual(0, rootChild3.StartIndex);
            Assert.AreEqual(0, rootChild3.Children.Count);
        }

        public void CheckSuffixTreeProperties(SuffixTreeNode root, string text)
        {
            List<SuffixTreeNode> nodes = new List<SuffixTreeNode>();
            GetNodes(root, nodes);
            
            int leafCounter = 0;
            int rootCounter = 0;
            SuffixTreeNode rootNode = null;
            List<SuffixTreeNode> intermediateNodes = new List<SuffixTreeNode>();
            foreach (SuffixTreeNode node in nodes)
            {
                if (node.IsLeaf)
                    leafCounter++;
                if (node.IsRoot)
                {
                    rootCounter++;
                    rootNode = node;
                }
                if (node.IsIntermediate)
                {
                    intermediateNodes.Add(node);
                }
            }
            
            /* Property1: the suffix tree must contain exactly 'text.Length' leaf nodes. */
            Assert.AreEqual(text.Length, leafCounter);

            /* Property2: The tree must have exactly one root node. */
            Assert.AreEqual(1, rootCounter);
            Assert.IsTrue(ReferenceEquals(rootNode, root));

            /* Property3: Root's childrenCount is >= 0 */
            Assert.IsTrue(root.Children.Count >= 0);

            /* Property4: All intermediate nodes' childrenCount >= 2 */
            foreach(SuffixTreeNode node in intermediateNodes)
            {
                Assert.IsTrue(node.Children.Count >= 2);
            }
        }

        public void GetNodes(SuffixTreeNode root, List<SuffixTreeNode> nodes)
        {
            nodes.Add(root);
            foreach (SuffixTreeNode node in root.Children)
            {
                GetNodes(node, nodes);
            }
        }

    }
}
